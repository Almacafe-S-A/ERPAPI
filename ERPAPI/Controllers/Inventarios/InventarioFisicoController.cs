using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioFisicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public InventarioFisicosController(ILogger<InventarioFisicosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        ///// <summary>
        ///// Obtiene el Listado de InventarioFisico paginado
        ///// </summary>
        ///// <returns></returns>    
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetInventarioFisicoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        //{
        //    List<InventarioFisico> Items = new List<InventarioFisico>();
        //    try
        //    {
        //        var query = _context.InventarioFisico.AsQueryable();
        //        var totalRegistro = query.Count();

        //        Items = await query
        //           .Skip(cantidadDeRegistros * (numeroDePagina - 1))
        //           .Take(cantidadDeRegistros)
        //            .ToListAsync();

        //        Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
        //        Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }


        //    return await Task.Run(() => Ok(Items));
        //}

        ///// <summary>
        ///// Obtiene el Listado de InventarioFisicoes 
        ///// El estado define cuales son los cai activos
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetInventarioFisico()
        //{
        //    List<InventarioFisico> Items = new List<InventarioFisico>();
        //    try
        //    {
        //        Items = await _context.InventarioFisico.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //    //  int Count = Items.Count();
        //    return await Task.Run(() => Ok(Items));
        //}


       



        ///// <summary>
        ///// Obtiene los Datos de la InventarioFisico por medio del Id enviado.
        ///// </summary>
        ///// <param name="InventarioFisicoId"></param>
        ///// <returns></returns>
        //[HttpGet("[action]/{InventarioFisicoId}")]
        //public async Task<IActionResult> GetInventarioFisicoById(Int64 InventarioFisicoId)
        //{
        //    InventarioFisico Items = new InventarioFisico();
        //    try
        //    {
        //        Items = await _context.InventarioFisico.Where(q => q.Id == InventarioFisicoId).FirstOrDefaultAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }


        //    return await Task.Run(() => Ok(Items));
        //}



        ///// <summary>
        ///// Inserta una nueva InventarioFisico
        ///// </summary>
        ///// <param name="_InventarioFisico"></param>
        ///// <returns></returns>
        //[HttpPost("[action]")]
        //public async Task<ActionResult<InventarioFisico>> Insert([FromBody]InventarioFisico _InventarioFisico)
        //{
        //    InventarioFisico _InventarioFisicoq = new InventarioFisico();
        //    try
        //    {
        //        using (var transaction = _context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                _context.InventarioFisico.Add(_InventarioFisico);
        //                //await _context.SaveChangesAsync();

        //                foreach (var item in _InventarioFisico.InventarioFisicoLines)
        //                {
        //                    item.InventarioFisico = _InventarioFisico.Id;
        //                    _context.InventarioFisicoLines.Add(item);
        //                }
        //                await _context.SaveChangesAsync();

        //                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
        //                {
        //                    IdOperacion = _InventarioFisico.Id,
        //                    DocType = "InventarioFisico",
        //                    ClaseInicial =
        //                      Newtonsoft.Json.JsonConvert.SerializeObject(_InventarioFisico, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
        //                    ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_InventarioFisico, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
        //                    Accion = "Insert",
        //                    FechaCreacion = DateTime.Now,
        //                    FechaModificacion = DateTime.Now,
        //                    UsuarioCreacion = _InventarioFisico.UsuarioCreacion,
        //                    UsuarioModificacion = _InventarioFisico.UsuarioModificion,
        //                    UsuarioEjecucion = _InventarioFisico.UsuarioModificion,

        //                });

        //                await _context.SaveChangesAsync();

        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw ex;
        //            }

        //        }
        //        //_InventarioFisicoq = _InventarioFisico;
        //        //_context.InventarioFisico.Add(_InventarioFisicoq);
        //        //await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //    return await Task.Run(() => Ok(_InventarioFisicoq));
        //}

        ///// <summary>
        ///// Actualiza la InventarioFisico
        ///// </summary>
        ///// <param name="_InventarioFisico"></param>
        ///// <returns></returns>
        //[HttpPut("[action]")]
        //public async Task<ActionResult<InventarioFisico>> Update([FromBody]InventarioFisico _InventarioFisico)
        //{
        //    InventarioFisico _InventarioFisicoq = _InventarioFisico;
        //    try
        //    {
        //        _InventarioFisicoq = await (from c in _context.InventarioFisico
        //                         .Where(q => q.Id == _InventarioFisico.Id)
        //                                 select c
        //                        ).FirstOrDefaultAsync();

        //        _context.Entry(_InventarioFisicoq).CurrentValues.SetValues((_InventarioFisico));

        //        //_context.InventarioFisico.Update(_InventarioFisicoq);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //    return await Task.Run(() => Ok(_InventarioFisicoq));
        //}

        ///// <summary>
        ///// Elimina una InventarioFisico       
        ///// </summary>
        ///// <param name="_InventarioFisico"></param>
        ///// <returns></returns>
        //[HttpPost("[action]")]
        //public async Task<IActionResult> Delete([FromBody]InventarioFisico _InventarioFisico)
        //{
        //    InventarioFisico _InventarioFisicoq = new InventarioFisico();
        //    try
        //    {
        //        _InventarioFisicoq = _context.InventarioFisico
        //        .Where(x => x.Id == (Int64)_InventarioFisico.Id)
        //        .FirstOrDefault();

        //        _context.InventarioFisico.Remove(_InventarioFisicoq);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //    return await Task.Run(() => Ok(_InventarioFisicoq));

        //}







    }
}
