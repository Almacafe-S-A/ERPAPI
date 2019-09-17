using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Kardex")]
    [ApiController]
    public class KardexController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public KardexController(ILogger<KardexController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Kardex paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetKardexPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Kardex> Items = new List<Kardex>();
            try
            {
                var query = _context.Kardex.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de Kardexes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetKardex()
        {
            List<Kardex> Items = new List<Kardex>();
            try
            {
                Items = await _context.Kardex.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la Kardex por medio del Id enviado.
        /// </summary>
        /// <param name="KardexId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{KardexId}")]
        public async Task<IActionResult> GetKardexById(Int64 KardexId)
        {
            Kardex Items = new Kardex();
            try
            {
                Items = await _context.Kardex.Where(q => q.KardexId == KardexId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetSaldoProductoByCertificado([FromBody]Kardex _Kardexq)
        {
            List<KardexLine> _kardexproduct = new List<KardexLine>();
            try
            {
                Int64 KardexId = await _context.Kardex
                                              .Where(q => q.DocumentId == _Kardexq.DocumentId)
                                              .Where(q => q.DocumentName == _Kardexq.DocumentName)
                                              .Select(q => q.KardexId)
                                              .MaxAsync();

                _kardexproduct = await (_context.KardexLine
                                              .Where(q => q.KardexId == KardexId)                                              
                                             )
                                              .ToListAsync();

               // _kardexproduct = await _context.KardexLine.Where(q => q.SubProducId == _Kardexq.CurrencyId).FirstOrDefaultAsync();
                

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

           
            return await Task.Run(() => Ok(_kardexproduct));
        }

        /// <summary>
        /// Inserta una nueva Kardex
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Kardex>> Insert([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = new Kardex();
            try
            {
                _Kardexq = _Kardex;
                _context.Kardex.Add(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));
        }

        /// <summary>
        /// Actualiza la Kardex
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Kardex>> Update([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = _Kardex;
            try
            {
                _Kardexq = await (from c in _context.Kardex
                                 .Where(q => q.KardexId == _Kardex.KardexId)
                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Kardexq).CurrentValues.SetValues((_Kardex));

                //_context.Kardex.Update(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));
        }

        /// <summary>
        /// Elimina una Kardex       
        /// </summary>
        /// <param name="_Kardex"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Kardex _Kardex)
        {
            Kardex _Kardexq = new Kardex();
            try
            {
                _Kardexq = _context.Kardex
                .Where(x => x.KardexId == (Int64)_Kardex.KardexId)
                .FirstOrDefault();

                _context.Kardex.Remove(_Kardexq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Kardexq));

        }







    }
}