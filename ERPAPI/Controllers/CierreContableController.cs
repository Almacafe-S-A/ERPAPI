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
        [HttpPost("[action]")]
        public async Task<IActionResult> EjecutarCierreContable([FromBody]BitacoraCierreContable pBitacoraCierre)
        {
            try
            {
                BitacoraCierreContable existeCierre = await _context.BitacoraCierreContable.Where(b => b.FechaCierre.Date==pBitacoraCierre.FechaCierre.Date).FirstOrDefaultAsync();
                if (existeCierre != null)
                {
                    return await Task.Run(() => BadRequest("Ya existe un Cierre Contable para esta Fecha"));
                }
                BitacoraCierreContable cierre = new BitacoraCierreContable
                {
                    FechaCierre = pBitacoraCierre.FechaCierre.Date,
                    FechaCreacion = DateTime.Now,
                    Estatus = "PENDIENTE",
                    EstatusId = 1,
                    UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                    UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                    FechaModificacion = DateTime.Now,


                };
                _context.BitacoraCierreContable.Add(cierre);

                //Paso 1
                BitacoraCierreProcesos proceso1 = new BitacoraCierreProcesos
                {
                    IdBitacoraCierre = cierre.Id,
                    //IdProceso = 1,
                    Estatus = "PENDIENTE",
                    Proceso = "CALCULO SALDOS",
                    PasoCierre = 1,
                    UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                    UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                    FechaModificacion = DateTime.Now,
                    FechaCierre = DateTime.Now,
                    FechaCreacion = DateTime.Now,

                };
                //Paso2
                BitacoraCierreProcesos proceso2 = new BitacoraCierreProcesos
                {
                    IdBitacoraCierre = cierre.Id,
                    //IdProceso = 1,
                    Estatus = "PENDIENTE",
                    Proceso = "PROCESO 2",
                    PasoCierre = 2,
                    UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                    UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                    FechaModificacion = DateTime.Now,
                    FechaCierre = DateTime.Now,
                    FechaCreacion = DateTime.Now,

                };
                _context.BitacoraCierreProceso.Add(proceso1);
                _context.BitacoraCierreProceso.Add(proceso2);

                _context.SaveChanges();

                List< BitacoraCierreProcesos> spCierre = await _context.BitacoraCierreProceso.FromSql("Cierres @p0, @p1, @p2", "2000/01/01", pBitacoraCierre.FechaCierre, cierre.Id).ToListAsync();
                return await Task.Run(() => Ok(spCierre));
            }
            catch (Exception ex)
            {

                return await Task.Run(()=> BadRequest(ex.Message));
            }
            
        }
    }
}