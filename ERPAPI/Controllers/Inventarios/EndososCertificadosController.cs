﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/EndososCertificados")]
    [ApiController]
    public class EndososCertificadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public EndososCertificadosController(ILogger<EndososCertificadosController> logger, IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificados paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                var query = _context.EndososCertificados.AsQueryable();
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
        /// Obtienne los de los controles de salidas pendientes y le resta el saldo autorizado 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndosos([FromQuery(Name = "Recibos")] int[] ARs)
        {
            List<EndososCertificados> endososCertificados = new List<EndososCertificados>();
            

            try
            {
                endososCertificados = _context.EndososCertificados.Where(q => ARs.Any(a => a == (int)q.NoCD)).ToList();

               



                return Ok(endososCertificados.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Ocurrio un error:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el Listado de EndososCertificadoses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificados()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }


        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetEndososCertificadosByIdCD(Int64 IdCD)
        {
            EndososCertificados Items = new EndososCertificados();
            try
            {
                Items = await _context.EndososCertificados
                    .Where(q=>q.IdCD == IdCD).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }


        

        [HttpPost("[action]")]
        public async Task<IActionResult> GetEndososSaldoByLineByIdCD(EndososCertificadosLine _EndososCertificadosLine)
        {
            EndososLiberacion Items = new EndososLiberacion();
            try
            {
                Items = await _context.EndososLiberacion
                         .OrderByDescending(q=>q.EndososLiberacionId)
                         .Where(q => q.EndososLineId == _EndososCertificadosLine.EndososCertificadosLineId)
                         .FirstOrDefaultAsync();

                EndososCertificadosLine _endosoline = await
                           _context.EndososCertificadosLine
                           .Where(q => q.EndososCertificadosLineId == _EndososCertificadosLine.EndososCertificadosLineId).FirstOrDefaultAsync();

                //if (Items == null)
                //{
                //    Items = new EndososLiberacion { EndososId=0, EndososLineId=0, Saldo = _endosoline.Quantity };
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososCertificadosByCustomer()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.ToListAsync();
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
        /// Obtiene los Datos de la EndososCertificados por medio del Id enviado.
        /// </summary>
        /// <param name="EndososCertificadosId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{EndososCertificadosId}")]
        public async Task<IActionResult> GetEndososCertificadosById(Int64 EndososCertificadosId)
        {
            EndososCertificados Items = new EndososCertificados();
            try
            {
                Items = await _context.EndososCertificados.Where(q => q.EndososCertificadosId == EndososCertificadosId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Obtiene los Datos de la EndososCertificados por medio del Id enviado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEndososporVencer()
        {
            List<EndososCertificados> Items = new List<EndososCertificados>();
            try
            {
                Items = await _context.EndososCertificados.Include(i => i.CertificadoDeposito)
                    .Where(q => DateTime.Now -  q.CertificadoDeposito.FechaVencimientoCertificado <TimeSpan.FromDays(30)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: {ex.ToString()}");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }




        /// <summary>
        /// Inserta una nueva EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<EndososCertificados>> Insert([FromBody]EndososDTO _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _EndososCertificadosq = _EndososCertificados;
                        _EndososCertificadosq.FechaCancelacion = null;
                        _EndososCertificadosq.FechaLiberacion = null;
                        _EndososCertificadosq.Saldo = _EndososCertificados.EndososCertificadosLine.Sum(s => s.Saldo);
                        _EndososCertificadosq.CantidadEndosar = _EndososCertificados.EndososCertificadosLine.Sum(p => p.Quantity);
                        _EndososCertificadosq.TotalEndoso = _EndososCertificados.EndososCertificadosLine.Sum(p => p.Price )* _EndososCertificadosq.CantidadEndosar;

                        _EndososCertificadosq.ProductoEndosado = _EndososCertificados.EndososCertificadosLine.Count > 1 ? "Productos Varios" : _EndososCertificados.EndososCertificadosLine.FirstOrDefault().SubProductName;
                        _EndososCertificadosq.Estado = "Vigente";
                        _EndososCertificadosq.EstadoId = 1;
                        

                        _context.EndososCertificados.Add(_EndososCertificadosq);

                        foreach (var item in _EndososCertificadosq.EndososCertificadosLine)
                        {
                            item.EndososCertificadosId = _EndososCertificados.EndososCertificadosId;
                            //item.Saldo = item.Quantity;
                            _context.EndososCertificadosLine.Add(item);
                        }

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _EndososCertificadosq.EndososCertificadosId,
                            DocType = "EndososCertificados",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_EndososCertificados, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_EndososCertificadosq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _EndososCertificadosq.UsuarioCreacion,
                            UsuarioModificacion = _EndososCertificadosq.UsuarioModificacion,
                            UsuarioEjecucion = _EndososCertificadosq.UsuarioModificacion,
                            


                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                  
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Actualiza la EndososCertificados
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<EndososCertificados>> Update([FromBody]EndososCertificados _EndososCertificados)
        {
            if (_EndososCertificados.FechaCancelacion == null)
            {
                return BadRequest("Fecha de cancelacion no puede ser Nula");
            }
            EndososCertificados _EndososCertificadosq = _EndososCertificados;
            try
            {
                _EndososCertificadosq = await _context.EndososCertificados
                    .Where(q => q.EndososCertificadosId == _EndososCertificados.EndososCertificadosId)
                    .Include(i => i.EndososCertificadosLine)
                    .FirstOrDefaultAsync();

               // _context.Entry(_EndososCertificadosq).CurrentValues.SetValues((_EndososCertificados));
               _EndososCertificadosq.FechaCancelacion = _EndososCertificados.FechaCancelacion;


                foreach (var item in _EndososCertificados.EndososCertificadosLine)
                {
                    _context.EndososLiberacion.Add(new EndososLiberacion {
                        Pda = item.Pda,
                        EndososId = item.EndososCertificadosId,
                        EndososLineId = item.EndososCertificadosLineId,
                        Quantity = item.CantidadLiberacion,
                        UnitOfMeasureId = item.UnitOfMeasureId,
                        UnitOfMeasureName = item.UnitOfMeasureName, 
                        FechaCreacion =DateTime.Now,
                        FechaLiberacion = (DateTime) _EndososCertificados.FechaCancelacion,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name,
                        Saldo = item.Quantity - item.CantidadLiberacion,
                        FechaModificacion= DateTime.Now,
                        SubProductName = item.SubProductName,
                        SubProductId = item.SubProductId,   
                        TipoEndoso = _EndososCertificados.NombreEndoso,
                        
                        
                    
                    
                    });

                    EndososCertificadosLine linea = _EndososCertificadosq.EndososCertificadosLine
                        .Where(q => q.EndososCertificadosLineId == item.EndososCertificadosLineId).FirstOrDefault();

                    linea.Saldo = item.Saldo - item.CantidadLiberacion;
                }

                _EndososCertificadosq.Saldo = _EndososCertificadosq.EndososCertificadosLine.Sum(s=>s.Saldo);

                if (_EndososCertificadosq.Saldo == 0)
                {
                    _EndososCertificadosq.Estado = "Cancelado";
                    _EndososCertificadosq.EstadoId = 2;
                }


                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.EndososCertificados.Update(_EndososCertificadosq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);
        }

        /// <summary>
        /// Elimina una EndososCertificados       
        /// </summary>
        /// <param name="_EndososCertificados"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]EndososCertificados _EndososCertificados)
        {
            EndososCertificados _EndososCertificadosq = new EndososCertificados();
            try
            {
                _EndososCertificadosq = _context.EndososCertificados
                .Where(x => x.EndososCertificadosId == (Int64)_EndososCertificados.EndososCertificadosId)
                .FirstOrDefault();

                _context.EndososCertificados.Remove(_EndososCertificadosq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_EndososCertificadosq);

        }







    }
}