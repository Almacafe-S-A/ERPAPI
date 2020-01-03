/********************************************************************************************************

 -- NAME   :  CRUDColors

 -- PROPOSE:  show records Colors from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 2.0                  02/01/2020          Marvin.Guillen                Changes of Validation to delete record
 
 1.0                  12/12/2019          Alfredo.Ochoa                Creation of Controller


 ********************************************************************************************************/

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
    [Route("api/Colors")]
    [ApiController]
    public class ColorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ColorsController(ILogger<ColorsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Colores, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetColorsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Colors> Items = new List<Colors>();
            try
            {
                var query = _context.Colors.AsQueryable();
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
        /// Obtiene el Listado de Colores, ordenado por codigo de colores
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetColors()
        {
            List<Colors> Items = new List<Colors>();
            try
            {
                Items = await _context.Colors.OrderBy(b => b.ColorCode).ToListAsync();
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
        /// Obtiene los Datos del Color por medio del Id enviado.
        /// </summary>
        /// <param name="ColorsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsId}")]
        public async Task<IActionResult> GetColorsById(Int64 ColorsId)
        {
            Colors Items = new Colors();
            try
            {
                Items = await _context.Colors.Where(q => q.ColorId == ColorsId).FirstOrDefaultAsync();
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
        /// <param name="ColorsCode"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsCode}")]
        public async Task<IActionResult> GetColorsByCode(string ColorsCode)
        {
            Colors Items = new Colors();
            try
            {
                Items = await _context.Colors.Where(q => q.ColorCode == ColorsCode).FirstOrDefaultAsync();
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
        /// <param name="ColorsDescription"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ColorsDescription}")]
        public async Task<IActionResult> GetColorsByDescription(string ColorsDescription)
        {
            Colors Items = new Colors();
            try
            {
                Items = await _context.Colors.Where(q => q.Description == ColorsDescription).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{ColorsId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 ColorsId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;//await _context.CheckAccount.Where(a => a.BankId == BankId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }



        /// <summary>
        /// Inserta un nuevo color
        /// </summary>
        /// <param name="_Colors"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Colors>> Insert([FromBody]Colors _Colors)
        {
            Colors _Colorsq = new Colors();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Colorsq = _Colors;
                        _context.Colors.Add(_Colorsq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Colorsq.ColorId,
                            DocType = "Colors",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Colorsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Colorsq.UsuarioCreacion,
                            UsuarioModificacion = _Colorsq.UsuarioModificacion,
                            UsuarioEjecucion = _Colorsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Colorsq));
        }

        /// <summary>
        /// Actualiza el Color
        /// </summary>
        /// <param name="_Colors"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Colors>> Update([FromBody]Colors _Colors)
        {
            Colors _Colorsq = _Colors;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Colorsq = await (from c in _context.Colors
                        .Where(q => q.ColorId == _Colors.ColorId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Colorsq).CurrentValues.SetValues((_Colors));
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Colorsq.ColorId,
                            DocType = "Colors",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Colorsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Colorsq.UsuarioCreacion,
                            UsuarioModificacion = _Colorsq.UsuarioModificacion,
                            UsuarioEjecucion = _Colorsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Colorsq));
        }

        /// <summary>
        /// Elimina un Color       
        /// </summary>
        /// <param name="_Colors"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Colors _Colors)
        {
            Colors _Colorsq = new Colors();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Colorsq = _context.Colors
                        .Where(x => x.ColorId == (Int64)_Colors.ColorId)
                        .FirstOrDefault();

                        _context.Colors.Remove(_Colorsq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Colorsq.ColorId,
                            DocType = "Colors",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Colorsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Colorsq.UsuarioCreacion,
                            UsuarioModificacion = _Colorsq.UsuarioModificacion,
                            UsuarioEjecucion = _Colorsq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Colorsq));

        }
    }
}
