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
    public class BitacoraCierreContableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public BitacoraCierreContableController(ILogger<BitacoraCierreContableController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


       

        /// <summary>
        /// Obtiene el Listado de BitacoraCierreContable paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreContablePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();
            try
            {
                var query = _context.BitacoraCierreContable.AsQueryable();
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

        // GET: api/BitacoraCierreContable
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreContable()

        {
            List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();
            try
            {
                Items = await _context.BitacoraCierreContable.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        // GET: api/BitacoraCierreContable
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreContableAsientos()
        {
            List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();
            try
            {
                Items = await _context.BitacoraCierreContable.Where(q => q.Id == 65).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        // GET: api/BitacoraCierreContable
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBitacoraCierreContableAjustes()

        {
            List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();
            try
            {
                Items = await _context.BitacoraCierreContable.Where(q => q.Id == 66).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la BitacoraCierreContable por medio del Id enviado.
        /// </summary>
        /// <param name="BitacoraCierreContableId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BitacoraCierreContableId}")]
        public async Task<IActionResult> GetBitacoraCierreContableById(Int64 BitacoraCierreContableId)
        {
            BitacoraCierreContable Items = new BitacoraCierreContable();
            try
            {
                Items = await _context.BitacoraCierreContable.Where(q => q.Id == BitacoraCierreContableId).Include(q => q.Id ).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la BitacoraCierreContableLine por medio del Id enviado.
        /// </summary>
        /// 
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="AccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{FechaInicio}/{FechaFinal}/{AccountId}")]
        public async Task<IActionResult> GetBitacoraCierreContableByDateAccount(string FechaInicio, string FechaFinal, Int64 AccountId)
        {
            //string fecha = Date.ToString("yyyy-MM-dd");
            DateTime fechainicio = Convert.ToDateTime(FechaInicio);
            DateTime fechafinal = Convert.ToDateTime(FechaFinal);
            //List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();
            //var Items;
            //List<BitacoraCierreContable> Items = new List<BitacoraCierreContable>();

            //var Items = new List<double>();

            //var Items = new List<double>();

            List<ConciliacionDTO> Items = new List<ConciliacionDTO>();



            try
            {




                var query = "select sum(debit) as Debito ,SUM(CREDIT) as Credito from dbo.BitacoraCierreContableline jel   "
                  + $"inner join  dbo.BitacoraCierreContable je  on je.BitacoraCierreContableid = jel.BitacoraCierreContableid "
                  + $"where JE.[DATE] >= '{FechaInicio}' and JE.[DATE] < ='{FechaFinal}' and jel.AccountId = {AccountId}"
                 + "  ";


                using (var dr = await _context.Database.ExecuteSqlQueryAsync(query))
                {
                    // Output rows.
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),

                        /*Items.Add(new ConciliacionDTO
                        {
                            Debit = Convert.ToDouble(reader["Debito"]),
                            Credit = Convert.ToDouble(reader["Credito"])
                        });*/

                        //Items.Add(Convert.ToDouble(reader["CREDITO"]));
                        //Items.Add(
                        //{
                        //    //AccountId = reader["AccountId"] == DBNull.Value ? 0 : Convert.ToInt64(reader["AccountId"]),
                        //DEBITO = reader["DEBITO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalCredit"]),
                        //CREDITO = reader["CREDITO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TotalDebit"]),
                        //});

                    }
                }



            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva BitacoraCierreContable
        /// </summary>
        /// <param name="_BitacoraCierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BitacoraCierreContable>> Insert([FromBody]dynamic dto)
        //public async Task<ActionResult<BitacoraCierreContable>> Insert([FromBody]BitacoraCierreContable _BitacoraCierreContable)
        {
            BitacoraCierreContable _BitacoraCierreContable = new BitacoraCierreContable();
            BitacoraCierreContable _BitacoraCierreContableq = new BitacoraCierreContable();
            try
            {
                _BitacoraCierreContable = JsonConvert.DeserializeObject<BitacoraCierreContable>(dto.ToString());
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _BitacoraCierreContableq = _BitacoraCierreContable;
                        _context.BitacoraCierreContable.Add(_BitacoraCierreContableq);
                        // await _context.SaveChangesAsync();
                        double sumacreditos = 0, sumadebitos = 0;
                        foreach (var item in _BitacoraCierreContableq.CierreContableLineas)
                        {
                            item.IdBitacoraCierre = _BitacoraCierreContableq.Id;
                            // item.BitacoraCierreContableLineId = 0;
                            _context.BitacoraCierreProceso.Add(item);
                        }


                        if (sumacreditos != sumadebitos)
                        {
                            transaction.Rollback();
                            _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos:{sumacreditos}");
                            return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos:{sumacreditos}");
                        }

                        await _context.SaveChangesAsync();



                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _BitacoraCierreContable.Id,
                            DocType = "BitacoraCierreContable",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_BitacoraCierreContable, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _BitacoraCierreContable.UsuarioCreacion,
                            UsuarioModificacion = _BitacoraCierreContable.UsuarioModificacion,
                            UsuarioEjecucion = _BitacoraCierreContable.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_BitacoraCierreContableq));
        }

        /// <summary>
        /// Actualiza la BitacoraCierreContable
        /// </summary>
        /// <param name="_BitacoraCierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        // public async Task<ActionResult<BitacoraCierreContable>> Update([FromBody]BitacoraCierreContable _BitacoraCierreContable)
        public async Task<ActionResult<BitacoraCierreContable>> Update([FromBody]dynamic dto)
        {
            //BitacoraCierreContable _BitacoraCierreContableq = _BitacoraCierreContable;
            BitacoraCierreContable _BitacoraCierreContable = new BitacoraCierreContable();
            BitacoraCierreContable _BitacoraCierreContableq = new BitacoraCierreContable();
            try
            {
                _BitacoraCierreContable = JsonConvert.DeserializeObject<BitacoraCierreContable>(dto.ToString());
                _BitacoraCierreContableq = await (from c in _context.BitacoraCierreContable
                                 .Where(q => q.Id == _BitacoraCierreContable.Id)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_BitacoraCierreContableq).CurrentValues.SetValues((_BitacoraCierreContable));

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BitacoraCierreContableq));
        }

        /// <summary>
        /// Elimina una BitacoraCierreContable       
        /// </summary>
        /// <param name="_BitacoraCierreContable"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]BitacoraCierreContable _BitacoraCierreContable)
        {
            BitacoraCierreContable _BitacoraCierreContableq = new BitacoraCierreContable();
            try
            {
                _BitacoraCierreContableq = _context.BitacoraCierreContable
                .Where(x => x.Id == (Int64)_BitacoraCierreContable.Id)
                .FirstOrDefault();

                _context.BitacoraCierreContable.Remove(_BitacoraCierreContableq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_BitacoraCierreContableq));

        }
    }
}
