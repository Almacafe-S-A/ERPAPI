using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Boleto_Ent")]
    [ApiController]
    public class Boleto_EntController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Boleto_EntController(ILogger<Boleto_EntController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Boleto_Entes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_Ent()
        {
            List<Boleto_Ent> Items = new List<Boleto_Ent>();
            try
            {
                Items = await _context.Boleto_Ent.ToListAsync();
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
        public async Task<IActionResult> GetBoleto_EntPag(int numeroDePagina=1,int cantidadDeRegistros=3500)
        {
            List<Boleto_Ent> Items = new List<Boleto_Ent>();
            try
            {



                var query = (from c in _context.Boleto_Ent
                             join d in _context.Boleto_Sal on  c.clave_e   equals d.clave_e into ba
                             from e in ba.DefaultIfEmpty()
                             select new Boleto_Ent {
                                 clave_e = c.clave_e,
                                 bascula_e = c.bascula_e,
                                 clave_C = c.clave_C,
                                 clave_o = c.clave_o,
                                 clave_p = c.clave_p,
                                 clave_u = c.clave_u,
                                 fecha_e = c.fecha_e,
                                 completo = c.completo,
                                 hora_e = c.hora_e,
                                 conductor = c.conductor,
                                 nombre_oe = c.nombre_oe,
                                 observa_e = c.observa_e,
                                 peso_e = c.peso_e,
                                 placas = c.placas,
                                 turno_oe = c.turno_oe,
                                 t_entrada = c.t_entrada,
                                 unidad_e = c.unidad_e,
                                 Boleto_Sal = e
                               //  Boleto_Sal =  _context.Boleto_Sal.Where(q => q.clave_e == c.clave_e).FirstOrDefault(),

                            } ) .AsQueryable();

                var totalRegistro = query.Count();

                Items = await query                       
                    .OrderByDescending(q=>q.clave_e)
                         .Include(q=>q.Boleto_Sal)
                             .Skip(cantidadDeRegistros*(numeroDePagina-1))
                                      .Take(cantidadDeRegistros)
                                     .ToListAsync();

                 
                         

                //foreach (var item in Items)
                //{
                //   item.Boleto_Sal =  await _context.Boleto_Sal.Where(q => q.clave_e == item.clave_e).FirstOrDefaultAsync();
                //}
                

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();



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
        public async Task<IActionResult> GetBoleto_EntMax()
        {
            Int64 Max = 0;
            try
            {
                //Max = await _context.Boleto_Ent.Select(x => x.clave_e).DefaultIfEmpty(0).Max();
                Max =  _context.Boleto_Ent.Max(x => x.clave_e);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Max));
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int64>> GetBoleto_EntCount()
        {
            // List<Boleto_Ent> Items = new List<Boleto_Ent>();
            Boleto_Ent _Boleto_Ent = new Boleto_Ent();
            Int64 Total = 0;
            try
            {

               Total = await _context.Boleto_Ent.CountAsync();

                //Items = await _context.Boleto_Ent.ToListAsync();
                //_Boleto_Ent = await _context.Boleto_Ent.FromSql("select  count(clave_e) clave_e  from Boleto_Ent ").FirstOrDefaultAsync();
                //_Boleto_Ent = await _context.Query<Boleto_Ent>()
                // .FromSql($"SELECT count(clave_e) as clave_e FROM dbo.Boleto_Ent ")
                //  .AsNoTracking()
                //  .FirstOrDefaultAsync();

               // Total = _Boleto_Ent.clave_e;
                //Items = await _context.Boleto_Ent.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(()=>Ok(Total));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_EntByClaveEList([FromBody]List<Int64> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                _logger.LogInformation($"Arranca comparación Entrada: {stopwatch.Elapsed}");
                //string listadoentradas = string.Join(",", clave_e_list);
                _context.Database.SetCommandTimeout(30);
                List<Int64> _encontrados = await _context.Boleto_Ent.Select(q => q.clave_e).ToListAsync();
                Items = clave_e_list.Except(_encontrados).ToList();
                _logger.LogInformation($"Termina comparación Entrada: {stopwatch.Elapsed}");

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        private async Task<List<Int64>> GetBatchExistsEntry(List<Int64> Id)
        {
            List<Int64> _entradasexistentes = new List<Int64>();

            try
            {
                Int64 p = 0;
                foreach (var item in Id)
                {
                    p = await _context.Boleto_Ent.Where(q => q.clave_e==item).Select(q => q.clave_e).FirstOrDefaultAsync();
                    if(p>0)
                    {
                        _entradasexistentes.Add(p);
                    }
                }
              //  _entradasexistentes = await _context.Boleto_Ent.Where(q => Id.Contains(q.clave_e)).Select(q => q.clave_e).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _entradasexistentes;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_E_ByClassList([FromBody]List<Boleto_Ent> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {
                //using (var transaction = _context.Database.BeginTransaction())
                //{
                try
                {                  
                     _context.BulkInsert(clave_e_list);
                    await _context.SaveChangesAsync();            


                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    throw ex;
                }                  


              //  }
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
        /// Obtiene los Datos de la Boleto_Ent por medio del Id enviado.
        /// </summary>
        /// <param name="Boleto_EntId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Boleto_EntId}")]
        public async Task<IActionResult> GetBoleto_EntById(Int64 Boleto_EntId)
        {
            Boleto_Ent Items = new Boleto_Ent();
            try
            {
                Items = await _context.Boleto_Ent.Include(q=>q.Boleto_Sal).Where(q => q.clave_e == Boleto_EntId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Insert([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _Boleto_Entq = _Boleto_Ent;
                        _context.Boleto_Ent.Add(_Boleto_Entq);
                        await _context.SaveChangesAsync();


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Boleto_Ent.clave_e,
                            DocType = "Boleto_Ent",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _Boleto_Ent.UsuarioCreacion,
                            //UsuarioModificacion = _Boleto_Ent.UsuarioModificacion,
                            //UsuarioEjecucion = _Boleto_Ent.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Actualiza la Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Update([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = _Boleto_Ent;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Boleto_Entq = await (from c in _context.Boleto_Ent
                                 .Where(q => q.clave_e == _Boleto_Ent.clave_e)
                                              select c
                                ).FirstOrDefaultAsync();

                        _context.Entry(_Boleto_Entq).CurrentValues.SetValues((_Boleto_Ent));

                        //_context.Boleto_Ent.Update(_Boleto_Entq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Boleto_Ent.clave_e,
                            DocType = "Boleto_Ent",
                            ClaseInicial =
                                   Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _Boleto_Ent.UsuarioCreacion,
                            //UsuarioModificacion = _Boleto_Ent.UsuarioModificacion,
                            //UsuarioEjecucion = _Boleto_Ent.UsuarioModificacion,

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

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Elimina una Boleto_Ent       
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                _Boleto_Entq = _context.Boleto_Ent
                .Where(x => x.clave_e == (Int64)_Boleto_Ent.clave_e)
                .FirstOrDefault();

                _context.Boleto_Ent.Remove(_Boleto_Entq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);

        }







    }
}