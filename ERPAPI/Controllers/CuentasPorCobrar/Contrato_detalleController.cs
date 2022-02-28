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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contrato_detalle")]
    [ApiController]
    public class Contrato_detalleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Contrato_detalleController(ILogger<Contrato_detalleController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: /<controller>/

        [HttpGet("[action]/{ContratoId}")]
        public async Task<ActionResult<Contrato_detalle>> GetContrato_detalleByContratoId(Int64 ContratoId)
        {
            List<Contrato_detalle> Items = new List<Contrato_detalle>();
            try
            {
                Items = await _context.Contrato_detalle.Where(q => q.ContratoId == ContratoId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Contrato_detalleId}")]
        public async Task<ActionResult<Contrato_detalle>> GetContrato_detalleById(Int64 Contrato_detalleId)
        {
            Contrato_detalle Items = new Contrato_detalle();
            try
            {
                Items = await _context.Contrato_detalle.Where(q => q.Contrato_detalleId == Contrato_detalleId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContrato_detallePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Contrato_detalle> Items = new List<Contrato_detalle>();
            try
            {
                var query = _context.Contrato_detalle.AsQueryable();
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
        /// Inserta una nueva Contrato_detalle
        /// </summary>
        /// <param name="_Contrato_detalle"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_detalle>> Insert([FromBody]Contrato_detalle _Contrato_detalle)
        {
            Contrato_detalle _Contrato_detalleq = new Contrato_detalle();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_detalleq = _Contrato_detalle;
                        _context.Contrato_detalle.Add(_Contrato_detalleq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_detalle.Contrato_detalleId,
                            DocType = "Contrato_detalle",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_detalle, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_detalle.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_detalle.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_detalle.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_detalleq));
        }

        /// <summary>
        /// Actualiza  Contrato_detalle
        /// </summary>
        /// <param name="_Contrato_detalle"></param>
        /// <returns></returns>
        [HttpPut("[action]")]

        public async Task<ActionResult<Contrato_detalle>> Update([FromBody]Contrato_detalle _Contrato_detalle)
        {
            Contrato_detalle _Contrato_detalleq = _Contrato_detalle;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contrato_detalleq = await (from c in _context.Contrato_detalle
                                         .Where(q => q.Contrato_detalleId == _Contrato_detalle.Contrato_detalleId)
                                                    select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Contrato_detalleq).CurrentValues.SetValues((_Contrato_detalle));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Contrato_detalle.Update(_Contrato_detalleq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato_detalle.Contrato_detalleId,
                            DocType = "Contrato_detalle",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_detalleq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_detalle, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato_detalle.UsuarioCreacion,
                            UsuarioModificacion = _Contrato_detalle.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato_detalle.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contrato_detalleq));
        }

        /// <summary>
        /// Elimina  Contrato_detalle       
        /// </summary>
        /// <param name="_Contrato_detalle"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato_detalle>> Delete([FromBody]Contrato_detalle _Contrato_detalle)
        {
            Contrato_detalle _Contrato_detalleq = new Contrato_detalle();
            try
            {
                _Contrato_detalleq = _context.Contrato_detalle
                .Where(x => x.Contrato_detalleId == (Int64)_Contrato_detalle.Contrato_detalleId)
                .FirstOrDefault();

                _context.Contrato_detalle.Remove(_Contrato_detalleq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contrato_detalleq));

        }

    }

}
