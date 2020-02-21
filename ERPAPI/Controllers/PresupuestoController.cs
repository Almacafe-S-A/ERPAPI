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
                Items = await _context.Presupuesto.ToListAsync();
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
                        _Presupuestoq = _Presupuesto;
                        _context.Presupuesto.Add(_Presupuestoq);
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
                        _Presupuestoq = await (from c in _context.Presupuesto
                                         .Where(q => q.Id == _Presupuesto.Id
                                         )
                                         select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Presupuestoq).CurrentValues.SetValues((_Presupuesto));

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