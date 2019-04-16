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

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = payload;
                _context.Branch.Add(branch);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(branch);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]Branch payload)
        {
            Branch branch = payload;
            try
            {
                _context.Branch.Update(branch);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(branch);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = _context.Branch
              .Where(x => x.BranchId == (int)payload.BranchId)
              .FirstOrDefault();
                _context.Branch.Remove(branch);
                _context.SaveChanges();
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