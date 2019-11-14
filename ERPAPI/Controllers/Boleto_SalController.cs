using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
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
    [Route("api/Boleto_Sal")]
    [ApiController]
    public class Boleto_SalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Boleto_SalController(ILogger<Boleto_SalController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Boleto_Sal paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_SalPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Boleto_Sal> Items = new List<Boleto_Sal>();
            try
            {
                var query = _context.Boleto_Sal.AsQueryable();
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
        /// Obtiene el Listado de Boleto_Sales 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_Sal()
        {
            List<Boleto_Sal> Items = new List<Boleto_Sal>();
            try
            {
                Items = await _context.Boleto_Sal.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int64>> GetBoleto_SalCount()
        {
            // List<Boleto_Sal> Items = new List<Boleto_Sal>();
            Boleto_Sal _Boleto_Sal = new Boleto_Sal();
            Int64 Total = 0;
            try
            {
                // Items = await _context.Boleto_Sal.ToListAsync();
                _Boleto_Sal = await _context.Boleto_Sal.FromSql("select  count(clave_e) clave_e  from Boleto_Sal ").FirstOrDefaultAsync();
                Total = _Boleto_Sal.clave_e;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Total);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_SalMax()
        {
            Int64? Max = 0;
            try
            {
                //Max = await _context.Boleto_Ent.Select(x => x.clave_e).DefaultIfEmpty(0).Max();
                Max = _context.Boleto_Sal.Max(x => x.clave_e);
                if (Max == null) { Max = 0; }
            }
            catch (Exception ex)
            {
                Max = 0;
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> Ok(Max));
                //return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Max));
        }

        /// <summary>
        /// Obtiene los Datos de la Boleto_Sal por medio del Id enviado.
        /// </summary>
        /// <param name="clave_e"></param>
        /// <returns></returns>
        [HttpGet("[action]/{clave_e}")]
        public async Task<IActionResult> GetBoleto_SalById(Int64 clave_e)
        {
            Boleto_Sal Items = new Boleto_Sal();
            try
            {
                Items = await _context.Boleto_Sal.Where(q => q.clave_e == clave_e).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_SalByClaveEList([FromBody]List<Int64> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {
                //string listadosalidas = string.Join(",", clave_e_list);
                _context.Database.SetCommandTimeout(60);

                List<Int64> _encontrados = await _context.Boleto_Sal.Select(q => q.clave_e).ToListAsync();
                Items = clave_e_list.Except(_encontrados).ToList(); 
                // Items = await _context.Boleto_Sal.Where(q => clave_e_list.Contains(q.clave_e)).Select(q => q.clave_e).ToListAsync();

                // Items = await _context.Boleto_Sal.Any(q => q.clave_e == clave_e_list)();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_S_ByClassList([FromBody]List<Boleto_Sal> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {                
                try
                {                 
                    _context.BulkInsert(clave_e_list);
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {                   
                    throw ex;
                }
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }



        /// <summary>
        /// Inserta una nueva Boleto_Sal
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Boleto_Sal>> Insert([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = new Boleto_Sal();
            try
            {
                _Boleto_Salq = _Boleto_Sal;
                _context.Boleto_Sal.Add(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);
        }

        /// <summary>
        /// Actualiza la Boleto_Sal
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Boleto_Sal>> Update([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = _Boleto_Sal;
            try
            {
                _Boleto_Salq = await (from c in _context.Boleto_Sal
                                 .Where(q => q.clave_e == _Boleto_Sal.clave_e)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Boleto_Salq).CurrentValues.SetValues((_Boleto_Sal));

                //_context.Boleto_Sal.Update(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);
        }

        /// <summary>
        /// Elimina una Boleto_Sal       
        /// </summary>
        /// <param name="_Boleto_Sal"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Boleto_Sal _Boleto_Sal)
        {
            Boleto_Sal _Boleto_Salq = new Boleto_Sal();
            try
            {
                _Boleto_Salq = _context.Boleto_Sal
                .Where(x => x.clave_e == (Int64)_Boleto_Sal.clave_e)
                .FirstOrDefault();

                _context.Boleto_Sal.Remove(_Boleto_Salq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Salq);

        }







    }
}