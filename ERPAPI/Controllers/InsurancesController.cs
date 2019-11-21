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
    [Route("api/Insurances")]
    [ApiController]
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public InsurancesController(ILogger<InsurancesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Insurances paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurancesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Insurances> Items = new List<Insurances>();
            try
            {
                var query = _context.Insurances.AsQueryable();
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
        /// Obtiene los Datos de la Insurances en una lista.
        /// </summary>

        // GET: api/Insurances
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsurances()

        {
            List<Insurances> Items = new List<Insurances>();
            try
            {
                Items = await _context.Insurances.ToListAsync();
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
        /// Obtiene los Datos de la Insurances por medio del Id enviado.
        /// </summary>
        /// <param name="InsurancesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsurancesId}")]
        public async Task<IActionResult> GetInsurancesById(Int64 InsurancesId)
        {
            Insurances Items = new Insurances();
            try
            {
                Items = await _context.Insurances.Where(q => q.InsurancesId == InsurancesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la Insurances por medio del Nombre enviado.
        /// </summary>
        /// <param name="InsurancesName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsurancesName}")]
        public async Task<IActionResult> GetInsurancesByInsurancesName(String InsurancesName)
        {
            Insurances Items = new Insurances();
            try
            {
                Items = await _context.Insurances.Where(q => q.InsurancesName == InsurancesName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Inserta una nueva Insurances
        /// </summary>
        /// <param name="_Insurances"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Insurances>> Insert([FromBody]Insurances _Insurances)
        {
            Insurances _Insurancesq = new Insurances();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Insurancesq = _Insurances;
                        _context.Insurances.Add(_Insurancesq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Insurances.InsurancesId,
                            DocType = "Insurances",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Insurances, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Insurances.CreatedUser,
                            UsuarioModificacion = _Insurances.ModifiedUser,
                            UsuarioEjecucion = _Insurances.ModifiedUser,

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

            return await Task.Run(() => Ok(_Insurancesq));
        }

        /// <summary>
        /// Actualiza la Insurances
        /// </summary>
        /// <param name="_Insurances"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Insurances>> Update([FromBody]Insurances _Insurances)
        {
            Insurances _Insurancesq = _Insurances;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Insurancesq = await (from c in _context.Insurances
                                         .Where(q => q.InsurancesId == _Insurances.InsurancesId)
                                                 select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Insurancesq).CurrentValues.SetValues((_Insurances));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Insurances.InsurancesId,
                            DocType = "Insurances",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Insurancesq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Insurances, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Insurances.CreatedUser,
                            UsuarioModificacion = _Insurances.ModifiedUser,
                            UsuarioEjecucion = _Insurances.ModifiedUser,

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

            return await Task.Run(() => Ok(_Insurancesq));
        }

        /// <summary>
        /// Elimina una Insurances       
        /// </summary>
        /// <param name="_Insurances"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Insurances _Insurances)
        {
            Insurances _Insurancesq = new Insurances();
            try
            {
                _Insurancesq = _context.Insurances
                .Where(x => x.InsurancesId == (Int64)_Insurances.InsurancesId)
                .FirstOrDefault();

                _context.Insurances.Remove(_Insurancesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Insurancesq));

        }
       
    }
}