using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Warehouse")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public WarehouseController(ILogger<SalesTypeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        // GET: api/Warehouse
        [HttpGet]
        public async Task<IActionResult> GetWarehouse()
        {
            List<Warehouse> Items = new List<Warehouse>();
            try
            {
                Items =  await _context.Warehouse.ToListAsync();
                //int Count = Items.Count();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
            return Ok( Items);
        }


        [HttpGet("[action]/{BranchId}")]
        public async Task<IActionResult> GetWarehouseByBranchId(Int64 BranchId)
        {
            List<Warehouse> Items = new List<Warehouse>();
            try
            {
                Items = await _context.Warehouse.Where(q=>q.BranchId==BranchId).ToListAsync();
              
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(Items);
        }




        [HttpPost("[action]")]
        public async Task<ActionResult<Warehouse>> Insert([FromBody]Warehouse payload)
        {
            Warehouse warehouse = payload;
            try
            {              
                _context.Warehouse.Add(warehouse);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          


            return Ok(warehouse);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Warehouse>> Update([FromBody]Warehouse _Warehouse)
        {            
            try
            {

                Warehouse warehouseq = (from c in _context.Warehouse
                     .Where(q => q.WarehouseId == _Warehouse.WarehouseId)
                                                   select c
                       ).FirstOrDefault();

                _Warehouse.FechaCreacion = warehouseq.FechaCreacion;
                _Warehouse.UsuarioCreacion = warehouseq.UsuarioCreacion;

                _context.Warehouse.Update(_Warehouse);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
            return Ok(_Warehouse);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Warehouse>> Delete([FromBody]Warehouse payload)
        {
            Warehouse warehouse = new Warehouse();

            try
            {
                warehouse = _context.Warehouse
               .Where(x => x.WarehouseId == (int)payload.WarehouseId)
               .FirstOrDefault();

               _context.Warehouse.Remove(warehouse);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(warehouse);

        }
    }
}