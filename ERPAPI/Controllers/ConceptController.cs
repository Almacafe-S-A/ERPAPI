using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
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
    [Route("api/Concept")]
    [ApiController]
    public class ConceptController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ConceptController(ILogger<ConceptController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Concept paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConceptPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Concept> Items = new List<Concept>();
            try
            {
                var query = _context.Concept.AsQueryable();
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
        /// Obtiene el Listado de Conceptes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetConcept()
        {
            List<Concept> Items = new List<Concept>();
            try
            {
                Items = await _context.Concept.ToListAsync();
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
        /// Obtiene los Datos de la Concept por medio del Id enviado.
        /// </summary>
        /// <param name="ConceptId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ConceptId}")]
        public async Task<IActionResult> GetConceptById(Int64 ConceptId)
        {
            Concept Items = new Concept();
            try
            {
                Items = await _context.Concept.Where(q => q.ConceptId == ConceptId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Concept
        /// </summary>
        /// <param name="_Concept"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Concept>> Insert([FromBody]Concept _Concept)
        {
            Concept _Conceptq = new Concept();
            try
            {
                _Conceptq = _Concept;
                _context.Concept.Add(_Conceptq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Conceptq));
        }

        /// <summary>
        /// Actualiza la Concept
        /// </summary>
        /// <param name="_Concept"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Concept>> Update([FromBody]Concept _Concept)
        {
            Concept _Conceptq = _Concept;
            try
            {
                _Conceptq = await (from c in _context.Concept
                                 .Where(q => q.ConceptId == _Concept.ConceptId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Conceptq).CurrentValues.SetValues((_Concept));

                //_context.Concept.Update(_Conceptq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Conceptq));
        }

        /// <summary>
        /// Elimina una Concept       
        /// </summary>
        /// <param name="_Concept"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Concept _Concept)
        {
            Concept _Conceptq = new Concept();
            try
            {
                _Conceptq = _context.Concept
                .Where(x => x.ConceptId == (Int64)_Concept.ConceptId)
                .FirstOrDefault();

                _context.Concept.Remove(_Conceptq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Conceptq));

        }







    }
}