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
    [Route("api/PurchDocument")]
    [ApiController]
    public class PurchDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PurchDocumentController(ILogger<PurchDocumentController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PurchDocument paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchDocumentPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PurchDocument> Items = new List<PurchDocument>();
            try
            {
                var query = _context.PurchDocument.AsQueryable();
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
        /// Obtiene el Listado de PurchDocument 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchDocument()
        {
            List<PurchDocument> Items = new List<PurchDocument>();
            try
            {
                Items = await _context.PurchDocument.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{PurchId}")]
        public async Task<IActionResult> GeDocumentByPurchId(Int64 PurchId)
        {
            List<PurchDocument> Items = new List<PurchDocument>();
            try
            {
                Items = await _context.PurchDocument.Where(q => q.PurchId == PurchId).ToListAsync();
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
        /// Obtiene los Datos de la PurchDocument por medio del Id enviado.
        /// </summary>
        /// <param name="PurchDocumentId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchDocumentId}")]
        public async Task<IActionResult> GetPurchDocumentById(Int64 PurchDocumentId)
        {
            PurchDocument Items = new PurchDocument();
            try
            {
                Items = await _context.PurchDocument.Where(q => q.PurchDocumentId == PurchDocumentId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PurchDocument
        /// </summary>
        /// <param name="_PurchDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PurchDocument>> Insert([FromBody]PurchDocument _PurchDocument)
        {
            PurchDocument _PurchDocumentq = new PurchDocument();
            try
            {
                _PurchDocumentq = _PurchDocument;
                _context.PurchDocument.Add(_PurchDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchDocumentq));
        }

        /// <summary>
        /// Actualiza la PurchDocument
        /// </summary>
        /// <param name="_PurchDocument"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PurchDocument>> Update([FromBody]PurchDocument _PurchDocument)
        {
            PurchDocument _PurchDocumentq = _PurchDocument;
            try
            {
                _PurchDocumentq = await (from c in _context.PurchDocument
                                 .Where(q => q.PurchDocumentId == _PurchDocument.PurchDocumentId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PurchDocumentq).CurrentValues.SetValues((_PurchDocument));
                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchDocumentq));
        }

        /// <summary>
        /// Elimina una PurchDocument       
        /// </summary>
        /// <param name="_PurchDocument"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PurchDocument _PurchDocument)
        {
            PurchDocument _PurchDocumentq = new PurchDocument();
            try
            {
                _PurchDocumentq = _context.PurchDocument
                .Where(x => x.PurchDocumentId == (Int64)_PurchDocument.PurchDocumentId)
                .FirstOrDefault();

                _context.PurchDocument.Remove(_PurchDocumentq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchDocumentq));

        }







    }
}