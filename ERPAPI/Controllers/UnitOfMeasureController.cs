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

        // GET: api/UnitOfMeasure
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUnitOfMeasure()
        {
            List<UnitOfMeasure> Items = new List<UnitOfMeasure>();
            try
            {
                Items = await _context.UnitOfMeasure.ToListAsync();
                //int Count = Items.Count();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok( Items);
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
          
            return Ok(unitOfMeasure);
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(_UnitOfMeasure);
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
         
            return Ok(unitOfMeasure);

        }



    }
}