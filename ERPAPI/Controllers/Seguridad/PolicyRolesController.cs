using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/PolicyRoles")]
    [ApiController]
    public class PolicyRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PolicyRolesController(ILogger<PolicyRolesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Policy paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPolicyPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PolicyRoles> Items = new List<PolicyRoles>();
            try
            {
                var query = _context.PolicyRoles.AsQueryable();
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

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de PolicyRoleses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPolicyRoles()
        {
            List<PolicyRoles> Items = new List<PolicyRoles>();
            try
            {
                Items = await _context.PolicyRoles.Include(e => e.Policy).Include( e => e.Role).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la PolicyRoles por medio del Id enviado.
        /// </summary>
        /// <param name="PolicyRolesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PolicyRolesId}")]
        public async Task<IActionResult> GetPolicyRolesById(Guid PolicyRolesId)
        {
            PolicyRoles Items = new PolicyRoles();
            try
            {
                Items = await _context.PolicyRoles.Where(q => q.Id == PolicyRolesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PolicyRoles
        /// </summary>
        /// <param name="_PolicyRoles"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PolicyRoles>> Insert([FromBody]PolicyRoles _PolicyRoles)
        {
            PolicyRoles _PolicyRolesq = new PolicyRoles();
            try
            {
                _PolicyRolesq = _PolicyRoles;
                _context.PolicyRoles.Add(_PolicyRolesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PolicyRolesq));
        }

        /// <summary>
        /// Actualiza la PolicyRoles
        /// </summary>
        /// <param name="_PolicyRoles"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PolicyRoles>> Update([FromBody]PolicyRoles _PolicyRoles)
       // public async Task<ActionResult<PolicyRoles>> Update([FromBody]dynamic dto)
        {
            //PolicyRoles _PolicyRoles = new PolicyRoles();
            PolicyRoles _PolicyRolesq = _PolicyRoles;
            try
            {
              //  _PolicyRoles = JsonConvert.DeserializeObject<PolicyRoles>(dto.ToString());
                _PolicyRolesq = await (from c in _context.PolicyRoles
                                 .Where(q => q.Id == _PolicyRoles.Id)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PolicyRolesq).CurrentValues.SetValues((_PolicyRoles));

                //_context.PolicyRoles.Update(_PolicyRolesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PolicyRolesq));
        }

        /// <summary>
        /// Elimina una PolicyRoles       
        /// </summary>
        /// <param name="_PolicyRoles"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PolicyRoles _PolicyRoles)
        {
            PolicyRoles _PolicyRolesq = new PolicyRoles();
            try
            {
                _PolicyRolesq = _context.PolicyRoles
                .Where(x => x.Id == _PolicyRoles.Id)
                .FirstOrDefault();

                _context.PolicyRoles.Remove(_PolicyRolesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PolicyRolesq));

        }







    }
}