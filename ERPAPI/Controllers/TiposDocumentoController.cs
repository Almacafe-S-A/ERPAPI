using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/TiposDocumento")]
    [ApiController]
    public class TiposDocumentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public TiposDocumentoController(ILogger<TiposDocumentoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCAI()
        {
            List<TiposDocumento> Items = new List<TiposDocumento>();
            try
            {
                Items = await _context.TiposDocumento.ToListAsync();
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
        public async Task<IActionResult> Insert([FromBody]TiposDocumento payload)
        {
            TiposDocumento _CAI = new TiposDocumento();
            try
            {
                _CAI = payload;
                _context.TiposDocumento.Add(_CAI);
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
        public async Task<IActionResult> Update([FromBody]TiposDocumento payload)
        {
            TiposDocumento _tiposdocumento = payload;
            try
            {
                _context.TiposDocumento.Update(_tiposdocumento);
              await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_tiposdocumento);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TiposDocumento _tiposdocumento)
        {
            TiposDocumento _tiposdocumentoq = new TiposDocumento();
            try
            {
                _tiposdocumentoq = _context.TiposDocumento
               .Where(x => x.IdTipoDocumento == (int)_tiposdocumentoq.IdTipoDocumento)
               .FirstOrDefault();
                _context.TiposDocumento.Remove(_tiposdocumentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_tiposdocumentoq);

        }







    }
}