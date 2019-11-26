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
    [Route("api/City")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CityController(ILogger<CityController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de City paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCityPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<City> Items = new List<City>();
            try
            {
                var query = _context.City.AsQueryable();
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
           
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de Cityes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCity()
        {
            List<City> Items = new List<City>();
            try
            {
                Items = await _context.City.ToListAsync();
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
        /// Obtiene los Datos de la City por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCityById(Int64 Id)
        {
            City Items = new City();
            try
            {
                Items = await _context.City.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCityByName(String Name, Int64 StateId)
        {
            City Items = new City();
            try
            {
                Items = await _context.City.Where(q => q.Name == Name && q.StateId == StateId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva City
        /// </summary>
        /// <param name="_City"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<City>> Insert([FromBody]City _City)
        {
            City _Cityq = new City();
            try
            {
                _Cityq = _City;
                _context.City.Add(_Cityq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Cityq));
        }

        /// <summary>
        /// Actualiza la City
        /// </summary>
        /// <param name="_City"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<City>> Update([FromBody]City _City)
        {
            City _Cityq = _City;
            try
            {
                _Cityq = await (from c in _context.City
                                 .Where(q => q.Id == _City.Id)
                                select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Cityq).CurrentValues.SetValues((_City));

                //_context.City.Update(_Cityq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Cityq));
        }

        /// <summary>
        /// Elimina una City       
        /// </summary>
        /// <param name="_City"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]City _City)
        {
            City _Cityq = new City();
            try
            {
                _Cityq = _context.City
                .Where(x => x.Id == (Int64)_City.Id)
                .FirstOrDefault();

                _context.City.Remove(_Cityq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Cityq));

        }







    }
}