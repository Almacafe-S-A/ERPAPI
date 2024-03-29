﻿/********************************************************************************************************
-- NAME   :  CRUDUserClaims
-- PROPOSE:  show relation UserClaims
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
3.0                  03/01/2020          Marvin.Guillen                     Changes of avoid duplicate
2.0                  15/07/2019          Freddy.Chinchilla                  Changes of Numeracion SAR
1.0                  04/06/2019          Freddy.Chinchilla                  Creation of Controller
********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/UserClaims")]
    [ApiController]
    public class UserClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UserClaimsController(ILogger<UserClaimsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de AspNetUserClaimses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetApplicationUserClaim()
        {
            List<ApplicationUserClaim> Items = new List<ApplicationUserClaim>();
            try
            {
                Items = await _context.UserClaims.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la AspNetUserClaims por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetAspNetUserClaimsById(Int64 Id)
        {
            ApplicationUserClaim Items = new ApplicationUserClaim();
            try
            {
                Items = await _context.UserClaims.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 Id )
        {
            try
            {
                Int32 Items = 0;//await _context.Branch.Where(a => a.CurrencyId == CurrencyId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
        /// <summary>
        /// Inserta una nueva AspNetUserClaims
        /// </summary>
        /// <param name="_AspNetUserClaims"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserClaim>> Insert([FromBody]ApplicationUserClaim _AspNetUserClaims)
        {
            ApplicationUserClaim _AspNetUserClaimsq = new ApplicationUserClaim();
            try
            {
                _AspNetUserClaimsq = _AspNetUserClaims;
                _context.UserClaims.Add(_AspNetUserClaimsq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_AspNetUserClaimsq));
        }

        /// <summary>
        /// Actualiza la AspNetUserClaims
        /// </summary>
        /// <param name="_AspNetUserClaims"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ApplicationUserClaim>> Update([FromBody]ApplicationUserClaim _AspNetUserClaims)
        {
            ApplicationUserClaim _AspNetUserClaimsq = new ApplicationUserClaim();
            try
            {
                _AspNetUserClaimsq = await (from c in _context.UserClaims
                                 .Where(q => q.Id == _AspNetUserClaims.Id)
                                            select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_AspNetUserClaimsq).CurrentValues.SetValues((_AspNetUserClaims));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.UserClaims.Update(_AspNetUserClaimsq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_AspNetUserClaimsq));
        }
        /// <summary>
        /// Consultar una nueva AspNetUserClaims  en la Tabla para evitar duplicarla
        /// </summary>
        /// <param name="_ApplicationUserClaimP"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserClaim>> GetAspNetUserClaimsByUser([FromBody]ApplicationUserClaim _ApplicationUserClaimP)
        {
            ApplicationUserClaim _Item = new ApplicationUserClaim();
            try
            {

                _Item = _context.ApplicationUserClaim.Where(z => z.ClaimType == _ApplicationUserClaimP.ClaimType
                  && z.ClaimValue == _ApplicationUserClaimP.ClaimValue && z.UserId == _ApplicationUserClaimP.UserId
                   ).FirstOrDefault();




            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Item));
        }

        /// <summary>
        /// Elimina una AspNetUserClaims       
        /// </summary>
        /// <param name="_AspNetUserClaims"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ApplicationUserClaim _AspNetUserClaims)
        {
            ApplicationUserClaim _AspNetUserClaimsq = new ApplicationUserClaim();
            try
            {
                _AspNetUserClaimsq = _context.UserClaims
                .Where(x => x.Id == (Int64)_AspNetUserClaims.Id)
                .FirstOrDefault();

                _context.UserClaims.Remove(_AspNetUserClaimsq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_AspNetUserClaimsq));

        }







    }
}