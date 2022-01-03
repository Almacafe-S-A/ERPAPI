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
    [Route("api/GoodsDelivered")]
    [ApiController]
    public class GoodsDeliveredController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsDeliveredController(ILogger<GoodsDeliveredController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsDelivered paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                var query = _context.GoodsDelivered.AsQueryable();
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
        /// Obtiene el Listado de GoodsDeliveredes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDelivered()
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();

           
           try
           {
               var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
               int count = user.Count();
               List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
               if (branchlist.Count > 0)
               {
                   Items = await _context.GoodsDelivered.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.GoodsDeliveredId).ToListAsync();
               }
               else
               {
                   Items = await _context.GoodsDelivered.OrderByDescending(b => b.GoodsDeliveredId).ToListAsync();
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


        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsDeliveredNoSelected()
        {
            List<GoodsDelivered> Items = new List<GoodsDelivered>();
            try
            {
                List<Int64> listayaprocesada = _context.BoletaDeSalida
                                              .Where(q => q.GoodsDeliveredId > 0)
                                              .Select(q => q.GoodsDeliveredId).ToList();

                Items = await _context.GoodsDelivered.Where(q => !listayaprocesada.Contains(q.GoodsDeliveredId)).ToListAsync();
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
        /// Obtiene los Datos de la GoodsDelivered por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsDeliveredId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsDeliveredId}")]
        public async Task<IActionResult> GetGoodsDeliveredById(Int64 GoodsDeliveredId)
        {
            GoodsDelivered Items = new GoodsDelivered();
            try
            {
                Items = await _context.GoodsDelivered.Include(q=>q._GoodsDeliveredLine).Where(q => q.GoodsDeliveredId == GoodsDeliveredId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Insert([FromBody]GoodsDeliveredDTO _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsDeliveredq = _GoodsDelivered;

                        BoletaDeSalida _boletadesalida = new BoletaDeSalida
                        {
                            BranchId = _GoodsDelivered.BranchId,
                            BranchName = _GoodsDelivered.BranchName,
                            CustomerId = _GoodsDelivered.CustomerId,
                            CustomerName = _GoodsDelivered.CustomerName,
                            DocumentDate = _GoodsDelivered.DocumentDate,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Marca = _GoodsDelivered.Marca,
                            Placa = _GoodsDelivered.Placa,
                            Motorista = _GoodsDelivered.Name,
                            Quantity = _GoodsDelivered._GoodsDeliveredLine.Select(q => q.QuantitySacos).Sum(),
                            SubProductId = _GoodsDelivered.SubProductId,
                            SubProductName = _GoodsDelivered.SubProductName,
                            GoodsDeliveryAuthorizationId = _GoodsDelivered.GoodsDeliveryAuthorizationId,
                            GoodsDeliveredId = _GoodsDeliveredq.GoodsDeliveredId,
                            CargadoId = 13,
                            Cargadoname = "Cargado",
                            UsuarioCreacion = _GoodsDelivered.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDelivered.UsuarioModificacion,
                            UnitOfMeasureId = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureId,
                            UnitOfMeasureName = _GoodsDelivered._GoodsDeliveredLine[0].UnitOfMeasureName,
                            WeightBallot = _GoodsDelivered.WeightBallot,
                        };

                        _context.BoletaDeSalida.Add(_boletadesalida);

                        await _context.SaveChangesAsync();


                        _GoodsDeliveredq.ExitTicket = _boletadesalida.BoletaDeSalidaId;

                        _context.GoodsDelivered.Add(_GoodsDeliveredq);
                        await _context.SaveChangesAsync();

                        foreach (var item in _GoodsDeliveredq._GoodsDeliveredLine)
                        {
                            List<Kardex> kardexAutorizaciones = _context.Kardex
                                .Where(q =>q.DocType==3 && q.DocumentId == item.NoAR).ToList();
                            //Genera Kardex de cada Autorizacion y Recibo de Mercadrias
                            foreach (var autorizacion in kardexAutorizaciones)
                            {
                                _context.Kardex.Add(new Kardex
                                {
                                    DocumentDate = _GoodsDeliveredq.DocumentDate,
                                    ProducId = _GoodsDeliveredq.ProductId,
                                    ProductName = _GoodsDeliveredq.ProductName,
                                    SubProducId = item.SubProductId,
                                    SubProductName = item.SubProductName,
                                    QuantityEntry = 0,
                                    QuantityOut = item.Quantity,
                                    BranchId = _GoodsDeliveredq.BranchId,
                                    BranchName = _GoodsDeliveredq.BranchName,
                                    WareHouseId = item.WareHouseId,
                                    WareHouseName = item.WareHouseName,
                                    UnitOfMeasureId = item.UnitOfMeasureId,
                                    UnitOfMeasureName = item.UnitOfMeasureName,
                                    TypeOperationId = TipoOperacion.Salida,
                                    TypeOperationName = "Salida",
                                    Total = item.Total,
                                    //TotalBags = Convert.ToDecimal(kardexAutorizaciones.TotalBags) - item.QuantitySacos,
                                   //9 QuantityOutCD = item.Quantity - (item.Quantity * _subproduct.Merma),
                                    //TotalCD = Convert.ToDecimal(kardexAutorizaciones.) - (item.Quantity - (item.Quantity * _subproduct.Merma)),
                                });
                            }
                           
                            
                        }

                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsDelivered.GoodsDeliveredId,
                            DocType = "GoodsDelivered",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDelivered, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsDelivered, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsDelivered.UsuarioCreacion,
                            UsuarioModificacion = _GoodsDelivered.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsDelivered.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                      

                        _boletadesalida.GoodsDeliveredId = _GoodsDeliveredq.GoodsDeliveredId;

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

            return await Task.Run(() => Ok(_GoodsDeliveredq));
        }

        /// <summary>
        /// Actualiza la GoodsDelivered
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsDelivered>> Update([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = _GoodsDelivered;
            try
            {
                _GoodsDeliveredq = await (from c in _context.GoodsDelivered
                                 .Where(q => q.GoodsDeliveredId == _GoodsDelivered.GoodsDeliveredId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsDeliveredq).CurrentValues.SetValues((_GoodsDelivered));

                //_context.GoodsDelivered.Update(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredq));
        }

        /// <summary>
        /// Elimina una GoodsDelivered       
        /// </summary>
        /// <param name="_GoodsDelivered"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsDelivered _GoodsDelivered)
        {
            GoodsDelivered _GoodsDeliveredq = new GoodsDelivered();
            try
            {
                _GoodsDeliveredq = _context.GoodsDelivered
                .Where(x => x.GoodsDeliveredId == (Int64)_GoodsDelivered.GoodsDeliveredId)
                .FirstOrDefault();

                _context.GoodsDelivered.Remove(_GoodsDeliveredq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsDeliveredq));

        }







    }
}