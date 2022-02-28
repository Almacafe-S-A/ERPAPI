using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/UserBranch")]
    [ApiController]
    public class UserBranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UserBranchController(ILogger<UserBranchController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de UserBranch paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserBranchPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<UserBranch> Items = new List<UserBranch>();
            try
            {
                var query = _context.UserBranch.AsQueryable();
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
        /// Obtiene el Listado de UserBranch 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserBranch()
        {
            List<UserBranch> Items = new List<UserBranch>();
            try
            {
                Items = await _context.UserBranch.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetBranchesbyUserId(Guid UserId)
        {
            List<UserBranch> Items = new List<UserBranch>();
            try
            {
                Items = await _context.UserBranch.Where(q => q.UserId == UserId).ToListAsync();
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
        /// Obtiene los Datos de la UserBranch por medio del Id enviado.
        /// </summary>
        /// <param name="UserBranchId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{UserBranchId}")]
        public async Task<IActionResult> GetUserBranchById(Int64 UserBranchId)
        {
            UserBranch Items = new UserBranch();
            try
            {
                Items = await _context.UserBranch.Where(q => q.Id == UserBranchId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva UserBranch
        /// </summary>
        /// <param name="_UserBranch"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<UserBranch>> Insert([FromBody]UserBranch _UserBranch)
        {
            UserBranch _UserBranchq = new UserBranch();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _UserBranchq = _UserBranch;
                        _context.UserBranch.Add(_UserBranchq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _UserBranchq.Id,
                            DocType = "UserBranch",
                            ClaseInicial =
                       Newtonsoft.Json.JsonConvert.SerializeObject(_UserBranchq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _UserBranchq.CreatedUser,
                            UsuarioModificacion = _UserBranchq.ModifiedUser,
                            UsuarioEjecucion = _UserBranchq.ModifiedUser,

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

            return await Task.Run(() => Ok(_UserBranchq));
        }

        /// <summary>
        /// Actualiza la UserBranch
        /// </summary>
        /// <param name="_UserBranch"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<UserBranch>> Update([FromBody]UserBranch _UserBranch)
        {
            UserBranch _UserBranchq = _UserBranch;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _UserBranchq = await (from c in _context.UserBranch
                                 .Where(q => q.Id == _UserBranch.Id)
                                                  select c
                                ).FirstOrDefaultAsync();

                        _context.Entry(_UserBranchq).CurrentValues.SetValues((_UserBranch));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();
                        //await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _UserBranchq.Id,
                            DocType = "UserBranch",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_UserBranchq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_UserBranchq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _UserBranchq.CreatedUser,
                            UsuarioModificacion = _UserBranchq.ModifiedUser,
                            UsuarioEjecucion = _UserBranchq.ModifiedUser,

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

            return await Task.Run(() => Ok(_UserBranchq));
        }

        /// <summary>
        /// Elimina una UserBranch       
        /// </summary>
        /// <param name="_UserBranch"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]UserBranch _UserBranch)
        {
            UserBranch _UserBranchq = new UserBranch();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _UserBranchq = _context.UserBranch.Where(x => x.BranchId == _UserBranch.BranchId && x.UserId == _UserBranch.UserId).FirstOrDefault();

                        _context.UserBranch.Remove(_UserBranchq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _UserBranchq.Id,
                            DocType = "UserBranch",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_UserBranchq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_UserBranchq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _UserBranchq.CreatedUser,
                            UsuarioModificacion = _UserBranchq.ModifiedUser,
                            UsuarioEjecucion = _UserBranchq.ModifiedUser,

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

            return await Task.Run(() => Ok(_UserBranchq));

        }

        /// <summary>
        /// Obtiene los Datos de la UserBranch por medio del BranchId y UserId enviado.
        /// </summary>
        /// <param name="DatosBrach"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ObtenerUserBranch([FromBody]UserBranch DatosBrach)
        {

            UserBranch Items = new UserBranch();

            try
            {
                Items = await _context.UserBranch.Where(q =>                
                 q.BranchId == DatosBrach.BranchId &&
                 q.UserId == DatosBrach.UserId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }





    }
}