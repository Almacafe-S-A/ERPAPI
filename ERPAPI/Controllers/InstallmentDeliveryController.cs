/********************************************************************************************************

 -- NAME   :  CRUDInstallmentDelivery

 -- PROPOSE:  show InstallmentDelivery from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 2.0                  01/01/2020          Marvin.Guillen                Changes to validation to delete records
 1.0                  13/12/2019          Marvin.Guillen                Changes to create controller
 

 ********************************************************************************************************/

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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/InstallmentDelivery")]
    public class InstallmentDeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InstallmentDeliveryController(ILogger<InstallmentDeliveryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Measure paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInstallmentDeliveryPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<InstallmentDelivery> Items = new List<InstallmentDelivery>();
            try
            {
                var query = _context.InstallmentDelivery.AsQueryable();
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
        /// Obtiene el listado de Plazos de entrega.
        /// </summary>
        /// <returns></returns>
        // GET: api/InstallmentDelivery
        [HttpGet("[action]")]
        public async Task<ActionResult<InstallmentDelivery>> GetInstallmentDelivery()
        {
            List<InstallmentDelivery> Items = new List<InstallmentDelivery>();
            try
            {
                Items = await _context.InstallmentDelivery.ToListAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los datos de la plao de entrega con el id enviado
        /// </summary>
        /// <param name="InstallmentDeliveryId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{InstallmentDeliveryId}")]
        public async Task<ActionResult<InstallmentDelivery>> GetInstallmentDeliveryById(Int64 InstallmentDeliveryId)
        {
            InstallmentDelivery Items = new InstallmentDelivery();
            try
            {
                Items = await _context.InstallmentDelivery.Where
                    (q => q.InstallmentDeliveryId == InstallmentDeliveryId).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los datos de la InstallmentDelivery con el descripcion enviado
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Description}")]
        public async Task<ActionResult<InstallmentDelivery>> GetInstallmentDeliveryByDescrption(String Description)
        {
            InstallmentDelivery Items = new InstallmentDelivery();
            try
            {
                Items = await _context.InstallmentDelivery.Where(q => q.Description == Description).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }
        /// <summary>
        /// Obtiene los Datos de la InstallmentDelivery por medio del Id enviado. Validación de eliminar
        /// </summary>
        /// <param name="InstallmentDeliveryId"></param>
        /// <returns></returns>

        [HttpGet("[action]/{InstallmentDeliveryId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 InstallmentDeliveryId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;//await _context.Departamento.Where(a => a.ComisionId == InstallmentDeliveryId)
                  //                  .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        /// <summary>
        /// Inserta la InstallmentDelivery
        /// </summary>
        /// <param name="_InstallmentDelivery"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InstallmentDelivery>> Insert([FromBody]InstallmentDelivery _InstallmentDelivery)
        {
            InstallmentDelivery InstallmentDeliveryM = _InstallmentDelivery;
            try
            {
                _context.InstallmentDelivery.Add(InstallmentDeliveryM);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(InstallmentDeliveryM));
        }

        /// <summary>
        /// Actualiza la InstallmentDelivery
        /// </summary>
        /// <param name="_InstallmentDelivery"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InstallmentDelivery>> Update([FromBody]InstallmentDelivery _InstallmentDelivery)
        {

            try
            {
                InstallmentDelivery InstallmentDeliveryq = (from c in _context.InstallmentDelivery
                                       .Where(q => q.InstallmentDeliveryId == _InstallmentDelivery.InstallmentDeliveryId)
                                    select c
                                      ).FirstOrDefault();

                _InstallmentDelivery.CreatedDate = InstallmentDeliveryq.CreatedDate;
                _InstallmentDelivery.CreatedUser = InstallmentDeliveryq.CreatedUser;

                _context.Entry(InstallmentDeliveryq).CurrentValues.SetValues((_InstallmentDelivery));
                //  _context.Currency.Update(_Currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_InstallmentDelivery));
        }

        /// <summary>
        /// Elimina la InstallmentDelivery
        /// </summary>
        /// <param name="_InstallmentDelivery"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<InstallmentDelivery>> Delete([FromBody]InstallmentDelivery _InstallmentDelivery)
        {
            InstallmentDelivery InstallmentDeliveryy = new InstallmentDelivery();
            try
            {

                InstallmentDeliveryy = _context.InstallmentDelivery
                    .Where(x => x.InstallmentDeliveryId == _InstallmentDelivery.InstallmentDeliveryId)
                   .FirstOrDefault();
                _context.InstallmentDelivery.Remove(InstallmentDeliveryy);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(InstallmentDeliveryy));

        }


    }
}