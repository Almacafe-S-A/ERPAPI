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
    [Route("api/CustomerContractWareHouse")]
    [ApiController]
    public class CustomerContractWareHouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerContractWareHouseController(ILogger<CustomerContractWareHouseController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerContractWareHouse paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractWareHousePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerContractWareHouse> Items = new List<CustomerContractWareHouse>();
            try
            {
                var query = _context.CustomerContractWareHouse.AsQueryable();
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
        /// Obtiene el Listado de CustomerContractWareHousees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerContractWareHouse()
        {
            List<CustomerContractWareHouse> Items = new List<CustomerContractWareHouse>();
            try
            {
                Items = await _context.CustomerContractWareHouse.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la CustomerContractWareHouse por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerContractWareHouseId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerContractWareHouseId}")]
        public async Task<IActionResult> GetCustomerContractWareHouseById(Int64 CustomerContractWareHouseId)
        {
            CustomerContractWareHouse Items = new CustomerContractWareHouse();
            try
            {
                Items = await _context.CustomerContractWareHouse.Where(q => q.CustomerContractWareHouseId == CustomerContractWareHouseId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerContractWareHouse
        /// </summary>
        /// <param name="_CustomerContractWareHouse"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerContractWareHouse>> Insert([FromBody]CustomerContractWareHouse _CustomerContractWareHouse)
        {
            CustomerContractWareHouse _CustomerContractWareHouseq = new CustomerContractWareHouse();
            try
            {
                _CustomerContractWareHouseq = _CustomerContractWareHouse;
                _context.CustomerContractWareHouse.Add(_CustomerContractWareHouseq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractWareHouseq));
        }

        /// <summary>
        /// Actualiza la CustomerContractWareHouse
        /// </summary>
        /// <param name="_CustomerContractWareHouse"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerContractWareHouse>> Update([FromBody]CustomerContractWareHouse _CustomerContractWareHouse)
        {
            CustomerContractWareHouse _CustomerContractWareHouseq = _CustomerContractWareHouse;
            try
            {
                _CustomerContractWareHouseq = await (from c in _context.CustomerContractWareHouse
                                 .Where(q => q.CustomerContractWareHouseId == _CustomerContractWareHouse.CustomerContractWareHouseId)
                                                     select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerContractWareHouseq).CurrentValues.SetValues((_CustomerContractWareHouse));

                //_context.CustomerContractWareHouse.Update(_CustomerContractWareHouseq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractWareHouseq));
        }

        /// <summary>
        /// Elimina una CustomerContractWareHouse       
        /// </summary>
        /// <param name="_CustomerContractWareHouse"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerContractWareHouse _CustomerContractWareHouse)
        {
            CustomerContractWareHouse _CustomerContractWareHouseq = new CustomerContractWareHouse();
            try
            {
                _CustomerContractWareHouseq = _context.CustomerContractWareHouse
                .Where(x => x.CustomerContractWareHouseId == (Int64)_CustomerContractWareHouse.CustomerContractWareHouseId)
                .FirstOrDefault();

                _context.CustomerContractWareHouse.Remove(_CustomerContractWareHouseq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerContractWareHouseq));

        }







    }
}