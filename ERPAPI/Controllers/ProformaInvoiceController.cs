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
    [Route("api/ProformaInvoice")]
    [ApiController]
    public class ProformaInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProformaInvoiceController(ILogger<ProformaInvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ProformaInvoicees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoice()
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                Items = await _context.ProformaInvoice.ToListAsync();
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
        /// Obtiene los Datos de la ProformaInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="ProformaInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProformaInvoiceId}")]
        public async Task<IActionResult> GetProformaInvoiceById(Int64 ProformaInvoiceId)
        {
            ProformaInvoice Items = new ProformaInvoice();
            try
            {
                Items = await _context.ProformaInvoice.Where(q => q.ProformaId == ProformaInvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Insert([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.ProformaInvoice.Add(_ProformaInvoice);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _ProformaInvoice.ProformaInvoiceLine)
                        {
                            item.ProformaInvoiceId = _ProformaInvoice.ProformaId;
                            _context.ProformaInvoiceLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_ProformaInvoiceq = _ProformaInvoice;
                //_context.ProformaInvoice.Add(_ProformaInvoiceq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ProformaInvoiceq);
        }

        /// <summary>
        /// Actualiza la ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Update([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = _ProformaInvoice;
            try
            {
                _ProformaInvoiceq = await (from c in _context.ProformaInvoice
                                 .Where(q => q.ProformaId == _ProformaInvoice.ProformaId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ProformaInvoiceq).CurrentValues.SetValues((_ProformaInvoice));

                //_context.ProformaInvoice.Update(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ProformaInvoiceq);
        }

        /// <summary>
        /// Elimina una ProformaInvoice       
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                _ProformaInvoiceq = _context.ProformaInvoice
                .Where(x => x.ProformaId == (Int64)_ProformaInvoice.ProformaId)
                .FirstOrDefault();

                _context.ProformaInvoice.Remove(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ProformaInvoiceq);

        }







    }
}