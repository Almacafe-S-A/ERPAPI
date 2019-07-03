using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OFAC;

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
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<PolicyClaims> PolicyClaims { get; set; }
        public DbSet<PolicyRoles> PolicyRoles { get; set; }
       // public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }       
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
        public DbSet<CertificadoDeposito> CertificadoDeposito { get; set; }
        public DbSet<CertificadoLine> CertificadoLine { get; set; }
        public DbSet<CAI> CAI { get; set; }
        public DbSet<NumeracionSAR> NumeracionSAR { get; set; }
        public DbSet<PuntoEmision> PuntoEmision { get; set; }
        public DbSet<TiposDocumento> TiposDocumento { get; set; }
        public DbSet<SubProduct> SubProduct { get; set; }
        public DbSet<ProductRelation> ProductRelation { get; set; }
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
        public DbSet<City> City { get; set; }
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

        public DbSet<Boleto_Ent> Boleto_Ent { get; set; }
        public DbSet<Boleto_Sal> Boleto_Sal { get; set; }

        public DbSet<ERPAPI.Models.Puesto> Puesto { get; set; }

        public DbSet<ERPAPI.Models.Empresa> Empresa { get; set; }

        public DbSet<ERPAPI.Models.Departamento> Departamento { get; set; }

        public DbSet<ERPAPI.Models.TipoContrato> TipoContrato { get; set; }

        public DbSet<ERPAPI.Models.Dependientes> Dependientes { get; set; }

        public DbSet<ERPAPI.Models.TipoDocumento> TipoDocumento { get; set; }

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

        ///// <summary>
        ////////

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var Customers = new List<Customer>()
            //{
            //     new Customer(){CustomerId=1,CustomerName="CAFE TOSTANO"},
            //     new Customer(){CustomerId=2,CustomerName="CAFE INDIO"},
            //};

            //modelBuilder.Entity<Customer>().HasData(Customers);         
          

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PolicyClaims>()
           .HasKey(c => new { c.idroleclaim, c.IdPolicy });

          modelBuilder.Entity<Boleto_Ent>()
            .HasOne(a => a.Boleto_Sal)
            .WithOne(b => b.Boleto_Ent)
           .HasForeignKey<Boleto_Sal>(b => b.clave_e);


            //modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");

            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims"); 
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");



        }

        ///// <summary>
        ////////

     
    }
}
