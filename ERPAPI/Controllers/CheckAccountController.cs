/********************************************************************************************************
-- NAME   :  CRUDCheckAccount
-- PROPOSE:  show CheckAccount records
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
3.0                  09/12/2019          Marvin.Guillen                  Validation of duplicated
2.0                  11/11/2019          Marvin.Guillen                  Changes of Conciliaicon
1.0                  19/09/2019          Freddy.Chinchilla               Creation of Controller
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
    [Route("api/CheckAccount")]
    [ApiController]
    public class CheckAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CheckAccountController(ILogger<CheckAccountController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CheckAccount paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCheckAccountPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CheckAccount> Items = new List<CheckAccount>();
            try
            {
                var query = _context.CheckAccount.AsQueryable();
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
        /// Obtiene el Listado de CheckAccountes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCheckAccount()
        {
            List<CheckAccount> Items = new List<CheckAccount>();
            try
            {
                Items = await _context.CheckAccount.ToListAsync();
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
        /// Obtiene los Datos de la CheckAccount por medio del Id enviado.
        /// </summary>
        /// <param name="CheckAccountNumber"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountNumber}")]
        public async Task<IActionResult> GetCheckAccountByAccountNumber(string CheckAccountNumber)
        {
            CheckAccount Items = new CheckAccount();
            try
            {
                Items = await _context.CheckAccount.Where(q => q.CheckAccountNo 
                == CheckAccountNumber).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la CheckAccount por medio del Id enviado.
        /// </summary>
        /// <param name="CheckAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CheckAccountId}")]
        public async Task<IActionResult> GetCheckAccountById(Int64 CheckAccountId)
        {
            CheckAccount Items = new CheckAccount();
            try
            {
                Items = await _context.CheckAccount.Where(q => q.CheckAccountId == CheckAccountId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Verifica una nueva CheckAccount
        /// </summary>
        /// <param name="_CheckAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CheckAccount>> GetCheckAccountByCheckAccountNo([FromBody]CheckAccount _CheckAccount)
        {
            CheckAccount _CheckAccountq = new CheckAccount();
            try
            {
                
                        _CheckAccountq =  _context.CheckAccount.Where(z => z.BankId == _CheckAccount.BankId
          && z.CheckAccountNo == _CheckAccount.CheckAccountNo &&
            z.AssociatedAccountNumber== _CheckAccount.AssociatedAccountNumber).FirstOrDefault();


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CheckAccountq));
        }


        /// <summary>
        /// Inserta una nueva CheckAccount
        /// </summary>
        /// <param name="_CheckAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CheckAccount>> Insert([FromBody]CheckAccount _CheckAccount)
        {
            CheckAccount _CheckAccountq = new CheckAccount();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CheckAccountq = _CheckAccount;
                _context.CheckAccount.Add(_CheckAccountq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CheckAccountq.CheckAccountId,
                            DocType = "CheckAccount",
                            ClaseInicial =
 Newtonsoft.Json.JsonConvert.SerializeObject(_CheckAccountq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CheckAccountq.UsuarioCreacion,
                            UsuarioModificacion = _CheckAccountq.UsuarioModificacion,
                            UsuarioEjecucion = _CheckAccountq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CheckAccountq));
        }

        /// <summary>
        /// Actualiza la CheckAccount
        /// </summary>
        /// <param name="_CheckAccount"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CheckAccount>> Update([FromBody]CheckAccount _CheckAccount)
        {
            CheckAccount _CheckAccountq = _CheckAccount;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _CheckAccountq = await (from c in _context.CheckAccount
                                 .Where(q => q.CheckAccountId == _CheckAccount.CheckAccountId)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CheckAccountq).CurrentValues.SetValues((_CheckAccount));

                //_context.CheckAccount.Update(_CheckAccountq);
                await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _CheckAccountq.CheckAccountId,
                            DocType = "CheckAccount",
                            ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(_CheckAccountq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _CheckAccountq.UsuarioCreacion,
                            UsuarioModificacion = _CheckAccountq.UsuarioModificacion,
                            UsuarioEjecucion = _CheckAccountq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_CheckAccountq));
        }

        /// <summary>
        /// Elimina una CheckAccount       
        /// </summary>
        /// <param name="_CheckAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CheckAccount _CheckAccount)
        {
            CheckAccount _CheckAccountq = new CheckAccount();
            try
            {
                _CheckAccountq = _context.CheckAccount
                .Where(x => x.CheckAccountId == (Int64)_CheckAccount.CheckAccountId)
                .FirstOrDefault();

                _context.CheckAccount.Remove(_CheckAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_CheckAccountq));

        }

        /// <summary>
        /// Obtiene las cuentas de banco de un banco especifico
        /// </summary>
        /// <param name="bankId">Id de banco</param>
        /// <returns></returns>
        [HttpGet("[action]/{bankId}")]
        public async Task<IActionResult> GetCheckAccountByBankId(Int64 bankId)
        {
            List<CheckAccount> cuentas = new List<CheckAccount>();
            try
            {
                cuentas = await _context.CheckAccount.Where(q => q.BankId == bankId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(cuentas));
        }






    }
}