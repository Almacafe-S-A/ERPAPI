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
    [Route("api/Material")]
    [ApiController]
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MaterialController(ILogger<MaterialController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Materiales, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMaterialPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Material> Items = new List<Material>();
            try
            {
                var query = _context.Material.AsQueryable();
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
        /// Obtiene el Listado de Materiales, ordenado por codigo de Materiales
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMaterial()
        {
            List<Material> Items = new List<Material>();
            try
            {
                Items = await _context.Material.OrderBy(b => b.MaterialCode).ToListAsync();
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
        /// Obtiene los Datos del Material por medio del Id enviado.
        /// </summary>
        /// <param name="MaterialId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialId}")]
        public async Task<IActionResult> GetMaterialById(Int64 MaterialId)
        {
            Material Items = new Material();
            try
            {
                Items = await _context.Material.Where(q => q.MaterialId == MaterialId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el color por el codigo enviado
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialCode}")]
        public async Task<IActionResult> GetMaterialByCode(string MaterialCode)
        {
            Material Items = new Material();
            try
            {
                Items = await _context.Material.Where(q => q.MaterialCode == MaterialCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el color por la descripcion enviada
        /// </summary>
        /// <param name="MaterialDescription"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialDescription}")]
        public async Task<IActionResult> GetMaterialByDescription(string MaterialDescription)
        {
            Material Items = new Material();
            try
            {
                Items = await _context.Material.Where(q => q.Description == MaterialDescription).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo color
        /// </summary>
        /// <param name="_Material"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Material>> Insert([FromBody]Material _Material)
        {
            Material _Materialq = new Material();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Materialq = _Material;
                        _context.Material.Add(_Materialq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Materialq.MaterialId,
                            DocType = "Material",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Materialq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Materialq.UsuarioCreacion,
                            UsuarioModificacion = _Materialq.UsuarioModificacion,
                            UsuarioEjecucion = _Materialq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Materialq));
        }

        /// <summary>
        /// Actualiza el Material
        /// </summary>
        /// <param name="_Material"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Material>> Update([FromBody]Material _Material)
        {
            Material _Materialq = _Material;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Materialq = await (from c in _context.Material
                        .Where(q => q.MaterialId == _Material.MaterialId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Materialq).CurrentValues.SetValues((_Material));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Materialq.MaterialId,
                            DocType = "Material",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Materialq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Materialq.UsuarioCreacion,
                            UsuarioModificacion = _Materialq.UsuarioModificacion,
                            UsuarioEjecucion = _Materialq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Materialq));
        }

        /// <summary>
        /// Elimina un Material       
        /// </summary>
        /// <param name="_Material"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Material _Material)
        {
            Material _Materialq = new Material();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Materialq = _context.Material
                        .Where(x => x.MaterialId == (Int64)_Material.MaterialId)
                        .FirstOrDefault();

                        _context.Material.Remove(_Materialq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Materialq.MaterialId,
                            DocType = "Material",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Materialq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Materialq.UsuarioCreacion,
                            UsuarioModificacion = _Materialq.UsuarioModificacion,
                            UsuarioEjecucion = _Materialq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Materialq));

        }
    }
}
