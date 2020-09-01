using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.Models;
using ERP.Contexts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Produces("application/json")]
    [Route("api/Branch")]
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public BranchController(ILogger<BranchController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Branch paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBranchPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Branch> Items = new List<Branch>();
            try
            {
                var query = _context.Branch.AsQueryable();
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

        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetBranchByCustomer(Int64 CustomerId)
        {
            List<Branch> Items = new List<Branch>();
            try
            {
                Items = await _context.Branch.Where(q => q.CustomerId == CustomerId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
          
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de sucursales.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBranch()
        {
            List<Branch> Items = new List<Branch>();
            try
            {
                var user = _context.Users.Where(w => w.UserName == User.Identity.Name.ToString());
                int count = user.Count();
                List<UserBranch> branchlist = await _context.UserBranch.Where(w => w.UserId == user.FirstOrDefault().Id).ToListAsync();
                if(branchlist.Count > 0)
                {
                    Items = await _context.Branch.Where(p => branchlist.Any(b => p.BranchId == b.BranchId)).ToListAsync();
                    Items = Items.Where(q => q.CustomerId == null).ToList();
                }
                else
                {
                    Items = await _context.Branch.ToListAsync();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok( Items));
        }


        /// <summary>
        /// Obtiene el Listado de sucursales.
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = "Seguridad.Sucursales por Usuario")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBranchUserAssignement()
        {
            List<Branch> Items = new List<Branch>();
            try
            {
                Items = await _context.Branch.ToListAsync();               
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
        /// Obtiene la sucursal mediante el Id enviado.
        /// </summary>
        /// <param name="BranchId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{BranchId}")]
        public async Task<IActionResult> GetBranchById(int BranchId)
        {
            Branch Items = new Branch();
            try
            {
                Items = await _context.Branch.Where(q=>q.BranchId== BranchId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(()=> BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{BranchName}")]
        public async Task<IActionResult> GetBranchByName(String BranchName)
        {
            Branch Items = new Branch();
            try
            {
                Items = await _context.Branch.Where(q => q.BranchName == BranchName).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async  Task<IActionResult> Insert([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = payload;
                _context.Branch.Add(branch);

               

                await  _context.SaveChangesAsync();
                BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                {
                    IdOperacion = branch.BranchId,
                    DocType = "Sucursal",
                    ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(branch, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                    Accion = "Insertar",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    UsuarioModificacion = User.Identity.Name,
                    UsuarioEjecucion = User.Identity.Name,

                });
                if (branch.CustomerId != null)
                {
                    CostCenter costCenter = new CostCenter
                    {
                        CostCenterName = branch.BranchName,
                        BranchId = branch.BranchId,
                        BranchName = branch.BranchName,
                        Estado = "Activo",
                        IdEstado = 1,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name

                    };
                    _context.CostCenter.Add(costCenter);
                    await _context.SaveChangesAsync();

                    BitacoraWrite _write2 = new BitacoraWrite(_context, new Bitacora
                    {
                        IdOperacion = costCenter.CostCenterId,
                        DocType = "Centro de Costo",
                        ClaseInicial =
Newtonsoft.Json.JsonConvert.SerializeObject(branch, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                        Accion = "Insertar",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = User.Identity.Name,
                        UsuarioModificacion = User.Identity.Name,
                        UsuarioEjecucion = User.Identity.Name,

                    });

                    ApplicationUser applicationUser = _context.Users.Where(q => q.UserName == User.Identity.Name).FirstOrDefault();

                    UserBranch userbranch = new UserBranch {
                        BranchId = branch.BranchId,
                        UserId = applicationUser.Id,                        
                        BranchName = branch.BranchName,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedUser = User.Identity.Name,
                        ModifiedUser = User.Identity.Name,

                    };

                    _context.UserBranch.Add(userbranch);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(branch));
        }

        /// <summary>
        /// Actualiza una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody]Branch payload)
        {
            Branch branch = payload;
            try
            {
                branch = (from c in _context.Branch
                                    .Where(q => q.BranchId == payload.BranchId)
                                      select c
                                    ).FirstOrDefault();

                payload.FechaCreacion = branch.FechaCreacion;
                payload.UsuarioCreacion = branch.UsuarioCreacion;

                _context.Entry(branch).CurrentValues.SetValues(payload);
                // _context.Branch.Update(payload);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(branch));
        }


        /// <summary>
        /// Elimina una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Branch payload)
        {
            Branch branch = new Branch();
            try
            {
                branch = _context.Branch
               .Where(x => x.BranchId == (int)payload.BranchId)
               .FirstOrDefault();
                _context.Branch.Remove(branch);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(branch));

        }


        
    }
}