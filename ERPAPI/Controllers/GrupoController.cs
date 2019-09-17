﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Grupo")]
    public class GrupoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GrupoController(ILogger<GrupoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Grupo
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGrupo()
        {
            List<Grupo> Items = new List<Grupo>();
            try
            {
                Items = await _context.Grupo.Include(c => c.Linea).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/GrupoGetGrupoById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetGrupoById(int Id)
        {
            Grupo Items = new Grupo();
            try
            {
                Items = await _context.Grupo.Include(c => c.Linea).Where(q => q.GrupoId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Grupo>> Insert([FromBody]Grupo payload)
        {
            Grupo Grupo = payload;

            try
            {
                _context.Grupo.Add(Grupo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Grupo));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Grupo>> Update([FromBody]Grupo _Grupo)
        {

            try
            {
                Grupo Grupoq = (from c in _context.Grupo
                   .Where(q => q.GrupoId == _Grupo.GrupoId)
                                                select c
                     ).FirstOrDefault();

                _Grupo.FechaCreacion = Grupoq.FechaCreacion;
                _Grupo.UsuarioCreacion = Grupoq.UsuarioCreacion;

                _context.Entry(Grupoq).CurrentValues.SetValues((_Grupo));
                // _context.Grupo.Update(_Grupo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Grupo));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Grupo payload)
        {
            Grupo Grupo = new Grupo();
            try
            {
                Grupo = _context.Grupo
                .Where(x => x.GrupoId == (int)payload.GrupoId)
                .FirstOrDefault();
                _context.Grupo.Remove(Grupo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Grupo));

        }



    }
}