﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Helpers;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BiometricoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public BiometricoController(ApplicationDbContext context, ILogger<BiometricoController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Biometrico biometrico)
        {
            try
            {
                var biometricofecha = this.context.Biometricos.Where(q => q.Fecha.Date == biometrico.Fecha.Date).FirstOrDefault();
                if (biometricofecha != null)
                {
                    throw new Exception("Ya existe una carga para esta fecha");
                }



                if (biometrico.Detalle == null)
                {
                    throw new Exception("Archivo Biometrico sin Detalle");
                }


                foreach (var det in biometrico.Detalle)
                {
                    if (det.IdEmpleado == 0)
                    {
                        var relacion =
                           await context.EmpleadosBiometrico.FirstOrDefaultAsync(r => r.BiometricoId == det.IdBiometrico);
                        if (relacion == null)
                        {
                            throw new Exception("Empleado no existe en asignado a un codigo de biometrico");
                        }
                        det.IdEmpleado = relacion.EmpleadoId;

                        //SI ENCUENTRA UNA INASISTENCIA EN EL PARA EL EMPLEADO PARA LA FECHA QUE SE CARGO EL BIOMETRICO, CAMBIA LA INASISTENCIA A ANULADA
                        var inasistencia = await
                            context.Inasistencias.FirstOrDefaultAsync(
                                i => i.Fecha.Equals(biometrico.Fecha) && i.IdEmpleado == det.IdEmpleado);

                        if (inasistencia != null)
                        {
                            inasistencia.IdEstado = 81; //81 = ESTADO ANULADO
                        }
                    }
                }

                if (biometrico.Id == 0)
                {
                    context.Biometricos.Add(biometrico);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();
                    return Ok(biometrico);
                }
                Biometrico registroExistente = await context.Biometricos.Include(d => d.Detalle)
                    .FirstOrDefaultAsync(r => r.Id == biometrico.Id);
                context.Entry(registroExistente).CurrentValues.SetValues(biometrico);
                await context.SaveChangesAsync();
                return Ok(biometrico);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar los registros del biometrico");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBiometricos()
        {
            try
            {
                var registros = await context.Biometricos
                    //.Include(d => d.Detalle)
                    .Include(e => e.Estado).ToListAsync();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar los registros del biometrico");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{IdBiometrico}")]
        public async Task<ActionResult> GetDetalleBiometrico(long IdBiometrico)
        {
            try
            {
                var registros = await context.DetallesBiometricos.Include(e => e.Empleado).Where(b => b.Encabezado.Id == IdBiometrico).OrderBy(b => b.IdEmpleado).ThenBy(b => b.Tipo).ToListAsync();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar los registros del biometrico");
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]/{IdBiometrico}")]
        public async Task<ActionResult> AprobarBiometrico(long IdBiometrico)
        {
            try
            {
                var biometrico = await context.Biometricos.Include(d => d.Detalle).ThenInclude(e => e.Empleado)
                    .FirstOrDefaultAsync(b => b.Id == IdBiometrico);

                if (biometrico.IdEstado != 60)
                {
                    throw new Exception("Solo se puede aprobar un archivo biometrico en estado de CARGADO");
                }

                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var confGraciaEntrada = await context.ElementoConfiguracion.FirstOrDefaultAsync(p => p.Id == 140);
                        int periodoGraciaEntrada = (int)(confGraciaEntrada.Valordecimal ?? 0);
                        
                        var confGraciaSalida = await context.ElementoConfiguracion.FirstOrDefaultAsync(p => p.Id == 141);
                        int periodoGraciaSalida = (int)(confGraciaSalida.Valordecimal ?? 0);

                        List<DetalleBiometrico> detalleBiometricoaevaluar = biometrico.Detalle.Where(q => !q.SalidaPendiente).ToList();

                        DateTime diaaterior = biometrico.Fecha.AddDays(-1);

                        List<DetalleBiometrico> detalleBiometricoSalidaspendiente = context.
                            DetallesBiometricos.Where(q => q.SalidaPendiente && (q.FechaHora.Date == diaaterior.Date)).ToList();

                        detalleBiometricoaevaluar.AddRange(detalleBiometricoSalidaspendiente);

                        

                        foreach (var detalle in detalleBiometricoaevaluar)
                        {

                            
                            //Se omiten los empleados con tipo de planilla Funcionarios
                            Employees empleado = context.Employees.Where(q => q.IdEmpleado == detalle.IdEmpleado).FirstOrDefault();
                            if (empleado.IdTipoPlanilla == 7) continue;

                            EmpleadoHorario horarioEmpleado = await context.EmpleadoHorarios.Include(h => h.HorarioEmpleado).Where(q => q.HorarioId == detalle.IdHorario)
                                .FirstOrDefaultAsync(e => e.EmpleadoId == detalle.IdEmpleado);
                            
                            
                            if (horarioEmpleado == null)
                            {
                                horarioEmpleado = await context.EmpleadoHorarios.Include(h => h.HorarioEmpleado).FirstOrDefaultAsync(e => e.EmpleadoId == detalle.IdEmpleado);
                                if (horarioEmpleado == null)
                                {
                                    throw new Exception("Empleado sin horario asignado en el archivo del biometrico:" + detalle.Empleado.NombreEmpleado);
                                }
                            }


                            //detalle.IdHorario = horarioEmpleado.HorarioId;
                            Horario horario = horarioEmpleado.HorarioEmpleado;
                            //contextos para buscar los horarios de los empleados
                            EmpleadoHorario empleadohorario = await context.EmpleadoHorarios.FirstOrDefaultAsync(r => r.EmpleadoId == detalle.IdEmpleado);
                            var diasempleados = await context.Horarios.FirstOrDefaultAsync(r => r.Id == empleadohorario.HorarioId);
                            

                            if (detalle.Tipo.Equals("Entrada"))
                            {
                                /////Registro Control de Asitencia como Dia Laboral
                                ControlAsistencias controlAsistencias = new ControlAsistencias()
                                {
                                    Id = 0,
                                    IdEmpleado = detalle.IdEmpleado,
                                    Fecha = detalle.FechaHora,
                                    TipoAsistencia = 83,
                                    Dia = ((int)biometrico.Fecha.DayOfWeek),
                                    FechaCreacion = DateTime.Now,
                                    UsuarioCreacion = User.Identity.Name,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioModificacion = User.Identity.Name
                                };
                                context.ControlAsistencias.Add(controlAsistencias);

                                context.SaveChanges();

                                

                                //Entradas
                                DateTime horaEntradaHorario = await Util.ParseHora(horario.HoraInicio);
                                
                                DateTime horaEntradaBiometrico = DateTime.Today.AddHours(detalle.FechaHora.Hour).AddMinutes(detalle.FechaHora.Minute);
                                
                                string horaEntradaString = horaEntradaBiometrico.ToString("HH:mm:ss");


                                ///Llegadas Tarde
                                if (horaEntradaBiometrico > horaEntradaHorario)
                                {
                                    var diferencia = horaEntradaBiometrico.Subtract(horaEntradaHorario);
                                    if ((diferencia.Hours * 60 + diferencia.Minutes) >= periodoGraciaEntrada)
                                    {
                                        //Es una llegada tarde
                                        LlegadasTardeBiometrico registro = new LlegadasTardeBiometrico()
                                        {
                                            Id = 0,
                                            IdEmpleado = detalle.IdEmpleado,
                                            Horas = diferencia.Hours,
                                            Minutos = diferencia.Minutes,
                                            IdBiometrico = biometrico.Id,
                                            IdEstado = 97, //Estado 97 = Pendiente de Aprobacion,
                                            Fecha = detalle.FechaHora,
                                            Dia = ((int)biometrico.Fecha.DayOfWeek),
                                            ControlAsistenciaId = controlAsistencias.Id,
                                            HoraLlegada = horaEntradaString
                                        };
                                        controlAsistencias.TipoAsistencia = 77; // Marca como llegada tarde el control de asistencia
                                        

                                        context.LlegadasTardeBiometrico.Add(registro);
                                        await context.SaveChangesAsync();
                                    }
                                }

                                ///Horas Extras de Entradas
                                ///

                                if (detalle.Empleado.HorasExtra ?? false && horaEntradaBiometrico < horaEntradaHorario  )
                                {
                                    TimeSpan diferencia = horaEntradaHorario.Subtract(horaEntradaBiometrico);
                                    int diferenciaminutos = (diferencia.Hours * 60) + diferencia.Minutes;

                                    if (diferenciaminutos >= periodoGraciaEntrada)
                                    {
                                        string horasalida = biometrico.Detalle
                                            .Where(q => q.IdEmpleado == detalle.IdEmpleado &&
                                                        q.Tipo.Equals("Salida") &&
                                                        q.FechaHora.Date == biometrico.Fecha.Date)
                                            .FirstOrDefault()?.FechaHora.ToString("HH:mm:ss") ?? "No se encontró registro de salida";
                                        HorasExtraBiometrico registro = new HorasExtraBiometrico()
                                        {
                                            Id = 0,
                                            IdEmpleado = detalle.IdEmpleado,
                                            HoraEntrada = horaEntradaString,
                                            HoraSalida = horasalida,
                                            Horas = diferencia.Hours,
                                            Minutos = diferencia.Minutes,
                                            IdBiometrico = biometrico.Id,
                                            IdEstado = 70,
                                            Estados = "Pendiente",
                                            HoraAlumerzo = 0
                                        };

                                        context.HorasExtrasBiometrico.Add(registro);
                                        await context.SaveChangesAsync();
                                    }
                                }
                                /*    //Validacion Los empleados que no trabajan el sábado y mostrar como Descanso,
                                    if (diasempleados.Sabado == false && registroentrada.Dia == 6)
                                    {
                                        registroentrada.TipoAsistencia = 78;
                                    }
                                    //Validacion Los empleados que no trabajan el domingo y mostrar como Descanso,
                                    if (diasempleados.Domingo == false && registroentrada.Dia == 0)
                                    {
                                        registroentrada.TipoAsistencia = 78;
                                    }*/
                            }
                            if (detalle.Tipo.Equals("Salida"))
                            {
                                //Salidas
                                DateTime horasalidaHorario = await Util.ParseHora(horario.HoraFinal);

                                DateTime horaSalidaBiometrico = DateTime.Today.AddHours(detalle.FechaHora.Hour)
                                    .AddMinutes(detalle.FechaHora.Minute);
                                
                                if (horaSalidaBiometrico > horasalidaHorario)
                                {
                                    var diferencia = horaSalidaBiometrico.Subtract(horasalidaHorario);
                                    if ((diferencia.Hours * 60 + diferencia.Minutes) >= periodoGraciaSalida)
                                    {
                                        if (detalle.Empleado.HorasExtra ?? false)
                                        {
                                            string horaentrada = biometrico.Detalle
                                                .Where(q => q.IdEmpleado == detalle.IdEmpleado &&
                                                            q.Tipo.Equals("Entrada") &&
                                                            q.FechaHora.Date == biometrico.Fecha.Date)
                                                .FirstOrDefault()?.FechaHora.ToString("HH:mm:ss") ?? "No se encontró registro de salida";
                                            
                                            HorasExtraBiometrico registroExistente = await context.HorasExtrasBiometrico
                                                .Include(b => b.Encabezado)
                                                .FirstOrDefaultAsync(
                                                    r => r.IdEmpleado == detalle.IdEmpleado &&
                                                         r.Encabezado.Fecha.Equals(biometrico.Fecha));

                                            
                                            if (registroExistente != null)
                                            {
                                                // Sumar las horas y minutos del registro existente al registro actual
                                                int totalHoras = registroExistente.Horas + diferencia.Hours;
                                                int totalMinutos = registroExistente.Minutos + diferencia.Minutes;

                                                // Ajustar los minutos a horas si superan los 60
                                                int horasExtra = totalMinutos / 60;
                                                int minutosExtra = totalMinutos % 60;

                                                // Actualizar el registro actual con las horas y minutos ajustados
                                                registroExistente.Horas = totalHoras + horasExtra;
                                                registroExistente.Minutos = minutosExtra;

                                            }
                                            else
                                            {
                                                var registro = new HorasExtraBiometrico()
                                                {
                                                    Id = 0,
                                                    IdEmpleado = detalle.IdEmpleado,
                                                    HoraEntrada = horaentrada,
                                                    HoraSalida = horaSalidaBiometrico.ToString("HH:mm:ss"),
                                                    Horas = diferencia.Hours,
                                                    Minutos = diferencia.Minutes,
                                                    IdBiometrico = biometrico.Id,
                                                    IdEstado = 70,
                                                    Estados = "Pendiente"
                                                };
                                                context.HorasExtrasBiometrico.Add(registro);
                                            }
                                            
                                            await context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                        }

                        biometrico.IdEstado = 62;/// marcar biometrico lo marca como aprobado
                        transaction.Commit();
                        await context.SaveChangesAsync();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al aprobar los registros del archivo biometrico.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]/{IdBiometrico}")]
        public async Task<ActionResult> RechazarBiometrico(long IdBiometrico)
        {
            try
            {
                var biometrico = await context.Biometricos.FirstOrDefaultAsync(r => r.Id == IdBiometrico);
                if (biometrico == null)
                    throw new Exception("Archivo biometrico no existe");


                if (biometrico.IdEstado != 60)
                    throw new Exception("Solo se puede rechazar un archivo biometrico en estado de CARGADO.");

                biometrico.IdEstado = 61;

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error al rechazar archivo biometrico.");
                return BadRequest(ex.Message);
            }
        }


    }
}