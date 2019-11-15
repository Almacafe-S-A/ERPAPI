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
        public async Task<IActionResult> GetSeveridadRiesgo()
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
        public async Task<IActionResult> GetSeveridadRiesgoById(Int64 IdInsurancePolicy)
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
        /// Inserta una nueva poliza
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
                        _context.InsurancePolicy.Add(InsurancePolicyq);
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
        /// Elimina una Poliza
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
