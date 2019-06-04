using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OFAC;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/OFAC")]
    [ApiController]
    public class OFACController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public OFACController(ILogger<OFACController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> GetPersonByName([FromBody]sdnListSdnEntryM _sdnListSdnEntryM)
        {
            List<sdnListSdnEntryM> _personapornombre = new List<sdnListSdnEntryM>();
            try
            {
                var query = _context.sdnListSdnEntry
                      .Where(q => q.lastName.Contains(_sdnListSdnEntryM.lastName)
                           || q.firstName.Contains(_sdnListSdnEntryM.firstName));
                _personapornombre = await query
                        .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return Ok(_personapornombre);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetPersonByNumber([FromBody]sdnListSdnEntryIDM _sdnListSdnEntryIDM)
        {
            List<sdnListSdnEntryIDM> _personapornombre = new List<sdnListSdnEntryIDM>();
            try
            {
                _personapornombre = await _context.sdnListSdnEntryID
                      .Where(q => q.idNumber.Contains(_sdnListSdnEntryIDM.idNumber)
                         )
                        .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return Ok(_personapornombre);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]sdnListM payload)
        {
            try
            {
                sdnListM customerType = payload;
                _context.sdnList.Add(customerType);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(customerType));
                // return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: { ex.Message }");
            }

        }


    }
}