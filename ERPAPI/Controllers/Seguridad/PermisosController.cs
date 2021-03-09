using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Permisos")]
    [ApiController]
    public class PermisosController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _rolemanager;

        public PermisosController(
              UserManager<ApplicationUser> userManager,
             RoleManager<ApplicationRole> rolemanager
        )
        {
            _userManager = userManager;
            _rolemanager = rolemanager;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> ListarPermisos()
        {
            try
            {
                var permisosText = System.IO.File.ReadAllText("PermisosSistema.txt");
                permisosText = permisosText.Replace("\r", "");
                var permisos = permisosText.Split("\n");
                return await Task.Run((() => Ok(permisos)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetPermissionByUser()
        {
            try
            {
                ApplicationUser usuario = await _userManager.FindByEmailAsync(User.Identity.Name);
                var _approle = await _userManager.GetRolesAsync(usuario);
                var claims = (List<Claim>)await _userManager.GetClaimsAsync(usuario);
                var listaRoles = _approle.Select(async x => await _rolemanager.FindByNameAsync(x));
                var listClaims = listaRoles.Select(x => _rolemanager.GetClaimsAsync(x.Result).Result.Where(c => c.Value.Equals("true")).ToList());
                foreach (IList<Claim> claimsRole in listClaims)
                {
                    claims.AddRange(claimsRole.ToArray());
                }
                return await Task.Run((() => Ok(claims)));
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }
    }
}