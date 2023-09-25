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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Horario")]
    [ApiController]
    public class HorarioController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public HorarioController(ApplicationDbContext context, ILogger<HorarioController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Guardar(Horario horario)
        {
            try
            {
                if (horario.Id == 0)
                {
                    context.Horarios.Add(horario);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();
                    return Ok(horario);
                }
                else
                {
                    Horario horarioExistente = await context.Horarios.FirstOrDefaultAsync(f => f.Id == horario.Id);
                    if (horarioExistente == null)
                    {
                        throw new Exception("Registro de horario a actualizar no existe");
                    }

                    context.Entry(horarioExistente).CurrentValues.SetValues(horario);

                    //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                    new appAuditor(context, logger, User.Identity.Name).SetAuditor();

                    await context.SaveChangesAsync();
                    return Ok(horarioExistente);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el registro del horario");
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetHorarios()
        {
            try
            {
                var horarios = await context.Horarios.ToListAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al cargar horarios");
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Obtiene los Datos de la horarios por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetHorarioById(Int64 Id)
        {
            Horario Items = new Horario();
            try
            {
                Items = await context.Horarios.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <param name="_Horario"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Horario>> Insert([FromBody] Horario _Horario)
        {
            Horario _Horarioq = new Horario();
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    try
                    {
                        _Horarioq = _Horario;
                        context.Horarios.Add(_Horarioq);
                        
                        _Horario.UsuarioCreacion = User.Identity.Name;
                        _Horario.FechaCreacion = DateTime.Now;
                        _Horario.FechaModificacion = DateTime.Now;
                        _Horario.UsuarioModificacion = User.Identity.Name;
                        
                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(context, logger, User.Identity.Name).SetAuditor();
                        await context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Horarioq));
        }



        /// <param name="_Horario"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Horario>> Update([FromBody] Horario _Horario)
        {
            Horario _Horarioq =  new Horario();
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        _Horarioq = await (from c in context.Horarios
                                  .Where(q => q.Id == _Horario.Id)
                                           select c
                                 ).FirstOrDefaultAsync();

                        _Horario.UsuarioCreacion = _Horarioq.UsuarioCreacion;
                        _Horario.FechaCreacion = _Horarioq.FechaCreacion;
                        _Horario.FechaModificacion = DateTime.Now;
                        _Horario.UsuarioModificacion = User.Identity.Name;

                        context.Entry(_Horarioq).CurrentValues.SetValues((_Horario));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(context, logger, User.Identity.Name).SetAuditor();                      
                        await context.SaveChangesAsync();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
            }
            catch (Exception ex)
            {

                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Horarioq));
        }


        /// <param name="_Horario"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] Horario _Horario)
        {
            Horario _Horarioq = new Horario();
            try
            {
                _Horarioq = context.Horarios
                .Where(x => x.Id == (Int64)_Horario.Id)
                .FirstOrDefault();

                context.Horarios.Remove(_Horarioq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(context, logger, User.Identity.Name).SetAuditor();


                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Horarioq));

        }

    }
}