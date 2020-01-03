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
    [Route("api/CompanyInfo")]
    [ApiController]
    public class CompanyInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CompanyInfoController(ILogger<CompanyInfoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CompanyInfo paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanyInfoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CompanyInfo> Items = new List<CompanyInfo>();
            try
            {
                var query = _context.CompanyInfo.AsQueryable();
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
        /// Obtiene el Listado de CompanyInfoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompanyInfo()
        {
            List<CompanyInfo> Items = new List<CompanyInfo>();
            try
            {
                Items = await _context.CompanyInfo.ToListAsync();
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
        /// Obtiene los Datos de la CompanyInfo por medio del Id enviado.
        /// </summary>
        /// <param name="CompanyInfoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CompanyInfoId}")]
        public async Task<IActionResult> GetCompanyInfoById(Int64 CompanyInfoId)
        {
            CompanyInfo Items = new CompanyInfo();
            try
            {
                Items = await _context.CompanyInfo.Where(q => q.CompanyInfoId == CompanyInfoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// Obtiene los Datos de la CompanyInfo por medio del RTN enviado.
        [HttpPost("[action]")]
        public async Task<IActionResult> GetCompanyByRTN([FromBody]CompanyInfo _Company)
        {
            CompanyInfo Items = new CompanyInfo();
            try
            {
                Items = await _context.CompanyInfo.Where(q => q.Tax_Id == _Company.Tax_Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        /// Obtiene los Datos de la CompanyInfo por medio del RTNMANAGER enviado.
        [HttpPost("[action]")]
        public async Task<IActionResult> GetCompanyByRTNMANAGER([FromBody]CompanyInfo _Company)
        {
            CompanyInfo Items = new CompanyInfo();
            try
            {
                Items = await _context.CompanyInfo.Where(q => q.RTNMANAGER == _Company.RTNMANAGER).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CompanyInfo
        /// </summary>
        /// <param name="_CompanyInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CompanyInfo>> Insert([FromBody]CompanyInfo _CompanyInfo)
        {
            CompanyInfo _CompanyInfoq = new CompanyInfo();
            try
            {
                _CompanyInfoq = _CompanyInfo;
                _context.CompanyInfo.Add(_CompanyInfoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CompanyInfoq));
        }

        /// <summary>
        /// Actualiza la CompanyInfo
        /// </summary>
        /// <param name="_CompanyInfo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CompanyInfo>> Update([FromBody]CompanyInfo _CompanyInfo)
        {
            CompanyInfo _CompanyInfoq = _CompanyInfo;
            try
            {
                _CompanyInfoq = await (from c in _context.CompanyInfo
                                 .Where(q => q.CompanyInfoId == _CompanyInfo.CompanyInfoId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CompanyInfoq).CurrentValues.SetValues((_CompanyInfo));

                //_context.CompanyInfo.Update(_CompanyInfoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CompanyInfoq));
        }

        /// <summary>
        /// Elimina una CompanyInfo       
        /// </summary>
        /// <param name="_CompanyInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CompanyInfo _CompanyInfo)
        {
            CompanyInfo _CompanyInfoq = new CompanyInfo();
            try
            {
                _CompanyInfoq = _context.CompanyInfo
                .Where(x => x.CompanyInfoId == (Int64)_CompanyInfo.CompanyInfoId)
                .FirstOrDefault();

                _context.CompanyInfo.Remove(_CompanyInfoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CompanyInfoq));

        }







    }
}