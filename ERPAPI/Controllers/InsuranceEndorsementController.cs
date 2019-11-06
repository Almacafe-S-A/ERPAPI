using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceEndorsementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsuranceEndorsementController(ILogger<InsuranceEndorsementController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsuranceEndorsement paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceEndorsementPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsuranceEndorsement> Items = new List<InsuranceEndorsement>();
            try
            {
                var query = _context.InsuranceEndorsement.AsQueryable();
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
        /// Obtiene el Listado de InsuranceEndorsementes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceEndorsement()
        {
            List<InsuranceEndorsement> Items = new List<InsuranceEndorsement>();
            try
            {
                Items = await _context.InsuranceEndorsement.ToListAsync();
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
        /// Obtiene los Datos de la InsuranceEndorsement por medio del Id enviado.
        /// </summary>
        /// <param name="InsuranceEndorsementId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsuranceEndorsementId}")]
        public async Task<IActionResult> GetInsuranceEndorsementById(Int64 InsuranceEndorsementId)
        {
            InsuranceEndorsement Items = new InsuranceEndorsement();
            try
            {
                Items = await _context.InsuranceEndorsement.Where(q => q.InsuranceEndorsementId == InsuranceEndorsementId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva InsuranceEndorsement
        /// </summary>
        /// <param name="pInsuranceEndorsement"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsuranceEndorsement>> Insert([FromBody]InsuranceEndorsement pInsuranceEndorsement)
        {
            InsuranceEndorsement _InsuranceEndorsementq = new InsuranceEndorsement();
            _InsuranceEndorsementq = pInsuranceEndorsement;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;
                    

                    _context.InsuranceEndorsement.Add(_InsuranceEndorsementq);
                    //await _context.SaveChangesAsync();

                    JournalEntry _je = new JournalEntry
                    {
                        Date = _InsuranceEndorsementq.DateGenerated,
                        Memo = "Factura de Compra a Proveedores",
                        DatePosted = _InsuranceEndorsementq.DateGenerated,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = _InsuranceEndorsementq.UsuarioModificacion,
                        CreatedUser = _InsuranceEndorsementq.UsuarioCreacion,
                        //DocumentId = _InsuranceEndorsementq.InsuranceEndorsementId,
                    };

                    //Accounting account = await _context.Accounting.Where(acc => acc.AccountId == _InsuranceEndorsementq.AccountId).FirstOrDefaultAsync();
                    //_je.JournalEntryLines.Add(new JournalEntryLine
                    //{
                    //    AccountId = Convert.ToInt32(_InsuranceEndorsementq.AccountId),
                    //    //Description = _InsuranceEndorsementq.Account.AccountName,
                    //    Description = account.AccountName,
                    //    Credit = 0,
                    //    Debit = _InsuranceEndorsementq.Total,
                    //    CreatedDate = DateTime.Now,
                    //    ModifiedDate = DateTime.Now,
                    //    CreatedUser = _InsuranceEndorsementq.UsuarioCreacion,
                    //    ModifiedUser = _InsuranceEndorsementq.UsuarioModificacion,
                    //    Memo = "",
                    //});
                    //foreach (var item in _InsuranceEndorsementq.InsuranceEndorsementLines)
                    //{
                    //    account = await _context.Accounting.Where(acc => acc.AccountId == _InsuranceEndorsementq.AccountId).FirstOrDefaultAsync();
                    //    item.InsuranceEndorsementId = _InsuranceEndorsementq.Id;
                    //    _context.InsuranceEndorsementLine.Add(item);
                    //    _je.JournalEntryLines.Add(new JournalEntryLine
                    //    {
                    //        AccountId = Convert.ToInt32(item.AccountId),
                    //        Description = account.AccountName,
                    //        Credit = item.Total,
                    //        Debit = 0,
                    //        CreatedDate = DateTime.Now,
                    //        ModifiedDate = DateTime.Now,
                    //        CreatedUser = _InsuranceEndorsementq.UsuarioCreacion,
                    //        ModifiedUser = _InsuranceEndorsementq.UsuarioModificacion,
                    //        Memo = "",
                    //    });
                    //}

                    await _context.SaveChangesAsync();

                    double sumacreditos = 0, sumadebitos = 0;
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

                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = 4, ///////Falta definir los Id de las Operaciones
                        DocType = "InsuranceEndorsement",
                        ClaseInicial =
                        Newtonsoft.Json.JsonConvert.SerializeObject(_InsuranceEndorsementq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InsuranceEndorsementq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insert",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _InsuranceEndorsementq.UsuarioCreacion,
                        UsuarioModificacion = _InsuranceEndorsementq.UsuarioModificacion,
                        UsuarioEjecucion = _InsuranceEndorsementq.UsuarioModificacion,

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


            return await Task.Run(() => Ok(_InsuranceEndorsementq));
        }




        /// <summary>
        /// Actualiza la InsuranceEndorsement
        /// </summary>
        /// <param name="_InsuranceEndorsement"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsuranceEndorsement>> Update([FromBody]InsuranceEndorsement _InsuranceEndorsement)
        {
            InsuranceEndorsement _InsuranceEndorsementq = _InsuranceEndorsement;
            try
            {
                _InsuranceEndorsementq = await (from c in _context.InsuranceEndorsement
                                 .Where(q => q.InsuranceEndorsementId == _InsuranceEndorsement.InsuranceEndorsementId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InsuranceEndorsementq).CurrentValues.SetValues((_InsuranceEndorsement));

                //_context.InsuranceEndorsement.Update(_InsuranceEndorsementq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceEndorsementq));
        }

        /// <summary>
        /// Elimina una InsuranceEndorsement       
        /// </summary>
        /// <param name="_InsuranceEndorsement"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsuranceEndorsement _InsuranceEndorsement)
        {
            InsuranceEndorsement _InsuranceEndorsementq = new InsuranceEndorsement();
            try
            {
                _InsuranceEndorsementq = _context.InsuranceEndorsement
                .Where(x => x.InsuranceEndorsementId == (Int64)_InsuranceEndorsement.InsuranceEndorsementId)
                .FirstOrDefault();

                _context.InsuranceEndorsement.Remove(_InsuranceEndorsementq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceEndorsementq));

        }





    }
}
