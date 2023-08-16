using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using ERPAPI.Helpers;
using ERPMVC.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using ERPMVC.Helpers;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/GoodsReceived")]
    [ApiController]
    public class GoodsReceivedController : Controller
    {
        private readonly IOptions<MyConfig> config;
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GoodsReceivedController(ILogger<GoodsReceivedController> logger, ApplicationDbContext context, IOptions<MyConfig> config)
        {
            this.config = config;
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de GoodsReceived paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                var query = _context.GoodsReceived.AsQueryable();
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
        /// Obtiene el Listado de GoodsReceivedes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceived()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    Items = await _context.GoodsReceived.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                }
                else
                {
                    Items = await _context.GoodsReceived.OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                }
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
        /// Obtiene el Listado de recibnos de mercaderias segun el cliente y el servicio
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{clienteid}/{servicioid}/{escafe}/{sucursal}")]
        public async Task<IActionResult> RecibosPendientesCertificar(int clienteid, int servicioid, int escafe, int sucursal)
        {            
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();

                UserBranch branch = _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id && w.BranchId == sucursal).FirstOrDefault();
                if (branch != null)
                {
                    Items = await _context.GoodsReceived
                      .Where(p =>
                           p.CustomerId == clienteid
                          && p.ProductId == servicioid
                          && (p.Porcertificar == null || (bool)p.Porcertificar)
                          )
                      .OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                    if (escafe == 0)
                    {
                        
                        Items = Items.Where(q => _context.LiquidacionLine
                                            .Include(i => i.GoodsReceivedLine)
                                            .Include(i => i.Liqudacion )
                                            .Where(s => s.Liqudacion.EstadoId==6)
                                            .Any(a => a.GoodsReceivedLine.GoodsReceivedId == q.GoodsReceivedId)).ToList();
                        
                        
                        return await Task.Run(() => Ok(Items));
                    }
                    else
                    {
                        return await Task.Run(() => Ok(Items));
                    }
                }
                else
                {
                    return await Task.Run(() => Ok(Items));
                    // Items = await _context.GoodsReceived.OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }
               

        /// <summary>
        /// Obtiene el Listado de recibnos de mercaderias segun el cliente y el servicio
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{clienteid}/{servicioid}")]
        public async Task<IActionResult> RecibosPendientesLiquidar(int clienteid, int servicioid)
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if (branchlist.Count > 0)
                {
                    List<GoodsReceivedLine> GoodsRecievedList = _context.GoodsReceivedLine
                        .Where(p => !_context.LiquidacionLine.Any(a => a.GoodsReceivedLineId == p.GoodsReceiveLinedId)).ToList();
                    Items = await (from gr in _context.GoodsReceived
                                   join grl in _context.GoodsReceivedLine on gr.GoodsReceivedId equals grl.GoodsReceivedId
                                   where branchlist.Any(b => gr.BranchId == b.BranchId)
                                       && gr.CustomerId == clienteid
                                       && gr.ProductId == servicioid
                                       && !_context.LiquidacionLine.Any(a => a.GoodsReceivedLineId == grl.GoodsReceiveLinedId)
                                   orderby gr.GoodsReceivedId descending
                                   select gr).ToListAsync();
                }
                else
                {
                    Items = await _context.GoodsReceived.OrderByDescending(b => b.GoodsReceivedId).ToListAsync();
                }
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
        /// Obtiene los Datos de la GoodsReceived por medio del Id enviado.
        /// </summary>
        /// <param name="GoodsReceivedId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{GoodsReceivedId}")]
        public async Task<IActionResult> GetGoodsReceivedById(Int64 GoodsReceivedId)
        {
            GoodsReceived Items = new GoodsReceived();
            try
            {
                Items = await _context.GoodsReceived.Where(q => q.GoodsReceivedId == GoodsReceivedId).Include(q => q._GoodsReceivedLine).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGoodsReceivedNoSelected()
        {
            List<GoodsReceived> Items = new List<GoodsReceived>();
            try
            {
                List<Int64> listayaprocesada = _context.RecibosCertificado
                                              .Where(q => q.IdRecibo > 0)
                                              .Select(q => q.IdRecibo).ToList();
                Items = await _context.GoodsReceived.Where(q => !listayaprocesada.Contains(q.GoodsReceivedId)).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{SubProductId}")]
        public async Task<ActionResult<SubProduct>> GetSubProductById(Int64 SubProductId)
        {
            SubProduct Items = new SubProduct();
            try
            {
                Items = await _context.SubProduct.Where(q => q.SubproductId == SubProductId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Inserta una nueva Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Alert>> InsertAlert([FromBody] Alert _Alert)
        {
            Alert _Alertq = new Alert();
            try
            {
                _Alertq = _Alert;
                _context.Alert.Add(_Alertq);
                await _context.SaveChangesAsync();

                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = _Alert.AlertId,
                    DocType = "Alert",
                    ClaseInicial =
                    Newtonsoft.Json.JsonConvert.SerializeObject(_Alert, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _Alert.UsuarioCreacion,
                    UsuarioModificacion = _Alert.UsuarioModificacion,
                    UsuarioEjecucion = _Alert.UsuarioModificacion,

                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Alertq));
        }




        GoodsReceived ToGoodsReceived(dynamic dto)
        {
            try
            {
                GoodsReceived goodsReceived = new GoodsReceived
                {
                    GoodsReceivedId = dto.GoodsReceivedId,
                    CustomerId = dto.CustomerId,
                    CustomerName = dto.CustomerName,
                    ControlId = dto.ControlId,
                    CountryId = dto.CountryId,
                    VigilanteId = dto.VigilanteId,
                    VigilanteName = dto.VigilanteName,
                    CountryName = dto.CountryName,
                    BranchId = dto.BranchId,
                    BranchName = dto.BranchName,
                    WarehouseId = 0,
                    WarehouseName = dto.WarehouseName,
                    ProductId = dto.ProductId,
                    ProductName = dto.ProductName,
                    SubProductId = 0,
                    SubProductName = dto.SubProductName,
                    OrderDate = dto.OrderDate,
                    DocumentDate = dto.DocumentDate,
                    Motorista = dto.Motorista,
                    BoletaSalidaId = dto.BoletaSalidaId,
                    Placa = dto.Placa,
                    Marca = dto.Marca,
                    WeightBallot = dto.WeightBallot,
                    PesoBruto = dto.PesoBruto,
                    TaraTransporte = dto.TaraTransporte,
                    TaraCamion = dto.TaraCamion,
                    PesoNeto = dto.PesoNeto,
                    TaraUnidadMedida = dto.TaraUnidadMedida,
                    PesoNeto2 = dto.PesoNeto2,
                    Comments = dto.Comments,
                    FechaCreacion = dto.FechaCreacion,
                    FechaModificacion = dto.FechaModificacion,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioModificacion,
                    //_GoodsReceivedLine = dto._GoodsReceivedLine,




                };

                return goodsReceived;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async Task<ActionResult<BoletaDeSalida>> InsertBoletaSalida(GoodsReceived _GoodsReceived) {
            try
            {
                ControlPallets controlPallets = _context.ControlPallets
                    .Where(q => q.ControlPalletsId == _GoodsReceived.ControlId)
                    .Include(i => i._ControlPalletsLine).FirstOrDefault();


                if (controlPallets == null) return BadRequest(); 
                Boleto_Ent boletapeso = await  _context.Boleto_Ent
                    .Where(q => q.clave_e == controlPallets.WeightBallot)
                    .FirstOrDefaultAsync();

                

                decimal cantidadSacos = (decimal)_GoodsReceived._GoodsReceivedLine.Sum(s => s.QuantitySacos);



                BoletaDeSalida _boletadesalida = new BoletaDeSalida
                {
                    BranchId = _GoodsReceived.BranchId,
                    BranchName = _GoodsReceived.BranchName,
                    CustomerId = _GoodsReceived.CustomerId,
                    CustomerName = _GoodsReceived.CustomerName,
                    DocumentDate = _GoodsReceived.DocumentDate,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Marca = _GoodsReceived.Marca,
                    Placa = _GoodsReceived.Placa,
                    Motorista = _GoodsReceived.Motorista,
                    Quantity = cantidadSacos == 0 ? (decimal) _GoodsReceived.PesoNeto2 :cantidadSacos,
                    SubProductId = (long)_GoodsReceived._GoodsReceivedLine[0].SubProductId,
                    SubProductName = _GoodsReceived._GoodsReceivedLine.Count()>1?"Productos Varios":
                                _GoodsReceived._GoodsReceivedLine[0].SubProductName,
                    ProductName = _GoodsReceived.ProductName,     
                    Producto = _GoodsReceived.ProductId,
                    CargadoId = 14,
                    Cargadoname = "Vacío",
                    DocumentoTipo = "Recibo de Mercaderías",
                    UsuarioCreacion = _GoodsReceived.UsuarioCreacion,
                    UsuarioModificacion = _GoodsReceived.UsuarioModificacion,
                    UnitOfMeasureId = _GoodsReceived._GoodsReceivedLine[0].UnitOfMeasureId,
                    UnitOfMeasureName = _GoodsReceived._GoodsReceivedLine.Count() > 1 ? "Productos Varios":
                            _GoodsReceived._GoodsReceivedLine[0].UnitOfMeasureName,
                    WeightBallot = _GoodsReceived.WeightBallot,
                    VigilanteId = _GoodsReceived.VigilanteId,
                    Vigilante = _GoodsReceived.VigilanteName,
                    BoletaDeSalidaLines =  new List<BoletaDeSalidaLine>(),
                    

                };

                if (boletapeso!=null)
                {
                    boletapeso.Boleto_Sal = _context.Boleto_Sal
                    .Where(q => q.clave_e == boletapeso.clave_e)
                    .FirstOrDefault();
                    boletapeso.Boleto_Sal = await _context.Boleto_Sal.Where(q => q.clave_e == boletapeso.clave_e).FirstOrDefaultAsync();
                    _boletadesalida.OrdenNo = boletapeso.Orden;
                    _boletadesalida.Transportista = boletapeso.Tranportista;
                    _boletadesalida.Motorista = boletapeso.conductor;
                    _boletadesalida.RTNTransportista = boletapeso.RTNTransportista;
                    _boletadesalida.DNIMotorista = boletapeso.DNIConductor;
                    _boletadesalida.FechaIngreso = boletapeso.fecha_e;
                    _boletadesalida.FechaSalida = boletapeso.Boleto_Sal.fecha_s;

                }

                


                foreach (var item in controlPallets._ControlPalletsLine)
                {
                    _boletadesalida.BoletaDeSalidaLines.Add(new BoletaDeSalidaLine()
                    {
                        SubProductId =(long) item.SubProductId,
                        SubProductName = item.SubProductName,
                        Quantity = (decimal) item.Totallinea,
                        UnitOfMeasureId = (int)item.UnitofMeasureId,
                        UnitOfMeasureName = item.cantidadPoliEtileno>0|| item.cantidadYute>0?"Sacos": item.UnitofMeasureName,
                        WarehouseName = item.WarehouseName,
                        Warehouseid = (int)item.WarehouseId,
                        
                    });
                }

                 _context.BoletaDeSalida.Add(_boletadesalida);
                await _context.SaveChangesAsync();

                _boletadesalida.DocumentoId = (int)_GoodsReceived.GoodsReceivedId;
                await _context.SaveChangesAsync();
                return _boletadesalida;
            }
            catch (Exception ex)
            {

                return BadRequest("Error al registrar la boleta de salida: " + ex);
            }


        }

        private async Task<ActionResult<Alert>> ValidarPaisesGAFI(GoodsReceived goodsReceived)
        {

            Alert Alerta = null;
            Country country = await _context.Country.Where(q => q.Id == goodsReceived.CountryId).FirstOrDefaultAsync();
            Country countryGAFI = await _context.Country.Where(q => q.Name.ToLower() == country.Name.ToLower() && country.GAFI == true).FirstOrDefaultAsync();

            if (countryGAFI != null )
            {

                Alerta = new Alert();
                Alerta.DocumentId = (long)goodsReceived.CountryId;
                Alerta.DocumentName = "LISTA PROHIBIDA";
                Alerta.AlertName = "Países GAFI";
                Alerta.Code = "COUNTRY01";
                Alerta.DescriptionAlert = "Lista de Países GAFI";
                Alerta.FechaCreacion = DateTime.Now;
                Alerta.FechaModificacion = DateTime.Now;
                Alerta.UsuarioCreacion = User.Identity.Name;
                Alerta.UsuarioModificacion = User.Identity.Name;
                Alerta.PersonName = goodsReceived.CustomerName;
                Alerta.Description = $"País GAFI {goodsReceived.CountryName} en recibo de mercaderia";
                Alerta.DescriptionAlert = $"País GAFI {goodsReceived.CountryName} en recibo de mercaderia";
                Alerta.Type = "168";
                Alerta.DescriptionAlert = _context.ElementoConfiguracion.Where(p => p.Id == 168).FirstOrDefault().Nombre;
                _context.Alert.Add(Alerta);

                BitacoraWrite _writealert = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = Alerta.AlertId,
                    DocType = "Alert",
                    ClaseInicial =
                    Newtonsoft.Json.JsonConvert.SerializeObject(Alerta, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = Alerta.UsuarioCreacion,
                    UsuarioModificacion = Alerta.UsuarioModificacion,
                    UsuarioEjecucion = Alerta.UsuarioModificacion,

                });

                await _context.SaveChangesAsync();
                return Alerta;
            }
            else
            {

                return Alerta;
            }
        }
        
        private async Task<ActionResult<Alert>> ValidarProductosProhibidos(GoodsReceivedLine producto,GoodsReceived goodsReceived, SubProduct _subproduct)
        {
           
                Alert Alerta = null;
            List<SubProduct> productoprohibidos = new List<SubProduct> ();

            productoprohibidos = _context.SubProduct.Where(q => _subproduct.ProductName.ToLower().Contains(q.ProductName.ToLower()) && q.ProductTypeId == 3).ToList();
           
            if ( productoprohibidos.Count > 0)
            {
                Alerta = new Alert();
                Alerta.DocumentId = (long)producto.SubProductId;
                Alerta.DocumentName = "LISTA PROHIBIDA";
                Alerta.AlertName = "Productos";
                Alerta.Code = "PRODUCT01";
                Alerta.DescriptionAlert = "Lista de producto Prohibida";
                Alerta.FechaCreacion = DateTime.Now;
                Alerta.FechaModificacion = DateTime.Now;
                Alerta.UsuarioCreacion = User.Identity.Name;
                Alerta.UsuarioModificacion = User.Identity.Name;
                Alerta.PersonName = goodsReceived.CustomerName;
                Alerta.Description = $"Producto Prohibido {producto.SubProductName} en recibo de mercaderia";
                Alerta.DescriptionAlert = $"Producto Prohibido {producto.SubProductName} en recibo de mercaderia";
                Alerta.Type = "168";
                Alerta.DescriptionAlert = _context.ElementoConfiguracion.Where(p => p.Id == 168).FirstOrDefault().Nombre;
                _context.Alert.Add(Alerta);

                BitacoraWrite _writealert = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = Alerta.AlertId,
                    DocType = "Alert",
                    ClaseInicial =
                    Newtonsoft.Json.JsonConvert.SerializeObject(Alerta, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = Alerta.UsuarioCreacion,
                    UsuarioModificacion = Alerta.UsuarioModificacion,
                    UsuarioEjecucion = Alerta.UsuarioModificacion,

                });

                 await _context.SaveChangesAsync();
                return Alerta;
            }
            else
            {
                
                return Alerta;
            }




        }


        private async Task<ActionResult<bool>> ValidarProductoTipoCafe(GoodsReceived goodsReceived) {

            foreach (var item in goodsReceived._GoodsReceivedLine)
            {
                SubProduct subProduct =await _context.SubProduct.Where(q => q.SubproductId == item.SubProductId).FirstOrDefaultAsync();
                if (goodsReceived.esCafe!= null&&(bool)goodsReceived.esCafe&& subProduct.TipoCafe == 0)
                {
                    return BadRequest("Error en el ");


                }
                goodsReceived.esCafe = subProduct.TipoCafe != 0;
                
            }



            return (bool)goodsReceived.esCafe;
        
        
        
        
        }



        /// <summary>
        /// Inserta una nueva GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ResponseDTO<GoodsReceived>>> Insert([FromBody] GoodsReceived _GoodsReceived)
        {
            List<Alert> alerts = new List<Alert>();
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _GoodsReceivedq = _GoodsReceived;
                        var validarProductoTipoCafe = await ValidarProductoTipoCafe(_GoodsReceivedq);
                        if (validarProductoTipoCafe.Result is BadRequestObjectResult)
                        {
                            return BadRequest("Los recibos de mercaderias que contienen cafe solo pueden contener ese tipo de producto"); 
                        }

                        var validarpais = await ValidarPaisesGAFI(_GoodsReceivedq);
                        if (validarpais.Value != null)// Valida si es o no un pais GAFI
                        {
                            alerts.Add(validarpais.Value);
                            try {
                                string correoOrigen = config.Value.emailsender;
                                string contraseña = config.Value.passwordsmtp;
                                string correoDestino = config.Value.EmailAlertaNivelUno;
                                string host = config.Value.smtp;
                                string puerto = config.Value.port;

                            MailMessage correo = new MailMessage();
                            correo.From = new MailAddress(correoOrigen, "Notificación de Alerta", Encoding.UTF8);
                            correo.To.Add(correoDestino);
                            correo.Subject = "ALERTA";

                            StringBuilder bodyBuilder = new StringBuilder();
                            foreach (var alert in alerts)
                            {
                                bodyBuilder.AppendLine("ALERTA: " + alert.Description);
                            }

                            correo.Body = bodyBuilder.ToString();
                            correo.IsBodyHtml = true;
                            correo.Priority = MailPriority.Normal;

                            SmtpClient smtp = new SmtpClient();
                                smtp.Host = host;
                                smtp.Port = Convert.ToInt32(puerto);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = new NetworkCredential(correoOrigen, contraseña);
                            smtp.EnableSsl = true;

                            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, sslPolicyErrors) => true;

                            smtp.Send(correo);

                            Console.WriteLine("Correo enviado correctamente.");
                        }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al enviar el correo: " + ex.Message);
                    }
                        }

                        //_GoodsReceived = (GoodsReceived)valido.Value;
                        _GoodsReceived.esCafe = validarProductoTipoCafe.Value;

                        if (_GoodsReceivedq.ControlId > 0)/// valida si proviene de una boleta de peso y marca Control de Ingresos  como recibida
                        {
                            ControlPallets controlPallets = await _context.ControlPallets.Where(q => q.ControlPalletsId == _GoodsReceived.ControlId)
                           .FirstOrDefaultAsync();
                            controlPallets.Estado = "Recibido";
                            _context.ControlPallets.Update(controlPallets);
                            _GoodsReceivedq.SubProductName = controlPallets.SubProductName;
                            Boleto_Sal boleto = _context.Boleto_Sal.Where(q => q.clave_e > controlPallets.WeightBallot).FirstOrDefault();
                            _GoodsReceivedq.TaraTransporte = boleto != null ? boleto.peso_s : 0;
                        }
                        else //Marca el producto del encabezado como productos Varios
                        {
                            _GoodsReceivedq.SubProductName = "Productos Varios No Pesado";
                        }
                        _context.GoodsReceived.Add(_GoodsReceivedq);
                        await _context.SaveChangesAsync();

                        foreach (var item in _GoodsReceivedq._GoodsReceivedLine)
                        {
                            SubProduct _subproduct = _context.SubProduct
                                .Where(q => q.SubproductId == item.SubProductId).FirstOrDefault();
                            //item.GoodsReceivedId = _GoodsReceivedq.GoodsReceivedId;
                            var validarproductos = await ValidarProductosProhibidos(item,_GoodsReceivedq, _subproduct);
                            if (validarproductos.Value != null)// Valida si es o no producto prohibido
                            {
                                alerts.Add(validarproductos.Value);
                            
                            }
                            //_context.GoodsReceivedLine.Add(item); // Siempre Agrega el prodcuto
                            var generarKardex = await GenerarKardexRecibo(item,_GoodsReceivedq, _subproduct); // Genera el Kardex Fisico
                            item.Kardex =  generarKardex.Value;
                            item.SaldoporCertificar = item.Quantity;////Asigna el valor al saldo por certificar al detalle

                            
                        }//Fin Foreach

                        _GoodsReceivedq.Porcertificar = true;

                        await _context.SaveChangesAsync();
                        


                        
                        foreach (var item in _GoodsReceivedq._GoodsReceivedLine)
                        {
                            
                            ControlPallets _ControlPalletsq = await _context.ControlPallets.Where(q => q.ControlPalletsId == item.ControlPalletsId)
                           .FirstOrDefaultAsync();
                            

                            if (_ControlPalletsq != null)
                            {
                                _ControlPalletsq.QQPesoBruto = _GoodsReceivedq.PesoBruto==null?0 : (double)_GoodsReceivedq.PesoBruto;
                                _ControlPalletsq.QQPesoNeto = _GoodsReceivedq.PesoNeto == null ? 0 : (double)_GoodsReceivedq.PesoNeto;
                                _ControlPalletsq.QQPesoFinal = _GoodsReceivedq.PesoNeto2 == null ? 0 : (double)_GoodsReceivedq.PesoNeto2;

                                _context.Entry(_ControlPalletsq).CurrentValues.SetValues((_ControlPalletsq));
                            }


                          
                        }


                        var boletasalida = await InsertBoletaSalida(_GoodsReceivedq);
                        if (!(boletasalida.Result is BadRequestResult))
                        {
                            _GoodsReceivedq.BoletaSalidaId = boletasalida.Value.BoletaDeSalidaId;

                            await _context.SaveChangesAsync();///Asigna el numero del recibo a la boleta de salida 
                        }

                        var generarasiento = await GeneraAsientoReciboMercaderias(_GoodsReceivedq);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            ResponseDTO<GoodsReceived> response = new ResponseDTO<GoodsReceived> {
                model = _GoodsReceivedq,
                alerts = alerts
            };

            return await Task.Run(() => Ok(response));
        }
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }


        private async Task<ActionResult<Kardex>> GenerarKardexRecibo(GoodsReceivedLine item, GoodsReceived _GoodsReceivedq, SubProduct producto)
        {
            
            try
            {
                Kardex kardex = new Kardex
                {
                    DocumentDate = _GoodsReceivedq.DocumentDate,
                    DocumentName = "ReciboMercaderia/GoodsReceived",
                    DocType = 1,
                    DocumentLine = (int)item.GoodsReceiveLinedId,
                    ProducId = _GoodsReceivedq.ProductId,
                    ProductName = _GoodsReceivedq.ProductName,
                    SubProducId = item.SubProductId,
                    SubProductName = item.SubProductName,
                    QuantityEntry = item.Quantity,
                    QuantityOut = 0,
                    QuantityEntryBags = item.QuantitySacos == null ? 0 : (decimal)item.QuantitySacos,
                    BranchId = _GoodsReceivedq.BranchId,
                    BranchName = _GoodsReceivedq.BranchName,
                    WareHouseId = item.WareHouseId,
                    WareHouseName = item.WareHouseName,
                    UnitOfMeasureId = item.UnitOfMeasureId,
                    UnitOfMeasureName = item.UnitOfMeasureName,
                    TypeOperationId = TipoOperacion.Entrada,
                    TypeOperationName = "Entrada",
                    Total = item.Quantity,
                    TotalBags = item.QuantitySacos == null ? 0 : (decimal)item.QuantitySacos,
                    KardexDate = DateTime.Now,
                    KardexTypeId = KardexTypes.InventarioFisico,
                    CustomerId = _GoodsReceivedq.CustomerId,
                    CustomerName = _GoodsReceivedq.CustomerName,
                    DocumentId = _GoodsReceivedq.GoodsReceivedId,
                    Estiba = item.ControlPalletsId,
                    SourceDocumentId = (int)_GoodsReceivedq.GoodsReceivedId,
                    SourceDocumentName = "ReciboMercaderia/GoodsReceived",
                    SourceDocumentLine = (int)item.GoodsReceiveLinedId,
                    

                };   
                _context.Kardex.Add(kardex);

                await _context.SaveChangesAsync();

                return kardex;
            }
            catch (Exception)
            {

                return BadRequest("Error al generar el kardex del producto");
            }


        }

        private async Task<ActionResult<JournalEntry>> GeneraAsientoReciboMercaderias(GoodsReceived goodsReceived) {
            try
            {
                JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                   .Where(q => q.TransactionId == 1)
                                                                   .Where(q => q.BranchId == goodsReceived.BranchId)
                                                                   .Where(q => q.EstadoName == "Activo")
                                                                   .Include(q => q.JournalEntryConfigurationLine)
                                                                   ).FirstOrDefaultAsync();
                if (_journalentryconfiguration == null)
                {
                    return BadRequest("No se encontro configuracion de asiento contable");
                }

                decimal sumacreditos = 0, sumadebitos = 0;
                //if (_journalentryconfiguration != null)
                if (_journalentryconfiguration == null)
                {
                    //Crear el asiento contable configurado
                    //.............................///////
                    BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = goodsReceived.CustomerId,
                        DocType = "JournalEntryConfiguration",
                        ClaseInicial =
                     Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "InsertGoodsReceived",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = goodsReceived.UsuarioCreacion,
                        UsuarioModificacion = goodsReceived.UsuarioModificacion,
                        UsuarioEjecucion = goodsReceived.UsuarioModificacion,

                    });
                    JournalEntry _je = new JournalEntry
                    {
                        Date = goodsReceived.OrderDate,
                        Memo = "Bienes Recibidos",
                        DatePosted = goodsReceived.OrderDate,
                        ModifiedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        ModifiedUser = goodsReceived.UsuarioModificacion,
                        CreatedUser = goodsReceived.UsuarioCreacion,
                        DocumentId = goodsReceived.GoodsReceivedId,
                    };



                    foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                    {

                        GoodsReceivedLine _iline = new GoodsReceivedLine();
                        _iline = goodsReceived._GoodsReceivedLine.Where(q => q.SubProductId == item.SubProductId).FirstOrDefault();
                        if (_iline != null || item.SubProductName.ToUpper().Contains(("Impuesto").ToUpper()))
                        {
                            if (!item.AccountName.ToUpper().Contains(("Impuestos sobre ventas").ToUpper())
                                   && !item.AccountName.ToUpper().Contains(("Sobre Servicios Diversos").ToUpper()))
                            {
                                _je.JournalEntryLines.Add(new JournalEntryLine
                                {
                                    AccountId = Convert.ToInt32(item.AccountId),
                                    Description = item.AccountName,
                                    Credit = item.DebitCredit == "Credito" ? _iline.Total : 0,
                                    Debit = item.DebitCredit == "Debito" ? _iline.Total : 0,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now,
                                    CreatedUser = goodsReceived.UsuarioCreacion,
                                    ModifiedUser = goodsReceived.UsuarioModificacion,
                                    Memo = "",
                                });

                                sumacreditos += item.DebitCredit == "Credito" ? _iline.Total : 0;
                                sumadebitos += item.DebitCredit == "Debito" ? _iline.Total : 0;
                            }
                            else
                            {
                                _je.JournalEntryLines.Add(new JournalEntryLine
                                {
                                    AccountId = Convert.ToInt32(item.AccountId),
                                    Description = item.AccountName,
                                    CreatedDate = DateTime.Now,
                                    ModifiedDate = DateTime.Now,
                                    CreatedUser = goodsReceived.UsuarioCreacion,
                                    ModifiedUser = goodsReceived.UsuarioModificacion,
                                    Memo = "",
                                });

                            }
                        }
                    }
                    if (sumacreditos != sumadebitos)
                    {
                        _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                        return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                    }
                    _je.TotalCredit = sumacreditos;
                    _je.TotalDebit = sumadebitos;
                    _context.JournalEntry.Add(_je);
                    await _context.SaveChangesAsync();
                }
                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = goodsReceived.GoodsReceivedId,
                    DocType = "GoodsReceived",
                    ClaseInicial =
                    Newtonsoft.Json.JsonConvert.SerializeObject(goodsReceived, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(goodsReceived, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insert",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = goodsReceived.UsuarioCreacion,
                    UsuarioModificacion = goodsReceived.UsuarioModificacion,
                    UsuarioEjecucion = goodsReceived.UsuarioModificacion,

                });
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        /// <summary>
        /// Actualiza la GoodsReceived
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<GoodsReceived>> Update([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = _GoodsReceived;
            try
            {
                _GoodsReceivedq = await (from c in _context.GoodsReceived
                                 .Where(q => q.GoodsReceivedId == _GoodsReceived.GoodsReceivedId)
                                         select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_GoodsReceivedq).CurrentValues.SetValues((_GoodsReceived));

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                //_context.GoodsReceived.Update(_GoodsReceivedq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));
        }

        /// <summary>
        /// Elimina una GoodsReceived       
        /// </summary>
        /// <param name="_GoodsReceived"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GoodsReceived _GoodsReceived)
        {
            GoodsReceived _GoodsReceivedq = new GoodsReceived();
            try
            {
                _GoodsReceivedq = _context.GoodsReceived
                .Where(x => x.GoodsReceivedId == (Int64)_GoodsReceived.GoodsReceivedId)
                .FirstOrDefault();

                _context.GoodsReceived.Remove(_GoodsReceivedq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_GoodsReceivedq));

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<List<GoodsReceivedLine>>> AgruparRecibos([FromBody]List<Int64> listarecibos)
        {
            List<GoodsReceivedLine> _goodsreceivedlis = new List<GoodsReceivedLine>();
            try
            {
                string inparams = "";
                foreach (var item in listarecibos)
                {
                    inparams += item +",";
                }

                inparams = inparams.Substring(0,inparams.Length-1);
                // string[] ids = listarecibos.Split(',');
                _goodsreceivedlis = await _context.GoodsReceivedLine
                    //.Select(s => s.)
                    .Where(q => listarecibos.Any(a => a == q.GoodsReceivedId))
                    //.GroupBy(g => g.SubProductId)
                    .Include(i => i.GoodsReceived)
                    .ToListAsync();
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                throw ex;
            }

            return await Task.Run(() => Ok(_goodsreceivedlis));
        }





    }
}