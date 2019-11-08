using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceEndorsementLineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InsuranceEndorsementLineController(ILogger<InsuranceEndorsementLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InsuranceEndorsementLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceEndorsementLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InsuranceEndorsementLine> Items = new List<InsuranceEndorsementLine>();
            try
            {
                var query = _context.InsuranceEndorsementLine.AsQueryable();
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


        [HttpGet("[action]/{InsuranceEndorsementId}")]
        public async Task<IActionResult> GetInsuranceEndorsementLineByInsuranceEndorsementId(Int64 InsuranceEndorsementId)
        {
            List<InsuranceEndorsementLine> Items = new List<InsuranceEndorsementLine>();
            try
            {
                Items = await _context.InsuranceEndorsementLine
                             .Where(q => q.InsuranceEndorsementId == InsuranceEndorsementId).ToListAsync();
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
        /// Obtiene el Listado de InsuranceEndorsementLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInsuranceEndorsementLine()
        {
            List<InsuranceEndorsementLine> Items = new List<InsuranceEndorsementLine>();
            try
            {
                Items = await _context.InsuranceEndorsementLine.ToListAsync();
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
        /// Obtiene los Datos de la InsuranceEndorsementLine por medio del Id enviado.
        /// </summary>
        /// <param name="InsuranceEndorsementLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InsuranceEndorsementLineId}")]
        public async Task<IActionResult> GetInsuranceEndorsementLineById(Int64 InsuranceEndorsementLineId)
        {
            InsuranceEndorsementLine Items = new InsuranceEndorsementLine();
            try
            {
                Items = await _context.InsuranceEndorsementLine.Where(q => q.InsuranceEndorsementLineId == InsuranceEndorsementLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva InsuranceEndorsementLine
        /// </summary>
        /// <param name="_InsuranceEndorsementLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InsuranceEndorsementLine>> Insert([FromBody]InsuranceEndorsementLine _InsuranceEndorsementLine)
        {
            InsuranceEndorsementLine _InsuranceEndorsementLineq = new InsuranceEndorsementLine();
            try
            {
                _InsuranceEndorsementLineq = _InsuranceEndorsementLine;
                _context.InsuranceEndorsementLine.Add(_InsuranceEndorsementLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InsuranceEndorsementLineq));
        }

        /// <summary>
        /// Actualiza la InsuranceEndorsementLine
        /// </summary>
        /// <param name="_InsuranceEndorsementLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<InsuranceEndorsementLine>> Update([FromBody]InsuranceEndorsementLine _InsuranceEndorsementLine)
        {
            InsuranceEndorsementLine _InsuranceEndorsementLineq = _InsuranceEndorsementLine;
            try
            {
                _InsuranceEndorsementLineq = await (from c in _context.InsuranceEndorsementLine
                                 .Where(q => q.InsuranceEndorsementLineId == _InsuranceEndorsementLine.InsuranceEndorsementLineId)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InsuranceEndorsementLineq).CurrentValues.SetValues((_InsuranceEndorsementLine));

                //_context.InsuranceEndorsementLine.Update(_InsuranceEndorsementLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceEndorsementLineq));
        }

        /// <summary>
        /// Elimina una InsuranceEndorsementLine       
        /// </summary>
        /// <param name="_InsuranceEndorsementLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]InsuranceEndorsementLine _InsuranceEndorsementLine)
        {
            InsuranceEndorsementLine _InsuranceEndorsementLineq = new InsuranceEndorsementLine();
            try
            {
                _InsuranceEndorsementLineq = _context.InsuranceEndorsementLine
                .Where(x => x.InsuranceEndorsementLineId == (Int64)_InsuranceEndorsementLine.InsuranceEndorsementLineId)
                .FirstOrDefault();

                _context.InsuranceEndorsementLine.Remove(_InsuranceEndorsementLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_InsuranceEndorsementLineq));

        }





    }
}
