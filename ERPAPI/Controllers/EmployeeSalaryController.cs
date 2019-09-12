using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/EmployeeSalary")]
    [ApiController]
    public class EmployeeSalaryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public EmployeeSalaryController(ILogger<EmployeeSalaryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene los Datos de la EmployeeSalary en una lista.
        /// </summary>

        // GET: api/EmployeeSalary
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeSalary()

        {
            List<EmployeeSalary> Items = new List<EmployeeSalary>();
            try
            {
                Items = await _context.EmployeeSalary.ToListAsync();
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
        /// Obtiene los Datos de la EmployeeSalary por medio del Id enviado.
        /// </summary>
        /// <param name="EmployeeSalaryId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EmployeeSalaryId}")]
        public async Task<IActionResult> GetEmployeeSalaryById(Int64 EmployeeSalaryId)
        {
            EmployeeSalary Items = new EmployeeSalary();
            try
            {
                Items = await _context.EmployeeSalary.Where(q => q.EmployeeSalaryId == EmployeeSalaryId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }





        /// <summary>
        /// Inserta una nueva EmployeeSalary
        /// </summary>
        /// <param name="_EmployeeSalary"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EmployeeSalary>> Insert([FromBody]EmployeeSalary _EmployeeSalary)
        {
            EmployeeSalary _EmployeeSalaryq = new EmployeeSalary();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeSalaryq = _EmployeeSalary;
                        _context.EmployeeSalary.Add(_EmployeeSalaryq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeSalary.EmployeeSalaryId,
                            DocType = "EmployeeSalary",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeSalary, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeSalary.CreatedUser,
                            UsuarioModificacion = _EmployeeSalary.ModifiedUser,
                            UsuarioEjecucion = _EmployeeSalary.ModifiedUser,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_EmployeeSalaryq));
        }

        /// <summary>
        /// Actualiza la EmployeeSalary
        /// </summary>
        /// <param name="_EmployeeSalary"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EmployeeSalary>> Update([FromBody]EmployeeSalary _EmployeeSalary)
        {
            EmployeeSalary _EmployeeSalaryq = _EmployeeSalary;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeSalaryq = await (from c in _context.EmployeeSalary
                                         .Where(q => q.EmployeeSalaryId == _EmployeeSalary.EmployeeSalaryId)
                                              select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_EmployeeSalaryq).CurrentValues.SetValues((_EmployeeSalary));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeSalary.EmployeeSalaryId,
                            DocType = "EmployeeSalary",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeSalaryq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_EmployeeSalary, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EmployeeSalary.CreatedUser,
                            UsuarioModificacion = _EmployeeSalary.ModifiedUser,
                            UsuarioEjecucion = _EmployeeSalary.ModifiedUser,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_EmployeeSalaryq));
        }

        /// <summary>
        /// Elimina una EmployeeSalary       
        /// </summary>
        /// <param name="_EmployeeSalary"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EmployeeSalary _EmployeeSalary)
        {
            EmployeeSalary _EmployeeSalaryq = new EmployeeSalary();
            try
            {
                _EmployeeSalaryq = _context.EmployeeSalary
                .Where(x => x.EmployeeSalaryId == (Int64)_EmployeeSalary.EmployeeSalaryId)
                .FirstOrDefault();

                _context.EmployeeSalary.Remove(_EmployeeSalaryq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_EmployeeSalaryq));

        }

    }
}