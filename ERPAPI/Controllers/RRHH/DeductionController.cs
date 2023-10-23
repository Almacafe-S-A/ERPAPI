using System;
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
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Deduction")]
    [ApiController]
    public class DeductionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public DeductionController(ILogger<DeductionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Deducciones, con paginacion
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDeductionPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Deduction> Items = new List<Deduction>();
            try
            {
                var query = _context.Deduction.AsQueryable();
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
        /// Obtiene el Listado de Deducciones, ordenado por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDeduction()
        {
            List<Deduction> Items = new List<Deduction>();
            try
            {
                Items = await _context.Deduction.OrderByDescending(b => b.DeductionId).ToListAsync();
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
        /// Obtiene los Datos de Deduccion por medio del Id enviado.
        /// </summary>
        /// <param name="DeductionId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{DeductionId}")]
        public async Task<IActionResult> GetDeductionById(Int64 DeductionId)
        {
            Deduction Items = new Deduction();
            try
            {
                Items = await _context.Deduction.Where(q => q.DeductionId == DeductionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el color por la descripcion enviada
        /// </summary>
        /// <param name="DeductionDescription"></param>
        /// <returns></returns>
        [HttpGet("[action]/{DeductionDescription}")]
        public async Task<IActionResult> GetDeductionByDescription(string DeductionDescription)
        {
            Deduction Items = new Deduction();
            try
            {
                Items = await _context.Deduction.Where(q => q.Description.ToUpper().Equals(DeductionDescription.ToUpper())).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{DeductionId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(Int64 DeductionId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = 0;//await _context.CheckAccount.Where(a => a.BankId == BankId)
                                //    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        /// <summary>
        /// Inserta deduccion nueva
        /// </summary>
        /// <param name="_Deduction"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Deduction>> Insert([FromBody]Deduction _Deduction)
        {
            Deduction _Deductionq = new Deduction();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Deductionq = _Deduction;
                        _context.Deduction.Add(_Deductionq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Deductionq.DeductionId,
                            DocType = "Deduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Deductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Deductionq.UsuarioCreacion,
                            UsuarioModificacion = _Deductionq.UsuarioModificacion,
                            UsuarioEjecucion = _Deductionq.UsuarioModificacion,
                            
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
            return await Task.Run(() => Ok(_Deductionq));

        }

        /// <summary>
        /// Actualiza la deduccion
        /// </summary>
        /// <param name="_Deduction"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Deduction>> Update([FromBody]Deduction _Deduction)
        {
            Deduction _Deductionq = _Deduction;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Deductionq = await (from c in _context.Deduction
                        .Where(q => q.DeductionId == _Deduction.DeductionId)
                                          select c
                        ).FirstOrDefaultAsync();

                        _context.Entry(_Deductionq).CurrentValues.SetValues((_Deduction));

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Deductionq.DeductionId,
                            DocType = "Deduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Deductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Deductionq.UsuarioCreacion,
                            UsuarioModificacion = _Deductionq.UsuarioModificacion,
                            UsuarioEjecucion = _Deductionq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Deductionq));
        }

        /// <summary>
        /// Elimina una deduccion       
        /// </summary>
        /// <param name="_Deduction"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Deduction _Deduction)
        {
            Deduction _Deductionq = new Deduction();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Deductionq = _context.Deduction
                        .Where(x => x.DeductionId == (Int64)_Deduction.DeductionId)
                        .FirstOrDefault();

                        _context.Deduction.Remove(_Deductionq);

                        //YOJOCASU 2022-02-26 REGISTRO DE LOS DATOS DE AUDITORIA
                        new appAuditor(_context, _logger, User.Identity.Name).SetAuditor();

                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Deductionq.DeductionId,
                            DocType = "Deduction",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Deductionq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Eliminar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Deductionq.UsuarioCreacion,
                            UsuarioModificacion = _Deductionq.UsuarioModificacion,
                            UsuarioEjecucion = _Deductionq.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Deductionq));

        }
    }
}
