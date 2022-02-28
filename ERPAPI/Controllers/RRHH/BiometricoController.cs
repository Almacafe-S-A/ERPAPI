using System;
using System.Collections.Generic;
using System.Linq;
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
                    return BadRequest("Ya existe una carga para esta fecha");
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

                        var inasistencia = await
                            context.Inasistencias.FirstOrDefaultAsync(
                                i => i.Fecha.Equals(biometrico.Fecha) && i.IdEmpleado == det.IdEmpleado);

                        if (inasistencia != null)
                        {
                            inasistencia.IdEstado = 81;
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
                var registros = await context.DetallesBiometricos.Include(e=> e.Empleado).Where(b => b.Encabezado.Id == IdBiometrico).ToListAsync();
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
                        var confGracia = await context.ElementoConfiguracion.FirstOrDefaultAsync(p => p.Id == 140);
                        int periodoGraciaEntrada = (int) (confGracia.Valordecimal ?? 5);
                        confGracia = await context.ElementoConfiguracion.FirstOrDefaultAsync(p => p.Id == 141);
                        int periodoGraciaSalida = (int) (confGracia.Valordecimal ?? 60);
                        foreach (var detalle in biometrico.Detalle)
                        {
                            var horarioEmpleado = await context.EmpleadoHorarios.Include(h => h.HorarioEmpleado)
                                .FirstOrDefaultAsync(e => e.EmpleadoId == detalle.IdEmpleado);
                            if (horarioEmpleado == null)
                                throw new Exception("Empleado sin horario asignado en el archivo del biometrico:" + detalle.Empleado.NombreEmpleado);

                            var horario = horarioEmpleado.HorarioEmpleado;
                            
                            //Eliminar inasistencia si existe

                            var inasistencia = await context.Inasistencias.FirstOrDefaultAsync(r =>
                                r.Fecha.Equals(biometrico.Fecha) && r.IdEmpleado == detalle.IdEmpleado);

                            if (inasistencia != null)
                            {
                                context.Inasistencias.Remove(inasistencia);
                            }

                            if (detalle.Tipo.ToUpper().Equals("E"))
                            {
                                //Entradas
                                var horaEntradaHorario = await Util.ParseHora(horario.HoraInicio);
                                var horaEntradaBiometrico = DateTime.Today.AddHours(detalle.FechaHora.Hour)
                                    .AddMinutes(detalle.FechaHora.Minute);

                                if (horaEntradaBiometrico > horaEntradaHorario)
                                {
                                    var diferencia = horaEntradaBiometrico.Subtract(horaEntradaHorario);
                                    if (diferencia.Minutes > periodoGraciaEntrada)
                                    {
                                        //Es una llegada tarde
                                        var registro = new LlegadasTardeBiometrico()
                                                       {
                                                            Id = 0,
                                                            IdEmpleado = detalle.IdEmpleado,
                                                            Horas = diferencia.Hours,
                                                            Minutos = diferencia.Minutes,
                                                            IdBiometrico = biometrico.Id,
                                                            IdEstado = 70
                                                       };

                                        var registroExistente = await context.LlegadasTardeBiometrico
                                            .Include(b => b.Encabezado)
                                            .FirstOrDefaultAsync(
                                                r => r.IdEmpleado == detalle.IdEmpleado &&
                                                     r.Encabezado.Fecha.Equals(biometrico.Fecha));

                                        if (registroExistente != null)
                                        {
                                            if (registroExistente.Horas < registro.Horas)
                                            {
                                                registroExistente.Horas = registro.Horas;
                                                await context.SaveChangesAsync();
                                                continue;
                                            }
                                        }

                                        context.LlegadasTardeBiometrico.Add(registro);
                                        await context.SaveChangesAsync();
                                    }
                                }
                            }
                            else
                            {
                                //Salidas
                                var horasalidaHorario = await Util.ParseHora(horario.HoraFinal);
                                var horaSalidaBiometrico = DateTime.Today.AddHours(detalle.FechaHora.Hour)
                                    .AddMinutes(detalle.FechaHora.Minute);

                                if (horaSalidaBiometrico > horasalidaHorario)
                                {
                                    var diferencia = horaSalidaBiometrico.Subtract(horasalidaHorario);
                                    if ((diferencia.Hours * 60 + diferencia.Minutes) >= periodoGraciaSalida)
                                    {
                                        if (detalle.Empleado.HorasExtra ?? false)
                                        {
                                            //Potencial Hora Extra
                                            var registro = new HorasExtraBiometrico()
                                            {
                                                Id = 0,
                                                IdEmpleado = detalle.IdEmpleado,
                                                Horas = diferencia.Hours,
                                                Minutos = diferencia.Minutes,
                                                IdBiometrico = biometrico.Id,
                                                IdEstado = 70
                                            };
                                            var registroExistente = await context.HorasExtrasBiometrico
                                                .Include(b => b.Encabezado)
                                                .FirstOrDefaultAsync(
                                                    r => r.IdEmpleado == detalle.IdEmpleado &&
                                                         r.Encabezado.Fecha.Equals(biometrico.Fecha));

                                            if (registroExistente != null)
                                            {
                                                if (registroExistente.Horas < registro.Horas)
                                                {
                                                    registroExistente.Horas = registro.Horas;
                                                    await context.SaveChangesAsync();
                                                    continue;
                                                }
                                            }

                                            context.HorasExtrasBiometrico.Add(registro);
                                            await context.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                        }

                        biometrico.IdEstado = 62;
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