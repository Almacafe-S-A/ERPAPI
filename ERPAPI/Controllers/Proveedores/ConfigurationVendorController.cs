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
    /*public class ConfigurationVendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }*/
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ConfigurationVendor")]
    [ApiController]
    public class ConfigurationVendorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        
        public ConfigurationVendorController(ILogger<ConfigurationVendorController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el Listado de ConfigurationVendor paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConfigurationVendorPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ConfigurationVendor> Items = new List<ConfigurationVendor>();
            try
            {
                var query = _context.ConfigurationVendor.AsQueryable();
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
        /// Obtiene los Datos de la ConfigurationVendor en una lista.
        /// </summary>

        // GET: api/ConfigurationVendor
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConfigurationVendor()

        {
            List<ConfigurationVendor> Items = new List<ConfigurationVendor>();
            try
            {
                Items = await _context.ConfigurationVendor.ToListAsync();
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
        /// Obtiene los Datos de la ConfigurationVendor en una lista.
        /// </summary>

        // GET: api/ConfigurationVendor
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConfigurationVendorActive()

        {
            ConfigurationVendor Items = new ConfigurationVendor();
            try
            {
                Items = await _context.ConfigurationVendor.Where(p =>p.Estado == "Activo").FirstOrDefaultAsync();
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
        /// Obtiene los Datos de la ContactPerson por medio del Id enviado.
        /// </summary>
        /// <param name="ConfigurationVendorId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ConfigurationVendorId}")]
        public async Task<IActionResult> GetConfigurationVendorById(Int64 ConfigurationVendorId)
        {
            ConfigurationVendor Items = new ConfigurationVendor();
            try
            {
                Items = await _context.ConfigurationVendor.Where(q => q.ConfigurationVendorId == ConfigurationVendorId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }





        /// <summary>
        /// Inserta una nueva ConfigurationVendor
        /// </summary>
        /// <param name="_ConfigurationVendor"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ConfigurationVendor>> Insert([FromBody]ConfigurationVendor _ConfigurationVendor)
        {
            ConfigurationVendor _ConfigurationVendorq = new ConfigurationVendor();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ConfigurationVendorq = _ConfigurationVendor;
                        _context.ConfigurationVendor.Add(_ConfigurationVendorq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ConfigurationVendor.ConfigurationVendorId,
                            DocType = "ConfigurationVendor",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ConfigurationVendor, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ConfigurationVendor.CreatedUser,
                            UsuarioModificacion = _ConfigurationVendor.ModifiedUser,
                            UsuarioEjecucion = _ConfigurationVendor.ModifiedUser,

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

            return await Task.Run(() => Ok(_ConfigurationVendorq));
        }

        /// <summary>
        /// Actualiza la ConfigurationVendor
        /// </summary>
        /// <param name="_ConfigurationVendor"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ConfigurationVendor>> Update([FromBody]ConfigurationVendor _ConfigurationVendor)
        {
            ConfigurationVendor _ConfigurationVendorq = _ConfigurationVendor;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ConfigurationVendorq = await (from c in _context.ConfigurationVendor
                                         .Where(q => q.ConfigurationVendorId == _ConfigurationVendor.ConfigurationVendorId)
                                                 select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_ConfigurationVendorq).CurrentValues.SetValues((_ConfigurationVendor));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ConfigurationVendor.ConfigurationVendorId,
                            DocType = "ConfigurationVendor",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ConfigurationVendorq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ConfigurationVendor, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ConfigurationVendor.CreatedUser,
                            UsuarioModificacion = _ConfigurationVendor.ModifiedUser,
                            UsuarioEjecucion = _ConfigurationVendor.ModifiedUser,

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

            return await Task.Run(() => Ok(_ConfigurationVendorq));
        }

        /// <summary>
        /// Elimina una ContactPerson       
        /// </summary>
        /// <param name="_ConfigurationVendor"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ConfigurationVendor _ConfigurationVendor)
        {
            ConfigurationVendor _ConfigurationVendorq = new ConfigurationVendor();
            try
            {
                _ConfigurationVendorq = _context.ConfigurationVendor
                .Where(x => x.ConfigurationVendorId == (Int64)_ConfigurationVendor.ConfigurationVendorId)
                .FirstOrDefault();

                _context.ConfigurationVendor.Remove(_ConfigurationVendorq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ConfigurationVendorq));

        }

    }
}