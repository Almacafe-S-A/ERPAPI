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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/RetentionReceipt")]
    [ApiController]
    public class RetentionReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public RetentionReceiptController(ILogger<RetentionReceiptController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Retenciones, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRetentionReceiptPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<RetentionReceipt> Items = new List<RetentionReceipt>();
            try
            {
                var query = _context.RetentionReceipt.AsQueryable();
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
        /// Obtiene el Listado de Retenciones, ordenado por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRetentionReceipt()
        {
            List<RetentionReceipt> Items = new List<RetentionReceipt>();
            try
            {
                Items = await _context.RetentionReceipt.OrderBy(b => b.RetentionReceiptId).ToListAsync();
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
        /// Obtiene los Datos de Retencion por medio del Id enviado.
        /// </summary>
        /// <param name="RetentionReceiptId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RetentionReceiptId}")]
        public async Task<IActionResult> GetRetentionReceiptById(Int64 RetentionReceiptId)
        {
            RetentionReceipt Items = new RetentionReceipt();
            try
            {
                Items = await _context.RetentionReceipt.Where(q => q.RetentionReceiptId == RetentionReceiptId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{RetentionReceiptId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 RetentionReceiptId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;//await _context.CheckAccount.Where(a => a.BankId == BankId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }



        /// <summary>
        /// Inserta una nueva retencion
        /// </summary>
        /// <param name="_RetentionReceipt"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<RetentionReceipt>> Insert([FromBody]RetentionReceipt _RetentionReceipt)
        {
            RetentionReceipt _RetentionReceiptq = new RetentionReceipt();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RetentionReceiptq = _RetentionReceipt;
                        RetentionReceipt _retentionreceipt = await _context.RetentionReceipt.Where(q => q.BranchId == _RetentionReceipt.BranchId)
                                                 .Where(q => q.IdPuntoEmision == _RetentionReceipt.IdPuntoEmision)
                                                 .FirstOrDefaultAsync();
                        if (_retentionreceipt != null)
                        {
                            _RetentionReceiptq.NumeroDEI = _context.RetentionReceipt.Where(q => q.BranchId == _RetentionReceipt.BranchId)
                                                  .Where(q => q.IdPuntoEmision == _RetentionReceipt.IdPuntoEmision).Max(q => q.NumeroDEI);
                        }

                        _RetentionReceiptq.NumeroDEI += 1;
                        
                        Int64 IdCai = await _context.NumeracionSAR
                                                 .Where(q => q.BranchId == _RetentionReceiptq.BranchId)
                                                 .Where(q => q.IdPuntoEmision == _RetentionReceiptq.IdPuntoEmision)
                                                 .Where(q => q.Estado == "Activo").Select(q => q.IdCAI).FirstOrDefaultAsync();


                        if (IdCai == 0)
                        {
                            return BadRequest("No existe un CAI activo para el punto de emisión");
                        }

                        _RetentionReceiptq.DueDate = await _context.NumeracionSAR
                                                     .Where(q => q.BranchId == _RetentionReceipt.BranchId)
                                                     .Where(q => q.IdCAI == IdCai)
                                                     .Where(q => q.IdPuntoEmision == _RetentionReceipt.IdPuntoEmision)
                                                     .Where(q => q.Estado == "Activo").Select(q => q.FechaLimite).FirstOrDefaultAsync();

                        _RetentionReceiptq.BranchCode = await _context.Branch.Where(q => q.BranchId == _RetentionReceipt.BranchId).Select(q => q.BranchCode).FirstOrDefaultAsync();
                        _RetentionReceiptq.CAI = await _context.CAI.Where(q => q.IdCAI == IdCai).Select(q => q._cai).FirstOrDefaultAsync();

                        _context.RetentionReceipt.Add(_RetentionReceiptq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RetentionReceiptq.RetentionReceiptId,
                            DocType = "RetentionReceipt",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RetentionReceiptq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RetentionReceiptq.UsuarioCreacion,
                            UsuarioModificacion = _RetentionReceiptq.UsuarioModificacion,
                            UsuarioEjecucion = _RetentionReceiptq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_RetentionReceiptq));
        }

        /// <summary>
        /// Actualiza la retencion
        /// </summary>
        /// <param name="_RetentionReceipt"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<RetentionReceipt>> Update([FromBody]RetentionReceipt _RetentionReceipt)
        {
            RetentionReceipt _RetentionReceiptq = _RetentionReceipt;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RetentionReceiptq = await (from c in _context.RetentionReceipt
                        .Where(q => q.RetentionReceiptId == _RetentionReceipt.RetentionReceiptId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_RetentionReceiptq).CurrentValues.SetValues((_RetentionReceipt));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RetentionReceiptq.RetentionReceiptId,
                            DocType = "RetentionReceipt",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RetentionReceiptq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RetentionReceiptq.UsuarioCreacion,
                            UsuarioModificacion = _RetentionReceiptq.UsuarioModificacion,
                            UsuarioEjecucion = _RetentionReceiptq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_RetentionReceiptq));
        }

        /// <summary>
        /// Elimina una retencion       
        /// </summary>
        /// <param name="_RetentionReceipt"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]RetentionReceipt _RetentionReceipt)
        {
            RetentionReceipt _RetentionReceiptq = new RetentionReceipt();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RetentionReceiptq = _context.RetentionReceipt
                        .Where(x => x.RetentionReceiptId == (Int64)_RetentionReceipt.RetentionReceiptId)
                        .FirstOrDefault();

                        _context.RetentionReceipt.Remove(_RetentionReceiptq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RetentionReceiptq.RetentionReceiptId,
                            DocType = "RetentionReceipt",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RetentionReceiptq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RetentionReceiptq.UsuarioCreacion,
                            UsuarioModificacion = _RetentionReceiptq.UsuarioModificacion,
                            UsuarioEjecucion = _RetentionReceiptq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_RetentionReceiptq));

        }
    }
}
