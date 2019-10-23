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
    public class CierreContableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public CierreContableController(ILogger<CierreContableController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CierreContable paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCierreContablePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CierreContable> Items = new List<CierreContable>();
            try
            {
                var query = _context.CierreContable.AsQueryable();
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

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la Diarios en una lista.
        /// </summary>

        // GET: api/CierreContable
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCierreContable()

        {
            List<CierreContable> Items = new List<CierreContable>();
            try
            {
                Items = await _context.CierreContable.ToListAsync();
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
        /// Obtiene los Datos de la CierreContable por medio del Id enviado.
        /// </summary>
        /// <param name="CierreContableId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CierreContableId}")]
        public async Task<IActionResult> GetCierreContableById(Int64 CierreContableId)
        {
            CierreContable Items = new CierreContable();
            try
            {
                Items = await _context.CierreContable.Where(q => q.Id == CierreContableId).Include(q => q.CierreContableLineas).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Inserta una nueva CierreContable
        /// </summary>
        /// <param name="_CierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CierreContable>> Insert([FromBody]dynamic dto)
        //public async Task<ActionResult<CierreContable>> Insert([FromBody]CierreContable _CierreContable)
        {
            CierreContable _CierreContable = new CierreContable();
            CierreContable _CierreContableq = new CierreContable();
            try
            {
                _CierreContable = JsonConvert.DeserializeObject<CierreContable>(dto.ToString());
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _CierreContableq = _CierreContable;
                        _context.CierreContable.Add(_CierreContableq);
                        foreach (var item in _CierreContableq.CierreContableLineas)
                        {
                            item.IdBitacoracierreContable = _CierreContableq.Id;
                            // item.CierreContableLineId = 0;
                            _context.CierreContableLinea.Add(item);
                        }

                        

                        await _context.SaveChangesAsync();



                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CierreContable.Id,
                            DocType = "CierreContable",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_CierreContable, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CierreContable.UsuarioCreacion,
                            UsuarioModificacion = _CierreContable.UsuarioModificacion,
                            UsuarioEjecucion = _CierreContable.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CierreContableq));
        }

        /// <summary>
        /// Actualiza la CierreContable
        /// </summary>
        /// <param name="_CierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        // public async Task<ActionResult<CierreContable>> Update([FromBody]CierreContable _CierreContable)
        public async Task<ActionResult<CierreContable>> Update([FromBody]dynamic dto)
        {
            //CierreContable _CierreContableq = _CierreContable;
            CierreContable _CierreContable = new CierreContable();
            CierreContable _CierreContableq = new CierreContable();
            try
            {
                _CierreContable = JsonConvert.DeserializeObject<CierreContable>(dto.ToString());
                _CierreContableq = await (from c in _context.CierreContable
                                 .Where(q => q.Id == _CierreContable.Id)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CierreContableq).CurrentValues.SetValues((_CierreContable));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CierreContableq));
        }

        /// <summary>
        /// Elimina una CierreContable       
        /// </summary>
        /// <param name="_CierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CierreContable _CierreContable)
        {
            CierreContable _CierreContableq = new CierreContable();
            try
            {
                _CierreContableq = _context.CierreContable
                .Where(x => x.Id == (Int64)_CierreContable.Id)
                .FirstOrDefault();

                _context.CierreContable.Remove(_CierreContableq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CierreContableq));

        }


    }
}
