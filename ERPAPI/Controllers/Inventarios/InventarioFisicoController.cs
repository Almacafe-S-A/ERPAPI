using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ERPAPI.Contexts;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/InventarioFisico")]
    [ApiController]
    public class InventarioFisicoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InventarioFisicoController(ILogger<InventarioFisicoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de InventarioFisicoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInventarioFisico()
        {
            List<InventarioFisico> Items = new List<InventarioFisico>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch
                    //.Where(w => w.UserId == user.FirstOrDefault().Id)
                    .ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.InventarioFisico
                        //.Include(i => i.Warehouse.)
                        //.Where(p => branchlist.Any(b => p.Warehouse.BranchId == b.BranchId))
                        .OrderByDescending(b => b.Id).ToListAsync();
                }
                else
                {
                    Items = await _context.InventarioFisico.OrderByDescending(b => b.Id).ToListAsync();
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


        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetInventarioFisicoByCustomer(Int64 CustomerId)
        {
            List<InventarioFisico> Items = new List<InventarioFisico>();
            try
            {
                Items = await _context.InventarioFisico.Where(q => q.CustomerId == CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetInventarioFisicoByCustomerPendientecertificar(Int64 CustomerId)
        {
            List<InventarioFisico> Items = new List<InventarioFisico>();
            try
            {
                Items = await _context.InventarioFisico.Where(q => q.CustomerId == CustomerId)
                    .OrderByDescending(d => d.Id)
                    .Take(3)
                    .ToListAsync();
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
        /// Obtiene los Datos de la InventarioFisico por medio del Id enviado.
        /// </summary>
        /// <param name="InventarioFisicoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InventarioFisicoId}")]
        public async Task<IActionResult> GetInventarioFisicoById(Int64 InventarioFisicoId)
        {
            InventarioFisico Items = new InventarioFisico();
            try
            {
                Items = await _context.InventarioFisico.Where(q => q.Id == InventarioFisicoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        private InventarioFisico CheckDetailsInventario(InventarioFisico inventario) {

            foreach (var producto in inventario.InventarioFisicoLines)
            {
                producto.ProductoNombre = producto.Product != null ? producto.Product.ProductName : producto.ProductoNombre;
                producto.ProductoId = producto.Product != null ? producto.Product.SubproductId : 1;
                producto.Product = null;
                producto.UnitOfMeasureName = producto.UnitOfMeasure != null ? producto.UnitOfMeasure.UnitOfMeasureName : producto.UnitOfMeasureName;
                producto.UnitOfMeasureId = producto.UnitOfMeasure != null ? producto.UnitOfMeasure.UnitOfMeasureId : 1;
                producto.UnitOfMeasure = null;
                //producto.WarehouseId = producto.Warehouse != null ? (int)producto.Warehouse.WarehouseId : 1;
                //producto.WarehouseName = producto.Warehouse != null ? producto.Warehouse.WarehouseName : "";
                //producto.Warehouse = null;
            }
            foreach (var producto in inventario.InventarioBodegaHabilitadaLines)
            {
                producto.ProductoNombre = producto.Product != null ? producto.Product.ProductName : producto.ProductoNombre;
                producto.ProductoId = producto.Product != null ? producto.Product.SubproductId : 1;
                producto.Product = null;
                producto.UnitOfMeasureName = producto.UnitOfMeasure != null ? producto.UnitOfMeasure.UnitOfMeasureName : producto.UnitOfMeasureName;
                producto.UnitOfMeasureId = producto.UnitOfMeasure != null ? producto.UnitOfMeasure.UnitOfMeasureId : 1;
                producto.UnitOfMeasure = null;
                producto.WarehouseId = producto.Warehouse != null ? (int)producto.Warehouse.WarehouseId : 1;
                producto.WarehouseName = producto.Warehouse != null ? producto.Warehouse.WarehouseName : producto.WarehouseName;
                producto.Warehouse = null;
            }

            return inventario;


        }

        /// <summary>
        /// Inserta una nueva InventarioFisico
        /// </summary>
        /// <param name="_InventarioFisico"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InventarioFisico>> Insert([FromBody] InventarioFisico _InventarioFisico)
        {
            InventarioFisico _InventarioFisicoq = new InventarioFisico();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _InventarioFisico = CheckDetailsInventario(_InventarioFisico);
                        _context.InventarioFisico.Add(_InventarioFisico);
                        


                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _InventarioFisico.Id,
                            DocType = "InventarioFisico",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_InventarioFisico, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InventarioFisico, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _InventarioFisico.UsuarioCreacion,
                            UsuarioModificacion = _InventarioFisico.UsuarioModificacion,
                            UsuarioEjecucion = _InventarioFisico.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_InventarioFisicoq = _InventarioFisico;
                //_context.InventarioFisico.Add(_InventarioFisicoq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventarioFisicoq));
        }

        /// <summary>
        /// Actualiza la InventarioFisico
        /// </summary>
        /// <param name="_InventarioFisico"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InventarioFisico>> Update([FromBody] InventarioFisico _InventarioFisico)
        {
            InventarioFisico _InventarioFisicoq = new InventarioFisico(); 
            _InventarioFisicoq = CheckDetailsInventario(_InventarioFisico);
            try
            {
                
                var InventariodetEliminarbh = _context.InventarioBodegaHabilitada
                    .Where(q => q.InventarioFisicoId == _InventarioFisico.Id && 
                    !_InventarioFisico.InventarioBodegaHabilitadaLines.Where(s => s.Id != 0)
                    .Any(a => a.Id == q.Id )).ToList();
                _context.InventarioBodegaHabilitada.RemoveRange(InventariodetEliminarbh); 


                var InventariodetEliminar = _context.InventarioFisicoLines
                    .Where(q => q.InventarioFisicoId == _InventarioFisico.Id &&
                    !_InventarioFisico.InventarioFisicoLines.Where(s => s.Id != 0)
                    .Any(a => a.Id == q.Id)).ToList();
                _context.InventarioFisicoLines.RemoveRange(InventariodetEliminar);


                _InventarioFisicoq = await _context.InventarioFisico
                    .Where(q => q.Id == _InventarioFisico.Id)
                    .Include(i => i.InventarioBodegaHabilitadaLines)
                    .Include(i => i.InventarioFisicoLines)
                    .FirstOrDefaultAsync();

                _context.Entry(_InventarioFisicoq).CurrentValues.SetValues((_InventarioFisico));

                foreach (var item in _InventarioFisicoq.InventarioBodegaHabilitadaLines)
                {
                    var det = _InventarioFisico.InventarioBodegaHabilitadaLines.Where(q => q.Id == item.Id).FirstOrDefault();
                    if (det != null)
                    _context.Entry(item).CurrentValues.SetValues(det);
                }

                foreach (var item in _InventarioFisicoq.InventarioFisicoLines)
                {
                    var det = _InventarioFisico.InventarioFisicoLines.Where(q => q.Id == item.Id).FirstOrDefault();
                    if (det != null)
                        _context.Entry(item).CurrentValues.SetValues(det);
                }

                
                _InventarioFisicoq.InventarioBodegaHabilitadaLines
                    .AddRange(_InventarioFisico.InventarioBodegaHabilitadaLines
                    .Where(q => q.InventarioFisicoId == 0).ToList());
                _InventarioFisicoq.InventarioFisicoLines
                    .AddRange(_InventarioFisico.InventarioFisicoLines.Where(q => q.InventarioFisicoId == 0).ToList());


                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                //_context.InventarioFisico.Update(_InventarioFisicoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventarioFisicoq));
        }

        /// <summary>
        /// Elimina una InventarioFisico       
        /// </summary>
        /// <param name="_InventarioFisico"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody] InventarioFisico _InventarioFisico)
        {
            InventarioFisico _InventarioFisicoq = new InventarioFisico();
            try
            {
                _InventarioFisicoq = _context.InventarioFisico
                .Where(x => x.Id == (Int64)_InventarioFisico.Id)
                .FirstOrDefault();

                _context.InventarioFisico.Remove(_InventarioFisicoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InventarioFisicoq));

        }



        /// <summary>
        /// Obtiene el saldo en libros
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{BranchId}/{CustomerId}")]
        public async Task<IActionResult> GetSaldoLibros(int BranchId, long CustomerId)
        {
            List<InventarioFisicoLine> inventarioFisicoLines = new List<InventarioFisicoLine>();
            try
            {
                
                List<Kardex> kardex = await _context.Kardex.Where(q =>
                //q.WareHouseId == WarehouseId 
                q.BranchId == BranchId
                && q.Max == true
                ).ToListAsync();

                /* inventarioFisicoLines = (from k in kardex.GroupBy(g => g.ProducId)
                                          select new InventarioFisicoLine
                                          {
                                              ProductoId = (long) k.Key,
                                              ProductoNombre =  k.First().ProductName,
                                              Diferencia = 0,
                                              SaldoLibros =k.Sum(s => (decimal)s.TotalBags),
                                              InventarioFisicoCantidad = 0,

                                          }).ToList();*/

                inventarioFisicoLines = (from k in _context.GoodsReceivedLine.Include(g => g.GoodsReceived)
                                         .Include(p => p.SubProduct)
                    .Where(q =>
                   // q.WareHouseId == WarehouseId
                    q.GoodsReceived.BranchId == BranchId
                    && (CustomerId == 0 || q.GoodsReceived.CustomerId == CustomerId)
                    )
                                         select new InventarioFisicoLine {
                                            ProductoId = (long)k.SubProductId,
                                            ProductoNombre = k.SubProductName,
                                            Diferencia = - ( k.QuantitySacos != null && k.QuantitySacos > 0 ? (decimal)k.QuantitySacos : k.Quantity),
                                            SaldoLibros = k.QuantitySacos != null && k.QuantitySacos>0 ?  (decimal)k.QuantitySacos: k.Quantity,
                                            InventarioFisicoCantidad = 0,
                                            Product = k.SubProduct,
                                            UnitOfMeasure = _context.UnitOfMeasure.Where(e => e.UnitOfMeasureId == k.UnitOfMeasureId).FirstOrDefault(),
                                            UnitOfMeasureId = (int)k.UnitOfMeasureId,
                                            Estiba = k.ControlPalletsId.ToString(),
                                            WarehouseId = (int)k.WareHouseId,
                                            WarehouseName = k.WareHouseName,
                                            NSacos = (int)k.QuantitySacos,




                                         
                                         
                                         
                                         }).ToList();

                

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(inventarioFisicoLines);
        }



    }
}
