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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper mapper;


        public UserRolController(ApplicationDbContext context
            , RoleManager<IdentityRole> rolemanager
            , IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
        }


        /// <summary>
        /// Obtiene los roles asignados a los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<IdentityUserRole<string>>>> GetUserRoles()
        {
            List<IdentityUserRole<string>> _users = new List<IdentityUserRole<string>>();
            try
            {
               _users= await (_context.UserRoles.ToListAsync());
               // _users = mapper.Map<,ApplicationUserRole>(list);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return _users;
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
                List<IdentityUserRole<string>> _listrole = (_context.UserRoles
                                                       .Where(q => q.RoleId == _ApplicationUserRole.RoleId)
                                                        .Where(q => q.UserId == _ApplicationUserRole.UserId)
                                                       ).ToList();
                if (_listrole.Count == 0)
                {
                    IdentityUserRole<string> _userrole = mapper.Map<IdentityUserRole<string>>(_ApplicationUserRole);
                    _context.UserRoles.Add(_userrole);
                    await _context.SaveChangesAsync();
                    return Ok(_ApplicationUserRole);
                }
                else
                {
                   return  BadRequest("Ya existe esta agregado el rol para el usuario!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
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
        public async Task<ActionResult<ApplicationUserRole>> Delete([FromBody]ApplicationUserRole _ApplicationUserRole)
        {
            try
            {

                IdentityUserRole<string> customer = _context.UserRoles
                  .Where(x => x.RoleId == _ApplicationUserRole.RoleId)
                  .Where(x => x.UserId == _ApplicationUserRole.UserId)
                  .FirstOrDefault();

                if (customer != null)
                {
                    _context.UserRoles.Remove(customer);
                    await _context.SaveChangesAsync();
                     return Ok(_ApplicationUserRole);
                }
                else
                {
                   return BadRequest($"No existe ese usuario con el rol enviado!");
                }
               
            }
            catch (Exception ex)
            {

                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        

        }




    }
}