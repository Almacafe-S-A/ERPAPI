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
                        .Where(p => branchlist.Any(b => p.Warehouse.BranchId == b.BranchId))
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
                        _context.InventarioFisico.Add(_InventarioFisico);
                        //await _context.SaveChangesAsync();

                        //foreach (var item in _InventarioFisico.InventarioFisicoLines)
                        //{
                        //    item.InventarioFisicoId = _InventarioFisico.Id;
                        //    _context.InventarioFisicoLines.Add(item);
                        //}


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
        [HttpPut("[action]")]
        public async Task<ActionResult<InventarioFisico>> Update([FromBody] InventarioFisico _InventarioFisico)
        {
            InventarioFisico _InventarioFisicoq = _InventarioFisico;
            try
            {
                _InventarioFisicoq = await (from c in _context.InventarioFisico
                                 .Where(q => q.Id == _InventarioFisico.Id)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_InventarioFisicoq).CurrentValues.SetValues((_InventarioFisico));

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
        [HttpGet("[action]/{BranchId}/{WarehouseId}")]
        public async Task<IActionResult> GetSaldoLibros(int BranchId, long WarehouseId)
        {
            List<InventarioFisicoLine> inventarioFisicoLines = new List<InventarioFisicoLine>();
            try
            {
                
                List<Kardex> kardex = await _context.Kardex.Where(q =>q.WareHouseId == WarehouseId 
                && q.BranchId == BranchId
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
                    .Where(q => q.WareHouseId == WarehouseId)
                                         select new InventarioFisicoLine {
                                            ProductoId = (long)k.SubProductId,
                                            ProductoNombre = k.SubProductName,
                                            Diferencia = - ( k.QuantitySacos != null && k.QuantitySacos > 0 ? (decimal)k.QuantitySacos : k.Quantity),
                                            SaldoLibros = k.QuantitySacos != null && k.QuantitySacos>0 ?  (decimal)k.QuantitySacos: k.Quantity,
                                            InventarioFisicoCantidad = 0,
                                         
                                         
                                         
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
