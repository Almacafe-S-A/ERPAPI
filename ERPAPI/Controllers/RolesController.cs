using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _rolemanager;

        public RolesController(ApplicationDbContext context
            , RoleManager<ApplicationRole> rolemanager)
        {
            _context = context;
            _rolemanager = rolemanager;
        }

     
        /// <summary>
        /// Obtiene el listado de roles 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<ActionResult<List<ApplicationRole>>> GetRoles()
        {
            List<ApplicationRole> _roles = new List<ApplicationRole>();
            try
            {
              _roles  = await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return _roles;
        }


   

        /// <summary>
        /// Crea un rol que permite tener accesos a recursos del API.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CrearRole")]
        public async Task<ActionResult<IdentityRole>> CreateRole([FromBody] IdentityRole model)
        {
            try
            {
                var result = await _rolemanager.CreateAsync(new ApplicationRole { Name = model.Name, NormalizedName = model.NormalizedName });
                if (result.Succeeded)
                {
                    return model;
                }
                else
                {
                    return BadRequest("Role exists");
                }
            }
            catch (Exception ex)
            {

               return BadRequest($"Ocurrio un error:{ex.Message}");
            }
         
        }


        /// <summary>
        /// Modifica/Actualiza un Rol 
        /// </summary>
        /// <param name="_rol"></param>
        /// <returns></returns>
        [HttpPut("PutRol")]
        public async Task<ActionResult<ApplicationRole>> PutRol([FromBody]ApplicationRole _rol)
        {
            try
            {
                _context.Roles.Update(_rol);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
              return  BadRequest($"Ocurrio un error: {ex.Message}");
            }
          

            return _rol;
        }

        /// <summary>
        /// Elimina un rol que no este asignado a un usuario con el Id proporcionado.
        /// </summary>
        /// <param name="_ApplicationRole"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationRole>> Remove([FromBody]ApplicationRole _ApplicationRole)
        {
            try
            {
                List<ApplicationUserRole> _rolasignado = await _context.UserRoles.Where(q => q.RoleId == _ApplicationRole.Id).ToListAsync();
                if (_rolasignado.Count == 0)
                {
                    _context.Roles.Remove(_ApplicationRole);
                    await _context.SaveChangesAsync();
                    return (_ApplicationRole);
                }
                else
                {
                    return BadRequest("El rol esta asignado a Usuarios");
                }
            }
            catch (Exception ex)
            {

               return BadRequest($"Ocurrio un error:{ex.Message}");
            }     

           
        }





    }
}