using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarantiaBancariaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GarantiaBancariaController(ILogger<GarantiaBancariaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GarantiaBancaria
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGarantiaBancariaPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GarantiaBancaria> Items = new List<GarantiaBancaria>();
            try
            {
                var query = _context.GarantiaBancaria.AsQueryable();
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
        /// Obtiene el Listado de GarantiaBancariaes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGarantiaBancaria()
        {
            List<GarantiaBancaria> Items = new List<GarantiaBancaria>();
            try
            {
                Items = await _context.GarantiaBancaria.ToListAsync();
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
        /// Obtiene los Datos de la GarantiaBancaria por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetGarantiaBancariaById(Int64 Id)
        {
            GarantiaBancaria Items = new GarantiaBancaria();
            try
            {
                Items = await _context.GarantiaBancaria.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



       

        /// <summary>
        /// Inserta una nueva GarantiaBancaria
        /// </summary>
        /// <param name="_GarantiaBancaria"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GarantiaBancaria>> Insert([FromBody]GarantiaBancaria _GarantiaBancaria)
        {
            GarantiaBancaria _GarantiaBancariaq = new GarantiaBancaria();
            try
            {
                _GarantiaBancariaq = _GarantiaBancaria;
                _context.GarantiaBancaria.Add(_GarantiaBancariaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GarantiaBancariaq));
        }

        /// <summary>
        /// Actualiza la GarantiaBancaria
        /// </summary>
        /// <param name="_GarantiaBancaria"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GarantiaBancaria>> Update([FromBody]GarantiaBancaria _GarantiaBancaria)
        {
            GarantiaBancaria _GarantiaBancariaq = _GarantiaBancaria;
            try
            {
                _GarantiaBancariaq = await (from c in _context.GarantiaBancaria
                                 .Where(q => q.Id == _GarantiaBancaria.Id)
                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GarantiaBancariaq).CurrentValues.SetValues((_GarantiaBancaria));

                //_context.GarantiaBancaria.Update(_GarantiaBancariaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GarantiaBancariaq));
        }

        /// <summary>
        /// Elimina una GarantiaBancaria       
        /// </summary>
        /// <param name="_GarantiaBancaria"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GarantiaBancaria _GarantiaBancaria)
        {
            GarantiaBancaria _GarantiaBancariaq = new GarantiaBancaria();
            try
            {
                _GarantiaBancariaq = _context.GarantiaBancaria
                .Where(x => x.Id == (Int64)_GarantiaBancaria.Id)
                .FirstOrDefault();

                _context.GarantiaBancaria.Remove(_GarantiaBancariaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GarantiaBancariaq));

        }
    }
}
