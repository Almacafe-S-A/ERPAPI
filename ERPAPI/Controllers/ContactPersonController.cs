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
    [Route("api/ContactPerson")]
    [ApiController]
    public class ContactPersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ContactPersonController(ILogger<ContactPersonController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ContactPerson paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContactPersonPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ContactPerson> Items = new List<ContactPerson>();
            try
            {
                var query = _context.ContactPerson.AsQueryable();
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
        /// Obtiene los Datos de la ContactPerson en una lista.
        /// </summary>

        // GET: api/ContactPerson
        [HttpGet("[action]/{VendorId}")]
        public async Task<IActionResult> GetContactPerson(Int64 VendorId)

        {
            List<ContactPerson> Items = new List<ContactPerson>();
            try
            {
                Items = await _context.ContactPerson.Where(q => q.VendorId == VendorId).ToListAsync();
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
        /// <param name="ContactPersonId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ContactPersonId}")]
        public async Task<IActionResult> GetContactPersonById(Int64 ContactPersonId)
        {
            ContactPerson Items = new ContactPerson();
            try
            {
                Items = await _context.ContactPerson.Where(q => q.ContactPersonId == ContactPersonId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }





        /// <summary>
        /// Inserta una nueva ContactPerson
        /// </summary>
        /// <param name="_ContactPerson"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ContactPerson>> Insert([FromBody]ContactPerson _ContactPerson)
        {
            ContactPerson _ContactPersonq = new ContactPerson();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ContactPersonq = _ContactPerson;
                        _context.ContactPerson.Add(_ContactPersonq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ContactPerson.ContactPersonId,
                            DocType = "ContactPerson",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ContactPerson, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ContactPerson.CreatedUser,
                            UsuarioModificacion = _ContactPerson.ModifiedUser,
                            UsuarioEjecucion = _ContactPerson.ModifiedUser,

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

            return await Task.Run(() => Ok(_ContactPersonq));
        }

        /// <summary>
        /// Actualiza la ContactPerson
        /// </summary>
        /// <param name="_ContactPerson"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ContactPerson>> Update([FromBody]ContactPerson _ContactPerson)
        {
            ContactPerson _ContactPersonq = _ContactPerson;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ContactPersonq = await (from c in _context.ContactPerson
                                         .Where(q => q.ContactPersonId == _ContactPerson.ContactPersonId)
                                                 select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_ContactPersonq).CurrentValues.SetValues((_ContactPerson));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ContactPerson.ContactPersonId,
                            DocType = "ContactPerson",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ContactPersonq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ContactPerson, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ContactPerson.CreatedUser,
                            UsuarioModificacion = _ContactPerson.ModifiedUser,
                            UsuarioEjecucion = _ContactPerson.ModifiedUser,

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

            return await Task.Run(() => Ok(_ContactPersonq));
        }

        /// <summary>
        /// Elimina una ContactPerson       
        /// </summary>
        /// <param name="_ContactPerson"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ContactPerson _ContactPerson)
        {
            ContactPerson _ContactPersonq = new ContactPerson();
            try
            {
                _ContactPersonq = _context.ContactPerson
                .Where(x => x.ContactPersonId == (Int64)_ContactPerson.ContactPersonId)
                .FirstOrDefault();

                _context.ContactPerson.Remove(_ContactPersonq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ContactPersonq));

        }
       
    }
}