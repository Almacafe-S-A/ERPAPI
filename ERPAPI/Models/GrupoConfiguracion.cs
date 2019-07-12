using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GrupoConfiguracion
    {
        [Key]
        [Display(Name = "Id")]
        public long IdConfiguracion { get; set; }
        [Display(Name = "Configuración")]
        public string Nombreconfiguracion { get; set; }
        [Display(Name = "Centro de costos")]
        public string Tipoconfiguracion { get; set; }
        [Display(Name = "Id Configuración")]
        public long? IdConfiguracionorigen { get; set; }
        [Display(Name = "Id Destino")]
        public long? IdConfiguraciondestino { get; set; }
        [Display(Name = "Zona")]
        public long IdZona { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }


      


    }


}
