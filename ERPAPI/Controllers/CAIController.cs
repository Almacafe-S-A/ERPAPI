using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    public class CAIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CAIController(ILogger<CAIController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCAI()
        {
            List<CAI> Items = new List<CAI>();
            try
            {
                Items = await _context.CAI.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CAI payload)
        {
            CAI _CAI = new CAI();
            try
            {
                _CAI = payload;
                _context.CAI.Add(_CAI);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CAI);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CAI payload)
        {
            CAI _Cai = payload;
            try
            {
                _context.CAI.Update(_Cai);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Cai);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CAI _cai)
        {
            CAI _caiq = new CAI();
            try
            {
                _caiq = _context.CAI
                .Where(x => x.IdCAI == (int)_cai.IdCAI)
                .FirstOrDefault();
                _context.CAI.Remove(_caiq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_caiq);

        }







    }
}