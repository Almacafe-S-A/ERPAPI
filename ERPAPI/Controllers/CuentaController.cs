using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public CuentaController(
            UserManager<ApplicationUser> userManager,
             RoleManager<ApplicationRole> rolemanager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            , ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _rolemanager = rolemanager;
            _context = context;
        }



        /// <summary>
        /// Crea una nueva cuenta de usuario,con el email y el Usuario , el cual puede autenticarse a la api , y generar un web token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return await Task.Run(() => BuildToken(model));
            }
            else
            {
                return await Task.Run(() => BadRequest("Username or password invalid"));
            }


        }


        [HttpPost("CambiarPassword")]
        public async Task<ActionResult<UserToken>> CambiarPassword([FromBody] UserInfo model)
        {
            var user = _context.Users.Where(q => q.Email == model.Email).FirstOrDefault();

            var result = await _userManager.ChangePasswordAsync(user, model.PasswordAnterior, model.Password);

            if (result.Succeeded)
            {
                return await Task.Run(() => Ok());
            }
            else
            {
                return await Task.Run(() => BadRequest(@"Contrasña o usuario incorrecto"));
            }
        }



        /// <summary>
        /// Le permite a un usuario creado , autenticarse y generar el web token para el uso de los endpoints de la API.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {

            try
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    UserToken token = BuildToken(userInfo);
                    if (token==null)
                    {
                        return BadRequest($"El Usuario no tiene ningun rol asignado");
                    }
                    //return await Task.Run(() => BuildToken(userInfo));
                    return await Task.Run(()=>token);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return await Task.Run(() => BadRequest(ModelState));
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            ApplicationUser _appuser = _context.Users.Where(q => q.Email == userInfo.Email).FirstOrDefault();
            ApplicationUserRole _approle = _context.UserRoles.Where(q => q.UserId == _appuser.Id).FirstOrDefault();
            if (_approle== null)
            {
                return null;
            }
            Branch _branch = _context.Branch.Where(b => b.BranchId == _appuser.BranchId).FirstOrDefault();
            var claims = new[]
             {
                //new Claim("UserEmail", userInfo.Email),
                //new Claim("UserName", _appuser.UserName),
                new Claim(ClaimTypes.Email,userInfo.Email),
                //new Claim(ClaimTypes.Role,_approle.RoleId.ToString(_)),
                new Claim("BranchName", _branch.BranchName),
                new Claim(ClaimTypes.Name, _appuser.UserName),
                //new Claim()                
                //new Claim(ClaimTy)
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token.
            var expiration = DateTime.UtcNow.AddMinutes(120);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,               
               signingCredentials: creds
               );

             
             Int32? cambiopassworddias = Convert.ToInt32(_context.ElementoConfiguracion.Where(q => q.Id == 20).Select(q=>q.Valordecimal).FirstOrDefault());
             if (cambiopassworddias == null) { cambiopassworddias = 0; }

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                BranchId = Convert.ToInt32(_appuser.BranchId),
                IsEnabled = _appuser.IsEnabled,
                LastPasswordChangedDate = _appuser.LastPasswordChangedDate,
                Passworddias = cambiopassworddias.Value,
            };
        }






    }
}