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
    [Route("api/Purch")]
    [ApiController]
    public class PurchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public PurchController(ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la Proveedores en una lista.
        /// </summary>

        // GET: api/Purch
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurch()

        {
            List<Purch> Items = new List<Purch>();
            try
            {
                Items = await _context.Purch.ToListAsync();
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
        /// Obtiene los Datos de la Purch por medio del Id enviado.
        /// </summary>
        /// <param name="PurchId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchId}")]
        public async Task<IActionResult> GetPurchById(Int64 PurchId)
        {
            Purch Items = new Purch();
            try
            {
                Items = await _context.Purch.Where(q => q.PurchId == PurchId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Purch
        /// </summary>
        /// <param name="_Purch"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Purch>> Insert([FromBody]Purch _Purch)
        {
            Purch _Purchq = new Purch();
            try
            {
                _Purchq = _Purch;
                _context.Purch.Add(_Purchq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Purchq));
        }

        /// <summary>
        /// Actualiza la Purch
        /// </summary>
        /// <param name="_Purch"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Purch>> Update([FromBody]Purch _Purch)
        {
            Purch _Purchq = _Purch;
            try
            {
                _Purchq = await (from c in _context.Purch
                                 .Where(q => q.PurchId == _Purch.PurchId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Purchq).CurrentValues.SetValues((_Purch));

                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Purchq));
        }

        /// <summary>
        /// Elimina una Purch       
        /// </summary>
        /// <param name="_Purch"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Purch _Purch)
        {
            Purch _Purchq = new Purch();
            try
            {
                _Purchq = _context.Purch
                .Where(x => x.PurchId == (Int64)_Purch.PurchId)
                .FirstOrDefault();

                _context.Purch.Remove(_Purchq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Purchq));

        }
    }
}