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
//using ERPAPI.Migrations;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Conciliacion")]
    [ApiController]
    public class ConciliacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public ConciliacionController(ILogger<Conciliacion> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Conciliacion paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConciliacionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            try
            {    
                var query = _context.Conciliacion.AsQueryable();
                var totalRegistro = query.Count();

                List<Conciliacion> Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                   .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }
        
        /// <summary>
        /// Obtiene los Datos de la Conciliacion en una lista.
        /// </summary>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConciliacion()
        {
            try
            {
                List<Conciliacion> Items = await _context.Conciliacion.ToListAsync();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }


        /// <summary>
        /// Obtiene el Saldo de la cuenta hasta la fecha
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetSaldoLibrosCuenta([FromBody] dynamic dto)
        {
            try
            {

                int accountingId = dto.accountingId;
                DateTime fecha = dto.fecha;
                decimal debe = _context.JournalEntryLine.Include(i => i.JournalEntry)
                    .Where(q => q.AccountId == accountingId && q.JournalEntry.DatePosted<fecha && q.JournalEntry.EstadoId == 6).Sum(s => s.Debit);

                decimal haber = _context.JournalEntryLine.Include(i => i.JournalEntry)
                    .Where(q => q.AccountId == accountingId && q.JournalEntry.DatePosted < fecha && q.JournalEntry.EstadoId == 6).Sum(s => s.Credit);
                decimal saldo = debe - haber;
                                
                return await Task.Run(() => Ok(saldo));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetConciliacionById([FromRoute(Name = "Id")] Int64 id)
        {
            try
            {
                Conciliacion conciliacion = await _context.Conciliacion.Where(c=> c.ConciliacionId == id).FirstOrDefaultAsync();
                if (conciliacion != null)
                {
                    List<ConciliacionLinea> lineas =
                        await _context.ConciliacionLinea.Where(c => c.ConciliacionId == id).ToListAsync();
                    conciliacion.ConciliacionLinea = lineas;
                }
                return await Task.Run(() => Ok(conciliacion));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetConciliacionConCuenta()
        {
            try
            {
                var Items = from conciliaciones in _context.Conciliacion
                    join cuentas in _context.Accounting on conciliaciones.AccountId equals cuentas.AccountId
                    select new ConciliacionDTO(conciliaciones, cuentas.AccountCode + " - " + cuentas.AccountName);
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }

        /// <summary>
        /// Inserta una nueva Conciliacion
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Conciliacion>> Insert([FromBody]Conciliacion _Conciliacion)
        {   
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var linea in _Conciliacion.ConciliacionLinea)
                        {
                            if (linea.MotivoId == 0)
                            {
                                linea.MotivoId = null;
                            }

                            linea.VoucherTypeId = null;
                        }
                        _context.Conciliacion.Add(_Conciliacion);
                        //await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Conciliacion.ConciliacionId,
                            DocType = "Conciliacion",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Conciliacion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Conciliacion.UsuarioCreacion,
                            UsuarioModificacion = _Conciliacion.UsuarioModificacion,
                            UsuarioEjecucion = _Conciliacion.UsuarioModificacion,
                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Conciliacion));
        }
        
        /// <summary>
        /// Actualiza la Conciliacion
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Conciliacion>> Update([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion _Conciliacionq = _Conciliacion;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {


                        _Conciliacionq = await (from c in _context.Conciliacion
                                 .Where(q => q.ConciliacionId == _Conciliacion.ConciliacionId)
                                            select c
                                ).FirstOrDefaultAsync();
                        var lineas = await _context.ConciliacionLinea
                            .Where(l => l.ConciliacionId == _Conciliacion.ConciliacionId).ToListAsync();
                        _Conciliacionq.ConciliacionLinea = lineas;
                        _context.Entry(_Conciliacionq).CurrentValues.SetValues((_Conciliacion));
                        foreach (var linea in _Conciliacion.ConciliacionLinea.Where(l => l.ConciliacionLineaId != 0)
                            .ToList())
                        {
                            var lineaCon = _Conciliacionq.ConciliacionLinea.FirstOrDefault(
                                l => l.ConciliacionLineaId == linea.ConciliacionLineaId);
                            if(lineaCon != null)
                                lineaCon.Reconciled = linea.Reconciled;
                        }

                        foreach (var linea in _Conciliacion.ConciliacionLinea.Where(l=> l.ConciliacionLineaId == 0).ToList())
                        {
                            _Conciliacionq.ConciliacionLinea.Add(linea);
                        }
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Conciliacionq.ConciliacionId,
                            DocType = "Conciliacion",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Conciliacionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Conciliacionq.UsuarioCreacion,
                            UsuarioModificacion = _Conciliacionq.UsuarioModificacion,
                            UsuarioEjecucion = _Conciliacionq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Conciliacionq));
        }
  
    }
}