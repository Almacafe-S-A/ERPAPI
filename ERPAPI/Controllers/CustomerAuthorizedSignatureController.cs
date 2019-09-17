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
    [Route("api/CustomerAuthorizedSignature")]
    [ApiController]
    public class CustomerAuthorizedSignatureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerAuthorizedSignatureController(ILogger<CustomerAuthorizedSignatureController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerAuthorizedSignature paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerAuthorizedSignaturePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerAuthorizedSignature> Items = new List<CustomerAuthorizedSignature>();
            try
            {
                var query = _context.CustomerAuthorizedSignature.AsQueryable();
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

        /// <summary>
        /// Obtiene el Listado de CustomerAuthorizedSignaturees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerAuthorizedSignature()
        {
            List<CustomerAuthorizedSignature> Items = new List<CustomerAuthorizedSignature>();
            try
            {
                Items = await _context.CustomerAuthorizedSignature.ToListAsync();
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
        public async Task<IActionResult> GetCustomerAuthorizedSignatureByCustomerId(Int64 CustomerId)
        {
            List<CustomerAuthorizedSignature> Items = new List<CustomerAuthorizedSignature>();
            try
            {
                Items = await _context.CustomerAuthorizedSignature.Where(q=>q.CustomerId==CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la CustomerAuthorizedSignature por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerAuthorizedSignatureId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerAuthorizedSignatureId}")]
        public async Task<IActionResult> GetCustomerAuthorizedSignatureById(Int64 CustomerAuthorizedSignatureId)
        {
            CustomerAuthorizedSignature Items = new CustomerAuthorizedSignature();
            try
            {
                Items = await _context.CustomerAuthorizedSignature.Where(q => q.CustomerAuthorizedSignatureId == CustomerAuthorizedSignatureId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerAuthorizedSignature
        /// </summary>
        /// <param name="_CustomerAuthorizedSignature"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerAuthorizedSignature>> Insert([FromBody]CustomerAuthorizedSignature _CustomerAuthorizedSignature)
        {
            CustomerAuthorizedSignature _CustomerAuthorizedSignatureq = new CustomerAuthorizedSignature();
            try
            {
                _CustomerAuthorizedSignatureq = _CustomerAuthorizedSignature;
                _context.CustomerAuthorizedSignature.Add(_CustomerAuthorizedSignatureq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerAuthorizedSignatureq));
        }

        /// <summary>
        /// Actualiza la CustomerAuthorizedSignature
        /// </summary>
        /// <param name="_CustomerAuthorizedSignature"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerAuthorizedSignature>> Update([FromBody]CustomerAuthorizedSignature _CustomerAuthorizedSignature)
        {
            CustomerAuthorizedSignature _CustomerAuthorizedSignatureq = _CustomerAuthorizedSignature;
            try
            {
                _CustomerAuthorizedSignatureq = await (from c in _context.CustomerAuthorizedSignature
                                 .Where(q => q.CustomerAuthorizedSignatureId == _CustomerAuthorizedSignature.CustomerAuthorizedSignatureId)
                                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerAuthorizedSignatureq).CurrentValues.SetValues((_CustomerAuthorizedSignature));

                //_context.CustomerAuthorizedSignature.Update(_CustomerAuthorizedSignatureq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerAuthorizedSignatureq));
        }

        /// <summary>
        /// Elimina una CustomerAuthorizedSignature       
        /// </summary>
        /// <param name="_CustomerAuthorizedSignature"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerAuthorizedSignature _CustomerAuthorizedSignature)
        {
            CustomerAuthorizedSignature _CustomerAuthorizedSignatureq = new CustomerAuthorizedSignature();
            try
            {
                _CustomerAuthorizedSignatureq = _context.CustomerAuthorizedSignature
                .Where(x => x.CustomerAuthorizedSignatureId == (Int64)_CustomerAuthorizedSignature.CustomerAuthorizedSignatureId)
                .FirstOrDefault();

                _context.CustomerAuthorizedSignature.Remove(_CustomerAuthorizedSignatureq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerAuthorizedSignatureq));

        }







    }
}