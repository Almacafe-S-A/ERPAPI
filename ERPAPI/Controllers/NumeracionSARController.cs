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
    public class NumeracionSARController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public NumeracionSARController(ILogger<NumeracionSARController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNumeracion()
        {
            List<NumeracionSAR> Items = new List<NumeracionSAR>();
            try
            {
                Items = await _context.NumeracionSAR.ToListAsync();
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
        public async Task<IActionResult> Insert([FromBody]NumeracionSAR _NumeracionSAR)
        {
            NumeracionSAR _NumeracionSARq = new NumeracionSAR();
            try
            {
                _NumeracionSARq = _NumeracionSAR;
                _context.NumeracionSAR.Add(_NumeracionSARq);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_NumeracionSAR);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]NumeracionSAR _NumeracionSARq)
        {
            NumeracionSAR __NumeracionSARq = _NumeracionSARq;
            try
            {
                _context.NumeracionSAR.Update(_NumeracionSARq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(__NumeracionSARq);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]NumeracionSAR __NumeracionSAR)
        {
            NumeracionSAR __NumeracionSARq = new NumeracionSAR();
            try
            {
                __NumeracionSARq = _context.NumeracionSAR
                .Where(x => x.IdNumeracion== (int)__NumeracionSARq.IdNumeracion)
                .FirstOrDefault();
                _context.NumeracionSAR.Remove(__NumeracionSARq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(__NumeracionSARq);

        }







    }
}