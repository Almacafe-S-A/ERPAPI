using System;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contrato_plan_pagos")]
    [ApiController]
    public class Contrato_plan_pagosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Contrato_plan_pagosController(ILogger<Contrato_plan_pagosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: /<controller>/

        [HttpGet("[action]/{ContratoId}")]
        public async Task<ActionResult<Contrato_plan_pagos>> GetContrato_plan_pagosByContratoId(Int64 ContratoId)
        {
            List<Contrato_plan_pagos> Items = new List<Contrato_plan_pagos>();
            try
            {
                Items = await _context.Contrato_plan_pagos.Where(q => q.ContratoId == ContratoId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Contrato_plan_pagosId}")]
        public async Task<ActionResult<Contrato_plan_pagos>> GetContrato_plan_pagosById(Int64 Contrato_plan_pagosId)
        {
            Contrato_plan_pagos Items = new Contrato_plan_pagos();
            try
            {
                Items = await _context.Contrato_plan_pagos.Where(q => q.Nro_cuota == Contrato_plan_pagosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContrato_plan_pagosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Contrato_plan_pagos> Items = new List<Contrato_plan_pagos>();
            try
            {
                var query = _context.Contrato_plan_pagos.AsQueryable();
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
        /// Inserta una nueva Contrato_plan_pagos
        /// </summary>
        /// <param name="_Contrato_plan_pagos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_plan_pagos>> Insert([FromBody]Contrato_plan_pagos _Contrato_plan_pagos)
        {
            Contrato_plan_pagos _Contrato_plan_pagosq = new Contrato_plan_pagos();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_plan_pagosq = _Contrato_plan_pagos;
                        _context.Contrato_plan_pagos.Add(_Contrato_plan_pagosq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_plan_pagos.Nro_cuota,
                            DocType = "Contrato_plan_pagos",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_plan_pagos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_plan_pagos.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_plan_pagos.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_plan_pagos.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_plan_pagosq));
        }

        /// <summary>
        /// Actualiza  Contrato_plan_pagos
        /// </summary>
        /// <param name="_Contrato_plan_pagos"></param>
        /// <returns></returns>
        [HttpPut("[action]")]

        public async Task<ActionResult<Contrato_plan_pagos>> Update([FromBody]Contrato_plan_pagos _Contrato_plan_pagos)
        {
            Contrato_plan_pagos _Contrato_plan_pagosq = _Contrato_plan_pagos;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_plan_pagosq = await (from c in _context.Contrato_plan_pagos
                                         .Where(q => q.Nro_cuota == _Contrato_plan_pagos.Nro_cuota)
                                                       select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Contrato_plan_pagosq).CurrentValues.SetValues((_Contrato_plan_pagos));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Contrato_plan_pagos.Update(_Contrato_plan_pagosq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_plan_pagos.Nro_cuota,
                            DocType = "Contrato_plan_pagos",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_plan_pagosq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_plan_pagos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_plan_pagos.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_plan_pagos.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_plan_pagos.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_plan_pagosq));
        }

        /// <summary>
        /// Elimina  Contrato_plan_pagos       
        /// </summary>
        /// <param name="_Contrato_plan_pagos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_plan_pagos>> Delete([FromBody]Contrato_plan_pagos _Contrato_plan_pagos)
        {
            Contrato_plan_pagos _Contrato_plan_pagosq = new Contrato_plan_pagos();
            try
            {
                _Contrato_plan_pagosq = _context.Contrato_plan_pagos
                .Where(x => x.Nro_cuota== (Int64)_Contrato_plan_pagos.Nro_cuota)
                .FirstOrDefault();

                _context.Contrato_plan_pagos.Remove(_Contrato_plan_pagosq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contrato_plan_pagosq));

        }

    }

}
