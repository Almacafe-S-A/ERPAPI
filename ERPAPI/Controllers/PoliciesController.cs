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





    }


}