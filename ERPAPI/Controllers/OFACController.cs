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

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/OFAC")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
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
                      .Where(q =>
                           q.lastName.Contains(_sdnListSdnEntryM.lastName)
                           || q.firstName.Contains(_sdnListSdnEntryM.firstName)
                           ||  (q.lastName + q.firstName).Contains(_sdnListSdnEntryM.lastName)
                           || ( q.firstName+ q.lastName ).Contains(_sdnListSdnEntryM.firstName)
                           || (q.lastName + " " + q.firstName).Contains(_sdnListSdnEntryM.lastName)  //+" "+_sdnListSdnEntryM.firstName)
                           || (  q.firstName + " " + q.lastName ).Contains(_sdnListSdnEntryM.firstName)//+" "+ _sdnListSdnEntryM.lastName)
                         
                           );
                _personapornombre = await query
                        .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
                throw ex;
            }

            return await Task.Run(() => Ok(_personapornombre));
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]sdnListM payload)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {                     

                        sdnListM customerType = payload;
                        _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(70));
                        // List<sdnListPublshInformationM> _publis = new List<sdnListPublshInformationM>();
                        // _publis.Add( customerType.publshInformation);
                        //_context.BulkInsert(_publis);
                        // _context.BulkInsert(customerType.sdnEntry);

                        List<sdnListM> rangelist = _context.sdnList.ToList();
                        _context.sdnList.RemoveRange(rangelist);
                       // _context.Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");
                        await _context.SaveChangesAsync();

                        _context.sdnList.Add(customerType);
                        await _context.SaveChangesAsync();                      

                        transaction.Commit();
                        return await Task.Run(() => Ok(customerType));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                  
                }
                // return Ok(customerType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: { ex.Message }"));
            }

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]sdnListM payload)
        {
            try
            {
                List<sdnListM> rangelist = _context.sdnList.ToList();
                _context.sdnList.RemoveRange(rangelist);
               // _context.Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok(rangelist.Count));
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