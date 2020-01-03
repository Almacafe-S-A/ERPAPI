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
    [Route("api/RecipeDetail")]
    [ApiController]
    public class RecipeDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public RecipeDetailController(ILogger<RecipeDetailController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de detalles de recetas, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecipeDetailPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<RecipeDetail> Items = new List<RecipeDetail>();
            try
            {
                var query = _context.RecipeDetail.AsQueryable();
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
        /// Obtiene el Listado de detalle de recetas, ordenado por id de receta
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecipeDetail()
        {
            List<RecipeDetail> Items = new List<RecipeDetail>();
            try
            {
                Items = await _context.RecipeDetail.OrderBy(b => b.RecipeId).ToListAsync();
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
        /// Obtiene los Datos de detalle de receta por medio del Id de receta enviado.
        /// </summary>
        /// <param name="RecipeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RecipeDetailId}")]
        public async Task<IActionResult> GetRecipeDetailById(Int64 RecipeId)
        {
            RecipeDetail Items = new RecipeDetail();
            try
            {
                Items = await _context.RecipeDetail.Where(q => q.RecipeId == RecipeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el detalle de receta por el codigo enviado y id de receta
        /// </summary>
        /// <param name="IngredientCode"></param>
        /// <param name="RecipeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RecipeDetailCode}")]
        public async Task<IActionResult> GetRecipeDetailByCode(Int64 IngredientCode, Int64 RecipeId)
        {
            RecipeDetail Items = new RecipeDetail();
            try
            {
                Items = await _context.RecipeDetail.Where(x => x.IngredientCode == IngredientCode && x.RecipeId == RecipeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo detalle de receta
        /// </summary>
        /// <param name="_RecipeDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<RecipeDetail>> Insert([FromBody]RecipeDetail _RecipeDetail)
        {
            RecipeDetail _RecipeDetailq = new RecipeDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RecipeDetailq = _RecipeDetail;
                        _context.RecipeDetail.Add(_RecipeDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RecipeDetailq.IngredientCode,
                            DocType = "RecipeDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RecipeDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RecipeDetailq.UsuarioCreacion,
                            UsuarioModificacion = _RecipeDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _RecipeDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_RecipeDetailq));
        }

        /// <summary>
        /// Actualiza el detalle de receta
        /// </summary>
        /// <param name="_RecipeDetail"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<RecipeDetail>> Update([FromBody]RecipeDetail _RecipeDetail)
        {
            RecipeDetail _RecipeDetailq = _RecipeDetail;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RecipeDetailq = await (from c in _context.RecipeDetail
                        .Where(x => x.IngredientCode == (Int64)_RecipeDetail.IngredientCode && x.RecipeId == (Int64)_RecipeDetail.RecipeId)
                                                select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_RecipeDetailq).CurrentValues.SetValues((_RecipeDetail));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RecipeDetailq.IngredientCode,
                            DocType = "RecipeDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RecipeDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RecipeDetailq.UsuarioCreacion,
                            UsuarioModificacion = _RecipeDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _RecipeDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_RecipeDetailq));
        }

        /// <summary>
        /// Elimina un detalle de receta       
        /// </summary>
        /// <param name="_RecipeDetail"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]RecipeDetail _RecipeDetail)
        {
            RecipeDetail _RecipeDetailq = new RecipeDetail();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _RecipeDetailq = _context.RecipeDetail
                        .Where(x => x.IngredientCode == (Int64)_RecipeDetail.IngredientCode && x.RecipeId == (Int64)_RecipeDetail.RecipeId)
                        .FirstOrDefault();

                        _context.RecipeDetail.Remove(_RecipeDetailq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _RecipeDetailq.IngredientCode,
                            DocType = "RecipeDetail",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_RecipeDetailq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _RecipeDetailq.UsuarioCreacion,
                            UsuarioModificacion = _RecipeDetailq.UsuarioModificacion,
                            UsuarioEjecucion = _RecipeDetailq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_RecipeDetailq));

        }
    }
}
