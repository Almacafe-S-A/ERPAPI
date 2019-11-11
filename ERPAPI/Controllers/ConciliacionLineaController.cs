using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EFCore.BulkExtensions;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ConciliacionLinea")]
    [ApiController]
    public class ConciliacionLineaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ConciliacionLineaController(ILogger<ConciliacionLinea> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene la ConciliacionLinea 
        /// por un id de ConciliacionLineaId
        /// </summary>
        /// <param name="ConciliacionLineaId"></param>
        /// <returns></returns>
        
        [HttpGet("[action]/{ConciliacionLineaId}")]
        public async Task<IActionResult> GetConciliacionLineaById(int ConciliacionLineaId)
        {
            ConciliacionLinea Items = new ConciliacionLinea();
            try
            {
                Items = await _context.ConciliacionLinea
                             .Where(q => q.ConciliacionLineaId == ConciliacionLineaId).FirstOrDefaultAsync();
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
        /// 
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>


        [HttpPost("[action]")]
        public async Task<IActionResult> InsertBulk([FromBody]dynamic _Conciliacion)
        {
            List<Int64> Items = new List<Int64>();

            _Conciliacion = JsonConvert.DeserializeObject<List<ConciliacionLinea>>(_Conciliacion.ToString());

            //List<Boleto_Ent> clave_e_list

            List<ConciliacionLinea> guardar = new List<ConciliacionLinea>();
                guardar = _Conciliacion;
                guardar[0].ReferenciaBancaria = "referencia";
                guardar[0].MonedaName = "MonedaF";
                guardar[0].UsuarioCreacion = "MARIO";
                //guardar[0].AccountId ;
                guardar[0].UsuarioModificacion = "aguilar";
                guardar[0].AccountId = 1;



               
            //guardar[0].Moneda = new Currency { CurrencyId = 1};
            //guardar[0].TipoTransaccion = new ElementoConfiguracion { Id = 1 };/////prueba conciliacion
            try
            {
                //using (var transaction = _context.Database.BeginTransaction())
                //{
                try
                {
                    foreach (var item in guardar)
                    {
                        item.FechaCreacion = DateTime.Now;
                        _context.ConciliacionLinea.Add(item);
                    }
                    
                    //_context.BulkInsert(guardar);
                    await _context.SaveChangesAsync();


                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    throw ex;
                }


                //  }
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
        /// Inserta una nueva JournalEntryLine
        /// </summary>
        /// <param name="_ConciliacionLinea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ConciliacionLinea>> Insert([FromBody]ConciliacionLinea _ConciliacionLinea)
        {
            ConciliacionLinea _ConciliacionLineaq = new ConciliacionLinea();
            try
            {
                _ConciliacionLineaq = _ConciliacionLinea;
                _context.ConciliacionLinea.Add(_ConciliacionLineaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConciliacionLineaq));
        }

        /// <summary>
        /// Actualiza la ConciliacionLinea
        /// </summary>
        /// <param name="_ConciliacionLinea"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ConciliacionLinea>> Update([FromBody]ConciliacionLinea _ConciliacionLinea)
        {
            ConciliacionLinea _ConciliacionLineaq = _ConciliacionLinea;
            try
            {
                _ConciliacionLineaq = await (from c in _context.ConciliacionLinea
                                 .Where(q => q.ConciliacionLineaId == _ConciliacionLinea.ConciliacionLineaId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ConciliacionLineaq).CurrentValues.SetValues((_ConciliacionLinea));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConciliacionLineaq));
        }

        /// <summary>
        /// Elimina una ConciliacionLinea       
        /// </summary>
        /// <param name="_ConciliacionLinea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ConciliacionLinea _ConciliacionLinea)
        {
            ConciliacionLinea _ConciliacionLineaq = new ConciliacionLinea();
            try
            {
                _ConciliacionLineaq = _context.ConciliacionLinea
                .Where(x => x.ConciliacionLineaId == _ConciliacionLinea.ConciliacionLineaId)
                .FirstOrDefault();

                _context.ConciliacionLinea.Remove(_ConciliacionLineaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConciliacionLineaq));

        }
    }
}