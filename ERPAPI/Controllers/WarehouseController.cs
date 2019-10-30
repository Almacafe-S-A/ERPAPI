using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Warehouse")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public WarehouseController(ILogger<WarehouseController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de Warehouse paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWarehousePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Warehouse> Items = new List<Warehouse>();
            try
            {
                var query = _context.Warehouse.AsQueryable();
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

            return await Task.Run(() => Ok(Items));
        }

        // GET: api/Warehouse
        [HttpGet("[action]")]
        public async Task<IActionResult> GetWarehouse()
        {
            List<Warehouse> Items = new List<Warehouse>();
            try
            {
                Items =  await _context.Warehouse.ToListAsync();
                //int Count = Items.Count();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok( Items));
        }

        [HttpGet("[action]/{WarehouseId}")]
        public async Task<IActionResult> GetWarehouseById(Int64 WarehouseId)
        {
            Warehouse Items = new Warehouse();
            try
            {
                Items = await _context.Warehouse.Where(q => q.WarehouseId == WarehouseId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{BranchId}")]
        public async Task<IActionResult> GetWarehouseByBranchId(Int64 BranchId)
        {
            List<Warehouse> Items = new List<Warehouse>();
            try
            {
                Items = await _context.Warehouse.Where(q=>q.BranchId==BranchId).ToListAsync();
              
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{WarehouseName}")]
        public async Task<IActionResult> GetWarehouseByName(String WarehouseName)
        {
            Warehouse Items = new Warehouse();
            try
            {
                Items = await _context.Warehouse.Where(q => q.WarehouseName == WarehouseName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Warehouse>> Insert([FromBody]Warehouse _warehouse)
        {
            Warehouse warehouse = _warehouse;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Warehouse.Add(warehouse);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _warehouse.WarehouseId,
                            DocType = "Warehouse",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_warehouse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_warehouse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _warehouse.UsuarioCreacion,
                            UsuarioModificacion = _warehouse.UsuarioModificacion,
                            UsuarioEjecucion = _warehouse.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }



            return await Task.Run(() => Ok(warehouse));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Warehouse>> Update([FromBody]Warehouse _Warehouse)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        Warehouse warehouseq = (from c in _context.Warehouse
                        .Where(q => q.WarehouseId == _Warehouse.WarehouseId)
                                                select c
                         ).FirstOrDefault();

                        _Warehouse.FechaCreacion = warehouseq.FechaCreacion;
                        _Warehouse.UsuarioCreacion = warehouseq.UsuarioCreacion;
                        _context.Entry(warehouseq).CurrentValues.SetValues((_Warehouse));
                        //    _context.Warehouse.Update(_Warehouse);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Warehouse.WarehouseId,
                            DocType = "Warehouse",
                            ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(warehouseq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Warehouse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Warehouse.UsuarioCreacion,
                            UsuarioModificacion = _Warehouse.UsuarioModificacion,
                            UsuarioEjecucion = _Warehouse.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Warehouse));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Warehouse>> Delete([FromBody]Warehouse _Warehouse)
        {
            Warehouse warehouse = new Warehouse();

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        warehouse = _context.Warehouse
                         .Where(x => x.WarehouseId == (int)_Warehouse.WarehouseId)
                          .FirstOrDefault();

                        _context.Warehouse.Remove(warehouse);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Warehouse.WarehouseId,
                            DocType = "Warehouse",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_Warehouse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Warehouse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Delete",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Warehouse.UsuarioCreacion,
                            UsuarioModificacion = _Warehouse.UsuarioModificacion,
                            UsuarioEjecucion = _Warehouse.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(warehouse));

        }
    }
}