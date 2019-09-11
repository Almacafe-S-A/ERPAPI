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
    [Route("api/Vendor")]
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorsController(ILogger<VendorsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Vendor
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendor()
        {
            List<Vendor> Items = new List<Vendor>();
            try
            {
                Items = await _context.Vendor.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/VendorGetVendorById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetVendorById(int Id)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.VendorId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{VendorName}")]
        public async Task<IActionResult> GetVendorByName(string VendorName)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.VendorName.Equals(VendorName)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<Vendor>> Insert([FromBody]Vendor payload)
        {
            Vendor Vendor = payload;

            try
            {
                _context.Vendor.Add(Vendor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Vendor));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Vendor>> Update([FromBody]Vendor _Vendor)
        {

            try
            {
                Vendor Vendorq = (from c in _context.Vendor
                   .Where(q => q.VendorId == _Vendor.VendorId)
                                                select c
                     ).FirstOrDefault();

                _Vendor.FechaCreacion = Vendorq.FechaCreacion;
                _Vendor.UsuarioCreacion = Vendorq.UsuarioCreacion;

                _context.Entry(Vendorq).CurrentValues.SetValues((_Vendor));
                // _context.Vendor.Update(_Vendor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Vendor));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Vendor payload)
        {
            Vendor Vendor = new Vendor();
            try
            {
                Vendor = _context.Vendor
                .Where(x => x.VendorId == (int)payload.VendorId)
                .FirstOrDefault();
                _context.Vendor.Remove(Vendor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Vendor));

        }



    }
}