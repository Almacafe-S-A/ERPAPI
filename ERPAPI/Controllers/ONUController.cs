using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OFAC;
using ONUListas;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ONU")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ONUController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ONUController(ILogger<OFACController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> GetPersonByName([FromBody]INDIVIDUALM _INDIVIDUALM)
        {
            List<INDIVIDUALM> _personapornombre = new List<INDIVIDUALM>();
            try
            {
                var query = _context.INDIVIDUALM
                      .Where(q => (q.SECOND_NAME + q.FIRST_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                      || (q.FIRST_NAME+ q.SECOND_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                      || (q.SECOND_NAME +" "+ q.FIRST_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                      || (q.FIRST_NAME +" " + q.SECOND_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                      || (q.FIRST_NAME + " " + q.SECOND_NAME +" "+ q.THIRD_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                      || (q.SECOND_NAME + " " + q.FIRST_NAME + " " + q.THIRD_NAME).Contains(_INDIVIDUALM.SECOND_NAME)
                           );
                _personapornombre = await query
                        .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return await Task.Run(() => Ok(_personapornombre));
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

            return await Task.Run(() => Ok(_personapornombre));
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]CONSOLIDATED_LISTM payload)
        {
            try
            {
               CONSOLIDATED_LISTM customerType = payload;
              //  _context.BulkInsert(payload);
                 _context.CONSOLIDATED_LISTM.Add(customerType);
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(customerType));
                // return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: { ex.Message }"));
            }

        }


    }
}