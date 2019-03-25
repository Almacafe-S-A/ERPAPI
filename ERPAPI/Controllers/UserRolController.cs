using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Agrega un nuevo usuario con los datos proporcionados , el CustomerId es un identity.
        /// </summary>
        /// <param name="_ApplicationUserRole"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Insert([FromBody]ApplicationUserRole _ApplicationUserRole)
        {

            try
            {
                IdentityUserRole<string> _userrole = mapper.Map<IdentityUserRole<string>>(_ApplicationUserRole);
                _context.UserRoles.Add(_userrole);
                await _context.SaveChangesAsync();
                return Ok(_ApplicationUserRole);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           // ApplicationUserRole _userrole = _ApplicationUserRole;
         
        }

        /// <summary>
        /// Actualiza rol con usuario con el CustomerId y datos del cliente proporcionados.
        /// </summary>
        /// <param name="_ApplicationUserRole"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Update([FromBody]ApplicationUserRole _ApplicationUserRole)
        {
            try
            {
                _context.UserRoles.Update(_ApplicationUserRole);
                await _context.SaveChangesAsync();
                return Ok(_ApplicationUserRole);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
        }

        /// <summary>
        /// Elimina un cliente con el CustomerId proporcionado.
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

                _ApplicationUserRole = mapper.Map<ApplicationUserRole>(customer);
                _context.UserRoles.Remove(customer);
                await _context.SaveChangesAsync();


                return Ok(_ApplicationUserRole);
            }
            catch (Exception ex)
            {

                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        

        }




    }
}