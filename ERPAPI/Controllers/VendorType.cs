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

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/VendorType")]
    public class VendorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorTypesController(ILogger<VendorTypesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/VendorType
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorType()
        {
            List<VendorType> Items = new List<VendorType>();
            try
            {
                Items = await _context.VendorType.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/VendorTypeGetVendorTypeById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetVendorTypeById(int Id)
        {
            VendorType Items = new VendorType();
            try
            {
                Items = await _context.VendorType.Where(q => q.VendorTypeId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{VendorTypeName}")]
        public async Task<IActionResult> GetVendorTypeByName(string VendorTypeName)
        {
            VendorType Items = new VendorType();
            try
            {
                Items = await _context.VendorType.Where(q => q.VendorTypeName.Equals(VendorTypeName)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<VendorType>> Insert([FromBody]VendorType payload)
        {
            VendorType VendorType = payload;

            try
            {
                _context.VendorType.Add(VendorType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(VendorType));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<VendorType>> Update([FromBody]VendorType _VendorType)
        {

            try
            {
                VendorType VendorTypeq = (from c in _context.VendorType
                   .Where(q => q.VendorTypeId == _VendorType.VendorTypeId)
                                  select c
                     ).FirstOrDefault();

                _VendorType.FechaCreacion = VendorTypeq.FechaCreacion;
                _VendorType.UsuarioCreacion = VendorTypeq.UsuarioCreacion;

                _context.Entry(VendorTypeq).CurrentValues.SetValues((_VendorType));
                // _context.VendorType.Update(_VendorType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorType));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorType payload)
        {
            VendorType VendorType = new VendorType();
            try
            {
                VendorType = _context.VendorType
                .Where(x => x.VendorTypeId == (int)payload.VendorTypeId)
                .FirstOrDefault();
                _context.VendorType.Remove(VendorType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(VendorType));

        }



    }
}