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
    [Route("api/[controller]")]
    public class PoliciesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly IMapper mapper;
        private readonly ILogger _logger;

        public PoliciesController(ILogger<PoliciesController> logger,
            ApplicationDbContext context
            , RoleManager<ApplicationRole> rolemanager
            , IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de Policy paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPolicyPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Policy> Items = new List<Policy>();
            try
            {
                var query = _context.Policy.AsQueryable();
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

           
            return await Task.Run(() => Ok(Items));
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
                return await Task.Run(() => Ok(Items));
                //return Ok(Items);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        [HttpGet("[action]/{PolicyId}")]
        public async Task<ActionResult<Policy>> GetPoliciesById(Guid PolicyId)
        {
            try
            {
                var Items = await _context.Policy.Where(q=>q.Id==PolicyId).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
                //return Ok(Items);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }
        [HttpGet("[action]/{PolicyName}")]
        public async Task<ActionResult<Policy>> GetPoliciesByName(String PolicyName)
        {
            try
            {
                var Items = await _context.Policy.Where(q => q.Name == PolicyName).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
                //return Ok(Items);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
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

                List<Guid> _policiesrole = await _context.PolicyRoles.Where(q=>q.IdPolicy==PolicyId)
                    .Select(q=>q.IdRol).ToListAsync();

                
                List<ApplicationRole> Items = await _context.Roles.Where(q => _policiesrole.Contains(q.Id)).ToListAsync();
                return await Task.Run(() => Ok(Items));
                // return Ok(Items);

            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
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
                //string query = "";
                // query = $"select Id,UserId,ClaimType,ClaimValue,PolicyId from [dbo].[AspNetUserClaims] where PolicyId='{PolicyId.ToString()}'";

                List<ApplicationUserClaim> _usersclaims = await _context.UserClaims.Where(q => q.PolicyId == PolicyId).ToListAsync();
                  //  await _context.UserClaims.FromSql(query).ToListAsync();


                return await Task.Run(() => Ok(_usersclaims));
                //  return Ok(_usersclaims);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
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
                    return await Task.Run(() => Ok(_Policy));
                    // return Ok(_Policy);
                }
                else
                {
                     _logger.LogError($"Ya existe la politica con ese nombre!");
                    return BadRequest("Ya existe la politica con ese nombre!");
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
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
            Policy _Policyq = _Policy;
            try
            {

                _Policyq = await (from c in _context.Policy
                                .Where(q => q.Id == _Policy.Id)
                                             select c
                               ).FirstOrDefaultAsync();

                _context.Entry(_Policyq).CurrentValues.SetValues((_Policy));
                // _context.Policy.Update(_Policy);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(_Policy));
                // return (_Policy);
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
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

                _Policy = await (_context.Policy
                                          .Where(q => q.Id == _Policy.Id)
                             ).FirstOrDefaultAsync();

                if (_listrole.Count > 0)
                {
                    _context.Policy.Remove(_Policy);
                    await _context.SaveChangesAsync();
                    return await Task.Run(() => Ok(_Policy));
                    // return Ok(_Policy);
                }
                else
                {

                    return await Task.Run(() => BadRequest("No existe la policita enviada!"));
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