﻿using System;
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
using ERPAPI.Helpers;
using Newtonsoft.Json;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/CheckAccountLines")]
    [ApiController]
    public class CheckAccountLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CheckAccountLinesController(ILogger<CheckAccountLinesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CheckAccountLineses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCheckAccountLines()
        {
            List<CheckAccountLines> Items = new List<CheckAccountLines>();
            try
            {
                Items = await _context.CheckAccountLines.ToListAsync();
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
        /// Obtiene los Datos de la CheckAccountLines por medio del Id enviado.
        /// </summary>
        /// <param name="CheckAccountLinesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountLinesId}")]
        public async Task<IActionResult> GetCheckAccountLinesById(Int64 CheckAccountLinesId)
        {
            CheckAccountLines Items = new CheckAccountLines();
            try
            {
                Items = await _context.CheckAccountLines
                    .Include( i => i.JournalEntry)
                    .Include(i => i.CheckAccount.AccountManagement)
                    .Where(q => q.Id == CheckAccountLinesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el detalle de las mercaderias.
        /// </summary>
        /// <param name="CheckAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountId}")]
        public async Task<IActionResult> GetCheckAccountLinesByCheckAccountId(Int64 CheckAccountId)
        {
            List<CheckAccountLines> Items = new List<CheckAccountLines>();
            try
            {
                Items = await _context.CheckAccountLines                             
                             .Where(q => q.CheckAccountId == CheckAccountId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva CheckAccountLines
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CheckAccountLines>> Insert([FromBody]dynamic _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            CheckAccountLines check = new CheckAccountLines();
            try
            {

                _CheckAccountLinesq = JsonConvert.DeserializeObject<CheckAccountLines>(_CheckAccountLines.ToString());

                List<JournalEntryLine> journalEntryLines = _CheckAccountLinesq.JournalEntry.JournalEntryLines;

                
                check = new CheckAccountLines
                {
                    CheckAccountId = _CheckAccountLinesq.CheckAccountId,

                    CheckNumber = _CheckAccountLinesq.CheckNumber,
                    Address = _CheckAccountLinesq.Address,
                    PaytoOrderOf = _CheckAccountLinesq.PaytoOrderOf,
                    RTN = _CheckAccountLinesq.RTN,
                    Ammount = _CheckAccountLinesq.Ammount,
                    Sinopsis = _CheckAccountLinesq.Sinopsis,
                    Date = _CheckAccountLinesq.Date,
                    RetencionId = _CheckAccountLinesq.RetencionId,
                    Place = _CheckAccountLinesq.Place,
                    Estado = "Pendiente de Aprobacion",
                    IdEstado = 97,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _CheckAccountLinesq.UsuarioCreacion,
                    UsuarioModificacion = _CheckAccountLinesq.UsuarioModificacion,
                    Impreso = false,
                    PartyId = _CheckAccountLinesq.PartyId,
                    PartyTypeId = _CheckAccountLinesq.PartyTypeId
                    


                };



                int actual = Convert.ToInt32(check.CheckNumber);

                //Conteo Cheques
                CheckAccount chequera = await _context.CheckAccount.Where(c =>c.CheckAccountId == check.CheckAccountId).FirstOrDefaultAsync();
                chequera.NumeroActual = (actual+1).ToString();
                if (actual > Convert.ToInt32(chequera.NoFinal))
                {
                    return BadRequest("No se pueden emitir más Cheques.");

                }
                else
                {
                   
                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;
                    check.AmountWords = let.ToCustomCardinal((check.Ammount)).ToUpper();                    
                    _context.CheckAccountLines.Add(check);
                    chequera.NumeroActual = (actual + 1).ToString();
                    await _context.SaveChangesAsync();


                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = check.Id,
                        DocType = "JournalEntry",
                        ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(check, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insertar Asiento",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = check.UsuarioCreacion,
                        UsuarioModificacion = check.UsuarioModificacion,
                        UsuarioEjecucion = check.UsuarioModificacion,

                    });

                    foreach (var item in journalEntryLines)
                    {
                        item.CreatedUser = _CheckAccountLinesq.UsuarioCreacion;
                        item.ModifiedUser = _CheckAccountLinesq.UsuarioCreacion;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        item.JournalEntryLineId = 0;


                    }
                    decimal suma = journalEntryLines.Sum(s => s.Debit);
                    int numcheque = 0;
                    try
                    {
                        numcheque = Convert.ToInt32(check.CheckNumber);

                    }
                    catch (Exception)
                    {

                        numcheque = 0;
                    }

                    JournalEntry _je = new JournalEntry
                    {
                        Date = check.Date,
                        //Memo = $"Cheque Numero {check.CheckNumber} ",
                        Memo = $"Cheque Numero {check.CheckNumber}  " + _CheckAccountLinesq.Sinopsis,                        
                        DatePosted = check.Date,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = check.UsuarioModificacion,
                        CreatedUser = check.UsuarioCreacion,
                        //PartyId = Convert.ToInt32(_VendorInvoiceq.VendorId),
                        PartyName = check.PaytoOrderOf,
                        DocumentId =numcheque,
                        TotalDebit = suma,
                        TotalCredit = suma,
                        PartyTypeId = 3,
                        //PartyName = "Proveedor",
                        TypeJournalName = "Cheques",
                        VoucherType = 10,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobacion",
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Asiento diario",


                    };



                    Periodo periodoactivo = new Periodo();
                    periodoactivo = await _context.Periodo.Where(q => q.Estado == "Abierto").FirstOrDefaultAsync();
                    _je.PeriodoId = periodoactivo.Id;
                    _je.Periodo = periodoactivo.Anio.ToString();


                    _je.JournalEntryLines.AddRange(journalEntryLines);

                    _context.JournalEntry.Add(_je);

                    await _context.SaveChangesAsync();

                    check.JournalEntrId = _je.JournalEntryId;

                    await _context.SaveChangesAsync();


                    BitacoraWrite _writeje = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = _je.JournalEntryId,
                        DocType = "JournalEntry",
                        ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_je, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Asiento Contable de Cheque",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _je.CreatedUser,
                        UsuarioModificacion = _je.ModifiedUser,
                        UsuarioEjecucion = _je.ModifiedUser,

                    });

                    await _context.SaveChangesAsync();
                }
                


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(check));
        }

        /// <summary>
        /// Actualiza la CheckAccountLines
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CheckAccountLines>> Update([FromBody]dynamic check)
        {
            CheckAccountLines _CheckAccountLines = new CheckAccountLines();
            _CheckAccountLines = JsonConvert.DeserializeObject<CheckAccountLines>(check.ToString());
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            List<JournalEntryLine> journalEntryLines = new List<JournalEntryLine>();
            JournalEntry journalEntry = new JournalEntry();
            try
            {
                _CheckAccountLinesq = await _context.CheckAccountLines
                    .Where(w => w.Id == _CheckAccountLines.Id).FirstOrDefaultAsync();

                foreach (var item in _CheckAccountLines.JournalEntry.JournalEntryLines)
                {
                    if (item.JournalEntryLineId == 0)
                    {
                        item.JournalEntryId = Convert.ToInt64(_CheckAccountLinesq.JournalEntrId ) ;
                        _context.JournalEntryLine.Add(item);
                    }
                    else
                    {
                        _context.JournalEntryLine.Update(item);
                    }
                }

                journalEntry = _context.JournalEntry.Include(j => j.JournalEntryLines).
                    Where(q => q.JournalEntryId == _CheckAccountLinesq.JournalEntrId).FirstOrDefault();


                journalEntry.PartyId = _CheckAccountLines.PartyId;
                journalEntry.PartyName = _CheckAccountLines.PaytoOrderOf;
                journalEntry.PartyTypeId = Convert.ToInt32(_CheckAccountLines.PartyTypeId);
                //journalEntry.JournalEntryLines = _CheckAccountLines.JournalEntry.JournalEntryLines;


                

                decimal suma = journalEntry.JournalEntryLines.Sum(s => s.Debit);

                journalEntry.TotalCredit = suma;
                journalEntry.TotalDebit = suma;

                //_CheckAccountLines.JournalEntry = null;
                //_CheckAccountLines.JournalEntry.JournalEntryLines = null;
                _CheckAccountLinesq.IdEstado = 97;
                _CheckAccountLinesq.Estado = "Pendiente Aprobacion";
                _CheckAccountLinesq.UsuarioModificacion = User.Identity.Name;
                _CheckAccountLinesq.FechaModificacion = DateTime.Now;
                _CheckAccountLinesq.Address = _CheckAccountLines.Address;
                _CheckAccountLinesq.Sinopsis = _CheckAccountLines.Sinopsis;
                _CheckAccountLinesq.PartyId = _CheckAccountLines.PartyId;
                _CheckAccountLinesq.PartyTypeId = _CheckAccountLines.PartyTypeId;
                _CheckAccountLinesq.Place = _CheckAccountLines.Place;
                _CheckAccountLinesq.Ammount = _CheckAccountLines.Ammount;
                _CheckAccountLinesq.PaytoOrderOf = _CheckAccountLines.PaytoOrderOf;
                Numalet let;
                let = new Numalet();
                let.SeparadorDecimalSalida = "Lempiras";
                let.MascaraSalidaDecimal = "00/100 ";
                let.ApocoparUnoParteEntera = true;
                _CheckAccountLinesq.AmountWords = let.ToCustomCardinal((_CheckAccountLines.Ammount)).ToUpper();


                //_context.Entry(_CheckAccountLinesq).CurrentValues.SetValues((_CheckAccountLines));

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _CheckAccountLinesq.Id,
                    DocType = "Cheques",
                    ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_CheckAccountLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Edivion de Cheque",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _CheckAccountLinesq.UsuarioCreacion,
                    UsuarioModificacion = _CheckAccountLinesq.UsuarioModificacion,
                    UsuarioEjecucion = _CheckAccountLinesq.UsuarioModificacion,

                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));
        }



        /// <summary>
        /// Actualiza el estado del Cheque a Impreso
        /// </summary>
        /// <param name="CheckId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckId}")]
        public async Task<ActionResult<CheckAccountLines>> SetImpreso(int CheckId)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            try
            {
                 _CheckAccountLinesq = await _context.CheckAccountLines
                                 .Where(q => q.Id == CheckId).FirstOrDefaultAsync();
                _CheckAccountLinesq.Impreso = true;
                bool cambio = false;
                
                if (_CheckAccountLinesq.IdEstado == 52) // si ya habia sido emitido se cambia emitido reimpreso 
                {
                    _CheckAccountLinesq.Estado = "Emitido - Reimpreso";
                    _CheckAccountLinesq.IdEstado = 100;
                    cambio = true;
                }
                if (_CheckAccountLinesq.IdEstado == 98) // si solamente estaba aprobado se pasa a emitido
                {
                    _CheckAccountLinesq.Estado = "Emitido";
                    _CheckAccountLinesq.IdEstado = 52;
                    cambio = true;
                }
                
                if (!cambio)
                {
                    return await Task.Run(() => Ok(_CheckAccountLinesq));
                }
                //_context.Entry(_CheckAccountLinesq).CurrentValues.SetValues((_CheckAccountLines));

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _CheckAccountLinesq.Id,
                    DocType = "CheckAccount",
                    ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_CheckAccountLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Impresion Cheque",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });

                //_context.CheckAccountLines.Update(_CheckAccountLinesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));
        }


        /// <summary>
        /// Actualiza el estado del Cheque a Impreso
        /// </summary>
        /// <param name="CheckId"></param>
        /// <param name="Aprobado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckId}/{Aprobado}")]
        public async Task<ActionResult<CheckAccountLines>> SetAprobadoRechazado(int CheckId, bool Aprobado)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            JournalEntry journalEntry = new JournalEntry();
            try
            {
                _CheckAccountLinesq = await (from c in _context.CheckAccountLines
                                .Where(q => q.Id == CheckId)
                                             select c
                               ).FirstOrDefaultAsync();
                journalEntry = await _context.JournalEntry.Include(j => j.JournalEntryLines).Where(j => j.JournalEntryId == _CheckAccountLinesq.JournalEntrId).FirstOrDefaultAsync();
                
                if (Aprobado)
                {
                    _CheckAccountLinesq.Estado = "Aprobado";
                    _CheckAccountLinesq.IdEstado = 98;
                    journalEntry.EstadoId = 6;
                    journalEntry.EstadoName = "Aprobado";
                    journalEntry.ApprovedBy = User.Identity.Name;
                    journalEntry.ApprovedDate = DateTime.Now;
                    ////Actualiza el saldo de las cuentas contables del catalogo 
                    ContabilidadHandler.ActualizarSaldoCuentas(_context, journalEntry);
                }
                else
                {
                    _CheckAccountLinesq.Estado = "Rechazado";
                    _CheckAccountLinesq.IdEstado = 99;
                    journalEntry.EstadoId = 7;
                    journalEntry.EstadoName = "Rechazado";

                }

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _CheckAccountLinesq.Id,
                    DocType = "CheckAccount",
                    ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(_CheckAccountLinesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = _CheckAccountLinesq.Estado + " Cheque",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });

                //_context.CheckAccountLines.Update(_CheckAccountLinesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));
        }

        /// <summary>
        /// Anula el cheque del id proporcionado reversando el asiento del cheque 
        /// </summary>
        /// <param name="checkid"></param>
        /// <returns></returns>
        [HttpGet("[action]/{checkid}")]
        public async Task<ActionResult<CheckAccountLines>> AnularCheque(int checkid)
        {
            
            
            try
            {
                CheckAccountLines _CheckAccountLinesq = await _context.CheckAccountLines
                    .Include(i => i.JournalEntry)
                    .Where(w => w.Id == checkid).FirstOrDefaultAsync();
                if (_CheckAccountLinesq == null)
                {
                    return NotFound("No se encontro el Cheque");
                }
                if (_CheckAccountLinesq.IdEstado == 53)
                {
                    return BadRequest("Este Cheque ya ha sido Anulado anteriormente");
                }
                if (_CheckAccountLinesq.IdEstado != 51 &&_CheckAccountLinesq.IdEstado != 52 && _CheckAccountLinesq.IdEstado !=  98 && _CheckAccountLinesq.IdEstado !=100 )
                    
                {
                    return BadRequest("Solo se pueden anular cheques de estado Emitido, Aprobado, o Emitido Reimpreso");
                }

                Periodo periodoactivo = new Periodo();
                periodoactivo = await _context.Periodo.Where(q => q.Estado == "Abierto").FirstOrDefaultAsync();

                _CheckAccountLinesq.AmountWords = "Anulado";
                _CheckAccountLinesq.Ammount = 0;
                _CheckAccountLinesq.Estado = "Anulado";
                _CheckAccountLinesq.IdEstado = 53;       
                

                if (_CheckAccountLinesq.RetencionId != null)
                {
                    RetentionReceipt retention = await _context.RetentionReceipt.Where(w => w.RetentionReceiptId == _CheckAccountLinesq.RetencionId).FirstOrDefaultAsync();
                    retention.IdEstado = 2;
                    retention.Estado = "Inactiva";
                }

                JournalEntry jecheck = _CheckAccountLinesq.JournalEntry;     
                
                if (jecheck == null)// Validacion en caso de que no se encuentre el asiento en el cheque, por validaciones anteriores, no aplica para nuevos cheques
                {
                    jecheck= await _context.JournalEntry.Where(w => w.DocumentId == _CheckAccountLinesq.Id && w.VoucherType == 10 && w.EstadoId == 6).FirstOrDefaultAsync();
                    _CheckAccountLinesq.JournalEntrId = jecheck == null ? 0 : jecheck.JournalEntryId;
                }
                if (jecheck != null )
                {
                    JournalEntry jeAnulacion = new JournalEntry
                    {
                        Date = DateTime.Now,
                        Memo = $"Anulación Cheque Numero {_CheckAccountLinesq.CheckNumber} ",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = _CheckAccountLinesq.UsuarioModificacion,
                        CreatedUser = _CheckAccountLinesq.UsuarioCreacion,
                        //PartyId = Convert.ToInt32(_VendorInvoiceq.VendorId),                    
                        PartyTypeName = _CheckAccountLinesq.PaytoOrderOf,
                        TotalDebit = jecheck.TotalDebit,
                        TotalCredit = jecheck.TotalCredit,
                        PartyTypeId = jecheck.PartyTypeId,
                        PartyName = jecheck.PartyName,
                        TypeJournalName = "Reversión",
                        VoucherType = 23,
                        EstadoId = 6,
                        EstadoName = "Aprobado",
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Asiento diario",
                        PeriodoId = periodoactivo.Id,
                        Periodo= periodoactivo.Anio.ToString(),

                    };
                    var lineas = await _context.JournalEntryLine.Where(w => w.JournalEntryId == jecheck.JournalEntryId).ToListAsync();

                    foreach (var item in lineas)
                    {
                        jeAnulacion.JournalEntryLines.Add(new JournalEntryLine
                        {
                            AccountId = item.AccountId,
                            AccountName = item.AccountName,
                            Debit = item.Credit,
                            Credit = item.Debit,
                            CostCenterId = item.CostCenterId,
                            CostCenterName = item.CostCenterName,
                            CreatedUser = _CheckAccountLinesq.UsuarioCreacion,
                            ModifiedUser = _CheckAccountLinesq.UsuarioCreacion,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                        });
                    }
                    _context.JournalEntry.Add(jeAnulacion);

                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = jeAnulacion.JournalEntryId,
                        DocType = "JournalEntry",
                        ClaseInicial =
                          Newtonsoft.Json.JsonConvert.SerializeObject(jeAnulacion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Anular",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = jeAnulacion.CreatedUser,
                        UsuarioModificacion = jeAnulacion.ModifiedUser,
                        UsuarioEjecucion = jeAnulacion.ModifiedUser,

                    });
                    await _context.SaveChangesAsync();
                    //Actualizar el saldo de las cuentas con el asiento reversado 
                    
                    ContabilidadHandler.ActualizarSaldoCuentas(_context, jeAnulacion);

                    _context.JournalEntryCanceled.Add(new JournalEntryCanceled()
                    {
                        CanceledJournalentryId = Convert.ToInt32(jecheck.JournalEntryId),
                        ReverseJournalEntryId = Convert.ToInt32(jeAnulacion.JournalEntryId),
                        DocumentId = _CheckAccountLinesq.Id,
                        VoucherType = 10,
                        TypeJournalName = "Cheques"


                    });
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("No se encontro el asiento contable del cheque seleccionado");
                }

                

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok());
        }




        /// <summary>
        /// Elimina una CheckAccountLines       
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            try
            {
                _CheckAccountLinesq = _context.CheckAccountLines
                .Where(x => x.Id == (Int64)_CheckAccountLines.Id)
                .FirstOrDefault();

                _context.CheckAccountLines.Remove(_CheckAccountLinesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));

        }







    }
}