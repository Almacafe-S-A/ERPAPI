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
    [Route("api/BoletaDeSalida")]
    [ApiController]
    public class BoletaDeSalidaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BoletaDeSalidaController(ILogger<BoletaDeSalidaController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de BoletaDeSalida
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoletaDeSalidaPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<BoletaDeSalida> Items = new List<BoletaDeSalida>();
            try
            {
                var query = _context.BoletaDeSalida.AsQueryable();
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
        /// Obtiene el Listado de BoletaDeSalidaes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoletaDeSalida()
        {
            List<BoletaDeSalida> Items = new List<BoletaDeSalida>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.BoletaDeSalida.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.BoletaDeSalidaId).ToListAsync();
                }
                else
                {
                    Items = await _context.BoletaDeSalida.OrderByDescending(b => b.BoletaDeSalidaId).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }




        /// <summary>
        /// Obtiene el detalle de BoletaDeSalidaes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{BoletaId}")]
        public async Task<IActionResult> GetBoletaDeSalidaLines(int BoletaId)
        {
            List<BoletaDeSalidaLine> Items = new List<BoletaDeSalidaLine>();
            try
            {
                    Items = await _context.BoletaDeSalidaLines
                        .Where(q =>q.BoletaSalidaId == BoletaId).ToListAsync();
                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        /// <summary>
        /// Obtiene los Datos de la BoletaDeSalida por medio del Id enviado.
        /// </summary>
        /// <param name="BoletaDeSalidaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BoletaDeSalidaId}")]
        public async Task<IActionResult> GetBoletaDeSalidaById(Int64 BoletaDeSalidaId)
        {
            BoletaDeSalida Items = new BoletaDeSalida();
            try
            {
                Items = await _context.BoletaDeSalida.Where(q => q.BoletaDeSalidaId == BoletaDeSalidaId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }



        /// <summary>
        /// Obtiene los Datos de la BoletaDeSalida por medio del Id enviado.
        /// </summary>
        /// <param name="BoletaDeSalidaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BoletaDeSalidaId}")]
        public async Task<IActionResult> GenerarGuiaRemision(Int64 BoletaDeSalidaId)
        {
            BoletaDeSalida boleta = new BoletaDeSalida();
            GuiaRemision guiaRemision = new GuiaRemision();
            try
            {
                boleta = await _context.BoletaDeSalida
                    .Include(i=>i.BoletaDeSalidaLines)
                    .Where(q => q.BoletaDeSalidaId == BoletaDeSalidaId).FirstOrDefaultAsync();


                NumeracionSAR numeracionSAR = _context.NumeracionSAR
                    .Where(q => q.IdEstado == 1
                    && q.DocTypeId == 13
                    && q.FechaLimite < DateTime.Now
                    ).FirstOrDefault();

                if (numeracionSAR == null)
                {
                    return BadRequest("No existe una numeracion SAR Activa o Vigente para la Generacion de las Guias de Remisión");
                }
                guiaRemision = new GuiaRemision {
                    NumeroDocumento =  numeracionSAR.GetNumeroSiguiente(),
                    CAI = numeracionSAR._cai,
                    FechaLimiteEmision = numeracionSAR.FechaLimite,
                    Rango = numeracionSAR.NoInicio + " - " + numeracionSAR.NoFin,
                    CustomerName = boleta.CustomerName,
                    CustomerId = (int)boleta.CustomerId,
                    Transportista = boleta.Motorista,
                    Placa = boleta.Placa,
                    UsuarioCreacion = User.Identity.Name,
                    FechaCreacion = DateTime.Now,
                    Marca = boleta.Marca,
                    Fecha = DateTime.Now,
                    GuiaRemisionLines = new List<GuiaRemisionLine>(),
                    Vigilante = boleta.Vigilante,
                    //Observaciones = boleta.=
                    

                };

                foreach (var item in boleta.BoletaDeSalidaLines)
                {
                    guiaRemision.GuiaRemisionLines.Add(new GuiaRemisionLine {
                        SubProductId = item.SubProductId,
                        SubProductName = item.SubProductName,
                        Quantity = item.Quantity,
                        UnitOfMeasureId = item.UnitOfMeasureId,
                        UnitOfMeasureName = item.UnitOfMeasureName
                    });
                }
                _context.Add(guiaRemision);

                await _context.SaveChangesAsync();

                boleta.GuiaRemisionId = guiaRemision.Id;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(guiaRemision);
        }

        /// <summary>
        /// Inserta una nueva BoletaDeSalida
        /// </summary>
        /// <param name="_BoletaDeSalida"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<BoletaDeSalida>> Insert([FromBody]BoletaDeSalida _BoletaDeSalida)
        {
            BoletaDeSalida _BoletaDeSalidaq = new BoletaDeSalida();
            try
            {
                _BoletaDeSalidaq = _BoletaDeSalida;
                _context.BoletaDeSalida.Add(_BoletaDeSalidaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_BoletaDeSalidaq);
        }

        /// <summary>
        /// Actualiza la BoletaDeSalida
        /// </summary>
        /// <param name="_BoletaDeSalida"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<BoletaDeSalida>> Update([FromBody]BoletaDeSalida _BoletaDeSalida)
        {
            BoletaDeSalida _BoletaDeSalidaq = _BoletaDeSalida;
            try
            {
                _BoletaDeSalidaq = await (from c in _context.BoletaDeSalida
                                 .Where(q => q.BoletaDeSalidaId == _BoletaDeSalida.BoletaDeSalidaId)
                                          select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_BoletaDeSalidaq).CurrentValues.SetValues((_BoletaDeSalida));

                //_context.BoletaDeSalida.Update(_BoletaDeSalidaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_BoletaDeSalidaq);
        }

        /// <summary>
        /// Elimina una BoletaDeSalida       
        /// </summary>
        /// <param name="_BoletaDeSalida"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]BoletaDeSalida _BoletaDeSalida)
        {
            BoletaDeSalida _BoletaDeSalidaq = new BoletaDeSalida();
            try
            {
                _BoletaDeSalidaq = _context.BoletaDeSalida
                .Where(x => x.BoletaDeSalidaId == (Int64)_BoletaDeSalida.BoletaDeSalidaId)
                .FirstOrDefault();

                _context.BoletaDeSalida.Remove(_BoletaDeSalidaq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_BoletaDeSalidaq);

        }







    }
}