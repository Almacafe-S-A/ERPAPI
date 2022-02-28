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
    [Route("api/Periodo")]
    [ApiController]
    public class PeriodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PeriodoController(ILogger<PeriodoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPeriodoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Periodo> Items = new List<Periodo>();
            try
            {
                var query = _context.Periodo.AsQueryable();
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
        /// Obtiene el Listado de Periodoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Periodo>>> GetPeriodo()
        {
            List<Periodo> Items = new List<Periodo>();
            try
            {
                Items = await _context.Periodo.ToListAsync();
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
        /// Obtiene los Datos de la Periodo por medio del Id enviado.
        /// </summary>
        /// <param name="PeriodoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PeriodoId}")]
        public async Task<ActionResult<Periodo>> GetPeriodoById(Int64 PeriodoId)
        {
            Periodo Items = new Periodo();
            try
            {
                Items = await _context.Periodo.Where(q => q.Id == PeriodoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Periodo
        /// </summary>
        /// <param name="_Periodo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Periodo>> Insert([FromBody]Periodo _Periodo)
        {
            Periodo _Periodoq = new Periodo();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Periodoq = _Periodo;
                        _context.Periodo.Add(_Periodoq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Periodo.Id,
                            DocType = "Periodo",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Periodo, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Periodo.UsuarioCreacion,
                            UsuarioModificacion = _Periodo.UsuarioModificacion,
                            UsuarioEjecucion = _Periodo.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Periodoq));
        }

        /// <summary>
        /// Actualiza la Periodo
        /// </summary>
        /// <param name="_Periodo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Periodo>> Update(Periodo _Periodo)
        {
            Periodo _Periodoq = _Periodo;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Periodoq = await (from c in _context.Periodo
                                         .Where(q => q.Id == _Periodo.Id
                                         )
                                               select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Periodoq).CurrentValues.SetValues((_Periodo));

                        //_context.Periodo.Update(_Periodoq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Periodo.Id
                            ,
                            DocType = "Periodo",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Periodoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Periodo, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Periodo.UsuarioCreacion,
                            UsuarioModificacion = _Periodo.UsuarioModificacion,
                            UsuarioEjecucion = _Periodo.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Periodoq));
        }

        /// <summary>
        /// Elimina una Periodo       
        /// </summary>
        /// <param name="_Periodo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Periodo>> Delete([FromBody]Periodo _Periodo)
        {
            Periodo _Periodoq = new Periodo();
            try
            {
                _Periodoq = _context.Periodo
                .Where(x => x.Id == (Int64)_Periodo.Id)
                .FirstOrDefault();

                _context.Periodo.Remove(_Periodoq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Periodoq));

        }







    }
}