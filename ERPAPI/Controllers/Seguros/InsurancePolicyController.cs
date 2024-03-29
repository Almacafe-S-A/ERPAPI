﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePolicyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsurancePolicyController(ILogger<InsurancePolicyController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Polizas por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancePolicyPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsurancePolicy> Items = new List<InsurancePolicy>();
            try
            {
                var query = _context.InsurancePolicy.AsQueryable();
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
        /// Obtiene el Listado de Polizas
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancePolicy()
        {
            List<InsurancePolicy> Items = new List<InsurancePolicy>();
            try
            {
                Items = await _context.InsurancePolicy.ToListAsync();
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
        /// Obtiene los Datos de la poliza por medio del Id enviado.
        /// </summary>
        /// <param name="IdInsurancePolicy"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdInsurancePolicy}")]
        public async Task<IActionResult> GetInsurancePolicyById(Int64 IdInsurancePolicy)
        {
            InsurancePolicy Items = new InsurancePolicy();
            try
            {
                Items = await _context.InsurancePolicy.Where(q => q.InsurancePolicyId == IdInsurancePolicy).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene las polizas de la Aseguradora
        /// </summary>
        /// <param name="IdInsuranceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdInsuranceId}")]
        public async Task<IActionResult> GetInsurancePolicyByInsuranceId(Int64 IdInsuranceId)
        {
            List<InsurancePolicy> Items = new List<InsurancePolicy>();
            try
            {
                Items = await _context.InsurancePolicy.Where(q => q.InsurancesId == IdInsuranceId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el listado de Polizas dependiendo si es poliza propia o de los clientes
        /// </summary>
        /// <param name="polizaPropia"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{polizaPropia}/{CustomerId}")]
        public async Task<IActionResult> GetInsuracesPoliciesByProperty(bool polizaPropia,int CustomerId)
        {
            List<InsurancePolicy> Items = new List<InsurancePolicy>();
            try
            {
                if (polizaPropia)
                {
                    Items = await _context.InsurancePolicy
                        .Where(q => q.Propias 
                        && q.PolicyDueDate.Date>=DateTime.Now.Date
                        && (q.EstadoId == 1||q.EstadoId ==101))
                        .ToListAsync();
                }
                else
                {
                    Items = await _context.InsurancePolicy
                    .Where(q => 
                    q.CustomerId == CustomerId
                    && q.PolicyDueDate.Date >= DateTime.Now.Date
                    && (q.EstadoId == 1 || q.EstadoId == 101))
                    .ToListAsync();
                }
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva poliza con asiento contable
        /// </summary>
        /// <param name="_InsurancePolicy"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsurancePolicy>> Insert([FromBody]InsurancePolicy _InsurancePolicy)
        {
            InsurancePolicy InsurancePolicyq = new InsurancePolicy();
            

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        InsurancePolicyq = _InsurancePolicy;
                        //InsurancePolicyq.EstadoId = 1;
                        //InsurancePolicyq.Estado = "Activo";

                        _context.InsurancePolicy.Add(InsurancePolicyq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();
                       

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = InsurancePolicyq.InsurancePolicyId,
                            DocType = "InsurancePolicy",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(InsurancePolicyq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                            UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                            UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(InsurancePolicyq));
        }

        /// <summary>
        /// Actualiza la poliza y hace una contra partida de ajuste
        /// </summary>
        /// <param name="_InsurancePolicy"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsurancePolicy>> AjusteDiferencialCambiario([FromBody]InsurancePolicy _InsurancePolicy)
        {
            InsurancePolicy InsurancePolicyq = _InsurancePolicy;
            InsurancePolicy _Original;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Original = await _context.InsurancePolicy.Where(q => q.InsurancePolicyId == _InsurancePolicy.InsurancePolicyId).FirstOrDefaultAsync();
                        InsurancePolicyq = await (from c in _context.InsurancePolicy
                        .Where(q => q.InsurancePolicyId == _InsurancePolicy.InsurancePolicyId)
                                                  select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(InsurancePolicyq).CurrentValues.SetValues((_InsurancePolicy));

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = InsurancePolicyq.InsurancePolicyId,
                            DocType = "InsurancePolicy",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(InsurancePolicyq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "AjusteDiferencialCambiario",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                            UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                            UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        if (_Original != null)
                        {
                            if (_Original.LpsAmount != _InsurancePolicy.LpsAmount || _Original.DollarAmount != _InsurancePolicy.DollarAmount)
                            {
                                JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                      .Where(q => q.TransactionId == 1)
                                                                      //.Where(q => q.BranchId == InsurancePolicyq.BranchId)
                                                                      .Where(q => q.EstadoName == "Activo")
                                                                      .Include(q => q.JournalEntryConfigurationLine)
                                                                      ).FirstOrDefaultAsync();

                                BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                                {
                                    IdOperacion = InsurancePolicyq.CustomerId,
                                    DocType = "JournalEntryConfiguration",
                                    ClaseInicial =
                                     Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                    ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                    Accion = "AjusteDiferencialCambiario",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                                    UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                                    UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

                                });

                                // await _context.SaveChangesAsync();

                                decimal sumacreditos = 0, sumadebitos = 0;
                                if (_journalentryconfiguration != null)
                                {
                                    //Crear el asiento contable configurado
                                    //.............................///////
                                    JournalEntry _je = new JournalEntry
                                    {
                                        Date = InsurancePolicyq.PolicyDate,
                                        Memo = "Contra Partidad de Poliza: Ajuste por diferencial cambiario",
                                        DatePosted = InsurancePolicyq.PolicyDate,
                                        ModifiedDate = DateTime.Now,
                                        CreatedDate = DateTime.Now,
                                        ModifiedUser = InsurancePolicyq.UsuarioModificacion,
                                        CreatedUser = InsurancePolicyq.UsuarioCreacion,
                                        DocumentId = InsurancePolicyq.InsurancePolicyId,
                                        TypeOfAdjustmentId = 65,
                                        VoucherType = 1,

                                    };



                                    foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                                    {

                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            AccountName = item.AccountName,
                                            Description = item.AccountName,
                                            Debit = item.DebitCredit == "Credito" ? (InsurancePolicyq.LpsAmount > 0 ? InsurancePolicyq.LpsAmount : InsurancePolicyq.DollarAmount) : 0,
                                            Credit = item.DebitCredit == "Debito" ? (InsurancePolicyq.LpsAmount > 0 ? InsurancePolicyq.LpsAmount : InsurancePolicyq.DollarAmount) : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = InsurancePolicyq.UsuarioCreacion,
                                            ModifiedUser = InsurancePolicyq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Debito" ? (InsurancePolicyq.LpsAmount > 0 ? InsurancePolicyq.LpsAmount : InsurancePolicyq.DollarAmount) : 0;
                                        sumadebitos += item.DebitCredit == "Credito" ? (InsurancePolicyq.LpsAmount > 0 ? InsurancePolicyq.LpsAmount : InsurancePolicyq.DollarAmount) : 0;

                                        // _context.JournalEntryLine.Add(_je);

                                    }


                                    if (sumacreditos != sumadebitos)
                                    {
                                        transaction.Rollback();
                                        _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                        return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                    }

                                    _je.TotalCredit = sumacreditos;
                                    _je.TotalDebit = sumadebitos;
                                    _context.JournalEntry.Add(_je);

                                    await _context.SaveChangesAsync();
                                }

                                _write = new BitacoraWrite(_context, new Bitacora
                                {
                                    IdOperacion = InsurancePolicyq.InsurancePolicyId,
                                    DocType = "InsurancePolicy",
                                    ClaseInicial =
                                   Newtonsoft.Json.JsonConvert.SerializeObject(InsurancePolicyq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                    Accion = "AjusteDiferencialCambiario",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                                    UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                                    UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

                                });

                                await _context.SaveChangesAsync();
                            }
                        }
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

            return await Task.Run(() => Ok(InsurancePolicyq));
        }

        /// <summary>
        /// Actualiza la poliza
        /// </summary>
        /// <param name="_InsurancePolicy"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsurancePolicy>> Update([FromBody]InsurancePolicy _InsurancePolicy)
        {
            InsurancePolicy InsurancePolicyq = _InsurancePolicy;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        
                        InsurancePolicyq = await _context.InsurancePolicy                            
                            .Where(q => q.InsurancePolicyId == _InsurancePolicy.InsurancePolicyId).FirstOrDefaultAsync();
                        if (InsurancePolicyq == null)
                        {
                            return NotFound();
                        }


                        if (_InsurancePolicy.Propias)
                        {
                            List<InsuranceEndorsement> endosos = await _context.InsuranceEndorsement
                                .Where(q => q.InsurancePolicyId == InsurancePolicyq.InsurancePolicyId).ToListAsync();
                            _context.InsuranceEndorsement.RemoveRange(endosos);
                        }
                        else
                        {
                            List<InsuredAssets> activos = await _context.InsuredAssets
                                .Where(q => q.InsurancePolicyId == InsurancePolicyq.InsurancePolicyId).ToListAsync();
                            _context.InsuredAssets.RemoveRange(activos);
                        }



                        _context.Entry(InsurancePolicyq).CurrentValues.SetValues((_InsurancePolicy));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = InsurancePolicyq.InsurancePolicyId,
                            DocType = "InsurancePolicy",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(InsurancePolicyq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                            UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                            UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(InsurancePolicyq));
        }



        /// <summary>
        /// Elimina una Poliza haciendo una contra partida
        /// </summary>
        /// <param name="_InsurancePolicy"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsurancePolicy _InsurancePolicy)
        {
            InsurancePolicy InsurancePolicyq = new InsurancePolicy();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        InsurancePolicyq = _context.InsurancePolicy
                        .Where(x => x.InsurancePolicyId == (Int64)_InsurancePolicy.InsurancePolicyId)
                        .FirstOrDefault();

                        _context.InsurancePolicy.Remove(InsurancePolicyq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = InsurancePolicyq.InsurancePolicyId,
                            DocType = "InsurancePolicy",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(InsurancePolicyq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = InsurancePolicyq.UsuarioCreacion,
                            UsuarioModificacion = InsurancePolicyq.UsuarioModificacion,
                            UsuarioEjecucion = InsurancePolicyq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(InsurancePolicyq));

        }
    }
}
