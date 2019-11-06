using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Server.HttpSys;
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
    [Route("api/DepreciationFixedAsset")]
    [ApiController]
    public class DepreciationFixedAssetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DepreciationFixedAssetController(ILogger<DepreciationFixedAssetController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de DepreciationFixedAsset paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDepreciationFixedAssetPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<DepreciationFixedAsset> Items = new List<DepreciationFixedAsset>();
            try
            {
                var query = _context.DepreciationFixedAsset.AsQueryable();
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
        /// Obtiene el Listado de DepreciationFixedAssetes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDepreciationFixedAsset()
        {
            List<DepreciationFixedAsset> Items = new List<DepreciationFixedAsset>();
            try
            {
                Items = await _context.DepreciationFixedAsset.ToListAsync();
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
        /// Obtiene el Listado de DepreciationFixedAssets por Id de FixedAsset
        /// </summary>
        /// <param name="FixedAssetId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{FixedAssetId}")]
        public async Task<IActionResult> GetDepreciationFixedAssetByFixedAssetId(Int64 FixedAssetId)
        {
            var query = _context.DepreciationFixedAsset.AsQueryable();
            List<DepreciationFixedAsset> Items = new List<DepreciationFixedAsset>();
            try
            {
                Items = await query.Where(dep => dep.FixedAssetId == FixedAssetId)
                    .ToListAsync();
                //Items = await _context.DepreciationFixedAsset.ToListAsync();
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
        /// Obtiene los Datos de la DepreciationFixedAsset por medio del Id enviado.
        /// </summary>
        /// <param name="DepreciationFixedAssetId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{DepreciationFixedAssetId}")]
        public async Task<IActionResult> GetDepreciationFixedAssetById(Int64 DepreciationFixedAssetId)
        {
            DepreciationFixedAsset Items = new DepreciationFixedAsset();
            try
            {
                Items = await _context.DepreciationFixedAsset.Where(q => q.DepreciationFixedAssetId == DepreciationFixedAssetId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva DepreciationFixedAsset
        /// </summary>
        /// <param name="_DepreciationFixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<DepreciationFixedAsset>> Insert([FromBody]DepreciationFixedAsset _DepreciationFixedAsset)
        {
            DepreciationFixedAsset _DepreciationFixedAssetq = new DepreciationFixedAsset();
            try
            {
                _DepreciationFixedAssetq = _DepreciationFixedAsset;
                _context.DepreciationFixedAsset.Add(_DepreciationFixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_DepreciationFixedAssetq));
        }

        /// <summary>
        /// Actualiza la DepreciationFixedAsset
        /// </summary>
        /// <param name="_DepreciationFixedAsset"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<DepreciationFixedAsset>> Update([FromBody]DepreciationFixedAsset _DepreciationFixedAsset)
        {
            DepreciationFixedAsset _DepreciationFixedAssetq = _DepreciationFixedAsset;
            try
            {
                _DepreciationFixedAssetq = await (from c in _context.DepreciationFixedAsset
                                 .Where(q => q.DepreciationFixedAssetId == _DepreciationFixedAsset.DepreciationFixedAssetId)
                                                  select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_DepreciationFixedAssetq).CurrentValues.SetValues((_DepreciationFixedAsset));

                //_context.DepreciationFixedAsset.Update(_DepreciationFixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DepreciationFixedAssetq));
        }

        /// <summary>
        /// Elimina una DepreciationFixedAsset       
        /// </summary>
        /// <param name="_DepreciationFixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]DepreciationFixedAsset _DepreciationFixedAsset)
        {
            DepreciationFixedAsset _DepreciationFixedAssetq = new DepreciationFixedAsset();
            try
            {
                _DepreciationFixedAssetq = _context.DepreciationFixedAsset
                .Where(x => x.DepreciationFixedAssetId == (Int64)_DepreciationFixedAsset.DepreciationFixedAssetId)
                .FirstOrDefault();

                _context.DepreciationFixedAsset.Remove(_DepreciationFixedAssetq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok());

        }

        /// <summary>
        /// Elimina Todos los DepreciationFixedAsset por FixedAssetID       
        /// </summary>
        /// <param name="_DepreciationFixedAsset"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAll([FromBody]DepreciationFixedAsset _DepreciationFixedAsset)
        {
            try
            {
                _context.Database.ExecuteSqlCommand($"DELETE FROM DepreciationFixedAsset WHERE FixedAssetId={_DepreciationFixedAsset.FixedAssetId}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
            return await Task.Run(() => Ok());

        }

        /// <summary>
        /// Elimina Todos los DepreciationFixedAsset por FixedAssetID       
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteByFixedAssetId([FromBody]Int64 _id)
        {
            try
            {
                _context.Database.ExecuteSqlCommand($"DELETE FROM DepreciationFixedAsset WHERE FixedAssetId={_id}");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
            return await Task.Run(() => Ok());

        }

    }
}