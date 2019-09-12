﻿using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Employees
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public decimal? Salario { get; set; }
        public string Identidad { get; set; }
        public DateTime? FechaEgreso { get; set; }
        public string Direccion { get; set; }
        public string Genero { get; set; }
        public long? IdActivoinactivo { get; set; }
        public string Foto { get; set; }

        public long IdPuesto { get; set; }
        [ForeignKey("IdPuesto")]
        public  Puesto Puesto { get; set; }
        public Int64 IdEstado { get; set; }
        public  Estados Estados { get; set; }

        public long IdCity { get; set; }
        [ForeignKey("IdCity")]
        public  City City { get; set; }

        public long IdState { get; set; }
        [ForeignKey("IdState")]
        public State State { get; set; }
        public long IdCountry { get; set; }
        [ForeignKey("IdCountry")]
        public  Country Country { get; set; }
        public int IdCurrency { get; set; }
        [ForeignKey("IdCurrency")]
        public  Currency Currency { get; set; }
        public Guid ApplicationUserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
        public long IdEscala { get; set; }
        [ForeignKey("IdEscala")]
        public  Escala Escala { get; set; }
        public Int64 IdBank { get; set; }
        [ForeignKey("IdBank")]
        public  Bank Bank { get; set; }        
        public int IdBranch { get; set; }
        [ForeignKey("IdBranch")]
        public  Branch Branch { get; set; }
        public long IdTipoContrato { get; set; }
        [ForeignKey("IdTipoContrato")]
        public  TipoContrato TipoContrato { get; set; }
        public  long IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public Departamento Departamento { get; set; }

        public string CuentaBanco { get; set; }

        public DateTime FechaFinContrato { get; set; }
        public string Telefono { get; set; }
        public int Extension { get; set; }
        public string Notas { get; set; }

        public string TipoSangre { get; set; }
        public string NombreContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public int IdPlanilla { get; set; }

        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; } 
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
       // [Display(Name = "Party")]
        //public  Party Party { get; set; }

        //public int? PartyId { get; set; }
    }
}
