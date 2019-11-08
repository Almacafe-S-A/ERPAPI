using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/SubProduct")]
    public class SubProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SubProductController(ILogger<SubProductController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de SubProduct paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubProductPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                var query = _context.SubProduct.AsQueryable();
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

        // GET: api/Currency
        [HttpGet("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProduct()
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.ToListAsync();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int64>> GetSubProductCount()
        {
            //List<SubProduct> Items = new List<SubProduct>();
            SubProduct _SubProduct = new SubProduct();
            Int64 Total = 0;
            try
            {
                //Items = await _context.SubProduct.ToListAsync();
                _SubProduct = await _context.SubProduct.FromSql("select  count(SubproductId) SubproductId  from SubProduct ").FirstOrDefaultAsync();
                Total = _SubProduct.SubproductId;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Total));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int64>> GetSubProductByCodeCount()
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.Where(q => q.Description == "Balanza").ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items.Count));
        }



        [HttpGet("[action]/{codigo}")]
        public async Task<ActionResult<SubProduct>> GetSubProductByCodigoBalanza(string codigo)
        {
            SubProduct Items = new SubProduct();
            try
            {
                Items = await _context.SubProduct.Where(q => q.ProductCode == codigo).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }



        [HttpGet("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProductByCodeBalanza()
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.Where(q => q.Description == "Balanza").ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{ProductTypeId}")]
        public async Task<ActionResult<SubProduct>> GetSubProductbByProductTypeId(Int64 ProductTypeId)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.Where(q=>q.ProductTypeId== ProductTypeId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  return Ok(Items);
        }

        [HttpGet("[action]/{ProductTypeId}")]
        public async Task<ActionResult<SubProduct>> GetSubProductbByProductTypeIdPropios(Int64 ProductTypeId)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                Items = await _context.SubProduct.Where(q => q.ProductTypeId == ProductTypeId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
            //  return Ok(Items);
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

        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProductByProductCodeAndInsert([FromBody]SubProduct _SubProductp)
        {
            SubProduct Items = new SubProduct();
            try
            {
                Items = await _context.SubProduct.Where(q => q.ProductCode == _SubProductp.ProductCode).FirstOrDefaultAsync();
                if (Items == null) { Items = new SubProduct(); }
                if(Items.SubproductId==0)
                {
                    _SubProductp.FechaCreacion = DateTime.Now;
                    _SubProductp.FechaModificacion = DateTime.Now;
                  
                    var subproductinsert=  Insert(_SubProductp).Result;
                    var value = (subproductinsert.Result as ObjectResult).Value;
                    Items = ((SubProduct)(value));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
            return await Task.Run(() => Ok(Items));
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> GetSubProductoByTipoByCustomer([FromBody]CustomerTypeSubProduct _CustomerTypeSubProduct)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                List<Int64> SubProductsCustomer = (from c in _context.CustomerProduct
                                                    .Where(q=>q.CustomerId == _CustomerTypeSubProduct.CustomerId)
                                                    select c.SubProductId
                                                    ).ToList();

                Items = await _context.SubProduct
                              .Where(q=> SubProductsCustomer.Contains(q.SubproductId))
                              //.Where(q => q.ProductTypeId == _CustomerTypeSubProduct.ProductTypeId)
                              .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
            //  
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> InsertSubproduct_ClassList([FromBody]List<SubProduct> SubProducts)
        {
            List<SubProduct> Items = new List<SubProduct>();
            try
            {
                //using (var transaction = _context.Database.BeginTransaction())
                //{
                try
                {
                    List<string> _existentes = await _context.SubProduct.Where(q => q.Description == "Balanza").Select(q => q.ProductCode).ToListAsync();

                    List<string> total = SubProducts.Select(q => q.ProductCode).ToList();

                    List<string> noexistenItems = total.Except(_existentes).ToList();

                    Items = SubProducts.Where(q => noexistenItems.Contains(q.ProductCode)).ToList();

                    _context.BulkInsert(Items);
                    await _context.SaveChangesAsync();


                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    throw ex;
                }


                //  }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }




        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Insert([FromBody]SubProduct _subproduct)
        {
            SubProduct subProduct = _subproduct;
            try
            {
                subProduct.ProductTypeName = await _context.ProductType
                                            .Where(q => q.ProductTypeId == _subproduct.ProductTypeId)
                                            .Select(q=>q.ProductTypeName)
                                            .FirstOrDefaultAsync();

                _context.SubProduct.Add(subProduct);
                await  _context.SaveChangesAsync();
                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora { IdOperacion = subProduct.SubproductId,DocType="SubProducto" ,
                    ClaseInicial = Newtonsoft.Json.JsonConvert.SerializeObject(_subproduct, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                    , Accion="Insertar" , FechaCreacion =DateTime.Now , FechaModificacion = DateTime.Now, UsuarioCreacion = _subproduct.UsuarioCreacion, UsuarioModificacion = _subproduct.UsuarioModificacion
                    , UsuarioEjecucion = _subproduct.UsuarioModificacion,
                    
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(subProduct));
            //  return Ok(currency);
        }

        ///// <param name="_SubProduct"></param>
        ///// <returns></returns>
        //[HttpPost("[action]")]
        //public async Task<IActionResult> Insert([FromBody]SubProduct _SubProduct)
        //{
        //    SubProduct subproduct = new SubProduct();
        //    try
        //    {
        //        subproduct = _SubProduct;
        //        _context.SubProduct.Add(subproduct);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
        //        return BadRequest($"Ocurrio un error:{ex.Message}");
        //    }

        //    return await Task.Run(() => Ok(subproduct));
        //}


        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="_Subproduct"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Update([FromBody]SubProduct _Subproduct)
        {
            
            try
            {

                SubProduct subproductq = (from c in _context.SubProduct
                                   .Where(q => q.SubproductId == _Subproduct.SubproductId)
                                          select c
                                    ).FirstOrDefault();

                _Subproduct.ProductTypeName = await _context.ProductType
                                          .Where(q => q.ProductTypeId == _Subproduct.ProductTypeId)
                                          .Select(q => q.ProductTypeName)
                                          .FirstOrDefaultAsync();

                _Subproduct.FechaCreacion = subproductq.FechaCreacion;
                _Subproduct.UsuarioCreacion = subproductq.UsuarioCreacion;


                _context.Entry(subproductq).CurrentValues.SetValues((_Subproduct));
                //                _context.SubProduct.Update(_subproduct);
                await _context.SaveChangesAsync();
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(_Subproduct));
            //   return Ok(subproduct);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SubProduct>> Delete([FromBody]SubProduct _Currency)
        {
            SubProduct subproduct = new SubProduct();
            try
            {
                subproduct = _context.SubProduct
               .Where(x => x.SubproductId == _Currency.SubproductId)
               .FirstOrDefault();
                _context.SubProduct.Remove(subproduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            // return Ok(currency);
            return await Task.Run(() => Ok(subproduct));
        }


    }
}