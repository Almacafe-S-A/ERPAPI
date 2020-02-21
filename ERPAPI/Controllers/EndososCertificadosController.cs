using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/EndososCertificados")]
    [ApiController]
    public class EndososCertificadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public EndososCertificadosController(ILogger<EndososCertificadosController> logger, IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificados paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                var query = _context.EndososCertificados.AsQueryable();
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
        /// Obtiene el Listado de EndososCertificadoses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificados()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }


        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetEndososCertificadosByIdCD(Int64 IdCD)
        {
            EndososCertificados Items = new EndososCertificados();
            try
            {
                Items = await _context.EndososCertificados
                    .Where(q=>q.IdCD == IdCD).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetEndososSaldoByLineByIdCD(EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososLiberacion Items = new EndososLiberacion();
            try
            {
                Items = await _context.EndososLiberacion
                         .OrderByDescending(q=>q.EndososLiberacionId)
                         .Where(q => q.EndososLineId == _EndososCertificadosLine.EndososCertificadosLineId)
                         .FirstOrDefaultAsync();

                EndososCertificadosLine _endosoline = await
                           _context.EndososCertificadosLine
                           .Where(q => q.EndososCertificadosLineId == _EndososCertificadosLine.EndososCertificadosLineId).FirstOrDefaultAsync();

                if (Items == null)
                {
                    Items = new EndososLiberacion { EndososId=0, EndososLineId=0, Saldo = _endosoline.Quantity };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosByCustomer()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.ToListAsync();
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
        /// Obtiene los Datos de la EndososCertificados por medio del Id enviado.
        /// </summary>
        /// <param name="EndososCertificadosId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososCertificadosId}")]
        public async Task<IActionResult> GetEndososCertificadosById(Int64 EndososCertificadosId)
        {
            EndososCertificados Items = new EndososCertificados();
            try
            {
                Items = await _context.EndososCertificados.Where(q => q.EndososCertificadosId == EndososCertificadosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }
       



        /// <summary>
        /// Inserta una nueva EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososCertificados>> Insert([FromBody]EndososDTO _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EndososCertificadosq = _EndososCertificados;

                        _context.EndososCertificados.Add(_EndososCertificadosq);

                        foreach (var item in _EndososCertificadosq.EndososCertificadosLine)
                        {
                            item.EndososCertificadosId = _EndososCertificados.EndososCertificadosId;
                            _context.EndososCertificadosLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EndososCertificadosq.EndososCertificadosId,
                            DocType = "EndososCertificados",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_EndososCertificados, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_EndososCertificadosq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EndososCertificadosq.UsuarioCreacion,
                            UsuarioModificacion = _EndososCertificadosq.UsuarioModificacion,
                            UsuarioEjecucion = _EndososCertificadosq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                  
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Actualiza la EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososCertificados>> Update([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = _EndososCertificados;
            try
            {
                _EndososCertificadosq = await (from c in _context.EndososCertificados
                                 .Where(q => q.EndososCertificadosId == _EndososCertificados.EndososCertificadosId)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_EndososCertificadosq).CurrentValues.SetValues((_EndososCertificados));

                //_context.EndososCertificados.Update(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Elimina una EndososCertificados       
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                _EndososCertificadosq = _context.EndososCertificados
                .Where(x => x.EndososCertificadosId == (Int64)_EndososCertificados.EndososCertificadosId)
                .FirstOrDefault();

                _context.EndososCertificados.Remove(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);

        }







    }
}