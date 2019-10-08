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
    [Route("api/VendorDocument")]
    [ApiController]
    public class VendorDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorDocumentController(ILogger<VendorDocumentController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de VendorDocument paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorDocumentPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<VendorDocument> Items = new List<VendorDocument>();
            try
            {
                var query = _context.VendorDocument.AsQueryable();
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
        /// Obtiene el Listado de VendorDocument 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorDocument()
        {
            List<VendorDocument> Items = new List<VendorDocument>();
            try
            {
                Items = await _context.VendorDocument.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{VendorId}")]
        public async Task<IActionResult> GeDocumentByVendorId(Int64 VendorId)
        {
            List<VendorDocument> Items = new List<VendorDocument>();
            try
            {
                Items = await _context.VendorDocument.Where(q => q.VendorId == VendorId).ToListAsync();
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
        /// Obtiene los Datos de la VendorDocument por medio del Id enviado.
        /// </summary>
        /// <param name="VendorDocumentId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{VendorDocumentId}")]
        public async Task<IActionResult> GetVendorDocumentById(Int64 VendorDocumentId)
        {
            VendorDocument Items = new VendorDocument();
            try
            {
                Items = await _context.VendorDocument.Where(q => q.VendorDocumentId == VendorDocumentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva VendorDocument
        /// </summary>
        /// <param name="_VendorDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<VendorDocument>> Insert([FromBody]VendorDocument _VendorDocument)
        {
            VendorDocument _VendorDocumentq = new VendorDocument();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _VendorDocumentq = _VendorDocument;
                _context.VendorDocument.Add(_VendorDocumentq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _VendorDocumentq.VendorDocumentId,
                            DocType = "VendorDocument",
                            ClaseInicial =
                       Newtonsoft.Json.JsonConvert.SerializeObject(_VendorDocumentq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _VendorDocumentq.CreatedUser,
                            UsuarioModificacion = _VendorDocumentq.ModifiedUser,
                            UsuarioEjecucion = _VendorDocumentq.ModifiedUser,

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

            return await Task.Run(() => Ok(_VendorDocumentq));
        }

        /// <summary>
        /// Actualiza la VendorDocument
        /// </summary>
        /// <param name="_VendorDocument"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<VendorDocument>> Update([FromBody]VendorDocument _VendorDocument)
        {
            VendorDocument _VendorDocumentq = _VendorDocument;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _VendorDocumentq = await (from c in _context.VendorDocument
                                 .Where(q => q.VendorDocumentId == _VendorDocument.VendorDocumentId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_VendorDocumentq).CurrentValues.SetValues((_VendorDocument));
                await _context.SaveChangesAsync();
                        //await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _VendorDocumentq.VendorDocumentId,
                            DocType = "VendorDocument",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_VendorDocumentq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_VendorDocumentq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _VendorDocumentq.CreatedUser,
                            UsuarioModificacion = _VendorDocumentq.ModifiedUser,
                            UsuarioEjecucion = _VendorDocumentq.ModifiedUser,

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

            return await Task.Run(() => Ok(_VendorDocumentq));
        }

        /// <summary>
        /// Elimina una VendorDocument       
        /// </summary>
        /// <param name="_VendorDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorDocument _VendorDocument)
        {
            VendorDocument _VendorDocumentq = new VendorDocument();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _VendorDocumentq = _context.VendorDocument
                .Where(x => x.VendorDocumentId == (Int64)_VendorDocument.VendorDocumentId)
                .FirstOrDefault();

                _context.VendorDocument.Remove(_VendorDocumentq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _VendorDocumentq.VendorDocumentId,
                            DocType = "VendorDocument",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_VendorDocumentq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_VendorDocumentq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _VendorDocumentq.CreatedUser,
                            UsuarioModificacion = _VendorDocumentq.ModifiedUser,
                            UsuarioEjecucion = _VendorDocumentq.ModifiedUser,

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

            return await Task.Run(() => Ok(_VendorDocumentq));

        }







    }
}