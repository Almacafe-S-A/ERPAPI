﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class EmpleadoBiometrico
    {
        [Display(Name = "Id")]
        [Key]
        public long EmpleadoId { get; set; }

        [Required]
        public long BiometricoId { get; set; }

        [ForeignKey("EmpleadoId")]
        public Employees Empleado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class EmpleadoBiometricoAsignacionDTO
    {
        public long EmpleadoId { get; set; }

        public string NombreEmpleado { get; set; }
        
        public long? BiometricoId { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
