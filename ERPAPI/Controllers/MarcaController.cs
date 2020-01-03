/********************************************************************************************************
-- NAME   :  CRUDMarca
-- PROPOSE:  show relation Marca
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
7.0                  02/01/2020          Marvin.Guillen                     Changes of Validation to duplicated and delete
6.0                  10/12/2019          Maria.Funez                        Changes of add metodo to record active
5.0                  16/09/2019          Freddy.Chinchilla                  Changes of reparacion de llave forranea
4.0                  16/09/2019          Carlos.Castillo                    Changes of merger branch master
3.0                  16/09/2019          Freddy.Chinchilla                  Changes of pagination of controller
2.0                  16/09/2019          Carlos.Castillo                    Changes of rename de table
1.0                  09/09/2019          Carlos.Castillo                    Creation of Controller
********************************************************************************************************/

using System;
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
    [Route("api/Marca")]
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MarcaController(ILogger<MarcaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Marcas paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarcasPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Marca> Items = new List<Marca>();
            try
            {
                var query = _context.Marca.AsQueryable();
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

        [HttpGet("[action]/{idestado}")]
        public async Task<ActionResult> GetMarcaByEstado(Int64 idestado)
        {
            try
            {
                List<Marca> Items = await _context.Marca.Where(q => q.IdEstado == idestado).ToListAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        // GET: api/Marca
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMarca()
        {
            List<Marca> Items = new List<Marca>();
            try
            {
                Items = await _context.Marca.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/MarcaGetMarcaById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMarcaById(int Id)
        {
            Marca Items = new Marca();
            try
            {
                Items = await _context.Marca.Where(q => q.MarcaId.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Obtiene los datos de la Marca con el descripcion enviado
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Description}")]
        public async Task<ActionResult<Linea>> GetMarcaByDescription(String Description)
        {
            Marca Items = new Marca();
            try
            {
                Items = await _context.Marca.Where(q => q.Description == Description).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }
        /// <summary>
        /// Obtiene los Datos de la Marca por medio del Id enviado. Validación de eliminar
        /// </summary>
        /// <param name="MarcaId"></param>
        /// <returns></returns>

        [HttpGet("[action]/{MarcaId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 MarcaId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = await _context.Product.Where(a => a.MarcaId == MarcaId)
                                                 .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }



        [HttpPost("[action]")]
        public async Task<ActionResult<Marca>> Insert([FromBody]Marca payload)
        {
            Marca Marca = payload;

            try
            {
                _context.Marca.Add(Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Marca));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Marca>> Update([FromBody]Marca _Marca)
        {

            try
            {
                Marca Marcaq = (from c in _context.Marca
                   .Where(q => q.MarcaId == _Marca.MarcaId)
                                                select c
                     ).FirstOrDefault();

                _Marca.FechaCreacion = Marcaq.FechaCreacion;
                _Marca.UsuarioCreacion = Marcaq.UsuarioCreacion;

                _context.Entry(Marcaq).CurrentValues.SetValues((_Marca));
                // _context.Marca.Update(_Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Marca));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Marca payload)
        {
            Marca Marca = new Marca();
            try
            {
                Marca = _context.Marca
                .Where(x => x.MarcaId == (int)payload.MarcaId)
                .FirstOrDefault();
                _context.Marca.Remove(Marca);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Marca));

        }



    }
}