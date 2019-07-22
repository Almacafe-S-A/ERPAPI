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
    [Route("api/State")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public StateController(ILogger<StateController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Statees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetState()
        {
            List<State> Items = new List<State>();
            try
            {
                Items = await _context.State.ToListAsync();
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
        /// Obtiene los Datos de la State por medio del Id enviado.
        /// </summary>
        /// <param name="StateId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{StateId}")]
        public async Task<IActionResult> GetStateById(Int64 StateId)
        {
            State Items = new State();
            try
            {
                Items = await _context.State.Where(q => q.StateId == StateId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva State
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<State>> Insert([FromBody]State _State)
        {
            State _Stateq = new State();
            try
            {
                _Stateq = _State;
                _context.State.Add(_Stateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Stateq));
        }

        /// <summary>
        /// Actualiza la State
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<State>> Update([FromBody]State _State)
        {
            State _Stateq = _State;
            try
            {
                _Stateq = await (from c in _context.State
                                 .Where(q => q.StateId == _State.StateId)
                                 select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Stateq).CurrentValues.SetValues((_State));

                //_context.State.Update(_Stateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Stateq));
        }

        /// <summary>
        /// Elimina una State       
        /// </summary>
        /// <param name="_State"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]State _State)
        {
            State _Stateq = new State();
            try
            {
                _Stateq = _context.State
                .Where(x => x.StateId == (Int64)_State.StateId)
                .FirstOrDefault();

                _context.State.Remove(_Stateq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Stateq));

        }







    }
}