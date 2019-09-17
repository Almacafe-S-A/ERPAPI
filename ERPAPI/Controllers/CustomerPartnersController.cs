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
    [Route("api/CustomerPartners")]
    [ApiController]
    public class CustomerPartnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerPartnersController(ILogger<CustomerPartnersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerPartners paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerPartnersPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerPartners> Items = new List<CustomerPartners>();
            try
            {
                var query = _context.CustomerPartners.AsQueryable();
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
        /// Obtiene el Listado de CustomerPartnerses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerPartners()
        {
            List<CustomerPartners> Items = new List<CustomerPartners>();
            try
            {
                Items = await _context.CustomerPartners.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetCustomerPartnersCustomerId(Int64 CustomerId)
        {
            List<CustomerPartners> Items = new List<CustomerPartners>();
            try
            {
                Items = await _context.CustomerPartners.Where(q => q.CustomerId == CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la CustomerPartners por medio del Id enviado.
        /// </summary>
        /// <param name="PartnerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PartnerId}")]
        public async Task<IActionResult> GetCustomerPartnersById(Int64 PartnerId)
        {
            CustomerPartners Items = new CustomerPartners();
            try
            {
                Items = await _context.CustomerPartners.Where(q => q.PartnerId == PartnerId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerPartners
        /// </summary>
        /// <param name="_CustomerPartners"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerPartners>> Insert([FromBody]CustomerPartners _CustomerPartners)
        {
            CustomerPartners _CustomerPartnersq = new CustomerPartners();
            try
            {
                _CustomerPartnersq = _CustomerPartners;
                _context.CustomerPartners.Add(_CustomerPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerPartnersq));
        }

        /// <summary>
        /// Actualiza la CustomerPartners
        /// </summary>
        /// <param name="_CustomerPartners"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerPartners>> Update([FromBody]CustomerPartners _CustomerPartners)
        {
            CustomerPartners _CustomerPartnersq = _CustomerPartners;
            try
            {
                _CustomerPartnersq = await (from c in _context.CustomerPartners
                                 .Where(q => q.PartnerId == _CustomerPartners.PartnerId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerPartnersq).CurrentValues.SetValues((_CustomerPartners));

                //_context.CustomerPartners.Update(_CustomerPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerPartnersq));
        }

        /// <summary>
        /// Elimina una CustomerPartners       
        /// </summary>
        /// <param name="_CustomerPartners"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerPartners _CustomerPartners)
        {
            CustomerPartners _CustomerPartnersq = new CustomerPartners();
            try
            {
                _CustomerPartnersq = _context.CustomerPartners
                .Where(x => x.PartnerId == (Int64)_CustomerPartners.PartnerId)
                .FirstOrDefault();

                _context.CustomerPartners.Remove(_CustomerPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerPartnersq));

        }







    }
}