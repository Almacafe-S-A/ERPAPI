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
    [Route("api/CAI")]
    [ApiController]
    public class CAIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CAIController(ILogger<CAIController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CAI , de los documentos que ha tenido y tiene la empresa.
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCAI()
        {
            List<CAI> Items = new List<CAI>();
            try
            {
                Items = await _context.CAI.ToListAsync();
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
        /// Inserta un nuevo cai
        /// </summary>
        /// <param name="_CAI"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CAI _CAI)
        {
            CAI _CAIq = new CAI();
            try
            {
                _CAIq = _CAI;
                _context.CAI.Add(_CAIq);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CAI);
        }

        /// <summary>
        /// Actualiza el CAI 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CAI payload)
        {
            CAI _Cai = payload;
            try
            {
                _context.CAI.Update(_Cai);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Cai);
        }

        /// <summary>
        /// Elimina un cai , un cai se puede eliminar si no ha sido usado en cualquiera
        /// de los documentos fiscales , asegurarse por cai y tipo de documento.
        /// </summary>
        /// <param name="_cai"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CAI _cai)
        {
            CAI _caiq = new CAI();
            try
            {
                _caiq = _context.CAI
                .Where(x => x.IdCAI == (int)_cai.IdCAI)
                .FirstOrDefault();
                _context.CAI.Remove(_caiq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_caiq);

        }







    }
}