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
                Items = await _context.InsuranceEndorsement.Include(i => i.CostCenter).ToListAsync();
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
        /// Obtiene el Listado de InsuranceEndorsementes de una Poliza 
        /// </summary>
        /// <param name="InsurancePolicyId"></param>       
        /// <returns></returns>
        [HttpGet("[action]/{InsurancePolicyId}")]
        public async Task<IActionResult> GetInsuranceEndorsementByInsurancePolicyId( int InsurancePolicyId)
        {
            List<InsuranceEndorsement> Items = new List<InsuranceEndorsement>();
            try
            {
                Items = await _context.InsuranceEndorsement
                    .Include(i => i.CostCenter)
                    .Where(w => w.InsurancePolicyId == InsurancePolicyId).ToListAsync();
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
                    _context.InsuranceEndorsement.Add(_InsuranceEndorsementq);
                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = 24, ///////Falta definir los Id de las Operaciones
                        DocType = "Endoso de Seguros",
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

                    JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                     .Where(q => q.TransactionId == 7)
                                                                     //.Where(q => q.BranchId == _InsuranceEndorsementq.BranchId)
                                                                     .Where(q => q.EstadoName == "Activo")
                                                                     .Include(q => q.JournalEntryConfigurationLine)
                                                                     ).FirstOrDefaultAsync();

                    

                    // await _context.SaveChangesAsync();

                    decimal sumacreditos = 0, sumadebitos = 0;
                    if (_journalentryconfiguration != null)
                    {
                        //Crear el asiento contable configurado
                        //.............................///////
                        JournalEntry _je = new JournalEntry
                        {
                            Date = _InsuranceEndorsementq.DateGenerated,
                            Memo = "Partidad Ingreso de Poliza",
                            DatePosted = _InsuranceEndorsementq.DateGenerated,
                            ModifiedDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            ModifiedUser = _InsuranceEndorsementq.UsuarioModificacion,
                            CreatedUser = _InsuranceEndorsementq.UsuarioCreacion,
                            DocumentId = _InsuranceEndorsementq.InsurancePolicyId,
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
                                Credit = item.DebitCredit == "Credito" ? _InsuranceEndorsementq.TotalAmountLp : 0,
                                Debit = item.DebitCredit == "Debito" ? _InsuranceEndorsementq.TotalAmountLp : 0,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                CreatedUser = _InsuranceEndorsementq.UsuarioCreacion,
                                ModifiedUser = _InsuranceEndorsementq.UsuarioModificacion,
                                Memo = "",
                            });

                            sumacreditos += item.DebitCredit == "Credito" ? _InsuranceEndorsementq.TotalAmountLp : 0;
                            sumadebitos += item.DebitCredit == "Debito" ? _InsuranceEndorsementq.TotalAmountLp : 0;

                            // _context.JournalEntryLine.Add(_je);

                        }


                        if (sumacreditos != sumadebitos)
                        {
                            transaction.Rollback();
                            //_logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            //return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            return BadRequest($"Error :No Coincide la suma del debe y el haber generada por el asiento automatico");
                        }

                        _je.TotalCredit = sumacreditos;
                        _je.TotalDebit = sumadebitos;
                        _context.JournalEntry.Add(_je);
                        //asientogenerado = _je.JournalEntryId;

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
                        BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InsuranceEndorsementq.InsuranceEndorsementId,
                            DocType = "Asiento Configurado para Endosos",
                            ClaseInicial =
                         Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertado Asiento de Endoso",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InsuranceEndorsementq.UsuarioCreacion,
                            UsuarioModificacion = _InsuranceEndorsementq.UsuarioModificacion,
                            UsuarioEjecucion = _InsuranceEndorsementq.UsuarioModificacion,

                        });

                        transaction.Commit();
                    }
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
