﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerConditions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ClientConditionId { get; set; }
        public Int64 ConditionId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 IdTipoDocumento { get; set; }
        public string ClientConditionName { get; set; }
        public string LogicalCondition { get; set; }
        public string ValueToEvaluate { get; set; }
        public double ValueDecimal { get; set; }
        public string ValueString { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}