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
        /// Obtiene el Listado de PrecioCafees por Cliente
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<ActionResult<List<PrecioCafe>>> GetPrecioCafeByCustomer(int CustomerId)
        {
            List<PrecioCafe> Items = new List<PrecioCafe>();

            try
            {
                Items = await _context.PrecioCafe
                    .Where(q =>q.CustomerId== CustomerId)
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


        private PrecioCafe CalculoPrecioCafe(PrecioCafe _PrecioCafe) {
            ExchangeRate tasacambio = _context.ExchangeRate
                            .Where(q => q.ExchangeRateId == _PrecioCafe.ExchangeRateId)
                        .FirstOrDefault();
            //PrecioCafe _PrecioCafe = new PrecioCafe();

            _PrecioCafe.ExchangeRateValue = tasacambio.ExchangeRateValueCompra;
            _PrecioCafe.BrutoLPSIngreso = ((decimal)_PrecioCafe.PrecioBolsaUSD * (decimal)tasacambio.ExchangeRateValueCompra);
            _PrecioCafe.BrutoLPSConsumoInterno = ((decimal)_PrecioCafe.BrutoLPSIngreso * (decimal)0.6);
            _PrecioCafe.NetoLPSIngreso = ((decimal)_PrecioCafe.BrutoLPSIngreso * _PrecioCafe.PorcentajeIngreso / 100);
            _PrecioCafe.NetoLPSConsumoInterno = ((decimal)_PrecioCafe.BrutoLPSConsumoInterno * (_PrecioCafe.PorcentajeConsumoInterno / 100));
            _PrecioCafe.TotalLPSIngreso = ((decimal)_PrecioCafe.NetoLPSIngreso + _PrecioCafe.NetoLPSConsumoInterno);
            _PrecioCafe.TotalUSDEgreso = (_PrecioCafe.BeneficiadoUSD + _PrecioCafe.FideicomisoUSD + (decimal)_PrecioCafe.Otros
                + _PrecioCafe.UtilidadUSD + _PrecioCafe.PermisoExportacionUSD);
            _PrecioCafe.TotalLPSEgreso = ((decimal)tasacambio.ExchangeRateValueCompra * _PrecioCafe.TotalUSDEgreso);
            _PrecioCafe.PrecioQQOro = Decimal.Round(((decimal)_PrecioCafe.TotalLPSIngreso - (decimal)_PrecioCafe.TotalLPSEgreso) / 5, MidpointRounding.ToEven)*5;
            _PrecioCafe.PercioQQPergamino = Decimal.Round((decimal)_PrecioCafe.PrecioQQOro / (Convert.ToDecimal(1.25)) / 5, MidpointRounding.ToEven)*5;
            _PrecioCafe.PrecioQQCalidadesInferiores = Decimal.Round(((decimal) _PrecioCafe.PercioQQPergamino * (decimal)0.6)/5,MidpointRounding.ToEven)*5;

            return _PrecioCafe;
        }


        /// <summary>
        /// Inserta una nueva PrecioCafe
        /// </summary>
        /// <param name="_PrecioCafe"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PrecioCafe>> Insert([FromBody]PrecioCafe _PrecioCafe)
        {
            List<PrecioCafe> precios = _context.PrecioCafe.Where(q => q.CustomerId == _PrecioCafe.CustomerId &&
                                                q.Fecha.Date == _PrecioCafe.Fecha.Date).ToList();
            if (precios.Count > 0)
            {
                return BadRequest();
            }
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _PrecioCafe = CalculoPrecioCafe(_PrecioCafe);
                        _context.PrecioCafe.Add(_PrecioCafe);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


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

            return await Task.Run(() => Ok(_PrecioCafe));
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
            List<PrecioCafe> precios = _context.PrecioCafe.Where(q => q.CustomerId == _PrecioCafe.CustomerId &&
                                                q.Fecha.Date == _PrecioCafe.Fecha.Date &&
                                                q.Id != _PrecioCafe.Id).ToList();

            
            if (precios.Count > 0)
            {
                return BadRequest();
            }
            try
            {
                _PrecioCafeq = CalculoPrecioCafe(_PrecioCafe);               

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

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
               new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_PrecioCafeq));

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Tasacambio ([FromBody]ExchangeRate pBitacoraCierre)
        {

           
            ExchangeRate tasacambio = await _context.ExchangeRate
                            .Where(b => b.DayofRate >= DateTime.Now.AddDays(-1)).FirstOrDefaultAsync();

            if (tasacambio == null)//Revisa la tasa de cambio actualizada
            {
                return await Task.Run(() => BadRequest("Debe de Agregar una tasa de cambio actualizada"));
            }

            return await Task.Run(() => Ok(tasacambio));
        }







        }
}