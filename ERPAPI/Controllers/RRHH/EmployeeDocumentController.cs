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
    [Route("api/EmployeeDocument")]
    [ApiController]
    public class EmployeeDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeeDocumentController(ILogger<EmployeeDocumentController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EmployeeDocument paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeDocumentPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EmployeeDocument> Items = new List<EmployeeDocument>();
            try
            {
                var query = _context.EmployeeDocument.AsQueryable();
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
        /// Obtiene el Listado de EmployeeDocumentes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeDocument()
        {
            List<EmployeeDocument> Items = new List<EmployeeDocument>();
            try
            {
                Items = await _context.EmployeeDocument.ToListAsync();
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
        /// Obtiene los Datos de la EmployeeDocument por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeId}")]
        public async Task<IActionResult> GetDocumentByEmployeeId(Int64 EmployeeId)
        {
            List<EmployeeDocument> Items = new List<EmployeeDocument>();
            try
            {
                Items = await _context.EmployeeDocument.Where(q => q.EmployeeId == EmployeeId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la EmployeeDocument por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeDocumentId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeDocumentId}")]
        public async Task<IActionResult> GetEmployeeDocumentById(Int64 EmployeeDocumentId)
        {
            EmployeeDocument Items = new EmployeeDocument();
            try
            {
                Items = await _context.EmployeeDocument.Where(q => q.EmployeeDocumentId == EmployeeDocumentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva EmployeeDocument
        /// </summary>
        /// <param name="_EmployeeDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeeDocument>> Insert([FromBody]EmployeeDocument _EmployeeDocument)
        {
            EmployeeDocument _EmployeeDocumentq = new EmployeeDocument();
            try
            {
                _EmployeeDocumentq = _EmployeeDocument;
                _context.EmployeeDocument.Add(_EmployeeDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_EmployeeDocumentq));
        }

        /// <summary>
        /// Actualiza la EmployeeDocument
        /// </summary>
        /// <param name="_EmployeeDocument"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EmployeeDocument>> Update([FromBody]EmployeeDocument _EmployeeDocument)
        {
            EmployeeDocument _EmployeeDocumentq = _EmployeeDocument;
            try
            {
                _EmployeeDocumentq = await (from c in _context.EmployeeDocument
                                 .Where(q => q.EmployeeDocumentId == _EmployeeDocument.EmployeeDocumentId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EmployeeDocumentq).CurrentValues.SetValues((_EmployeeDocument));

                //_context.EmployeeDocument.Update(_EmployeeDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_EmployeeDocumentq));
        }

        /// <summary>
        /// Elimina una EmployeeDocument       
        /// </summary>
        /// <param name="_EmployeeDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EmployeeDocument _EmployeeDocument)
        {
            EmployeeDocument _EmployeeDocumentq = new EmployeeDocument();
            try
            {
                _EmployeeDocumentq = _context.EmployeeDocument
                .Where(x => x.EmployeeDocumentId == (Int64)_EmployeeDocument.EmployeeDocumentId)
                .FirstOrDefault();

                _context.EmployeeDocument.Remove(_EmployeeDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_EmployeeDocumentq));

        }







    }
}