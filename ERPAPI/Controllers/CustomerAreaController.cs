using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/CustomerArea")]
    [ApiController]
    public class CustomerAreaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerAreaController(ILogger<CustomerAreaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerAreaes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerArea()
        {
            List<CustomerArea> Items = new List<CustomerArea>();
            try
            {
                Items = await _context.CustomerArea.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la CustomerArea por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerAreaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerAreaId}")]
        public async Task<IActionResult> GetCustomerAreaById(Int64 CustomerAreaId)
        {
            CustomerArea Items = new CustomerArea();
            try
            {
                Items = await _context.CustomerArea.Where(q => q.CustomerAreaId == CustomerAreaId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva CustomerArea
        /// </summary>
        /// <param name="_CustomerArea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerArea>> Insert([FromBody]CustomerArea _CustomerArea)
        {
            CustomerArea _CustomerAreaq = new CustomerArea();
            try
            {
                _CustomerAreaq = _CustomerArea;
                _context.CustomerArea.Add(_CustomerAreaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerAreaq);
        }

        /// <summary>
        /// Actualiza la CustomerArea
        /// </summary>
        /// <param name="_CustomerArea"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerArea>> Update([FromBody]CustomerArea _CustomerArea)
        {
            CustomerArea _CustomerAreaq = _CustomerArea;
            try
            {
                _CustomerAreaq = await (from c in _context.CustomerArea
                                 .Where(q => q.CustomerAreaId == _CustomerArea.CustomerAreaId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerAreaq).CurrentValues.SetValues((_CustomerArea));

                //_context.CustomerArea.Update(_CustomerAreaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerAreaq);
        }

        /// <summary>
        /// Elimina una CustomerArea       
        /// </summary>
        /// <param name="_CustomerArea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerArea _CustomerArea)
        {
            CustomerArea _CustomerAreaq = new CustomerArea();
            try
            {
                _CustomerAreaq = _context.CustomerArea
                .Where(x => x.CustomerAreaId == (Int64)_CustomerArea.CustomerAreaId)
                .FirstOrDefault();

                _context.CustomerArea.Remove(_CustomerAreaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CustomerAreaq);

        }







    }
}