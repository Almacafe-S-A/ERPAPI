﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ScheduleSubservices
    {

        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ScheduleSubservicesId { get; set; }

        [Display(Name = "Día")]
        public string Day { get; set; }

        [Display(Name = "Condición")]
        public string Condition { get; set; }

        [Display(Name = "Hora de Inicio")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Hora de Fin")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Cantidad de horas")]
        public double QuantityHours { get; set; }

        [Display(Name = "Subservicio")]
        public Int64 SubServiceId { get; set; }

        [Display(Name = "Descripción")]
        public double Description { get; set; }

        [Display(Name = "Genera Transporte")]
        public bool Transport { get; set; }



    }
}
