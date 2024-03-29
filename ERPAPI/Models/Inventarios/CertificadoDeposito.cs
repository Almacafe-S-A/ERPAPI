﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CertificadoDeposito
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 IdCD { get; set; }
        [Display(Name = "No de certificado")]
        public string NoCD { get; set; }

        public int SolicitudCertificadoId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }


        public int? PrecioCafeId { get; set; }
        [ForeignKey("PrecioCafeId")]
        public PrecioCafe PrecioCafe { get; set; }

        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }
        ///Dirreccion de la sucursal
        public string SituadoEn { get; set; }


        [Display(Name = "Tipo Servicio")]
        public Int64 ServicioId { get; set; }
        [ForeignKey("ServicioId")]
        public Product Servicio { get; set; }

        [Display(Name = "Servicio")]
        public string ServicioName { get; set; }
        
        public string Producto { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Fecha de certificado")]
        public DateTime FechaCertificado { get; set; }

        [Display(Name = "Empresa")]
        public string NombreEmpresa { get; set; }

        public Int64? IdEstado { get; set; }
        public string Estado { get; set; }
        public int? InsuranceId { get; set; }
        [ForeignKey("InsuranceId")]
        public Insurances Insurances { get; set; }

        [Display(Name = "Seguro")]
        public string EmpresaSeguro { get; set; }


        public string Recibos { get; set; }

        public Int64? InsurancePolicyId { get; set; }
        [ForeignKey("InsurancePolicyId")]
        public InsurancePolicy InsurancePolicy { get; set; }
        [Display(Name = "No. Poliza")]
        public string NoPoliza { get; set; }
        [Display(Name = "Sujetos a pago")]
        public decimal? SujetasAPago { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public DateTime FechaVencimientoDeposito { get; set; }

        [Display(Name = "Número de traslado")]
        public string NoTraslado { get; set; }


        [Display(Name = "Aduana")]
        public string Aduana { get; set; }

        [Display(Name = "Carta de porte o manifiesto No.")]
        public string ManifiestoNo { get; set; }

        [Display(Name = "Almacenaje")]
        public string Almacenaje { get; set; }
        [Display(Name = "Seguro")]
        public string Seguro { get; set; }

        [Display(Name = "Otros Cargos")]
        public string OtrosCargos { get; set; }


        [Display(Name = "Fecha de Vencimiento")]
        public DateTime? FechaVencimientoCertificado { get; set; }


        //public string SituadoEn { get; set; }

        public decimal PorcentajeDeudas { get; set; }


        public decimal?  TotalDerechos { get; set; }

        public bool? PendienteAutorizar { get; set; }



        public string Comentario { get; set; }

        public string Mensaje { get; set; }
        [NotMapped]
        public string NoRecibo { get; set; }

        public bool? PolizaPropia { get; set; }

        /// <summary>
        /// Totales de Detalle de Linea
        /// </summary>
        [Display(Name = "Suma Cantidad")]
        public decimal Quantitysum { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Totales de Detalle de Linea
        /// </summary>


        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        [NotMapped]
        public EndososCertificados Endoso { get; set; } 

        public string Impreso { get; set; }

        public int Impresiones { get; set; }

        public int impresionesTalon { get; set; }

        public List<CertificadoLine> _CertificadoLine { get; set; } = new List<CertificadoLine>();
    }




    public class CertificadoDepositoDTO : CertificadoDeposito
    {
        public Kardex Kardex { get; set; } = new Kardex();
        public List<Int64> RecibosAsociados { get; set; }
        public int editar { get; set; } = 1;



    }



}
