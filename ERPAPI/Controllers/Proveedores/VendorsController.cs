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
using Newtonsoft.Json;
using ERPAPI.Contexts;

namespace coderush.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Vendor")]
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorsController(ILogger<VendorsController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Vendor paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Vendor> Items = new List<Vendor>();
            try
            {
                var query = _context.Vendor.AsQueryable();
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

        // GET: api/Vendor
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendor()
        {
            List<Vendor> Items = new List<Vendor>();
            try
            {
                Items = await _context.Vendor.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        // api/VendorGetVendorById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetVendorById(int Id)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.VendorId.Equals(Id)).FirstOrDefaultAsync();
                if (Items == null)
                {
                    return await Task.Run(() => NotFound());
                }
                double? limite =  _context.ElementoConfiguracion.Where(q => q.Id == 181).FirstOrDefault().Valordecimal;
                Items.QtyMonth = limite == null? 0 : Convert.ToDouble(limite) ;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<double>> GetVendorLimit()
        {
            try
            {
                double? limite = _context.ElementoConfiguracion.Where(q => q.Id == 181).FirstOrDefault().Valordecimal;
                return await Task.Run(() => Ok(limite));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

           
        }


        [HttpGet("[action]/{VendorName}")]
        public async Task<IActionResult> GetVendorByName(string VendorName)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.VendorName.Equals(VendorName)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> GetVendorByRTN([FromBody]Vendor _Invoice)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.RTN == _Invoice.RTN).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{RTN}")]
        public async Task<IActionResult> GetVendorByRTNS(String RTN)
        {
            Vendor Items = new Vendor();
            try
            {
                Items = await _context.Vendor.Where(q => q.RTN == RTN).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Vendor>> Insert([FromBody]Vendor payload)
        {
            /* Vendor Vendor = payload;

             try
             {
                 _context.Vendor.Add(Vendor);
                 await _context.SaveChangesAsync();
             }
             catch (Exception ex)
             {

                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                 return BadRequest($"Ocurrio un error:{ex.Message}");
             }

             return await Task.Run(() => Ok(Vendor));*/
            Vendor _Vendorq = new Vendor();
            // Alert _Alertq = new Alert();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Vendorq = payload;
                        _context.Vendor.Add(_Vendorq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Vendorq.VendorId,
                            DocType = "Vendor",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Vendorq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Vendorq.UsuarioCreacion,
                            UsuarioModificacion = _Vendorq.UsuarioModificacion,
                            UsuarioEjecucion = _Vendorq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Vendorq));

        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Vendor>> Update([FromBody]Vendor _Vendor)
        {
            /*
            try
            {
                Vendor Vendorq = (from c in _context.Vendor
                   .Where(q => q.VendorId == _Vendor.VendorId)
                                                select c
                     ).FirstOrDefault();

                _Vendor.FechaCreacion = Vendorq.FechaCreacion;
                _Vendor.UsuarioCreacion = Vendorq.UsuarioCreacion;

                _context.Entry(Vendorq).CurrentValues.SetValues((_Vendor));
                // _context.Vendor.Update(_Vendor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Vendor));*/
            Vendor _Vendorq = _Vendor;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Vendorq = await (from c in _context.Vendor
                                         .Where(q => q.VendorId == _Vendor.VendorId)
                                                  select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Vendorq).CurrentValues.SetValues((_Vendor));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Vendor.VendorId,
                            DocType = "Vendor",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Vendorq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Vendor, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Vendor.UsuarioCreacion,
                            UsuarioModificacion = _Vendor.UsuarioModificacion,
                            UsuarioEjecucion = _Vendor.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Vendorq));

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Deletes([FromBody]Vendor payload)
        {
            Vendor Vendor = new Vendor();
            try
            {
                Vendor = _context.Vendor
                .Where(x => x.VendorId == (int)payload.VendorId)
                .FirstOrDefault();
                _context.Vendor.Remove(Vendor);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Vendor));

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationUserRole>> Delete([FromBody]Vendor payload)
        {
            Vendor Vendor = new Vendor();
            try
            {
                Vendor = _context.Vendor
                .Where(x => x.VendorId == (int)payload.VendorId)
                .FirstOrDefault();
                _context.Vendor.Remove(Vendor);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Vendor));

        }

        // GET: api/Vendors/GetProductVendorsByVendorID
        /// <summary>
        ///   Obtiene el listado de Productos por Proveedor.        
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{VendorId}")]
        public async Task<IActionResult> GetProductVendorsByVendorID(Int64 VendorId)
        {
            try
            {
                //Items = await _context.ProductRelation.Include(q=>q.Product).Include(q=>q.SubProduct).ToListAsync();
                var Items = await (from c in _context.VendorProduct
                                   join e in _context.Product on c.ProductId equals e.ProductId
                                   //join d in _context.Vendor on c.VendorId equals d.VendorId
                                   where c.VendorId == VendorId

                                   select new Product
                                   {
                                       ProductId = e.ProductId,
                                       ProductCode = e.ProductCode,
                                       ProductName =e.ProductName,
                                       DefaultBuyingPrice = e.DefaultBuyingPrice,
                                       DefaultSellingPrice = e.DefaultSellingPrice,
                                       Description = e.Description
                                       
                                   }
                               ).ToListAsync();

                return await Task.Run(() => Ok(Items));
                // Items = await _context.ProductRelation.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


        }



    }
}