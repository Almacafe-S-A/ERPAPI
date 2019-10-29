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
    [Route("api/InsurancesCertificate")]
    [ApiController]
    public class InsurancesCertificateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public InsurancesCertificateController(ILogger<InsurancesCertificateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsurancesCertificate paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancesCertificatePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsurancesCertificate> Items = new List<InsurancesCertificate>();
            try
            {
                var query = _context.InsurancesCertificate.AsQueryable();
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


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la InsurancesCertificate en una lista.
        /// </summary>

        // GET: api/InsurancesCertificate
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancesCertificate()

        {
            List<InsurancesCertificate> Items = new List<InsurancesCertificate>();
            try
            {
                Items = await _context.InsurancesCertificate.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));

        }
        //
        /// <summary>
        /// Obtiene los Datos de la InsurancesCertificate por medio del Id enviado.
        /// </summary>
        /// <param name="_InsurancesCertificateP"></param>

        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> GetInsurancesCertificateByFecha([FromBody]InsurancesCertificate _InsurancesCertificateP)
        {
            InsurancesCertificate Items = new InsurancesCertificate();
            try
            {//_ExchangeRate.DayofRate.ToString("yyyy-MM-dd")
                Items = await _context.InsurancesCertificate.Where(q => q.BeginDateofInsurance.ToString("yyyy-MM-dd") == _InsurancesCertificateP.BeginDateofInsurance.ToString("yyyy-MM-dd")).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la InsurancesCertificate por medio del Id enviado.
        /// </summary>
        /// <param name="InsurancesCertificateId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsurancesCertificateId}")]
        public async Task<IActionResult> GetInsurancesCertificateById(Int64 InsurancesCertificateId)
        {
            InsurancesCertificate Items = new InsurancesCertificate();
            try
            {
                Items = await _context.InsurancesCertificate.Where(q => q.InsurancesCertificateId == InsurancesCertificateId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }





        /// <summary>
        /// Inserta una nueva InsurancesCertificate
        /// </summary>
        /// <param name="_InsurancesCertificateP"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsurancesCertificate>> Insert([FromBody]InsurancesCertificate _InsurancesCertificateP)
        {
            InsurancesCertificate _InsurancesCertificateq = new InsurancesCertificate();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _InsurancesCertificateq = _InsurancesCertificateP;
                        _context.InsurancesCertificate.Add(_InsurancesCertificateq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InsurancesCertificateq.InsurancesCertificateId,
                            DocType = "InsurancesCertificate",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_InsurancesCertificateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InsurancesCertificateP.CreatedUser,
                            UsuarioModificacion = _InsurancesCertificateP.ModifiedUser,
                            UsuarioEjecucion = _InsurancesCertificateP.ModifiedUser,

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

            return await Task.Run(() => Ok(_InsurancesCertificateq));
        }

        /// <summary>
        /// Actualiza la Insurances
        /// </summary>
        /// <param name="_InsurancesCertificateP"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsurancesCertificate>> Update([FromBody]InsurancesCertificate _InsurancesCertificateP)
        {
            InsurancesCertificate _InsurancesCertificateq = _InsurancesCertificateP;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _InsurancesCertificateq = await (from c in _context.InsurancesCertificate
                                         .Where(q => q.InsurancesCertificateId == _InsurancesCertificateP.InsurancesCertificateId)
                                              select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_InsurancesCertificateq).CurrentValues.SetValues((_InsurancesCertificateP));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InsurancesCertificateq.InsurancesCertificateId,
                            DocType = "Insurances",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_InsurancesCertificateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InsurancesCertificateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InsurancesCertificateP.CreatedUser,
                            UsuarioModificacion = _InsurancesCertificateP.ModifiedUser,
                            UsuarioEjecucion = _InsurancesCertificateP.ModifiedUser,

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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsurancesCertificateq));
        }

        /// <summary>
        /// Elimina una InsurancesCertificate       
        /// </summary>
        /// <param name="_InsurancesCertificateP"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsurancesCertificate _InsurancesCertificateP)
        {
            InsurancesCertificate _InsurancesCertificateq = new InsurancesCertificate();
            try
            {
                _InsurancesCertificateq = _context.InsurancesCertificate
                .Where(x => x.InsurancesCertificateId == (Int64)_InsurancesCertificateP.InsurancesCertificateId)
                .FirstOrDefault();

                _context.InsurancesCertificate.Remove(_InsurancesCertificateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InsurancesCertificateq));

        }

    }
}