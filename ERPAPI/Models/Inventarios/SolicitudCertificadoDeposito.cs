﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class SolicitudCertificadoDeposito
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 IdSCD { get; set; }
        [Display(Name = "No de solicitud")]
        public Int64 NoCD { get; set; }
        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Bodega")]
        public Int64 WarehouseId { get; set; }
        [Display(Name = "Bodega")]
        public string WarehouseName { get; set; }

        [Display(Name = "Tipo Servicio")]
        public Int64 ServicioId { get; set; }

        [Display(Name = "Servicio")]
        public string ServicioName { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Fecha de certificado")]
        public DateTime FechaCertificado { get; set; }

        [Display(Name = "Empresa")]
        public string NombreEmpresa { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        [Display(Name = "Seguro")]
        public string EmpresaSeguro { get; set; }

        [Display(Name = "No. Poliza")]
        public string NoPoliza { get; set; }
        [Display(Name = "Sujetos a pago")]
        public double SujetasAPago { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public DateTime? FechaVencimientoDeposito { get; set; }

        [Display(Name = "Número de traslado")]
        public Int64? NoTraslado { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime FechaVencimiento { get; set; }

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

        [Display(Name = "Banco")]
        public Int64? BankId { get; set; }
        [Display(Name = "Banco")]
        public string BankName { get; set; }
        [Display(Name = "Moneda")]
        public Int64 CurrencyId { get; set; }
        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }
        [Display(Name = "Monto de garantia")]
        public double? MontoGarantia { get; set; }
        [Display(Name = "Fecha pago")]
        public DateTime? FechaPagoBanco { get; set; }

        [Display(Name = "Porcentaje intereses")]
        public double? PorcentajeInteresesInsolutos { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? FechaInicioComputo { get; set; }

        [Display(Name = "Lugar de firma")]
        public string LugarFirma { get; set; }

        [Display(Name = "Fecha de firma")]
        public DateTime? FechaFirma { get; set; }

        [Display(Name = "Nombre prestatario")]
        public string NombrePrestatario { get; set; }


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

        public string Impreso { get; set; }

        public List<SolicitudCertificadoLine> _SolicitudCertificadoLine { get; set; } = new List<SolicitudCertificadoLine>();


        public SolicitudCertificadoDeposito()
        {

        }

        /// <summary>
        /// Para convertir de Certificado a Solicitud
        /// </summary>
        /// <param name="_CertificadoDeposito"></param>
        public SolicitudCertificadoDeposito (CertificadoDeposito _CertificadoDeposito)
        {
                
                Almacenaje = _CertificadoDeposito.Almacenaje;
                CustomerId = _CertificadoDeposito.CustomerId;
                CustomerName = _CertificadoDeposito.CustomerName;
                Direccion = _CertificadoDeposito.Direccion;
                EmpresaSeguro = _CertificadoDeposito.EmpresaSeguro;
                Estado = _CertificadoDeposito.Estado;
                FechaCertificado = _CertificadoDeposito.FechaCertificado;
                //FechaFirma = _CertificadoDeposito.FechaFirma;
                //FechaInicioComputo = _CertificadoDeposito.FechaInicioComputo;
                FechaVencimientoDeposito = _CertificadoDeposito.FechaVencimientoDeposito;
                //FechaVencimiento = _CertificadoDeposito.FechaVencimiento;
                NoCD = _CertificadoDeposito.IdCD;
                //FechaPagoBanco = _CertificadoDeposito.FechaPagoBanco;
                NombreEmpresa = _CertificadoDeposito.NombreEmpresa;
                //LugarFirma = _CertificadoDeposito.LugarFirma;
                //MontoGarantia = _CertificadoDeposito.MontoGarantia;
                NoPoliza = _CertificadoDeposito.NoPoliza;
                //NombrePrestatario = _CertificadoDeposito.NombrePrestatario;
                NoTraslado = _CertificadoDeposito.NoTraslado;
                OtrosCargos = _CertificadoDeposito.OtrosCargos;
                //PorcentajeInteresesInsolutos = _CertificadoDeposito.PorcentajeInteresesInsolutos;
                Seguro = _CertificadoDeposito.Seguro;
                ServicioId = _CertificadoDeposito.ServicioId;
                ServicioName = _CertificadoDeposito.ServicioName;
                Quantitysum = _CertificadoDeposito.Quantitysum;
                Total = _CertificadoDeposito.Total;
                SujetasAPago = _CertificadoDeposito.SujetasAPago == null ? 0 : (double)_CertificadoDeposito.SujetasAPago;
                // WarehouseId = _CertificadoDeposito.WarehouseId;
                // WarehouseName = _CertificadoDeposito.WarehouseName;
                Aduana = _CertificadoDeposito.Aduana;
                ManifiestoNo = _CertificadoDeposito.ManifiestoNo;

              _SolicitudCertificadoLine = ToSolicitudLine(_CertificadoDeposito._CertificadoLine);

        }


        public List<SolicitudCertificadoLine> ToSolicitudLine(List<CertificadoLine> certificadoLines)
        {
            List<SolicitudCertificadoLine> _SolicitudCertificadoLines =
                (from linea in certificadoLines
                 select
                     new SolicitudCertificadoLine
                     {
                         IdSCD = this.IdSCD,
                         Amount = linea.Amount,
                         Description = linea.Description,
                         Price = linea.Price,
                         Quantity = linea.Quantity,
                         SubProductId = linea.SubProductId,
                         SubProductName = linea.SubProductName,
                         TotalCantidad = linea.TotalCantidad,
                         UnitMeasureId = linea.UnitMeasureId,
                         UnitMeasurName = linea.UnitMeasurName,
                         ReciboId = linea.ReciboId == null ? 0 : (int)linea.ReciboId,
                         GoodsReceivedLineId = linea.GoodsReceivedLineId

                     }).ToList();

            return _SolicitudCertificadoLines;



        }
    }





}
