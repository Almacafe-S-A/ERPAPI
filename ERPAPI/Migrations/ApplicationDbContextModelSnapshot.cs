﻿// <auto-generated />
using System;
using ERP.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ERPAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ERPAPI.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ERPAPI.Models.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("PolicyId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUserClaim");
                });

            modelBuilder.Entity("ERPAPI.Models.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("BranchName")
                        .IsRequired();

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.Property<string>("ZipCode");

                    b.HasKey("BranchId");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("ERPAPI.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyCode")
                        .IsRequired();

                    b.Property<string>("CurrencyName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("ERPAPI.Models.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<int>("CustomerTypeId");

                    b.Property<string>("Email");

                    b.Property<long>("IdEstado");

                    b.Property<string>("Identidad")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("RTN")
                        .IsRequired();

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ERPAPI.Models.CustomerType", b =>
                {
                    b.Property<long>("CustomerTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerTypeName")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.HasKey("CustomerTypeId");

                    b.ToTable("CustomerType");
                });

            modelBuilder.Entity("ERPAPI.Models.CustomersOfCustomer", b =>
                {
                    b.Property<long>("CustomerOfId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<long>("CustomerId");

                    b.Property<string>("CustomerName")
                        .IsRequired();

                    b.Property<int>("CustomerTypeId");

                    b.Property<string>("Email");

                    b.Property<string>("Identidad")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("RTN")
                        .IsRequired();

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("CustomerOfId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomersOfCustomer");
                });

            modelBuilder.Entity("ERPAPI.Models.Estados", b =>
                {
                    b.Property<long>("IdEstado")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescripcionEstado");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<long>("IdGrupoEstado");

                    b.Property<string>("NombreEstado")
                        .IsRequired();

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.HasKey("IdEstado");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("ERPAPI.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("BranchId");

                    b.Property<string>("CAI");

                    b.Property<string>("Caja");

                    b.Property<string>("Correo");

                    b.Property<int>("CurrencyId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerRefNumber");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<string>("Direccion");

                    b.Property<double>("Discount");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaLimiteEmision");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<double>("Freight");

                    b.Property<int>("IdEstado");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<DateTime>("InvoiceDueDate");

                    b.Property<string>("InvoiceName");

                    b.Property<int>("InvoiceTypeId");

                    b.Property<string>("NoConstanciadeRegistro");

                    b.Property<string>("NoFin");

                    b.Property<string>("NoInicio");

                    b.Property<string>("NoOCExenta");

                    b.Property<string>("NoSAG");

                    b.Property<int>("NumeroDEI");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("RTN");

                    b.Property<string>("Remarks");

                    b.Property<int>("SalesTypeId");

                    b.Property<int>("ShipmentId");

                    b.Property<double>("SubTotal");

                    b.Property<string>("Sucursal");

                    b.Property<double>("Tax");

                    b.Property<double>("Tax18");

                    b.Property<string>("Tefono");

                    b.Property<string>("TipoDocumento");

                    b.Property<double>("Total");

                    b.Property<double>("TotalExento");

                    b.Property<double>("TotalExonerado");

                    b.Property<double>("TotalGravado");

                    b.Property<double>("TotalGravado18");

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("InvoiceId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("ERPAPI.Models.InvoiceLine", b =>
                {
                    b.Property<long>("SalesOrderLineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<double>("Price");

                    b.Property<long>("ProductId");

                    b.Property<double>("Quantity");

                    b.Property<int>("SalesOrderId");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<string>("TaxCode");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("SalesOrderLineId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("InvoiceLine");
                });

            modelBuilder.Entity("ERPAPI.Models.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.Property<string>("type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Policy");
                });

            modelBuilder.Entity("ERPAPI.Models.PolicyClaims", b =>
                {
                    b.Property<int>("idroleclaim");

                    b.Property<Guid>("IdPolicy");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.HasKey("idroleclaim", "IdPolicy");

                    b.HasAlternateKey("IdPolicy", "idroleclaim");

                    b.ToTable("PolicyClaims");
                });

            modelBuilder.Entity("ERPAPI.Models.PolicyRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdPolicy");

                    b.Property<Guid>("IdRol");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PolicyRoles");
                });

            modelBuilder.Entity("ERPAPI.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode");

                    b.Property<int>("BranchId");

                    b.Property<int>("CurrencyId");

                    b.Property<double>("DefaultBuyingPrice");

                    b.Property<double>("DefaultSellingPrice");

                    b.Property<string>("Description");

                    b.Property<string>("ProductCode");

                    b.Property<string>("ProductImageUrl");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("UnitOfMeasureId");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ERPAPI.Models.ProductType", b =>
                {
                    b.Property<long>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ProductTypeName")
                        .IsRequired();

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("ERPAPI.Models.ProformaInvoice", b =>
                {
                    b.Property<int>("SalesOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("BranchId");

                    b.Property<string>("Correo");

                    b.Property<int>("CurrencyId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerRefNumber");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<string>("Direccion");

                    b.Property<double>("Discount");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<double>("Freight");

                    b.Property<int>("IdEstado");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("RTN");

                    b.Property<string>("Remarks");

                    b.Property<string>("SalesOrderName");

                    b.Property<int>("SalesTypeId");

                    b.Property<double>("SubTotal");

                    b.Property<double>("Tax");

                    b.Property<double>("Tax18");

                    b.Property<string>("Tefono");

                    b.Property<double>("Total");

                    b.Property<double>("TotalExento");

                    b.Property<double>("TotalExonerado");

                    b.Property<double>("TotalGravado");

                    b.Property<double>("TotalGravado18");

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("SalesOrderId");

                    b.ToTable("ProformaInvoice");
                });

            modelBuilder.Entity("ERPAPI.Models.ProformaInvoiceLine", b =>
                {
                    b.Property<long>("ProformaLineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<double>("Price");

                    b.Property<long>("ProductId");

                    b.Property<int>("ProformaInvoiceId");

                    b.Property<double>("Quantity");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<string>("TaxCode");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("ProformaLineId");

                    b.HasIndex("ProformaInvoiceId");

                    b.ToTable("ProformaInvoiceLine");
                });

            modelBuilder.Entity("ERPAPI.Models.SalesOrder", b =>
                {
                    b.Property<int>("SalesOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("BranchId");

                    b.Property<string>("Correo");

                    b.Property<int>("CurrencyId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("CustomerRefNumber");

                    b.Property<DateTime>("DeliveryDate");

                    b.Property<string>("Direccion");

                    b.Property<double>("Discount");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<double>("Freight");

                    b.Property<int>("IdEstado");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("RTN");

                    b.Property<string>("Remarks");

                    b.Property<string>("SalesOrderName");

                    b.Property<int>("SalesTypeId");

                    b.Property<double>("SubTotal");

                    b.Property<double>("Tax");

                    b.Property<double>("Tax18");

                    b.Property<string>("Tefono");

                    b.Property<double>("Total");

                    b.Property<double>("TotalExento");

                    b.Property<double>("TotalExonerado");

                    b.Property<double>("TotalGravado");

                    b.Property<double>("TotalGravado18");

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("SalesOrderId");

                    b.ToTable("SalesOrder");
                });

            modelBuilder.Entity("ERPAPI.Models.SalesOrderLine", b =>
                {
                    b.Property<long>("SalesOrderLineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountAmount");

                    b.Property<double>("DiscountPercentage");

                    b.Property<int?>("InvoiceId");

                    b.Property<double>("Price");

                    b.Property<long>("ProductId");

                    b.Property<double>("Quantity");

                    b.Property<int>("SalesOrderId");

                    b.Property<double>("SubTotal");

                    b.Property<double>("TaxAmount");

                    b.Property<string>("TaxCode");

                    b.Property<double>("TaxPercentage");

                    b.Property<double>("Total");

                    b.HasKey("SalesOrderLineId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("SalesOrderLine");
                });

            modelBuilder.Entity("ERPAPI.Models.SalesType", b =>
                {
                    b.Property<int>("SalesTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("SalesTypeName")
                        .IsRequired();

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.HasKey("SalesTypeId");

                    b.ToTable("SalesType");
                });

            modelBuilder.Entity("ERPAPI.Models.Shipment", b =>
                {
                    b.Property<int>("ShipmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsFullShipment");

                    b.Property<int>("SalesOrderId");

                    b.Property<DateTime>("ShipmentDate");

                    b.Property<string>("ShipmentName");

                    b.Property<int>("ShipmentTypeId");

                    b.Property<int>("WarehouseId");

                    b.HasKey("ShipmentId");

                    b.ToTable("Shipment");
                });

            modelBuilder.Entity("ERPAPI.Models.ShipmentType", b =>
                {
                    b.Property<int>("ShipmentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ShipmentTypeName")
                        .IsRequired();

                    b.HasKey("ShipmentTypeId");

                    b.ToTable("ShipmentType");
                });

            modelBuilder.Entity("ERPAPI.Models.Tax", b =>
                {
                    b.Property<long>("TaxId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("TaxCode");

                    b.Property<double>("TaxPercentage");

                    b.Property<string>("UsuarioCreacion");

                    b.Property<string>("UsuarioModificacion");

                    b.HasKey("TaxId");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("ERPAPI.Models.UnitOfMeasure", b =>
                {
                    b.Property<int>("UnitOfMeasureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("UnitOfMeasureName")
                        .IsRequired();

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.HasKey("UnitOfMeasureId");

                    b.ToTable("UnitOfMeasure");
                });

            modelBuilder.Entity("ERPAPI.Models.Vendor", b =>
                {
                    b.Property<long>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<string>("VendorName")
                        .IsRequired();

                    b.Property<int>("VendorTypeId");

                    b.Property<string>("ZipCode");

                    b.HasKey("VendorId");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("ERPAPI.Models.VendorOfCustomer", b =>
                {
                    b.Property<long>("VendorOfId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactPerson");

                    b.Property<long?>("CustomerId");

                    b.Property<string>("Email");

                    b.Property<string>("Phone");

                    b.Property<string>("State");

                    b.Property<long>("VendorId");

                    b.Property<string>("VendorName")
                        .IsRequired();

                    b.Property<int>("VendorTypeId");

                    b.Property<string>("ZipCode");

                    b.HasKey("VendorOfId");

                    b.HasIndex("CustomerId");

                    b.ToTable("VendorOfCustomer");
                });

            modelBuilder.Entity("ERPAPI.Models.VendorType", b =>
                {
                    b.Property<long>("VendorTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("VendorTypeName")
                        .IsRequired();

                    b.HasKey("VendorTypeId");

                    b.ToTable("VendorType");
                });

            modelBuilder.Entity("ERPAPI.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("UsuarioCreacion")
                        .IsRequired();

                    b.Property<string>("UsuarioModificacion")
                        .IsRequired();

                    b.Property<string>("WarehouseName")
                        .IsRequired();

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ERPAPI.Models.CustomersOfCustomer", b =>
                {
                    b.HasOne("ERPAPI.Models.Customer")
                        .WithMany("_Customers")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ERPAPI.Models.InvoiceLine", b =>
                {
                    b.HasOne("ERPAPI.Models.SalesOrder", "SalesOrder")
                        .WithMany()
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ERPAPI.Models.ProformaInvoiceLine", b =>
                {
                    b.HasOne("ERPAPI.Models.ProformaInvoice", "ProformaInvoice")
                        .WithMany("ProformaInvoiceLine")
                        .HasForeignKey("ProformaInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ERPAPI.Models.SalesOrderLine", b =>
                {
                    b.HasOne("ERPAPI.Models.Invoice")
                        .WithMany("SalesOrderLines")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("ERPAPI.Models.SalesOrder", "SalesOrder")
                        .WithMany("SalesOrderLines")
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ERPAPI.Models.VendorOfCustomer", b =>
                {
                    b.HasOne("ERPAPI.Models.Customer")
                        .WithMany("_Vendor")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ERPAPI.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ERPAPI.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ERPAPI.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ERPAPI.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
