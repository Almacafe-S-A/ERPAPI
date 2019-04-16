using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace ERPAPI.Controllers
{
     [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Estados")]
    [ApiController]
    public class EstadosController : Controller
    {

        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public EstadosController(ILogger<EstadosController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger ;
        }

        // GET: Estados
               
        [HttpGet]
        public async Task<ActionResult> GetEstados()
        {
            try
            {
                List<Estados> Items = await _context.Estados.ToListAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
               return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
        }


        [HttpGet("[action]/{idgrupoestado}")]
        public async Task<ActionResult> GetEstadosByGrupo(Int64 idgrupoestado)
        {
            try
            {
                List<Estados> Items = await _context.Estados.Where(q=>q.IdGrupoEstado==idgrupoestado).ToListAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }




    }
}