using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/CertificadoDeposito")]
    [ApiController]
    public class CertificadoDepositoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper mapper;

        public CertificadoDepositoController(ILogger<CertificadoDepositoController> logger, ApplicationDbContext context
            , IMapper mapper
            )
        {
            this.mapper = mapper;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CertificadoDepositoes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCertificadoDeposito()
        {
            List<CertificadoDeposito> Items = new List<CertificadoDeposito>();
            try
            {
                Items = await _context.CertificadoDeposito.ToListAsync();
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
        /// Obtiene los Datos de la CertificadoDeposito por medio del Id enviado.
        /// </summary>
        /// <param name="IdCD"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdCD}")]
        public async Task<IActionResult> GetCertificadoDepositoById(Int64 IdCD)
        {
            CertificadoDeposito Items = new CertificadoDeposito();
            try
            {
                Items = await _context.CertificadoDeposito.Include(q=>q._CertificadoLine).Where(q => q.IdCD == IdCD).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Inserta una nueva CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Insert([FromBody]CertificadoDepositoDTO _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            SolicitudCertificadoDeposito _SolicitudCertificado = new SolicitudCertificadoDeposito();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //Solicitud de certificado
                        _SolicitudCertificado = mapper.Map<SolicitudCertificadoDeposito>(_CertificadoDeposito);
                        _context.SolicitudCertificadoDeposito.Add(_SolicitudCertificado);
                        foreach (var item in _CertificadoDeposito._CertificadoLine)
                        {
                            SolicitudCertificadoLine _SolicitudCertificadoLine = new SolicitudCertificadoLine();
                            _SolicitudCertificadoLine = mapper.Map<SolicitudCertificadoLine>(item);
                            _context.SolicitudCertificadoLine.Add(_SolicitudCertificadoLine);

                        }

                        //Certificado

                        _CertificadoDepositoq = _CertificadoDeposito;
                        _context.CertificadoDeposito.Add(_CertificadoDepositoq);
                        // await _context.SaveChangesAsync();

                        foreach (var item in _CertificadoDeposito._CertificadoLine)
                        {
                            item.IdCD = _CertificadoDepositoq.IdCD;
                            _context.CertificadoLine.Add(item);


                        }

                        await _context.SaveChangesAsync();
                        foreach (var item in _CertificadoDeposito.RecibosAsociados)
                        {
                            RecibosCertificado _recibocertificado =
                                new RecibosCertificado
                                {
                                    IdCD = _CertificadoDepositoq.IdCD,
                                    IdRecibo = item,
                                    productocantidadbultos = _CertificadoDeposito.Quantitysum,
                                    productorecibolempiras = _CertificadoDeposito.Total,
                                    // UnitMeasureId =_CertificadoDeposito.
                                };

                            _context.RecibosCertificado.Add(_recibocertificado);
                        }

                       


                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Commit();
                        throw ex;
                    }
                }
                //_CertificadoDepositoq = _CertificadoDeposito;
                //_context.CertificadoDeposito.Add(_CertificadoDepositoq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CertificadoDepositoq);
        }

        /// <summary>
        /// Actualiza la CertificadoDeposito
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<CertificadoDeposito>> Update([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = _CertificadoDeposito;
            try
            {
                _CertificadoDepositoq = await (from c in _context.CertificadoDeposito
                                 .Where(q => q.IdCD == _CertificadoDeposito.IdCD)
                                               select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_CertificadoDepositoq).CurrentValues.SetValues((_CertificadoDeposito));

                //_context.CertificadoDeposito.Update(_CertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CertificadoDepositoq);
        }

        /// <summary>
        /// Elimina una CertificadoDeposito       
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]CertificadoDeposito _CertificadoDeposito)
        {
            CertificadoDeposito _CertificadoDepositoq = new CertificadoDeposito();
            try
            {
                _CertificadoDepositoq = _context.CertificadoDeposito
                .Where(x => x.IdCD == (Int64)_CertificadoDeposito.IdCD)
                .FirstOrDefault();

                _context.CertificadoDeposito.Remove(_CertificadoDepositoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_CertificadoDepositoq);

        }







    }
}