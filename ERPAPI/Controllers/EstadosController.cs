using System;
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

        // GET: Estados
               
        [HttpGet("Get")]
        public async Task<ActionResult> GetEstados()
        {
            try
            {
                
                List<Estados> Items = await _context.Estados.Include(c => c.GrupoConfiguracion).ToListAsync();
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
                
                Estados Items = await _context.Estados.Where(q=>q.IdEstado==IdEstado).Include(c => c.GrupoConfiguracion).FirstOrDefaultAsync();
                return Ok(Items);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
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





    }
}