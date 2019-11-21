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

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    BitacoraCierreContable existeCierre = await _context.BitacoraCierreContable.Where(b => b.FechaCierre.Date == pBitacoraCierre.FechaCierre.Date).FirstOrDefaultAsync();
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
                        Proceso = "HISTORICOS",
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
                        Proceso = "VALOR MAXIMO CERTIFICADO DE DEPOSITO",
                        PasoCierre = 2,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = DateTime.Now,
                        FechaCreacion = DateTime.Now,

                    };

                    //Paso3
                    BitacoraCierreProcesos proceso3 = new BitacoraCierreProcesos
                    {
                        IdBitacoraCierre = cierre.Id,
                        //IdProceso = 1,
                        Estatus = "PENDIENTE",
                        Proceso = "POLIZAS DE SEGURO VENCIDAS",
                        PasoCierre = 2,
                        UsuarioCreacion = User.Claims.FirstOrDefault().Value.ToString(),
                        UsuarioModificacion = User.Claims.FirstOrDefault().Value.ToString(),
                        FechaModificacion = DateTime.Now,
                        FechaCierre = DateTime.Now,
                        FechaCreacion = DateTime.Now,

                    };
                    _context.BitacoraCierreProceso.Add(proceso1);
                    _context.BitacoraCierreProceso.Add(proceso2);
                    _context.BitacoraCierreProceso.Add(proceso3);

                    List<InsurancePolicy> insurancePolicies = _context.InsurancePolicy.Where(i => i.PolicyDueDate < DateTime.Now).ToList();

                    if (insurancePolicies.Count > 0)
                    {
                        foreach (var item in insurancePolicies)
                        {
                            item.Status = "INACTIVA";
                           
                            

                        }
                        proceso3.Estatus = "FINALIZADO";
                        //proceso3.Mensaje = "FINALIZADO No se encontraron Polizas Vencidas";
                       
                    }
                    else
                    {
                        proceso3.Estatus = "FINALIZADO";
                    }

                    _context.InsurancePolicy.UpdateRange(insurancePolicies);
                



                    /////////////Fin del Paso 3

                _context.SaveChanges();

                    //List< BitacoraCierreProcesos> spCierre = await _context.BitacoraCierreProceso.FromSql("Cierres @p0, @p1, @p2", pBitacoraCierre.FechaCierre, cierre.Id).ToListAsync();
                    _context.Database.ExecuteSqlCommand("Cierres @p0, @p1", pBitacoraCierre.FechaCierre, cierre.Id);

                    transaction.Commit();
                    return await Task.Run(() => Ok());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return await Task.Run(() => BadRequest(ex.Message));
                }




            }

        }
    }
}