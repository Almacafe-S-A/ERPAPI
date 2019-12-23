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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contrato_movimientos")]
    [ApiController]
    public class Contrato_movimientosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Contrato_movimientosController(ILogger<Contrato_movimientosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: /<controller>/

        [HttpGet("[action]/{ContratoId}")]
        public async Task<ActionResult<Contrato_movimientos>> GetContrato_movimientosByContratoId(Int64 ContratoId)
        {
            List<Contrato_movimientos> Items = new List<Contrato_movimientos>();
            try
            {
                Items = await _context.Contrato_movimientos.Where(q => q.ContratoId == ContratoId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Contrato_movimientosId}")]
        public async Task<ActionResult<Contrato_movimientos>> GetContrato_movimientosById(Int64 Contrato_movimientosId)
        {
            Contrato_movimientos Items = new Contrato_movimientos();
            try
            {
                Items = await _context.Contrato_movimientos.Where(q => q.Contrato_movimientosId == Contrato_movimientosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContrato_movimientosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Contrato_movimientos> Items = new List<Contrato_movimientos>();
            try
            {
                var query = _context.Contrato_movimientos.AsQueryable();
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
        /// Inserta una nueva Contrato_movimientos
        /// </summary>
        /// <param name="_Contrato_movimientos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_movimientos>> Insert([FromBody]Contrato_movimientos _Contrato_movimientos)
        {
            Contrato_movimientos _Contrato_movimientosq = new Contrato_movimientos();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_movimientosq = _Contrato_movimientos;
                        _context.Contrato_movimientos.Add(_Contrato_movimientosq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_movimientos.Contrato_movimientosId,
                            DocType = "Contrato_movimientos",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_movimientos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_movimientos.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_movimientos.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_movimientos.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_movimientosq));
        }

        /// <summary>
        /// Actualiza  Contrato_movimientos
        /// </summary>
        /// <param name="_Contrato_movimientos"></param>
        /// <returns></returns>
        [HttpPut("[action]")]

        public async Task<ActionResult<Contrato_movimientos>> Update([FromBody]Contrato_movimientos _Contrato_movimientos)
        {
            Contrato_movimientos _Contrato_movimientosq = _Contrato_movimientos;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_movimientosq = await (from c in _context.Contrato_movimientos
                                         .Where(q => q.Contrato_movimientosId == _Contrato_movimientos.Contrato_movimientosId)
                                                        select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Contrato_movimientosq).CurrentValues.SetValues((_Contrato_movimientos));

                        //_context.Contrato_movimientos.Update(_Contrato_movimientosq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_movimientos.Contrato_movimientosId,
                            DocType = "Contrato_movimientos",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_movimientosq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_movimientos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_movimientos.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_movimientos.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_movimientos.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_movimientosq));
        }

        /// <summary>
        /// Elimina  Contrato_movimientos       
        /// </summary>
        /// <param name="_Contrato_movimientos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_movimientos>> Delete([FromBody]Contrato_movimientos _Contrato_movimientos)
        {
            Contrato_movimientos _Contrato_movimientosq = new Contrato_movimientos();
            try
            {
                _Contrato_movimientosq = _context.Contrato_movimientos
                .Where(x => x.Contrato_movimientosId == (Int64)_Contrato_movimientos.Contrato_movimientosId)
                .FirstOrDefault();

                _context.Contrato_movimientos.Remove(_Contrato_movimientosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contrato_movimientosq));

        }

    }

}
