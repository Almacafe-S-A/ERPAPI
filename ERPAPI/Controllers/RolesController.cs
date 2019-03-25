using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper mapper;


        public RolesController(ApplicationDbContext context
            , RoleManager<IdentityRole> rolemanager
            ,IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
        }

        /// <summary>
        /// Obtiene un rol , filtrado por su id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RoleId}")]
        public async Task<ActionResult> GetRoleById(string RoleId)
        {
            try
            {
                IdentityRole Items = await _context.Roles.Where(q => q.Id == RoleId).FirstOrDefaultAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
        }


        [HttpGet("[action]")]
        public async Task<List<ApplicationRole>> GetJsonRoles()
        {
            List<ApplicationRole> _users = new List<ApplicationRole>();
            try
            {
                var _rol = await _context.Roles.ToListAsync();
                _users = mapper.Map<List<ApplicationRole>>(_rol);
            }
            catch (System.Exception myExc)
            {
                throw (new Exception(myExc.Message));
            }
            return (_users);
        }



        /// <summary>
        /// Obtiene el listado de roles 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<ActionResult<List<ApplicationRole>>> GetRoles()
        {
            List<IdentityRole> _roles = new List<IdentityRole>();
            List<ApplicationRole> _rolesprod = new List<ApplicationRole>();
            try
            {
              _roles  = await _context.Roles.ToListAsync();
              _rolesprod = mapper.Map<List<IdentityRole>, List<ApplicationRole>>(_roles);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return _rolesprod;
        }


   

        /// <summary>
        /// Crea un rol que permite tener accesos a recursos del API.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateRole")]
        public async Task<ActionResult<IdentityRole>> CreateRole([FromBody] ApplicationRole model)
        {
            try
            {
                 IdentityRole _idrole = mapper.Map<ApplicationRole, IdentityRole>(model);
                //new ApplicationRole { Name = model.Name, NormalizedName = model.NormalizedName }
                var result = await _rolemanager.CreateAsync(model);
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
        public async Task<ActionResult<IdentityRole>> PutRol([FromBody]ApplicationRole _rol)
        {
            try
            {
                IdentityRole _idrole = mapper.Map<ApplicationRole, IdentityRole>(_rol);

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
        public async Task<ActionResult<ApplicationRole>> Delete([FromBody]ApplicationRole _ApplicationRole)
        {
            try
            {
                List<IdentityUserRole<string>> _rolasignado = await _context.UserRoles.Where(q => q.RoleId == _ApplicationRole.Id).ToListAsync();
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