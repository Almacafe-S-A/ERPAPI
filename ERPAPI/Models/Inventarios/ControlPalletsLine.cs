using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ControlPalletsLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Linea Id")]
        public Int64 ControlPalletsLineId { get; set; }
        [Display(Name = "Id")]
        public Int64 ControlPalletsId { get; set; }
        public int Alto { get; set; }
        public int Ancho { get; set; }
        public int Otros { get; set; }
        public double Totallinea { get; set; }
        [Display(Name = "Cantidad de Sacos Yute")]
        public int cantidadYute{ get; set; }        
        [Display(Name = "Cantidad de Sacos de Polietileno")]
        public int cantidadPoliEtileno { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Centro de costos")]
        public Int64 CenterCostId { get; set; }
        [Display(Name = "Centro de costos")]
        public string CenterCostName { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
