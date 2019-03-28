using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioController(ApplicationDbContext context
            , RoleManager<IdentityRole> rolemanager
            , UserManager<ApplicationUser> userManager
          )
        {
            _context = context;
            _rolemanager = rolemanager;
            _userManager = userManager;
        }

        /// <summary>
        /// Agregar un rol a un usario.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult<IdentityResult>> AddRoleToUser([FromBody] UserRole model)
        {
            var result = await _userManager.AddToRoleAsync(model.ApplicationUser, model.rol);
            if (result.Succeeded)
            {
                return result;
            }
            else
            {
                return BadRequest("Role exists");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<JsonResult> GetUsuarios()
        {
            List<ApplicationUser> _users = new List<ApplicationUser>();
            try
            {
               _users = await _context.Users.ToListAsync();
            }
            catch (System.Exception myExc)
            {
                throw (new Exception(myExc.Message));
            }
            return Json(_users);
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
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return _users;
        }


        /// <summary>
        /// Obtiene un usuario , filtrado por su id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{UserId}")]
        public async Task<ActionResult> GetUserById(string UserId)
        {
            ApplicationUser Items = await _context.Users.Where(q => q.Id == UserId).FirstOrDefaultAsync();
            return Ok(Items);
        }


        /// <summary>
        /// Agrega/Inserta un Usuario a la aplicacion
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPost("PostUsuario")]
        public async Task< ActionResult<ApplicationUser>> PostUsuario([FromBody]ApplicationUser _usuario)
        {

           _context.Users.Add(_usuario);
            await _context.SaveChangesAsync();

           return _usuario;

        }

        /// <summary>
        /// Modifica/Actualiza un Usuario 
        /// </summary>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        [HttpPut("PutUsuario")]
        public async Task<ActionResult<ApplicationUser>> PutUsuario([FromBody]ApplicationUser _usuario)
        {
            _context.Users.Update(_usuario);
            await _context.SaveChangesAsync();

            return _usuario;
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          

            return (_user);
        }


    }
}