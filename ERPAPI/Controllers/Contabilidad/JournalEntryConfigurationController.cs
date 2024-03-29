﻿using System;
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
    [Route("api/JournalEntryConfiguration")]
    [ApiController]
    public class JournalEntryConfigurationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public JournalEntryConfigurationController(ILogger<JournalEntryConfigurationController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de JournalEntryConfiguration paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryConfigurationPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<JournalEntryConfiguration> Items = new List<JournalEntryConfiguration>();
            try
            {
                var query = _context.JournalEntryConfiguration.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
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
        /// Obtiene el Listado de JournalEntryConfigurationes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJournalEntryConfiguration()
        {
            List<JournalEntryConfiguration> Items = new List<JournalEntryConfiguration>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.JournalEntryConfiguration.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.JournalEntryConfigurationId).ToListAsync();
                }
                else
                {
                    Items = await _context.JournalEntryConfiguration.OrderByDescending(b => b.JournalEntryConfigurationId).ToListAsync();
                }
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
        /// Obtiene los Datos de la JournalEntryConfiguration por medio del Id enviado.
        /// </summary>
        /// <param name="JournalEntryConfigurationId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{JournalEntryConfigurationId}")]
        public async Task<IActionResult> GetJournalEntryConfigurationById(Int64 JournalEntryConfigurationId)
        {
            JournalEntryConfiguration Items = new JournalEntryConfiguration();
            try
            {
                Items = await _context.JournalEntryConfiguration.Where(q => q.JournalEntryConfigurationId == JournalEntryConfigurationId).Include(q=>q.JournalEntryConfigurationLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva JournalEntryConfiguration
        /// </summary>
        /// <param name="_JournalEntryConfiguration"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<JournalEntryConfiguration>> Insert([FromBody]JournalEntryConfiguration _JournalEntryConfiguration)
        {
            JournalEntryConfiguration _JournalEntryConfigurationq = new JournalEntryConfiguration();
            try
            {
                _JournalEntryConfigurationq = _JournalEntryConfiguration;
                _context.JournalEntryConfiguration.Add(_JournalEntryConfigurationq);

                foreach (var item in _JournalEntryConfigurationq.JournalEntryConfigurationLine)
                {
                    item.JournalEntryConfigurationId = _JournalEntryConfigurationq.JournalEntryConfigurationId;
                    _context.JournalEntryConfigurationLine.Add(item);
                }

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationq));
        }

        /// <summary>
        /// Actualiza la JournalEntryConfiguration
        /// </summary>
        /// <param name="_JournalEntryConfiguration"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<JournalEntryConfiguration>> Update([FromBody]JournalEntryConfiguration _JournalEntryConfiguration)
        {
            JournalEntryConfiguration _JournalEntryConfigurationq = new JournalEntryConfiguration();
            try
            {
                _JournalEntryConfigurationq = await (from c in _context.JournalEntryConfiguration
                                 .Where(q => q.JournalEntryConfigurationId == _JournalEntryConfiguration.JournalEntryConfigurationId)
                                                     select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_JournalEntryConfigurationq).CurrentValues.SetValues((_JournalEntryConfiguration));

                //_context.JournalEntryConfiguration.Update(_JournalEntryConfigurationq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationq));
        }

        /// <summary>
        /// Elimina una JournalEntryConfiguration       
        /// </summary>
        /// <param name="_JournalEntryConfiguration"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]JournalEntryConfiguration _JournalEntryConfiguration)
        {
            JournalEntryConfiguration _JournalEntryConfigurationq = new JournalEntryConfiguration();
            try
            {
                _JournalEntryConfigurationq = _context.JournalEntryConfiguration
                .Where(x => x.JournalEntryConfigurationId == (Int64)_JournalEntryConfiguration.JournalEntryConfigurationId)
                .FirstOrDefault();

                _context.JournalEntryConfiguration.Remove(_JournalEntryConfigurationq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_JournalEntryConfigurationq));

        }







    }
}