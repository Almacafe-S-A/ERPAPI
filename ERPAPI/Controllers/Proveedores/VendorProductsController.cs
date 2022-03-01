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
using ERPAPI.Contexts;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/VendorProduct")]
    public class VendorProducts : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorProducts(ILogger<VendorProduct> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de VendorProducts paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorProductsPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<VendorProduct> Items = new List<VendorProduct>();
            try
            {
                var query = _context.VendorProduct.AsQueryable();
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


        // GET: api/VendorProduct
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorProduct()
        {
            List<VendorProduct> Items = new List<VendorProduct>();
            try
            {
                Items = await _context.VendorProduct.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/VendorProductGetVendorProductById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetVendorProductById(int Id)
        {
            VendorProduct Items = new VendorProduct();
            try
            {
                Items = await _context.VendorProduct.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        

        [HttpPost("[action]")]
        public async Task<ActionResult<VendorProduct>> Insert([FromBody]VendorProduct payload)
        {
            VendorProduct VendorProduct = payload;

            try
            {
                _context.VendorProduct.Add(VendorProduct);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(VendorProduct));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<VendorProduct>> Update([FromBody]VendorProduct _VendorProduct)
        {

            try
            {
                VendorProduct VendorProductq = (from c in _context.VendorProduct
                   .Where(q => q.Id == _VendorProduct.Id)
                                select c
                     ).FirstOrDefault();

                

                _context.Entry(VendorProductq).CurrentValues.SetValues((_VendorProduct));
                // _context.VendorProduct.Update(_VendorProduct);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorProduct));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorProduct payload)
        {
            VendorProduct VendorProduct = new VendorProduct();
            try
            {
                VendorProduct = _context.VendorProduct
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.VendorProduct.Remove(VendorProduct);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(VendorProduct));

        }



    }
}