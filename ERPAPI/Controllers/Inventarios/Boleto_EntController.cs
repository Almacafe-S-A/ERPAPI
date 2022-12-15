using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Boleto_Ent")]
    [ApiController]
    public class Boleto_EntController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public Boleto_EntController(ILogger<Boleto_EntController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Boleto_Entes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBoleto_Ent()
        {
            List<Boleto_Ent> Items = new List<Boleto_Ent>();
            try
            {
                //Items = await _context.Boleto_Ent.ToListAsync();
                Items =  await  (from c in _context.Boleto_Ent
                             join d in _context.Boleto_Sal on c.clave_e equals d.clave_e into ba
                             join p in _context.SubProduct on c.SubProductId equals p.SubproductId 
                             join cl in _context.Customer on c.CustomerId equals cl.CustomerId
                             //join u in _context.UnitOfMeasure on c.unidad_e equals u.
                             from e in ba.DefaultIfEmpty()
                             select new Boleto_Ent
                             {
                                 clave_e = c.clave_e,
                                 bascula_e = c.bascula_e,
                                 clave_C = c.clave_C,
                                 clave_p = c.clave_p,
                                 clave_u = c.clave_u,
                                 fecha_e = c.fecha_e,
                                 completo = c.completo,
                                 hora_e = c.hora_e,
                                 conductor = c.conductor,
                                 observa_e = c.observa_e,
                                 peso_e = c.peso_e,
                                 placas = c.placas,
                                 turno_oe = c.turno_oe,
                                 t_entrada = c.t_entrada,
                                 unidad_e = c.unidad_e,
                                 Boleto_Sal = e,
                                 Cliente = cl.CustomerName,
                                 NombreProducto = p.ProductName,
                                 Ingreso =  c.Ingreso,
                                 PesoLBS = c.PesoLBS,
                                 
                                 //  Boleto_Sal =  _context.Boleto_Sal.Where(q => q.clave_e == c.clave_e).FirstOrDefault(),

                             }).OrderByDescending(o => o.clave_e).ToListAsync();


                //Items = await query
                //                     //.Include(q => q.Boleto_Sal)                             
                //                     .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }



        [HttpGet("[action]/{customerId}/{esIngreso}/{completo}")]
        public async Task<IActionResult> GetBoletasdePesoByCustomer(long customerId,bool esIngreso,bool completo)
        {
            List<Boleto_Ent> Items = new List<Boleto_Ent>();
            Customer customer = _context.Customer.Where(q => q.CustomerId == customerId).FirstOrDefault();
            List<Boleto_Ent> boletas = new List<Boleto_Ent>();
            //esIngreso = true;
            if (customer == null )
            {
                return BadRequest("No se encontro Cliente");
            }
            try
            {
                if (completo) {
                    boletas = await _context.Boleto_Ent
                    .Where(q => q.CustomerId == customerId
                    && !_context.ControlPallets.Any(a => a.WeightBallot == q.clave_e) 
                    && !(_context.BoletaDeSalida.Any(a => a.WeightBallot == q.clave_e))
                    && q.completo == completo
                    && q.Ingreso == esIngreso
                    ).ToListAsync();
                }
                else
                {
                    boletas = await _context.Boleto_Ent
                    .Where(q => q.CustomerId == customerId
                    && !_context.ControlPallets.Any(a => a.WeightBallot == q.clave_e) 
                    && !(_context.BoletaDeSalida.Any(a => a.WeightBallot == q.clave_e))
                    && q.completo == completo
                    && q.Ingreso == esIngreso
                    ).ToListAsync();
                }
                                       
                    var query =  (from c in boletas
                                 join d in _context.Boleto_Sal on c.clave_e equals d.clave_e                                 
                                 into ba
                                 from e in ba.DefaultIfEmpty()
                                 join p in _context.SubProduct on c.SubProductId equals p.SubproductId
                                 select new Boleto_Ent
                                 {
                                     clave_e = c.clave_e,
                                     bascula_e = c.bascula_e,
                                     clave_C = c.clave_C,
                                     clave_p = c.clave_p,
                                     clave_u = c.clave_u,
                                     fecha_e = c.fecha_e,
                                     completo = c.completo,
                                     hora_e = c.hora_e,
                                     conductor = c.conductor,
                                     observa_e = c.observa_e,
                                     peso_e = c.peso_e,
                                     placas = c.placas,
                                     turno_oe = c.turno_oe,
                                     t_entrada = c.t_entrada,
                                     unidad_e = c.unidad_e,
                                     Boleto_Sal = e,
                                     NombreProducto = p.ProductName
                                 }).AsQueryable();
                Items =  query.ToList();
                Items = Items
                    .Where(q => !_context.ControlPallets.Select(c => c.WeightBallot).Contains(q.clave_e))
                    .OrderByDescending(q => q.clave_e)
                    .ToList(); ///Filtra las Boletas de peso que no han sido utilizadas en ningun Control de salidas o ingresos
                
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
        public async Task<IActionResult> GetBoleto_EntMax()
        {
            Int64 Max = 0;
            try
            {
                //Max = await _context.Boleto_Ent.Select(x => x.clave_e).DefaultIfEmpty(0).Max();
                Max =  _context.Boleto_Ent.Max(x => x.clave_e);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Max));
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int64>> GetBoleto_EntCount()
        {
            // List<Boleto_Ent> Items = new List<Boleto_Ent>();
            Boleto_Ent _Boleto_Ent = new Boleto_Ent();
            Int64 Total = 0;
            try
            {

               Total = await _context.Boleto_Ent.CountAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(()=>Ok(Total));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_EntByClaveEList([FromBody]List<Int64> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                _logger.LogInformation($"Arranca comparación Entrada: {stopwatch.Elapsed}");
                //string listadoentradas = string.Join(",", clave_e_list);
                _context.Database.SetCommandTimeout(30);
                List<Int64> _encontrados = await _context.Boleto_Ent.Select(q => q.clave_e).ToListAsync();
                Items = clave_e_list.Except(_encontrados).ToList();
                _logger.LogInformation($"Termina comparación Entrada: {stopwatch.Elapsed}");

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }

        private async Task<List<Int64>> GetBatchExistsEntry(List<Int64> Id)
        {
            List<Int64> _entradasexistentes = new List<Int64>();

            try
            {
                Int64 p = 0;
                foreach (var item in Id)
                {
                    p = await _context.Boleto_Ent.Where(q => q.clave_e==item).Select(q => q.clave_e).FirstOrDefaultAsync();
                    if(p>0)
                    {
                        _entradasexistentes.Add(p);
                    }
                }
              //  _entradasexistentes = await _context.Boleto_Ent.Where(q => Id.Contains(q.clave_e)).Select(q => q.clave_e).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _entradasexistentes;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBoleto_E_ByClassList([FromBody]List<Boleto_Ent> clave_e_list)
        {
            List<Int64> Items = new List<Int64>();
            try
            {
                //using (var transaction = _context.Database.BeginTransaction())
                //{
                try
                {                  
                     _context.BulkInsert(clave_e_list);
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return Ok(Items);
        }


        /// <summary>
        /// Obtiene los Datos de la Boleto_Ent por medio del Id enviado.
        /// </summary>
        /// <param name="Boleto_EntId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Boleto_EntId}")]
        public async Task<IActionResult> GetBoleto_EntById(Int64 Boleto_EntId)
        {
            Boleto_Ent Items = new Boleto_Ent();
            if (Boleto_EntId == 0)
            {
                return NotFound();
            }
            try
            {
                Items = await _context.Boleto_Ent.Include(q=>q.Boleto_Sal).Where(q => q.clave_e == Boleto_EntId).FirstOrDefaultAsync();
                if (Items.CustomerId == null && Items!=null && Items.clave_e!=0)
                {
                    Items.CustomerId = _context.Customer.Where(q => q.CustomerRefNumber == Items.clave_C).FirstOrDefault().CustomerId;
                    Items.SubProductId = _context.SubProduct.Where(q => q.ProductCode == Items.clave_p).FirstOrDefault().SubproductId;
                }
                if (Items.clave_e == 0)
                {
                    Items = null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return Ok(Items);
        }


        /// <summary>
        /// Obtiene los Datos de la Boleto_Ent por medio del Id enviado.
        /// </summary>
        /// <param name="Boleto_EntId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Boleto_EntId}")]
        public async Task<IActionResult> GetBoleto_EntByIdCustomerUOM(Int64 Boleto_EntId)
        {
            Boleto_Ent Items = new Boleto_Ent();
            if (Boleto_EntId == 0)
            {
                return NotFound();
            }
            try
            {
                Items = await _context.Boleto_Ent
                    .Include(q => q.Boleto_Sal)
                    .Include(q => q.Customer)
                    .Where(q => q.clave_e == Boleto_EntId)
                    .FirstOrDefaultAsync();

                int uompreferida = Items.Customer.UnitOfMeasurePreference == null ? 3 : (int)Items.Customer.UnitOfMeasurePreference;

                Items.PesoUnidadPreferidaEntrada = Items.Convercion(Items.peso_e, uompreferida);
                Items.PesoUnidadPreferidaNeto = Items.Convercion(Items.Boleto_Sal.peso_s, uompreferida);
                Items.PesoUnidadPreferidaSalida = Items.Convercion(Items.Boleto_Sal.peso_n, uompreferida);
                Items.UnidadPreferidaId = uompreferida;



            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:No se encontro la unidad preferidad del cliente");
            }


            return Ok(Items);
        }


        


        /// <summary>
        /// Inserta una nueva Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Insert([FromBody]Boleto_Ent _Boleto_Ent)
        {
            _Boleto_Ent.Boleto_Sal = null;
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _Boleto_Entq = _Boleto_Ent;
                        _context.Boleto_Ent.Add(_Boleto_Entq);
                        long maxid = _context.Boleto_Ent.DefaultIfEmpty().Max(m => m.clave_e);                        
                        _Boleto_Ent.clave_e = maxid + 1;

                        SubProduct  subproduct = await _context.SubProduct.Where(q => q.SubproductId == _Boleto_Ent.SubProductId).FirstOrDefaultAsync();
                        Customer customer = await _context.Customer.Where(c => c.CustomerId == _Boleto_Ent.CustomerId).FirstOrDefaultAsync();

                        _Boleto_Ent.hora_e = _Boleto_Ent.fecha_e.ToString("hh:mm:ss");
                        _Boleto_Ent.unidad_e = "lb E.";
                        _Boleto_Ent.bascula_e = "1";
                        _Boleto_Ent.t_entrada = 1;
                        _Boleto_Ent.clave_C = customer.CustomerRefNumber;
                        _Boleto_Ent.clave_p = subproduct.ProductCode;
                        _Boleto_Ent.SubProductName = subproduct.ProductName;
                        _Boleto_Ent.clave_u = User.Identity.Name;
                        _Boleto_Ent.turno_oe = "MATUTINO";


                        



                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Boleto_Ent.clave_e,
                            DocType = "Boleto_Ent",
                            ClaseInicial =
                           Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _Boleto_Ent.UsuarioCreacion,
                            //UsuarioModificacion = _Boleto_Ent.UsuarioModificacion,
                            //UsuarioEjecucion = _Boleto_Ent.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Actualiza la Boleto_Ent
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Boleto_Ent>> Update([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = _Boleto_Ent;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Boleto_Ent boleto_Ent = _context.Boleto_Ent.Where(q => q.clave_e == _Boleto_Ent.clave_e).FirstOrDefault();
                        boleto_Ent.Boleto_Sal = _context.Boleto_Sal.Where(q => q.clave_e == _Boleto_Ent.clave_e).FirstOrDefault();
                        boleto_Ent.observa_e = _Boleto_Entq.observa_e;
                        boleto_Ent.completo = true;
                        boleto_Ent.PesoKG = _Boleto_Entq.PesoKG;
                        boleto_Ent.PesoLBS = _Boleto_Entq.PesoLBS;
                        boleto_Ent.PesoQQ = _Boleto_Entq.PesoQQ;
                        boleto_Ent.PesoTM = _Boleto_Entq.PesoTM;
                        boleto_Ent.PesoKGI = _Boleto_Entq.PesoKGI;
                        boleto_Ent.PesoLBSI = _Boleto_Entq.PesoLBSI;
                        boleto_Ent.PesoQQI = _Boleto_Entq.PesoQQI;
                        boleto_Ent.PesoTMI = _Boleto_Entq.PesoTMI;

                        if (boleto_Ent.Boleto_Sal ==null)
                        {
                            Boleto_Sal boleto_Sal = new Boleto_Sal()
                            {
                                clave_e = _Boleto_Entq.clave_e,
                                completo = true,
                                fecha_s = DateTime.Now,
                                hora_s = DateTime.Now.ToString("HH:mm:ss"),
                                peso_n = _Boleto_Ent.Boleto_Sal.peso_n,
                                peso_s = _Boleto_Ent.Boleto_Sal.peso_s,
                                turno_s = "MATUTINO",
                                s_manual = true,
                                bascula_s = "MA",
                                UsuarioSegundaPesada = User.Identity.Name,
                                



                            };

                            _context.Boleto_Sal.Add(boleto_Sal);

                        }


                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        //_context.Boleto_Ent.Update(_Boleto_Entq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Boleto_Ent.clave_e,
                            DocType = "Boleto_Ent",
                            ClaseInicial =
                                   Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Boleto_Ent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            //UsuarioCreacion = _Boleto_Ent.UsuarioCreacion,
                            //UsuarioModificacion = _Boleto_Ent.UsuarioModificacion,
                            //UsuarioEjecucion = _Boleto_Ent.UsuarioModificacion,

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

            return Ok(_Boleto_Entq);
        }

        /// <summary>
        /// Elimina una Boleto_Ent       
        /// </summary>
        /// <param name="_Boleto_Ent"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Boleto_Ent _Boleto_Ent)
        {
            Boleto_Ent _Boleto_Entq = new Boleto_Ent();
            try
            {
                _Boleto_Entq = _context.Boleto_Ent
                .Where(x => x.clave_e == (Int64)_Boleto_Ent.clave_e)
                .FirstOrDefault();

                _context.Boleto_Ent.Remove(_Boleto_Entq);

                //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return Ok(_Boleto_Entq);

        }







    }
}