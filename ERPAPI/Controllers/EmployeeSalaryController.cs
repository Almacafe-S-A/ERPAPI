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
        /// 

        /// <summary>
        /// Obtiene el Listado de Dimensions paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDimensionsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Dimensions> Items = new List<Dimensions>();
            try
            {
                var query = _context.Dimensions.AsQueryable();
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


        // GET: api/EmployeeSalary
        [HttpGet("[action]")]
        public async Task<ActionResult<List<EmployeeSalary>>> GetEmployeeSalary()

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
             Alert _Alertq = new Alert();
            string employeeName =  _context.Employees.Where(e => e.IdEmpleado == _EmployeeSalary.IdEmpleado).FirstOrDefault().NombreEmpleado;

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EmployeeSalaryq = _EmployeeSalary;
                        _context.EmployeeSalary.Add(_EmployeeSalaryq);
                       

                        //////////////Alerta al guardar un nuevo Salario/////////////

                        _Alertq.AlertName = "Cambio de Salario";
                        _Alertq.DocumentName = "EMPLEADO";
                        _Alertq.Code = "PERSON005";
                        _Alertq.FechaCreacion = DateTime.Now;
                        _Alertq.FechaModificacion = DateTime.Now;
                        _Alertq.UsuarioCreacion = _EmployeeSalary.CreatedUser;
                        _Alertq.UsuarioModificacion = _EmployeeSalary.ModifiedUser;
                        _Alertq.AlertType = "PERSONA";
                        _Alertq.Description = "Se Modifico el Salario al Empleado: " + employeeName;

                        _context.Alert.Add(_Alertq);

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
                        List<EmployeeSalary> _EmployeeSalaryU = new List<EmployeeSalary>();
                        _EmployeeSalaryU = await (from c in _context.EmployeeSalary
                                         .Where(q => q.IdEmpleado == _EmployeeSalary.IdEmpleado && q.EmployeeSalaryId != _EmployeeSalary.EmployeeSalaryId) select c ).ToListAsync();
                        //_EmployeeSalaryU = await _context.EmployeeSalary.Where(q => q.IdEmpleado == _EmployeeSalary.IdEmpleado && q.EmployeeSalaryId != _EmployeeSalary.EmployeeSalaryId).FirstOrDefaultAsync();
                        foreach (EmployeeSalary p in _EmployeeSalaryU)
                        {
                            p.IdEstado = 2;
                            _context.Entry(p).CurrentValues.SetValues((p));
                        }


                        Employees _Employee = new Employees();
                        _Employee = await _context.Employees.Where(q => q.IdEmpleado == _EmployeeSalary.IdEmpleado).FirstOrDefaultAsync();

                        var Salario = _EmployeeSalary.QtySalary;
                        _Employee.Salario = Salario;
                        _context.Entry(_Employee).CurrentValues.SetValues((_Employee));



                        //await _context.SaveChangesAsync();

                        
                        //_Employee = await (from c in _context.Employees
                        //                 .Where(q => q.IdEmpleado == _EmployeeSalary.IdEmpleado)
                        //                          select c
                        //                ).FirstOrDefaultAsync();
                       

                        //_context.Alert.Update(_Alertq);
                        //await _context.SaveChangesAsync();
                        BitacoraWrite _writeejec = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EmployeeSalary.EmployeeSalaryId,
                            DocType = "Employeed",
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
                        
                        //await _context.SaveChangesAsync();

                        if (_EmployeeSalary.IdEstado == 1)
                        {
                            Employees _Employee = new Employees();
                            _Employee = await _context.Employees.Where(q => q.IdEmpleado == _EmployeeSalary.IdEmpleado).FirstOrDefaultAsync();

                            var Salario = _EmployeeSalary.QtySalary;
                            _Employee.Salario = Salario;
                            _context.Entry(_Employee).CurrentValues.SetValues((_Employee));

                            //_context.Alert.Update(_Alertq);
                            //await _context.SaveChangesAsync();
                            BitacoraWrite _writeejec = new BitacoraWrite(_context, new Bitacora
                            {
                                IdOperacion = _EmployeeSalary.EmployeeSalaryId,
                                DocType = "Employeed",
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
                        }
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
        [HttpGet("[action]/{IdEmployees}")]
        public async Task<IActionResult> GetEmployeeSalaryByIdEmployees(Int64 IdEmployees)
        {
            List<EmployeeSalary> Items = new List<EmployeeSalary>();
            try
            {
                Items = await _context.EmployeeSalary.Where(q => q.IdEmpleado == IdEmployees).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return Ok(Items);
        }
    }
}