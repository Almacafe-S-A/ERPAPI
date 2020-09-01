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
    [Route("api/DebitNoteLine")]
    [ApiController]
    public class DebitNoteLineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DebitNoteLineController(ILogger<DebitNoteLineController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de DebitNoteLine paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDebitNoteLinePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<DebitNoteLine> Items = new List<DebitNoteLine>();
            try
            {
                var query = _context.DebitNoteLine.AsQueryable();
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
        /// Obtiene el Listado de DebitNoteLinees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDebitNoteLine()
        {
            List<DebitNoteLine> Items = new List<DebitNoteLine>();
            try
            {
                Items = await _context.DebitNoteLine.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{DebitNoteId}")]
        public async Task<IActionResult> GetDebitNoteLineByDebitNoteId(Int64 DebitNoteId)
        {
            List<DebitNoteLine> Items = new List<DebitNoteLine>();
            try
            {
                Items = await _context.DebitNoteLine
                             .Where(q => q.DebitNoteId == DebitNoteId).ToListAsync();
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
        /// Obtiene los Datos de la DebitNoteLine por medio del Id enviado.
        /// </summary>
        /// <param name="DebitNoteLineId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{DebitNoteLineId}")]
        public async Task<IActionResult> GetDebitNoteLineById(Int64 DebitNoteLineId)
        {
            DebitNoteLine Items = new DebitNoteLine();
            try
            {
                Items = await _context.DebitNoteLine.Where(q => q.DebitNoteLineId == DebitNoteLineId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva DebitNoteLine
        /// </summary>
        /// <param name="_DebitNoteLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<DebitNoteLine>> Insert([FromBody]DebitNoteLine _DebitNoteLine)
        {
            DebitNoteLine _DebitNoteLineq = new DebitNoteLine();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _DebitNoteLineq = _DebitNoteLine;
                        _context.DebitNoteLine.Add(_DebitNoteLineq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _DebitNoteLine.DebitNoteLineId,
                            DocType = "DebitNoteLine",
                            ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(_DebitNoteLine, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_DebitNoteLine, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                           

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }

                }

                  
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteLineq));
        }

        /// <summary>
        /// Actualiza la DebitNoteLine
        /// </summary>
        /// <param name="_DebitNoteLine"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<DebitNoteLine>> Update([FromBody]DebitNoteLine _DebitNoteLine)
        {
            DebitNoteLine _DebitNoteLineq = _DebitNoteLine;
            try
            {
                _DebitNoteLineq = await (from c in _context.DebitNoteLine
                                 .Where(q => q.DebitNoteLineId == _DebitNoteLine.DebitNoteLineId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_DebitNoteLineq).CurrentValues.SetValues((_DebitNoteLine));

                //_context.DebitNoteLine.Update(_DebitNoteLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteLineq));
        }

        /// <summary>
        /// Elimina una DebitNoteLine       
        /// </summary>
        /// <param name="_DebitNoteLine"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]DebitNoteLine _DebitNoteLine)
        {
            DebitNoteLine _DebitNoteLineq = new DebitNoteLine();
            try
            {
                _DebitNoteLineq = _context.DebitNoteLine
                .Where(x => x.DebitNoteLineId == (Int64)_DebitNoteLine.DebitNoteLineId)
                .FirstOrDefault();

                _context.DebitNoteLine.Remove(_DebitNoteLineq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_DebitNoteLineq));

        }







    }
}