using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    [Route("api/Presupuesto")]
    [ApiController]
    public class PresupuestoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PresupuestoController(ILogger<PresupuestoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPresupuestoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Presupuesto> Items = new List<Presupuesto>();
            try
            {
                var query = _context.Presupuesto.AsQueryable();
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
        /// Obtiene el Listado de Presupuestoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Presupuesto>>> GetPresupuesto()
        {
            List<Presupuesto> Items = new List<Presupuesto>();
            try
            {
                Items = await _context.Presupuesto.Include("Accounting").ToListAsync();
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
        /// Obtiene el Listado de Presupuestoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{periodoId}/{centrocosto}")]
        public async Task<ActionResult<List<Presupuesto>>> GetPreuspuestosByPeriodo(int periodoId,int centrocosto)
        {
            List<Presupuesto> Items = new List<Presupuesto>();
            try
            {
                Items = await _context.Presupuesto
                    .Where(a =>a.PeriodoId == periodoId 
                    && (a.CostCenterId == centrocosto||centrocosto==0))
                    .Include("Accounting").ToListAsync();

                Items = (from c in Items
                         select new Presupuesto
                         {
                             AccountCode = c.Accounting.AccountCode,
                             AccountingName = c.Accounting.AccountName,
                             AccountigId = c.AccountigId,
                             PresupuestoEnero = c.PresupuestoEnero,
                             PresupuestoFebrero = c.PresupuestoFebrero,
                             PresupuestoMarzo = c.PresupuestoMarzo,
                             PresupuestoAbril = c.PresupuestoAbril,
                             PresupuestoMayo = c.PresupuestoMayo,
                             PresupuestoJunio = c.PresupuestoJunio,
                             PresupuestoJulio = c.PresupuestoJulio,
                             PresupuestoAgosto = c.PresupuestoAgosto,
                             PresupuestoSeptiembre = c.PresupuestoSeptiembre,
                             PresupuestoOctubre = c.PresupuestoOctubre,
                             PresupuestoNoviembre = c.PresupuestoNoviembre,
                             PresupuestoDiciembre = c.PresupuestoDiciembre,
                             TotalMontoPresupuesto = c.TotalMontoPresupuesto,
                             EjecucionEnero = c.EjecucionEnero,
                             EjecucionFebrero = c.EjecucionFebrero,
                             EjecucionMarzo = c.EjecucionMarzo,
                             EjecucionAbril = c.EjecucionAbril,
                             EjecucionMayo = c.EjecucionMayo,
                             EjecucionJunio = c.EjecucionJunio,
                             EjecucionJulio = c.EjecucionJulio,
                             EjecucionAgosto = c.EjecucionAgosto,
                             EjecucionSeptiembre = c.EjecucionSeptiembre,
                             EjecucionOctubre = c.EjecucionOctubre,
                             EjecucionNoviembre = c.EjecucionNoviembre,
                             EjecucionDiciembre = c.EjecucionDiciembre,
                             TotalMontoEjecucion = c.TotalMontoEjecucion
                         }
               ).ToList();
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
        /// Obtiene los Datos de la Presupuesto por medio del Id enviado.
        /// </summary>
        /// <param name="PresupuestoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PresupuestoId}")]
        public async Task<ActionResult<Presupuesto>> GetPresupuestoById(Int64 PresupuestoId)
        {
            Presupuesto Items = new Presupuesto();
            try
            {
                Items = await _context.Presupuesto.Where(q => q.Id == PresupuestoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Presupuesto
        /// </summary>
        /// <param name="_Presupuesto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Presupuesto>> Insert([FromBody]Presupuesto _Presupuesto)
        {
            Presupuesto _Presupuestoq = new Presupuesto();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        

                        Accounting _Presupuestop = await (from c in _context.Accounting
                                         .Where(q => q.AccountId == _Presupuesto.AccountigId
                                         )
                                                           select c
                                        ).FirstOrDefaultAsync();
                        _Presupuesto.AccountName = _Presupuestop.AccountName;

                        _Presupuesto.TotalMontoPresupuesto = (_Presupuesto.PresupuestoEnero + _Presupuesto.PresupuestoFebrero +
                            _Presupuesto.PresupuestoMarzo + _Presupuesto.PresupuestoAbril + _Presupuesto.PresupuestoMayo +
                            _Presupuesto.PresupuestoJunio + _Presupuesto.PresupuestoJulio + _Presupuesto.PresupuestoAgosto +
                           _Presupuesto.PresupuestoSeptiembre + _Presupuesto.PresupuestoOctubre + _Presupuesto.PresupuestoNoviembre +
                           _Presupuesto.PresupuestoDiciembre);

                        _Presupuesto.TotalMontoEjecucion = (_Presupuesto.EjecucionEnero + _Presupuesto.EjecucionFebrero +
                           _Presupuesto.EjecucionMarzo + _Presupuesto.EjecucionAbril + _Presupuesto.EjecucionMayo +
                           _Presupuesto.EjecucionJunio + _Presupuesto.EjecucionJulio + _Presupuesto.EjecucionAgosto +
                          _Presupuesto.EjecucionSeptiembre + _Presupuesto.EjecucionOctubre + _Presupuesto.EjecucionNoviembre +
                          _Presupuesto.EjecucionDiciembre);

                        _Presupuestoq = _Presupuesto;
                        _context.Presupuesto.Add(_Presupuestoq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Presupuesto.Id,
                            DocType = "Presupuesto",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Presupuesto, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Presupuesto.UsuarioCreacion,
                            UsuarioModificacion = _Presupuesto.UsuarioModificacion,
                            UsuarioEjecucion = _Presupuesto.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                        if (ex.InnerException is SqlException sqlException && sqlException.Message.Contains("Cannot insert duplicate key"))
                        {
                            return BadRequest("Ya existe una bodega registrada con este nombre en esta Sucursal.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Presupuestoq));
        }

        /// <summary>
        /// Actualiza la Presupuesto
        /// </summary>
        /// <param name="_Presupuesto"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Presupuesto>> Update(Presupuesto _Presupuesto)
        {
            Presupuesto _Presupuestoq = _Presupuesto;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        Presupuesto _Presupuestop = await (from c in _context.Presupuesto
                                         .Where(q => q.Id == _Presupuesto.Id
                                         )
                                               select c
                                        ).FirstOrDefaultAsync();
                        _Presupuesto.CostCenterId = _Presupuestop.CostCenterId;
                        _Presupuesto.PeriodoId = _Presupuestop.PeriodoId;


                        _Presupuesto.TotalMontoPresupuesto = (_Presupuesto.PresupuestoEnero + _Presupuesto.PresupuestoFebrero +
                          _Presupuesto.PresupuestoMarzo + _Presupuesto.PresupuestoAbril + _Presupuesto.PresupuestoMayo +
                          _Presupuesto.PresupuestoJunio + _Presupuesto.PresupuestoJulio + _Presupuesto.PresupuestoAgosto +
                         _Presupuesto.PresupuestoSeptiembre + _Presupuesto.PresupuestoOctubre + _Presupuesto.PresupuestoNoviembre +
                         _Presupuesto.PresupuestoDiciembre);

                        _Presupuesto.TotalMontoEjecucion = (_Presupuesto.EjecucionEnero + _Presupuesto.EjecucionFebrero +
                           _Presupuesto.EjecucionMarzo + _Presupuesto.EjecucionAbril + _Presupuesto.EjecucionMayo +
                           _Presupuesto.EjecucionJunio + _Presupuesto.EjecucionJulio + _Presupuesto.EjecucionAgosto +
                          _Presupuesto.EjecucionSeptiembre + _Presupuesto.EjecucionOctubre + _Presupuesto.EjecucionNoviembre +
                          _Presupuesto.EjecucionDiciembre);


                        _Presupuestoq = await (from c in _context.Presupuesto
                                         .Where(q => q.Id == _Presupuesto.Id
                                         )
                                         select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Presupuestoq).CurrentValues.SetValues((_Presupuesto));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Presupuesto.Update(_Presupuestoq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Presupuesto.Id
                            ,
                            DocType = "Presupuesto",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Presupuestoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Presupuesto, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Presupuesto.UsuarioCreacion,
                            UsuarioModificacion = _Presupuesto.UsuarioModificacion,
                            UsuarioEjecucion = _Presupuesto.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Presupuestoq));
        }

        /// <summary>
        /// Elimina una Presupuesto       
        /// </summary>
        /// <param name="_Presupuesto"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Presupuesto>> Delete([FromBody]Presupuesto _Presupuesto)
        {
            Presupuesto _Presupuestoq = new Presupuesto();
            try
            {
                _Presupuestoq = _context.Presupuesto
                .Where(x => x.Id == (Int64)_Presupuesto.Id)
                .FirstOrDefault();

                _context.Presupuesto.Remove(_Presupuestoq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Presupuestoq));

        }







    }
}