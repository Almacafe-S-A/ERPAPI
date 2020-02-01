﻿using System;
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

        /// <summary>
        /// Obtiene el Listado de Estados paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEstadosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Estados> Items = new List<Estados>();
            try
            {
                var query = _context.Estados.AsQueryable();
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

        // GET: Estados

        [HttpGet("Get")]
        public async Task<ActionResult> GetEstados()
        {
            try
            {
                
                List<Estados> Items = await _context.Estados.ToListAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
           
        }

        [HttpGet("[action]/{IdEstado}")]
        public async Task<ActionResult<Estados>> GetEstadosById(Int64 IdEstado)
        {
            try
            {
                
                Estados Items = await _context.Estados.Where(q=>q.IdEstado==IdEstado).FirstOrDefaultAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetEstadoByNombreEstado(String NombreEstado, Int64 IdConfi)
        {
            Estados Items = new Estados();
            try
            {
                Items = await _context.Estados.Where(q => q.NombreEstado == NombreEstado && q.IdGrupoEstado == IdConfi).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la tabla Grupo Estados.
        /// </summary>
        [HttpGet("[action]")]
        public async Task<ActionResult> GetGrupoEstados()
        {
            try
            {
                List<GrupoEstado> Items = await _context.GrupoEstado.ToListAsync();
                return await Task.Run(() => Ok(Items));

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
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }


        /// <summary>
        /// Inserta una nueva Estados
        /// </summary>
        /// <param name="_Estados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Estados>> Insert([FromBody]Estados _Estados)
        {
            Estados _Estadosq = new Estados();
            try
            {
                _Estadosq = _Estados;
                _context.Estados.Add(_Estadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Estadosq));
        }

        /// <summary>
        /// Actualiza la Estados
        /// </summary>
        /// <param name="_Estados"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Estados>> Update([FromBody]Estados _Estados)
        {
            Estados _Estadosq = _Estados;
            try
            {
                _Estadosq = await (from c in _context.Estados
                                 .Where(q => q.IdEstado == _Estados.IdEstado)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Estadosq).CurrentValues.SetValues((_Estados));

                //_context.Estados.Update(_Estadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Estadosq));
        }

        /// <summary>
        /// Elimina una Estados       
        /// </summary>
        /// <param name="_Estados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Estados>> Delete([FromBody]Estados _Estados)
        {
            Estados _Estadosq = new Estados();
            try
            {
                _Estadosq = _context.Estados
                .Where(x => x.IdEstado == (Int64)_Estados.IdEstado)
                .FirstOrDefault();

                _context.Estados.Remove(_Estadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Estadosq));

        }

        /// <summary>
        /// Obtiene los Datos de la tabla Estados por clasificación de Grupos.
        /// </summary>
        /// <param name="GrupoEstadoId"></param>
        /// <param name="GrupoConfiguracionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GrupoEstadoId}/{GrupoConfiguracionId}")]
        public async Task<ActionResult> GetEstadosByGrupoEstado(Int64 GrupoEstadoId, Int64 GrupoConfiguracionId)
        {
            List<Estados> Items = new List<Estados>();
            try
            {
                if (GrupoEstadoId == 0 && GrupoConfiguracionId == 0)
                {
                    Items = await _context.Estados.ToListAsync();
                    Items = Items.OrderByDescending(p => p.IdEstado).ToList();
                    return await Task.Run(() => Ok(Items));
                }
                else if (GrupoEstadoId > 0 && GrupoConfiguracionId > 0 && GrupoEstadoId == GrupoConfiguracionId)
                {
                    Items = await _context.Estados.Where(q => q.IdGrupoEstado == GrupoEstadoId).ToListAsync();
                    Items = Items.OrderByDescending(p => p.IdEstado).ToList();
                }
                else if (GrupoEstadoId > 0 && GrupoConfiguracionId > 0)
                {
                    List<Estados> ItemGruposDiferentes = new List<Estados>();
                    Items = await _context.Estados.ToListAsync();
                    foreach (var item in Items)
                    {
                        if (item.IdGrupoEstado.Equals(GrupoEstadoId))
                        {
                            ItemGruposDiferentes.Add(item);
                        } 
                    }
                    foreach (var item in Items)
                    {
                        if (item.IdGrupoEstado.Equals(GrupoConfiguracionId))
                        {
                            ItemGruposDiferentes.Add(item);
                        }
                    }
                    ItemGruposDiferentes = ItemGruposDiferentes.OrderByDescending(p => p.IdEstado).ToList();
                    return await Task.Run(() => Ok(ItemGruposDiferentes));
                }
                else if (GrupoConfiguracionId > 0 && GrupoEstadoId == 0)
                {
                    List<Estados> ItemsRemovePrepend = new List<Estados>();
                    Items = await _context.Estados.ToListAsync();
                    Items = Items.OrderByDescending(p => p.IdEstado).ToList();
                    ItemsRemovePrepend = Items.FindAll(q => q.IdGrupoEstado == GrupoConfiguracionId);
                    foreach (var item in ItemsRemovePrepend)
                    {
                        Items.Remove(item);
                        Items.Insert(0,item);
                    }
                }
                else if (GrupoConfiguracionId == 0 && GrupoEstadoId > 0)
                {
                    List<Estados> ItemsRemovePrepend = new List<Estados>();
                    Items = await _context.Estados.ToListAsync();
                    Items = Items.OrderByDescending(p => p.IdEstado).ToList();
                    ItemsRemovePrepend = Items.FindAll(q => q.IdGrupoEstado == GrupoEstadoId);
                    
                    foreach (var item in ItemsRemovePrepend)
                    {
                        Items.Remove(item);
                        Items.Insert(0,item);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));

        }

    }
}