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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LineaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public LineaController(ILogger<LineaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Lineas paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLineasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Linea> Items = new List<Linea>();
            try
            {
                var query = _context.Lineas.AsQueryable();
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



        // GET: api/Linea
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLinea()
        {
            List<Linea> Items = new List<Linea>();
            try
            {
                Items = await _context.Lineas.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/LineaGetLineaById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetLineaById(int Id)
        {
            Linea Items = new Linea();
            try
            {
                Items = await _context.Lineas.Where(q => q.LineaId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Linea>> Insert([FromBody]Linea payload)
        {
            Linea Linea = payload;

            try
            {
                _context.Lineas.Add(Linea);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Linea));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Linea>> Update([FromBody]Linea _Linea)
        {

            try
            {
                Linea Lineaq = (from c in _context.Lineas
                   .Where(q => q.LineaId == _Linea.LineaId)
                                                select c
                     ).FirstOrDefault();

                _Linea.FechaCreacion = Lineaq.FechaCreacion;
                _Linea.UsuarioCreacion = Lineaq.UsuarioCreacion;

                _context.Entry(Lineaq).CurrentValues.SetValues((_Linea));
                // _context.Linea.Update(_Linea);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Linea));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Linea payload)
        {
            Linea Linea = new Linea();
            try
            {
                Linea = _context.Lineas
                .Where(x => x.LineaId == (int)payload.LineaId)
                .FirstOrDefault();
                _context.Lineas.Remove(Linea);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Linea));

        }


    }
}
