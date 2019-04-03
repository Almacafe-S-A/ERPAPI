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
    public class PoliciesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper mapper;


        public PoliciesController(ApplicationDbContext context
            , RoleManager<IdentityRole> rolemanager
            , IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
        }

        /// <summary>
        /// Obtiene/Retorna todas las politicas creadas
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Policy>>> GetPolicies()
        {
            try
            {
                var Items = await _context.Policy.ToListAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }


        /// <summary>
        /// Obtiene Los Roles que existen por Politica
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PolicyId}")]
        public async Task<ActionResult> GetRolesByPolicy(Guid PolicyId)
        {
            try
            {

                List<string> _policiesrole = await _context.PolicyRoles.Where(q=>q.IdPolicy==PolicyId)
                    .Select(q=>q.IdRol.ToString()).ToListAsync();

                
                List<IdentityRole> Items = await _context.Roles.Where(q => _policiesrole.Contains(q.Id)).ToListAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }
        

         
        /// <summary>
        /// Obtiene los CLAIMS DE USUARIO que existen por politica
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PolicyId}")]
        public async Task<ActionResult> GetUserClaims(Guid PolicyId)
        {
            try
            {
                string query = "";
                query = $"select Id,UserId,ClaimType,ClaimValue,PolicyId from [dbo].[AspNetUserClaims] where PolicyId='{PolicyId.ToString()}'";

                List<ApplicationUserClaim> _usersclaims =
                    await _context.ApplicationUserClaim.FromSql(query).ToListAsync();
            
                                                          
                
               
                return Ok(_usersclaims);

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }


        /// <summary>
        /// Agrega una Politica de seguridad
        /// </summary>
        /// <param name="_Policy"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Insert([FromBody]Policy _Policy)
        {

            try
            {
                List<Policy> _listrole = (_context.Policy
                                          .Where(q => q.Name == _Policy.Name)                                                       
                                         ).ToList();

                if (_listrole.Count == 0)
                {                    
                    _context.Policy.Add(_Policy);
                    await _context.SaveChangesAsync();
                    return Ok(_Policy);
                }
                else
                {
                    return BadRequest("Ya existe la politica con ese nombre!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }  

        }



        /// <summary>
        /// Modifica la politica con el id enviado
        /// </summary>
        /// <param name="_Policy"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Policy>> Update([FromBody]Policy _Policy)
        {
            try
            {               
                _context.Policy.Update(_Policy);
                await _context.SaveChangesAsync();
                return (_Policy);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }



        /// <summary>
        /// Elimina una Politica de seguridad
        /// </summary>
        /// <param name="_Policy"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Delete([FromBody]Policy _Policy)
        {

            try
            {
                List<Policy> _listrole = (_context.Policy
                                          .Where(q => q.Id == _Policy.Id)
                                         ).ToList();

                if (_listrole.Count > 0)
                {
                    _context.Policy.Remove(_Policy);
                    await _context.SaveChangesAsync();
                    return Ok(_Policy);
                }
                else
                {
                    return BadRequest("No existe la policita enviada!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }  

        }



    }


}