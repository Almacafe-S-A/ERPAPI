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
using ERPAPI.Helpers;
using Newtonsoft.Json;

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
                Items = await _context.CheckAccountLines.Where(q => q.Id == CheckAccountLinesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
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
        public async Task<ActionResult<CheckAccountLines>> Insert([FromBody]dynamic dto)
        {
            CheckAccountLinesDTO _CheckAccountLinesq = new CheckAccountLinesDTO();
            CheckAccountLines check = new CheckAccountLines();
            try
            {

                _CheckAccountLinesq = JsonConvert.DeserializeObject<CheckAccountLinesDTO>(dto.ToString());

                List<JournalEntryLine> journalEntryLines = _CheckAccountLinesq.JournalEntryLines;

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
                    Estado = "Emitido",
                    IdEstado = 51,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _CheckAccountLinesq.UsuarioCreacion,
                    UsuarioModificacion = _CheckAccountLinesq.UsuarioModificacion,

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
                    CheckAccount _CheckAccountq = await _context.CheckAccount.Where(q => q.CheckAccountId == check.CheckAccountId).FirstOrDefaultAsync();
                    _CheckAccountq.NumeroActual = (actual + 1).ToString();
                    if (_CheckAccountq.NumeroActual == _CheckAccountq.NoFinal)
                    {
                        //_CheckAccountq.
                    }

                    _context.Entry(_CheckAccountq).CurrentValues.SetValues((chequera));
                    await _context.SaveChangesAsync();

                    foreach (var item in journalEntryLines)
                    {
                        item.CreatedUser = _CheckAccountLinesq.UsuarioCreacion;
                        item.ModifiedUser = _CheckAccountLinesq.UsuarioCreacion;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;


                    }
                    double suma = journalEntryLines.Sum(s => s.Debit);

                    JournalEntry _je = new JournalEntry
                    {
                        Date = check.Date,
                        //Memo = $"Cheque Numero {check.CheckNumber} ",
                        Memo = _CheckAccountLinesq.Sinopsis,                        
                        DatePosted = check.Date,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = check.UsuarioModificacion,
                        CreatedUser = check.UsuarioCreacion,
                        //PartyId = Convert.ToInt32(_VendorInvoiceq.VendorId),
                        PartyName = check.PaytoOrderOf,
                        DocumentId = check.Id,
                        TotalDebit = suma,
                        TotalCredit = suma,
                        PartyTypeId = 3,
                        //PartyName = "Proveedor",
                        TypeJournalName = "Cheques",
                        VoucherType = 8,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobacion",
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Asiento diario"

                    };

                    _je.JournalEntryLines.AddRange(journalEntryLines);

                    _context.JournalEntry.Add(_je);

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
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CheckAccountLines>> Update([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = _CheckAccountLines;
            try
            {
                _CheckAccountLinesq = await (from c in _context.CheckAccountLines
                                 .Where(q => q.Id == _CheckAccountLines.Id)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CheckAccountLinesq).CurrentValues.SetValues((_CheckAccountLines));

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
        /// Actualiza la CheckAccountLines
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CheckAccountLines>> AnularCheque([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = _CheckAccountLines;
            
            try
            {
                _CheckAccountLinesq = await _context.CheckAccountLines.Where(w => w.Id == _CheckAccountLines.Id).FirstOrDefaultAsync();

                _CheckAccountLinesq.Estado = "Anulado";
                _CheckAccountLinesq.IdEstado = 53;
                //_context.Entry(_CheckAccountLinesq).CurrentValues.SetValues((_CheckAccountLines));

                if (_CheckAccountLinesq.RetencionId != null)
                {
                    RetentionReceipt retention = await _context.RetentionReceipt.Where(w => w.RetentionReceiptId == _CheckAccountLinesq.RetencionId).FirstOrDefaultAsync();
                    retention.IdEstado = 2;
                    retention.Estado = "Inactiva";
                }



                JournalEntry jecheck = await _context.JournalEntry.Where(w => w.DocumentId == _CheckAccountLinesq.Id && w.VoucherType == 8).FirstOrDefaultAsync();
                if (jecheck != null)
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
                        PartyTypeId = 3,
                        //PartyName = "Proveedor",
                        TypeJournalName = "Cheques",
                        VoucherType = 8,
                        EstadoId = 5,
                        EstadoName = "Enviada a Aprobacion",
                        TypeOfAdjustmentId = 65,
                        TypeOfAdjustmentName = "Asiento diario"



                    };

                    foreach (var item in jecheck.JournalEntryLines)
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
                }

                

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