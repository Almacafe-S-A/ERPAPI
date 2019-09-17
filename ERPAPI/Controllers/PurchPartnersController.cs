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
    [Route("api/PurchPartners")]
    [ApiController]
    public class PurchPartnersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PurchPartnersController(ILogger<PurchPartnersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PurchPartners paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchPartnersPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PurchPartners> Items = new List<PurchPartners>();
            try
            {
                var query = _context.PurchPartners.AsQueryable();
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
        /// Obtiene el Listado de PurchPartnerses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchPartners()
        {
            List<PurchPartners> Items = new List<PurchPartners>();
            try
            {
                Items = await _context.PurchPartners.ToListAsync();
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
        public async Task<IActionResult> GetPurchPartnersPurchId(Int64 PurchId)
        {
            List<PurchPartners> Items = new List<PurchPartners>();
            try
            {
                Items = await _context.PurchPartners.Where(q => q.PurchId == PurchId).ToListAsync();
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
        /// Obtiene los Datos de la PurchPartners por medio del Id enviado.
        /// </summary>
        /// <param name="PartnerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PartnerId}")]
        public async Task<IActionResult> GetPurchPartnersById(Int64 PartnerId)
        {
            PurchPartners Items = new PurchPartners();
            try
            {
                Items = await _context.PurchPartners.Where(q => q.PartnerId == PartnerId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PurchPartners
        /// </summary>
        /// <param name="_PurchPartners"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PurchPartners>> Insert([FromBody]PurchPartners _PurchPartners)
        {
            PurchPartners _PurchPartnersq = new PurchPartners();
            try
            {
                _PurchPartnersq = _PurchPartners;
                _context.PurchPartners.Add(_PurchPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchPartnersq));
        }

        /// <summary>
        /// Actualiza la PurchPartners
        /// </summary>
        /// <param name="_PurchPartners"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PurchPartners>> Update([FromBody]PurchPartners _PurchPartners)
        {
            PurchPartners _PurchPartnersq = _PurchPartners;
            try
            {
                _PurchPartnersq = await (from c in _context.PurchPartners
                                 .Where(q => q.PartnerId == _PurchPartners.PartnerId)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PurchPartnersq).CurrentValues.SetValues((_PurchPartners));

                //_context.CustomerPartners.Update(_CustomerPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchPartnersq));
        }

        /// <summary>
        /// Elimina una PurchPartners       
        /// </summary>
        /// <param name="_PurchPartners"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PurchPartners _PurchPartners)
        {
            PurchPartners _PurchPartnersq = new PurchPartners();
            try
            {
                _PurchPartnersq = _context.PurchPartners
                .Where(x => x.PartnerId == (Int64)_PurchPartners.PartnerId)
                .FirstOrDefault();

                _context.PurchPartners.Remove(_PurchPartnersq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PurchPartnersq));

        }







    }
}