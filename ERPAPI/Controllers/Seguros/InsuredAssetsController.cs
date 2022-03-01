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
using ERPAPI.Contexts;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuredAssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsuredAssetsController(ILogger<InsuredAssetsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsuredAssets paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuredAssetsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsuredAssets> Items = new List<InsuredAssets>();
            try
            {
                var query = _context.InsuredAssets.AsQueryable();
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
        /// Obtiene el Listado de InsuredAssetses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuredAssets()
        {
            List<InsuredAssets> Items = new List<InsuredAssets>();
            try
            {
                Items = await _context.InsuredAssets.ToListAsync();
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
        /// Obtiene el Listado de InsuredAssetses 
        /// </summary>
        /// <param name="insurancePolicyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{insurancePolicyId}")]
        public async Task<IActionResult> GetInsuredAssetsByInsurancePolicyId( int insurancePolicyId)
        {
            List<InsuredAssets> Items = new List<InsuredAssets>();
            try
            {
                Items = await _context.InsuredAssets
                    .Include(b => b.Branch)
                    //.Include(e => e.Warehouse)
                    .Where(w => w.InsurancePolicyId == insurancePolicyId).ToListAsync();
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
        /// Obtiene los Datos de la InsuredAssets por medio del Id enviado.
        /// </summary>
        /// <param name="InsuredAssetsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsuredAssetsId}")]
        public async Task<IActionResult> GetInsuredAssetsById(Int64 InsuredAssetsId)
        {
            InsuredAssets Items = new InsuredAssets();
            try
            {
                Items = await _context.InsuredAssets.Where(q => q.Id == InsuredAssetsId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva InsuredAssets
        /// </summary>
        /// <param name="pInsuredAssets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsuredAssets>> Insert([FromBody]InsuredAssets pInsuredAssets)
        {
            InsuredAssets _InsuredAssetsq = new InsuredAssets();
            _InsuredAssetsq = pInsuredAssets;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;


                    _context.InsuredAssets.Add(_InsuredAssetsq);
                    //await _context.SaveChangesAsync();

                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,//_InsuredAssetsq.DateGenerated,
                        Memo = "Factura de Compra a Proveedores",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = _InsuredAssetsq.UsuarioModificacion,
                        CreatedUser = _InsuredAssetsq.UsuarioCreacion,
                        //DocumentId = _InsuredAssetsq.InsuredAssetsId,
                    };

                    //Accounting account = await _context.Accounting.Where(acc => acc.AccountId == _InsuredAssetsq.AccountId).FirstOrDefaultAsync();
                    //_je.JournalEntryLines.Add(new JournalEntryLine
                    //{
                    //    AccountId = Convert.ToInt32(_InsuredAssetsq.AccountId),
                    //    //Description = _InsuredAssetsq.Account.AccountName,
                    //    Description = account.AccountName,
                    //    Credit = 0,
                    //    Debit = _InsuredAssetsq.Total,
                    //    CreatedDate = DateTime.Now,
                    //    ModifiedDate = DateTime.Now,
                    //    CreatedUser = _InsuredAssetsq.UsuarioCreacion,
                    //    ModifiedUser = _InsuredAssetsq.UsuarioModificacion,
                    //    Memo = "",
                    //});
                    //foreach (var item in _InsuredAssetsq.InsuredAssetsLines)
                    //{
                    //    account = await _context.Accounting.Where(acc => acc.AccountId == _InsuredAssetsq.AccountId).FirstOrDefaultAsync();
                    //    item.InsuredAssetsId = _InsuredAssetsq.Id;
                    //    _context.InsuredAssetsLine.Add(item);
                    //    _je.JournalEntryLines.Add(new JournalEntryLine
                    //    {
                    //        AccountId = Convert.ToInt32(item.AccountId),
                    //        Description = account.AccountName,
                    //        Credit = item.Total,
                    //        Debit = 0,
                    //        CreatedDate = DateTime.Now,
                    //        ModifiedDate = DateTime.Now,
                    //        CreatedUser = _InsuredAssetsq.UsuarioCreacion,
                    //        ModifiedUser = _InsuredAssetsq.UsuarioModificacion,
                    //        Memo = "",
                    //    });
                    //}

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                    await _context.SaveChangesAsync();

                    decimal sumacreditos = 0, sumadebitos = 0;
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
                        DocType = "InsuredAssets",
                        ClaseInicial =
                        Newtonsoft.Json.JsonConvert.SerializeObject(_InsuredAssetsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InsuredAssetsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insert",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _InsuredAssetsq.UsuarioCreacion,
                        UsuarioModificacion = _InsuredAssetsq.UsuarioModificacion,
                        UsuarioEjecucion = _InsuredAssetsq.UsuarioModificacion,

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


            return await Task.Run(() => Ok(_InsuredAssetsq));
        }




        /// <summary>
        /// Actualiza la InsuredAssets
        /// </summary>
        /// <param name="_InsuredAssets"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsuredAssets>> Update([FromBody]InsuredAssets _InsuredAssets)
        {
            InsuredAssets _InsuredAssetsq = _InsuredAssets;
            try
            {
                _InsuredAssetsq = await (from c in _context.InsuredAssets
                                 .Where(q => q.Id == _InsuredAssets.Id)
                                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InsuredAssetsq).CurrentValues.SetValues((_InsuredAssets));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.InsuredAssets.Update(_InsuredAssetsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuredAssetsq));
        }

        /// <summary>
        /// Elimina una InsuredAssets       
        /// </summary>
        /// <param name="_InsuredAssets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsuredAssets _InsuredAssets)
        {
            InsuredAssets _InsuredAssetsq = new InsuredAssets();
            try
            {
                _InsuredAssetsq = _context.InsuredAssets
                .Where(x => x.Id == (Int64)_InsuredAssets.Id)
                .FirstOrDefault();

                _context.InsuredAssets.Remove(_InsuredAssetsq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuredAssetsq));

        }





    }
}
