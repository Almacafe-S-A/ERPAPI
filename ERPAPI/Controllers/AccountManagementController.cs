using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AccountManagementController(ILogger<AccountManagementController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Mantenimiento de cuentas, por paginas
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountManagementPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<AccountManagement> Items = new List<AccountManagement>();
            try
            {
                var query = _context.AccountManagement.AsQueryable();
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
        /// Obtiene el Listado de mantenimiento de cuentas
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountManagement()
        {
            List<AccountManagement> Items = new List<AccountManagement>();
            try
            {
                Items = await _context.AccountManagement.ToListAsync();
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
        /// Obtiene los Datos del mantenimiento de cuentas por medio del Id enviado.
        /// </summary>
        /// <param name="AccountManagementId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AccountManagementId}")]
        public async Task<IActionResult> GetSAccountManagementById(Int64 AccountManagementId)
        {
            AccountManagement Items = new AccountManagement();
            try
            {
                Items = await _context.AccountManagement.Where(q => q.AccountManagementId == AccountManagementId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos del mantenimiento de cuentas por medio del Id enviado.
        /// </summary>
        /// <param name="BankId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BankId}")]
        public async Task<IActionResult> GetAccountManagementByBankId(Int64 BankId)
        {
            List<AccountManagement> Items = new List<AccountManagement>();
            try
            {
                Items = await _context.AccountManagement.Where(q => q.BankId == BankId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }




        /// <summary>
        /// Obtiene los Datos del mantenimiento de cuentas por medio del Id enviado.
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AccountNumber}")]
        public async Task<IActionResult> GetSAccountManagementByAccountTypeAccountNumber(String AccountNumber)
        {
            AccountManagement Items = new AccountManagement();
            try
            {
                Items = await _context.AccountManagement.Where(q => q.AccountNumber == AccountNumber
                                            ).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta un nuevo mantenimiento de cuentas
        /// </summary>
        /// <param name="_AccountManagement"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<AccountManagement>> Insert([FromBody]AccountManagement _AccountManagement)
        {
            AccountManagement AccountManagementq = new AccountManagement();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        AccountManagementq = _AccountManagement;
                        _context.AccountManagement.Add(AccountManagementq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = AccountManagementq.AccountManagementId,
                            DocType = "AccountManagement",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(AccountManagementq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = AccountManagementq.UsuarioCreacion,
                            UsuarioModificacion = AccountManagementq.UsuarioModificacion,
                            UsuarioEjecucion = AccountManagementq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(AccountManagementq));
        }

        /// <summary>
        /// Actualiza el mantenimiento de cuentas
        /// </summary>
        /// <param name="_AccountManagement"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<AccountManagement>> Update([FromBody]AccountManagement _AccountManagement)
        {
            AccountManagement _AccountManagementq = _AccountManagement;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _AccountManagementq = await (from c in _context.AccountManagement
                        .Where(q => q.AccountManagementId == _AccountManagement.AccountManagementId)
                                                   select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_AccountManagementq).CurrentValues.SetValues((_AccountManagement));

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _AccountManagementq.AccountManagementId,
                            DocType = "AccountManagement",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_AccountManagementq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _AccountManagementq.UsuarioCreacion,
                            UsuarioModificacion = _AccountManagementq.UsuarioModificacion,
                            UsuarioEjecucion = _AccountManagementq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_AccountManagementq));
        }

        /// <summary>
        /// Elimina un mantenimiento de cuentas
        /// </summary>
        /// <param name="_AccountManagement"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]AccountManagement _AccountManagement)
        {
            AccountManagement _AccountManagementq = new AccountManagement();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _AccountManagementq = _context.AccountManagement
                        .Where(x => x.AccountManagementId == (Int64)_AccountManagement.AccountManagementId)
                        .FirstOrDefault();

                        _context.AccountManagement.Remove(_AccountManagementq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _AccountManagementq.AccountManagementId,
                            DocType = "AccountManagement",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_AccountManagementq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _AccountManagementq.UsuarioCreacion,
                            UsuarioModificacion = _AccountManagementq.UsuarioModificacion,
                            UsuarioEjecucion = _AccountManagementq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_AccountManagementq));

        }
    }
}
