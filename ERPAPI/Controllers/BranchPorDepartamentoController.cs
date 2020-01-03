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
    [Route("api/BranchPorDepartamento")]
    [ApiController]
    public class BranchPorDepartamentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BranchPorDepartamentoController(ILogger<BranchPorDepartamentoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Obtiene el Listado de Bankes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBranchPorDepartamento()
        {
            List<BranchPorDepartamento> Items = new List<BranchPorDepartamento>();
            try
            {
                Items = await _context.BranchPorDepartamento.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 Id)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;// await _context.BranchPorDepartamento.Where(a => a.IdDepartamento == Id)
                               //     .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        /// <summary>
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetBranchPorDepartamentoById(Int64 Id)
        {
            BranchPorDepartamento Items = new BranchPorDepartamento();
            try
            {
                Items = await _context.BranchPorDepartamento.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{IdBranch}&{IdDepartamento}")]
        public async Task<IActionResult> GetBranchPorDepartamentoByBranchDepartment(Int64 IdBranch,Int64 IdDepartamento)
        {
            BranchPorDepartamento Items = new BranchPorDepartamento();
            try
            {
                Items = await _context.BranchPorDepartamento.Where(q => q.BranchId == IdBranch && q.IdDepartamento ==IdDepartamento).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva Bank
        /// </summary>
        /// <param name="_BranchPorDepartamento"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BranchPorDepartamento>> Insert([FromBody]BranchPorDepartamento _BranchPorDepartamento)
        {
            BranchPorDepartamento _BranchPorDepartamentoq = new BranchPorDepartamento();
            try
            {
                _BranchPorDepartamentoq = _BranchPorDepartamento;
                _context.BranchPorDepartamento.Add(_BranchPorDepartamentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BranchPorDepartamentoq));
        }

        /// <summary>
        /// Actualiza la Bank
        /// </summary>
        /// <param name="_BranchPorDepartamento"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<BranchPorDepartamento>> Update([FromBody]BranchPorDepartamento _BranchPorDepartamento)
        {
            BranchPorDepartamento _BranchPorDepartamentoq = _BranchPorDepartamento;
            try
            {
                _BranchPorDepartamentoq = await (from c in _context.BranchPorDepartamento
                                 .Where(q => q.Id == _BranchPorDepartamento.Id)
                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_BranchPorDepartamentoq).CurrentValues.SetValues((_BranchPorDepartamento));


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BranchPorDepartamentoq));
        }

        /// <summary>
        /// Elimina una Bank       
        /// </summary>
        /// <param name="_BranchPorDepartamento"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]BranchPorDepartamento _BranchPorDepartamento)
        {
            BranchPorDepartamento _BranchPorDepartamentoq = new BranchPorDepartamento();
            try
            {
                _BranchPorDepartamentoq = _context.BranchPorDepartamento
                .Where(x => x.Id == (Int64)_BranchPorDepartamento.Id)
                .FirstOrDefault();

                _context.BranchPorDepartamento.Remove(_BranchPorDepartamentoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BranchPorDepartamentoq));

        }


    }
}