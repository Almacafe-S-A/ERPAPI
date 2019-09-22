using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/AccountingChilds")]
    [ApiController]
    public class AccountingChildsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public AccountingChildsController(ILogger<AccountingChildsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene los Datos de la AccountingChilds en una lista.
        /// </summary>
        /// <param name="AccountingChildsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AccountingChildsId}")]
        public async Task<IActionResult> GetAccountingCByAccountingCId(Int64 AccountingChildsId)

        {
            List<AccountingChilds> Items = new List<AccountingChilds>();
            try
            {
                Items = await _context.AccountingChilds.Where(p => p.AccountingChildsId == AccountingChildsId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }



        /// <summary>
        /// Obtiene los Datos de lod hijos contables por medio del Id del padre enviado.
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{AccountingChildsId}")]
        public async Task<IActionResult> GetAccountingChildsByParentId(Int64 ParentId)
        {
            List<AccountingChilds> Items = new List<AccountingChilds>();

           // AccountingChilds Items = new AccountingChilds();
            try
            {
                Items = await _context.AccountingChilds.Where(q => q.ParentAccountId == ParentId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            //List<AccountingChilds> Items = new List<AccountingChilds>();
            
            
              //  Items = await _context.AccountingChilds.Where(p => p.ParentAccountId == AccountId)
                //    .ToListAsync();
            
           

            return await Task.Run(() => Ok(Items));
        }

        

        /// <summary>
        /// Inserta una nueva Account
        /// </summary>
        /// <param name="_AccountingChilds"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<AccountingChilds>> Insert([FromBody]AccountingChilds _AccountingChilds)
        {
            AccountingChilds _AccountingChildsq = new AccountingChilds();


            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _AccountingChildsq = _AccountingChilds;
                        _context.AccountingChilds.Add(_AccountingChildsq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _AccountingChildsq.AccountingChildsId,
                            DocType = "AccountingChilds",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_AccountingChildsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _AccountingChildsq.CreatedUser,
                            UsuarioModificacion = _AccountingChildsq.ModifiedUser,
                            UsuarioEjecucion = _AccountingChildsq.ModifiedUser,

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

            return await Task.Run(() => Ok(_AccountingChildsq));
            
          
        }

        /// <summary>
        /// Actualiza la Account
        /// </summary>
        /// <param name="_AccountingChilds"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<AccountingChilds>> Update([FromBody]AccountingChilds _AccountingChilds)
        {
            AccountingChilds _AccountingChildsq = _AccountingChilds;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _AccountingChildsq = await (from c in _context.AccountingChilds
                                         .Where(q => q.AccountingChildsId == _AccountingChildsq.AccountingChildsId)
                                           select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_AccountingChildsq).CurrentValues.SetValues((_AccountingChilds));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _AccountingChildsq.AccountingChildsId,
                            DocType = "AccountingChilds",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_AccountingChildsq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_AccountingChilds, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _AccountingChilds.CreatedUser,
                            UsuarioModificacion = _AccountingChilds.ModifiedUser,
                            UsuarioEjecucion = _AccountingChilds.ModifiedUser,

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


            return await Task.Run(() => Ok(_AccountingChildsq));
        }

        /// <summary>
        /// Elimina una Account       
        /// </summary>
        /// <param name="_AccountingChilds"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]AccountingChilds _AccountingChilds)
        {
            AccountingChilds _AccountingChildsq = new AccountingChilds();
            try
            {
                _AccountingChildsq = _context.AccountingChilds
                .Where(x => x.AccountingChildsId == (Int64)_AccountingChilds.AccountingChildsId)
                .FirstOrDefault();

                _context.AccountingChilds.Remove(_AccountingChildsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_AccountingChildsq));

        }
        /*[HttpGet("[action]")]
        public async Task<ActionResult<AccountClasses>> GetEnumClass()//Int64 idgrupoestado
        {
            List<AccountClasses> Items = new List<AccountClasses>();

            try
            {
                Items = await _context.enum//.AccountClasses.ToListAsync();
                //List<AccountClasses> Items; //await _context.Account.a//.Where(q => q.IdGrupoEstado == idgrupoestado).ToListAsync();
                //return await Task.Run(() => Ok(AccountClasses));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }*/
        /*
        List<Account> Items = new List<Account>();
        try
        {
            Items = await _context.Account.ToListAsync();
        }
        catch (Exception ex)
        {

            _logger.LogError($"Ocurrio un error: { ex.ToString() }");
            return BadRequest($"Ocurrio un error:{ex.Message}");
        }

        //  int Count = Items.Count();
        return await Task.Run(() => Ok(Items));

         */
        //}
    }
}