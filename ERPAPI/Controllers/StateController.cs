/********************************************************************************************************
-- NAME   :  CRUDState
-- PROPOSE:  show State records
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
7.0                  23/12/2019          Marvin.Guillen                  Changes of Validation delete record
6.0                  08/12/2019          Marvin.Guillen                  Changes of Validation duplicated
5.0                  16/09/2019          Freddy.Chinchilla               Changes of Pagination Controller
4.0                  02/09/2019          Freddy.Chinchilla               Changes of Boleta_ent
3.0                  08/12/2019          Cristopher.Arias                TesT
2.0                  22/07/2019          Freddy.Chinchilla               Changes of Controller
1.0                  22/07/2019          Freddy.Chinchilla               Creation of Controller
********************************************************************************************************/

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
    [Route("api/State")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public StateController(ILogger<StateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de State paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStatePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<State> Items = new List<State>();
            try
            {
                var query = _context.State.AsQueryable();
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
        /// Obtiene el Listado de Statees 
        /// El estado define cuales son los Estate
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetState()
        {
            List<State> Items = new List<State>();
            try
            {
                Items = await _context.State.ToListAsync();
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
        /// Obtiene los Datos de la State por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetStateById(Int64 Id)
        {
            State Items = new State();
            try
            {
                Items = await _context.State.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Consultar una nueva State Name en la Tabla
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<State>> GetStateByName([FromBody]State _State)
        {
            State _Stateq = new State();
            try
            {

                _Stateq = _context.State.Where(z => z.Name == _State.Name
                  && z.CountryId == _State.CountryId).FirstOrDefault();




            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Stateq));
        }
        [HttpGet("[action]/{StateId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 StateId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = await _context.Employees.Where(a => a.IdState == StateId)
                                    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        /// <summary>
        /// Inserta una nueva State
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<State>> Insert([FromBody]State _State)
        {
            State _Stateq = new State();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {

                    try
                    {
                        _Stateq = _State;
                        _context.State.Add(_Stateq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Stateq.Id,
                            DocType = "Customer",
                            ClaseInicial =
                                Newtonsoft.Json.JsonConvert.SerializeObject(_Stateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_State, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _State.UsuarioCreacion,
                            //UsuarioModificacion = _State.UsuarioModificacion,
                            //UsuarioEjecucion = _State.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");

                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Stateq));
        }

        /// <summary>
        /// Actualiza la State
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<State>> Update([FromBody]State _State)
        {
            State _Stateq = _State;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Stateq = await (from c in _context.State
                                 .Where(q => q.Id == _State.Id)
                                         select c
                                ).FirstOrDefaultAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Stateq.Id,
                            DocType = "Customer",
                            ClaseInicial =
                                       Newtonsoft.Json.JsonConvert.SerializeObject(_Stateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_State, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _State.UsuarioCreacion,
                            //UsuarioModificacion = _State.UsuarioModificacion,
                            //UsuarioEjecucion = _State.UsuarioModificacion,

                        });

                        _context.Entry(_Stateq).CurrentValues.SetValues((_State));

                        //_context.State.Update(_Stateq);
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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Stateq));
        }

        /// <summary>
        /// Elimina una State       
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]State _State)
        {
            State _Stateq = new State();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Stateq = _context.State
                          .Where(x => x.Id == (Int64)_State.Id)
                          .FirstOrDefault();

                        _context.State.Remove(_Stateq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Stateq.Id,
                            DocType = "Customer",
                            ClaseInicial =
                                     Newtonsoft.Json.JsonConvert.SerializeObject(_Stateq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_State, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _State.UsuarioCreacion,
                            //UsuarioModificacion = _State.UsuarioModificacion,
                            //UsuarioEjecucion = _State.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Stateq));

        }







    }
}