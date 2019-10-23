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
    [Route("api/CustomerConditions")]
    [ApiController]
    public class CustomerConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CustomerConditionsController(ILogger<CustomerConditionsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerConditions paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerConditionsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<CustomerConditions> Items = new List<CustomerConditions>();
            try
            {
                var query = _context.CustomerConditions.AsQueryable();
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
        /// Obtiene el Listado de CustomerConditionses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerConditions()
        {
            List<CustomerConditions> Items = new List<CustomerConditions>();
            try
            {
                Items = await _context.CustomerConditions.ToListAsync();
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
        /// Obtiene los Datos de la CustomerConditions por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerConditionsId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerConditionsId}")]
        public async Task<IActionResult> GetCustomerConditionsById(Int64 CustomerConditionsId)
        {
            CustomerConditions Items = new CustomerConditions();
            try
            {
                Items = await _context.CustomerConditions.Where(q => q.CustomerConditionId == CustomerConditionsId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetCustomerConditionsByClass([FromBody]CustomerConditions _Ccq)
        {
            List<CustomerConditions> Items = new List<CustomerConditions>();
            try
            {
                Items = await   _context.CustomerConditions
                    .Where(q=>q.IdTipoDocumento==_Ccq.IdTipoDocumento)
                    .Where(q => q.SubProductId == _Ccq.SubProductId)
                    .Where(q => q.DocumentId == _Ccq.DocumentId).ToListAsync();

                Items = (from c in Items
                         select new CustomerConditions
                         {
                              LogicalConditionId = c.LogicalConditionId,
                              ConditionId = c.ConditionId,
                              CustomerConditionName = c.CustomerConditionName,
                              CustomerConditionId = c.CustomerConditionId,
                              Description = c.Description,
                              CustomerId = c.CustomerId,
                              SubProductId = c.SubProductId,
                              DocumentId = c.DocumentId,
                              Estado = c.Estado,
                              IdEstado = c.IdEstado,
                              IdTipoDocumento= c.IdTipoDocumento,
                              LogicalCondition = c.LogicalCondition,
                              ValueDecimal = c.ValueDecimal,
                              ValueToEvaluate =c.ValueToEvaluate !=null && c.ValueToEvaluate!="" ? Convert.ToDouble(c.ValueToEvaluate).ToString("n2") : "0",
                              ProductId = c.ProductId,
                              ValueString = c.ValueString,
                              FechaCreacion = c.FechaCreacion,
                              FechaModificacion = c.FechaModificacion,
                              UsuarioCreacion = c.UsuarioCreacion,
                              UsuarioModificacion = c.UsuarioModificacion,

                         }
                         ).ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva CustomerConditions
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CustomerConditions>> Insert([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = new CustomerConditions();
            try
            {
                _CustomerConditionsq = _CustomerConditions;
                _context.CustomerConditions.Add(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerConditionsq));
        }

        /// <summary>
        /// Actualiza la CustomerConditions
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CustomerConditions>> Update([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = _CustomerConditions;
            try
            {
                _CustomerConditionsq = (from c in _context.CustomerConditions
                                 .Where(q => q.CustomerConditionId == _CustomerConditions.CustomerConditionId)
                                        select c
                                ).FirstOrDefault();

                _context.Entry(_CustomerConditionsq).CurrentValues.SetValues((_CustomerConditions));

                //_context.CustomerConditions.Update(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerConditionsq));
        }

        /// <summary>
        /// Elimina una CustomerConditions       
        /// </summary>
        /// <param name="_CustomerConditions"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CustomerConditions _CustomerConditions)
        {
            CustomerConditions _CustomerConditionsq = new CustomerConditions();
            try
            {
                _CustomerConditionsq = _context.CustomerConditions
                .Where(x => x.CustomerConditionId == (Int64)_CustomerConditions.CustomerConditionId)
                .FirstOrDefault();

                _context.CustomerConditions.Remove(_CustomerConditionsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_CustomerConditionsq));

        }







    }
}