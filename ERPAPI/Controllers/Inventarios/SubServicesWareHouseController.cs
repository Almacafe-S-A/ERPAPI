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
    [Route("api/SubServicesWareHouse")]
    [ApiController]
    public class SubServicesWareHouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SubServicesWareHouseController(ILogger<SubServicesWareHouseController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SubServicesWareHouse paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubServicesWareHousePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<SubServicesWareHouse> Items = new List<SubServicesWareHouse>();
            try
            {
                var query = _context.SubServicesWareHouse.AsQueryable();
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de SubServicesWareHousees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubServicesWareHouse()
        {
            List<SubServicesWareHouse> Items = new List<SubServicesWareHouse>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.SubServicesWareHouse.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.SubServicesWareHouseId).ToListAsync();
                }
                else
                {
                    Items = await _context.SubServicesWareHouse.OrderByDescending(b => b.SubServicesWareHouseId).ToListAsync();
                }
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
        /// Obtiene los Datos de la SubServicesWareHouse por medio del Id enviado.
        /// </summary>
        /// <param name="SubServicesWareHouseId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{SubServicesWareHouseId}")]
        public async Task<IActionResult> GetSubServicesWareHouseById(Int64 SubServicesWareHouseId)
        {
            SubServicesWareHouse Items = new SubServicesWareHouse();
            try
            {
                Items = await _context.SubServicesWareHouse.Where(q => q.SubServicesWareHouseId == SubServicesWareHouseId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva SubServicesWareHouse
        /// </summary>
        /// <param name="_SubServicesWareHouse"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<SubServicesWareHouse>> Insert([FromBody]SubServicesWareHouse _SubServicesWareHouse)
        {
            SubServicesWareHouse _SubServicesWareHouseq = new SubServicesWareHouse();
            try
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _SubServicesWareHouseq = _SubServicesWareHouse;
                        _SubServicesWareHouseq.UsuarioCreacion = User.Identity.Name;
                        _SubServicesWareHouseq.FechaCreacion = DateTime.Now;
                        _context.SubServicesWareHouse.Add(_SubServicesWareHouseq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_SubServicesWareHouseq));
        }

        /// <summary>
        /// Aprueba (pasa a Estatus Cerrado),para que el subservcio pase a contabilidad, CxC, Facturacion.
        /// </summary>
        /// <param name="_SubServicesWareHouseq"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<SubServicesWareHouse>> Aprobar (int id)
        {

            SubServicesWareHouse _SubServicesWareHouseq = new SubServicesWareHouse();

            try
            {
                _SubServicesWareHouseq = await _context.SubServicesWareHouse
                    .Where(q => q.SubServicesWareHouseId == id)
                    .FirstOrDefaultAsync();
                if (_SubServicesWareHouseq.QuantityHours <= 0)
                {
                    return BadRequest("No se pueden aprobar Servicios brindados con cantidad de horas en cero");
                }

                if (_SubServicesWareHouseq.EndTime <= _SubServicesWareHouseq.StartTime)
                {
                    return BadRequest("No se pueden aprobar Servicios brindados con la fecha final menor o igual a la dee inicio");
                }
                _SubServicesWareHouseq.IdEstado = 6;
                _SubServicesWareHouseq.Estado = "Aprobado";
                _SubServicesWareHouseq.UsuarioModificacion = User.Identity.Name;

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SubServicesWareHouseq));
        }

        /// <summary>
        /// Actualiza la SubServicesWareHouse
        /// </summary>
        /// <param name="_SubServicesWareHouse"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<SubServicesWareHouse>> Update([FromBody]SubServicesWareHouse _SubServicesWareHouse)
        {
            SubServicesWareHouse _SubServicesWareHouseq = _SubServicesWareHouse;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _SubServicesWareHouseq = await (from c in _context.SubServicesWareHouse
                              .Where(q => q.SubServicesWareHouseId == _SubServicesWareHouse.SubServicesWareHouseId)
                                                        select c
                             ).FirstOrDefaultAsync();

                        _SubServicesWareHouse.FechaCreacion= _SubServicesWareHouseq.FechaCreacion;
                        _SubServicesWareHouse.UsuarioCreacion= _SubServicesWareHouseq.UsuarioCreacion;
                        _context.Entry(_SubServicesWareHouseq).CurrentValues.SetValues((_SubServicesWareHouse));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.SubServicesWareHouse.Update(_SubServicesWareHouseq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _SubServicesWareHouseq.SubServicesWareHouseId,
                            DocType = "SubServicesWareHouse",
                            ClaseInicial =
                                    Newtonsoft.Json.JsonConvert.SerializeObject(_SubServicesWareHouseq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _SubServicesWareHouseq.UsuarioCreacion,
                            UsuarioModificacion = _SubServicesWareHouseq.UsuarioModificacion,
                            UsuarioEjecucion = _SubServicesWareHouseq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_SubServicesWareHouseq));
        }

        /// <summary>
        /// Elimina una SubServicesWareHouse       
        /// </summary>
        /// <param name="_SubServicesWareHouse"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]SubServicesWareHouse _SubServicesWareHouse)
        {
            SubServicesWareHouse _SubServicesWareHouseq = new SubServicesWareHouse();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _SubServicesWareHouseq = _context.SubServicesWareHouse
                       .Where(x => x.SubServicesWareHouseId == (Int64)_SubServicesWareHouse.SubServicesWareHouseId)
                       .FirstOrDefault();

                        _context.SubServicesWareHouse.Remove(_SubServicesWareHouseq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _SubServicesWareHouseq.SubServicesWareHouseId,
                            DocType = "SubServicesWareHouse",
                            ClaseInicial =
                                      Newtonsoft.Json.JsonConvert.SerializeObject(_SubServicesWareHouseq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _SubServicesWareHouseq.UsuarioCreacion,
                            UsuarioModificacion = _SubServicesWareHouseq.UsuarioModificacion,
                            UsuarioEjecucion = _SubServicesWareHouseq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_SubServicesWareHouseq));

        }







    }
}