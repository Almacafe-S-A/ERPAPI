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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/UserRol")]
    public class UserRolController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly IMapper mapper;
         private readonly ILogger _logger;

        public UserRolController(ILogger<UserRolController> logger,ApplicationDbContext context
            , RoleManager<ApplicationRole> rolemanager
            , IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
            _logger = logger;
        }

        


        /// <summary>
        /// Obtiene los roles asignados a los usuarios
        /// </summary>
        /// <returns></returns> 
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ApplicationUserRole>>> GetUserRoles()
        {
            List<ApplicationUserRole> _users = new List<ApplicationUserRole>();
            try
            {
               _users= await (_context.UserRoles.ToListAsync());
               // _users = mapper.Map<,ApplicationUserRole>(list);
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return await Task.Run(() => _users);
        }



        /// <summary>
        /// Agrega un nuevo rola a un usuario con los datos proporcionados.
        /// </summary>
        /// <param name="_ApplicationUserRole"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Insert([FromBody]ApplicationUserRole _ApplicationUserRole)
        {

            try
            {
                List<ApplicationUserRole> _listrole = (_context.UserRoles
                                                       .Where(q => q.RoleId == _ApplicationUserRole.RoleId)
                                                        .Where(q => q.UserId == _ApplicationUserRole.UserId)
                                                       ).ToList();
                if (_listrole.Count == 0)
                {
                   // IdentityUserRole<string> _userrole = mapper.Map<IdentityUserRole<string>>(_ApplicationUserRole);
                    _context.UserRoles.Add(_ApplicationUserRole);
                    await _context.SaveChangesAsync();
                    return await Task.Run(() => Ok(_ApplicationUserRole));
                }
                else
                {
                      _logger.LogError($"Ya existe esta agregado el rol para el usuario");
                    return await Task.Run(() => BadRequest("Ya existe esta agregado el rol para el usuario!"));
                }
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
           // ApplicationUserRole _userrole = _ApplicationUserRole;
         
        }


        //[HttpPut("[action]")]
        //public async Task<ActionResult<ApplicationUserRole>> Update([FromBody]ApplicationUserRole _ApplicationUserRole)
        //{
        //    try
        //    {
        //        _context.UserRoles.Update(_ApplicationUserRole);
        //        await _context.SaveChangesAsync();
        //        return Ok(_ApplicationUserRole);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //}

        /// <summary>
        /// Elimina un Rol asignado a un usuario con la llave RoleId Y UserId .
        /// </summary>
        /// <param name="_ApplicationUserRole"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ApplicationUserRole _ApplicationUserRole)
        {
            try
            {

               ApplicationUserRole customer = _context.UserRoles
                  .Where(x => x.RoleId == _ApplicationUserRole.RoleId)
                  .Where(x => x.UserId == _ApplicationUserRole.UserId)
                  .FirstOrDefault();

                if (customer != null)
                {
                    _context.UserRoles.Remove(customer);
                    await _context.SaveChangesAsync();
                    return await Task.Run(() => Ok(_ApplicationUserRole));
                }
                else
                {
                      _logger.LogError($"No existe ese usuario con el rol enviado!");
                    return await Task.Run(() => BadRequest($"No existe ese usuario con el rol enviado!"));
                }
               
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
        

        }




    }
}