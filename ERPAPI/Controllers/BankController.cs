/********************************************************************************************************
-- NAME   :  CRUDBank
-- PROPOSE:  show records Bank
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
8.0                  23/12/2019          Marvin.Guillen                     Validation to eliminate record
7.0                  10/12/2019          Carlos.Solorzano                   Changes of Mdificacion de endpoints de conciliaciones para las cuenta debanco
6.0                  26/11/2019          Brayan.Interiano                   Changes of Validacion duplicados
5.0                  08/11/2019          Cristopher.Arias                   Changes of Cambios en conciliacion
4.0                  21/10/2019          Ana.Jimenez                        Changes of BanckController
3.0                  16/09/2019          Freddy.Chinchilla                  Changes of Pagination of Controller
2.0                  19/06/2019          Freddy.Chinchilla                  Changes of Task.Run return model
1.0                  06/06/2019          Freddy.Chinchilla                  Creation of Controller
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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Bank")]
    [ApiController]
    public class BankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BankController(ILogger<BankController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Bank
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBankPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Bank> Items = new List<Bank>();
            try
            {
                var query = _context.Bank.AsQueryable();
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
        /// Obtiene el Listado de Bankes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBank()
        {
            List<Bank> Items = new List<Bank>();
            try
            {
                Items = await _context.Bank.OrderBy(b=>b.BankName).ToListAsync();
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
        /// Obtiene los Datos de la Bank por medio del Id enviado.
        /// </summary>
        /// <param name="BankId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BankId}")]
        public async Task<IActionResult> GetBankById(Int64 BankId)
        {
            Bank Items = new Bank();
            try
            {
                Items = await _context.Bank.Where(q => q.BankId == BankId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }



        [HttpGet("[action]/{BankName}")]
        public async Task<IActionResult> GetBankByName(string BankName)
        {
            Bank Items = new Bank();
            try
            {
                Items = await _context.Bank.Where(q => q.BankName==BankName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{BankId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 BankId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = await _context.CheckAccount.Where(a => a.BankId == BankId)
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
        /// Inserta una nueva Bank
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Bank>> Insert([FromBody]Bank _Bank)
        {
            Bank _Bankq = new Bank();
            try
            {
                _Bankq = _Bank;
                _context.Bank.Add(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Bankq));
        }

        /// <summary>
        /// Actualiza la Bank
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Bank>> Update([FromBody]Bank _Bank)
        {
            Bank _Bankq = _Bank;
            try
            {
                _Bankq = await (from c in _context.Bank
                                 .Where(q => q.BankId == _Bank.BankId)
                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Bankq).CurrentValues.SetValues((_Bank));

                //_context.Bank.Update(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Bankq));
        }

        /// <summary>
        /// Elimina una Bank       
        /// </summary>
        /// <param name="_Bank"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Bank _Bank)
        {
            Bank _Bankq = new Bank();
            try
            {
                _Bankq = _context.Bank
                .Where(x => x.BankId == (Int64)_Bank.BankId)
                .FirstOrDefault();

                _context.Bank.Remove(_Bankq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Bankq));

        }

       





    }
}