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

        /// <summary>
        /// Obtiene el Listado de TipoDocumento paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTipoDocumentoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<TipoDocumento> Items = new List<TipoDocumento>();
            try
            {
                var query = _context.TipoDocumento.AsQueryable();
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTipoDocumento()
        {
            List<TiposDocumento> Items = new List<TiposDocumento>();
            try
            {
                Items = await _context.TiposDocumento.ToListAsync();

                Items = (from c in Items
                                  select new TiposDocumento
                                  {
                                      IdTipoDocumento = c.IdTipoDocumento,
                                      IdEstado = c.IdEstado,
                                      Estado = c.Estado,
                                      UsuarioCreacion = c.UsuarioCreacion,
                                      FechaModificacion = c.FechaModificacion,
                                      Codigo = c.Codigo + "--" + c.Descripcion,
                                      Descripcion = c.Descripcion,
                                      FechaCreacion = c.FechaCreacion,
                                  }).ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{IdTipoDocumento}")]
        public async Task<IActionResult> GetTipoDocumentoById(Int64 IdTipoDocumento)
        {
            TiposDocumento Items = new TiposDocumento();
            try
            {
                Items = await _context.TiposDocumento
                    .Where(q=>q.IdTipoDocumento==IdTipoDocumento).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Descripcion}")]
        public async Task<IActionResult> GetTipoDocumentoByName(string Descripcion)
        {
            TiposDocumento Items = new TiposDocumento();
            try
            {
                Items = await _context.TiposDocumento.Where(q => q.Descripcion== Descripcion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_TiposDocumento));
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

                _context.Entry(_tiposdocumentoq).CurrentValues.SetValues(_TipoDocumento);
              //  _context.TiposDocumento.Update(_TipoDocumento);
              await  _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_TipoDocumento));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TiposDocumento _tiposdocumento)
        {
            TiposDocumento _tiposdocumentoq = new TiposDocumento();
            try
            {
                _tiposdocumentoq = await _context.TiposDocumento
               .Where(x => x.IdTipoDocumento == (Int64)_tiposdocumento.IdTipoDocumento)
               .FirstOrDefaultAsync();

                _context.TiposDocumento.Remove(_tiposdocumentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_tiposdocumentoq));

        }







    }
}