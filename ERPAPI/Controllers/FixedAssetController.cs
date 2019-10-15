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
    [Route("api/FixedAsset")]
    [ApiController]
    public class FixedAssetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FixedAssetController(ILogger<FixedAssetController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FixedAsset paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAssetPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FixedAsset> Items = new List<FixedAsset>();
            try
            {
                var query = _context.FixedAsset.AsQueryable();
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
        /// Obtiene el Listado de FixedAssetes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAsset()
        {
            List<FixedAsset> Items = new List<FixedAsset>();
            try
            {
                Items = await _context.FixedAsset.ToListAsync();
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
        /// Obtiene los Datos de la FixedAsset por medio del Id enviado.
        /// </summary>
        /// <param name="FixedAssetId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{FixedAssetId}")]
        public async Task<IActionResult> GetFixedAssetById(Int64 FixedAssetId)
        {
            FixedAsset Items = new FixedAsset();
            try
            {
                Items = await _context.FixedAsset.Where(q => q.FixedAssetId == FixedAssetId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva FixedAsset
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<FixedAsset>> Insert([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = new FixedAsset();
            try
            {
                _FixedAssetq = _FixedAsset;
                _context.FixedAsset.Add(_FixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_FixedAssetq));
        }

        /// <summary>
        /// Actualiza la FixedAsset
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<FixedAsset>> Update([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = _FixedAsset;
            try
            {
                _FixedAssetq = await (from c in _context.FixedAsset
                                 .Where(q => q.FixedAssetId == _FixedAsset.FixedAssetId)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_FixedAssetq).CurrentValues.SetValues((_FixedAsset));

                //_context.FixedAsset.Update(_FixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetq));
        }

        /// <summary>
        /// Elimina una FixedAsset       
        /// </summary>
        /// <param name="_FixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FixedAsset _FixedAsset)
        {
            FixedAsset _FixedAssetq = new FixedAsset();
            try
            {
                _FixedAssetq = _context.FixedAsset
                .Where(x => x.FixedAssetId == (Int64)_FixedAsset.FixedAssetId)
                .FirstOrDefault();

                _context.FixedAsset.Remove(_FixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetq));

        }







    }
}