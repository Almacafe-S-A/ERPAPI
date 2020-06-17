using System;
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
    [Route("api/GoodsDeliveryAuthorization")]
    [ApiController]
    public class GoodsDeliveryAuthorizationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveryAuthorizationController(ILogger<GoodsDeliveryAuthorizationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorization paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var query = _context.GoodsDeliveryAuthorization.AsQueryable();
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

        /// <summary>
        /// Obtiene el Listado de GoodsDeliveryAuthorizationes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorization()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.GoodsDeliveryAuthorization.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();
                }
                else
                {
                    Items = await _context.GoodsDeliveryAuthorization.OrderByDescending(b => b.GoodsDeliveryAuthorizationId).ToListAsync();
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
        /// Obtiene los Datos de la GoodsDeliveryAuthorization por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveryAuthorizationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveryAuthorizationId}")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationById(Int64 GoodsDeliveryAuthorizationId)
        {
            GoodsDeliveryAuthorization Items = new GoodsDeliveryAuthorization();
            try
            {
                Items = await _context.GoodsDeliveryAuthorization.Include(q=>q.GoodsDeliveryAuthorizationLine)
                       .Where(q => q.GoodsDeliveryAuthorizationId == GoodsDeliveryAuthorizationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelected()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<Int64> listayaprocesada = _context.GoodsDeliveredLine
                                              .Where(q => q.NoAR > 0)
                                              .Select(q => q.NoAR).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains(q.GoodsDeliveryAuthorizationId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveryAuthorizationNoSelectedBoletaSalida()
        {
            List<GoodsDeliveryAuthorization> Items = new List<GoodsDeliveryAuthorization>();
            try
            {
                List<Int64> listayaprocesada = _context.BoletaDeSalida
                                              .Where(q => q.GoodsDeliveryAuthorizationId > 0)
                                              .Select(q => q.GoodsDeliveryAuthorizationId).ToList();

                Items = await _context.GoodsDeliveryAuthorization.Where(q => !listayaprocesada.Contains(q.GoodsDeliveryAuthorizationId)).ToListAsync();
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
        /// Inserta una nueva GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Insert([FromBody]GoodsDeliveryAuthorizationDTO _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
                        _context.GoodsDeliveryAuthorization.Add(_GoodsDeliveryAuthorizationq);

                        foreach (var item in _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationLine)
                        {
                            item.GoodsDeliveryAuthorizationId = _GoodsDeliveryAuthorizationq.GoodsDeliveryAuthorizationId;
                            _context.GoodsDeliveryAuthorizationLine.Add(item);

                            Int64 IdCD = await _context.CertificadoDeposito.Where(q => q.NoCD == item.NoCertificadoDeposito).Select(q => q.IdCD).FirstOrDefaultAsync();
                            

                            Kardex _kardexmax = await  _context.Kardex.Where(
                                    q =>q.CustomerId == _GoodsDeliveryAuthorizationq.CustomerId &&
                                    q.SubProducId == item.SubProductId &&
                                    q.DocumentId == _GoodsDeliveryAuthorizationq.NoCD &&
                                    q.DocumentName == "Certficado de Depósito" ).OrderByDescending(o => o.KardexId).FirstOrDefaultAsync();

                            

                            if (_kardexmax == null) { 
                                BadRequest($"No se Encontro Kardex Para este Producto en el Certificado"); 
                            }

                          

                            

                            SubProduct _subproduct = await (from c in _context.SubProduct
                                                     .Where(q => q.SubproductId == item.SubProductId)
                                                            select c
                                                     ).FirstOrDefaultAsync();

                            if(_kardexmax.TotalCD< item.Quantity)
                            {
                                return await Task.Run(() => BadRequest($"La cantidad a retirar no puede ser superior al total del ciertificado"));
                            }
                            ////////Entra a la Autorizacion/////////////
                            _context.Kardex.Add(new Kardex
                            {
                                DocumentDate = _GoodsDeliveryAuthorization.DocumentDate,
                                ProducId = _GoodsDeliveryAuthorizationq.ProductId,
                                ProductName = _GoodsDeliveryAuthorizationq.ProductName,
                                SubProducId = Convert.ToInt32(item.SubProductId),
                                SubProductName = item.SubProductName,
                                QuantityEntry = item.Quantity,
                                QuantityOut = 0,
                                QuantityEntryBags = 0,
                                BranchId = _GoodsDeliveryAuthorization.BranchId,
                                BranchName = _GoodsDeliveryAuthorization.BranchName,
                                WareHouseId = Convert.ToInt32(item.WarehouseId),
                                WareHouseName = item.WarehouseName,
                                UnitOfMeasureId = item.UnitOfMeasureId,
                                UnitOfMeasureName = item.UnitOfMeasureName,
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                                DocumentName = "SolicitudAutorizacion/GoodsDeliveryAuthorization",
                                Total = item.valorcertificado,
                                //TotalBags = item. + _KardexLine.TotalBags,
                               // QuantityEntryCD = item.Quantity / (1 + _subproduct.Merma),
                                //QuantityOutCD = item.Quantity,
                                TotalCD = item.valorcertificado ,
                            });

                            ///////////////////////Sale del Certificado////////////////
                            _context.Kardex.Add(new Kardex
                            {
                                DocumentDate = _kardexmax.DocumentDate,
                                ProducId = _kardexmax.ProducId,
                                ProductName = _kardexmax.ProductName,
                                SubProducId = Convert.ToInt32(_kardexmax.SubProducId),
                                SubProductName = _kardexmax.SubProductName,
                                QuantityEntry = 0,
                                QuantityOut = item.Quantity,
                                QuantityEntryBags = 0,
                                BranchId = _kardexmax.BranchId,
                                BranchName = _kardexmax.BranchName,
                                WareHouseId = Convert.ToInt32(_kardexmax.WareHouseId),
                                WareHouseName = _kardexmax.WareHouseName,
                                UnitOfMeasureId = _kardexmax.UnitOfMeasureId,
                                UnitOfMeasureName = _kardexmax.UnitOfMeasureName,
                                TypeOperationId = 2,
                                TypeOperationName = "Salida",
                                DocumentName = "SolicitudAutorizacion/GoodsDeliveryAuthorization",
                                Total = _kardexmax.Total -  item.valorcertificado,
                                //TotalBags = item. + _KardexLine.TotalBags,
                                QuantityEntryCD = 0,
                                QuantityOutCD = item.Quantity / (1 + Convert.ToDecimal(_subproduct.Merma)),
                                TotalCD = item.valorcertificado,
                            });


                        }
                                                                     
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId,
                            DocType = "GoodsDeliveryAuthorization",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDeliveryAuthorization, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsDeliveryAuthorization.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDeliveryAuthorization.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsDeliveryAuthorization.UsuarioModificacion,

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
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return Ok(_GoodsDeliveryAuthorizationq);
        }

        /// <summary>
        /// Actualiza la GoodsDeliveryAuthorization
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDeliveryAuthorization>> Update([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = _GoodsDeliveryAuthorization;
            try
            {
                _GoodsDeliveryAuthorizationq = await (from c in _context.GoodsDeliveryAuthorization
                                 .Where(q => q.GoodsDeliveryAuthorizationId == _GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                                                      select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveryAuthorizationq).CurrentValues.SetValues((_GoodsDeliveryAuthorization));

                //_context.GoodsDeliveryAuthorization.Update(_GoodsDeliveryAuthorizationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));
        }

        /// <summary>
        /// Elimina una GoodsDeliveryAuthorization       
        /// </summary>
        /// <param name="_GoodsDeliveryAuthorization"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDeliveryAuthorization _GoodsDeliveryAuthorization)
        {
            GoodsDeliveryAuthorization _GoodsDeliveryAuthorizationq = new GoodsDeliveryAuthorization();
            try
            {
                _GoodsDeliveryAuthorizationq = _context.GoodsDeliveryAuthorization
                .Where(x => x.GoodsDeliveryAuthorizationId == (Int64)_GoodsDeliveryAuthorization.GoodsDeliveryAuthorizationId)
                .FirstOrDefault();

                _context.GoodsDeliveryAuthorization.Remove(_GoodsDeliveryAuthorizationq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveryAuthorizationq));

        }







    }
}