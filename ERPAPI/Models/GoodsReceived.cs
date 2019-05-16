﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsReceived
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GoodsReceivedId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Fecha")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Fecha de documento")]
        public DateTime DocumentDate { get; set; }     
        [Display(Name = "Recibimos de")]
        public string Name { get; set; }
        [Display(Name = "Referencia")]
        public string Reference { get; set; }

        [Display(Name = "Boleta de salida")]
        public Int64 ExitTicket { get; set; }
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
     

    }
}
