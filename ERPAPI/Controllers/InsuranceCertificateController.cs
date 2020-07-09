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
using ERPAPI.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceCertificateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsuranceCertificateController(ILogger<InsuranceCertificateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsuranceCertificate paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceCertificatePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsuranceCertificate> Items = new List<InsuranceCertificate>();
            try
            {
                var query = _context.InsuranceCertificate.AsQueryable();
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
        /// Obtiene el Listado de InsuranceCertificatees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceCertificate()
        {
            List<InsuranceCertificate> Items = new List<InsuranceCertificate>();
            try
            {
                Items = await _context.InsuranceCertificate.Include(i =>i.Customer).Include(c => c.ProductType).ToListAsync();
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
        /// Obtiene los Datos de la InsuranceCertificate por medio del Id enviado.
        /// </summary>
        /// <param name="InsuranceCertificateId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsuranceCertificateId}")]
        public async Task<IActionResult> GetInsuranceCertificateById(Int64 InsuranceCertificateId)
        {
            InsuranceCertificate Items = new InsuranceCertificate();
            try
            {
                Items = await _context.InsuranceCertificate.Where(q => q.Id == InsuranceCertificateId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        /// <summary>  
        /// Genera los Certificados de Seguro para el mes acutal.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GenerateInsuranceCertificates()
        {
            try
            {
                ////Actualiza como vencidos los demas Certificados 
                List<InsuranceCertificate> insuracesCerticates = _context.InsuranceCertificate.Where(q => q.EstadoId == 1).ToList();

                foreach (var certificado in insuracesCerticates)
                {
                    certificado.EstadoId = 2;
                }


                List<Customer> customers = await _context.Customer.Where(w => w.IdEstado == 1).Include(p => p.ProductType).ToListAsync();
                DateTime fechavencimiento = DateTime.Now;

            
            foreach (var customer in customers)
                {
                    List<CertificadoDeposito> certificadoDepositos = await  _context.CertificadoDeposito
                        .Include(i => i.InsurancePolicy)
                        .Include(i => i._CertificadoLine)
                        .Include(i => i.Branch)
                        .Where(c => c.CustomerId == customer.CustomerId).ToListAsync();

                    

                    foreach (var certificado in certificadoDepositos)
                    {
                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        //check.AmountWords = let.ToCustomCardinal((check.Ammount)).ToUpper();
                        InsuranceCertificate insurance = new InsuranceCertificate
                        {
                            Code = certificado.Branch.BranchCode,
                            CustomerId = certificado.CustomerId,
                            CustomerName = certificado.CustomerName,
                            InsuranceId = Convert.ToInt32(certificado.InsuranceId),
                            Amount = Convert.ToDecimal(certificado.Total),
                            Date = DateTime.Now.AddDays(( DateTime.Now.Day * -1)+1),
                            DueDate = DateTime.Now.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day),
                            AmountWords = let.ToCustomCardinal((certificado.Total)).ToUpper(),
                            ProductTypeId = customer.ProductTypeId,
                            ProductTypes = customer.ProductType.ProductTypeName,
                            EstadoId =1,     
                            InsurancePolicyNumber = certificado.InsurancePolicy.PolicyNumber,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = User.Identity.Name,
                            UsuarioModificacion = User.Identity.Name,
                        };

                        //certificadoLines = await _context.CertificadoLine.Where(w => w.IdCD == certificado.IdCD).ToListAsync();

                        insurance.InsuranceCertificaLines = new List<InsuranceCertificateLine>();

                        foreach (var certificadoline in certificado._CertificadoLine)
                        {
                            insurance.InsuranceCertificaLines.Add(new InsuranceCertificateLine
                            {
                                WarehouseId = certificadoline.WarehouseId,
                                Amount = certificadoline.Amount,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = User.Identity.Name,
                                UsuarioModificacion = User.Identity.Name,

                            });

                        }

                        _context.InsuranceCertificate.Add(insurance);

                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest("Error al Generer los Certificados "+ ex.ToString());
            }
            

            

            return await Task.Run(() => Ok());
        }


        /// <summary>
        /// Inserta una nueva InsuranceCertificate
        /// </summary>
        /// <param name="pInsuranceCertificate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsuranceCertificate>> Insert([FromBody] InsuranceCertificate pInsuranceCertificate)
        {
            InsuranceCertificate _InsuranceCertificateq = new InsuranceCertificate();
            _InsuranceCertificateq = pInsuranceCertificate;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    Numalet let;
                    let = new Numalet();
                    let.SeparadorDecimalSalida = "Lempiras";
                    let.MascaraSalidaDecimal = "00/100 ";
                    let.ApocoparUnoParteEntera = true;


                    _context.InsuranceCertificate.Add(_InsuranceCertificateq);
                    //await _context.SaveChangesAsync();

                    JournalEntry _je = new JournalEntry
                    {
                        Date = DateTime.Now,//_InsuranceCertificateq.DateGenerated,
                        Memo = "Factura de Compra a Proveedores",
                        DatePosted = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = _InsuranceCertificateq.UsuarioModificacion,
                        CreatedUser = _InsuranceCertificateq.UsuarioCreacion,
                        //DocumentId = _InsuranceCertificateq.InsuranceCertificateId,
                    };



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
                        DocType = "InsuranceCertificate",
                        ClaseInicial =
                        Newtonsoft.Json.JsonConvert.SerializeObject(_InsuranceCertificateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InsuranceCertificateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insert",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _InsuranceCertificateq.UsuarioCreacion,
                        UsuarioModificacion = _InsuranceCertificateq.UsuarioModificacion,
                        UsuarioEjecucion = _InsuranceCertificateq.UsuarioModificacion,

                    });
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                    
                }
            }


            return await Task.Run(() => Ok(_InsuranceCertificateq));
        }




        /// <summary>
        /// Actualiza la InsuranceCertificate
        /// </summary>
        /// <param name="_InsuranceCertificate"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsuranceCertificate>> Update([FromBody] InsuranceCertificate _InsuranceCertificate)
        {
            InsuranceCertificate _InsuranceCertificateq = _InsuranceCertificate;
            try
            {
                _InsuranceCertificateq = await (from c in _context.InsuranceCertificate
                                 .Where(q => q.Id == _InsuranceCertificate.Id)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InsuranceCertificateq).CurrentValues.SetValues((_InsuranceCertificate));

                //_context.InsuranceCertificate.Update(_InsuranceCertificateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceCertificateq));
        }

        /// <summary>
        /// Elimina una InsuranceCertificate       
        /// </summary>
        /// <param name="_InsuranceCertificate"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] InsuranceCertificate _InsuranceCertificate)
        {
            InsuranceCertificate _InsuranceCertificateq = new InsuranceCertificate();
            try
            {
                _InsuranceCertificateq = _context.InsuranceCertificate
                .Where(x => x.Id == (Int64)_InsuranceCertificate.Id)
                .FirstOrDefault();

                _context.InsuranceCertificate.Remove(_InsuranceCertificateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceCertificateq));

        }





    }
}
