﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeesController(ILogger<EmployeesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de Employees paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Employees> Items = new List<Employees>();
            try
            {
                var query = _context.Employees.AsQueryable();
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
        /// Obtiene el Listado de Employeeses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employees> Items = new List<Employees>();
            try
            {
                Items = await _context.Employees.Include(c=> c.Puesto).ToListAsync();
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
        /// Obtiene los Datos de la Employees por medio del Id enviado.
        /// </summary>
        /// <param name="IdEmpleado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdEmpleado}")]
        public async Task<IActionResult> GetEmployeesById(Int64 IdEmpleado)
        {
            Employees Items = new Employees();
            try
            {
                Items = await _context.Employees.Include(c => c.Bank)
                                                .Include(c => c.Branch)
                                                .Include(c => c.City)
                                                .Include(c => c.Country)
                                                .Include(c => c.Departamento)
                                                .Include(c => c.ApplicationUser)
                                                .Where(q => q.IdEmpleado == IdEmpleado).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la Employees por medio del Id enviado.
        /// </summary>
        /// <param name="IdDepto"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdDepto}")]
        public async Task<IActionResult> GetEmployeesByDepartament(Int64 IdDepto)
        {
            List<Employees> Items = new List<Employees>();
            try
            {
                Items = await _context.Employees.Include(c => c.Bank)
                                                .Include(c => c.Branch)
                                                .Include(c => c.City)
                                                .Include(c => c.Country)
                                                .Include(c => c.Departamento)
                                                .Include(c => c.ApplicationUser)
                                                .Where(q => q.IdDepartamento == IdDepto).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Employees>> Insert([FromBody]Employees _Employees)
        {
            Employees Employees = _Employees;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Employees.Add(Employees);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _Employees._EmployeeSalary)
                        {
                            item.IdEmpleado = _Employees.IdEmpleado;
                            _context.EmployeeSalary.Add(item);
                        }

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = Employees.IdEmpleado,
                            DocType = "Employees",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_Employees, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(Employees, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = Employees.Usuariocreacion,
                            UsuarioModificacion = Employees.Usuariomodificacion,
                            UsuarioEjecucion = Employees.Usuariomodificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                // this.UpdateSalesOrder(salesOrder.SalesOrderId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Employees));
        }

        /// <summary>
        /// Actualiza la Employees
        /// </summary>
        /// <param name="_Employees"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Employees>> Update([FromBody]Employees _Employees)
        {
            Employees _Employeesq = _Employees;
            try
            {
                _Employeesq = await (from c in _context.Employees
                                 .Where(q => q.IdEmpleado == _Employees.IdEmpleado)
                                     select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Employeesq).CurrentValues.SetValues((_Employees));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.Employees.Update(_Employeesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Employeesq));
        }

        /// <summary>
        /// Elimina una Employees       
        /// </summary>
        /// <param name="_Employees"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Employees _Employees)
        {
            Employees _Employeesq = new Employees();
            try
            {
                _Employeesq = _context.Employees
                .Where(x => x.IdEmpleado == (Int64)_Employees.IdEmpleado)
                .FirstOrDefault();

                _context.Employees.Remove(_Employeesq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Employeesq));

        }

        /// <summary>
        /// Valida si ya exíste el numero de Identidad.       
        /// </summary>
        /// <param name="Identidad"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Identidad}/{Id}")]
        public async Task<IActionResult> ValidacionIdentidad(string Identidad, Int64 Id)
        {
            Employees Items = new Employees();
            try
            {
                if (Id == 0)
                {
                    Items = await _context.Employees.Where(q => q.Identidad == Identidad).FirstOrDefaultAsync();
                }
                else
                {
                    Items = await _context.Employees.Where(q => q.Identidad == Identidad && q.IdEmpleado != Id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));

        }

        /// <summary>
        /// Valida si ya exíste el numero de RTN.      
        /// </summary>
        /// <param name="RTN"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RTN}/{Id}")]
        public async Task<IActionResult> ValidacionRTN(string RTN, Int64 Id)
        {
            Employees Items = new Employees();
            try
            {
                if (Id == 0)
                {
                    Items = await _context.Employees.Where(q => q.RTN == RTN).FirstOrDefaultAsync();
                }
                else
                {
                    Items = await _context.Employees.Where(q => q.RTN == RTN && q.IdEmpleado != Id).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));

        }
    }
}