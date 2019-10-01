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
    [Route("api/CostCenter")]
    [ApiController]
    public class CostCenterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CostCenterController(ILogger<CostCenterController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CostCenter paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCostCenterPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CostCenter> Items = new List<CostCenter>();
            try
            {
                var query = _context.CostCenter.AsQueryable();
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
        /// Obtiene el Listado de CostCenteres 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCostCenter()
        {
            List<CostCenter> Items = new List<CostCenter>();
            try
            {
                Items = await _context.CostCenter.ToListAsync();
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
        /// Obtiene los Datos de la CostCenter por medio del Id enviado.
        /// </summary>
        /// <param name="CostCenterId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CostCenterId}")]
        public async Task<IActionResult> GetCostCenterById(Int64 CostCenterId)
        {
            CostCenter Items = new CostCenter();
            try
            {
                Items = await _context.CostCenter.Where(q => q.CostCenterId == CostCenterId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CostCenter
        /// </summary>
        /// <param name="_CostCenter"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CostCenter>> Insert([FromBody]CostCenter _CostCenter)
        {
            CostCenter _CostCenterq = new CostCenter();
            try
            {
                _CostCenterq = _CostCenter;
                _context.CostCenter.Add(_CostCenterq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CostCenterq));
        }

        /// <summary>
        /// Actualiza la CostCenter
        /// </summary>
        /// <param name="_CostCenter"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CostCenter>> Update([FromBody]CostCenter _CostCenter)
        {
            CostCenter _CostCenterq = _CostCenter;
            try
            {
                _CostCenterq = await (from c in _context.CostCenter
                                 .Where(q => q.CostCenterId == _CostCenter.CostCenterId)
                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CostCenterq).CurrentValues.SetValues((_CostCenter));

                //_context.CostCenter.Update(_CostCenterq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CostCenterq));
        }

        /// <summary>
        /// Elimina una CostCenter       
        /// </summary>
        /// <param name="_CostCenter"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CostCenter _CostCenter)
        {
            CostCenter _CostCenterq = new CostCenter();
            try
            {
                _CostCenterq = _context.CostCenter
                .Where(x => x.CostCenterId == (Int64)_CostCenter.CostCenterId)
                .FirstOrDefault();

                _context.CostCenter.Remove(_CostCenterq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CostCenterq));

        }







    }
}