using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.Models;
using ERP.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Produces("application/json")]
    [Route("api/Branch")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BranchController(ILogger<BranchController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        

        /// <summary>
        /// Obtiene el Listado de sucursales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBranch()
        {
            List<Branch> Items = new List<Branch>();
            try
            {
                Items = await _context.Branch.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
               
          //  int Count = Items.Count();
            return Ok( Items);
        }

        /// <summary>
        /// Obtiene la sucursal mediante el Id enviado.
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BranchId}")]
        public async Task<IActionResult> GetBranchById(int BranchId)
        {
            Branch Items = new Branch();
            try
            {
                Items = await _context.Branch.Where(q=>q.BranchId== BranchId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return Ok(Items);
        }


        /// <summary>
        /// Inserta una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> Insert([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = payload;
                _context.Branch.Add(branch);
              await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(branch);
        }

        /// <summary>
        /// Actualiza una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]Branch payload)
        {
            Branch branch = payload;
            try
            {
                branch = (from c in _context.Branch
                                    .Where(q => q.BranchId == payload.BranchId)
                                      select c
                                    ).FirstOrDefault();

                payload.FechaCreacion = branch.FechaCreacion;
                payload.UsuarioCreacion = branch.UsuarioCreacion;

                _context.Entry(branch).CurrentValues.SetValues(payload);
                // _context.Branch.Update(payload);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(branch);
        }


        /// <summary>
        /// Elimina una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = _context.Branch
               .Where(x => x.BranchId == (int)payload.BranchId)
               .FirstOrDefault();
                _context.Branch.Remove(branch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(branch);

        }


        
    }
}