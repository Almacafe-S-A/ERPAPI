﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/NumeracionSAR")]
    [ApiController]
    public class NumeracionSARController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public NumeracionSARController(ILogger<NumeracionSARController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de NumeracionSAR paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNumeracionSARPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<NumeracionSAR> Items = new List<NumeracionSAR>();
            try
            {
                var query = _context.NumeracionSAR.AsQueryable();
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
        /// Obtiene las numeraciones de SAR       
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNumeracion()
        {
            List<NumeracionSAR> Items = new List<NumeracionSAR>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.NumeracionSAR.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.IdNumeracion).ToListAsync();
                }
                else
                {
                    Items = await _context.NumeracionSAR.OrderByDescending(b => b.IdNumeracion).ToListAsync();
                }
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
        /// Obtiene la Numeracion por medio del Id enviado
        /// </summary>
        /// <param name="IdNumeracion"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdNumeracion}")]
        public async Task<ActionResult<NumeracionSAR>> GetNumeracionById(Int64 IdNumeracion )
        {
            NumeracionSAR Items = new NumeracionSAR();
            try
            {
                Items = await _context.NumeracionSAR.Where(q=>q.IdNumeracion== IdNumeracion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta numeracion SAR
        /// </summary>
        /// <param name="_NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> GetNumeracionByNumeracionSAR([FromBody]NumeracionSAR _NumeracionSAR)
        {
            NumeracionSAR _NumeracionSARq = new NumeracionSAR();
            try
            {
                _NumeracionSARq = await _context.NumeracionSAR.Where(a => a.BranchId== _NumeracionSAR.BranchId
                                        && a.IdCAI ==_NumeracionSAR.IdCAI
                                        && a.IdPuntoEmision== _NumeracionSAR.IdPuntoEmision
                ).FirstOrDefaultAsync();
              }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_NumeracionSARq));
        }


        /// <summary>
        /// Inserta numeracion SAR
        /// </summary>
        /// <param name="_NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Insert([FromBody]NumeracionSAR _NumeracionSAR)
        {
            NumeracionSAR _NumeracionSARq = new NumeracionSAR();
            try
            {
                _NumeracionSARq = _NumeracionSAR;
                _context.NumeracionSAR.Add(_NumeracionSARq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_NumeracionSAR));
        }

        /// <summary>
        /// Actualiza la numeracion SAR
        /// </summary>
        /// <param name="_NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Update([FromBody]NumeracionSAR _NumeracionSAR)
        {
          
            try
            {
                NumeracionSAR _NumeracionSARq = (from c in _context.NumeracionSAR
                                    .Where(q => q.IdNumeracion == _NumeracionSAR.IdNumeracion)
                                              select c
                                   ).FirstOrDefault();

                _NumeracionSAR.FechaCreacion = _NumeracionSARq.FechaCreacion;
                _NumeracionSAR.UsuarioCreacion = _NumeracionSARq.UsuarioCreacion;

                _context.Entry(_NumeracionSARq).CurrentValues.SetValues(_NumeracionSAR);
                // _context.NumeracionSAR.Update(_NumeracionSARq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_NumeracionSAR));
        }

        /// <summary>
        /// Elimina la Numeración  
        /// </summary>
        /// <param name="__NumeracionSAR"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<NumeracionSAR>> Delete([FromBody]NumeracionSAR __NumeracionSAR)
        {
            NumeracionSAR __NumeracionSARq = new NumeracionSAR();
            try
            {
                __NumeracionSARq = _context.NumeracionSAR
                .Where(x => x.IdNumeracion== (Int64)__NumeracionSAR.IdNumeracion)
                .FirstOrDefault();
                _context.NumeracionSAR.Remove(__NumeracionSARq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(__NumeracionSARq));

        }

        /// <summary>
        /// Obtiene las numeraciones por Vencer del SAR       
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNumeracionVencida()
        {
            List<NumeracionSAR> Items = new List<NumeracionSAR>();
            try
            {
                Items = await _context.NumeracionSAR.Where(c => c.FechaLimite <= DateTime.Now).ToListAsync();
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
        /// Obtiene la nuemracion SAR valida para el tipo de documento 
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <returns></returns>
        public NumeracionSAR ObtenerNumeracionSarValida( int tipoDocumento)
        {

            NumeracionSAR numeracionSAR = new NumeracionSAR();
            List<NumeracionSAR> numeracionSARs = new List<NumeracionSAR>();
            numeracionSARs = _context.NumeracionSAR
                    .Where(q => q.IdEstado == 1
                    && q.DocTypeId == tipoDocumento
                    && q.FechaLimite <= DateTime.Now
                    && (q.Correlativo <= q.NoFin || q.SiguienteNumero == null || q.Correlativo == null)
                    && q.IdEstado == 1
                    ).ToList();

            if (numeracionSARs.Count == 0)
            {
                Exception exception = new Exception("No existe numeracion valida");
                throw exception;
            }
            if (numeracionSARs.Count > 1)
            {
                Exception exception = new Exception("Se encontro mas de una numeracion valida");
                throw exception;
            }


            numeracionSAR = numeracionSARs.FirstOrDefault();

            return numeracionSAR;


        }



    }
}