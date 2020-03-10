﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
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
    [Route("api/PrecioCafe")]
    [ApiController]
    public class PrecioCafeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PrecioCafeController(ILogger<PrecioCafeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPrecioCafePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PrecioCafe> Items = new List<PrecioCafe>();
            try
            {
                var query = _context.PrecioCafe.AsQueryable();
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
        /// Obtiene el Listado de PrecioCafees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<PrecioCafe>>> GetPrecioCafe()
        {
            List<PrecioCafe> Items = new List<PrecioCafe>();
            try
            {
                Items = await _context.PrecioCafe.ToListAsync();
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
        /// Obtiene los Datos de la PrecioCafe por medio del Id enviado.
        /// </summary>
        /// <param name="PrecioCafeId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PrecioCafeId}")]
        public async Task<ActionResult<PrecioCafe>> GetPrecioCafeById(Int64 PrecioCafeId)
        {
            PrecioCafe Items = new PrecioCafe();
            try
            {
                Items = await _context.PrecioCafe.Where(q => q.Id == PrecioCafeId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva PrecioCafe
        /// </summary>
        /// <param name="_PrecioCafe"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PrecioCafe>> Insert([FromBody]PrecioCafe _PrecioCafe)
        {
            PrecioCafe _PrecioCafeq = new PrecioCafe();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PrecioCafeq = _PrecioCafe;
                        _context.PrecioCafe.Add(_PrecioCafeq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PrecioCafe.Id,
                            DocType = "PrecioCafe",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_PrecioCafe, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PrecioCafe.UsuarioCreacion,
                            UsuarioModificacion = _PrecioCafe.UsuarioModificacion,
                            UsuarioEjecucion = _PrecioCafe.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_PrecioCafeq));
        }

        /// <summary>
        /// Actualiza la PrecioCafe
        /// </summary>
        /// <param name="_PrecioCafe"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PrecioCafe>> Update(PrecioCafe _PrecioCafe)
        {
            PrecioCafe _PrecioCafeq = _PrecioCafe;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PrecioCafeq = await (from c in _context.PrecioCafe
                                         .Where(q => q.Id == _PrecioCafe.Id
                                         )
                                           select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_PrecioCafeq).CurrentValues.SetValues((_PrecioCafe));

                        //_context.PrecioCafe.Update(_PrecioCafeq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _PrecioCafe.Id
                            ,
                            DocType = "PrecioCafe",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_PrecioCafeq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_PrecioCafe, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _PrecioCafe.UsuarioCreacion,
                            UsuarioModificacion = _PrecioCafe.UsuarioModificacion,
                            UsuarioEjecucion = _PrecioCafe.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_PrecioCafeq));
        }

        /// <summary>
        /// Elimina una PrecioCafe       
        /// </summary>
        /// <param name="_PrecioCafe"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PrecioCafe>> Delete([FromBody]PrecioCafe _PrecioCafe)
        {
            PrecioCafe _PrecioCafeq = new PrecioCafe();
            try
            {
                _PrecioCafeq = _context.PrecioCafe
                .Where(x => x.Id == (Int64)_PrecioCafe.Id)
                .FirstOrDefault();

                _context.PrecioCafe.Remove(_PrecioCafeq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PrecioCafeq));

        }







    }
}