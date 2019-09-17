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
    [Route("api/CustomerDocument")]
    [ApiController]
    public class CustomerDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerDocumentController(ILogger<CustomerDocumentController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerDocument paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerDocumentPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerDocument> Items = new List<CustomerDocument>();
            try
            {
                var query = _context.CustomerDocument.AsQueryable();
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
        /// Obtiene el Listado de CustomerDocumentes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerDocument()
        {
            List<CustomerDocument> Items = new List<CustomerDocument>();
            try
            {
                Items = await _context.CustomerDocument.ToListAsync();
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
        public async Task<IActionResult> GeDocumentByCustomerId(Int64 CustomerId)
        {
            List<CustomerDocument> Items = new List<CustomerDocument>();
            try
            {
                Items = await _context.CustomerDocument.Where(q=>q.CustomerId== CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la CustomerDocument por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerDocumentId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerDocumentId}")]
        public async Task<IActionResult> GetCustomerDocumentById(Int64 CustomerDocumentId)
        {
            CustomerDocument Items = new CustomerDocument();
            try
            {
                Items = await _context.CustomerDocument.Where(q => q.CustomerDocumentId == CustomerDocumentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerDocument
        /// </summary>
        /// <param name="_CustomerDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerDocument>> Insert([FromBody]CustomerDocument _CustomerDocument)
        {
            CustomerDocument _CustomerDocumentq = new CustomerDocument();
            try
            {
                _CustomerDocumentq = _CustomerDocument;
                _context.CustomerDocument.Add(_CustomerDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerDocumentq));
        }

        /// <summary>
        /// Actualiza la CustomerDocument
        /// </summary>
        /// <param name="_CustomerDocument"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerDocument>> Update([FromBody]CustomerDocument _CustomerDocument)
        {
            CustomerDocument _CustomerDocumentq = _CustomerDocument;
            try
            {
                _CustomerDocumentq = await (from c in _context.CustomerDocument
                                 .Where(q => q.CustomerDocumentId == _CustomerDocument.CustomerDocumentId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CustomerDocumentq).CurrentValues.SetValues((_CustomerDocument));

                //_context.CustomerDocument.Update(_CustomerDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerDocumentq));
        }

        /// <summary>
        /// Elimina una CustomerDocument       
        /// </summary>
        /// <param name="_CustomerDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerDocument _CustomerDocument)
        {
            CustomerDocument _CustomerDocumentq = new CustomerDocument();
            try
            {
                _CustomerDocumentq = _context.CustomerDocument
                .Where(x => x.CustomerDocumentId == (Int64)_CustomerDocument.CustomerDocumentId)
                .FirstOrDefault();

                _context.CustomerDocument.Remove(_CustomerDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CustomerDocumentq));

        }







    }
}