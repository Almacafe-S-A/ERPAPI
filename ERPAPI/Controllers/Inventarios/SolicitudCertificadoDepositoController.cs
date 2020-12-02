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
    [Route("api/SolicitudCertificadoDeposito")]
    [ApiController]
    public class SolicitudCertificadoDepositoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SolicitudCertificadoDepositoController(ILogger<SolicitudCertificadoDepositoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SolicitudCertificadoDeposito paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSolicitudCertificadoDepositoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<SolicitudCertificadoDeposito> Items = new List<SolicitudCertificadoDeposito>();
            try
            {
                var query = _context.SolicitudCertificadoDeposito.AsQueryable();
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
        /// Obtiene el Listado de SolicitudCertificadoDepositoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSolicitudCertificadoDeposito()
        {
            List<SolicitudCertificadoDeposito> Items = new List<SolicitudCertificadoDeposito>();
            try
            {
                Items = await _context.SolicitudCertificadoDeposito.ToListAsync();
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
        /// Obtiene los Datos de la SolicitudCertificadoDeposito por medio del Id enviado.
        /// </summary>
        /// <param name="SolicitudCertificadoDepositoId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{SolicitudCertificadoDepositoId}")]
        public async Task<IActionResult> GetSolicitudCertificadoDepositoById(Int64 SolicitudCertificadoDepositoId)
        {
            SolicitudCertificadoDeposito Items = new SolicitudCertificadoDeposito();
            try
            {
                Items = await _context.SolicitudCertificadoDeposito.Where(q => q.IdSCD == SolicitudCertificadoDepositoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva SolicitudCertificadoDeposito
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<SolicitudCertificadoDeposito>> Insert([FromBody]SolicitudCertificadoDepositoDTO _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = new SolicitudCertificadoDeposito();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _SolicitudCertificadoDepositoq = _SolicitudCertificadoDeposito;
                        _context.SolicitudCertificadoDeposito.Add(_SolicitudCertificadoDepositoq);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _SolicitudCertificadoDeposito._SolicitudCertificadoLine)
                        {
                            item.IdSCD = _SolicitudCertificadoDepositoq.IdSCD;
                            _context.SolicitudCertificadoLine.Add(item);
                        }

                        await _context.SaveChangesAsync();
                        foreach (var item in _SolicitudCertificadoDeposito.RecibosAsociados)
                        {
                            RecibosCertificado _recibocertificado =
                                new RecibosCertificado
                                {
                                    IdCD = _SolicitudCertificadoDepositoq.IdSCD,
                                    IdRecibo = item,
                                    productocantidadbultos = _SolicitudCertificadoDeposito.Quantitysum,
                                    productorecibolempiras = _SolicitudCertificadoDeposito.Total,
                                    // UnitMeasureId =_CertificadoDeposito.
                                };

                            _context.RecibosCertificado.Add(_recibocertificado);
                        }         


                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _SolicitudCertificadoDeposito.IdSCD,
                            DocType = "SolicitudCertificadoDeposito",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_SolicitudCertificadoDeposito, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_SolicitudCertificadoDeposito, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _SolicitudCertificadoDeposito.UsuarioCreacion,
                            UsuarioModificacion = _SolicitudCertificadoDeposito.UsuarioModificacion,
                            UsuarioEjecucion = _SolicitudCertificadoDeposito.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Commit();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoDepositoq));
        }

        /// <summary>
        /// Actualiza la SolicitudCertificadoDeposito
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<SolicitudCertificadoDeposito>> Update([FromBody]SolicitudCertificadoDeposito _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = _SolicitudCertificadoDeposito;
            try
            {
                _SolicitudCertificadoDepositoq = await (from c in _context.SolicitudCertificadoDeposito
                                 .Where(q => q.IdSCD == _SolicitudCertificadoDeposito.IdSCD)
                                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_SolicitudCertificadoDepositoq).CurrentValues.SetValues((_SolicitudCertificadoDeposito));

                //_context.SolicitudCertificadoDeposito.Update(_SolicitudCertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoDepositoq));
        }

        /// <summary>
        /// Elimina una SolicitudCertificadoDeposito       
        /// </summary>
        /// <param name="_SolicitudCertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]SolicitudCertificadoDeposito _SolicitudCertificadoDeposito)
        {
            SolicitudCertificadoDeposito _SolicitudCertificadoDepositoq = new SolicitudCertificadoDeposito();
            try
            {
                _SolicitudCertificadoDepositoq = _context.SolicitudCertificadoDeposito
                .Where(x => x.IdSCD == (Int64)_SolicitudCertificadoDeposito.IdSCD)
                .FirstOrDefault();

                _context.SolicitudCertificadoDeposito.Remove(_SolicitudCertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_SolicitudCertificadoDepositoq));

        }







    }
}