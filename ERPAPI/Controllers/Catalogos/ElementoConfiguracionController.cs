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
    [Route("api/ElementoConfiguracion")]
    [ApiController]
    public class ElementoConfiguracionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ElementoConfiguracionController(ILogger<ElementoConfiguracionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de ElementoConfiguracion paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetElementoConfiguracionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ElementoConfiguracion> Items = new List<ElementoConfiguracion>();
            try
            {
                var query = _context.ElementoConfiguracion.AsQueryable();
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
        /// Obtiene el Listado de ElementoConfiguraciones 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetElementoConfiguracion()
        {
            List<ElementoConfiguracion> Items = new List<ElementoConfiguracion>();
            try
            {
                Items = await _context.ElementoConfiguracion.Include(e => e.GrupoConfiguracion).ToListAsync();
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
        /// Obtiene los Datos de la Country por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetElementoConfiguracionById(Int64 Id)
        {
            ElementoConfiguracion Items = new ElementoConfiguracion();
            try
            {
                Items = await _context.ElementoConfiguracion.Where(q => q.Id == Id).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");

            }

            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene un rol , filtrado por su Nombre.
        /// </summary>
        /// <param name="ElementoConfiguracionName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ElementoConfiguracionName}")]
        public async Task<ActionResult> GetElementoConfiguracionByName(String ElementoConfiguracionName)
        {
            try
            {
                ElementoConfiguracion Items = await _context.ElementoConfiguracion.Where(q => q.Nombre == ElementoConfiguracionName).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        /// <summary>
        /// Obtiene los Datos de la tabla Elementos de Configuración por clasificación de Grupos.
        /// </summary>
        /// <param name="GrupoConfiguracionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GrupoConfiguracionId}")]
        public async Task<ActionResult> GetElementosConfiguracionByGrupoConfiguracion(Int64 GrupoConfiguracionId)
        {
            List<ElementoConfiguracion> Items = new List<ElementoConfiguracion>();
            try
            {
                if (GrupoConfiguracionId == 0)
                {
                    Items = await _context.ElementoConfiguracion.ToListAsync();
                    Items = Items.OrderByDescending(p => p.Id).ToList();
                    return await Task.Run(() => Ok(Items));
                }
                else if (GrupoConfiguracionId > 0)
                {
                    Items = await _context.ElementoConfiguracion.Where(q => q.Idconfiguracion == GrupoConfiguracionId).ToListAsync();
                    Items = Items.OrderByDescending(p => p.Id).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));

        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetElementoConfiguracionByIdConfiguracion(Int64 Id)
        {
            List<ElementoConfiguracion> Items = new List<ElementoConfiguracion>();
            try
            {
                Items = await _context.ElementoConfiguracion.Where(q => q.Idconfiguracion == Id).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva ElementoConfiguracion
        /// </summary>
        /// <param name="_ElementoConfiguracion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ElementoConfiguracion>> Insert([FromBody]ElementoConfiguracion _ElementoConfiguracion)
        {
            ElementoConfiguracion _ElementoConfiguracionq = new ElementoConfiguracion();
            try
            {
                _ElementoConfiguracionq = _ElementoConfiguracion;
                _context.ElementoConfiguracion.Add(_ElementoConfiguracionq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_ElementoConfiguracionq);
        }

        /// <summary>
        /// Actualiza la ElementoConfiguracion
        /// </summary>
        /// <param name="_ElementoConfiguracion"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ElementoConfiguracion>> Update([FromBody]ElementoConfiguracion _ElementoConfiguracion)
        {
            ElementoConfiguracion _ElementoConfiguracionq = _ElementoConfiguracion;
            try
            {
                _ElementoConfiguracionq = await (from c in _context.ElementoConfiguracion
                                 .Where(q => q.Id == _ElementoConfiguracion.Id)
                                                 select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ElementoConfiguracionq).CurrentValues.SetValues((_ElementoConfiguracion));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ElementoConfiguracionq));
        }

        /// <summary>
        /// Elimina una ElementoConfiguracion       
        /// </summary>
        /// <param name="_ElementoConfiguracion"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ElementoConfiguracion _ElementoConfiguracion)
        {
            ElementoConfiguracion _ElementoConfiguracionq = new ElementoConfiguracion();
            try
            {
                _ElementoConfiguracionq = _context.ElementoConfiguracion
                .Where(x => x.Id == (Int64)_ElementoConfiguracion.Id)
                .FirstOrDefault();

                _context.ElementoConfiguracion.Remove(_ElementoConfiguracionq);


                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
               new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_ElementoConfiguracionq));

        }
        /// <summary>
        /// Obtiene los Datos de Puesto por medio del Nombre enviado.
        /// </summary>
        /// <param name="Nombre"></param>
        /// /// <param name="Idconfiguracion"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Nombre}/{Idconfiguracion}")]
        public async Task<IActionResult> GetElemntoConfiguracionByNombre(String Nombre, Int64 Idconfiguracion)
        {
            ElementoConfiguracion Items = new ElementoConfiguracion();
            try
            {
                Items = await _context.ElementoConfiguracion.Where(q => q.Nombre == Nombre && q.Idconfiguracion == Idconfiguracion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]/{Id}/{Valor}/{usuario}")]
        public async Task<ActionResult> UpdateElementoConfiguracionValorDecimal(long Id, double Valor, string usuario)
        {
            try
            {
                ElementoConfiguracion elemento = await _context.ElementoConfiguracion.FirstOrDefaultAsync(e => e.Id == Id);
                elemento.Valordecimal = Valor;
                elemento.FechaModificacion = DateTime.Now;
                elemento.UsuarioModificacion = usuario;
                await _context.SaveChangesAsync();
                return Ok(elemento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Ocurrio un error al actualizar el elemento configuración No. " + Id);
                return BadRequest(ex);
            }
        }



    }
}