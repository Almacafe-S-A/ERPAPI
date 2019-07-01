﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;

using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Dependientes")]
    [ApiController]
    public class DependientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DependientesController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Dependientes
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Dependientes>>> GetDependientes()
        {
            return await _context.Dependientes.ToListAsync();
        }

        // GET: api/Dependientes/5
        [HttpGet("[action]/{Id}")]
        public async Task<ActionResult<Dependientes>> GetDependientes(long id)
        {
            var dependientes = await _context.Dependientes.FindAsync(id);

            if (dependientes == null)
            {
                return await Task.Run(() => NotFound());
            }

            return await Task.Run(() => dependientes);
        }

        // PUT: api/Dependientes/5
        [HttpPut("[action]")]
        public async Task<ActionResult<Dependientes>> Update([FromBody]Dependientes _Dependientes)
        {
            Dependientes _Dependientesq = _Dependientes;
            try
            {
                _Dependientesq = await (from c in _context.Dependientes
                                 .Where(q => q.IdDependientes == _Dependientesq.IdDependientes)
                                        select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Dependientesq).CurrentValues.SetValues((_Dependientes));

                //_context.Escala.Update(_Escalaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }

        // POST: api/Dependientes
        [HttpPost("[action]")]
        public async Task<ActionResult<Dependientes>> Insert([FromBody]Dependientes _Dependientes)
        {
            Dependientes _Dependientesq = new Dependientes();
            try
            {
                _Dependientesq = _Dependientes;
                _context.Dependientes.Add(_Dependientesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }


        // DELETE: api/Dependientes/5
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Dependientes _Dependientes)
        {
            Dependientes _Dependientesq = new Dependientes();
            try
            {
                _Dependientesq = _context.Dependientes
                .Where(x => x.IdDependientes == (Int64)_Dependientes.IdDependientes)
                .FirstOrDefault();

                _context.Dependientes.Remove(_Dependientesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Dependientesq);
        }

        private bool DependientesExists(long id)
        {
            return _context.Dependientes.Any(e => e.IdDependientes == id);
        }
    }
}