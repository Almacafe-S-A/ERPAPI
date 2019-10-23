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
    public class CierreContableLineaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CierreContableLineaController(ILogger<CierreContableLineaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CierreContableLinea paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCierreContableLineaPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CierreContableLinea> Items = new List<CierreContableLinea>();
            try
            {
                var query = _context.CierreContableLinea.AsQueryable();
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
        /// Obtiene el Listado de CierreContableLineaes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCierreContableLinea()
        {
            List<CierreContableLinea> Items = new List<CierreContableLinea>();
            try
            {
                Items = await _context.CierreContableLinea.ToListAsync();
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
        /// Obtiene los Datos de la CierreContableLinea por medio del Id enviado.
        /// </summary>
        /// <param name="CierreContableLineaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CierreContableLineaId}")]
        public async Task<IActionResult> GetCierreContableLineaById(Int64 CierreContableLineaId)
        {
            CierreContableLinea Items = new CierreContableLinea();
            try
            {
                Items = await _context.CierreContableLinea.Where(q => q.IdLinea == CierreContableLineaId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CierreContableLinea
        /// </summary>
        /// <param name="_CierreContableLinea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CierreContableLinea>> Insert([FromBody]CierreContableLinea _CierreContableLinea)
        {
            CierreContableLinea _CierreContableLineaq = new CierreContableLinea();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CierreContableLineaq = _CierreContableLinea;
                        _context.CierreContableLinea.Add(_CierreContableLineaq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CierreContableLinea.IdLinea,
                            DocType = "CierreContableLinea",
                            ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(_CierreContableLinea, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_CierreContableLinea, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
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

            return await Task.Run(() => Ok(_CierreContableLineaq));
        }

        /// <summary>
        /// Actualiza la CierreContableLinea
        /// </summary>
        /// <param name="_CierreContableLinea"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CierreContableLinea>> Update([FromBody]CierreContableLinea _CierreContableLinea)
        {
            CierreContableLinea _CierreContableLineaq = _CierreContableLinea;
            try
            {
                _CierreContableLineaq = await (from c in _context.CierreContableLinea
                                 .Where(q => q.IdLinea == _CierreContableLinea.IdLinea)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CierreContableLineaq).CurrentValues.SetValues((_CierreContableLinea));

                //_context.CierreContableLinea.Update(_CierreContableLineaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CierreContableLineaq));
        }

        /// <summary>
        /// Elimina una CierreContableLinea       
        /// </summary>
        /// <param name="_CierreContableLinea"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CierreContableLinea _CierreContableLinea)
        {
            CierreContableLinea _CierreContableLineaq = new CierreContableLinea();
            try
            {
                _CierreContableLineaq = _context.CierreContableLinea
                .Where(x => x.IdLinea == (Int64)_CierreContableLinea.IdLinea)
                .FirstOrDefault();

                _context.CierreContableLinea.Remove(_CierreContableLineaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CierreContableLineaq));

        }





    }
}
