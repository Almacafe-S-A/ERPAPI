﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DeduccionEmpleadoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DeduccionEmpleadoController(ILogger<DeduccionEmpleadoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DeduccionEmpleado>> Guardar([FromBody] DeduccionEmpleado deduccion)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (deduccion.Id != 0)
                        {
                            DeduccionEmpleado deduccionExistente =
                                await _context.DeduccionesEmpleados.FirstOrDefaultAsync(d => d.Id == deduccion.Id);
                            if (deduccionExistente == null)
                            {
                                throw new Exception("No existe la deducción del empleado a modificar");
                            }

                            _context.Entry(deduccionExistente).CurrentValues.SetValues(deduccion);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                               {
                                   IdOperacion = deduccionExistente.Id,
                                   DocType = "DeduccionEmpleado",
                                   ClaseInicial =
                                       Newtonsoft.Json.JsonConvert.SerializeObject(deduccionExistente, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                   Accion = "Update",
                                   FechaCreacion = DateTime.Now,
                                   FechaModificacion = DateTime.Now,
                                   UsuarioCreacion = deduccionExistente.UsuarioCreacion,
                                   UsuarioModificacion = deduccionExistente.UsuarioModificacion,
                                   UsuarioEjecucion = deduccionExistente.UsuarioModificacion,

                               });

                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(deduccionExistente);
                        }
                        else
                        {
                            _context.DeduccionesEmpleados.Add(deduccion);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                                                                               {
                                                                                   IdOperacion = deduccion.Id,
                                                                                   DocType = "DeduccionEmpleado",
                                                                                   ClaseInicial =
                                                                                       Newtonsoft.Json.JsonConvert.SerializeObject(deduccion, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                                                                   Accion = "Insert",
                                                                                   FechaCreacion = DateTime.Now,
                                                                                   FechaModificacion = DateTime.Now,
                                                                                   UsuarioCreacion = deduccion.UsuarioCreacion,
                                                                                   UsuarioModificacion = deduccion.UsuarioModificacion,
                                                                                   UsuarioEjecucion = deduccion.UsuarioModificacion,

                                                                               });
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return Ok(deduccion);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Error Guardar Deducción Empleado");
                        return BadRequest(ex);
                    }
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult<List<DeduccionEmpleado>>> GetDeduccionesPorEmpleado(long empleadoId)
        {
            try
            {
                List<DeduccionEmpleado> deducciones =
                    await _context.DeduccionesEmpleados.Include(s=> s.Deduccion).Where(d => d.EmpleadoId == empleadoId).ToListAsync();
                return Ok(deducciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<DeduccionesEmpleadoDTO>>> GetDeduccionesEmpleados()
        {
            try
            {
                var detalleEmpleado = from emp in _context.Employees
                    join ded in _context.DeduccionesEmpleados on emp.IdEmpleado equals ded.EmpleadoId into dedEmp
                    from subDed in dedEmp.DefaultIfEmpty()
                    group new {emp, subDed} by emp.IdEmpleado
                    into g
                    select new DeduccionesEmpleadoDTO()
                           {
                               EmpleadoId = g.Key,
                               NombreEmpleado = g.FirstOrDefault().emp.NombreEmpleado,
                               CantidadDeducciones = g.Count(s=> s.subDed != null),
                               TotalDeducciones = g.Sum(s => s.subDed == null ? 0 : s.subDed.Monto ),
                               SalarioEmpleado = (double) (g.First().emp.Salario.HasValue ? g.First().emp.Salario.Value : 0)
                           };
                List<DeduccionesEmpleadoDTO> deducciones = await detalleEmpleado.ToListAsync();
                return Ok(deducciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Guardar Deducción Empleado");
                return BadRequest(ex);
            }
        }

        private decimal GetSalarioNominal(long empleadoId)
        {
            //Calculo del salario nominal, basado en los ultimos 6 meses de salario
            var salarios =  _context.EmployeeSalary.Where(s => s.IdEmpleado == empleadoId)
                .OrderByDescending(f => f.DayApplication).ToList();
            decimal salarioNominal = 0;

            if (salarios.Count() == 1)
            {
                salarioNominal = salarios[0].QtySalary ?? 0;
            }
            else
            {
                List<decimal> salariosHistoricos = new List<decimal>();
                for (int i = 0; i < salarios.Count - 1; i++)
                {
                    if (i == salarios.Count - 1)
                    {
                        EmployeeSalary salarioReciente = salarios[i];
                        salariosHistoricos.Add(salarioReciente.QtySalary ?? 0);
                    }
                    else
                    {
                        EmployeeSalary salarioReciente = salarios[i];
                        EmployeeSalary salarioAnterior = salarios[i + 1];
                        if (salarioReciente.DayApplication.Year == salarioAnterior.DayApplication.Year &&
                            salarioReciente.DayApplication.Month == salarioAnterior.DayApplication.Month)
                        {
                            salariosHistoricos.Add(salarioReciente.QtySalary ?? 0);
                        }
                        else
                        {
                            int mes = salarioReciente.DayApplication.Month;
                            int anio = salarioReciente.DayApplication.Year;
                            while (!(salarioAnterior.DayApplication.Year == anio &&
                                     salarioAnterior.DayApplication.Month == mes))
                            {
                                salariosHistoricos.Add(salarioReciente.QtySalary ?? 0);
                                if (mes == 1)
                                {
                                    mes = 12;
                                    anio--;
                                }
                                else
                                {
                                    mes--;
                                }
                            }
                        }
                    }

                    if (salariosHistoricos.Count() >= 6)
                    {
                        break;
                    }
                }

                salarioNominal = salariosHistoricos.Count() <= 6 ? salariosHistoricos.Average() : salariosHistoricos.GetRange(0, 6).Average();
            }

            return salarioNominal;
        }

        //public async Task<ActionResult> GetISREmpleado(long empleadoId)
        //{
        //    try
        //    {
        //        Employees empleado = _context.Employees.FirstOrDefault(e => e.IdEmpleado == empleadoId);
        //        if (empleado == null)
        //        {
        //            return Ok(0);
        //        }
        //        decimal salarioNominal = GetSalarioNominal(empleadoId);

        //        if (empleado.FechaIngreso == null)
        //        {
        //            return Ok(0);
        //        }

        //        if (empleado.FechaIngreso.Value.Year == DateTime.Today.Year)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error Guardar Deducción Empleado");
        //        return BadRequest(ex);
        //    }
        //}
    }

    
}