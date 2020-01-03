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
using ERPAPI.Helpers;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/CheckAccountLines")]
    [ApiController]
    public class CheckAccountLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CheckAccountLinesController(ILogger<CheckAccountLinesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CheckAccountLineses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCheckAccountLines()
        {
            List<CheckAccountLines> Items = new List<CheckAccountLines>();
            try
            {
                Items = await _context.CheckAccountLines.ToListAsync();
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
        /// Obtiene los Datos de la CheckAccountLines por medio del Id enviado.
        /// </summary>
        /// <param name="CheckAccountLinesId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountLinesId}")]
        public async Task<IActionResult> GetCheckAccountLinesById(Int64 CheckAccountLinesId)
        {
            CheckAccountLines Items = new CheckAccountLines();
            try
            {
                Items = await _context.CheckAccountLines.Where(q => q.Id == CheckAccountLinesId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el detalle de las mercaderias.
        /// </summary>
        /// <param name="CheckAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountId}")]
        public async Task<IActionResult> GetCheckAccountLinesByCheckAccountId(Int64 CheckAccountId)
        {
            List<CheckAccountLines> Items = new List<CheckAccountLines>();
            try
            {
                Items = await _context.CheckAccountLines
                             .Where(q => q.CheckAccountId == CheckAccountId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva CheckAccountLines
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CheckAccountLines>> Insert([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            try
            {

                _CheckAccountLinesq = _CheckAccountLines;

                Numalet let;
                let = new Numalet();
                let.SeparadorDecimalSalida = "Lempiras";
                let.MascaraSalidaDecimal = "00/100 ";
                let.ApocoparUnoParteEntera = true;
                _CheckAccountLinesq.AmountWords = let.ToCustomCardinal((_CheckAccountLinesq.Ammount)).ToUpper();
                _CheckAccountLinesq.IdEstado = 1;
                _CheckAccountLinesq.Estado = "Activo";
                //Conteo Cheques
                CheckAccount chequera = await _context.CheckAccount.Where(c =>c.CheckAccountId == _CheckAccountLinesq.CheckAccountId).FirstOrDefaultAsync();
                chequera.NumeroActual = Convert.ToInt32(_CheckAccountLines.CheckNumber);
                if(chequera.NumeroActual> Convert.ToInt32(chequera.NoFinal))
                {
                    return BadRequest("No se pueden emitir más Cheques.");

                }
                else
                {
                    _context.CheckAccountLines.Add(_CheckAccountLinesq);
                    CheckAccount _CheckAccountq = await (from c in _context.CheckAccount
                                 .Where(q => q.CheckAccountId == _CheckAccountLinesq.CheckAccountId)
                                            select c
                                ).FirstOrDefaultAsync();

                    _context.Entry(_CheckAccountq).CurrentValues.SetValues((chequera));
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));
        }

        /// <summary>
        /// Actualiza la CheckAccountLines
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CheckAccountLines>> Update([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = _CheckAccountLines;
            try
            {
                _CheckAccountLinesq = await (from c in _context.CheckAccountLines
                                 .Where(q => q.Id == _CheckAccountLines.Id)
                                             select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CheckAccountLinesq).CurrentValues.SetValues((_CheckAccountLines));

                //_context.CheckAccountLines.Update(_CheckAccountLinesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));
        }

        /// <summary>
        /// Elimina una CheckAccountLines       
        /// </summary>
        /// <param name="_CheckAccountLines"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CheckAccountLines _CheckAccountLines)
        {
            CheckAccountLines _CheckAccountLinesq = new CheckAccountLines();
            try
            {
                _CheckAccountLinesq = _context.CheckAccountLines
                .Where(x => x.Id == (Int64)_CheckAccountLines.Id)
                .FirstOrDefault();

                _context.CheckAccountLines.Remove(_CheckAccountLinesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountLinesq));

        }







    }
}