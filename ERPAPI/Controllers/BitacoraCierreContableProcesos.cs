using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraCierreProcesosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BitacoraCierreProcesosController(ILogger<BitacoraCierreProcesosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de BitacoraCierreProcesos paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreProcesosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<BitacoraCierreProcesos> Items = new List<BitacoraCierreProcesos>();
            try 
            {
                var query = _context.BitacoraCierreProceso.AsQueryable();
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
        /// Obtiene los roles asignados a los usuarios
        /// </summary>
        /// <returns></returns> 
        [HttpGet("[action]")]
        public async Task<ActionResult<List<BitacoraCierreProcesos>>> GetBitacoraCierreProceso()
        {
            List<BitacoraCierreProcesos> _cierre = new List<BitacoraCierreProcesos>();
            try
            {
                _cierre = await (_context.BitacoraCierreProceso.ToListAsync());
                // _users = mapper.Map<,ApplicationUserRole>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

            return await Task.Run(() => _cierre);
        }


        /// <summary>
        /// Obtiene los roles asignados a los usuarios
        /// </summary>
        /// <returns></returns> 
        [HttpGet("[action]")]
        public async Task<ActionResult<List<BitacoraCierreContable>>> GetBitacorasProcesos()
        {
            List<BitacoraCierreContable> _cierre = new List<BitacoraCierreContable>();
            try
            {
                _cierre = await (_context.BitacoraCierreContable.ToListAsync());
                // _users = mapper.Map<,ApplicationUserRole>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }
            return await Task.Run(() => _cierre);
        }


        /// <summary>
        /// Obtiene el Listado de BitacoraCierreProcesoses 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreProcesos()
        {
            List<BitacoraCierreProcesos> Items = new List<BitacoraCierreProcesos>();
            try
            {
                Items = await _context.BitacoraCierreProceso.ToListAsync();
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
        /// Obtiene los Datos de la BitacoraCierreProcesos por medio del Id enviado.
        /// </summary>
        /// <param name="BitacoraCierreProcesosId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BitacoraCierreProcesosId}")]
        public async Task<IActionResult> GetBitacoraCierreProcesosById(Int64 BitacoraCierreProcesosId)
        {
            BitacoraCierreProcesos Items = new BitacoraCierreProcesos();
            try
            {
                Items = await _context.BitacoraCierreProceso.Where(q => q.IdProceso == BitacoraCierreProcesosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva BitacoraCierreProcesos
        /// </summary>
        /// <param name="_BitacoraCierreProcesos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BitacoraCierreProcesos>> Insert([FromBody]BitacoraCierreProcesos _BitacoraCierreProcesos)
        {
            BitacoraCierreProcesos _BitacoraCierreProcesosq = new BitacoraCierreProcesos();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _BitacoraCierreProcesosq = _BitacoraCierreProcesos;
                        _context.BitacoraCierreProceso.Add(_BitacoraCierreProcesosq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _BitacoraCierreProcesos.IdProceso,
                            DocType = "BitacoraCierreProcesos",
                            ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(_BitacoraCierreProcesos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_BitacoraCierreProcesos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
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

            return await Task.Run(() => Ok(_BitacoraCierreProcesosq));
        }

        /// <summary>
        /// Actualiza la BitacoraCierreProcesos
        /// </summary>
        /// <param name="_BitacoraCierreProcesos"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<BitacoraCierreProcesos>> Update([FromBody]BitacoraCierreProcesos _BitacoraCierreProcesos)
        {
            BitacoraCierreProcesos _BitacoraCierreProcesosq = _BitacoraCierreProcesos;
            try
            {
                _BitacoraCierreProcesosq = await (from c in _context.BitacoraCierreProceso
                                 .Where(q => q.IdProceso == _BitacoraCierreProcesos.IdProceso)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_BitacoraCierreProcesosq).CurrentValues.SetValues((_BitacoraCierreProcesos));

                //_context.BitacoraCierreProcesos.Update(_BitacoraCierreProcesosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_BitacoraCierreProcesosq));
        }

        /// <summary>
        /// Elimina una BitacoraCierreProcesos       
        /// </summary>
        /// <param name="_BitacoraCierreProcesos"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]BitacoraCierreProcesos _BitacoraCierreProcesos)
        {
            BitacoraCierreProcesos _BitacoraCierreProcesosq = new BitacoraCierreProcesos();
            try
            {
                _BitacoraCierreProcesosq = _context.BitacoraCierreProceso
                .Where(x => x.IdProceso == (Int64)_BitacoraCierreProcesos.IdProceso)
                .FirstOrDefault();

                _context.BitacoraCierreProceso.Remove(_BitacoraCierreProcesosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_BitacoraCierreProcesosq));

        }





    }
}
