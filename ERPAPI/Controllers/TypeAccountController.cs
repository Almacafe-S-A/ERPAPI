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
    [Route("api/TypeAccount")]
    [ApiController]
    public class TypeAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        /*public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public TypeAccountController(ILogger<TypeAccountController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de TypeAccount paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypeAccountPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<TypeAccount> Items = new List<TypeAccount>();
            try
            {
                var query = _context.TypeAccount.AsQueryable();
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
        /// Obtiene los Datos de la TypeAccount en una lista.
        /// </summary>

        // GET: api/TypeAccount
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTypeAccount()

        {
            List<TypeAccount> Items = new List<TypeAccount>();
            try
            {
                Items = await _context.TypeAccount.ToListAsync();
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
        /// Obtiene los Datos de la Account por medio del Id enviado.
        /// </summary>
        /// <param name="TypeAccountId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeAccountId}")]
        public async Task<IActionResult> GetTypeAccountById(Int64 TypeAccountId)
        {
            TypeAccount Items = new TypeAccount();
            try
            {
                Items = await _context.TypeAccount.Where(q => q.TypeAccountId == TypeAccountId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los Datos de la Account por medio del Nombre enviado.
        /// </summary>
        /// <param name="TypeAccountName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{TypeAccountName}")]
        public async Task<IActionResult> GetTypeAccountByName(String TypeAccountName)
        {
            TypeAccount Items = new TypeAccount();
            try
            {
                Items = await _context.TypeAccount.Where(q => q.TypeAccountName == TypeAccountName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Account
        /// </summary>
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<TypeAccount>> Insert([FromBody]TypeAccountDTO _TypeAccount)
        {
            TypeAccount _TypeAccountq = new TypeAccount();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _TypeAccountq = _TypeAccount;
                        _context.TypeAccount.Add(_TypeAccountq);
                        await _context.SaveChangesAsync();

                        CompanyInfo _co = await _context.CompanyInfo.FirstOrDefaultAsync();
                        Accounting _padreaccount = new Accounting
                        {


                             AccountName = _TypeAccountq.TypeAccountName,  
                             AccountCode= _TypeAccountq.TypeAccountId.ToString(),
                             TypeAccountId = _TypeAccountq.TypeAccountId,
                             IsCash =false,
                             Description = _TypeAccountq.TypeAccountName,
                             CompanyInfoId = _co.CompanyInfoId,
                             UsuarioCreacion = _TypeAccountq.CreatedUser,
                             UsuarioModificacion = _TypeAccountq.ModifiedUser,
                             FechaCreacion = DateTime.Now,
                             FechaModificacion = DateTime.Now,
                             ParentAccountId = null,
                             Totaliza = true,
                             DeudoraAcreedora = _TypeAccount.DeudoraAcreedora,
                             IdEstado = 1,
                             Estado="Activo",

                        };


                        _context.Accounting.Add(_padreaccount);

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _TypeAccount.TypeAccountId,
                            DocType = "TypeAccount",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_TypeAccountq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _TypeAccountq.CreatedUser,
                            UsuarioModificacion = _TypeAccountq.ModifiedUser,
                            UsuarioEjecucion = _TypeAccountq.ModifiedUser,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                    }

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));
        }

        /// <summary>
        /// Actualiza la Account
        /// </summary>
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<TypeAccount>> Update([FromBody]TypeAccount _TypeAccount)
        {
            TypeAccount _TypeAccountq = _TypeAccount;
            try
            {
                _TypeAccountq = await (from c in _context.TypeAccount
                                 .Where(q => q.TypeAccountId == _TypeAccount.TypeAccountId)
                                       select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_TypeAccountq).CurrentValues.SetValues((_TypeAccount));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));
        }

        /// <summary>
        /// Elimina una Account       
        /// </summary>
        /// <param name="_TypeAccount"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TypeAccount _TypeAccount)
        {
            TypeAccount _TypeAccountq = new TypeAccount();
            try
            {
                _TypeAccountq = _context.TypeAccount
                .Where(x => x.TypeAccountId == (Int64)_TypeAccount.TypeAccountId)
                .FirstOrDefault();

                _context.TypeAccount.Remove(_TypeAccountq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_TypeAccountq));

        }
    }
}