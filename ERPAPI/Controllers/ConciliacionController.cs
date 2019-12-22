﻿using System;
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
using ERPAPI.Migrations;
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


        /*
        /// <summary>
        /// Obtiene los Datos de la JournalEntryLine por medio del Conciliacion.
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetJournalEntryByDateAccount([FromBody]Conciliacion _ConciliacionP)
        {
            //  JournalEntry Items = new JournalEntry();
            List<ConciliacionLinea> LineConciliacionLinea = new List<ConciliacionLinea>();
            try
            {

                var consulta = from journale in _context.JournalEntry
                               join journalel in _context.JournalEntryLine
                               on journale.JournalEntryId equals journalel.JournalEntryId
                               where journale.Date >= _ConciliacionP.DateBeginReconciled &&
                                     journale.Date <= _ConciliacionP.DateEndReconciled

                select new ConciliacionLinea
                {
                   Debit = journalel.Debit,
                   Credit = journalel.Credit,
                   Monto = journalel.Debit - journalel.Credit,
                   JournalEntryId = journalel.JournalEntryId,
                   JournalEntryLineId = journalel.JournalEntryLineId,
                   FechaCreacion = DateTime.Now,
                   UsuarioCreacion = _ConciliacionP.UsuarioCreacion,
                   ConciliacionId = _ConciliacionP.ConciliacionId,
                   ReferenceTrans = journale.ReferenceNo,
                   VoucherTypeId = (int)journale.VoucherType,
                   TransDate = journale.Date,
                   CurrencyId = journale.CurrencyId,
                   MonedaName = _context.Currency.Where(
                       p => p.CurrencyId ==  journale.CurrencyId
                       ).FirstOrDefault().CurrencyName,
                };

                LineConciliacionLinea = consulta.ToList();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var NodeConciline in LineConciliacionLinea)
                        {
                            _context.ConciliacionLinea.Add(NodeConciline);
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                            {
                                IdOperacion = NodeConciline.ConciliacionId,
                                DocType = "Conciliacion",
                                ClaseInicial = Newtonsoft.Json.JsonConvert.SerializeObject(NodeConciline, 
                                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                Accion = "Insertar",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = NodeConciline.UsuarioCreacion,
                                UsuarioModificacion = NodeConciline.UsuarioModificacion,
                                UsuarioEjecucion = NodeConciline.UsuarioModificacion,

                            });
                        }
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(LineConciliacionLinea));
        }
        
        /// <summary>
        /// Obtiene los Datos de la Conciliacion por medio del Id enviado.
        /// </summary>
        /// <param name="ConciliacionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ConciliacionId}")]
        public async Task<IActionResult> GetConciliacionById(int ConciliacionId)
        {
            Conciliacion Items = new Conciliacion();
            try
            {
                Items = await _context.Conciliacion.Where(q => q.ConciliacionId == ConciliacionId)
                    .Include(q => q.ConciliacionLinea).FirstOrDefaultAsync();
                


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la Conciliacion por medio de la Fecha enviado.
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Conciliacion>> GetConciliacionByDate([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion Items = new Conciliacion();
            try
            {
                Items = await _context.Conciliacion.Where(
                        q => q.DateBeginReconciled >= _Conciliacion.DateBeginReconciled  &&
                             q.DateEndReconciled <= _Conciliacion.DateEndReconciled &&
                             q.AccountId == _Conciliacion.AccountId
                        )
                    .Include(q => q.ConciliacionLinea).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //return await Task.Run(() => Ok(_Conciliacionq));
            return await Task.Run(() => Ok(Items));
        }
        
        /// <summary>
        /// Inserta una nueva Conciliacion
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Conciliacion>> Insert([FromBody]Conciliacion _Conciliacion)
        {

            Conciliacion _Conciliacionq = new Conciliacion();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _Conciliacionq = _Conciliacion;
                        _context.Conciliacion.Add(_Conciliacionq);
                        _Conciliacionq.ConciliacionLinea = new List<ConciliacionLinea>();
                        //await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Conciliacionq.ConciliacionId,
                            DocType = "Conciliacion",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Conciliacionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Conciliacionq.UsuarioCreacion,
                            UsuarioModificacion = _Conciliacionq.UsuarioModificacion,
                            UsuarioEjecucion = _Conciliacionq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        if (_Conciliacionq.ConciliacionLinea == null)
                {
                    List<ConciliacionLinea> ArrayConciliacionLine = new List<ConciliacionLinea>();
                    //JournalEntryController JEcontroller = new JournalEntryController();
                    var arraylist = await GetJournalEntryByDateAccount(_Conciliacionq);
                    if (arraylist != null)
                        { 
                            _Conciliacionq.ConciliacionLinea = ((List<ConciliacionLinea>)arraylist);
                                foreach (var item in _Conciliacionq.ConciliacionLinea)
                                {
                                    item.ConciliacionId = _Conciliacionq.ConciliacionId;
                                    item.FechaCreacion = _Conciliacionq.FechaCreacion;
                                    item.FechaModificacion = _Conciliacionq.FechaModificacion;
                                    item.UsuarioCreacion = _Conciliacionq.UsuarioCreacion;
                                    item.UsuarioModificacion = _Conciliacionq.UsuarioModificacion;

                                    _context.ConciliacionLinea.Add(item);

                                    BitacoraWrite _writealert = new BitacoraWrite(_context, new Bitacora
                                    {
                                        IdOperacion = item.ConciliacionLineaId,
                                        DocType = "ConciliacionLinea",
                                        ClaseInicial =
                                          Newtonsoft.Json.JsonConvert.SerializeObject(item, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                        Accion = "Insertar",
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = item.UsuarioCreacion,
                                        UsuarioModificacion = item.UsuarioModificacion,
                                        UsuarioEjecucion = item.UsuarioModificacion,

                                    });

                                }
                
                        await _context.SaveChangesAsync();
                       }
                    }

    
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

                       _context.Entry(_Conciliacionq).CurrentValues.SetValues((_Conciliacion));

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

        /// <summary>
        /// Elimina una Conciliacion       
        /// </summary>
        /// <param name="_Conciliacion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Conciliacion _Conciliacion)
        {
            Conciliacion _Conciliacionq = new Conciliacion();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _Conciliacionq = _context.Conciliacion
                .Where(x => x.ConciliacionId == (Int64)_Conciliacion.ConciliacionId)
                .FirstOrDefault();

                _context.Conciliacion.Remove(_Conciliacionq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Conciliacionq.ConciliacionId,
                            DocType = "Conciliacion",
                            ClaseInicial =
    Newtonsoft.Json.JsonConvert.SerializeObject(_Conciliacionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
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
        */
    }
}