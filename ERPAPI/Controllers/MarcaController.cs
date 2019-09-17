using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Marca")]
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MarcaController(ILogger<MarcaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Marcas paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarcasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Marca> Items = new List<Marca>();
            try
            {
                var query = _context.Marcas.AsQueryable();
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

        // GET: api/Marca
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarca()
        {
            List<Marca> Items = new List<Marca>();
            try
            {
                Items = await _context.Marcas.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/MarcaGetMarcaById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMarcaById(int Id)
        {
            Marca Items = new Marca();
            try
            {
                Items = await _context.Marcas.Where(q => q.MarcaId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Marca>> Insert([FromBody]Marca payload)
        {
            Marca Marca = payload;

            try
            {
                _context.Marcas.Add(Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Marca));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Marca>> Update([FromBody]Marca _Marca)
        {

            try
            {
                Marca Marcaq = (from c in _context.Marcas
                   .Where(q => q.MarcaId == _Marca.MarcaId)
                                                select c
                     ).FirstOrDefault();

                _Marca.FechaCreacion = Marcaq.FechaCreacion;
                _Marca.UsuarioCreacion = Marcaq.UsuarioCreacion;

                _context.Entry(Marcaq).CurrentValues.SetValues((_Marca));
                // _context.Marca.Update(_Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Marca));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Marca payload)
        {
            Marca Marca = new Marca();
            try
            {
                Marca = _context.Marcas
                .Where(x => x.MarcaId == (int)payload.MarcaId)
                .FirstOrDefault();
                _context.Marcas.Remove(Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Marca));

        }



    }
}