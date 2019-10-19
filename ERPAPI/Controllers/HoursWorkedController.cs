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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/HoursWorked")]
    [ApiController]
    public class HoursWorkedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public HoursWorkedController(ILogger<HoursWorkedController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de HoursWorked paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetHoursWorkedPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<HoursWorked> Items = new List<HoursWorked>();
            try
            {
                var query = _context.HoursWorked.AsQueryable();
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
        /// Obtiene el Listado de HoursWorkedes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetHoursWorked()
        {
            List<HoursWorked> Items = new List<HoursWorked>();
            try
            {
                Items = await _context.HoursWorked.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la HoursWorked por medio del Id enviado.
        /// </summary>
        /// <param name="IdHorastrabajadas"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdHorastrabajadas}")]
        public async Task<ActionResult<HoursWorked>> GetHoursWorkedById(Int64 IdHorastrabajadas)
        {
            HoursWorked Items = new HoursWorked();
            try
            {
                Items = await _context.HoursWorked.Where(q => q.IdHorastrabajadas == IdHorastrabajadas).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva HoursWorked
        /// </summary>
        /// <param name="hoursworked"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]HoursWorked hoursworked)
        {
            HoursWorked HoursWorked = hoursworked;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.HoursWorked.Add(HoursWorked);
                        //await _context.SaveChangesAsync();

                        foreach (var item in hoursworked.idhorastrabajadasconstrains)
                        {
                            item.IdHorasTrabajadas = hoursworked.IdHorastrabajadas;
                            _context.HoursWorkedDetail.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = HoursWorked.IdHorastrabajadas,
                            DocType = "HoursWorked",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(hoursworked, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(HoursWorked, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = HoursWorked.UsuarioCreacion,
                            UsuarioModificacion = HoursWorked.UsuarioModificacion,
                            UsuarioEjecucion = HoursWorked.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                // this.UpdateSalesOrder(salesOrder.SalesOrderId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(HoursWorked));
        }

        /// <summary>
        /// Actualiza la HoursWorked
        /// </summary>
        /// <param name="_HoursWorked"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<HoursWorked>> Update([FromBody]HoursWorked _HoursWorked)
        {
            HoursWorked _HoursWorkedq = _HoursWorked;
            try
            {
                _HoursWorkedq = await (from c in _context.HoursWorked
                                 .Where(q => q.IdHorastrabajadas == _HoursWorked.IdHorastrabajadas)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_HoursWorkedq).CurrentValues.SetValues((_HoursWorked));
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedq));
        }

        /// <summary>
        /// Elimina una HoursWorked       
        /// </summary>
        /// <param name="_HoursWorked"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]HoursWorked _HoursWorked)
        {
            HoursWorked _HoursWorkedq = new HoursWorked();
            try
            {
                _HoursWorkedq = _context.HoursWorked
                .Where(x => x.IdHorastrabajadas == (Int64)_HoursWorked.IdHorastrabajadas)
                .FirstOrDefault();

                _context.HoursWorked.Remove(_HoursWorkedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_HoursWorkedq));

        }







    }
}