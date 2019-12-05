using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> TodosLosPermisos = new List<Claim>()
         {
            new Claim("Permiso","Seguridad.Iniciar Sesion"),
            new Claim("Permiso","Contabilidad.Generar Balance")
         };
    }
}
