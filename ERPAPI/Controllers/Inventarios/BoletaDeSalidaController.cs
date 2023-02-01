using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
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
                    .Include(i => i.Customer)
                    .Where(q => q.BoletaDeSalidaId == BoletaDeSalidaId).FirstOrDefaultAsync();
                
                  
                NumeracionSAR numeracionSAR = new NumeracionSAR();
                numeracionSAR = numeracionSAR.ObtenerNumeracionSarValida(13, _context);

                
               guiaRemision = new GuiaRemision {
                    NumeroDocumento =  numeracionSAR.GetNumeroSiguiente(),
                    CAI = numeracionSAR._cai,
                    FechaLimiteEmision = numeracionSAR.FechaLimite,
                    Rango = numeracionSAR.getRango(),
                    CustomerName = boleta.CustomerName,
                    CustomerId = (int)boleta.CustomerId,
                    Transportista = boleta.Transportista,
                    OrdenNo = boleta.OrdenNo,
                    Origen = "ALMACAFE",
                    Destino = $"{boleta.CustomerName} - {boleta.Customer.Address}",
                    DNIMotorista = boleta.DNIMotorista,
                    FechaDocuemto= boleta.FechaIngreso,
                    FechaSalida= boleta.FechaSalida,
                    FechaEntrada= boleta.FechaIngreso,
                    PlacaContenedor = boleta.PlacaContenedor,
                    Placa = boleta.Placa,
                    UsuarioCreacion = User.Identity.Name,
                    FechaCreacion = DateTime.Now,
                    Marca = boleta.Marca,
                    Fecha = DateTime.Now,
                    GuiaRemisionLines = new List<GuiaRemisionLine>(),
                    Vigilante = boleta.Vigilante,
                    RTNTransportista = boleta.RTNTransportista,
                    Motorista = boleta.Motorista,
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

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();

                boleta.GuiaRemisionId = guiaRemision.Id;
                boleta.GuiRemisionNo = guiaRemision.NumeroDocumento;
                numeracionSAR.Correlativo++;

                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex }");
                return BadRequest($"{ex}");
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
            try
            {

                foreach (var item in _BoletaDeSalida.BoletaDeSalidaLines) {
                    item.WarehouseName = item.Warehouse.WarehouseName;
                    item.Warehouseid = item.Warehouse.WarehouseId;
                    item.Warehouse = null;

                    if (item.UnitOfMeasure != null)
                    {
                        item.UnitOfMeasureId = item.UnitOfMeasure.UnitOfMeasureId;
                        item.UnitOfMeasureName = item.UnitOfMeasure.UnitOfMeasureName;
                        item.UnitOfMeasure = null;
                    }
                    if (item.SubProduct != null)
                    {
                        item.SubProductId = item.SubProduct.SubproductId;
                        item.SubProductName = item.SubProduct.ProductName;
                        item.SubProduct = null;
                    }
                    
                }
                _BoletaDeSalida.UnitOfMeasureName = _BoletaDeSalida.BoletaDeSalidaLines.FirstOrDefault().UnitOfMeasureName;
                _BoletaDeSalida.UnitOfMeasureId = _BoletaDeSalida.BoletaDeSalidaLines.FirstOrDefault().UnitOfMeasureId;
                _BoletaDeSalida.Quantity = _BoletaDeSalida.BoletaDeSalidaLines.Sum(s => s.Quantity);

                if (_BoletaDeSalida.BoletaDeSalidaLines.Count > 1)
                {
                    _BoletaDeSalida.SubProductName = "Productos Varios";
                }
                else
                {
                    _BoletaDeSalida.SubProductName = _BoletaDeSalida.BoletaDeSalidaLines.FirstOrDefault().SubProductName;
                }
                _context.BoletaDeSalida.Add(_BoletaDeSalida);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_BoletaDeSalida);
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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

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