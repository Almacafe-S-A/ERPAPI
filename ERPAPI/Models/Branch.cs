using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Branch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int BranchId { get; set; }
        [Display(Name = "Codigo Sucursal") ]
        public string BranchCode { get; set; }
        public int Numero { get; set; }

        [Display(Name = "Cliente")]
        public Int64? CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Nombre Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        [Display(Name = "Moneda")]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Display(Name = "Ciudad")]
        public long CityId { get; set; }
        [ForeignKey("CityId")]
        public City Ciudad { get; set; }

       

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "País")]
        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Display(Name = "País")]
        public string CountryName { get; set; }
        [Display(Name = "Limite de CNBS")]
        public decimal? LimitCNBS { get; set; }

        [Display(Name = "Estado")]
        public long StateId { get; set; }
        [ForeignKey("StateId")]
        public State Departamento { get; set; }

        [Display(Name = "Departamento")]
        public string State { get; set; }
        [Display(Name = "Code Zip ")]
        public string ZipCode { get; set; }
        [Display(Name = "Telefono ")]
        public string Phone { get; set; }
        [Display(Name = "Correo ")]
        public string Email { get; set; }

        [Display(Name = "Persona de contacto")]
        public string ContactPerson { get; set; }
        
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
       
        public Int64 IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public Estados Estados { get; set; }
        public string Estado { get; set; }

        public string URL { get; set; }

        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }

        List<PuntoEmision> PuntoEmision = new List<PuntoEmision>();
    }
}
