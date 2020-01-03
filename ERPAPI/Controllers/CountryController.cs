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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Country")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CountryController(ILogger<CountryController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de Country paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Country>>> GetCountryPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Country> Items = new List<Country>();
            try
            {
                var query = _context.Country.AsQueryable();
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
        /// Obtiene el Listado de Countryes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCountry()
        {
            List<Country> Items = new List<Country>();
            try
            {
                Items = await _context.Country.ToListAsync();
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
        /// Obtiene los Datos de la Country por medio del Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCountryById(Int64 Id)
        {
            Country Items = new Country();
            try
            {
                Items = await _context.Country.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> GetCountryByName(String Name)
        {
            Country Items = new Country();
            try
            {
                Items = await _context.Country.Where(q => q.Name == Name).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Country
        /// </summary>
        /// <param name="_Country"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Country>> Insert([FromBody]Country _Country)
        {
            Country _Countryq = new Country();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {

                    try
                    {
                        _Countryq = _Country;
                        _context.Country.Add(_Countryq);
                        await _context.SaveChangesAsync();

                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Country.Id,
                            DocType = "Country",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Country.Usuariocreacion,
                            UsuarioModificacion = _Country.Usuariomodificacion,
                            UsuarioEjecucion = _Country.Usuariomodificacion,

                        });

                        await _context.SaveChangesAsync();
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

            return await Task.Run(() => Ok(_Countryq));
        }

        /// <summary>
        /// Actualiza la Country
        /// </summary>
        /// <param name="_Country"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Country>> Update([FromBody]Country _Country)
        {
            Country _Countryq = _Country;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Countryq = await (from c in _context.Country
                                  .Where(q => q.Id == _Country.Id)
                                           select c
                                 ).FirstOrDefaultAsync();

                        _context.Entry(_Countryq).CurrentValues.SetValues((_Country));

                        //_context.Country.Update(_Countryq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Country.Id,
                            DocType = "Country",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Country.Usuariocreacion,
                            UsuarioModificacion = _Country.Usuariomodificacion,
                            UsuarioEjecucion = _Country.Usuariomodificacion,

                        });

                        await _context.SaveChangesAsync();

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

            return await Task.Run(() => Ok(_Countryq));
        }

        /// <summary>
        /// Elimina una Country       
        /// </summary>
        /// <param name="_Country"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Country _Country)
        {
            Country _Countryq = new Country();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Countryq = _context.Country
                       .Where(x => x.Id == (Int64)_Country.Id)
                          .FirstOrDefault();

                        _context.Country.Remove(_Countryq);
                        await _context.SaveChangesAsync();


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Country.Id,
                            DocType = "Country",
                            ClaseInicial =
                                  Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Country, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Update",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Country.Usuariocreacion,
                            UsuarioModificacion = _Country.Usuariomodificacion,
                            UsuarioEjecucion = _Country.Usuariomodificacion,

                        });

                        await _context.SaveChangesAsync();

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

            return await Task.Run(() => Ok(_Countryq));

        }


        /// <summary>
        /// Elimina la moneda
        /// </summary>
        /// <param name="_Country"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Country>> DeleteCountry([FromBody]Country _Country)
        {
            Country country = new Country();
            try
            {
                bool flag = true;

                //Customer
                var VariableCustomer = _context.Customer.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableCustomer != null)
                {
                    flag = false;
                }
                //Vendor
                var VariableVendor = _context.Vendor.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableVendor != null)
                {
                    flag = false;
                }
                //GoodsReceived
                var VariableGoodsReceived = _context.GoodsReceived.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableGoodsReceived != null)
                {
                    flag = false;
                }

                //State
                var VariableState = _context.State.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableState != null)
                {
                    flag = false;
                }
                //BlackListCustomers
                var VariableBlackListCustomers = _context.BlackListCustomers.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableBlackListCustomers != null)
                {
                    flag = false;
                }
                //PEPS
                var VariablePEPS = _context.PEPS.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariablePEPS != null)
                {
                    flag = false;
                }
                //CompanyInfo
                var VariableCompanyInfo = _context.CompanyInfo.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableCompanyInfo != null)
                {
                    flag = false;
                }
                //Branch
                var VariableBranch = _context.Branch.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableBranch != null)
                {
                    flag = false;
                }
                //City
                var VariableCity = _context.City.Where(a => a.CountryId == _Country.Id)
                                                    .FirstOrDefault();
                if (VariableCity != null)
                {
                    flag = false;
                }

                if (flag)
                {
                    country = _context.Country
                   .Where(x => x.Id == (int)_Country.Id)
                   .FirstOrDefault();
                    _context.Country.Remove(country);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(country));

        }





    }
}