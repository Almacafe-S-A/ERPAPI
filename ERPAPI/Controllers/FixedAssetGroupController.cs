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
    [Route("api/FixedAssetGroup")]
    [ApiController]
    public class FixedAssetGroupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FixedAssetGroupController(ILogger<FixedAssetGroupController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FixedAssetGroup paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAssetGroupPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FixedAssetGroup> Items = new List<FixedAssetGroup>();
            try
            {
                var query = _context.FixedAssetGroup.AsQueryable();
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
        /// Obtiene el Listado de FixedAssetGroupes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFixedAssetGroup()
        {
            List<FixedAssetGroup> Items = new List<FixedAssetGroup>();
            try
            {
                Items = await _context.FixedAssetGroup.ToListAsync();
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
        /// Obtiene los Datos de la FixedAssetGroup por medio del Id enviado.
        /// </summary>
        /// <param name="FixedAssetGroupId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{FixedAssetGroupId}")]
        public async Task<IActionResult> GetFixedAssetGroupById(Int64 FixedAssetGroupId)
        {
            FixedAssetGroup Items = new FixedAssetGroup();
            try
            {
                Items = await _context.FixedAssetGroup.Where(q => q.FixedAssetGroupId == FixedAssetGroupId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva FixedAssetGroup
        /// </summary>
        /// <param name="_FixedAssetGroup"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<FixedAssetGroup>> Insert([FromBody]FixedAssetGroup _FixedAssetGroup)
        {
            FixedAssetGroup _FixedAssetGroupq = new FixedAssetGroup();
            try
            {
                _FixedAssetGroupq = _FixedAssetGroup;
                _context.FixedAssetGroup.Add(_FixedAssetGroupq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_FixedAssetGroupq));
        }

        /// <summary>
        /// Actualiza la FixedAssetGroup
        /// </summary>
        /// <param name="_FixedAssetGroup"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<FixedAssetGroup>> Update([FromBody]FixedAssetGroup _FixedAssetGroup)
        {
            FixedAssetGroup _FixedAssetGroupq = _FixedAssetGroup;
            try
            {
                _FixedAssetGroupq = await (from c in _context.FixedAssetGroup
                                 .Where(q => q.FixedAssetGroupId == _FixedAssetGroup.FixedAssetGroupId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_FixedAssetGroupq).CurrentValues.SetValues((_FixedAssetGroup));

                //_context.FixedAssetGroup.Update(_FixedAssetGroupq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetGroupq));
        }

        /// <summary>
        /// Elimina una FixedAssetGroup       
        /// </summary>
        /// <param name="_FixedAssetGroup"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FixedAssetGroup _FixedAssetGroup)
        {
            FixedAssetGroup _FixedAssetGroupq = new FixedAssetGroup();
            try
            {
                _FixedAssetGroupq = _context.FixedAssetGroup
                .Where(x => x.FixedAssetGroupId == (Int64)_FixedAssetGroup.FixedAssetGroupId)
                .FirstOrDefault();

                _context.FixedAssetGroup.Remove(_FixedAssetGroupq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FixedAssetGroupq));

        }







    }
}