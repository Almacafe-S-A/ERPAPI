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
        public async Task<IActionResult> GetTipoDocumento()
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
            TiposDocumento _TiposDocumento = new TiposDocumento();
            try
            {
                _TiposDocumento = payload;
                _context.TiposDocumento.Add(_TiposDocumento);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_TiposDocumento);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]TiposDocumento _TipoDocumento)
        {
          
            try
            {

                TiposDocumento _tiposdocumentoq = await (from c in _context.TiposDocumento
                       .Where(q => q.IdTipoDocumento == _TipoDocumento.IdTipoDocumento)
                            select c
                         ).FirstOrDefaultAsync();

                _TipoDocumento.FechaCreacion = _tiposdocumentoq.FechaCreacion;
                _TipoDocumento.UsuarioCreacion = _tiposdocumentoq.UsuarioCreacion;

                _context.TiposDocumento.Update(_tiposdocumentoq);
              await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_TipoDocumento);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TiposDocumento _tiposdocumento)
        {
            TiposDocumento _tiposdocumentoq = new TiposDocumento();
            try
            {
                _tiposdocumentoq = _context.TiposDocumento
               .Where(x => x.IdTipoDocumento == (Int64)_tiposdocumento.IdTipoDocumento)
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