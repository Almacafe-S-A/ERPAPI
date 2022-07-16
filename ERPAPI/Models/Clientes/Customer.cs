using ERPAPI.Helpers;
using ERPAPI.Models.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Cliente")]
        public Int64 CustomerId { get; set; }
        [Required]
        [Display(Name = "Nombre del cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Denominación social")]
        public string Denominacion { get; set; }


        [Display(Name = "RTN Gerente")]
        public string IdentidadApoderado { get; set; }

        [Display(Name = "Nombre Gerente")]
        public string NombreApoderado { get; set; }

        [Display(Name = "Número  de referencia de cliente")]
        public string CustomerRefNumber { get; set; }

        [Required]
        [Display(Name = "RTN del cliente")]
        public string RTN { get; set; }

        [Display(Name = "Tipo de cliente")]
        public long? CustomerTypeId { get; set; } = null;
        [ForeignKey("CustomerTypeId")]
        public CustomerType CustomerType { get; set; }

        [Display(Name = "Tipo de cliente")]
        public string CustomerTypeName { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "País")]
        public long? CountryId { get; set; } = null;
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Display(Name = "País")]
        public string CountryName { get; set; }

        [Display(Name = "Ciudad")]
        public long? CityId { get; set; } = null;
        [ForeignKey("CityId")]
        public City Ciudad { get; set; }

        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Display(Name = "Departamento")]
        public long? StateId { get; set; } = null;
        [ForeignKey("StateId")]
        public State Departamento { get; set; }

        [Display(Name = "Departamento")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Display(Name = "Teléfono de trabajo")]
        public string WorkPhone { get; set; }
        //[Display(Name = "Party")]
        //public virtual Party Party { get; set; }
        //public int? PartyId { get; set; }

        [Display(Name = "Identidad del contacto")]
        public string Identidad { get; set; }
        public string Email { get; set; }
        [Display(Name = "Persona de Contacto ")]
        public string ContactPerson { get; set; }

        [Display(Name = "Activo/Inactivo ")]
        public Int64? IdEstado { get; set; } = null;
        [ForeignKey("IdEstado")]
        public Estados Estados { get; set; }
        public string Estado { get; set; }

        [Display(Name = "Grupo económico")]
        public Int64? GrupoEconomicoId { get; set; }

        [Display(Name = "Grupo económico")]
        public string GrupoEconomico { get; set; }

        [Display(Name = "Monto de activos")]
        public double? MontoActivos { get; set; }

        [Display(Name = "Ingresos anuales")]
        public double MontoIngresosAnuales { get; set; }

        [Display(Name = "Proveedor 1")]
        public string Proveedor1 { get; set; }

        [Display(Name = "Proveedor 2")]
        public string Proveedor2 { get; set; }

        [Display(Name = "Proveedor 2")]
        public bool ClienteRecoger { get; set; }

        [Display(Name = "Proveedor 2")]
        public bool EnviarlaMensajero { get; set; }

        [Display(Name = "Dirección de envío con puntos de referencia")]
        public string DireccionEnvio { get; set; }

        [Display(Name = "Pertenece a la empresa u otra organización")]
        public string PerteneceEmpresa { get; set; }

        public double? ValorSeveridadRiesgo { get; set; }
        [NotMapped]
        public string NivelSeveridad { get; set; }
        [NotMapped]
        public string ColorHexadecimal { get; set; }

        public string Fax { get; set; }
        public long? TaxId { get; set; }
        public string SolicitadoPor { get; set; }
        public bool? EsExonerado { get; set; }
        public string Observaciones { get; set; }

        public Int64? ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }


        [Display(Name = "Confirmación por correo")]
        public bool ConfirmacionCorreo { get; set; }


        public int? UnitOfMeasurePreference { get; set; }
        [ForeignKey("UnitOfMeasurePreference")]

        public UnitOfMeasure UnitOfMeasure { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }

        public List<CustomersOfCustomer> _Customers { get; set; }

        public List<VendorOfCustomer> _Vendor { get; set; }

        public string LugarNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public EstadoCivilEnum? EstadoCivil { get; set; }
        public string ProfesionOficio { get; set; }
        public string Nacionalidad { get; set; }
        public string GiroActividadNegocio { get; set; }
        public bool? CargosPublicos { get; set; }
        public string Familiares { get; set; }
        public string Conyugue { get; set; }
        public bool? InstitucionSupervisada { get; set; }
        public string NombreFuncionario { get; set; }
        public string FirmaAuditoriaExterna { get; set; }

    }

    
}
