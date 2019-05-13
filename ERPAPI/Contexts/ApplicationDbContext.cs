using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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

            //modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");

            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims"); 
            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims");



        }
    }
}
