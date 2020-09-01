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
    [Route("api/PasswordHistory")]
    [ApiController]
    public class PasswordHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PasswordHistoryController(ILogger<PasswordHistoryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de PasswordHistoryes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPasswordHistory()
        {
            List<PasswordHistory> Items = new List<PasswordHistory>();
            try
            {
                Items = await _context.PasswordHistory.ToListAsync();
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
        /// Obtiene los Datos de la PasswordHistory por medio del Id enviado.
        /// </summary>
        /// <param name="PasswordHistoryId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PasswordHistoryId}")]
        public async Task<IActionResult> GetPasswordHistoryById(Int64 PasswordHistoryId)
        {
            PasswordHistory Items = new PasswordHistory();
            try
            {
                Items = await _context.PasswordHistory.Where(q => q.PasswordHistoryId == PasswordHistoryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> GetPasswordHistoryByUserId(string UserId)
        {
            List<PasswordHistory> Items = new List<PasswordHistory>();
            try
            {
                Items = await _context.PasswordHistory.Where(q => q.UserId == UserId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        


        /// <summary>
        /// Inserta una nueva PasswordHistory
        /// </summary>
        /// <param name="_PasswordHistory"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PasswordHistory>> Insert([FromBody]PasswordHistory _PasswordHistory)
        {
            PasswordHistory _PasswordHistoryq = new PasswordHistory();
            try
            {
                _PasswordHistoryq = _PasswordHistory;
                _context.PasswordHistory.Add(_PasswordHistoryq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PasswordHistoryq));
        }

        /// <summary>
        /// Actualiza la PasswordHistory
        /// </summary>
        /// <param name="_PasswordHistory"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PasswordHistory>> Update([FromBody]PasswordHistory _PasswordHistory)
        {
            PasswordHistory _PasswordHistoryq = _PasswordHistory;
            try
            {
                _PasswordHistoryq = await (from c in _context.PasswordHistory
                                 .Where(q => q.PasswordHistoryId == _PasswordHistory.PasswordHistoryId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_PasswordHistoryq).CurrentValues.SetValues((_PasswordHistory));

                //_context.PasswordHistory.Update(_PasswordHistoryq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PasswordHistoryq));
        }

        /// <summary>
        /// Elimina una PasswordHistory       
        /// </summary>
        /// <param name="_PasswordHistory"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PasswordHistory _PasswordHistory)
        {
            PasswordHistory _PasswordHistoryq = new PasswordHistory();
            try
            {
                _PasswordHistoryq = _context.PasswordHistory
                .Where(x => x.PasswordHistoryId == (Int64)_PasswordHistory.PasswordHistoryId)
                .FirstOrDefault();

                _context.PasswordHistory.Remove(_PasswordHistoryq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PasswordHistoryq));

        }







    }
}