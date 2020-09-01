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
    [Route("api/EmployeeAbsence")]
    [ApiController]
    public class EmployeeAbsenceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeeAbsenceController(ILogger<EmployeeAbsenceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EmployeeAbsence paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeAbsencePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EmployeeAbsence> Items = new List<EmployeeAbsence>();
            try
            {
                var query = _context.EmployeeAbsence.AsQueryable();
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
        /// Obtiene el Listado de EmployeeAbsencees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeAbsence()
        {
            List<EmployeeAbsence> Items = new List<EmployeeAbsence>();
            try
            {
                Items = await _context.EmployeeAbsence.ToListAsync();
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
        /// Obtiene los Datos de la EmployeeAbsence por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeAbsenceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeAbsenceId}")]
        public async Task<IActionResult> GetEmployeeAbsenceById(Int64 EmployeeAbsenceId)
        {
            EmployeeAbsence Items = new EmployeeAbsence();
            try
            {
                Items = await _context.EmployeeAbsence.Where(q => q.EmployeeAbsenceId == EmployeeAbsenceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva EmployeeAbsence
        /// </summary>
        /// <param name="_EmployeeAbsence"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeeAbsence>> Insert([FromBody]EmployeeAbsence _EmployeeAbsence)
        {
            EmployeeAbsence _EmployeeAbsenceq = new EmployeeAbsence();
            try
            {
                _EmployeeAbsenceq = _EmployeeAbsence;
                _context.EmployeeAbsence.Add(_EmployeeAbsenceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_EmployeeAbsenceq));
        }

        /// <summary>
        /// Actualiza la EmployeeAbsence
        /// </summary>
        /// <param name="_EmployeeAbsence"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EmployeeAbsence>> Update([FromBody]EmployeeAbsence _EmployeeAbsence)
        {
            EmployeeAbsence _EmployeeAbsenceq = _EmployeeAbsence;
            try
            {
                _EmployeeAbsenceq = await (from c in _context.EmployeeAbsence
                                 .Where(q => q.EmployeeAbsenceId == _EmployeeAbsence.EmployeeAbsenceId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EmployeeAbsenceq).CurrentValues.SetValues((_EmployeeAbsence));

                //_context.EmployeeAbsence.Update(_EmployeeAbsenceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_EmployeeAbsenceq));
        }

        /// <summary>
        /// Elimina una EmployeeAbsence       
        /// </summary>
        /// <param name="_EmployeeAbsence"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EmployeeAbsence _EmployeeAbsence)
        {
            EmployeeAbsence _EmployeeAbsenceq = new EmployeeAbsence();
            try
            {
                _EmployeeAbsenceq = _context.EmployeeAbsence
                .Where(x => x.EmployeeAbsenceId == (Int64)_EmployeeAbsence.EmployeeAbsenceId)
                .FirstOrDefault();

                _context.EmployeeAbsence.Remove(_EmployeeAbsenceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_EmployeeAbsenceq));

        }







    }
}