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
    [Route("api/ControlPallets")]
    [ApiController]
    public class ControlPalletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlPalletsController(ILogger<ControlPalletsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ControlPalletses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPallets()
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                Items = await _context.ControlPallets.ToListAsync();
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
        /// Obtiene los Datos de la ControlPallets por medio del Id enviado.
        /// </summary>
        /// <param name="ControlPalletsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ControlPalletsId}")]
        public async Task<IActionResult> GetControlPalletsById(Int64 ControlPalletsId)
        {
            ControlPallets Items = new ControlPallets();
            try
            {
                Items = await _context.ControlPallets.Where(q => q.ControlPalletsId == ControlPalletsId).Include(q => q._ControlPalletsLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ControlPallets>> Insert([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ControlPalletsq = _ControlPallets;
                        _context.ControlPallets.Add(_ControlPalletsq);
                        await _context.SaveChangesAsync();
                        foreach (var item in _ControlPalletsq._ControlPalletsLine)
                        {
                            item.ControlPalletsId = _ControlPalletsq.ControlPalletsId;
                            _context.ControlPalletsLine.Add(item);
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
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsq);
        }

        /// <summary>
        /// Actualiza la ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ControlPallets>> Update([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = _ControlPallets;
            try
            {
                _ControlPalletsq = await (from c in _context.ControlPallets
                                 .Where(q => q.ControlPalletsId == _ControlPallets.ControlPalletsId)
                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ControlPalletsq).CurrentValues.SetValues((_ControlPallets));

                //_context.ControlPallets.Update(_ControlPalletsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsq);
        }

        /// <summary>
        /// Elimina una ControlPallets       
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                _ControlPalletsq = _context.ControlPallets
                .Where(x => x.ControlPalletsId == (Int64)_ControlPallets.ControlPalletsId)
                .FirstOrDefault();

                _context.ControlPallets.Remove(_ControlPalletsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ControlPalletsq);

        }







    }
}