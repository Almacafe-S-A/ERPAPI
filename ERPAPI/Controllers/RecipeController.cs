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
    [Route("api/Recipe")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public RecipeController(ILogger<RecipeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Recetas, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecipePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Recipe> Items = new List<Recipe>();
            try
            {
                var query = _context.Recipe.AsQueryable();
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
        /// Obtiene el Listado de Recetas, ordenado por codigo de receta
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRecipe()
        {
            List<Recipe> Items = new List<Recipe>();
            try
            {
                Items = await _context.Recipe.OrderBy(b => b.RecipeCode).ToListAsync();
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
        /// Obtiene los Datos de la receta por medio del Id enviado.
        /// </summary>
        /// <param name="RecipeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RecipeId}")]
        public async Task<IActionResult> GetRecipeById(Int64 RecipeId)
        {
            Recipe Items = new Recipe();
            try
            {
                Items = await _context.Recipe.Where(q => q.RecipeId == RecipeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene la receta por el codigo enviado
        /// </summary>
        /// <param name="RecipeCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RecipeCode}")]
        public async Task<IActionResult> GetRecipeByRecipeCode(string RecipeCode)
        {
            Recipe Items = new Recipe();
            try
            {
                Items = await _context.Recipe.Where(q => q.RecipeCode == RecipeCode).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene la receta por la descripcion enviada
        /// </summary>
        /// <param name="RecipeDescription"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RecipeDescription}")]
        public async Task<IActionResult> GetRecipeByDescription(string RecipeDescription)
        {
            Recipe Items = new Recipe();
            try
            {
                Items = await _context.Recipe.Where(q => q.Description == RecipeDescription).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva receta
        /// </summary>
        /// <param name="_Recipe"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Recipe>> Insert([FromBody]Recipe _Recipe)
        {
            Recipe _Recipeq = new Recipe();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Recipeq = _Recipe;
                        _context.Recipe.Add(_Recipeq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Recipeq.RecipeId,
                            DocType = "Recipe",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Recipeq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Recipeq.UsuarioCreacion,
                            UsuarioModificacion = _Recipeq.UsuarioModificacion,
                            UsuarioEjecucion = _Recipeq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Recipeq));
        }

        /// <summary>
        /// Actualiza el Color
        /// </summary>
        /// <param name="_Recipe"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Recipe>> Update([FromBody]Recipe _Recipe)
        {
            Recipe _Recipeq = _Recipe;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Recipeq = await (from c in _context.Recipe
                        .Where(q => q.RecipeId == _Recipe.RecipeId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Recipeq).CurrentValues.SetValues((_Recipe));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Recipeq.RecipeId,
                            DocType = "Recipe",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Recipeq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Recipeq.UsuarioCreacion,
                            UsuarioModificacion = _Recipeq.UsuarioModificacion,
                            UsuarioEjecucion = _Recipeq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Recipeq));
        }

        /// <summary>
        /// Elimina un Color       
        /// </summary>
        /// <param name="_Recipe"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Recipe _Recipe)
        {
            Recipe _Recipeq = new Recipe();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Recipeq = _context.Recipe
                        .Where(x => x.RecipeId == (Int64)_Recipe.RecipeId)
                        .FirstOrDefault();

                        _context.Recipe.Remove(_Recipeq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Recipeq.RecipeId,
                            DocType = "Recipe",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Recipeq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Recipeq.UsuarioCreacion,
                            UsuarioModificacion = _Recipeq.UsuarioModificacion,
                            UsuarioEjecucion = _Recipeq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Recipeq));

        }
    }
}
