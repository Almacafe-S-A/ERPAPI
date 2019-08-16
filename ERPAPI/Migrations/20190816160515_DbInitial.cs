﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class DbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClass",
                columns: table => new
                {
                    AccountClassid = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NormalBalance = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClass", x => x.AccountClassid);
                });

            migrationBuilder.CreateTable(
                name: "Alert",
                columns: table => new
                {
                    AlertId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<long>(nullable: false),
                    DocumentName = table.Column<string>(nullable: true),
                    AlertName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ActionTakenId = table.Column<long>(nullable: false),
                    ActionTakenName = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    SujetaARos = table.Column<bool>(nullable: false),
                    FalsoPositivo = table.Column<bool>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    DescriptionAlert = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alert", x => x.AlertId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    LastPasswordChangedDate = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdOperacion = table.Column<long>(nullable: true),
                    HoraInicio = table.Column<DateTime>(nullable: true),
                    HoraFin = table.Column<DateTime>(nullable: true),
                    Accion = table.Column<string>(nullable: true),
                    NoReferencia = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    QueryEjecuto = table.Column<string>(nullable: true),
                    UsuarioEjecucion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    ResultadoSerializado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlackListCustomers",
                columns: table => new
                {
                    BlackListId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerReference = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Referencia = table.Column<string>(nullable: true),
                    Origen = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListCustomers", x => x.BlackListId);
                });

            migrationBuilder.CreateTable(
                name: "BoletaDeSalida",
                columns: table => new
                {
                    BoletaDeSalidaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveredId = table.Column<long>(nullable: false),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false),
                    Vigilante = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Motorista = table.Column<string>(nullable: true),
                    CargadoId = table.Column<long>(nullable: false),
                    Cargadoname = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoletaDeSalida", x => x.BoletaDeSalidaId);
                });

            migrationBuilder.CreateTable(
                name: "Boleto_Ent",
                columns: table => new
                {
                    clave_e = table.Column<long>(nullable: false),
                    clave_C = table.Column<string>(nullable: true),
                    clave_o = table.Column<string>(nullable: true),
                    clave_p = table.Column<string>(nullable: true),
                    completo = table.Column<bool>(nullable: false),
                    fecha_e = table.Column<DateTime>(nullable: false),
                    hora_e = table.Column<string>(nullable: true),
                    placas = table.Column<string>(nullable: true),
                    conductor = table.Column<string>(nullable: true),
                    peso_e = table.Column<int>(nullable: false),
                    observa_e = table.Column<string>(nullable: true),
                    nombre_oe = table.Column<string>(nullable: true),
                    turno_oe = table.Column<string>(nullable: true),
                    unidad_e = table.Column<string>(nullable: true),
                    bascula_e = table.Column<string>(nullable: true),
                    t_entrada = table.Column<int>(nullable: false),
                    clave_u = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto_Ent", x => x.clave_e);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "CAI",
                columns: table => new
                {
                    IdCAI = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    _cai = table.Column<string>(nullable: true),
                    FechaRecepcion = table.Column<DateTime>(nullable: false),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAI", x => x.IdCAI);
                });

            migrationBuilder.CreateTable(
                name: "CalculoPlanilla",
                columns: table => new
                {
                    Idcalculo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fechainicio = table.Column<DateTime>(nullable: true),
                    Fechafin = table.Column<DateTime>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    TasaCambio = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoPlanilla", x => x.Idcalculo);
                });

            migrationBuilder.CreateTable(
                name: "CalculoPlanillaDetalle",
                columns: table => new
                {
                    Iddetallecalculo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idempleado = table.Column<long>(nullable: true),
                    IdCalculo = table.Column<long>(nullable: true),
                    IdPuesto = table.Column<long>(nullable: true),
                    IdFormula = table.Column<long>(nullable: true),
                    ValorFormula = table.Column<decimal>(nullable: true),
                    IdtipoFormula = table.Column<int>(nullable: true),
                    NombreTipoFormula = table.Column<string>(nullable: true),
                    FormulaEjecutada = table.Column<string>(nullable: true),
                    Nombreempleado = table.Column<string>(nullable: true),
                    NombreFormula = table.Column<string>(nullable: true),
                    IdOrdenFormula = table.Column<long>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoPlanillaDetalle", x => x.Iddetallecalculo);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoDeposito",
                columns: table => new
                {
                    IdCD = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoCD = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    ServicioId = table.Column<long>(nullable: false),
                    ServicioName = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCertificado = table.Column<DateTime>(nullable: false),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    EmpresaSeguro = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    SujetasAPago = table.Column<double>(nullable: false),
                    FechaVencimientoDeposito = table.Column<DateTime>(nullable: false),
                    NoTraslado = table.Column<long>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Almacenaje = table.Column<string>(nullable: true),
                    Seguro = table.Column<string>(nullable: true),
                    OtrosCargos = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    MontoGarantia = table.Column<double>(nullable: false),
                    FechaPagoBanco = table.Column<DateTime>(nullable: false),
                    PorcentajeInteresesInsolutos = table.Column<double>(nullable: false),
                    FechaInicioComputo = table.Column<DateTime>(nullable: false),
                    LugarFirma = table.Column<string>(nullable: true),
                    FechaFirma = table.Column<DateTime>(nullable: false),
                    NombrePrestatario = table.Column<string>(nullable: true),
                    Quantitysum = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoDeposito", x => x.IdCD);
                });

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    CompanyInfoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company_Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tax_Id = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    RTNMANAGER = table.Column<string>(nullable: true),
                    PrintHeader = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    RevOffice = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.CompanyInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    ConditionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConditionName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.ConditionId);
                });

            migrationBuilder.CreateTable(
                name: "CONSOLIDATED_LISTM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dateGenerated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSOLIDATED_LISTM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControlPallets",
                columns: table => new
                {
                    ControlPalletsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Motorista = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    DescriptionProduct = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    PalletId = table.Column<int>(nullable: false),
                    EsIngreso = table.Column<int>(nullable: false),
                    EsSalida = table.Column<int>(nullable: false),
                    SubTotal = table.Column<int>(nullable: false),
                    TotalSacos = table.Column<int>(nullable: false),
                    TotalSacosPolietileno = table.Column<int>(nullable: false),
                    TotalSacosYute = table.Column<int>(nullable: false),
                    SacosDevueltos = table.Column<int>(nullable: false),
                    QQPesoBruto = table.Column<double>(nullable: false),
                    Tara = table.Column<double>(nullable: false),
                    QQPesoNeto = table.Column<double>(nullable: false),
                    QQPesoFinal = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true),
                    WeightBallot = table.Column<long>(nullable: false),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPallets", x => x.ControlPalletsId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SortName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneCode = table.Column<int>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyName = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(nullable: false),
                    RTN = table.Column<string>(nullable: false),
                    CustomerTypeId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    GrupoEconomico = table.Column<string>(nullable: true),
                    MontoActivos = table.Column<double>(nullable: false),
                    MontoIngresosAnuales = table.Column<double>(nullable: false),
                    Proveedor1 = table.Column<string>(nullable: true),
                    Proveedor2 = table.Column<string>(nullable: true),
                    ClienteRecoger = table.Column<bool>(nullable: false),
                    EnviarlaMensajero = table.Column<bool>(nullable: false),
                    DireccionEnvio = table.Column<string>(nullable: true),
                    PerteneceEmpresa = table.Column<string>(nullable: true),
                    ConfirmacionCorreo = table.Column<bool>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerArea",
                columns: table => new
                {
                    CustomerAreaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    TypeId = table.Column<long>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    Ancho = table.Column<double>(nullable: false),
                    Alto = table.Column<double>(nullable: false),
                    Largo = table.Column<double>(nullable: false),
                    UsedArea = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerArea", x => x.CustomerAreaId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAuthorizedSignature",
                columns: table => new
                {
                    CustomerAuthorizedSignatureId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Listados = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAuthorizedSignature", x => x.CustomerAuthorizedSignatureId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerConditions",
                columns: table => new
                {
                    CustomerConditionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConditionId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CustomerConditionName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LogicalCondition = table.Column<string>(nullable: true),
                    ValueToEvaluate = table.Column<string>(nullable: true),
                    ValueDecimal = table.Column<double>(nullable: false),
                    ValueString = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerConditions", x => x.CustomerConditionId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContract",
                columns: table => new
                {
                    CustomerContractId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerManager = table.Column<string>(nullable: true),
                    RTNCustomerManager = table.Column<string>(nullable: true),
                    CustomerManagerProfesionNacionalidad = table.Column<string>(nullable: true),
                    CustomerConstitution = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<long>(nullable: false),
                    Manager = table.Column<string>(nullable: true),
                    RTNMANAGER = table.Column<string>(nullable: true),
                    StorageTime = table.Column<string>(nullable: true),
                    LatePayment = table.Column<double>(nullable: false),
                    UsedArea = table.Column<double>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Reception = table.Column<string>(nullable: true),
                    WareHouses = table.Column<string>(nullable: true),
                    ValueCD = table.Column<double>(nullable: false),
                    ValueSecure = table.Column<double>(nullable: false),
                    ValueBascula = table.Column<double>(nullable: false),
                    DelegateSalary = table.Column<double>(nullable: false),
                    WarehouseRequirements = table.Column<string>(nullable: true),
                    Resolution = table.Column<string>(nullable: true),
                    Mercancias = table.Column<string>(nullable: true),
                    BandaTransportadora = table.Column<double>(nullable: false),
                    ExtraHours = table.Column<double>(nullable: false),
                    FoodPayment = table.Column<double>(nullable: false),
                    Transport = table.Column<double>(nullable: false),
                    Porcentaje1 = table.Column<double>(nullable: false),
                    Porcentaje2 = table.Column<double>(nullable: false),
                    FechaContrato = table.Column<DateTime>(nullable: false),
                    MontaCargas = table.Column<double>(nullable: false),
                    MulasHidraulicas = table.Column<double>(nullable: false),
                    Papeleria = table.Column<double>(nullable: false),
                    Valor1 = table.Column<double>(nullable: false),
                    Valor2 = table.Column<double>(nullable: false),
                    Correo1 = table.Column<string>(nullable: true),
                    Correo2 = table.Column<string>(nullable: true),
                    Correo3 = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContract", x => x.CustomerContractId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContractWareHouse",
                columns: table => new
                {
                    CustomerContractWareHouseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerContractId = table.Column<long>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    EdificioName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContractWareHouse", x => x.CustomerContractWareHouseId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocument",
                columns: table => new
                {
                    CustomerDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocument", x => x.CustomerDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPartners",
                columns: table => new
                {
                    PartnerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Identidad = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Listados = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPartners", x => x.PartnerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhones",
                columns: table => new
                {
                    CustomerPhoneId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhones", x => x.CustomerPhoneId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProduct",
                columns: table => new
                {
                    CustomerProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    SaldoProductoCertificado = table.Column<double>(nullable: false),
                    SaldoProductoTotal = table.Column<double>(nullable: false),
                    SaldoProductoSacos = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProduct", x => x.CustomerProductId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    CustomerTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.CustomerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    IdDepartamento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.IdDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Dependientes",
                columns: table => new
                {
                    IdDependientes = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreDependientes = table.Column<string>(nullable: true),
                    Parentezco = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependientes", x => x.IdDependientes);
                });

            migrationBuilder.CreateTable(
                name: "DESIGNATIONM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DESIGNATIONM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Num = table.Column<string>(maxLength: 30, nullable: false),
                    DimCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 60, nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Num);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmpleado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Puesto = table.Column<long>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: true),
                    FechaEgreso = table.Column<DateTime>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    NombreEstado = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true),
                    IdPais = table.Column<long>(nullable: true),
                    NombrePais = table.Column<string>(nullable: true),
                    IdCiudad = table.Column<long>(nullable: true),
                    NombreCiudad = table.Column<string>(nullable: true),
                    MonedaSalario = table.Column<long>(nullable: true),
                    Userid = table.Column<string>(nullable: true),
                    Idsescalas = table.Column<long>(nullable: false),
                    IdActivoinactivo = table.Column<long>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    IdBanco = table.Column<long>(nullable: true),
                    CuentaBanco = table.Column<string>(nullable: true),
                    FechaFinContrato = table.Column<DateTime>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Extension = table.Column<int>(nullable: false),
                    Notas = table.Column<string>(nullable: true),
                    IdPuesto = table.Column<int>(nullable: false),
                    NombrePuesto = table.Column<string>(nullable: true),
                    IdSucursal = table.Column<int>(nullable: false),
                    NombreSucursal = table.Column<string>(nullable: true),
                    IdTipoContrato = table.Column<int>(nullable: false),
                    IdDepartamento = table.Column<int>(nullable: false),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    NombreContacto = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "EndososBono",
                columns: table => new
                {
                    EndososBonoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososBono", x => x.EndososBonoId);
                });

            migrationBuilder.CreateTable(
                name: "EndososCertificados",
                columns: table => new
                {
                    EndososCertificadosId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososCertificados", x => x.EndososCertificadosId);
                });

            migrationBuilder.CreateTable(
                name: "EndososLiberacion",
                columns: table => new
                {
                    EndososLiberacionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososId = table.Column<long>(nullable: false),
                    EndososLineId = table.Column<long>(nullable: false),
                    TipoEndoso = table.Column<string>(nullable: true),
                    FechaLiberacion = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososLiberacion", x => x.EndososLiberacionId);
                });

            migrationBuilder.CreateTable(
                name: "EndososTalon",
                columns: table => new
                {
                    EndososTalonId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CantidadEndosar = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<double>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<double>(nullable: false),
                    TotalEndoso = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososTalon", x => x.EndososTalonId);
                });

            migrationBuilder.CreateTable(
                name: "ENTITIESM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITIESM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escala",
                columns: table => new
                {
                    IdEscala = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEscala = table.Column<string>(nullable: true),
                    DescripcionEscala = table.Column<string>(nullable: true),
                    ValorInicial = table.Column<decimal>(nullable: true),
                    ValorFinal = table.Column<decimal>(nullable: true),
                    Porcentaje = table.Column<decimal>(nullable: true),
                    ValorCalculo = table.Column<decimal>(nullable: true),
                    Idpadre = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escala", x => x.IdEscala);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    IdEstado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEstado = table.Column<string>(nullable: false),
                    DescripcionEstado = table.Column<string>(nullable: true),
                    IdGrupoEstado = table.Column<long>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "Formula",
                columns: table => new
                {
                    IdFormula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreFormula = table.Column<string>(nullable: true),
                    CalculoFormula = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    NombreEstado = table.Column<string>(nullable: true),
                    IdTipoFormula = table.Column<int>(nullable: true),
                    NombreTipoformula = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formula", x => x.IdFormula);
                });

            migrationBuilder.CreateTable(
                name: "FormulasAplicadas",
                columns: table => new
                {
                    IdFormulaAplicada = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFormula = table.Column<long>(nullable: true),
                    NombreFormula = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    FormulaEmpleada = table.Column<string>(nullable: true),
                    MultiplicarPor = table.Column<decimal>(nullable: true),
                    IdCalculo = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasAplicadas", x => x.IdFormulaAplicada);
                });

            migrationBuilder.CreateTable(
                name: "FormulasConcepto",
                columns: table => new
                {
                    IdformulaConcepto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idformula = table.Column<long>(nullable: true),
                    IdConcepto = table.Column<long>(nullable: true),
                    NombreConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConcepto", x => x.IdformulaConcepto);
                });

            migrationBuilder.CreateTable(
                name: "FormulasConFormulas",
                columns: table => new
                {
                    IdFormulaconformula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFormula = table.Column<long>(nullable: true),
                    IdFormulachild = table.Column<long>(nullable: true),
                    NombreFormulachild = table.Column<string>(nullable: true),
                    EstructuraConcepto = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    Fechamodificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormulasConFormulas", x => x.IdFormulaconformula);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerHeader",
                columns: table => new
                {
                    GeneralLedgerHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    DocumentType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerHeader", x => x.GeneralLedgerHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDelivered",
                columns: table => new
                {
                    GoodsDeliveredId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ExitTicket = table.Column<long>(nullable: false),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    WeightBallot = table.Column<long>(nullable: false),
                    PesoBruto = table.Column<double>(nullable: false),
                    TaraTransporte = table.Column<double>(nullable: false),
                    PesoNeto = table.Column<double>(nullable: false),
                    TaraUnidadMedida = table.Column<double>(nullable: false),
                    PesoNeto2 = table.Column<double>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDelivered", x => x.GoodsDeliveredId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthorization",
                columns: table => new
                {
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorizationName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    AuthorizationDate = table.Column<DateTime>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    TotalCertificado = table.Column<double>(nullable: false),
                    TotalFinanciado = table.Column<double>(nullable: false),
                    DerechoLps = table.Column<double>(nullable: false),
                    NoPoliza = table.Column<long>(nullable: false),
                    DelegadoFiscal = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthorization", x => x.GoodsDeliveryAuthorizationId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceived",
                columns: table => new
                {
                    GoodsReceivedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    ExitTicket = table.Column<long>(nullable: false),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    WeightBallot = table.Column<long>(nullable: false),
                    PesoBruto = table.Column<double>(nullable: false),
                    TaraTransporte = table.Column<double>(nullable: false),
                    PesoNeto = table.Column<double>(nullable: false),
                    TaraUnidadMedida = table.Column<double>(nullable: false),
                    PesoNeto2 = table.Column<double>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceived", x => x.GoodsReceivedId);
                });

            migrationBuilder.CreateTable(
                name: "GrupoConfiguracion",
                columns: table => new
                {
                    IdConfiguracion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombreconfiguracion = table.Column<string>(nullable: true),
                    Tipoconfiguracion = table.Column<string>(nullable: true),
                    IdConfiguracionorigen = table.Column<long>(nullable: true),
                    IdConfiguraciondestino = table.Column<long>(nullable: true),
                    IdZona = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoConfiguracion", x => x.IdConfiguracion);
                });

            migrationBuilder.CreateTable(
                name: "HoursWorked",
                columns: table => new
                {
                    IdHorastrabajadas = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: true),
                    FechaEntrada = table.Column<DateTime>(nullable: true),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    EsFeriado = table.Column<bool>(nullable: true),
                    MultiplicaHoras = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursWorked", x => x.IdHorastrabajadas);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    IdIncidencia = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    DescripcionIncidencia = table.Column<string>(nullable: true),
                    IdTipoIncidencia = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.IdIncidencia);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUALSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUALSM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    InvoiceDueDate = table.Column<DateTime>(nullable: false),
                    InvoiceTypeId = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    Caja = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NumeroDEI = table.Column<int>(nullable: false),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimiteEmision = table.Column<DateTime>(nullable: false),
                    CAI = table.Column<string>(nullable: true),
                    NoOCExenta = table.Column<string>(nullable: true),
                    NoConstanciadeRegistro = table.Column<string>(nullable: true),
                    NoSAG = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "Kardex",
                columns: table => new
                {
                    KardexId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    KardexDate = table.Column<DateTime>(nullable: false),
                    DocType = table.Column<long>(nullable: false),
                    DocName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: false),
                    TypeOperationName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    DocumentName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kardex", x => x.KardexId);
                });

            migrationBuilder.CreateTable(
                name: "LAST_DAY_UPDATEDM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LAST_DAY_UPDATEDM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LIST_TYPEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LIST_TYPEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NATIONALITYM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NATIONALITYM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeracionSAR",
                columns: table => new
                {
                    IdNumeracion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCAI = table.Column<long>(nullable: false),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimite = table.Column<DateTime>(nullable: false),
                    CantidadOtorgada = table.Column<int>(nullable: false),
                    SiguienteNumero = table.Column<string>(nullable: true),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    PuntoEmision = table.Column<string>(nullable: true),
                    DocTypeId = table.Column<long>(nullable: false),
                    DocType = table.Column<string>(nullable: true),
                    DocSubTypeId = table.Column<long>(nullable: false),
                    DocSubType = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeracionSAR", x => x.IdNumeracion);
                });

            migrationBuilder.CreateTable(
                name: "OrdenFormula",
                columns: table => new
                {
                    Idordenformula = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Idformula = table.Column<long>(nullable: true),
                    Orden = table.Column<int>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenFormula", x => x.Idordenformula);
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    IdPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombrePlanilla = table.Column<string>(nullable: true),
                    DescripcionPlanilla = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.IdPlanilla);
                });

            migrationBuilder.CreateTable(
                name: "PayrollEmployee",
                columns: table => new
                {
                    IdPlanillaempleado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPlanilla = table.Column<long>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollEmployee", x => x.IdPlanillaempleado);
                });

            migrationBuilder.CreateTable(
                name: "PEPS",
                columns: table => new
                {
                    PEPSId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    Funcionario = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    StateId = table.Column<long>(nullable: false),
                    StateName = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    CityName = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    Official = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEPS", x => x.PEPSId);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyClaims",
                columns: table => new
                {
                    idroleclaim = table.Column<int>(nullable: false),
                    IdPolicy = table.Column<Guid>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyClaims", x => new { x.idroleclaim, x.IdPolicy });
                    table.UniqueConstraint("AK_PolicyClaims_IdPolicy_idroleclaim", x => new { x.IdPolicy, x.idroleclaim });
                });

            migrationBuilder.CreateTable(
                name: "PolicyRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdPolicy = table.Column<Guid>(nullable: false),
                    IdRol = table.Column<Guid>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductImageUrl = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    DefaultBuyingPrice = table.Column<double>(nullable: false),
                    DefaultSellingPrice = table.Column<double>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoice",
                columns: table => new
                {
                    ProformaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProformaName = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAreaId = table.Column<long>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoice", x => x.ProformaId);
                });

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    IdPuesto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombrePuesto = table.Column<string>(nullable: true),
                    IdDepartamento = table.Column<long>(nullable: true),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puesto", x => x.IdPuesto);
                });

            migrationBuilder.CreateTable(
                name: "PuntoEmision",
                columns: table => new
                {
                    IdPuntoEmision = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PuntoEmisionCod = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuntoEmision", x => x.IdPuntoEmision);
                });

            migrationBuilder.CreateTable(
                name: "RecibosCertificado",
                columns: table => new
                {
                    IdReciboCertificado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdRecibo = table.Column<long>(nullable: false),
                    IdCD = table.Column<long>(nullable: false),
                    UnitMeasureId = table.Column<long>(nullable: false),
                    productorecibolempiras = table.Column<double>(nullable: false),
                    productounidad = table.Column<double>(nullable: false),
                    productocantidadbultos = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecibosCertificado", x => x.IdReciboCertificado);
                });

            migrationBuilder.CreateTable(
                name: "Reconciliacion",
                columns: table => new
                {
                    IdReconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescripcionReconciliacion = table.Column<string>(nullable: true),
                    FechaReconciliacion = table.Column<DateTime>(nullable: true),
                    IdCalculoplanilla = table.Column<long>(nullable: true),
                    FechaAplicacion = table.Column<DateTime>(nullable: true),
                    TotalReconciliacion = table.Column<decimal>(nullable: true),
                    SaldoEmpleado = table.Column<decimal>(nullable: true),
                    FechaFinReconciliacion = table.Column<DateTime>(nullable: true),
                    Tasacambio = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reconciliacion", x => x.IdReconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionDetalle",
                columns: table => new
                {
                    IdDetallereconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdReconciliacion = table.Column<long>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Dia = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    Horasextras = table.Column<decimal>(nullable: true),
                    Bonos = table.Column<decimal>(nullable: true),
                    OtrosIngresos = table.Column<decimal>(nullable: true),
                    SalarioRecibido = table.Column<decimal>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Deducciones = table.Column<decimal>(nullable: true),
                    CatorceSalarioProporcional = table.Column<decimal>(nullable: true),
                    TrecesalarioProporcional = table.Column<decimal>(nullable: true),
                    Quincesalarioproporcional = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionDetalle", x => x.IdDetallereconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionEscala",
                columns: table => new
                {
                    IdEscalareconciliacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdEscala = table.Column<long>(nullable: true),
                    DescripcionEscala = table.Column<string>(nullable: true),
                    IdConcepto = table.Column<long>(nullable: true),
                    NombreConcepto = table.Column<string>(nullable: true),
                    MontoEscala = table.Column<decimal>(nullable: true),
                    IdReconciliacion = table.Column<long>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Montoretenido = table.Column<decimal>(nullable: true),
                    DiferenciaPorretener = table.Column<decimal>(nullable: true),
                    MesesRestantes = table.Column<int>(nullable: true),
                    MesesEjecutados = table.Column<int>(nullable: true),
                    Montotrecesalario = table.Column<decimal>(nullable: true),
                    Montocatorcesalario = table.Column<decimal>(nullable: true),
                    Montoquincesalario = table.Column<decimal>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionEscala", x => x.IdEscalareconciliacion);
                });

            migrationBuilder.CreateTable(
                name: "ReconciliacionGasto",
                columns: table => new
                {
                    Idreconciliaciongasto = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Idreconciliacion = table.Column<long>(nullable: true),
                    Descripciongasto = table.Column<string>(nullable: true),
                    Montogasto = table.Column<decimal>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    IdPlanilla = table.Column<long>(nullable: true),
                    Fechaaplicacion = table.Column<DateTime>(nullable: true),
                    Fechacreacion = table.Column<DateTime>(nullable: true),
                    Fechamodificacion = table.Column<DateTime>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconciliacionGasto", x => x.Idreconciliaciongasto);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    SalesOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrderName = table.Column<string>(nullable: true),
                    TypeContractId = table.Column<long>(nullable: false),
                    NameContract = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<double>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    Freight = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.SalesOrderId);
                });

            migrationBuilder.CreateTable(
                name: "SalesType",
                columns: table => new
                {
                    SalesTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesType", x => x.SalesTypeId);
                });

            migrationBuilder.CreateTable(
                name: "sdnListPublshInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Publish_Date = table.Column<string>(nullable: true),
                    Record_Count = table.Column<int>(nullable: false),
                    Record_CountSpecified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListPublshInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryVesselInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    callSign = table.Column<string>(nullable: true),
                    vesselType = table.Column<string>(nullable: true),
                    vesselFlag = table.Column<string>(nullable: true),
                    vesselOwner = table.Column<string>(nullable: true),
                    tonnage = table.Column<int>(nullable: false),
                    tonnageSpecified = table.Column<bool>(nullable: false),
                    grossRegisteredTonnage = table.Column<int>(nullable: false),
                    grossRegisteredTonnageSpecified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryVesselInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShipmentName = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<int>(nullable: false),
                    ShipmentDate = table.Column<DateTime>(nullable: false),
                    ShipmentTypeId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    IsFullShipment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.ShipmentId);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentType",
                columns: table => new
                {
                    ShipmentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShipmentTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentType", x => x.ShipmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCertificadoDeposito",
                columns: table => new
                {
                    IdSCD = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoCD = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    ServicioId = table.Column<long>(nullable: false),
                    ServicioName = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCertificado = table.Column<DateTime>(nullable: false),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    EmpresaSeguro = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    SujetasAPago = table.Column<double>(nullable: false),
                    FechaVencimientoDeposito = table.Column<DateTime>(nullable: false),
                    NoTraslado = table.Column<long>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Almacenaje = table.Column<string>(nullable: true),
                    Seguro = table.Column<string>(nullable: true),
                    OtrosCargos = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    MontoGarantia = table.Column<double>(nullable: false),
                    FechaPagoBanco = table.Column<DateTime>(nullable: false),
                    PorcentajeInteresesInsolutos = table.Column<double>(nullable: false),
                    FechaInicioComputo = table.Column<DateTime>(nullable: false),
                    LugarFirma = table.Column<string>(nullable: true),
                    FechaFirma = table.Column<DateTime>(nullable: false),
                    NombrePrestatario = table.Column<string>(nullable: true),
                    Quantitysum = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoDeposito", x => x.IdSCD);
                });

            migrationBuilder.CreateTable(
                name: "SubProduct",
                columns: table => new
                {
                    SubproductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(nullable: true),
                    ProductTypeId = table.Column<long>(nullable: false),
                    ProductTypeName = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    BagBalance = table.Column<long>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<int>(nullable: true),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Merma = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProduct", x => x.SubproductId);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaxCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    TaxPercentage = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "TipoContrato",
                columns: table => new
                {
                    IdTipoContrato = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreTipoContrato = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContrato", x => x.IdTipoContrato);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreTipoDocumento = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "TITLEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VALUE = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TITLEM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    UnitOfMeasureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitOfMeasureName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.UnitOfMeasureId);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorName = table.Column<string>(nullable: false),
                    VendorTypeId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "VendorType",
                columns: table => new
                {
                    VendorTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorType", x => x.VendorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WarehouseName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    PolicyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boleto_Sal",
                columns: table => new
                {
                    clave_e = table.Column<long>(nullable: false),
                    clave_o = table.Column<string>(nullable: true),
                    fecha_s = table.Column<DateTime>(nullable: false),
                    hora_s = table.Column<string>(nullable: true),
                    peso_s = table.Column<double>(nullable: false),
                    peso_n = table.Column<double>(nullable: false),
                    observa_s = table.Column<string>(nullable: true),
                    turno_s = table.Column<string>(nullable: true),
                    bascula_s = table.Column<string>(nullable: true),
                    s_manual = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto_Sal", x => x.clave_e);
                    table.ForeignKey(
                        name: "FK_Boleto_Sal_Boleto_Ent_clave_e",
                        column: x => x.clave_e,
                        principalTable: "Boleto_Ent",
                        principalColumn: "clave_e",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoLine",
                columns: table => new
                {
                    CertificadoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCD = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitMeasureId = table.Column<long>(nullable: false),
                    UnitMeasurName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TotalCantidad = table.Column<double>(nullable: false),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoLine", x => x.CertificadoLineId);
                    table.ForeignKey(
                        name: "FK_CertificadoLine_CertificadoDeposito_IdCD",
                        column: x => x.IdCD,
                        principalTable: "CertificadoDeposito",
                        principalColumn: "IdCD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    IsContraAccount = table.Column<bool>(nullable: false),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    AccountClassid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_AccountClass_AccountClassid",
                        column: x => x.AccountClassid,
                        principalTable: "AccountClass",
                        principalColumn: "AccountClassid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_Account_ParentAccountAccountId",
                        column: x => x.ParentAccountAccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlPalletsLine",
                columns: table => new
                {
                    ControlPalletsLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Alto = table.Column<int>(nullable: false),
                    Ancho = table.Column<int>(nullable: false),
                    Otros = table.Column<int>(nullable: false),
                    Totallinea = table.Column<double>(nullable: false),
                    cantidadYute = table.Column<int>(nullable: false),
                    cantidadPoliEtileno = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPalletsLine", x => x.ControlPalletsLineId);
                    table.ForeignKey(
                        name: "FK_ControlPalletsLine_ControlPallets_ControlPalletsId",
                        column: x => x.ControlPalletsId,
                        principalTable: "ControlPallets",
                        principalColumn: "ControlPalletsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    CountryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomersOfCustomer",
                columns: table => new
                {
                    CustomerOfId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: false),
                    RTN = table.Column<string>(nullable: false),
                    CustomerTypeId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersOfCustomer", x => x.CustomerOfId);
                    table.ForeignKey(
                        name: "FK_CustomersOfCustomer_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorOfCustomer",
                columns: table => new
                {
                    VendorOfId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorId = table.Column<long>(nullable: false),
                    VendorName = table.Column<string>(nullable: false),
                    VendorTypeId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorOfCustomer", x => x.VendorOfId);
                    table.ForeignKey(
                        name: "FK_VendorOfCustomer_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAreaProduct",
                columns: table => new
                {
                    CustomerAreaProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAreaId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaMoficacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAreaProduct", x => x.CustomerAreaProductId);
                    table.ForeignKey(
                        name: "FK_CustomerAreaProduct_CustomerArea_CustomerAreaId",
                        column: x => x.CustomerAreaId,
                        principalTable: "CustomerArea",
                        principalColumn: "CustomerAreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndososBonoLine",
                columns: table => new
                {
                    EndososBonoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososBonoId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososBonoLine", x => x.EndososBonoLineId);
                    table.ForeignKey(
                        name: "FK_EndososBonoLine_EndososBono_EndososBonoId",
                        column: x => x.EndososBonoId,
                        principalTable: "EndososBono",
                        principalColumn: "EndososBonoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndososCertificadosLine",
                columns: table => new
                {
                    EndososCertificadosLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososCertificadosId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososCertificadosLine", x => x.EndososCertificadosLineId);
                    table.ForeignKey(
                        name: "FK_EndososCertificadosLine_EndososCertificados_EndososCertificadosId",
                        column: x => x.EndososCertificadosId,
                        principalTable: "EndososCertificados",
                        principalColumn: "EndososCertificadosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndososTalonLine",
                columns: table => new
                {
                    EndososTalonLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndososTalonId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    ValorEndoso = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososTalonLine", x => x.EndososTalonLineId);
                    table.ForeignKey(
                        name: "FK_EndososTalonLine_EndososTalon_EndososTalonId",
                        column: x => x.EndososTalonId,
                        principalTable: "EndososTalon",
                        principalColumn: "EndososTalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveredLine",
                columns: table => new
                {
                    GoodsDeliveredLinedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveredId = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    NoAR = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveredLine", x => x.GoodsDeliveredLinedId);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveredLine_GoodsDelivered_GoodsDeliveredId",
                        column: x => x.GoodsDeliveredId,
                        principalTable: "GoodsDelivered",
                        principalColumn: "GoodsDeliveredId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsDeliveryAuthorizationLine",
                columns: table => new
                {
                    GoodsDeliveryAuthorizationLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsDeliveryAuthorizationId = table.Column<long>(nullable: false),
                    NoCertificadoDeposito = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<long>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    valorcertificado = table.Column<double>(nullable: false),
                    valorfinanciado = table.Column<double>(nullable: false),
                    ValorImpuestos = table.Column<double>(nullable: false),
                    SaldoProducto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthorizationLine", x => x.GoodsDeliveryAuthorizationLineId);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorization_GoodsDeliveryAuthorizationId",
                        column: x => x.GoodsDeliveryAuthorizationId,
                        principalTable: "GoodsDeliveryAuthorization",
                        principalColumn: "GoodsDeliveryAuthorizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceivedLine",
                columns: table => new
                {
                    GoodsReceiveLinedId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoodsReceivedId = table.Column<long>(nullable: false),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ControlPalletsId = table.Column<long>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceivedLine", x => x.GoodsReceiveLinedId);
                    table.ForeignKey(
                        name: "FK_GoodsReceivedLine_GoodsReceived_GoodsReceivedId",
                        column: x => x.GoodsReceivedId,
                        principalTable: "GoodsReceived",
                        principalColumn: "GoodsReceivedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementoConfiguracion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Idconfiguracion = table.Column<long>(nullable: true),
                    Valordecimal = table.Column<double>(nullable: true),
                    Valorstring = table.Column<string>(nullable: true),
                    Valorstring2 = table.Column<string>(nullable: true),
                    Formula = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementoConfiguracion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementoConfiguracion_GrupoConfiguracion_Idconfiguracion",
                        column: x => x.Idconfiguracion,
                        principalTable: "GrupoConfiguracion",
                        principalColumn: "IdConfiguracion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoursWorkedDetail",
                columns: table => new
                {
                    IdDetallehorastrabajadas = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdHorasTrabajadas = table.Column<long>(nullable: true),
                    Horaentrada = table.Column<DateTime>(nullable: true),
                    Horasalida = table.Column<DateTime>(nullable: true),
                    Multiplicahoras = table.Column<decimal>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoursWorkedDetail", x => x.IdDetallehorastrabajadas);
                    table.ForeignKey(
                        name: "FK_HoursWorkedDetail_HoursWorked_IdHorasTrabajadas",
                        column: x => x.IdHorasTrabajadas,
                        principalTable: "HoursWorked",
                        principalColumn: "IdHorastrabajadas",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    InvoiceLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KardexLine",
                columns: table => new
                {
                    KardexLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KardexId = table.Column<long>(nullable: false),
                    KardexDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    GoodsReceivedId = table.Column<long>(nullable: false),
                    ControlEstibaId = table.Column<long>(nullable: false),
                    ControlEstibaName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProducId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    QuantityEntry = table.Column<double>(nullable: false),
                    QuantityOut = table.Column<double>(nullable: false),
                    QuantityEntryBags = table.Column<double>(nullable: false),
                    QuantityOutBags = table.Column<double>(nullable: false),
                    QuantityEntryCD = table.Column<double>(nullable: false),
                    QuantityOutCD = table.Column<double>(nullable: false),
                    TotalCD = table.Column<double>(nullable: false),
                    TotalBags = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: false),
                    TypeOperationName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KardexLine", x => x.KardexLineId);
                    table.ForeignKey(
                        name: "FK_KardexLine_Kardex_KardexId",
                        column: x => x.KardexId,
                        principalTable: "Kardex",
                        principalColumn: "KardexId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ENTITYM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DATAID = table.Column<string>(nullable: true),
                    VERSIONNUM = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    UN_LIST_TYPE = table.Column<string>(nullable: true),
                    REFERENCE_NUMBER = table.Column<string>(nullable: true),
                    LISTED_ON = table.Column<DateTime>(nullable: false),
                    SUBMITTED_ON = table.Column<DateTime>(nullable: false),
                    SUBMITTED_ONSpecified = table.Column<bool>(nullable: false),
                    NAME_ORIGINAL_SCRIPT = table.Column<string>(nullable: true),
                    COMMENTS1 = table.Column<string>(nullable: true),
                    LIST_TYPEId = table.Column<int>(nullable: true),
                    LAST_DAY_UPDATED = table.Column<string>(nullable: true),
                    SORT_KEY = table.Column<string>(nullable: true),
                    SORT_KEY_LAST_MOD = table.Column<string>(nullable: true),
                    DELISTED_ON = table.Column<DateTime>(nullable: false),
                    DELISTED_ONSpecified = table.Column<bool>(nullable: false),
                    CONSOLIDATED_LISTMId = table.Column<int>(nullable: true),
                    ENTITIESMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITYM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITYM_CONSOLIDATED_LISTM_CONSOLIDATED_LISTMId",
                        column: x => x.CONSOLIDATED_LISTMId,
                        principalTable: "CONSOLIDATED_LISTM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ENTITYM_ENTITIESM_ENTITIESMId",
                        column: x => x.ENTITIESMId,
                        principalTable: "ENTITIESM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ENTITYM_LIST_TYPEM_LIST_TYPEId",
                        column: x => x.LIST_TYPEId,
                        principalTable: "LIST_TYPEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUALM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DATAID = table.Column<string>(nullable: true),
                    VERSIONNUM = table.Column<string>(nullable: true),
                    FIRST_NAME = table.Column<string>(nullable: true),
                    SECOND_NAME = table.Column<string>(nullable: true),
                    THIRD_NAME = table.Column<string>(nullable: true),
                    FOURTH_NAME = table.Column<string>(nullable: true),
                    UN_LIST_TYPE = table.Column<string>(nullable: true),
                    REFERENCE_NUMBER = table.Column<string>(nullable: true),
                    LISTED_ON = table.Column<DateTime>(nullable: false),
                    GENDER = table.Column<string>(nullable: true),
                    SUBMITTED_BY = table.Column<string>(nullable: true),
                    NAME_ORIGINAL_SCRIPT = table.Column<string>(nullable: true),
                    COMMENTS1 = table.Column<string>(nullable: true),
                    NATIONALITY2 = table.Column<string>(nullable: true),
                    TITLE = table.Column<string>(nullable: true),
                    DESIGNATION = table.Column<string>(nullable: true),
                    NATIONALITY = table.Column<string>(nullable: true),
                    LIST_TYPEId = table.Column<int>(nullable: true),
                    LAST_DAY_UPDATED = table.Column<string>(nullable: true),
                    SORT_KEY = table.Column<string>(nullable: true),
                    SORT_KEY_LAST_MOD = table.Column<string>(nullable: true),
                    DELISTED_ON = table.Column<DateTime>(nullable: false),
                    DELISTED_ONSpecified = table.Column<bool>(nullable: false),
                    CONSOLIDATED_LISTMId = table.Column<int>(nullable: true),
                    INDIVIDUALSMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUALM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_CONSOLIDATED_LISTM_CONSOLIDATED_LISTMId",
                        column: x => x.CONSOLIDATED_LISTMId,
                        principalTable: "CONSOLIDATED_LISTM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_INDIVIDUALSM_INDIVIDUALSMId",
                        column: x => x.INDIVIDUALSMId,
                        principalTable: "INDIVIDUALSM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_INDIVIDUALM_LIST_TYPEM_LIST_TYPEId",
                        column: x => x.LIST_TYPEId,
                        principalTable: "LIST_TYPEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoiceLine",
                columns: table => new
                {
                    ProformaLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProformaInvoiceId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoiceLine", x => x.ProformaLineId);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceLine_ProformaInvoice_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoice",
                        principalColumn: "ProformaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLine",
                columns: table => new
                {
                    SalesOrderLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesOrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DiscountPercentage = table.Column<double>(nullable: false),
                    DiscountAmount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    TaxPercentage = table.Column<double>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLine", x => x.SalesOrderLineId);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListPublshInformationId = table.Column<int>(nullable: false),
                    publshInformationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnList_sdnListPublshInformation_publshInformationId",
                        column: x => x.publshInformationId,
                        principalTable: "sdnListPublshInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCertificadoLine",
                columns: table => new
                {
                    CertificadoLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdSCD = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitMeasureId = table.Column<long>(nullable: false),
                    UnitMeasurName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    TotalCantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoLine", x => x.CertificadoLineId);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
                        column: x => x.IdSCD,
                        principalTable: "SolicitudCertificadoDeposito",
                        principalColumn: "IdSCD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelation",
                columns: table => new
                {
                    RelationProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelation", x => x.RelationProductId);
                    table.ForeignKey(
                        name: "FK_ProductRelation_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRelation_SubProduct_SubProductId",
                        column: x => x.SubProductId,
                        principalTable: "SubProduct",
                        principalColumn: "SubproductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerLine",
                columns: table => new
                {
                    GeneralLedgerHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    DrCr = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AccountId1 = table.Column<long>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerLine", x => x.GeneralLedgerHeaderId);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLine_Account_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneralLedgerLine_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    State_Id = table.Column<long>(nullable: true),
                    StateId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTITY_ADDRESSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    STREET = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    ZIP_CODE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    ENTITYMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITY_ADDRESSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITY_ADDRESSM_ENTITYM_ENTITYMId",
                        column: x => x.ENTITYMId,
                        principalTable: "ENTITYM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ENTITY_ALIASM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QUALITY = table.Column<string>(nullable: true),
                    ALIAS_NAME = table.Column<string>(nullable: true),
                    ENTITYMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTITY_ALIASM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENTITY_ALIASM_ENTITYM_ENTITYMId",
                        column: x => x.ENTITYMId,
                        principalTable: "ENTITYM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_ADDRESSM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    STREET = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    ZIP_CODE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_ADDRESSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_ADDRESSM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_ALIASM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QUALITY = table.Column<string>(nullable: true),
                    ALIAS_NAME = table.Column<string>(nullable: true),
                    DATE_OF_BIRTH = table.Column<string>(nullable: true),
                    CITY_OF_BIRTH = table.Column<string>(nullable: true),
                    COUNTRY_OF_BIRTH = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_ALIASM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_ALIASM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_DATE_OF_BIRTHM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TYPE_OF_DATE = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    Items = table.Column<string>(nullable: true),
                    ItemsElementName = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_DATE_OF_BIRTHM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_DATE_OF_BIRTHM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_DOCUMENTM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TYPE_OF_DOCUMENT = table.Column<string>(nullable: true),
                    TYPE_OF_DOCUMENT2 = table.Column<string>(nullable: true),
                    NUMBER = table.Column<string>(nullable: true),
                    ISSUING_COUNTRY = table.Column<string>(nullable: true),
                    DATE_OF_ISSUE = table.Column<string>(nullable: true),
                    CITY_OF_ISSUE = table.Column<string>(nullable: true),
                    COUNTRY_OF_ISSUE = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_DOCUMENTM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_DOCUMENTM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INDIVIDUAL_PLACE_OF_BIRTHM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CITY = table.Column<string>(nullable: true),
                    STATE_PROVINCE = table.Column<string>(nullable: true),
                    COUNTRY = table.Column<string>(nullable: true),
                    NOTE = table.Column<string>(nullable: true),
                    INDIVIDUALMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDIVIDUAL_PLACE_OF_BIRTHM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDIVIDUAL_PLACE_OF_BIRTHM_INDIVIDUALM_INDIVIDUALMId",
                        column: x => x.INDIVIDUALMId,
                        principalTable: "INDIVIDUALM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    uid = table.Column<int>(nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    sdnType = table.Column<string>(nullable: true),
                    remarks = table.Column<string>(nullable: true),
                    programList = table.Column<string>(nullable: true),
                    vesselInfoId = table.Column<int>(nullable: true),
                    sdnListMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnList_sdnListMId",
                        column: x => x.sdnListMId,
                        principalTable: "sdnList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntry_sdnListSdnEntryVesselInfo_vesselInfoId",
                        column: x => x.vesselInfoId,
                        principalTable: "sdnListSdnEntryVesselInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    address1 = table.Column<string>(nullable: true),
                    address2 = table.Column<string>(nullable: true),
                    address3 = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    stateOrProvince = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAddress_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryAka",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryAka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryAka_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryCitizenship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    country = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryCitizenship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryCitizenship_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryDateOfBirthItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    dateOfBirth = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryDateOfBirthItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryID",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    idType = table.Column<string>(nullable: true),
                    idNumber = table.Column<string>(nullable: true),
                    idCountry = table.Column<string>(nullable: true),
                    issueDate = table.Column<string>(nullable: true),
                    expirationDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryID_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryNationality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    country = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryNationality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryNationality_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sdnListSdnEntryPlaceOfBirthItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sdnListSdnEntryMId = table.Column<int>(nullable: false),
                    uid = table.Column<int>(nullable: false),
                    placeOfBirth = table.Column<string>(nullable: true),
                    mainEntry = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sdnListSdnEntryPlaceOfBirthItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntry_sdnListSdnEntryMId",
                        column: x => x.sdnListSdnEntryMId,
                        principalTable: "sdnListSdnEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountClassid",
                table: "Account",
                column: "AccountClassid");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CompanyInfoId",
                table: "Account",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentAccountAccountId",
                table: "Account",
                column: "ParentAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoLine_IdCD",
                table: "CertificadoLine",
                column: "IdCD");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_ControlPalletsId",
                table: "ControlPalletsLine",
                column: "ControlPalletsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RTN",
                table: "Customer",
                column: "RTN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAreaProduct_CustomerAreaId",
                table: "CustomerAreaProduct",
                column: "CustomerAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersOfCustomer_CustomerId",
                table: "CustomersOfCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion",
                column: "Idconfiguracion");

            migrationBuilder.CreateIndex(
                name: "IX_EndososBonoLine_EndososBonoId",
                table: "EndososBonoLine",
                column: "EndososBonoId");

            migrationBuilder.CreateIndex(
                name: "IX_EndososCertificadosLine_EndososCertificadosId",
                table: "EndososCertificadosLine",
                column: "EndososCertificadosId");

            migrationBuilder.CreateIndex(
                name: "IX_EndososTalonLine_EndososTalonId",
                table: "EndososTalonLine",
                column: "EndososTalonId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITY_ADDRESSM_ENTITYMId",
                table: "ENTITY_ADDRESSM",
                column: "ENTITYMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITY_ALIASM_ENTITYMId",
                table: "ENTITY_ALIASM",
                column: "ENTITYMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_CONSOLIDATED_LISTMId",
                table: "ENTITYM",
                column: "CONSOLIDATED_LISTMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_ENTITIESMId",
                table: "ENTITYM",
                column: "ENTITIESMId");

            migrationBuilder.CreateIndex(
                name: "IX_ENTITYM_LIST_TYPEId",
                table: "ENTITYM",
                column: "LIST_TYPEId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_AccountId1",
                table: "GeneralLedgerLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedgerLine_GeneralLedgerHeaderId1",
                table: "GeneralLedgerLine",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveredLine_GoodsDeliveredId",
                table: "GoodsDeliveredLine",
                column: "GoodsDeliveredId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorizationId",
                table: "GoodsDeliveryAuthorizationLine",
                column: "GoodsDeliveryAuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedLine_GoodsReceivedId",
                table: "GoodsReceivedLine",
                column: "GoodsReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_HoursWorkedDetail_IdHorasTrabajadas",
                table: "HoursWorkedDetail",
                column: "IdHorasTrabajadas");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ADDRESSM_INDIVIDUALMId",
                table: "INDIVIDUAL_ADDRESSM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_ALIASM_INDIVIDUALMId",
                table: "INDIVIDUAL_ALIASM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_DATE_OF_BIRTHM_INDIVIDUALMId",
                table: "INDIVIDUAL_DATE_OF_BIRTHM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_DOCUMENTM_INDIVIDUALMId",
                table: "INDIVIDUAL_DOCUMENTM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUAL_PLACE_OF_BIRTHM_INDIVIDUALMId",
                table: "INDIVIDUAL_PLACE_OF_BIRTHM",
                column: "INDIVIDUALMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_CONSOLIDATED_LISTMId",
                table: "INDIVIDUALM",
                column: "CONSOLIDATED_LISTMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_INDIVIDUALSMId",
                table: "INDIVIDUALM",
                column: "INDIVIDUALSMId");

            migrationBuilder.CreateIndex(
                name: "IX_INDIVIDUALM_LIST_TYPEId",
                table: "INDIVIDUALM",
                column: "LIST_TYPEId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_KardexLine_KardexId",
                table: "KardexLine",
                column: "KardexId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCode",
                table: "Product",
                column: "ProductCode",
                unique: true,
                filter: "[ProductCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_ProductId",
                table: "ProductRelation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelation_SubProductId",
                table: "ProductRelation",
                column: "SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoiceLine_ProformaInvoiceId",
                table: "ProformaInvoiceLine",
                column: "ProformaInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_InvoiceId",
                table: "SalesOrderLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_SalesOrderId",
                table: "SalesOrderLine",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnList_publshInformationId",
                table: "sdnList",
                column: "publshInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_sdnListMId",
                table: "sdnListSdnEntry",
                column: "sdnListMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntry_vesselInfoId",
                table: "sdnListSdnEntry",
                column: "vesselInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAddress_sdnListSdnEntryMId",
                table: "sdnListSdnEntryAddress",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryAka_sdnListSdnEntryMId",
                table: "sdnListSdnEntryAka",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryCitizenship_sdnListSdnEntryMId",
                table: "sdnListSdnEntryCitizenship",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryDateOfBirthItem_sdnListSdnEntryMId",
                table: "sdnListSdnEntryDateOfBirthItem",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryID_sdnListSdnEntryMId",
                table: "sdnListSdnEntryID",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryNationality_sdnListSdnEntryMId",
                table: "sdnListSdnEntryNationality",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_sdnListSdnEntryPlaceOfBirthItem_sdnListSdnEntryMId",
                table: "sdnListSdnEntryPlaceOfBirthItem",
                column: "sdnListSdnEntryMId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCertificadoLine_IdSCD",
                table: "SolicitudCertificadoLine",
                column: "IdSCD");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProduct_ProductCode",
                table: "SubProduct",
                column: "ProductCode",
                unique: true,
                filter: "[ProductCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VendorOfCustomer_CustomerId",
                table: "VendorOfCustomer",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alert");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "BlackListCustomers");

            migrationBuilder.DropTable(
                name: "BoletaDeSalida");

            migrationBuilder.DropTable(
                name: "Boleto_Sal");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "CAI");

            migrationBuilder.DropTable(
                name: "CalculoPlanilla");

            migrationBuilder.DropTable(
                name: "CalculoPlanillaDetalle");

            migrationBuilder.DropTable(
                name: "CertificadoLine");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "ControlPalletsLine");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "CustomerAreaProduct");

            migrationBuilder.DropTable(
                name: "CustomerAuthorizedSignature");

            migrationBuilder.DropTable(
                name: "CustomerConditions");

            migrationBuilder.DropTable(
                name: "CustomerContract");

            migrationBuilder.DropTable(
                name: "CustomerContractWareHouse");

            migrationBuilder.DropTable(
                name: "CustomerDocument");

            migrationBuilder.DropTable(
                name: "CustomerPartners");

            migrationBuilder.DropTable(
                name: "CustomerPhones");

            migrationBuilder.DropTable(
                name: "CustomerProduct");

            migrationBuilder.DropTable(
                name: "CustomersOfCustomer");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Dependientes");

            migrationBuilder.DropTable(
                name: "DESIGNATIONM");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "ElementoConfiguracion");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "EndososBonoLine");

            migrationBuilder.DropTable(
                name: "EndososCertificadosLine");

            migrationBuilder.DropTable(
                name: "EndososLiberacion");

            migrationBuilder.DropTable(
                name: "EndososTalonLine");

            migrationBuilder.DropTable(
                name: "ENTITY_ADDRESSM");

            migrationBuilder.DropTable(
                name: "ENTITY_ALIASM");

            migrationBuilder.DropTable(
                name: "Escala");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Formula");

            migrationBuilder.DropTable(
                name: "FormulasAplicadas");

            migrationBuilder.DropTable(
                name: "FormulasConcepto");

            migrationBuilder.DropTable(
                name: "FormulasConFormulas");

            migrationBuilder.DropTable(
                name: "GeneralLedgerLine");

            migrationBuilder.DropTable(
                name: "GoodsDeliveredLine");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropTable(
                name: "GoodsReceivedLine");

            migrationBuilder.DropTable(
                name: "HoursWorkedDetail");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_ADDRESSM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_ALIASM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_DATE_OF_BIRTHM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_DOCUMENTM");

            migrationBuilder.DropTable(
                name: "INDIVIDUAL_PLACE_OF_BIRTHM");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "KardexLine");

            migrationBuilder.DropTable(
                name: "LAST_DAY_UPDATEDM");

            migrationBuilder.DropTable(
                name: "NATIONALITYM");

            migrationBuilder.DropTable(
                name: "NumeracionSAR");

            migrationBuilder.DropTable(
                name: "OrdenFormula");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "PayrollEmployee");

            migrationBuilder.DropTable(
                name: "PEPS");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "PolicyClaims");

            migrationBuilder.DropTable(
                name: "PolicyRoles");

            migrationBuilder.DropTable(
                name: "ProductRelation");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ProformaInvoiceLine");

            migrationBuilder.DropTable(
                name: "Puesto");

            migrationBuilder.DropTable(
                name: "PuntoEmision");

            migrationBuilder.DropTable(
                name: "RecibosCertificado");

            migrationBuilder.DropTable(
                name: "Reconciliacion");

            migrationBuilder.DropTable(
                name: "ReconciliacionDetalle");

            migrationBuilder.DropTable(
                name: "ReconciliacionEscala");

            migrationBuilder.DropTable(
                name: "ReconciliacionGasto");

            migrationBuilder.DropTable(
                name: "SalesOrderLine");

            migrationBuilder.DropTable(
                name: "SalesType");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAddress");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryAka");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryCitizenship");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryDateOfBirthItem");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryID");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryNationality");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryPlaceOfBirthItem");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "ShipmentType");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoLine");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "TipoContrato");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TiposDocumento");

            migrationBuilder.DropTable(
                name: "TITLEM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "VendorOfCustomer");

            migrationBuilder.DropTable(
                name: "VendorType");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Boleto_Ent");

            migrationBuilder.DropTable(
                name: "CertificadoDeposito");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "ControlPallets");

            migrationBuilder.DropTable(
                name: "CustomerArea");

            migrationBuilder.DropTable(
                name: "GrupoConfiguracion");

            migrationBuilder.DropTable(
                name: "EndososBono");

            migrationBuilder.DropTable(
                name: "EndososCertificados");

            migrationBuilder.DropTable(
                name: "EndososTalon");

            migrationBuilder.DropTable(
                name: "ENTITYM");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "GeneralLedgerHeader");

            migrationBuilder.DropTable(
                name: "GoodsDelivered");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthorization");

            migrationBuilder.DropTable(
                name: "GoodsReceived");

            migrationBuilder.DropTable(
                name: "HoursWorked");

            migrationBuilder.DropTable(
                name: "INDIVIDUALM");

            migrationBuilder.DropTable(
                name: "Kardex");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SubProduct");

            migrationBuilder.DropTable(
                name: "ProformaInvoice");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntry");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoDeposito");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "ENTITIESM");

            migrationBuilder.DropTable(
                name: "AccountClass");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "CONSOLIDATED_LISTM");

            migrationBuilder.DropTable(
                name: "INDIVIDUALSM");

            migrationBuilder.DropTable(
                name: "LIST_TYPEM");

            migrationBuilder.DropTable(
                name: "sdnList");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryVesselInfo");

            migrationBuilder.DropTable(
                name: "sdnListPublshInformation");
        }
    }
}
