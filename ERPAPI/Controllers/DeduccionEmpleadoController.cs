using System;
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

        private async Task<decimal> GetSalarioNominal(long empleadoId)
        {
            //Calculo del salario nominal, basado en los ultimos 6 meses de salario
            var salarios = await _context.EmployeeSalary.Where(s => s.IdEmpleado == empleadoId)
                .OrderByDescending(f => f.DayApplication).ToListAsync();
            decimal salarioNominal = 0;

            if (salarios.Count() == 1)
            {
                salarioNominal = salarios[0].QtySalary ?? 0;
            }
            else
            {
                List<decimal> salariosHistoricos = new List<decimal>();

                var today = DateTime.Today;

                if (salarios[0].DayApplication < today)
                {
                    var ultimoSalario = salarios[0];
                    //Recorrer los meses hasta llegar al mes de aplicacion del ultimo salario
                    int mes = today.Month;
                    int anio = today.Year;
                    while (!(ultimoSalario.DayApplication.Year == anio && ultimoSalario.DayApplication.Month == mes))
                    {
                        salariosHistoricos.Add(ultimoSalario.QtySalary ?? 0);
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

        private async Task<decimal> GetSalarioFecha(long empleadoId, DateTime fecha)
        {
            var salarios = _context.EmployeeSalary.Where(s => s.IdEmpleado == empleadoId)
                .OrderByDescending(f => f.DayApplication).ToList();

            decimal salario = 0;
            foreach (var salarioHistorico in salarios)
            {
                if (salarioHistorico.DayApplication.Date <= fecha)
                {
                    salario = (decimal) (salarioHistorico.QtySalary ?? 0);
                    break;
                }
            }

            return salario;
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetISREmpleado(long empleadoId)
        {
            try
            {
                Employees empleado = _context.Employees.FirstOrDefault(e => e.IdEmpleado == empleadoId);
                if (empleado == null)
                {
                    throw new Exception("El empleado no existe");
                }

                if (empleado.FechaIngreso == null)
                {
                    throw new Exception("La fecha de ingreso del empleado no es valida.");
                }

                if (empleado.FechaNacimiento == null)
                {
                    throw new Exception("El empleado no tiene fecha de nacimiento en su registro.");
                }

                var salMinimo = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 160);
                decimal salarioMinimo = (decimal) (salMinimo.Valordecimal ?? 0);
                decimal salarioNominal = await GetSalarioNominal(empleadoId);
                decimal valor13vo = 0;
                decimal valor14vo = 0;
                
                if (empleado.FechaIngreso.Value.Year == DateTime.Today.Year)
                {
                    //Determinar los valores proporcionales del 13vo y 14vo
                    if (empleado.FechaIngreso.Value.Month < 6)
                    {
                        valor13vo = salarioNominal;
                        valor14vo = salarioNominal * (decimal)((6.00 - empleado.FechaIngreso.Value.Month) / 6.00);
                    }
                    else
                    {
                        valor14vo = 0;
                        valor13vo = salarioNominal * (decimal)((12.00-empleado.FechaIngreso.Value.Month) / 6.00);
                    }
                }
                else
                {
                    valor13vo = salarioNominal;
                    valor14vo = salarioNominal;
                }

                decimal limiteExceso = salarioMinimo * 10;
                decimal exceso13vo = (valor13vo > limiteExceso) ? valor13vo - limiteExceso : 0;
                decimal exceso14vo = (valor14vo > limiteExceso) ? valor14vo - limiteExceso : 0;

                decimal bonificaciones =  (decimal)(await _context.Bonificaciones
                    .Where(b => b.FechaBono.Year == DateTime.Today.Year && b.EstadoId == 90).SumAsync(r=> r.Monto));
                var horasExtras = await _context.HorasExtrasBiometrico.Include(r=>r.Encabezado).Where(h => h.Encabezado.Fecha.Year == DateTime.Today.Year && h.IdEstado == 71).ToListAsync();
                decimal valorHorasExtra = 0;
                foreach (var horaExtra in horasExtras)
                {
                    decimal salario = await GetSalarioFecha(empleadoId, horaExtra.Encabezado.Fecha);
                    decimal salarioHora = salario / 30 / 8;
                    valorHorasExtra += salarioHora * ((decimal) horaExtra.Horas + (decimal) horaExtra.Minutos / 60);
                }

                decimal colegiacionAnual = (decimal) (await _context.DeduccionesEmpleados
                                               .Where(d => d.Deduccion.DeductionTypeId == 3 && d.EstadoId == 1)
                                               .SumAsync(d=>d.Monto)) * 12;
                decimal afpAnual = (decimal)(await _context.DeduccionesEmpleados
                                       .Where(d => d.Deduccion.DeductionTypeId == 4 && d.EstadoId == 1)
                                       .SumAsync(d => d.Monto)) * 12;

                int edad = DateTime.Today.Subtract(empleado.FechaNacimiento.Value).Days / 365;
                decimal gastosMedicos = edad < 60 ? 40000 : 70000;

                decimal totalIngresosGravables =
                    (salarioNominal * 12) + exceso13vo + exceso14vo + bonificaciones + valorHorasExtra;

                decimal totalDeduccionAnual = gastosMedicos + colegiacionAnual + afpAnual;

                decimal rentaNetaGravable = totalIngresosGravables - totalDeduccionAnual;

                var tarifas = await _context.ISRConfiguracion.OrderByDescending(t=>t.Porcentaje).ToListAsync();

                decimal montoTarifar = rentaNetaGravable;

                decimal totalTarifar = 0;
                foreach (var tarifa in tarifas)
                {
                    if (montoTarifar >= (decimal)tarifa.De)
                    {
                        totalTarifar += (montoTarifar - (decimal)(tarifa.De - 0.01)) * (decimal)(tarifa.Porcentaje / 100.0);
                        montoTarifar -= (montoTarifar - (decimal) (tarifa.De - 0.01));
                    }
                }

                var regPagado = await _context.PagosISR.FirstOrDefaultAsync(p =>
                    p.EmpleadoId == empleadoId && p.Periodo == DateTime.Today.Year);
                decimal totalPagado = (decimal) (regPagado?.PagoAcumulado ?? 0);
                
                decimal netoTarifar = totalTarifar - totalPagado;

                var confISR = await _context.ElementoConfiguracion.FirstOrDefaultAsync(r => r.Id == 123);

                int cuotasISR = (int) (confISR?.Valordecimal ?? 10);

                decimal isr = 0;

                if (DateTime.Today.Month < cuotasISR)
                {
                    cuotasISR -= DateTime.Today.Month;
                    isr = netoTarifar / cuotasISR;
                }

                return Ok(isr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el ISR");
                return BadRequest(ex.Message);
            }
        }
    }

    
}