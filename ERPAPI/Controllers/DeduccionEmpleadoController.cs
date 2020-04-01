using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private async Task<decimal[]> GetSalariosPeriodo(long empleadoId, int periodo)
        {
            var salarios = await _context.EmployeeSalary.Where(s => s.IdEmpleado == empleadoId && s.DayApplication.Year <= periodo)
                .OrderByDescending(f => f.DayApplication).ToListAsync();
            decimal[] salProyectados = new decimal[12];
            if (salarios.Count() == 1)
            {
                for (int i = 0; i < 12; i++)
                {
                    salProyectados[i]= salarios[0].QtySalary ?? 0;
                }
            }
            else
            {
                if (salarios[0].DayApplication.Year < periodo)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        salProyectados[i] = salarios[0].QtySalary ?? 0;
                    }
                }
                else
                {
                    var ultimoSalario = salarios[0];
                    for (int i = ultimoSalario.DayApplication.Month - 1; i < 12; i++)
                    {
                        salProyectados[i] = ultimoSalario.QtySalary ?? 0;
                    }

                    int faltaLlenar = ultimoSalario.DayApplication.Month - 1;
                    int siguienteSalario = 1;
                    while (siguienteSalario < salarios.Count)
                    {
                        ultimoSalario = salarios[siguienteSalario];
                        if (ultimoSalario.DayApplication.Year < periodo)
                        {
                            for (int i = 0; i < faltaLlenar; i++)
                            {
                                salProyectados[i] = ultimoSalario.QtySalary ?? 0;
                            }
                            faltaLlenar = 0;
                            break;
                        }
                        for (int i = ultimoSalario.DayApplication.Month-1; i < faltaLlenar; i++)
                        {
                            salProyectados[i] = ultimoSalario.QtySalary ?? 0;
                        }
                        faltaLlenar = faltaLlenar - ultimoSalario.DayApplication.Month;
                        if(faltaLlenar==0)
                            break;
                        siguienteSalario++;
                    }

                    if (faltaLlenar > 0)
                    {
                        for (int i = 0; i < faltaLlenar; i++)
                        {
                            salProyectados[i] = ultimoSalario.QtySalary ?? 0;
                        }
                    }
                }
            }

            return salProyectados;
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

        private async Task<decimal[]> CalcularISR(long empleadoId, int periodo, int mes)
        {
            Employees empleado = _context.Employees.FirstOrDefault(e => e.IdEmpleado == empleadoId);
            if (empleado == null)
            {
                throw new Exception("El empleado no existe");
            }

            if (empleado.FechaIngreso == null)
            {
                throw new Exception($"La fecha de ingreso del empleado {empleado.NombreEmpleado} no es valida.");
            }

            if (empleado.FechaNacimiento == null)
            {
                throw new Exception($"El empleado {empleado.NombreEmpleado} no tiene fecha de nacimiento en su registro.");
            }

            var salMinimo = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 160);
            decimal salarioMinimo = (decimal)(salMinimo.Valordecimal ?? 0);
            decimal[] salarios;
            try
            {
                 salarios = await GetSalariosPeriodo(empleadoId, periodo);
            }
            catch (Exception)
            {

                throw new Exception($"El empleado {empleado.NombreEmpleado} no salario");
            }
            
            decimal valor13vo = 0;
            decimal valor14vo = 0;

            if (empleado.FechaIngreso.Value.Year == periodo)
            {
                for (int i = 0 ; i < empleado.FechaIngreso.Value.Month-1; i++)
                {
                    salarios[i] = 0;
                }

                

            }
            valor14vo = (salarios[0] + salarios[1] + salarios[2] + salarios[3] + salarios[4] + salarios[5]) / (decimal)6.00;
            valor13vo = (salarios[6] + salarios[7] + salarios[8] + salarios[9] + salarios[10] + salarios[11]) / (decimal)6.00;

            decimal limiteExceso = salarioMinimo * 10;
            decimal exceso13vo = (valor13vo > limiteExceso) ? valor13vo - limiteExceso : 0;
            decimal exceso14vo = (valor14vo > limiteExceso) ? valor14vo - limiteExceso : 0;

            decimal bonificaciones = (decimal)(await _context.Bonificaciones
                .Where(b => b.FechaBono.Year == periodo && b.EstadoId == 90).SumAsync(r => r.Monto));
            var horasExtras = await _context.HorasExtrasBiometrico.Include(r => r.Encabezado).Where(h => h.Encabezado.Fecha.Year == periodo && h.IdEstado == 71).ToListAsync();
            decimal valorHorasExtra = 0;
            foreach (var horaExtra in horasExtras)
            {
                decimal salario = await GetSalarioFecha(empleadoId, horaExtra.Encabezado.Fecha);
                decimal salarioHora = salario / 30 / 8;
                valorHorasExtra += salarioHora * ((decimal)horaExtra.Horas + (decimal)horaExtra.Minutos / 60);
            }

            decimal colegiacionAnual = (decimal)(await _context.DeduccionesEmpleados
                                           .Where(d => d.Deduccion.DeductionTypeId == 3 && d.EstadoId == 1)
                                           .SumAsync(d => d.Monto)) * 12;
            decimal afpAnual = (decimal)(await _context.DeduccionesEmpleados
                                   .Where(d => d.Deduccion.DeductionTypeId == 4 && d.EstadoId == 1)
                                   .SumAsync(d => d.Monto)) * 12;

            int diaFin = 31;
            if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                diaFin = 30;
            }else if (mes == 2 && periodo % 4 == 0)
            {
                diaFin = 29;
            }else if (mes == 2)
            {
                diaFin = 28;
            }

            int edad = new DateTime(periodo,mes,diaFin).Subtract(empleado.FechaNacimiento.Value).Days / 365;
            decimal gastosMedicos = edad < 60 ? 40000 : 70000;

            decimal totalIngresosGravables = (salarios.Sum()) + exceso13vo + exceso14vo + bonificaciones + valorHorasExtra;

            decimal totalDeduccionAnual = gastosMedicos + colegiacionAnual + afpAnual;

            decimal rentaNetaGravable = totalIngresosGravables - totalDeduccionAnual;

            var tarifas = await _context.ISRConfiguracion.OrderByDescending(t => t.Porcentaje).ToListAsync();

            decimal montoTarifar = rentaNetaGravable;

            decimal totalTarifar = 0;
            foreach (var tarifa in tarifas)
            {
                if (montoTarifar >= (decimal)tarifa.De)
                {
                    totalTarifar += (montoTarifar - (decimal)(tarifa.De - 0.01)) * (decimal)(tarifa.Porcentaje / 100.0);
                    montoTarifar -= (montoTarifar - (decimal)(tarifa.De - 0.01));
                }
            }

            var regPagado = await _context.PagosISR.FirstOrDefaultAsync(p =>
                p.EmpleadoId == empleadoId && p.Periodo == periodo);
            decimal totalPagado = (decimal)(regPagado?.PagoAcumulado ?? 0);

            decimal netoTarifar = totalTarifar - totalPagado;

            var confISR = await _context.ElementoConfiguracion.FirstOrDefaultAsync(r => r.Id == 123);

            int cuotasISR = (int)(confISR?.Valordecimal ?? 12);

            decimal isr = 0;

            if (mes < cuotasISR)
            {
                cuotasISR -= mes;
                isr = netoTarifar / cuotasISR;
            }

            return new []{isr,totalTarifar};
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetISREmpleado(long empleadoId)
        {
            try
            {
                decimal isr = (await CalcularISR(empleadoId, DateTime.Today.Year, DateTime.Today.Month))[0];
                return Ok(isr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el ISR");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetRAPEmpleado(long empleadoId)
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

                var elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 124);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo del RAP");
                }

                decimal techoRap = (decimal)(elmConf.Valordecimal??0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 125);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportación del RAP para el empleado.");
                }

                decimal porcentajeAportacion = ((decimal)(elmConf.Valordecimal??0)/100);

                if (empleado.Salario < techoRap)
                    return Ok((decimal)0);

                decimal aporteRAP = (empleado.Salario??0) * porcentajeAportacion;

                return Ok(aporteRAP);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el aporte del RAP");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetRAPPatrono(long empleadoId)
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

                var elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 124);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo del RAP");
                }

                decimal techoRap = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 126);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportación del RAP para el patrono.");
                }

                decimal porcentajeAportacion = ((decimal)(elmConf.Valordecimal ?? 0) / 100);

                if (empleado.Salario < techoRap)
                    return Ok((decimal)0);

                decimal aporteRAP = (empleado.Salario ?? 0) * porcentajeAportacion;

                return Ok(aporteRAP);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el aporte patronal del RAP");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetRAPCesantia(long empleadoId)
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

                decimal salarioNominal = await GetSalarioNominal(empleadoId);

                var elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 124);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo del RAP");
                }

                decimal techoRap = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 127);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el factor de multiplicación para cesantias");
                }

                decimal factor = (decimal) (elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 161);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportación del RAP para cesantias.");
                }

                decimal porcentajeAportacion = ((decimal)(elmConf.Valordecimal ?? 0) / 100);

                decimal aporteRAPCesantia = salarioNominal * porcentajeAportacion;

                if (aporteRAPCesantia > (techoRap * factor))
                {
                    aporteRAPCesantia = techoRap * factor;
                }

                return Ok(aporteRAPCesantia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el aporte por cesantia del RAP");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetIHSSEmpleado(long empleadoId)
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

                if (empleado.Salario == null)
                {
                    throw new Exception("El empleado no tiene un salario asignado.");
                }

                var elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 128);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo del IVM del IHSS");
                }

                decimal techoIVM = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 131);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo de Salud del IHSS");
                }

                decimal techoSalud = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 129);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportacion del IVM del empleado");
                }

                decimal porcentajeIVM = (decimal)(elmConf.Valordecimal ?? 0)/100;

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 132);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportacion de Salud del empleado");
                }

                decimal porcentajeSalud = (decimal)(elmConf.Valordecimal ?? 0)/100;

                decimal aportIVM = 0;
                decimal aportSalud = 0;
                decimal salario = empleado.Salario.Value;

                if (salario > techoIVM)
                {
                    aportIVM = techoIVM * porcentajeIVM;
                }
                else
                {
                    aportIVM = salario * porcentajeIVM;
                }

                if (salario > techoSalud)
                {
                    aportSalud = techoSalud * porcentajeSalud;
                }
                else
                {
                    aportSalud = salario * porcentajeSalud;
                }

                decimal aporteIHSS = aportIVM + aportSalud;

                return Ok(aporteIHSS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular deducción del IHSS");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}")]
        public async Task<ActionResult> GetIHSSPatrono(long empleadoId)
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

                if (empleado.Salario == null)
                {
                    throw new Exception("El empleado no tiene un salario asignado.");
                }

                var elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 128);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo del IVM del IHSS");
                }

                decimal techoIVM = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 131);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el techo del cálculo de Salud del IHSS");
                }

                decimal techoSalud = (decimal)(elmConf.Valordecimal ?? 0);

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 130);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportacion del IVM del patrono");
                }

                decimal porcentajeIVM = (decimal)(elmConf.Valordecimal ?? 0) / 100;

                elmConf = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == 133);
                if (elmConf == null)
                {
                    throw new Exception("No esta configurado el porcentaje de aportacion de Salud del patrono");
                }

                decimal porcentajeSalud = (decimal)(elmConf.Valordecimal ?? 0) / 100;

                decimal aportIVM = 0;
                decimal aportSalud = 0;
                decimal salario = empleado.Salario.Value;

                if (salario > techoIVM)
                {
                    aportIVM = techoIVM * porcentajeIVM;
                }
                else
                {
                    aportIVM = salario * porcentajeIVM;
                }

                if (salario > techoSalud)
                {
                    aportSalud = techoSalud * porcentajeSalud;
                }
                else
                {
                    aportSalud = salario * porcentajeSalud;
                }

                decimal aporteIHSS = aportIVM + aportSalud;

                return Ok(aporteIHSS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular deducción del IHSS");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{empleadoId}/{periodo}")]
        public async Task<ActionResult> GetImpuestoVecinal(long empleadoId, int periodo)
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

                if (empleado.Salario == null)
                {
                    throw new Exception("El empleado no tiene un salario asignado.");
                }

                var ingresos =
                    await _context.IngresosAnuales.FirstOrDefaultAsync(
                        i => i.EmpleadoId == empleadoId && i.Periodo == (periodo-1));

                if (ingresos == null)
                {
                    throw new Exception($"El empleado no tiene ingresos anuales para el año {(periodo - 1)}");
                }

                var factores = await _context.ImpuestoVecinalConfiguraciones.OrderBy(c => c.De).ToListAsync();

                if (factores.Count == 0)
                {
                    throw new Exception("No hay factores de impuesto vecinal, definidos en el sistema.");
                }

                decimal impuesto = 0;

                foreach (var factor in factores)
                {
                    if (ingresos.IngresoAcumulado > factor.Hasta)
                    {
                        decimal millar = Decimal.Round((factor.Hasta - factor.De)) / 1000;
                        impuesto += (millar * factor.FactorMillar);
                    }
                    else
                    {
                        decimal millar = (ingresos.IngresoAcumulado - factor.De) / 1000;
                        impuesto += (millar * factor.FactorMillar);
                        break;
                    }
                }

                return Ok(impuesto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el impuesto vecinal");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{periodo}/{mes}")]
        public async Task<ActionResult> CalcularISRGeneral(int periodo, int mes)
        {
            try
            {
                using (var transaccion = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var usuario = await  _context.Users.Where(w => w.UserName == User.Identity.Name.ToString()).FirstOrDefaultAsync();
                        int diaFin = 31;
                        if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
                        {
                            diaFin = 30;
                        }
                        else if (mes == 2 && periodo % 4 == 0)
                        {
                            diaFin = 29;
                        }
                        else if (mes == 2)
                        {
                            diaFin = 28;
                        }

                        var empleados = await _context.Employees.ToListAsync();
                        var estadoActivo = await _context.Estados.FirstOrDefaultAsync(e => e.IdEstado == 1);
                        foreach (var empleado in empleados)
                        {
                            var datosISR = await CalcularISR(empleado.IdEmpleado, periodo, mes);

                            var deduccion = await _context.DeduccionesEmpleados
                                .Where(r => r.DeductionId == 1 && r.EmpleadoId == empleado.IdEmpleado 
                                            && r.VigenciaInicio >= new DateTime(periodo, mes, 1)
                                            && r.VigenciaFinaliza <= new DateTime(mes == 12 ? (periodo + 1) : periodo,
                                                mes == 12 ? 1 : (mes + 1), 1)).FirstOrDefaultAsync();

                            if (deduccion == null)
                            {
                                deduccion = new DeduccionEmpleado()
                                            {
                                                CantidadCuotas = 12,
                                                DeductionId = 1,
                                                EmpleadoId = empleado.IdEmpleado,
                                                EstadoId = 1,
                                                Monto = (float) datosISR[0],
                                                FechaCreacion = DateTime.Today,
                                                FechaModificacion = DateTime.Today,
                                                UsuarioCreacion = usuario.UserName,
                                                UsuarioModificacion = usuario.UserName,
                                                VigenciaInicio = new DateTime(periodo, mes, 1),
                                                VigenciaFinaliza = new DateTime(periodo, mes, diaFin)
                                            };
                                await _context.DeduccionesEmpleados.AddAsync(deduccion);
                            }
                            else
                            {
                                deduccion.Monto = (float) datosISR[0];
                                deduccion.UsuarioModificacion = usuario.UserName;
                                deduccion.FechaModificacion = DateTime.Today;
                            }

                            var pagoISR =
                                await _context.PagosISR.FirstOrDefaultAsync(
                                    p => p.Periodo == periodo && p.EmpleadoId == empleado.IdEmpleado && p.EstadoId==1);
                            if (pagoISR == null)
                            {
                                pagoISR = new PagoISR()
                                          {
                                              EmpleadoId = empleado.IdEmpleado,
                                              EstadoId = 1,
                                              Periodo = periodo,
                                              PagoAcumulado = 0,
                                              TotalAnual = (double) datosISR[1],
                                              Saldo = (double) datosISR[1],
                                              UsuarioCreacion = usuario.UserName,
                                              UsuarioModificacion = usuario.UserName,
                                              FechaCreacion = DateTime.Today,
                                              FechaModificacion = DateTime.Today
                                          };
                                await _context.PagosISR.AddAsync(pagoISR);
                                
                            }
                            else
                            {
                                
                                pagoISR.TotalAnual = (double) datosISR[1];
                                if (pagoISR.TotalAnual < pagoISR.PagoAcumulado)
                                {
                                    pagoISR.TotalAnual = pagoISR.PagoAcumulado;
                                }

                                pagoISR.Saldo = pagoISR.TotalAnual - pagoISR.PagoAcumulado;
                                pagoISR.UsuarioModificacion = usuario.UserName;
                                
                            }
                            await _context.SaveChangesAsync();
                        }
                        transaccion.Commit();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular el ISR a nivel general para el periodo " + periodo);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{periodo}/{mes}")]
        public async Task<ActionResult> GetPagosISRPeriodo(int periodo, int mes)
        {
            try
            {
                int diaFin = 31;
                if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
                {
                    diaFin = 30;
                }
                else if (mes == 2 && periodo % 4 == 0)
                {
                    diaFin = 29;
                }
                else if (mes == 2)
                {
                    diaFin = 28;
                }

                DateTime fechaInicio = new DateTime(periodo, mes, 1);
                DateTime fechaFinal = new DateTime(periodo, mes, 1).AddMonths(1)
                    .Subtract(new TimeSpan(0, 0, 0, 0, 1));

                var qry = await _context.PagosISR
                    .SelectMany(
                        x => _context.DeduccionesEmpleados.Where(y=> y.EmpleadoId == x.EmpleadoId 
                                                                     && y.VigenciaInicio >= fechaInicio
                                                                     && y.VigenciaFinaliza <= fechaFinal
                                                                     && y.DeductionId == 1).DefaultIfEmpty(),
                        (pagos, y) => new PagosISRDTO()
                                  {
                                      Id = pagos.Id,
                                      EmpleadoId = pagos.EmpleadoId,
                                      FechaModificacion = pagos.FechaModificacion,
                                      UsuarioModificacion = pagos.UsuarioModificacion,
                                      UsuarioCreacion = pagos.UsuarioCreacion,
                                      FechaCreacion = pagos.FechaCreacion,
                                      Periodo = pagos.Periodo,
                                      EstadoId = pagos.EstadoId,
                                      TotalAnual = pagos.TotalAnual,
                                      Estado = pagos.Estado,
                                      Empleado = pagos.Empleado,
                                      Saldo = pagos.Saldo,
                                      PagoAcumulado = pagos.PagoAcumulado,
                                      CuotaISR = Convert.ToDecimal(y == null ? 0 : y.Monto)
                                  }
                    ).ToListAsync();

                return Ok(qry);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Error al cargar los pagos del ISR, para el periodo {periodo} y mes {mes}");
                return BadRequest(ex.Message);
            }
        }
    }

}