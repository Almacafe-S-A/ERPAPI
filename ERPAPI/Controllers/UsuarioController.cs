using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public UsuarioController(ILogger<UsuarioController> logger
            ,ApplicationDbContext context
            , RoleManager<ApplicationRole> rolemanager
            , UserManager<ApplicationUser> userManager
             , IMapper mapper
          )
        {
            _context = context;
            _rolemanager = rolemanager;
            _userManager = userManager;
            _logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Agregar un rol a un usario.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult<IdentityResult>> AddRoleToUser([FromBody] UserRole model)
        {
            try
            {
                var result = await _userManager.AddToRoleAsync(model.ApplicationUser, model.rol);
                if (result.Succeeded)
                {
                    return await Task.Run(() => result);
                }
                else
                {
                    return await Task.Run(() => BadRequest("Role exists"));
                }
            }
            catch (Exception ex)
            {

               _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }
          
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityUsuario()
        {
            try
            {
                var Items = await _context.Users.CountAsync();
                return await Task.Run(() => Ok(Items));
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        /// <summary>
        /// Obtiene los usuarios en Json
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<JsonResult>> GetUsuarios()
        {
            List<ApplicationUser> _users = new List<ApplicationUser>();
            try
            {
                _users = await _context.Users.Include(c => c.Branch).ToListAsync();
            }
            catch (System.Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                 return BadRequest($"Ocurrio un error: {ex.Message}");
            }
            return await Task.Run(() => Ok(_users));
        }


        /// <summary>
        /// Obtiene los usuarios de la aplicacion.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]      
        public async Task<ActionResult<List<ApplicationUser>>> GetUsers()
        {
            List<ApplicationUser> _users = new List<ApplicationUser>();
            try
            {
                 _users = await _context.Users.ToListAsync();
                    
                  //  .Include(c => c.Branch)
               // _users = await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return await Task.Run(() => _users);
        }


        /// <summary>
        /// Obtiene un usuario , filtrado por su id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{UserId}")]
        public async Task<ActionResult> GetUserById(Guid UserId)
        {
            try
            {
                ApplicationUser Items = await _context.Users.Where(q => q.Id == UserId).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {

                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }
           
        }



        [HttpGet("[action]/{UserEmail}")]
        public async Task<ActionResult> GetUserByEmail(string UserEmail)
        {
            try
            {
                ApplicationUser Items = await _context.Users.Where(q => q.Email == UserEmail).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }

        }


        /// <summary>
        /// Agrega/Inserta un Usuario a la aplicacion
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPost("PostUsuario")]
        public async Task< ActionResult<ApplicationUser>> PostUsuario([FromBody]ApplicationUser _usuario)
        {
            try
            {

                var user = new ApplicationUser { UserName = _usuario.Email, Email = _usuario.Email };
                var result = await _userManager.CreateAsync(user, _usuario.PasswordHash);

                if (!result.Succeeded)
                {
                    string errores = "";
                    foreach (var item in result.Errors)
                    {
                        errores += item.Description;
                    }
                    return await Task.Run(() => BadRequest($"Ocurrio un error: {errores}"));
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        ApplicationUser _newpass = await _context.Users.Where(q => q.Id == _usuario.Id).FirstOrDefaultAsync();
                        _context.PasswordHistory.Add(new PasswordHistory()
                        {
                            UserId = _usuario.Id.ToString(),
                            PasswordHash = _newpass.PasswordHash,
                        });

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            // IdOperacion =,
                            Descripcion = _usuario.Id.ToString(),
                            DocType = "Usuario",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_usuario, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_usuario, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "PostUsuario",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _usuario.UsuarioCreacion,
                            UsuarioModificacion = _usuario.UsuarioModificacion,
                            UsuarioEjecucion = _usuario.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return BadRequest($"Ocurrio un error: {ex.Message}");
                    }
                }


                return await Task.Run(() => _usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }
          

        }

        /// <summary>
        /// Modifica/Actualiza un Usuario 
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPut("PutUsuario")]
        public async Task<ActionResult<ApplicationUser>> PutUsuario([FromBody]ApplicationUserDTO _usuario)
        {
            try
            {
                ApplicationUser ApplicationUserq = (from c in _context.Users
                  .Where(q => q.Id == _usuario.Id)
                                                select c
                    ).FirstOrDefault();

                _usuario.FechaCreacion = ApplicationUserq.FechaCreacion;
                _usuario.UsuarioCreacion = ApplicationUserq.UsuarioCreacion;

                string password = "";

                if (_usuario.cambiarpassword) { password= _usuario.PasswordHash; }
                else
                {
                    _context.Entry(ApplicationUserq).Property(x => x.PasswordHash).IsModified = false;
                    //_context.Entry(ApplicationUserq).Property(x => x.PhoneNumber).IsModified = true;
                    //_context.Entry(ApplicationUserq).Property(x => x.IsEnabled).IsModified = true;


                    // _context.Users.Property(x => x.Password).IsModified = true;
                    //password = _userManager.PasswordHasher.HashPassword(ApplicationUserq,ApplicationUserq.PasswordHash);
                }

                _context.Entry(ApplicationUserq).CurrentValues.SetValues((_usuario));
                await _context.SaveChangesAsync();

                //await _userManager.UpdateAsync(_usuario);
                if (_usuario.cambiarpassword)
                {
                    var resultremove = await _userManager.RemovePasswordAsync(ApplicationUserq);
                    var unlock = await _userManager.SetLockoutEnabledAsync(ApplicationUserq, true);

                    var resultadadd = await _userManager.AddPasswordAsync(ApplicationUserq, password);
                    if (!resultadadd.Succeeded)
                    {
                        string errores = "";
                        foreach (var item in resultadadd.Errors)
                        {
                            errores += item.Description;
                        }
                        return await Task.Run(() => BadRequest($"Ocurrio un error: {errores}"));
                    }

                    ApplicationUser _newpass = await _context.Users.Where(q => q.Id == ApplicationUserq.Id).FirstOrDefaultAsync();
                    _context.PasswordHistory.Add(new PasswordHistory()
                    {
                        UserId = ApplicationUserq.Id.ToString(),
                        PasswordHash = _newpass.PasswordHash,
                    });

                    await _context.SaveChangesAsync();
                }

                return await Task.Run(() => _usuario);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }
          
        }



        /// <returns></returns>
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<ApplicationUser>> ChangePassword([FromBody]ApplicationUserDTO _usuario)
        {
            try
            {
                ApplicationUser ApplicationUserq = (from c in _context.Users
                  .Where(q => q.Id == _usuario.Id)
                       select c
                    ).FirstOrDefault();

                ApplicationUserDTO _appdto = new ApplicationUserDTO
                {
                     Id = ApplicationUserq.Id,
                     IsEnabled = ApplicationUserq.IsEnabled,
                     NormalizedEmail = ApplicationUserq.NormalizedEmail,
                     LockoutEnd= ApplicationUserq.LockoutEnd,
                     PhoneNumberConfirmed = ApplicationUserq.PhoneNumberConfirmed,
                     SecurityStamp = ApplicationUserq.SecurityStamp,
                     PhoneNumber = ApplicationUserq.PhoneNumber,
                     TwoFactorEnabled = ApplicationUserq.TwoFactorEnabled,
                     BranchId = ApplicationUserq.BranchId,
                     AccessFailedCount = ApplicationUserq.AccessFailedCount,
                     ConcurrencyStamp = ApplicationUserq.ConcurrencyStamp,
                     Email = ApplicationUserq.Email,
                     EmailConfirmed = ApplicationUserq.EmailConfirmed,
                     FechaCreacion = ApplicationUserq.FechaCreacion,
                     FechaModificacion = ApplicationUserq.FechaModificacion,
                     LastPasswordChangedDate = ApplicationUserq.LastPasswordChangedDate,
                     LockoutEnabled = ApplicationUserq.LockoutEnabled,
                     NormalizedUserName = ApplicationUserq.NormalizedUserName,
                     UsuarioCreacion = ApplicationUserq.UsuarioCreacion,
                     UsuarioModificacion = ApplicationUserq.UsuarioModificacion,
                     PasswordHash = ApplicationUserq.PasswordHash,
                     UserName = ApplicationUserq.UserName,
                     
                };

                string password = _usuario.PasswordHash;
                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var result = await passwordValidator.ValidateAsync(_userManager, null, password);

                if (result.Succeeded)
                {
                    

                    _appdto.LastPasswordChangedDate=_usuario.LastPasswordChangedDate = DateTime.Now;

                    _appdto.IsEnabled = true;
                    var actualizarusuario = await PutUsuario(_appdto);
                    var resultremove = await _userManager.RemovePasswordAsync(ApplicationUserq);
                    _appdto.PasswordHash = password;

                     resultremove = await _userManager.RemovePasswordAsync(ApplicationUserq);
                    var resultadadd = await _userManager.AddPasswordAsync(ApplicationUserq, password);

                                 

                    if (!resultadadd.Succeeded)
                    {
                        string errores = "";
                        foreach (var item in resultadadd.Errors)
                        {
                            errores += item.Description;
                        }
                        return await Task.Run(() => BadRequest($"Ocurrio un error: {errores}"));
                    }

                    ApplicationUser _newpass = await _context.Users.Where(q => q.Id == ApplicationUserq.Id).FirstOrDefaultAsync();
                    _context.PasswordHistory.Add(new PasswordHistory()
                    {
                        UserId = ApplicationUserq.Id.ToString(),
                        PasswordHash = _newpass.PasswordHash,
                    });

                    await  _context.SaveChangesAsync();


                    //ApplicationUserq.PasswordHistory.Add(new PasswordHistory()
                    //{
                    //    UserId = ApplicationUserq.Id.ToString(),
                    //    PasswordHash = _newpass.PasswordHash,
                    //});


                    BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                    {
                        // IdOperacion =,
                       Descripcion = _usuario.Id.ToString(),
                        DocType = "Usuario",
                        ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_usuario, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_usuario, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "ChangePassword",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _usuario.UsuarioCreacion,
                        UsuarioModificacion = _usuario.UsuarioModificacion,
                        UsuarioEjecucion = _usuario.UsuarioModificacion,

                    });

                    await _context.SaveChangesAsync();

                    return await Task.Run(() => _usuario);

                }
                else
                {
                    return await Task.Run(() => BadRequest($" El password debe tener mayusculas, minusculas y caracteres especiales!"));
                }

            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }

        }


        /// <summary>
        /// Elimina un usuario de la aplicacion 
        /// </summary>
        /// <param name="_user"></param>
        /// <returns></returns>
        [HttpPost("DeleteUsuario")]
        public async Task<ActionResult<ApplicationUser>> DeleteUsuario([FromBody]ApplicationUser _user)
        {

            try
            {
                _context.Users.Remove(_user);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => (_user));
        }


    }
}