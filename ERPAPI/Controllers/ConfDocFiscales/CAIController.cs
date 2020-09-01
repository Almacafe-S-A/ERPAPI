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
using Newtonsoft.Json;
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
        /// Obtiene el Listado de CAI paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCAIPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CAI> Items = new List<CAI>();
            try
            {
                var query = _context.CAI.AsQueryable();
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
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos por medio del Id enviado.
        /// </summary>
        /// <param name="IdCai"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCai}")]
        public async Task<IActionResult> GetCAIById(Int64 IdCai)
        {
            CAI Items = new CAI();
            try
            {
                Items = await _context.CAI.Where(q=>q.IdCAI==IdCai).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{_cai}")]
        public async Task<IActionResult> GetCAIByDescription(string _cai)
        {
            CAI Items = new CAI();
            try
            {
                Items = await _context.CAI.Where(q => q._cai == _cai).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
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

            return await Task.Run(() => Ok(_CAI));
        }

        /// <summary>
        /// Actualiza el CAI 
        /// </summary>
        /// <param name="_cai"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CAI _cai)
        {
            CAI _Caiq = new CAI();
            try
            {

                _Caiq = (from c in _context.CAI
                                .Where(q => q.IdCAI == _cai.IdCAI)
                                select c
                               ).FirstOrDefault();

                _cai.FechaCreacion = _Caiq.FechaCreacion;
                _cai.UsuarioCreacion = _Caiq.UsuarioCreacion;

                _context.Entry(_Caiq).CurrentValues.SetValues((_cai));
               // _context.CAI.Update(_Caiq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Caiq));
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

            return await Task.Run(() => Ok(_caiq));

        }







    }
}