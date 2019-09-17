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
    [Route("api/ControlPallets")]
    [ApiController]
    public class ControlPalletsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ControlPalletsController(ILogger<ControlPalletsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ControlPallets paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                var query = _context.ControlPallets.AsQueryable();
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
        /// Obtiene el Listado de Control Estibas 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPallets()
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                Items = await _context.ControlPallets.Where(q=>q.EsIngreso==1).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de Control Estibas 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsSalida()
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                Items = await _context.ControlPallets.Where(q=>q.EsIngreso==0).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Controles de ingresos 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetControlPalletsNoSelected()
        {
            List<ControlPallets> Items = new List<ControlPallets>();
            try
            {
                List<Int64> listayaprocesada = _context.GoodsReceived.Where(q => q.ControlId > 0).Select(q=>q.ControlId).ToList();

                //_context.GoodsReceivedLine
                //                          .Where(q=>q.ControlPalletsId>0)
                //                          .Select(q => q.ControlPalletsId).ToList();

                var query =  (from s in _context.ControlPallets
                where !_context.GoodsReceived.Any(es => (es.ControlId == s.ControlPalletsId))
                                select s);

                Items = query.ToList();
                //var val = await _context.ControlPallets.Where(q=> !listayaprocesada.Contains(q.ControlPalletsId) ).ToListAsync();

                List<Int64> _listaint = Items.Select(q => q.WeightBallot).ToList();
                List<Boleto_Ent> _weightballot = _context.Boleto_Ent.Where(q => _listaint.Contains(q.clave_e)).ToList();
                List<Int64> finalizados = _weightballot.Where(q => q.completo == true).Select(q=>q.clave_e).ToList();
                Items = Items.Where(q => finalizados.Contains(q.WeightBallot)).ToList();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la ControlPallets por medio del Id enviado.
        /// </summary>
        /// <param name="ControlPalletsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ControlPalletsId}")]
        public async Task<IActionResult> GetControlPalletsById(Int64 ControlPalletsId)
        {
            ControlPallets Items = new ControlPallets();
            try
            {
                Items = await _context.ControlPallets.Where(q => q.ControlPalletsId == ControlPalletsId).Include(q => q._ControlPalletsLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ControlPallets>> Insert([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _ControlPalletsq = _ControlPallets;
                        _context.ControlPallets.Add(_ControlPalletsq);
                      
                        foreach (var item in _ControlPalletsq._ControlPalletsLine)
                        {
                            item.ControlPalletsId = _ControlPalletsq.ControlPalletsId;
                            _context.ControlPalletsLine.Add(item);
                        }
                        // await _context.SaveChangesAsync();
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ControlPallets.ControlPalletsId,
                            DocType = "ControlPallets",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_ControlPallets, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ControlPallets.UsuarioCreacion,
                            UsuarioModificacion = _ControlPallets.UsuarioModificacion,
                            UsuarioEjecucion = _ControlPallets.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_ControlPalletsq));
        }

        /// <summary>
        /// Actualiza la ControlPallets
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ControlPallets>> Update([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = _ControlPallets;
            try
            {
                _ControlPalletsq = await (from c in _context.ControlPallets
                                 .Where(q => q.ControlPalletsId == _ControlPallets.ControlPalletsId)
                                    select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ControlPalletsq).CurrentValues.SetValues((_ControlPallets));

                //_context.ControlPallets.Update(_ControlPalletsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ControlPalletsq));
        }

        /// <summary>
        /// Elimina una ControlPallets       
        /// </summary>
        /// <param name="_ControlPallets"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ControlPallets _ControlPallets)
        {
            ControlPallets _ControlPalletsq = new ControlPallets();
            try
            {
                _ControlPalletsq = _context.ControlPallets
                .Where(x => x.ControlPalletsId == (Int64)_ControlPallets.ControlPalletsId)
                .FirstOrDefault();

                _context.ControlPallets.Remove(_ControlPalletsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ControlPalletsq));

        }







    }
}