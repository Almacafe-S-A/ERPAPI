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
    [Route("api/Dimensions")]
    [ApiController]
    public class DimensionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public DimensionsController(ILogger<DimensionsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Dimensions paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDimensionsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Dimensions> Items = new List<Dimensions>();
            try
            {
                var query = _context.Dimensions.AsQueryable();
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


        // GET: api/Dimensions
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDimensions()

        {
            List<Dimensions> Items = new List<Dimensions>();
            try
            {
                Items = await _context.Dimensions.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }

        // GET: api/Dimensions/5
        [HttpGet("[action]/{Numid}")]
        public async Task<IActionResult> GetDimensionsById(string Numid)

        {
            Dimensions Items = new Dimensions();
            try
            {
                Items = await _context.Dimensions.Where(q => q.Num == Numid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        // POST: api/Dimensions
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]Dimensions payload)
        {
            Dimensions dimensions = new Dimensions();
            try
            {
                dimensions = payload;
                _context.Dimensions.Add(dimensions);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(dimensions));
        }

        
        // PUT: api/Dimensions/5
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]Dimensions payload)
        {
            Dimensions dimensions = payload;
            try
            {
                dimensions = (from c in _context.Dimensions
                                    .Where(q => q.Num == payload.Num)
                          select c
                                    ).FirstOrDefault();

                payload.FechaCreacion = dimensions.FechaCreacion;
                payload.UsuarioCreacion = dimensions.UsuarioCreacion;

                _context.Entry(dimensions).CurrentValues.SetValues(payload);
                // _context.Branch.Update(payload);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(dimensions));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Dimensions payload)
        {
            Dimensions dimensions = new Dimensions();
            try
            {
                dimensions = _context.Dimensions
               .Where(x => x.Num == (string)payload.Num)
               .FirstOrDefault();
                _context.Dimensions.Remove(dimensions);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(dimensions));

        }



    }
}