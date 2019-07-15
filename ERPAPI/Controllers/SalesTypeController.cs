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
    //  [Produces("application/json")]
    [Route("api/SalesType")]
    public class SalesTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SalesTypeController(ILogger<SalesTypeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        // GET: api/SalesType
        [HttpGet]
        public async Task<ActionResult<SalesType>> GetSalesType()
        {
            List<SalesType> Items = new List<SalesType>();
            try
            {
                Items = await _context.SalesType.ToListAsync();
               // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
                
            return await Task.Run(()=> Ok(Items) ) ;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<SalesType>> Insert([FromBody]SalesType payload)
        {
            SalesType salesType = payload;

            try
            {
                _context.SalesType.Add(salesType);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(salesType));
            // return Ok(salesType);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SalesType>> Update([FromBody]SalesType payload)
        {
            SalesType salesType = payload;
            try
            {
                _context.SalesType.Update(salesType);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(salesType));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SalesType>> Remove([FromBody]SalesType payload)
        {
            SalesType salesType = new SalesType();
            try
            {
                salesType = _context.SalesType
                              .Where(x => x.SalesTypeId == (int)payload.SalesTypeId)
                              .FirstOrDefault();
                _context.SalesType.Remove(salesType);
               await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(salesType));
           // return Ok(salesType);

        }
    }
}