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
    [Route("api/BlackListCustomers")]
    [ApiController]
    public class BlackListCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BlackListCustomersController(ILogger<BlackListCustomersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de BlackListCustomerses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBlackListCustomers()
        {
            List<BlackListCustomers> Items = new List<BlackListCustomers>();
            try
            {
                Items = await _context.BlackListCustomers.ToListAsync();
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
        /// Obtiene los Datos de la BlackListCustomers por medio del Id enviado.
        /// </summary>
        /// <param name="BlackListCustomersId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BlackListCustomersId}")]
        public async Task<ActionResult<BlackListCustomers>> GetBlackListCustomersById(Int64 BlackListCustomersId)
        {
            BlackListCustomers Items = new BlackListCustomers();
            try
            {
                Items = await _context.BlackListCustomers.Where(q => q.BlackListId == BlackListCustomersId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva BlackListCustomers
        /// </summary>
        /// <param name="_BlackListCustomers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BlackListCustomers>> Insert([FromBody]BlackListCustomers _BlackListCustomers)
        {
            BlackListCustomers _BlackListCustomersq = new BlackListCustomers();
            try
            {
                _BlackListCustomersq = _BlackListCustomers;
                _context.BlackListCustomers.Add(_BlackListCustomersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BlackListCustomersq));
        }

        /// <summary>
        /// Actualiza la BlackListCustomers
        /// </summary>
        /// <param name="_BlackListCustomers"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<BlackListCustomers>> Update([FromBody]BlackListCustomers _BlackListCustomers)
        {
            BlackListCustomers _BlackListCustomersq = _BlackListCustomers;
            try
            {
                _BlackListCustomersq = await (from c in _context.BlackListCustomers
                                 .Where(q => q.BlackListId == _BlackListCustomers.BlackListId)
                                              select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_BlackListCustomersq).CurrentValues.SetValues((_BlackListCustomers));

                //_context.BlackListCustomers.Update(_BlackListCustomersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BlackListCustomersq));
        }

        /// <summary>
        /// Elimina una BlackListCustomers       
        /// </summary>
        /// <param name="_BlackListCustomers"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]BlackListCustomers _BlackListCustomers)
        {
            BlackListCustomers _BlackListCustomersq = new BlackListCustomers();
            try
            {
                _BlackListCustomersq = _context.BlackListCustomers
                .Where(x => x.BlackListId == (Int64)_BlackListCustomers.BlackListId)
                .FirstOrDefault();

                _context.BlackListCustomers.Remove(_BlackListCustomersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BlackListCustomersq));

        }







    }
}