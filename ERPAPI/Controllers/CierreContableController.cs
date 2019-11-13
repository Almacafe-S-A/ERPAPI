using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CierreContableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CierreContableController(ILogger<CierreContableController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Realiza un Cierre Contable
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> EjecutarCierreContable()
        {

            BitacoraCierreContable cierre = new BitacoraCierreContable {
                FechaCierre = DateTime.Now,
                FechaCreacion = DateTime.Now,
                Estatus = "ERROR",
                EstatusId = 1,
                UsuarioCreacion =  User.Claims.FirstOrDefault().Value.ToString(),
                UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                FechaModificacion = DateTime.Now,             


            };
            _context.BitacoraCierreContable.Add(cierre);
            _context.SaveChanges();
            return await Task.Run(() => Ok());
        }
    }
}