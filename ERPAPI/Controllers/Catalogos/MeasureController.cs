/********************************************************************************************************

 -- NAME   :  CRUDMeasure

 -- PROPOSE:  show Measure from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 2.0                  02/01/2020          Marvin.Guillen                Changes to validation delete records
 1.0                  12/12/2019          Marvin.Guillen                Changes to create model
 

 ********************************************************************************************************/

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
    //[Produces("application/json")]
    [Route("api/Measure")]
    public class MeasureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MeasureController(ILogger<MeasureController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Measure paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMeasurePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Measure> Items = new List<Measure>();
            try
            {
                var query = _context.Measure.AsQueryable();
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


        /// <summary>
        /// Obtiene el listado de Medidas.
        /// </summary>
        /// <returns></returns>
        // GET: api/Measure
        [HttpGet("[action]")]
        public async Task<ActionResult<Measure>> GetMeasure()
        {
            List<Measure> Items = new List<Measure>();
            try
            {
                Items = await _context.Measure.ToListAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los datos de la medida con el id enviado
        /// </summary>
        /// <param name="MeasureId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MeasureId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 MeasureId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;// await _context.Branch.Where(a => a.CurrencyId == MeasureId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        /// <summary>
        /// Obtiene los datos de la medida con el id enviado
        /// </summary>
        /// <param name="MeasureId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{MeasureId}")]
        public async Task<ActionResult<Measure>> GetMeasureById(Int64 MeasureId)
        {
            Measure Items = new Measure();
            try
            {
                Items = await _context.Measure.Where(q => q.MeasurelId == MeasureId).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los datos de la medida con el descripcion enviado
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Description}")]
        public async Task<ActionResult<Measure>> GetMeasureByDescrption(String Description)
        {
            Measure Items = new Measure();
            try
            {
                Items = await _context.Measure.Where(q => q.Description == Description).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        /// <summary>
        /// Inserta la Measure
        /// </summary>
        /// <param name="_Measure"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Measure>> Insert([FromBody]Measure _Measure)
        {
            Measure MeasureM = _Measure;
            try
            {
                _context.Measure.Add(MeasureM);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(MeasureM));
        }

        /// <summary>
        /// Actualiza la medida
        /// </summary>
        /// <param name="_Measure"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Measure>> Update([FromBody]Measure _Measure)
        {

            try
            {
                Measure Measureq = (from c in _context.Measure
                                       .Where(q => q.MeasurelId == _Measure.MeasurelId)
                                      select c
                                      ).FirstOrDefault();

                _Measure.CreatedDate = Measureq.CreatedDate;
                _Measure.CreatedUser = Measureq.CreatedUser;

                _context.Entry(Measureq).CurrentValues.SetValues((_Measure));
                //  _context.Currency.Update(_Currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Measure));
        }

        /// <summary>
        /// Elimina la medida
        /// </summary>
        /// <param name="_Measure"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Measure>> Delete([FromBody]Measure _Measure)
        {
            Measure Measurey = new Measure();
            try
            {

                Measurey = _context.Measure
                    .Where(x => x.MeasurelId == _Measure.MeasurelId)
                   .FirstOrDefault();
                    _context.Measure.Remove(Measurey);
                    await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Measurey));

        }


    }
}