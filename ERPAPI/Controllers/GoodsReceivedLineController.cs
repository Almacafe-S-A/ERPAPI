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
    [Route("api/GoodsReceivedLine")]
    [ApiController]
    public class GoodsReceivedLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsReceivedLineController(ILogger<GoodsReceivedLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsReceivedLinees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedLine()
        {
            List<GoodsReceivedLine> Items = new List<GoodsReceivedLine>();
            try
            {
                Items = await _context.GoodsReceivedLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la GoodsReceivedLine por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsReceivedLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsReceivedLineId}")]
        public async Task<IActionResult> GetGoodsReceivedLineById(Int64 GoodsReceivedLineId)
        {
            GoodsReceivedLine Items = new GoodsReceivedLine();
            try
            {
                Items = await _context.GoodsReceivedLine.Where(q => q.GoodsReceiveLinedId == GoodsReceivedLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }

        
        /// <summary>
        /// Obtiene el detalle de las mercaderias.
        /// </summary>
        /// <param name="GoodsReceivedId"></param>
        /// <returns></returns>
       [HttpGet("[action]/{GoodsReceivedId}")]
        public async Task<IActionResult> GetGoodsReceivedLineByGoodsReceivedId(Int64 GoodsReceivedId)
        {
            List<GoodsReceivedLine> Items = new List<GoodsReceivedLine>();
            try
            {
                Items = await _context.GoodsReceivedLine
                             .Where(q => q.GoodsReceivedId == GoodsReceivedId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{SubProductId}")]
        public async Task<ActionResult<SubProduct>> GetSubProductById(Int64 SubProductId)
        {
            SubProduct Items = new SubProduct();
            try
            {
                Items = await _context.SubProduct.Where(q => q.SubproductId == SubProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Alert>> InsertAlert([FromBody]Alert _Alert)
        {
            Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Alertq = _Alert;
                        _context.Alert.Add(_Alertq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Alert.AlertId,
                            DocType = "Alert",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Alert, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Alert.UsuarioCreacion,
                            UsuarioModificacion = _Alert.UsuarioModificacion,
                            UsuarioEjecucion = _Alert.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Alertq));
        }
        /// <summary>
        /// Inserta una nueva GoodsReceivedLine
        /// </summary>
        /// <param name="_GoodsReceivedLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<GoodsReceivedLine>> Insert([FromBody]GoodsReceivedLine _GoodsReceivedLine)
        {
            GoodsReceivedLine _GoodsReceivedLineq = new GoodsReceivedLine();
            try
            {
                SubProduct Subproducto = new SubProduct();
                var Subproducto01 = await GetSubProductById(_GoodsReceivedLine.SubProductId);
                Subproducto = ((SubProduct)Subproducto01.Value);
                if (Subproducto.ProductTypeId == 3)
                {
                    Alert AlertP = new Alert();
                    Alert Alerta = new Alert();
                    Alerta.DocumentId = _GoodsReceivedLine.SubProductId;
                    Alerta.DocumentName = "LISTA PROHIBIDA";
                    Alerta.AlertName = "Productos";
                    Alerta.Code = "PRODUCT01";
                    Alerta.DescriptionAlert = "Lista de producto Prohibida";
                    Alerta.FechaCreacion = Convert.ToDateTime(_GoodsReceivedLine.FechaCreacion);
                    Alerta.FechaModificacion = Convert.ToDateTime(_GoodsReceivedLine.FechaModificacion);
                    Alerta.UsuarioCreacion = _GoodsReceivedLine.UsuarioCreacion;
                    Alerta.UsuarioModificacion = _GoodsReceivedLine.UsuarioModificacion;
                    var AlertaP = await InsertAlert(Alerta);
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsReceivedLineq = _GoodsReceivedLine;
                        _context.GoodsReceivedLine.Add(_GoodsReceivedLineq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsReceivedLine.GoodsReceiveLinedId,
                            DocType = "GoodsReceivedLine",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLineq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsReceivedLineq.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceivedLineq.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsReceivedLineq.UsuarioModificacion,

                        });


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

            return await Task.Run(() => Ok(_GoodsReceivedLineq));
        }

        /// <summary>
        /// Actualiza la GoodsReceivedLine
        /// </summary>
        /// <param name="_GoodsReceivedLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsReceivedLine>> Update([FromBody]GoodsReceivedLine _GoodsReceivedLine)
        {
            GoodsReceivedLine _GoodsReceivedLineq = _GoodsReceivedLine;
            try
            {
                SubProduct Subproducto = new SubProduct();

                var Subproducto01 = await GetSubProductById(_GoodsReceivedLine.SubProductId);
                Subproducto = ((SubProduct)Subproducto01.Value);
                if (Subproducto.ProductTypeId == 3)
                {
                    Alert AlertP = new Alert();
                    Alert Alerta = new Alert();
                    Alerta.DocumentId = _GoodsReceivedLine.SubProductId;
                    Alerta.DocumentName = "LISTA PROHIBIDA";
                    Alerta.AlertName = "Productos";
                    Alerta.Code = "PRODUCT01";
                    Alerta.DescriptionAlert = "Lista de producto Prohibida";
                    Alerta.FechaCreacion = Convert.ToDateTime(_GoodsReceivedLine.FechaCreacion);
                    Alerta.FechaModificacion = Convert.ToDateTime(_GoodsReceivedLine.FechaModificacion);
                    Alerta.UsuarioCreacion = _GoodsReceivedLine.UsuarioCreacion;
                    Alerta.UsuarioModificacion = _GoodsReceivedLine.UsuarioModificacion;
                    var AlertaP = await InsertAlert(Alerta);
                }
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsReceivedLineq = _GoodsReceivedLine;
                        _context.GoodsReceivedLine.Add(_GoodsReceivedLineq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsReceivedLine.GoodsReceiveLinedId,
                            DocType = "GoodsReceivedLine",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLineq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsReceivedLineq.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceivedLineq.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsReceivedLineq.UsuarioModificacion,

                        });


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }



                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _GoodsReceivedLineq = await (from c in _context.GoodsReceivedLine
                             .Where(q => q.GoodsReceiveLinedId == _GoodsReceivedLine.GoodsReceiveLinedId)
                                                     select c
                                        ).FirstOrDefaultAsync();


                        _context.Entry(_GoodsReceivedLineq).CurrentValues.SetValues((_GoodsReceivedLine));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _GoodsReceivedLineq.GoodsReceiveLinedId,
                            DocType = "GoodsReceivedLine",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLineq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLine, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Deletw",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _GoodsReceivedLine.UsuarioCreacion,
                            UsuarioModificacion = _GoodsReceivedLine.UsuarioModificacion,
                            UsuarioEjecucion = _GoodsReceivedLineq.UsuarioModificacion,

                        });



                        //_context.GoodsReceivedLine.Update(_GoodsReceivedLineq);
                        await _context.SaveChangesAsync();
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

            return await Task.Run(() => Ok(_GoodsReceivedLineq));
        }

        /// <summary>
        /// Elimina una GoodsReceivedLine       
        /// </summary>
        /// <param name="_GoodsReceivedLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsReceivedLine _GoodsReceivedLine)
        {
            GoodsReceivedLine _GoodsReceivedLineq = new GoodsReceivedLine();
            try
            {
                
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            _GoodsReceivedLineq = await (from c in _context.GoodsReceivedLine
                                 .Where(q => q.GoodsReceiveLinedId == _GoodsReceivedLine.GoodsReceiveLinedId)
                                             select c
                                            ).FirstOrDefaultAsync();
                        /*     = _context.GoodsReceivedLine
              .Where(x => x.GoodsReceiveLinedId == (Int64)_GoodsReceivedLine.GoodsReceiveLinedId)
              .FirstOrDefault();
              */
                            _context.Entry(_GoodsReceivedLineq).CurrentValues.SetValues((_GoodsReceivedLine));

                            //_context.Alert.Update(_Alertq);
                            await _context.SaveChangesAsync();
                            BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                            {
                                IdOperacion = _GoodsReceivedLineq.GoodsReceiveLinedId,
                                DocType = "GoodsReceivedLine",
                                ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLineq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_GoodsReceivedLine, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                Accion = "Deletw",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = _GoodsReceivedLine.UsuarioCreacion,
                                UsuarioModificacion = _GoodsReceivedLine.UsuarioModificacion,
                                UsuarioEjecucion = _GoodsReceivedLineq.UsuarioModificacion,

                            });


                          
                                 _context.GoodsReceivedLine.Remove(_GoodsReceivedLineq);
                                    await _context.SaveChangesAsync();

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

            return await Task.Run(() => Ok(_GoodsReceivedLineq));

        }







    }
}