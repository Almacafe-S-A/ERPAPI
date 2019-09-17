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
    [Route("api/UnitOfMeasure")]
    public class UnitOfMeasureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UnitOfMeasureController(ILogger<UnitOfMeasureController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de UnitOfMeasure paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUnitOfMeasurePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<UnitOfMeasure> Items = new List<UnitOfMeasure>();
            try
            {
                var query = _context.UnitOfMeasure.AsQueryable();
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

        // GET: api/UnitOfMeasure
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUnitOfMeasure()
        {
            List<UnitOfMeasure> Items = new List<UnitOfMeasure>();
            try
            {
                Items = await _context.UnitOfMeasure.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/UnitOfMeasureGetUnitOfMeasureById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetUnitOfMeasureById(int Id)
        {
            UnitOfMeasure Items = new UnitOfMeasure();
            try
            {
                Items = await _context.UnitOfMeasure.Where(q => q.UnitOfMeasureId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        
        [HttpGet("[action]/{UnitOfMeasureName}")]
        public async Task<IActionResult> GetUnitOfMeasureByName(string UnitOfMeasureName)
        {
            UnitOfMeasure Items = new UnitOfMeasure();
            try
            {
                Items = await _context.UnitOfMeasure.Where(q => q.UnitOfMeasureName.Equals(UnitOfMeasureName)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<UnitOfMeasure>> Insert([FromBody]UnitOfMeasure payload)
        {
            UnitOfMeasure unitOfMeasure = payload;

            try
            {
                _context.UnitOfMeasure.Add(unitOfMeasure);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(unitOfMeasure));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<UnitOfMeasure>> Update([FromBody]UnitOfMeasure _UnitOfMeasure)
        {           

            try
            {
                UnitOfMeasure unitOfMeasureq = (from c in _context.UnitOfMeasure
                   .Where(q => q.UnitOfMeasureId == _UnitOfMeasure.UnitOfMeasureId)
                                                   select c
                     ).FirstOrDefault();

                _UnitOfMeasure.FechaCreacion = unitOfMeasureq.FechaCreacion;
                _UnitOfMeasure.UsuarioCreacion = unitOfMeasureq.UsuarioCreacion;

                _context.Entry(unitOfMeasureq).CurrentValues.SetValues((_UnitOfMeasure));
                // _context.UnitOfMeasure.Update(_UnitOfMeasure);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_UnitOfMeasure));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]UnitOfMeasure payload)
        {
            UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
            try
            {
                unitOfMeasure = _context.UnitOfMeasure
                .Where(x => x.UnitOfMeasureId == (int)payload.UnitOfMeasureId)
                .FirstOrDefault();
                _context.UnitOfMeasure.Remove(unitOfMeasure);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(unitOfMeasure));

        }



    }
}