/********************************************************************************************************
-- NAME   :  CRUDFundingInterestRate
-- PROPOSE:  show FundingInterestRate records
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
7.0                  05/01/2020          Marvin.Guillen                 Changes of delete records of FundingInterestRate
6.0                  30/12/2019          Maria.Funez                    Changes of modified method of controller
5.0                  23/12/2019          Marvin.Guillen                 Changes of Validation to delete
4.0                  10/12/2019          Maria.Funez                    Changes of Merger con rama de de sarrollo
3.0                  10/12/2019          Maria.Funez                    Changes of metodo para traer los registros activos
2.0                  05/12/2019          Marvin.Guillen                 Changes of FundingInterestRate
1.0                  19/09/2019          Carlos.Castillo                Creation of Controller
********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FundingInterestRateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public FundingInterestRateController(ILogger<FundingInterestRateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de FundingInterestRates paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFundingInterestRatesPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<FundingInterestRate> Items = new List<FundingInterestRate>();
            try
            {
                var query = _context.FundingInterestRate.AsQueryable();
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



        // GET: api/FundingInterestRate
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFundingInterestRate()
        {
            List<FundingInterestRate> Items = new List<FundingInterestRate>();
            try
            {
                Items = await _context.FundingInterestRate.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/FundingInterestRateGetFundingInterestRateById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetFundingInterestRateById(int Id)
        {
            FundingInterestRate Items = new FundingInterestRate();
            try
            {
                Items = await _context.FundingInterestRate.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{Descripcion}")]
        public async Task<IActionResult> GetFundingInterestRateByDescripcion(String Descripcion)
        {
            FundingInterestRate Items = new FundingInterestRate();
            try
            {
                Items = await _context.FundingInterestRate.Where(q => q.Descripcion== Descripcion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{idestado}")]
        public async Task<ActionResult> GetTasaInteresByEstado(Int64 idestado)
        {
            try
            {
                List<FundingInterestRate> Items = await _context.FundingInterestRate.Where(q => q.IdEstado == idestado).ToListAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }
        /// <summary>
        /// Obtiene los Datos de la Tasa por medio del Id enviado. Validación de eliminar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Int32>> ValidationDelete(int Id)
        {
            try
            {
                Int32 Items = await _context.Product.Where(a => a.FundingInterestRateId == Id)
                                                 .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<FundingInterestRate>> Insert([FromBody]FundingInterestRate payload)
        {
            FundingInterestRate FundingInterestRate = payload;

            try
            {
                _context.FundingInterestRate.Add(FundingInterestRate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(FundingInterestRate));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<FundingInterestRate>> Update([FromBody]FundingInterestRate _FundingInterestRate)
        {

            try
            {
                FundingInterestRate FundingInterestRateq = (from c in _context.FundingInterestRate
                   .Where(q => q.Id == _FundingInterestRate.Id)
                                select c
                     ).FirstOrDefault();

                _FundingInterestRate.FechaCreacion = FundingInterestRateq.FechaCreacion;
                _FundingInterestRate.UsuarioCreacion = FundingInterestRateq.UsuarioCreacion;

                _context.Entry(FundingInterestRateq).CurrentValues.SetValues((_FundingInterestRate));
                // _context.FundingInterestRate.Update(_FundingInterestRate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_FundingInterestRate));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]FundingInterestRate payload)
        {
            FundingInterestRate FundingInterestRate = new FundingInterestRate();
            try
            {
                FundingInterestRate = _context.FundingInterestRate
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.FundingInterestRate.Remove(FundingInterestRate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(FundingInterestRate));

        }


    }
}
