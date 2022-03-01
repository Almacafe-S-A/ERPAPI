using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contrato")]
    [ApiController]
    public class ContratoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ContratoController(ILogger<ContratoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: /<controller>/
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContrato()
        {
            List<Contrato> Items = new List<Contrato>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.Contrato.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.ContratoId).ToListAsync();
                }
                else
                {
                    Items = await _context.Contrato.OrderByDescending(b => b.ContratoId).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{ContratoId}")]
        public async Task<ActionResult<Contrato>> GetContratoById(Int64 ContratoId)
        {
            Contrato Items = new Contrato();
            try
            {
                Items = await _context.Contrato.Where(q => q.ContratoId == ContratoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContratoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Contrato> Items = new List<Contrato>();
            try
            {
                var query = _context.Contrato.AsQueryable();
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
        /// Inserta una nueva Contrato
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> Insert([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = new Contrato();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contratoq = _Contrato;
                        _context.Contrato.Add(_Contratoq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato.ContratoId,
                            DocType = "Contrato",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato.UsuarioCreacion,
                            UsuarioModificacion = _Contrato.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Contratoq));
        }

        /// <summary>
        /// Actualiza  Contrato
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPut("[action]")]

        public async Task<ActionResult<Contrato>> Update([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = _Contrato;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contratoq = await (from c in _context.Contrato
                                         .Where(q => q.ContratoId == _Contrato.ContratoId)
                                            select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Contratoq).CurrentValues.SetValues((_Contrato));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Contrato.Update(_Contratoq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato.ContratoId,
                            DocType = "Contrato",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Contratoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato.UsuarioCreacion,
                            UsuarioModificacion = _Contrato.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato.UsuarioModificacion,

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
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contratoq));
        }

        /// <summary>
        /// Elimina  Contrato       
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> Delete([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = new Contrato();
            try
            {
                _Contratoq = _context.Contrato
                .Where(x => x.ContratoId == (Int64)_Contrato.ContratoId)
                .FirstOrDefault();

                _context.Contrato.Remove(_Contratoq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contratoq));

        }

    }

}
