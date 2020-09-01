
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OFAC;
using ONUListas;
using ERPAPI.Helpers;

namespace ERP.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
    ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, AspNetUserLogins,
    AspNetRoleClaims, AspNetUserTokens>
    
    //  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //public class ApplicationDbContext :
    //    IdentityDbContext<ApplicationUser, IdentityRole, string, ApplicationUserClaim, IdentityUserRole<int>
    //        , IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Accounting> Accounting { get; set; }

        public DbSet<ConfigurationVendor> ConfigurationVendor { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocument { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalary { get; set; }

        public DbSet <Presupuesto> Presupuesto { get; set; }


        public DbSet <MantenimientoImpacto> MantenimientoImpacto { get; set; }


        public DbSet <TipodeAccionderiesgo> TipodeAccionderiesgo { get; set; }


        public DbSet<InsuranceCertificate> InsuranceCertificate { get; set; }

        public DbSet<InsuranceCertificateLine> InsurancesCertificateLines { get; set; }

        public DbSet<Concept> Concept { get; set; }

        public DbSet<InventoryTransfer> InventoryTransfer { get; set; }
        public DbSet<InventoryTransferLine> InventoryTransferLine { get; set; }
        public DbSet<Insurances> Insurances { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<TypeJournal> TypeJournal { get; set; }
        public DbSet<CostListItem> CostListItem { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }

        public DbSet<UserBranch> UserBranch { get; set; }


        public DbSet<GrupoEstado> GrupoEstado { get; set; }

        public DbSet<ExchangeRate> ExchangeRate { get; set; }

        public DbSet<JournalEntry> JournalEntry { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLine { get; set; }

        public DbSet<JournalEntryCanceled> JournalEntryCanceled { get; set; }
        public DbSet<VendorDocument> VendorDocument { get; set; }
        public DbSet<Measure> Measure { get; set; }
        public DbSet<TypeAccount> TypeAccount { get; set; }
        
        public DbSet<CheckAccountLines> CheckAccountLines { get; set; }

        public DbSet<PrecioCafe> PrecioCafe { get; set; }
        public DbSet<ContextoRiesgo> ContextoRiesgo { get; set; }
        public DbSet<ProbabilidadRiesgo> ProbabilidadRiesgo { get; set; }
        public DbSet<MatrizRiesgoCustomers> MatrizRiesgoCustomers { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public DbSet<Customer> Customer { get; set; }

       
        public DbSet<CustomersOfCustomer> CustomersOfCustomer { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<VendorType> VendorType { get; set; }
        public DbSet<SalesOrder> SalesOrder { get; set; }
       
        public DbSet<SalesOrderLine> SalesOrderLine { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<ShipmentType> ShipmentType { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }

        public DbSet<EmployeeExtraHours> EmployeeExtraHours { get; set; }
        public DbSet<EmployeeExtraHoursDetail> EmployeeExtraHoursDetail { get; set; }
        public DbSet<ScheduleSubservices> ScheduleSubservices { get; set; }
        public DbSet<PaymentScheduleRulesByCustomer> PaymentScheduleRulesByCustomer { get; set; }
        public DbSet<SubServicesWareHouse> SubServicesWareHouse { get; set; }

        public DbSet<CreditNote> CreditNote { get; set; }
        public DbSet<CreditNoteLine> CreditNoteLine { get; set; }

        public DbSet<DebitNote> DebitNote { get; set; }
        public DbSet<DebitNoteLine> DebitNoteLine { get; set; }
        public DbSet<FixedAssetGroup> FixedAssetGroup { get; set; }
        public DbSet<FixedAsset> FixedAsset { get; set; }
        public DbSet<DepreciationFixedAsset> DepreciationFixedAsset { get; set; }

        public DbSet<SeveridadRiesgo> SeveridadRiesgo { get; set; }
        public DbSet<InvoiceTransReport> InvoiceTransReport { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicy { get; set; }
        public DbSet<InsuredAssets> InsuredAssets { get; set; }
        public DbSet<AccountManagement> AccountManagement { get; set; }


        public DbSet<PayrollDeduction> PayrollDeduction { get; set; }
        /// <summary>
        /// ///Cierres
        /// </summary>
        /// 

        public DbSet<GarantiaBancaria> GarantiaBancaria { get; set; }


        public DbSet<BitacoraCierreContable> BitacoraCierreContable { get; set; }
        public DbSet<BitacoraCierreProcesos> BitacoraCierreProceso { get; set; }

        public DbSet<CierresAccounting> CierresAccounting { get; set; }

        public DbSet<CierresJournal> CierresJournal { get; set; }

        public DbSet<CierresJournalEntryLine> CierresJournalEntryLine { get; set; }

        public DbSet<InsuranceEndorsement> InsuranceEndorsement { get; set; }

        public DbSet<Estados> Estados { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<PolicyClaims> PolicyClaims { get; set; }
        public DbSet<PolicyRoles> PolicyRoles { get; set; }

        public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<SalesType> SalesType { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<ProformaInvoice> ProformaInvoice { get; set; }
        public DbSet<ProformaInvoiceLine> ProformaInvoiceLine { get; set; }
        public DbSet<InvoiceCalculation> InvoiceCalculation { get; set; }

        public DbSet<JournalEntryConfiguration> JournalEntryConfiguration { get; set; }
        public DbSet<JournalEntryConfigurationLine> JournalEntryConfigurationLine { get; set; }

        public DbSet<CertificadoDeposito> CertificadoDeposito { get; set; }
        public DbSet<CertificadoLine> CertificadoLine { get; set; }
        public DbSet<CAI> CAI { get; set; }
        public DbSet<NumeracionSAR> NumeracionSAR { get; set; }
        public DbSet<PuntoEmision> PuntoEmision { get; set; }
        public DbSet<TiposDocumento> TiposDocumento { get; set; }
        public DbSet<SubProduct> SubProduct { get; set; }
        public DbSet<ProductRelation> ProductRelation { get; set; }
        public DbSet<ProductUserRelation> ProductUserRelation { get; set; }

        public DbSet<Conditions> Conditions { get; set; }
        public DbSet<CustomerConditions> CustomerConditions { get; set; }
        public DbSet<ElementoConfiguracion> ElementoConfiguracion { get; set; }
        public DbSet<GrupoConfiguracion> GrupoConfiguracion { get; set; }
        public DbSet<ControlPallets> ControlPallets { get; set; }
        public DbSet<ControlPalletsLine> ControlPalletsLine { get; set; }
        public DbSet<GoodsReceived> GoodsReceived { get; set; }
        public DbSet<GoodsReceivedLine> GoodsReceivedLine { get; set; }
        public DbSet<GoodsDelivered> GoodsDelivered { get; set; }
        public DbSet<GoodsDeliveredLine> GoodsDeliveredLine { get; set; }
        public DbSet<CustomerProduct> CustomerProduct { get; set; }
        public DbSet<RecibosCertificado> RecibosCertificado { get; set; }
        public DbSet<PEPS> PEPS { get; set; }
        public DbSet<BlackListCustomers> BlackListCustomers { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<BankAccountTransfers> BankAccountTransfers { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<HoursWorked> HoursWorked { get; set; }
        public DbSet<HoursWorkedDetail> HoursWorkedDetail { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<PayrollEmployee> PayrollEmployee { get; set; }
        public DbSet<Bitacora> Bitacora { get; set; }
        public DbSet<CalculoPlanilla> CalculoPlanilla { get; set; }
        public DbSet<CalculoPlanillaDetalle> CalculoPlanillaDetalle { get; set; }
        public DbSet<Escala> Escala { get; set; }
        public DbSet<Formula> Formula { get; set; }
        public DbSet<FormulasAplicadas> FormulasAplicadas { get; set; }
        public DbSet<FormulasConcepto> FormulasConcepto { get; set; }
        public DbSet<FormulasConFormulas> FormulasConFormulas { get; set; }
        public DbSet<Incidencias> Incidencias { get; set; }
        public DbSet<Reconciliacion> Reconciliacion { get; set; }
        public DbSet<ReconciliacionDetalle> ReconciliacionDetalle { get; set; }
        public DbSet<ReconciliacionEscala> ReconciliacionEscala { get; set; }
        public DbSet<ReconciliacionGasto> ReconciliacionGasto { get; set; }
        public DbSet<OrdenFormula> OrdenFormula { get; set; }
        public DbSet<SolicitudCertificadoDeposito> SolicitudCertificadoDeposito { get; set; }
        public DbSet<SolicitudCertificadoLine> SolicitudCertificadoLine { get; set; }
        public DbSet<Kardex> Kardex { get; set; }
        public DbSet<KardexLine> KardexLine { get; set; }
        public DbSet<GoodsDeliveryAuthorizationLine> GoodsDeliveryAuthorizationLine { get; set; }
        public DbSet<GoodsDeliveryAuthorization> GoodsDeliveryAuthorization { get; set; }
        public DbSet<CustomerArea> CustomerArea { get; set; }
        public DbSet<EndososCertificados> EndososCertificados { get; set; }
        public DbSet<EndososCertificadosLine> EndososCertificadosLine { get; set; }
        public DbSet<EndososBono> EndososBono { get; set; }
        public DbSet<EndososBonoLine> EndososBonoLine { get; set; }
        public DbSet<EndososTalon> EndososTalon { get; set; }
        public DbSet<EndososTalonLine> EndososTalonLine { get; set; }
        public DbSet<EndososLiberacion> EndososLiberacion { get; set; }
        public DbSet<CustomerDocument> CustomerDocument { get; set; }
        public DbSet<CustomerPartners> CustomerPartners { get; set; }
        public DbSet<CustomerPhones> CustomerPhones { get; set; }
        public DbSet<PasswordHistory> PasswordHistory { get; set; }

        public DbSet<Boleto_Ent> Boleto_Ent { get; set; }
        public DbSet<Boleto_Sal> Boleto_Sal { get; set; }
        public DbSet<BoletaDeSalida> BoletaDeSalida { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<CustomerAreaProduct> CustomerAreaProduct { get; set; }
        public DbSet<CustomerAuthorizedSignature> CustomerAuthorizedSignature { get; set; }
        public DbSet<CustomerContract> CustomerContract { get; set; }
        public DbSet<CustomerContractWareHouse> CustomerContractWareHouse { get; set; }
        public DbSet<Alert> Alert { get; set; }
        public DbSet<ERPAPI.Models.Puesto> Puesto { get; set; }
        public DbSet<ERPAPI.Models.Departamento> Departamento { get; set; }
        public DbSet<ERPAPI.Models.TipoContrato> TipoContrato { get; set; }
        public DbSet<ERPAPI.Models.Dependientes> Dependientes { get; set; }
        public DbSet<ERPAPI.Models.CuentaBancoEmpleados> CuentaBancoEmpleados { get; set; }
        public DbSet<ERPAPI.Models.TipoDocumento> TipoDocumento { get; set; }
        public DbSet<ERPAPI.Models.TipoPlanillas> TipoPlanillas { get; set; }
        public DbSet<IncomeAndExpensesAccount> IncomeAndExpensesAccount { get; set; }
        public DbSet<IncomeAndExpenseAccountLine> IncomeAndExpenseAccountLine { get; set; }
        public DbSet<EmployeeAbsence> EmployeeAbsence { get; set; }
        public DbSet<PeriodicidadPago> PeriodicidadPago { get; set; }
        public DbSet<BranchPorDepartamento> BranchPorDepartamento { get; set; }
        public DbSet<Comision> Comision { get; set; }
        //  public DbSet<CDGoodsDeliveryAuthorization> CDGoodsDeliveryAuthorization { get; set; }

        /// <summary>
        /// OFAC
        /// </summary>
        public DbSet<sdnListM> sdnList { get; set; }
        public DbSet<sdnListPublshInformationM> sdnListPublshInformation { get; set; }
        public DbSet<sdnListSdnEntryM> sdnListSdnEntry { get; set; }
        public DbSet<sdnListSdnEntryIDM> sdnListSdnEntryID { get; set; }
        public DbSet<sdnListSdnEntryAkaM> sdnListSdnEntryAka { get; set; }
        public DbSet<sdnListSdnEntryAddressM> sdnListSdnEntryAddress { get; set; }
        public DbSet<sdnListSdnEntryNationalityM> sdnListSdnEntryNationality { get; set; }
        public DbSet<sdnListSdnEntryCitizenshipM> sdnListSdnEntryCitizenship { get; set; }
        public DbSet<sdnListSdnEntryDateOfBirthItemM> sdnListSdnEntryDateOfBirthItem { get; set; }
        public DbSet<sdnListSdnEntryPlaceOfBirthItemM> sdnListSdnEntryPlaceOfBirthItem { get; set; }
        public DbSet<sdnListSdnEntryVesselInfoM> sdnListSdnEntryVesselInfo { get; set; }
        public DbSet<CheckAccount> CheckAccount { get; set; }
        

        ///// <summary>
        ////////
        /// <summary>
        /// ONU 
        /// </summary>

        public DbSet<CONSOLIDATED_LISTM> CONSOLIDATED_LISTM { get; set; }
        public DbSet<INDIVIDUALM> INDIVIDUALM { get; set; }
        public DbSet<LIST_TYPEM> LIST_TYPEM { get; set; }
        public DbSet<INDIVIDUAL_ALIASM> INDIVIDUAL_ALIASM { get; set; }
        public DbSet<INDIVIDUAL_ADDRESSM> INDIVIDUAL_ADDRESSM { get; set; }
        public DbSet<INDIVIDUAL_DATE_OF_BIRTHM> INDIVIDUAL_DATE_OF_BIRTHM { get; set; }
        public DbSet<INDIVIDUAL_PLACE_OF_BIRTHM> INDIVIDUAL_PLACE_OF_BIRTHM { get; set; }
        public DbSet<INDIVIDUAL_DOCUMENTM> INDIVIDUAL_DOCUMENTM { get; set; }
        public DbSet<ENTITYM> ENTITYM { get; set; }
        public DbSet<ENTITY_ALIASM> ENTITY_ALIASM { get; set; }
        public DbSet<ENTITY_ADDRESSM> ENTITY_ADDRESSM { get; set; }
        public DbSet<INDIVIDUALSM> INDIVIDUALSM { get; set; }
        public DbSet<TITLEM> TITLEM { get; set; }
        public DbSet<DESIGNATIONM> DESIGNATIONM { get; set; }
        public DbSet<NATIONALITYM> NATIONALITYM { get; set; }
        public DbSet<ENTITIESM> ENTITIESM { get; set; }
        public DbSet<LAST_DAY_UPDATEDM> LAST_DAY_UPDATEDM { get; set; }

        public DbSet<FundingInterestRate> FundingInterestRate { get; set; }

        public DbSet<VendorProduct> VendorProduct { get; set; }

        public DbSet<MotivoConciliacion> MotivoConciliacion { get; set; }

        public DbSet<Conciliacion> Conciliacion { get; set; }
        public DbSet<ConciliacionLinea> ConciliacionLinea { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }

        public DbSet<Doc_CP> Doc_CP { get; set; }

        public DbSet<VendorInvoice> VendorInvoice { get; set; }

        public DbSet<VendorInvoiceLine> VendorInvoiceLine { get; set; }

        public DbSet<PaymentTerms> PaymentTerms { get; set; }
        public DbSet<ControlAsistencias> ControlAsistencias { get; set; }

     
        

        // Gestion de Contratos para Modulo CxC

        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Contrato_detalle> Contrato_detalle { get; set; }

        public DbSet<Contrato_plan_pagos> Contrato_plan_pagos { get; set; }
        public DbSet<Contrato_movimientos> Contrato_movimientos { get; set; }

        public DbSet<Deduction> Deduction { get; set; }

        public DbSet<RetentionReceipt> RetentionReceipt { get; set; }


        public DbSet<DeduccionEmpleado> DeduccionesEmpleados { get; set; }

        public DbSet<ISR> ISRConfiguracion { get; set; }

        public DbSet<Feriado> Feriados { get; set; }

        public DbSet<Horario> Horarios { get; set; }

        public DbSet<EmpleadoHorario> EmpleadoHorarios { get; set; }

        public DbSet<EmpleadoBiometrico> EmpleadosBiometrico { get; set; }

        public DbSet<Biometrico> Biometricos { get; set; }

        public DbSet<DetalleBiometrico> DetallesBiometricos { get; set; }

        public DbSet<PhoneLines> PhoneLines { get; set; }

        public DbSet<Periodo> Periodo { get; set; }

        public DbSet<HorasExtraBiometrico> HorasExtrasBiometrico { get; set; }

        public DbSet<LlegadasTardeBiometrico> LlegadasTardeBiometrico { get; set; }

        public DbSet<Inasistencia> Inasistencias { get; set; }

        public DbSet<TipoBonificacion> TiposBonificaciones { get; set; }

        public DbSet<Bonificacion> Bonificaciones { get; set; }

        public DbSet<ConfiguracionVacaciones> ConfiguracionVacaciones { get; set; }

        public DbSet<PagoISR> PagosISR { get; set; }

        public DbSet<Planilla> Planillas { get; set; }

        public DbSet<DetallePlanilla> DetallePlanillas { get; set; }

        public DbSet<DeduccionPlanilla> DeduccionesPlanilla { get; set; }

        public DbSet<IngresoAnual> IngresosAnuales { get; set; }

        public DbSet<ImpuestoVecinalConfiguracion> ImpuestoVecinalConfiguraciones { get; set; }

        public DbSet<CategoriaPlanilla> CategoriasPlanillas { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var Customers = new List<Customer>()
            //{
            //     new Customer(){CustomerId=1,CustomerName="CAFE TOSTANO"},
            //     new Customer(){CustomerId=2,CustomerName="CAFE INDIO"},
            //};

            //modelBuilder.Entity<Customer>().HasData(Customers);   

            //modelBuilder.Entity<ConfiguracionesGenerales>().HasBaseType<CamposAuditoria>();

            modelBuilder.Entity<UserBranch>()
            .HasIndex(k => new { k.BranchId, k.UserId })
            .IsUnique(true);


            


            modelBuilder.Entity<CheckAccount>()
            .HasIndex(u => u.CheckAccountNo)
            .IsUnique();

            //modelBuilder.Entity<UserBranch>()
            //.HasKey(k => new { k.BranchId, k.UserId });
            ////.IsUnique(true);


            modelBuilder.Entity<VendorProduct>()
           .HasIndex(p => new { p.ProductId,p.VendorId })
           .IsUnique(true);


            modelBuilder.Entity<ProductRelation>()
           .HasIndex(p => new { p.ProductId, p.SubProductId })
           .IsUnique(true);



            modelBuilder.Entity<Contrato_plan_pagos>().HasKey(t => new { t.Nro_cuota, t.ContratoId });



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PolicyClaims>()
           .HasKey(c => new { c.idroleclaim, c.IdPolicy });



            modelBuilder.Entity<Boleto_Ent>()
             .HasOne(a => a.Boleto_Sal)
             .WithOne(b => b.Boleto_Ent)
            .HasForeignKey<Boleto_Sal>(b => b.clave_e);


            modelBuilder.Entity<Product>()
           .HasIndex(p => new { p.ProductCode })
           .IsUnique(true);

            modelBuilder.Entity<UnitOfMeasure>()
             .HasIndex(p => new { p.UnitOfMeasureName })
              .IsUnique(true);

           // modelBuilder.Entity<Country>()
           //.HasIndex(p => new { p.Name })
           //.IsUnique(true);

           // modelBuilder.Entity<SubProduct>()
           //.HasIndex(p => new { p.ProductCode })
           //.IsUnique(true);

            modelBuilder.Entity<Customer>()
           .HasIndex(p => new { p.RTN })
           .IsUnique(true);

            modelBuilder.Entity<Departamento>()
            .HasIndex(e => e.NombreDepartamento)
            .IsUnique(true);

            modelBuilder.Entity<SubProduct>()
             .HasMany(c => c.ProductRelation)
             .WithOne(e => e.SubProduct)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
           .HasMany(c => c.ProductRelation)
           .WithOne(e => e.Product)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
            .HasIndex(e => e.BranchCode)
            .IsUnique(true);

            modelBuilder.Entity<Customer>()
                 .HasOne(i => i.Departamento)
                 .WithMany(c => c.Customer)
                 //.IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(i => i.Departamento)
                .WithMany(c => c.Branch)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
              .HasOne(i => i.Currency)
              .WithMany(c => c.Product)
              //.IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vendor>()
              .HasOne(i => i.State)
              .WithMany(c => c.Vendor)
              //.IsRequired()
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Employees>()
           .HasOne(i => i.City)
           .WithMany(c => c.Employees)
           //.IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
              .HasOne(i => i.Country)
              .WithMany(c => c.Employees)
                //.IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
             .HasOne(i => i.Currency)
             .WithMany(c => c.Employees)
             //.IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
          .HasOne(i => i.State)
          .WithMany(c => c.Employees)
          //.IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoriaPlanilla>().HasData(new[]
                                                             {
                                                                new CategoriaPlanilla(){Id=1, Nombre = "NOMINA"},
                                                                new CategoriaPlanilla(){Id=2, Nombre = "NOMINA CONFIDENCIAL"},
                                                                new CategoriaPlanilla(){Id=3, Nombre = "NOMINA EXTRAORDINARIA"},
                                                                new CategoriaPlanilla(){Id=4, Nombre = "OTRO"},
                                                             });
        }



        
        ///// <summary>
        ////////

     
    }
}
