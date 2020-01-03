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
    public class MaterialDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MaterialDetailController(ILogger<MaterialDetailController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de detalle de materiales, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMaterialDetailPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<MaterialDetail> Items = new List<MaterialDetail>();
            try
            {
                var query = _context.MaterialDetail.AsQueryable();
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
        /// Obtiene el Listado de detalle de materiales, ordenado por id de material
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMaterialDetail()
        {
            List<MaterialDetail> Items = new List<MaterialDetail>();
            try
            {
                Items = await _context.MaterialDetail.OrderBy(b => b.MaterialId).ToListAsync();
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
        /// Obtiene los Datos del detalle de materiales por medio del Id enviado.
        /// </summary>
        /// <param name="MaterialDetailId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialDetailId}")]
        public async Task<IActionResult> GetMaterialDetailById(Int64 MaterialDetailId)
        {
            MaterialDetail Items = new MaterialDetail();
            try
            {
                Items = await _context.MaterialDetail.Where(q => q.MaterialDetailId == MaterialDetailId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el detalle de materiales por el id del material enviado
        /// </summary>
        /// <param name="MaterialId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialDetailCode}")]
        public async Task<IActionResult> GetMaterialDetailByCode(Int64 MaterialId)
        {
            MaterialDetail Items = new MaterialDetail();
            try
            {
                Items = await _context.MaterialDetail.Where(q => q.MaterialId == MaterialId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el detalle de materiales por la descripcion enviada
        /// </summary>
        /// <param name="MaterialDetailDescription"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MaterialDetailDescription}")]
        public async Task<IActionResult> GetMaterialDetailByDescription(string MaterialDetailDescription)
        {
            MaterialDetail Items = new MaterialDetail();
            try
            {
                Items = await _context.MaterialDetail.Where(q => q.Description == MaterialDetailDescription).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo detalle de materiales
        /// </summary>
        /// <param name="_MaterialDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<MaterialDetail>> Insert([FromBody]MaterialDetail _MaterialDetail)
        {
            MaterialDetail _MaterialDetailq = new MaterialDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _MaterialDetailq = _MaterialDetail;
                        _context.MaterialDetail.Add(_MaterialDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _MaterialDetailq.MaterialDetailId,
                            DocType = "MaterialDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_MaterialDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _MaterialDetailq.UsuarioCreacion,
                            UsuarioModificacion = _MaterialDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _MaterialDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_MaterialDetailq));
        }

        /// <summary>
        /// Actualiza el detalle de materiales
        /// </summary>
        /// <param name="_MaterialDetail"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<MaterialDetail>> Update([FromBody]MaterialDetail _MaterialDetail)
        {
            MaterialDetail _MaterialDetailq = _MaterialDetail;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _MaterialDetailq = await (from c in _context.MaterialDetail
                        .Where(q => q.MaterialDetailId == _MaterialDetail.MaterialDetailId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_MaterialDetailq).CurrentValues.SetValues((_MaterialDetail));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _MaterialDetailq.MaterialDetailId,
                            DocType = "MaterialDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_MaterialDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _MaterialDetailq.UsuarioCreacion,
                            UsuarioModificacion = _MaterialDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _MaterialDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_MaterialDetailq));
        }

        /// <summary>
        /// Elimina un detalle de materiales       
        /// </summary>
        /// <param name="_MaterialDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]MaterialDetail _MaterialDetail)
        {
            MaterialDetail _MaterialDetailq = new MaterialDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _MaterialDetailq = _context.MaterialDetail
                        .Where(x => x.MaterialDetailId == (Int64)_MaterialDetail.MaterialDetailId)
                        .FirstOrDefault();

                        _context.MaterialDetail.Remove(_MaterialDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _MaterialDetailq.MaterialDetailId,
                            DocType = "MaterialDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_MaterialDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _MaterialDetailq.UsuarioCreacion,
                            UsuarioModificacion = _MaterialDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _MaterialDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_MaterialDetailq));

        }
    }
}
