using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    NivelRiesgoCliente = table.Column<string>(nullable: true),
                    ActionTakenId = table.Column<long>(nullable: false),
                    ActionTakenName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    SujetaARos = table.Column<bool>(nullable: false),
                    FalsoPositivo = table.Column<bool>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: true),
                    DescriptionAlert = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    AlertType = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    PersonName = table.Column<string>(nullable: true),
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
                    LastPasswordChangedDate = table.Column<DateTime>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    AwayBlockEnd = table.Column<DateTimeOffset>(nullable: true)
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
                    DocType = table.Column<string>(nullable: true),
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
                    ClaseInicial = table.Column<string>(nullable: true),
                    ResultadoSerializado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BitacoraCierreContable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    EstatusId = table.Column<long>(nullable: false),
                    Estatus = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraCierreContable", x => x.Id);
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
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    StateId = table.Column<long>(nullable: false),
                    StateName = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    CityName = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
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
                    GoodsReceivedId = table.Column<long>(nullable: false),
                    VigilanteId = table.Column<long>(nullable: false),
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
                    WeightBallot = table.Column<long>(nullable: false),
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
                name: "BranchPorDepartamento",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdDepartamento = table.Column<long>(nullable: false),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPorDepartamento", x => x.Id);
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
                name: "CategoriasPlanillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasPlanillas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comision",
                columns: table => new
                {
                    ComisionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoComision = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comision", x => x.ComisionId);
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
                    RTN = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    Manager = table.Column<string>(nullable: true),
                    RTNMANAGER = table.Column<string>(nullable: true),
                    PrintHeader = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    RevOffice = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    SocialNetworks = table.Column<string>(nullable: true),
                    WebPage = table.Column<string>(nullable: true),
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
                name: "Concept",
                columns: table => new
                {
                    ConceptId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConceptName = table.Column<string>(nullable: true),
                    TypeId = table.Column<long>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    Calculation = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concept", x => x.ConceptId);
                });

            migrationBuilder.CreateTable(
                name: "Conciliacion",
                columns: table => new
                {
                    ConciliacionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: false),
                    CheckAccountId = table.Column<long>(nullable: false),
                    FechaConciliacion = table.Column<DateTime>(nullable: false),
                    DateBeginReconciled = table.Column<DateTime>(nullable: false),
                    DateEndReconciled = table.Column<DateTime>(nullable: false),
                    SaldoConciliado = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    SaldoBanco = table.Column<decimal>(nullable: false),
                    SaldoLibro = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conciliacion", x => x.ConciliacionId);
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
                name: "ConfigurationVendor",
                columns: table => new
                {
                    ConfigurationVendorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QtyMonth = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationVendor", x => x.ConfigurationVendorId);
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
                name: "ContactPerson",
                columns: table => new
                {
                    ContactPersonId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactPersonName = table.Column<string>(nullable: true),
                    VendorId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ContactPersonIdentity = table.Column<string>(nullable: true),
                    ContactPersonPhone = table.Column<string>(nullable: true),
                    ContactPersonCityId = table.Column<int>(nullable: false),
                    ContactPersonCity = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true),
                    ContactPersonIdEstado = table.Column<long>(nullable: false),
                    ContactPersonEstado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.ContactPersonId);
                });

            migrationBuilder.CreateTable(
                name: "ContextoRiesgo",
                columns: table => new
                {
                    IdContextoRiesgo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoContexto = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContextoRiesgo", x => x.IdContextoRiesgo);
                });

            migrationBuilder.CreateTable(
                name: "ControlPallets",
                columns: table => new
                {
                    ControlPalletsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Motorista = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    DescriptionProduct = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
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
                    Alto = table.Column<int>(nullable: true),
                    Ancho = table.Column<int>(nullable: true),
                    Otros = table.Column<int>(nullable: true),
                    Totallinea = table.Column<double>(nullable: true),
                    cantidadYute = table.Column<int>(nullable: true),
                    cantidadPoliEtileno = table.Column<int>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: true),
                    CenterCostName = table.Column<string>(nullable: true),
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
                name: "CostCenter",
                columns: table => new
                {
                    CostCenterId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostCenterName = table.Column<string>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.CostCenterId);
                });

            migrationBuilder.CreateTable(
                name: "CostListItem",
                columns: table => new
                {
                    CostListItemId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubproductId = table.Column<long>(nullable: false),
                    ExchangeRateId = table.Column<long>(nullable: false),
                    DayofCalcule = table.Column<DateTime>(nullable: false),
                    PriceBagValue = table.Column<double>(nullable: false),
                    DifferencyPriceBagValue = table.Column<double>(nullable: false),
                    TotalPriceBagValue = table.Column<double>(nullable: false),
                    PriceBagValueCurrency = table.Column<double>(nullable: false),
                    PorcentagePriceBagValue = table.Column<double>(nullable: false),
                    RealBagValueCurrency = table.Column<double>(nullable: false),
                    ConRealBagValueInside = table.Column<double>(nullable: false),
                    PCRealBagValueInside = table.Column<double>(nullable: false),
                    RealBagValueInside = table.Column<double>(nullable: false),
                    TotalIncomes = table.Column<double>(nullable: false),
                    RecipientExpenses = table.Column<double>(nullable: false),
                    EscrowExpenses = table.Column<double>(nullable: false),
                    UtilityExpenses = table.Column<double>(nullable: false),
                    PermiseExportExpenses = table.Column<double>(nullable: false),
                    TaxesExpenses = table.Column<double>(nullable: false),
                    TotalExpenses = table.Column<double>(nullable: false),
                    TotalExpensesCurrency = table.Column<double>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostListItem", x => x.CostListItemId);
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
                    GAFI = table.Column<bool>(nullable: false),
                    ListaId = table.Column<int>(nullable: false),
                    ListaName = table.Column<string>(nullable: true),
                    Actualizacion = table.Column<DateTime>(nullable: false),
                    NivelRiesgo = table.Column<int>(nullable: false),
                    NivelRiesgoName = table.Column<string>(nullable: true),
                    TipoAlertaId = table.Column<int>(nullable: false),
                    TipoAlertaName = table.Column<string>(nullable: true),
                    AccionId = table.Column<int>(nullable: false),
                    AccionName = table.Column<string>(nullable: true),
                    SeguimientoId = table.Column<int>(nullable: false),
                    SeguimientoName = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
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
                name: "CreditNote",
                columns: table => new
                {
                    CreditNoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditNoteName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    Fiscal = table.Column<bool>(nullable: false),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    CreditNoteDate = table.Column<DateTime>(nullable: false),
                    CreditNoteDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CreditNoteTypeId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    Caja = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    NúmeroDEI = table.Column<int>(nullable: false),
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
                    SubProductId = table.Column<long>(nullable: true),
                    SubProductName = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Discount = table.Column<decimal>(type: "Money", nullable: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: false),
                    Tax18 = table.Column<decimal>(type: "Money", nullable: false),
                    Freight = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExento = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExonerado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado18 = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_CreditNote", x => x.CreditNoteId);
                });

            migrationBuilder.CreateTable(
                name: "CuentaBancoEmpleados",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    NumeroCuenta = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: false),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancoEmpleados", x => x.Id);
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
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
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
                    SubProductName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CustomerConditionName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LogicalConditionId = table.Column<long>(nullable: false),
                    LogicalCondition = table.Column<string>(nullable: true),
                    ValueToEvaluate = table.Column<string>(nullable: true),
                    ValueDecimal = table.Column<decimal>(nullable: false),
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
                    FuncionarioPublico = table.Column<bool>(nullable: false),
                    CargoPublico = table.Column<string>(nullable: true),
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
                name: "DebitNote",
                columns: table => new
                {
                    DebitNoteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitNoteName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    Fiscal = table.Column<bool>(nullable: false),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    DebitNoteDate = table.Column<DateTime>(nullable: false),
                    DebitNoteDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    DebitNoteTypeId = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    Sucursal = table.Column<string>(nullable: true),
                    Caja = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: true),
                    SubProductName = table.Column<string>(nullable: true),
                    NúmeroDEI = table.Column<int>(nullable: false),
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
                    Currency = table.Column<decimal>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Discount = table.Column<decimal>(type: "Money", nullable: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: false),
                    Tax18 = table.Column<decimal>(type: "Money", nullable: false),
                    Freight = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExento = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExonerado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado18 = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_DebitNote", x => x.DebitNoteId);
                });

            migrationBuilder.CreateTable(
                name: "Deduction",
                columns: table => new
                {
                    DeductionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    DeductionTypeId = table.Column<long>(nullable: false),
                    DeductionType = table.Column<string>(nullable: false),
                    Formula = table.Column<double>(nullable: false),
                    Fortnight = table.Column<double>(nullable: false),
                    EsPorcentaje = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deduction", x => x.DeductionId);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    IdDepartamento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreDepartamento = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    AplicaComision = table.Column<bool>(nullable: true),
                    ComisionId = table.Column<long>(nullable: true),
                    PeridicidadId = table.Column<long>(nullable: true),
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
                    TelefonoDependientes = table.Column<string>(nullable: true),
                    DireccionDependientes = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: true),
                    Edad = table.Column<int>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
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
                name: "DepreciationFixedAsset",
                columns: table => new
                {
                    DepreciationFixedAssetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetId = table.Column<long>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    January = table.Column<decimal>(nullable: false),
                    February = table.Column<decimal>(nullable: false),
                    March = table.Column<decimal>(nullable: false),
                    April = table.Column<decimal>(nullable: false),
                    May = table.Column<decimal>(nullable: false),
                    June = table.Column<decimal>(nullable: false),
                    July = table.Column<decimal>(nullable: false),
                    August = table.Column<decimal>(nullable: false),
                    September = table.Column<decimal>(nullable: false),
                    October = table.Column<decimal>(nullable: false),
                    November = table.Column<decimal>(nullable: false),
                    December = table.Column<decimal>(nullable: false),
                    TotalDepreciated = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepreciationFixedAsset", x => x.DepreciationFixedAssetId);
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
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
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
                name: "EmployeeAbsence",
                columns: table => new
                {
                    EmployeeAbsenceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    AbsenceTypeId = table.Column<long>(nullable: false),
                    AbsenceTypeName = table.Column<string>(nullable: true),
                    DeductionIndicator = table.Column<string>(nullable: true),
                    FechaInicial = table.Column<DateTime>(nullable: false),
                    FechaFinal = table.Column<DateTime>(nullable: false),
                    QuantityDays = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAbsence", x => x.EmployeeAbsenceId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocument",
                columns: table => new
                {
                    EmployeeDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocument", x => x.EmployeeDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeExtraHoursDetail",
                columns: table => new
                {
                    EmployeeExtraHoursDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeExtraHoursId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<decimal>(nullable: false),
                    HourlySalary = table.Column<decimal>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExtraHoursDetail", x => x.EmployeeExtraHoursDetailId);
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
                    CantidadEndosar = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    ValorEndosar = table.Column<decimal>(nullable: false),
                    TipoEndosoId = table.Column<long>(nullable: false),
                    TipoEndosoName = table.Column<string>(nullable: true),
                    FirmadoEn = table.Column<string>(nullable: true),
                    TipoEndoso = table.Column<long>(nullable: false),
                    NombreEndoso = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    FechaOtorgado = table.Column<DateTime>(nullable: false),
                    TasaDeInteres = table.Column<decimal>(nullable: false),
                    TotalEndoso = table.Column<decimal>(nullable: false),
                    FechaLiberacion = table.Column<DateTime>(nullable: false),
                    FechaCancelacion = table.Column<DateTime>(nullable: false),
                    CantidadEndosada = table.Column<decimal>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false),
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
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false),
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
                name: "FixedAsset",
                columns: table => new
                {
                    FixedAssetId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetName = table.Column<string>(nullable: true),
                    FixedAssetDescription = table.Column<string>(nullable: true),
                    AssetDate = table.Column<DateTime>(nullable: false),
                    FixedAssetGroupId = table.Column<long>(nullable: false),
                    FixedAssetCode = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CenterCostId = table.Column<long>(nullable: false),
                    CenterCostName = table.Column<string>(nullable: true),
                    FixedActiveLife = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Cost = table.Column<decimal>(type: "Money", nullable: true),
                    ResidualValue = table.Column<decimal>(type: "Money", nullable: false),
                    ToDepreciate = table.Column<decimal>(type: "Money", nullable: false),
                    TotalDepreciated = table.Column<decimal>(type: "Money", nullable: false),
                    ActiveTotalCost = table.Column<decimal>(type: "Money", nullable: false),
                    ResidualValuePercent = table.Column<decimal>(nullable: false),
                    NetValue = table.Column<decimal>(type: "Money", nullable: false),
                    AccumulatedDepreciation = table.Column<decimal>(type: "Money", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAsset", x => x.FixedAssetId);
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
                name: "FundingInterestRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    IdEstado = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingInterestRate", x => x.Id);
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
                    ControlId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
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
                    PesoBruto = table.Column<decimal>(nullable: false),
                    TaraTransporte = table.Column<decimal>(nullable: false),
                    PesoNeto = table.Column<decimal>(nullable: false),
                    TaraUnidadMedida = table.Column<decimal>(nullable: false),
                    PesoNeto2 = table.Column<decimal>(nullable: false),
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
                    Comments = table.Column<string>(nullable: true),
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
                    ControlId = table.Column<long>(nullable: false),
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    VigilanteId = table.Column<long>(nullable: false),
                    VigilanteName = table.Column<string>(nullable: true),
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
                    TaraCamion = table.Column<double>(nullable: false),
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
                name: "GrupoEstado",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Modulo = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoEstado", x => x.Id);
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
                name: "ImpuestoVecinalConfiguraciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    De = table.Column<decimal>(nullable: false),
                    Hasta = table.Column<decimal>(nullable: false),
                    FactorMillar = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpuestoVecinalConfiguraciones", x => x.Id);
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
                name: "IncomeAndExpenseAccountLine",
                columns: table => new
                {
                    IncomeAndExpenseAccountLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IncomeAndExpensesAccountId = table.Column<long>(nullable: false),
                    TypeDocument = table.Column<long>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DebitCredit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAndExpenseAccountLine", x => x.IncomeAndExpenseAccountLineId);
                });

            migrationBuilder.CreateTable(
                name: "IncomeAndExpensesAccount",
                columns: table => new
                {
                    IncomeAndExpensesAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    TypeAccountId = table.Column<long>(nullable: false),
                    TypeAccountName = table.Column<string>(nullable: true),
                    AccountDescription = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    EstadoName = table.Column<string>(nullable: true),
                    UsuarioEjecucion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeAndExpensesAccount", x => x.IncomeAndExpensesAccountId);
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
                name: "InstallmentDelivery",
                columns: table => new
                {
                    InstallmentDeliveryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDelivery", x => x.InstallmentDeliveryId);
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
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    InvoiceDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    InvoiceTypeId = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<long>(nullable: false),
                    ProformaInvoiceId = table.Column<long>(nullable: false),
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
                    Currency = table.Column<decimal>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Tax18 = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExento = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExonerado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado18 = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
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
                name: "InvoiceCalculation",
                columns: table => new
                {
                    InvoiceCalculationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProformaInvoiceId = table.Column<long>(nullable: false),
                    InvoiceId = table.Column<long>(nullable: false),
                    IdCD = table.Column<long>(nullable: false),
                    NoCD = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Dias = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    ValorLps = table.Column<decimal>(nullable: false),
                    ValorFacturar = table.Column<decimal>(nullable: false),
                    IngresoMercadería = table.Column<decimal>(nullable: false),
                    MercaderiaCertificada = table.Column<decimal>(nullable: false),
                    Dias2 = table.Column<int>(nullable: false),
                    PorcentajeMerma = table.Column<decimal>(nullable: false),
                    ValorLpsMerma = table.Column<decimal>(nullable: false),
                    ValorAFacturarMerma = table.Column<decimal>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Identificador = table.Column<Guid>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCalculation", x => x.InvoiceCalculationId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTransReport",
                columns: table => new
                {
                    IdInvoiceTransReport = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTransReport", x => x.IdInvoiceTransReport);
                });

            migrationBuilder.CreateTable(
                name: "ISRConfiguracion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    De = table.Column<double>(nullable: false),
                    Hasta = table.Column<double>(nullable: false),
                    Tramo = table.Column<string>(nullable: false),
                    Porcentaje = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ISRConfiguracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryCanceled",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CanceledJournalentryId = table.Column<int>(nullable: false),
                    ReverseJournalEntryId = table.Column<int>(nullable: false),
                    TypeJournalName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    VoucherType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryCanceled", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryConfiguration",
                columns: table => new
                {
                    JournalEntryConfigurationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionId = table.Column<long>(nullable: false),
                    Transaction = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    EstadoName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryConfiguration", x => x.JournalEntryConfigurationId);
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
                    DocumentId = table.Column<long>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: true),
                    TypeOperationName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: true),
                    WareHouseName = table.Column<string>(nullable: true),
                    ControlEstibaId = table.Column<long>(nullable: true),
                    ControlEstibaName = table.Column<string>(nullable: true),
                    ProducId = table.Column<long>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    SubProducId = table.Column<long>(nullable: true),
                    SubProductName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: true),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    SaldoAnterior = table.Column<decimal>(nullable: true),
                    QuantityEntry = table.Column<decimal>(nullable: true),
                    QuantityOut = table.Column<decimal>(nullable: true),
                    QuantityEntryBags = table.Column<decimal>(nullable: true),
                    QuantityOutBags = table.Column<decimal>(nullable: true),
                    QuantityEntryCD = table.Column<decimal>(nullable: true),
                    QuantityOutCD = table.Column<decimal>(nullable: true),
                    TotalCD = table.Column<decimal>(nullable: true),
                    TotalBags = table.Column<decimal>(nullable: true),
                    Total = table.Column<decimal>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: true),
                    CostCenterName = table.Column<string>(nullable: true)
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
                name: "MantenimientoImpacto",
                columns: table => new
                {
                    MantenimientoImpactoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoImpacto = table.Column<long>(nullable: false),
                    Rango = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoImpacto", x => x.MantenimientoImpactoId);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    MeasurelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    High = table.Column<decimal>(nullable: true),
                    width = table.Column<decimal>(nullable: true),
                    thickness = table.Column<decimal>(nullable: true),
                    quantity = table.Column<decimal>(nullable: true),
                    faces = table.Column<decimal>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.MeasurelId);
                });

            migrationBuilder.CreateTable(
                name: "MotivoConciliacion",
                columns: table => new
                {
                    MotivoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivoConciliacion", x => x.MotivoId);
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
                    _cai = table.Column<string>(nullable: true),
                    NoInicio = table.Column<string>(nullable: true),
                    NoFin = table.Column<string>(nullable: true),
                    FechaLimite = table.Column<DateTime>(nullable: false),
                    CantidadOtorgada = table.Column<int>(nullable: false),
                    SiguienteNumero = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
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
                name: "Party",
                columns: table => new
                {
                    PartyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartyType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentScheduleRulesByCustomer",
                columns: table => new
                {
                    PaymentScheduleRulesByCustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ScheduleSubservicesId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentScheduleRulesByCustomer", x => x.PaymentScheduleRulesByCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentTypesName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypesId);
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
                name: "PayrollDeduction",
                columns: table => new
                {
                    PayrollDeductionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: false),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    ConceptName = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Fees = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeduction", x => x.PayrollDeductionId);
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
                    Comments = table.Column<string>(nullable: true),
                    Periodo = table.Column<string>(nullable: true),
                    Official = table.Column<string>(nullable: true),
                    PartidoPoliticoId = table.Column<long>(nullable: false),
                    PartidoPoliticoName = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: true),
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
                name: "PeriodicidadPago",
                columns: table => new
                {
                    PeriodicidadId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicidadPago", x => x.PeriodicidadId);
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
                name: "ProbabilidadRiesgo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nivel = table.Column<long>(nullable: false),
                    Descriptor = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Frecuencia = table.Column<string>(nullable: true),
                    ColorHexadecimal = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProbabilidadRiesgo", x => x.Id);
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
                name: "ProductUserRelation",
                columns: table => new
                {
                    ProductUserRelationId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    DocType = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUserRelation", x => x.ProductUserRelationId);
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
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAreaId = table.Column<long>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    SalesOrderId = table.Column<long>(nullable: false),
                    CertificadoDepositoId = table.Column<long>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Tax18 = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    TotalExento = table.Column<decimal>(nullable: false),
                    TotalExonerado = table.Column<decimal>(nullable: false),
                    TotalGravado = table.Column<decimal>(nullable: false),
                    TotalGravado18 = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
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
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
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
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
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
                name: "RecipeDetail",
                columns: table => new
                {
                    IngredientCode = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecipeId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Thickness = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    NumCara = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: true),
                    MaterialId = table.Column<long>(nullable: false),
                    MaterialType = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeDetail", x => x.IngredientCode);
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
                name: "RetentionReceipt",
                columns: table => new
                {
                    RetentionReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DueDate = table.Column<DateTime>(nullable: false),
                    DocumentId = table.Column<long>(nullable: false),
                    IdTipoDocumento = table.Column<long>(nullable: false),
                    NoCorrelativo = table.Column<string>(nullable: true),
                    CAI = table.Column<string>(nullable: true),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    RTN = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    VendorId = table.Column<long>(nullable: false),
                    VendorCAI = table.Column<string>(nullable: true),
                    IdEmpleado = table.Column<long>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    BranchCode = table.Column<string>(nullable: true),
                    IdPuntoEmision = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    NumeroDEI = table.Column<int>(nullable: false),
                    RetentionTaxDescription = table.Column<string>(nullable: true),
                    TaxableBase = table.Column<double>(nullable: false),
                    Percentage = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionReceipt", x => x.RetentionReceiptId);
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
                    TypeInvoiceId = table.Column<long>(nullable: false),
                    TypeInvoiceName = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    Tefono = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
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
                    Currency = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Tax18 = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    TotalExento = table.Column<decimal>(nullable: false),
                    TotalExonerado = table.Column<decimal>(nullable: false),
                    TotalGravado = table.Column<decimal>(nullable: false),
                    TotalGravado18 = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
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
                name: "ScheduleSubservices",
                columns: table => new
                {
                    ScheduleSubservicesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<decimal>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    SubServiceId = table.Column<long>(nullable: false),
                    SubServiceName = table.Column<string>(nullable: true),
                    FactorHora = table.Column<decimal>(nullable: false),
                    Desayuno = table.Column<decimal>(nullable: false),
                    Almuerzo = table.Column<decimal>(nullable: false),
                    Cena = table.Column<decimal>(nullable: false),
                    Transporte = table.Column<decimal>(nullable: false),
                    LogicalConditionId = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Transport = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleSubservices", x => x.ScheduleSubservicesId);
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
                name: "SeveridadRiesgo",
                columns: table => new
                {
                    IdSeveridad = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Impacto = table.Column<long>(nullable: false),
                    Probabilidad = table.Column<long>(nullable: false),
                    LimiteCalidadInferior = table.Column<long>(nullable: false),
                    LimeteCalidadSuperir = table.Column<long>(nullable: false),
                    RangoInferiorSeveridad = table.Column<double>(nullable: false),
                    RangoSuperiorSeveridad = table.Column<double>(nullable: false),
                    Nivel = table.Column<string>(nullable: true),
                    ColorHexadecimal = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeveridadRiesgo", x => x.IdSeveridad);
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
                    Aduana = table.Column<string>(nullable: true),
                    ManifiestoNo = table.Column<string>(nullable: true),
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
                    TipoProhibidoId = table.Column<long>(nullable: false),
                    TipoProhibidoName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    BagBalance = table.Column<long>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    Barcode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<int>(nullable: true),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Merma = table.Column<decimal>(nullable: false),
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
                name: "SubServicesWareHouse",
                columns: table => new
                {
                    SubServicesWareHouseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    BranchId = table.Column<long>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    ServiceId = table.Column<long>(nullable: false),
                    ServiceName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    SubServiceId = table.Column<long>(nullable: false),
                    SubServiceName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubServicesWareHouse", x => x.SubServicesWareHouseId);
                });

            migrationBuilder.CreateTable(
                name: "Substratum",
                columns: table => new
                {
                    SubstratumId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SubstratumCode = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EstatusId = table.Column<long>(nullable: false),
                    Estatus = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substratum", x => x.SubstratumId);
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
                    TaxPercentage = table.Column<decimal>(nullable: false),
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
                name: "TipodeAccionderiesgo",
                columns: table => new
                {
                    TipodeAccionderiesgoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipodeaccion = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipodeAccionderiesgo", x => x.TipodeAccionderiesgoId);
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
                name: "TypeAccount",
                columns: table => new
                {
                    TypeAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeAccountName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    DeudoraAcreedora = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAccount", x => x.TypeAccountId);
                });

            migrationBuilder.CreateTable(
                name: "TypeJournal",
                columns: table => new
                {
                    TypeJournalId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeJournalName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeJournal", x => x.TypeJournalId);
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
                name: "VendorDocument",
                columns: table => new
                {
                    VendorDocumentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorId = table.Column<long>(nullable: false),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDocument", x => x.VendorDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "VendorProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<long>(nullable: false),
                    VendorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorType",
                columns: table => new
                {
                    VendorTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorTypeName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
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
                    CapacidadBodega = table.Column<double>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    CantidadPoliza = table.Column<double>(nullable: false),
                    FechaEmisionPoliza = table.Column<DateTime>(nullable: false),
                    FechaVencimientoPoliza = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    FechaLibertadGravamen = table.Column<DateTime>(nullable: true),
                    FechaHabilitacion = table.Column<DateTime>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    RoleName = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                columns: table => new
                {
                    PasswordHistoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.PasswordHistoryId);
                    table.ForeignKey(
                        name: "FK_PasswordHistory_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BitacoraCierreProceso",
                columns: table => new
                {
                    IdProceso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBitacoraCierre = table.Column<int>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    PasoCierre = table.Column<int>(nullable: false),
                    Proceso = table.Column<string>(nullable: true),
                    Mensaje = table.Column<string>(nullable: true),
                    Estatus = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraCierreProceso", x => x.IdProceso);
                    table.ForeignKey(
                        name: "FK_BitacoraCierreProceso_BitacoraCierreContable_IdBitacoraCierre",
                        column: x => x.IdBitacoraCierre,
                        principalTable: "BitacoraCierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    s_manual = table.Column<bool>(nullable: false),
                    completo = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto_Sal", x => x.clave_e);
                    table.ForeignKey(
                        name: "FK_Boleto_Sal_Boleto_Ent_clave_e",
                        column: x => x.clave_e,
                        principalTable: "Boleto_Ent",
                        principalColumn: "clave_e",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    AccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountBalance = table.Column<decimal>(type: "Money", nullable: false),
                    AceptaNegativo = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    Totaliza = table.Column<bool>(nullable: false),
                    DeudoraAcreedora = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounting_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounting_Accounting_ParentAccountAccountId",
                        column: x => x.ParentAccountAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConciliacionLinea",
                columns: table => new
                {
                    ConciliacionLineaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MotivoId = table.Column<long>(nullable: true),
                    ConciliacionId = table.Column<int>(nullable: false),
                    Credit = table.Column<double>(nullable: false),
                    Debit = table.Column<double>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    ReferenciaBancaria = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    TransDate = table.Column<DateTime>(nullable: false),
                    ReferenceTrans = table.Column<string>(nullable: true),
                    JournalEntryId = table.Column<long>(nullable: true),
                    JournalEntryLineId = table.Column<long>(nullable: true),
                    VoucherTypeId = table.Column<long>(nullable: true),
                    Reconciled = table.Column<bool>(nullable: false),
                    CheknumberId = table.Column<long>(nullable: true),
                    MonedaName = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConciliacionLinea", x => x.ConciliacionLineaId);
                    table.ForeignKey(
                        name: "FK_ConciliacionLinea_Conciliacion_ConciliacionId",
                        column: x => x.ConciliacionId,
                        principalTable: "Conciliacion",
                        principalColumn: "ConciliacionId",
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
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
                name: "CreditNoteLine",
                columns: table => new
                {
                    CreditNoteLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreditNoteId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "Money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteLine", x => x.CreditNoteLineId);
                    table.ForeignKey(
                        name: "FK_CreditNoteLine_CreditNote_CreditNoteId",
                        column: x => x.CreditNoteId,
                        principalTable: "CreditNote",
                        principalColumn: "CreditNoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    ExchangeRateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayofRate = table.Column<DateTime>(nullable: false),
                    ExchangeRateValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ExchangeRateValueCompra = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.ExchangeRateId);
                    table.ForeignKey(
                        name: "FK_ExchangeRate_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DebitNoteLine",
                columns: table => new
                {
                    DebitNoteLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitNoteId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<long>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitNoteLine", x => x.DebitNoteLineId);
                    table.ForeignKey(
                        name: "FK_DebitNoteLine_DebitNote_DebitNoteId",
                        column: x => x.DebitNoteId,
                        principalTable: "DebitNote",
                        principalColumn: "DebitNoteId",
                        onDelete: ReferentialAction.Restrict);
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
                    CertificadoLineId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<double>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                    CertificadoLineId = table.Column<long>(nullable: false),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ValorEndoso = table.Column<decimal>(nullable: false),
                    Saldo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndososCertificadosLine", x => x.EndososCertificadosLineId);
                    table.ForeignKey(
                        name: "FK_EndososCertificadosLine_EndososCertificados_EndososCertificadosId",
                        column: x => x.EndososCertificadosId,
                        principalTable: "EndososCertificados",
                        principalColumn: "EndososCertificadosId",
                        onDelete: ReferentialAction.Restrict);
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
                    CertificadoLineId = table.Column<long>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Biometricos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biometricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biometricos_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionVacaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Antiguedad = table.Column<int>(nullable: false),
                    DiasVacaciones = table.Column<int>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionVacaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionVacaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
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
                    Observcion = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContract", x => x.CustomerContractId);
                    table.ForeignKey(
                        name: "FK_CustomerContract_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feriados",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feriados_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GarantiaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    strign = table.Column<string>(nullable: true),
                    FechaInicioVigencia = table.Column<DateTime>(nullable: false),
                    FechaFianlVigencia = table.Column<DateTime>(nullable: false),
                    NumeroCertificado = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
                    Monto = table.Column<decimal>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Ajuste = table.Column<decimal>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarantiaBancaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GarantiaBancaria_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    HoraInicio = table.Column<string>(nullable: false),
                    HoraFinal = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsurancesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsurancesName = table.Column<string>(nullable: true),
                    DocumentTypeId = table.Column<long>(nullable: false),
                    DocumentTypeName = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsurancesId);
                    table.ForeignKey(
                        name: "FK_Insurances_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Periodo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Anio = table.Column<int>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periodo_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoPlanillas",
                columns: table => new
                {
                    IdTipoPlanilla = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPlanilla = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: false),
                    CategoriaId = table.Column<long>(nullable: false),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPlanillas", x => x.IdTipoPlanilla);
                    table.ForeignKey(
                        name: "FK_TipoPlanillas_CategoriasPlanillas_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasPlanillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoPlanillas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposBonificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposBonificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposBonificaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    valorcertificado = table.Column<decimal>(nullable: false),
                    valorfinanciado = table.Column<decimal>(nullable: false),
                    ValorImpuestos = table.Column<decimal>(nullable: false),
                    SaldoProducto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsDeliveryAuthorizationLine", x => x.GoodsDeliveryAuthorizationLineId);
                    table.ForeignKey(
                        name: "FK_GoodsDeliveryAuthorizationLine_GoodsDeliveryAuthorization_GoodsDeliveryAuthorizationId",
                        column: x => x.GoodsDeliveryAuthorizationId,
                        principalTable: "GoodsDeliveryAuthorization",
                        principalColumn: "GoodsDeliveryAuthorizationId",
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    QuantitySacos = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElementoConfiguracion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    WareHouseId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "Money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.InvoiceLineId);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryConfigurationLine",
                columns: table => new
                {
                    JournalEntryConfigurationLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryConfigurationId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    DebitCreditId = table.Column<long>(nullable: false),
                    DebitCredit = table.Column<string>(nullable: true),
                    SubProductId = table.Column<long>(nullable: false),
                    SubProductName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryConfigurationLine", x => x.JournalEntryConfigurationLineId);
                    table.ForeignKey(
                        name: "FK_JournalEntryConfigurationLine_JournalEntryConfiguration_JournalEntryConfigurationId",
                        column: x => x.JournalEntryConfigurationId,
                        principalTable: "JournalEntryConfiguration",
                        principalColumn: "JournalEntryConfigurationId",
                        onDelete: ReferentialAction.Restrict);
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
                    SaldoAnterior = table.Column<decimal>(nullable: false),
                    QuantityEntry = table.Column<decimal>(nullable: false),
                    QuantityOut = table.Column<decimal>(nullable: false),
                    QuantityEntryBags = table.Column<decimal>(nullable: false),
                    QuantityOutBags = table.Column<decimal>(nullable: false),
                    QuantityEntryCD = table.Column<decimal>(nullable: false),
                    QuantityOutCD = table.Column<decimal>(nullable: false),
                    TotalCD = table.Column<decimal>(nullable: false),
                    TotalBags = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    TypeOperationId = table.Column<int>(nullable: false),
                    TypeOperationName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    MinimumExistance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KardexLine", x => x.KardexLineId);
                    table.ForeignKey(
                        name: "FK_KardexLine_Kardex_KardexId",
                        column: x => x.KardexId,
                        principalTable: "Kardex",
                        principalColumn: "KardexId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "CierresJournal",
                columns: table => new
                {
                    CierresJournalEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<long>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    BitacoraCierreContableId = table.Column<int>(nullable: false),
                    GeneralLedgerHeaderId = table.Column<int>(nullable: true),
                    PartyTypeId = table.Column<int>(nullable: false),
                    PartyTypeName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    PartyId = table.Column<int>(nullable: true),
                    VoucherType = table.Column<int>(nullable: true),
                    TypeJournalName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    ApprovedBy = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Posted = table.Column<bool>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true),
                    PartyId1 = table.Column<long>(nullable: true),
                    IdPaymentCode = table.Column<int>(nullable: false),
                    IdTypeofPayment = table.Column<int>(nullable: false),
                    EstadoId = table.Column<long>(nullable: true),
                    EstadoName = table.Column<string>(nullable: true),
                    TotalDebit = table.Column<decimal>(nullable: false),
                    TotalCredit = table.Column<decimal>(nullable: false),
                    TypeOfAdjustmentId = table.Column<int>(nullable: false),
                    TypeOfAdjustmentName = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresJournal", x => x.CierresJournalEntryId);
                    table.ForeignKey(
                        name: "FK_CierresJournal_BitacoraCierreContable_BitacoraCierreContableId",
                        column: x => x.BitacoraCierreContableId,
                        principalTable: "BitacoraCierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresJournal_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresJournal_Party_PartyId1",
                        column: x => x.PartyId1,
                        principalTable: "Party",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntry",
                columns: table => new
                {
                    JournalEntryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeneralLedgerHeaderId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    PartyTypeId = table.Column<int>(nullable: false),
                    PartyTypeName = table.Column<string>(nullable: true),
                    DocumentId = table.Column<long>(nullable: false),
                    PartyId = table.Column<int>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    VoucherType = table.Column<int>(nullable: true),
                    TypeJournalName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DatePosted = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    Posted = table.Column<bool>(nullable: true),
                    GeneralLedgerHeaderId1 = table.Column<long>(nullable: true),
                    PartyId1 = table.Column<long>(nullable: true),
                    IdPaymentCode = table.Column<int>(nullable: false),
                    IdTypeofPayment = table.Column<int>(nullable: false),
                    EstadoId = table.Column<long>(nullable: true),
                    EstadoName = table.Column<string>(nullable: true),
                    TotalDebit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "Money", nullable: false),
                    TypeOfAdjustmentId = table.Column<int>(nullable: false),
                    TypeOfAdjustmentName = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntry", x => x.JournalEntryId);
                    table.ForeignKey(
                        name: "FK_JournalEntry_GeneralLedgerHeader_GeneralLedgerHeaderId1",
                        column: x => x.GeneralLedgerHeaderId1,
                        principalTable: "GeneralLedgerHeader",
                        principalColumn: "GeneralLedgerHeaderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntry_Party_PartyId1",
                        column: x => x.PartyId1,
                        principalTable: "Party",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdPolicy = table.Column<Guid>(nullable: false),
                    PolicyName = table.Column<string>(nullable: true),
                    IdRol = table.Column<Guid>(nullable: false),
                    RolName = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyRoles_Policy_IdPolicy",
                        column: x => x.IdPolicy,
                        principalTable: "Policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyRoles_AspNetRoles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "AspNetRoles",
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    WareHouseId = table.Column<long>(nullable: false),
                    WareHouseName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoiceLine", x => x.ProformaLineId);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceLine_ProformaInvoice_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoice",
                        principalColumn: "ProformaId",
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DiscountPercentage = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    Valor = table.Column<decimal>(nullable: true),
                    Porcentaje = table.Column<decimal>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLine", x => x.SalesOrderLineId);
                    table.ForeignKey(
                        name: "FK_SalesOrderLine_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TotalCantidad = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCertificadoLine", x => x.CertificadoLineId);
                    table.ForeignKey(
                        name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
                        column: x => x.IdSCD,
                        principalTable: "SolicitudCertificadoDeposito",
                        principalColumn: "IdSCD",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CierresAccounting",
                columns: table => new
                {
                    CierreAccountingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<long>(nullable: false),
                    ParentAccountId = table.Column<int>(nullable: true),
                    CompanyInfoId = table.Column<long>(nullable: false),
                    AccountBalance = table.Column<double>(nullable: false),
                    BitacoraCierreContableId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    IsCash = table.Column<bool>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: false),
                    BlockedInJournal = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<string>(maxLength: 50, nullable: false),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    HierarchyAccount = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    ParentAccountAccountId = table.Column<long>(nullable: true),
                    FechaCierre = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresAccounting", x => x.CierreAccountingId);
                    table.ForeignKey(
                        name: "FK_CierresAccounting_BitacoraCierreContable_BitacoraCierreContableId",
                        column: x => x.BitacoraCierreContableId,
                        principalTable: "BitacoraCierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresAccounting_CompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "CompanyInfo",
                        principalColumn: "CompanyInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresAccounting_Accounting_ParentAccountAccountId",
                        column: x => x.ParentAccountAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixedAssetGroup",
                columns: table => new
                {
                    FixedAssetGroupId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FixedAssetGroupName = table.Column<string>(nullable: true),
                    FixedAssetGroupDescription = table.Column<string>(nullable: true),
                    FixedGroupCode = table.Column<string>(nullable: true),
                    FixedAssetAccountingId = table.Column<long>(nullable: true),
                    DepreciationAccountingId = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FixedAssetGroupId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAssetGroup", x => x.FixedAssetGroupId);
                    table.ForeignKey(
                        name: "FK_FixedAssetGroup_Accounting_DepreciationAccountingId",
                        column: x => x.DepreciationAccountingId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedAssetGroup_Accounting_FixedAssetAccountingId",
                        column: x => x.FixedAssetAccountingId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixedAssetGroup_FixedAssetGroup_FixedAssetGroupId1",
                        column: x => x.FixedAssetGroupId1,
                        principalTable: "FixedAssetGroup",
                        principalColumn: "FixedAssetGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    PaymentTypesId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Days = table.Column<int>(nullable: false),
                    Fees = table.Column<int>(nullable: false),
                    FirstPayment = table.Column<double>(nullable: false),
                    EarlyPaymentDiscount = table.Column<double>(nullable: false),
                    AccountingAccountId = table.Column<long>(nullable: true),
                    ChekingAccount = table.Column<string>(nullable: true),
                    Default = table.Column<int>(nullable: false),
                    CustomerPayIn = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTerms_Accounting_AccountingAccountId",
                        column: x => x.AccountingAccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTerms_PaymentTypes_PaymentTypesId",
                        column: x => x.PaymentTypesId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    StateId = table.Column<long>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrecioCafe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    PrecioBolsaUSD = table.Column<decimal>(nullable: false),
                    DiferencialesUSD = table.Column<decimal>(nullable: false),
                    TotalUSD = table.Column<decimal>(nullable: false),
                    ExchangeRateId = table.Column<long>(nullable: false),
                    BrutoLPSIngreso = table.Column<decimal>(nullable: true),
                    PorcentajeIngreso = table.Column<decimal>(nullable: false),
                    NetoLPSIngreso = table.Column<decimal>(nullable: true),
                    BrutoLPSConsumoInterno = table.Column<decimal>(nullable: false),
                    PorcentajeConsumoInterno = table.Column<decimal>(nullable: false),
                    NetoLPSConsumoInterno = table.Column<decimal>(nullable: false),
                    TotalLPSIngreso = table.Column<decimal>(nullable: true),
                    BeneficiadoUSD = table.Column<decimal>(nullable: false),
                    FideicomisoUSD = table.Column<decimal>(nullable: false),
                    UtilidadUSD = table.Column<decimal>(nullable: false),
                    PermisoExportacionUSD = table.Column<decimal>(nullable: false),
                    TotalUSDEgreso = table.Column<decimal>(nullable: false),
                    TotalLPSEgreso = table.Column<decimal>(nullable: true),
                    PrecioQQOro = table.Column<decimal>(nullable: true),
                    PercioQQPergamino = table.Column<decimal>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecioCafe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecioCafe_ExchangeRate_ExchangeRateId",
                        column: x => x.ExchangeRateId,
                        principalTable: "ExchangeRate",
                        principalColumn: "ExchangeRateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostCenterId = table.Column<long>(nullable: false),
                    PeriodoId = table.Column<int>(nullable: false),
                    PresupuestoEnero = table.Column<decimal>(nullable: false),
                    PresupuestoFebrero = table.Column<decimal>(nullable: false),
                    PresupuestoMarzo = table.Column<decimal>(nullable: false),
                    PresupuestoAbril = table.Column<decimal>(nullable: false),
                    PresupuestoMayo = table.Column<decimal>(nullable: false),
                    PresupuestoJunio = table.Column<decimal>(nullable: false),
                    PresupuestoJulio = table.Column<decimal>(nullable: false),
                    PresupuestoAgosto = table.Column<decimal>(nullable: false),
                    PresupuestoSeptiembre = table.Column<decimal>(nullable: false),
                    PresupuestoOctubre = table.Column<decimal>(nullable: false),
                    PresupuestoNoviembre = table.Column<decimal>(nullable: false),
                    PresupuestoDiciembre = table.Column<decimal>(nullable: false),
                    EjecucionEnero = table.Column<decimal>(nullable: false),
                    EjecucionFebrero = table.Column<decimal>(nullable: false),
                    EjecucionMarzo = table.Column<decimal>(nullable: false),
                    EjecucionAbril = table.Column<decimal>(nullable: false),
                    EjecucionMayo = table.Column<decimal>(nullable: false),
                    EjecucionJunio = table.Column<decimal>(nullable: false),
                    EjecucionJulio = table.Column<decimal>(nullable: false),
                    EjecucionAgosto = table.Column<decimal>(nullable: false),
                    EjecucionSeptiembre = table.Column<decimal>(nullable: false),
                    EjecucionOctubre = table.Column<decimal>(nullable: false),
                    EjecucionNoviembre = table.Column<decimal>(nullable: false),
                    EjecucionDiciembre = table.Column<decimal>(nullable: false),
                    AccountigId = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    TotalMontoPresupuesto = table.Column<decimal>(nullable: false),
                    TotalMontoEjecucion = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Accounting_AccountigId",
                        column: x => x.AccountigId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuesto_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Periodo_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "Periodo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TipoPlanillaId = table.Column<long>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Periodo = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    TotalEmpleados = table.Column<int>(nullable: false),
                    TotalPlanilla = table.Column<double>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planillas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planillas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planillas_TipoPlanillas_TipoPlanillaId",
                        column: x => x.TipoPlanillaId,
                        principalTable: "TipoPlanillas",
                        principalColumn: "IdTipoPlanilla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountManagement",
                columns: table => new
                {
                    AccountManagementId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OpeningDate = table.Column<DateTime>(nullable: false),
                    AccountType = table.Column<string>(nullable: false),
                    TypeAccountId = table.Column<long>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountManagement", x => x.AccountManagementId);
                    table.ForeignKey(
                        name: "FK_AccountManagement_ElementoConfiguracion_TypeAccountId",
                        column: x => x.TypeAccountId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doc_CP",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocNumber = table.Column<string>(nullable: true),
                    DocTypeId = table.Column<int>(nullable: false),
                    DocTipoId = table.Column<long>(nullable: true),
                    DocDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    PartialAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentQty = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Balance_Mon = table.Column<double>(nullable: false),
                    DocPaymentNumber = table.Column<string>(nullable: true),
                    Payed = table.Column<bool>(nullable: false),
                    LatePaymentAmount = table.Column<double>(nullable: false),
                    LatePaymentInterest = table.Column<double>(nullable: false),
                    DayTerms = table.Column<int>(nullable: false),
                    VendorDocumentId = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    AnnulationReason = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: false),
                    PaymentTypeId = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    Base = table.Column<bool>(nullable: false),
                    PaymentNumber = table.Column<string>(nullable: true),
                    PaymentReference = table.Column<string>(nullable: true),
                    PaymentTerm = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doc_CP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Accounting_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doc_CP_ElementoConfiguracion_DocTipoId",
                        column: x => x.DocTipoId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doc_CP_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doc_CP_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
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
                name: "CierresJournalEntryLine",
                columns: table => new
                {
                    CierresJournalEntryLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryLineId = table.Column<long>(nullable: false),
                    JournalEntryId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(maxLength: 30, nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 60, nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    Debit = table.Column<decimal>(nullable: false),
                    Credit = table.Column<decimal>(nullable: false),
                    DebitSy = table.Column<decimal>(nullable: false),
                    CreditSy = table.Column<decimal>(nullable: false),
                    DebitME = table.Column<decimal>(nullable: false),
                    CreditME = table.Column<decimal>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    BitacoraCierreContableId = table.Column<int>(nullable: false),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CierresJournalEntryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CierresJournalEntryLine", x => x.CierresJournalEntryLineId);
                    table.ForeignKey(
                        name: "FK_CierresJournalEntryLine_BitacoraCierreContable_BitacoraCierreContableId",
                        column: x => x.BitacoraCierreContableId,
                        principalTable: "BitacoraCierreContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CierresJournalEntryLine_CierresJournal_CierresJournalEntryId",
                        column: x => x.CierresJournalEntryId,
                        principalTable: "CierresJournal",
                        principalColumn: "CierresJournalEntryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLine",
                columns: table => new
                {
                    JournalEntryLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalEntryId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    Debit = table.Column<decimal>(type: "Money", nullable: false),
                    Credit = table.Column<decimal>(type: "Money", nullable: false),
                    DebitSy = table.Column<decimal>(type: "Money", nullable: false),
                    CreditSy = table.Column<decimal>(type: "Money", nullable: false),
                    DebitME = table.Column<decimal>(type: "Money", nullable: false),
                    CreditME = table.Column<decimal>(type: "Money", nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    PartyTypeId = table.Column<int>(nullable: false),
                    PartyTypeName = table.Column<string>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    PartyName = table.Column<string>(nullable: true),
                    AccountId1 = table.Column<long>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLine", x => x.JournalEntryLineId);
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_Accounting_AccountId1",
                        column: x => x.AccountId1,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLine_JournalEntry_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
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
                name: "Branch",
                columns: table => new
                {
                    BranchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchCode = table.Column<string>(nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    LimitCNBS = table.Column<decimal>(nullable: true),
                    StateId = table.Column<long>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Observation = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_Branch_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branch_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(nullable: false),
                    Denominacion = table.Column<string>(nullable: true),
                    IdentidadApoderado = table.Column<string>(nullable: true),
                    NombreApoderado = table.Column<string>(nullable: true),
                    CustomerRefNumber = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: false),
                    CustomerTypeId = table.Column<long>(nullable: true),
                    CustomerTypeName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    CityId = table.Column<long>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateId = table.Column<long>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    WorkPhone = table.Column<string>(nullable: true),
                    Identidad = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    GrupoEconomicoId = table.Column<long>(nullable: true),
                    GrupoEconomico = table.Column<string>(nullable: true),
                    MontoActivos = table.Column<double>(nullable: false),
                    MontoIngresosAnuales = table.Column<double>(nullable: false),
                    Proveedor1 = table.Column<string>(nullable: true),
                    Proveedor2 = table.Column<string>(nullable: true),
                    ClienteRecoger = table.Column<bool>(nullable: false),
                    EnviarlaMensajero = table.Column<bool>(nullable: false),
                    DireccionEnvio = table.Column<string>(nullable: true),
                    PerteneceEmpresa = table.Column<string>(nullable: true),
                    ValorSeveridadRiesgo = table.Column<double>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    SolicitadoPor = table.Column<string>(nullable: true),
                    EsExonerado = table.Column<bool>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    ConfirmacionCorreo = table.Column<bool>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerType",
                        principalColumn: "CustomerTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorName = table.Column<string>(nullable: false),
                    VendorTypeId = table.Column<long>(nullable: false),
                    VendorTypeName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    IdEstadoVendorConfi = table.Column<long>(nullable: true),
                    EstadoVendorConfi = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    RTN = table.Column<string>(nullable: false),
                    Identidad = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    QtyMonth = table.Column<double>(nullable: false),
                    PhoneReferenceone = table.Column<string>(nullable: true),
                    CompanyReferenceone = table.Column<string>(nullable: true),
                    PhoneReferencetwo = table.Column<string>(nullable: true),
                    CompanyReferencetwo = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    IdentityRepresentative = table.Column<string>(nullable: true),
                    RTNRepresentative = table.Column<string>(nullable: true),
                    RepresentativeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendor_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorType_VendorTypeId",
                        column: x => x.VendorTypeId,
                        principalTable: "VendorType",
                        principalColumn: "VendorTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallePlanillas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlanillaId = table.Column<long>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    MontoBruto = table.Column<double>(nullable: false),
                    TotalDeducciones = table.Column<double>(nullable: false),
                    MontoNeto = table.Column<double>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePlanillas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallePlanillas_Planillas_PlanillaId",
                        column: x => x.PlanillaId,
                        principalTable: "Planillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckAccount",
                columns: table => new
                {
                    CheckAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckAccountNo = table.Column<string>(nullable: true),
                    AssociatedAccountNumber = table.Column<string>(nullable: true),
                    AccountManagementId = table.Column<long>(nullable: false),
                    BankId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    NoInicial = table.Column<string>(nullable: true),
                    NoFinal = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    NumeroActual = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckAccount", x => x.CheckAccountId);
                    table.ForeignKey(
                        name: "FK_CheckAccount_AccountManagement_AccountManagementId",
                        column: x => x.AccountManagementId,
                        principalTable: "AccountManagement",
                        principalColumn: "AccountManagementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckAccount_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmpleado = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    Salario = table.Column<decimal>(nullable: true),
                    Identidad = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    HorasExtra = table.Column<bool>(nullable: true),
                    IdTipoPlanilla = table.Column<long>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    Profesion = table.Column<string>(nullable: true),
                    FechaEgreso = table.Column<DateTime>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Genero = table.Column<string>(nullable: true),
                    IdActivoinactivo = table.Column<long>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    IdPuesto = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    IdCity = table.Column<long>(nullable: false),
                    IdState = table.Column<long>(nullable: false),
                    IdCountry = table.Column<long>(nullable: false),
                    IdCurrency = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: true),
                    IdBank = table.Column<long>(nullable: false),
                    IdBranch = table.Column<int>(nullable: false),
                    IdTipoContrato = table.Column<long>(nullable: false),
                    IdDepartamento = table.Column<long>(nullable: false),
                    CuentaBanco = table.Column<string>(nullable: true),
                    FechaFinContrato = table.Column<DateTime>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Extension = table.Column<int>(nullable: false),
                    Notas = table.Column<string>(nullable: true),
                    TipoSangre = table.Column<string>(nullable: true),
                    NombreContacto = table.Column<string>(nullable: true),
                    TelefonoContacto = table.Column<string>(nullable: true),
                    ApplyCommission = table.Column<bool>(nullable: false),
                    ComisionId = table.Column<long>(nullable: false),
                    CommissionName = table.Column<string>(nullable: true),
                    PeriodicidadId = table.Column<long>(nullable: false),
                    PeriodicityName = table.Column<string>(nullable: true),
                    Usuariocreacion = table.Column<string>(nullable: true),
                    Usuariomodificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Bank_IdBank",
                        column: x => x.IdBank,
                        principalTable: "Bank",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Branch_IdBranch",
                        column: x => x.IdBranch,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_City_IdCity",
                        column: x => x.IdCity,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Currency_IdCurrency",
                        column: x => x.IdCurrency,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Departamento_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "IdDepartamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Puesto_IdPuesto",
                        column: x => x.IdPuesto,
                        principalTable: "Puesto",
                        principalColumn: "IdPuesto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_State_IdState",
                        column: x => x.IdState,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_TipoContrato_IdTipoContrato",
                        column: x => x.IdTipoContrato,
                        principalTable: "TipoContrato",
                        principalColumn: "IdTipoContrato",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_TipoPlanillas_IdTipoPlanilla",
                        column: x => x.IdTipoPlanilla,
                        principalTable: "TipoPlanillas",
                        principalColumn: "IdTipoPlanilla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneLines",
                columns: table => new
                {
                    PhoneLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: false),
                    NombreEmpleado = table.Column<string>(nullable: true),
                    IdBranch = table.Column<int>(nullable: false),
                    CompanyPhone = table.Column<string>(nullable: true),
                    DueBalanceLps = table.Column<double>(nullable: false),
                    DueBalanceUS = table.Column<double>(nullable: false),
                    PaymentLps = table.Column<double>(nullable: false),
                    PaymentUS = table.Column<double>(nullable: false),
                    TotalLps = table.Column<double>(nullable: false),
                    TotalUS = table.Column<double>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLines", x => x.PhoneLineId);
                    table.ForeignKey(
                        name: "FK_PhoneLines_Branch_IdBranch",
                        column: x => x.IdBranch,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
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
                    Correlative = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductImageUrl = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    UnitOfMeasureId = table.Column<int>(nullable: true),
                    DefaultBuyingPrice = table.Column<double>(nullable: false),
                    DefaultSellingPrice = table.Column<double>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    FundingInterestRateId = table.Column<int>(nullable: true),
                    Prima = table.Column<decimal>(nullable: true),
                    ProductTypeId = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_FundingInterestRate_FundingInterestRateId",
                        column: x => x.FundingInterestRateId,
                        principalTable: "FundingInterestRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_ElementoConfiguracion_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: true),
                    ModifiedUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserBranch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    ContratoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Fecha_inicio = table.Column<DateTime>(nullable: false),
                    Valor_prima = table.Column<double>(nullable: false),
                    Valor_Contrato = table.Column<double>(nullable: false),
                    Saldo_Contrato = table.Column<double>(nullable: false),
                    Dias_mora = table.Column<int>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Valor_cuota = table.Column<double>(nullable: false),
                    Cuotas_pagadas = table.Column<int>(nullable: false),
                    Cuotas_pendiente = table.Column<int>(nullable: false),
                    Proxima_fecha_de_pago = table.Column<DateTime>(nullable: false),
                    Ultima_fecha_de_pago = table.Column<DateTime>(nullable: false),
                    Fecha_de_vencimiento = table.Column<DateTime>(nullable: false),
                    Plazo = table.Column<int>(nullable: false),
                    Tasa_de_Interes = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.ContratoId);
                    table.ForeignKey(
                        name: "FK_Contrato_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicy",
                columns: table => new
                {
                    InsurancePolicyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PolicyDate = table.Column<DateTime>(nullable: false),
                    PolicyDueDate = table.Column<DateTime>(nullable: false),
                    PolicyNumber = table.Column<string>(nullable: false),
                    InsurancesId = table.Column<int>(nullable: false),
                    InsurancesName = table.Column<string>(nullable: true),
                    Propias = table.Column<bool>(nullable: true),
                    CustomerId = table.Column<long>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    ReductibleRate = table.Column<decimal>(nullable: false),
                    MerchandiseRate = table.Column<decimal>(nullable: false),
                    LpsAmount = table.Column<decimal>(nullable: false),
                    DollarAmount = table.Column<decimal>(nullable: false),
                    InsuredTotal = table.Column<decimal>(nullable: false),
                    AttachmentURL = table.Column<string>(nullable: true),
                    AttachementFileName = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicy", x => x.InsurancePolicyId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
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
                name: "PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PONumber = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    EstadoId = table.Column<long>(nullable: true),
                    POTypeId = table.Column<long>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    VendorId = table.Column<long>(nullable: true),
                    VendorName = table.Column<string>(nullable: true),
                    DatePlaced = table.Column<DateTime>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    Terms = table.Column<string>(nullable: true),
                    Freight = table.Column<double>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    TaxName = table.Column<string>(nullable: true),
                    TaxRate = table.Column<decimal>(nullable: false),
                    ShippingTypeId = table.Column<long>(nullable: true),
                    ShippingTypeName = table.Column<string>(nullable: true),
                    Requisitioner = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    TaxAmount = table.Column<double>(nullable: false),
                    Tax18 = table.Column<double>(nullable: false),
                    TotalExento = table.Column<double>(nullable: false),
                    TotalExonerado = table.Column<double>(nullable: false),
                    TotalGravado = table.Column<double>(nullable: false),
                    TotalGravado18 = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_ElementoConfiguracion_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_ElementoConfiguracion_ShippingTypeId",
                        column: x => x.ShippingTypeId,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeduccionesPlanilla",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DetallePlanillaId = table.Column<long>(nullable: false),
                    DeduccionId = table.Column<long>(nullable: false),
                    NombreDeduccion = table.Column<string>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeduccionesPlanilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeduccionesPlanilla_DetallePlanillas_DetallePlanillaId",
                        column: x => x.DetallePlanillaId,
                        principalTable: "DetallePlanillas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckAccountLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckAccountId = table.Column<long>(nullable: false),
                    CheckNumber = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: true),
                    PaytoOrderOf = table.Column<string>(nullable: true),
                    RTN = table.Column<string>(nullable: true),
                    RetencionId = table.Column<int>(nullable: true),
                    Ammount = table.Column<decimal>(type: "Money", nullable: false),
                    AmountWords = table.Column<string>(nullable: true),
                    Sinopsis = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Impreso = table.Column<bool>(nullable: true),
                    JournalEntrId = table.Column<long>(nullable: true),
                    PartyTypeId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckAccountLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckAccountLines_CheckAccount_CheckAccountId",
                        column: x => x.CheckAccountId,
                        principalTable: "CheckAccount",
                        principalColumn: "CheckAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckAccountLines_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckAccountLines_JournalEntry_JournalEntrId",
                        column: x => x.JournalEntrId,
                        principalTable: "JournalEntry",
                        principalColumn: "JournalEntryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckAccountLines_RetentionReceipt_RetencionId",
                        column: x => x.RetencionId,
                        principalTable: "RetentionReceipt",
                        principalColumn: "RetentionReceiptId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bonificaciones",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    TipoId = table.Column<long>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    FechaBono = table.Column<DateTime>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonificaciones_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonificaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bonificaciones_TiposBonificaciones_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposBonificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlAsistencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Dia = table.Column<int>(nullable: false),
                    TipoAsistencia = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlAsistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlAsistencias_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeduccionesEmpleados",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    DeductionId = table.Column<long>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    VigenciaInicio = table.Column<DateTime>(nullable: false),
                    VigenciaFinaliza = table.Column<DateTime>(nullable: false),
                    EstadoId = table.Column<int>(nullable: false),
                    CantidadCuotas = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeduccionesEmpleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeduccionesEmpleados_Deduction_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Deduction",
                        principalColumn: "DeductionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeduccionesEmpleados_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesBiometricos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    FechaHora = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesBiometricos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesBiometricos_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesBiometricos_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoHorarios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<long>(nullable: false),
                    HorarioId = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoHorarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpleadoHorarios_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadosBiometrico",
                columns: table => new
                {
                    EmpleadoId = table.Column<long>(nullable: false),
                    BiometricoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosBiometrico", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_EmpleadosBiometrico_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeExtraHours",
                columns: table => new
                {
                    EmployeeExtraHoursId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    WorkDate = table.Column<DateTime>(nullable: false),
                    Motivo = table.Column<string>(nullable: true),
                    CustomerId = table.Column<long>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    QuantityHours = table.Column<decimal>(nullable: false),
                    HourlySalary = table.Column<decimal>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExtraHours", x => x.EmployeeExtraHoursId);
                    table.ForeignKey(
                        name: "FK_EmployeeExtraHours_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeExtraHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalary",
                columns: table => new
                {
                    EmployeeSalaryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEmpleado = table.Column<long>(nullable: false),
                    QtySalary = table.Column<decimal>(nullable: false),
                    IdFrequency = table.Column<long>(nullable: false),
                    DayApplication = table.Column<DateTime>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    CreatedUser = table.Column<string>(nullable: false),
                    ModifiedUser = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalary", x => x.EmployeeSalaryId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HorasExtrasBiometrico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Horas = table.Column<int>(nullable: false),
                    Minutos = table.Column<int>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasExtrasBiometrico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorasExtrasBiometrico_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorasExtrasBiometrico_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HorasExtrasBiometrico_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inasistencias",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    TipoInasistencia = table.Column<long>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inasistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inasistencias_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inasistencias_ElementoConfiguracion_TipoInasistencia",
                        column: x => x.TipoInasistencia,
                        principalTable: "ElementoConfiguracion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngresosAnuales",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Periodo = table.Column<int>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    IngresoAcumulado = table.Column<decimal>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosAnuales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosAnuales_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    SourceBranchId = table.Column<int>(nullable: false),
                    TargetBranchId = table.Column<int>(nullable: false),
                    DateGenerated = table.Column<DateTime>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    GeneratedbyEmployeeId = table.Column<long>(nullable: false),
                    ReceivedByEmployeeId = table.Column<long>(nullable: false),
                    CarriedByEmployeeId = table.Column<long>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    ReasonId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    CAI = table.Column<string>(nullable: true),
                    NumeroSAR = table.Column<string>(nullable: true),
                    Rango = table.Column<string>(nullable: true),
                    TipoDocumentoId = table.Column<long>(nullable: false),
                    NumeracionSARId = table.Column<long>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_CarriedByEmployeeId",
                        column: x => x.CarriedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_GeneratedbyEmployeeId",
                        column: x => x.GeneratedbyEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_NumeracionSAR_NumeracionSARId",
                        column: x => x.NumeracionSARId,
                        principalTable: "NumeracionSAR",
                        principalColumn: "IdNumeracion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Employees_ReceivedByEmployeeId",
                        column: x => x.ReceivedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Branch_SourceBranchId",
                        column: x => x.SourceBranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_Branch_TargetBranchId",
                        column: x => x.TargetBranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransfer_TiposDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumento",
                        principalColumn: "IdTipoDocumento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LlegadasTardeBiometrico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdBiometrico = table.Column<long>(nullable: false),
                    IdEmpleado = table.Column<long>(nullable: false),
                    Horas = table.Column<int>(nullable: false),
                    Minutos = table.Column<int>(nullable: false),
                    IdEstado = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlegadasTardeBiometrico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LlegadasTardeBiometrico_Biometricos_IdBiometrico",
                        column: x => x.IdBiometrico,
                        principalTable: "Biometricos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LlegadasTardeBiometrico_Employees_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LlegadasTardeBiometrico_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagosISR",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Periodo = table.Column<long>(nullable: false),
                    EmpleadoId = table.Column<long>(nullable: false),
                    TotalAnual = table.Column<double>(nullable: false),
                    PagoAcumulado = table.Column<double>(nullable: false),
                    Saldo = table.Column<double>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosISR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosISR_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PagosISR_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatrizRiesgoCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    ProductId = table.Column<long>(nullable: false),
                    IdFactorRiesgo = table.Column<long>(nullable: false),
                    FactorRiesgo = table.Column<string>(nullable: true),
                    Riesgo = table.Column<string>(nullable: true),
                    Efecto = table.Column<string>(nullable: true),
                    IdContextoRiesgo = table.Column<long>(nullable: false),
                    Responsable = table.Column<string>(nullable: true),
                    RiesgoInicialProbabilidad = table.Column<long>(nullable: false),
                    RiesgoInicialImpacto = table.Column<long>(nullable: false),
                    RiesgoInicialCalificacion = table.Column<long>(nullable: false),
                    RiesgoInicialValorSeveridad = table.Column<long>(nullable: false),
                    RiesgoInicialNivel = table.Column<string>(nullable: true),
                    RiesgoInicialColorHexadecimal = table.Column<string>(nullable: true),
                    Controles = table.Column<string>(nullable: true),
                    TipodeAccionderiesgoId = table.Column<int>(nullable: false),
                    FechaObjetvo = table.Column<string>(nullable: true),
                    RiesgoResidualProbabilidad = table.Column<long>(nullable: false),
                    RiesgoResidualImpacto = table.Column<long>(nullable: false),
                    RiesgoResidualCalificacion = table.Column<long>(nullable: false),
                    RiesgoResidualValorSeveridad = table.Column<long>(nullable: false),
                    RiesgoResidualNivel = table.Column<string>(nullable: true),
                    RiesgoResidualColorHexadecimal = table.Column<string>(nullable: true),
                    Seguimiento = table.Column<string>(nullable: true),
                    FechaRevision = table.Column<DateTime>(nullable: false),
                    Avance = table.Column<double>(nullable: false),
                    Eficaz = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrizRiesgoCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_ContextoRiesgo_IdContextoRiesgo",
                        column: x => x.IdContextoRiesgo,
                        principalTable: "ContextoRiesgo",
                        principalColumn: "IdContextoRiesgo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatrizRiesgoCustomers_TipodeAccionderiesgo_TipodeAccionderiesgoId",
                        column: x => x.TipodeAccionderiesgoId,
                        principalTable: "TipodeAccionderiesgo",
                        principalColumn: "TipodeAccionderiesgoId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Contrato_detalle",
                columns: table => new
                {
                    Contrato_detalleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    Monto = table.Column<double>(nullable: false),
                    Serie = table.Column<string>(maxLength: 100, nullable: true),
                    Modelo = table.Column<string>(maxLength: 100, nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_detalle", x => x.Contrato_detalleId);
                    table.ForeignKey(
                        name: "FK_Contrato_detalle_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato_movimientos",
                columns: table => new
                {
                    Contrato_movimientosId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    Fechamovimiento = table.Column<DateTime>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    tipo_movimiento = table.Column<int>(nullable: false),
                    Valorcapital = table.Column<double>(nullable: false),
                    Forma_pago = table.Column<int>(nullable: false),
                    EmployeesId = table.Column<long>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_movimientos", x => x.Contrato_movimientosId);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_movimientos_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato_plan_pagos",
                columns: table => new
                {
                    Nro_cuota = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContratoId = table.Column<long>(nullable: false),
                    Fechacuota = table.Column<DateTime>(nullable: false),
                    Valorcapital = table.Column<double>(nullable: false),
                    Valorintereses = table.Column<double>(nullable: false),
                    Valorseguros = table.Column<double>(nullable: false),
                    Interesesmoratorios = table.Column<double>(nullable: false),
                    Valorotroscargos = table.Column<double>(nullable: false),
                    Estadocuota = table.Column<short>(nullable: false),
                    Valorpagado = table.Column<double>(nullable: false),
                    Fechapago = table.Column<DateTime>(nullable: false),
                    Recibopago = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<string>(nullable: false),
                    UsuarioModificacion = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato_plan_pagos", x => new { x.Nro_cuota, x.ContratoId });
                    table.UniqueConstraint("AK_Contrato_plan_pagos_ContratoId_Nro_cuota", x => new { x.ContratoId, x.Nro_cuota });
                    table.ForeignKey(
                        name: "FK_Contrato_plan_pagos_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Restrict);
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
                    ServicioId = table.Column<long>(nullable: false),
                    ServicioName = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCertificado = table.Column<DateTime>(nullable: false),
                    NombreEmpresa = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    InsuranceId = table.Column<int>(nullable: true),
                    EmpresaSeguro = table.Column<string>(nullable: true),
                    InsurancePolicyId = table.Column<long>(nullable: true),
                    NoPoliza = table.Column<string>(nullable: true),
                    SujetasAPago = table.Column<double>(nullable: false),
                    FechaVencimientoDeposito = table.Column<DateTime>(nullable: false),
                    NoTraslado = table.Column<long>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    Aduana = table.Column<string>(nullable: true),
                    ManifiestoNo = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_CertificadoDeposito_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsurancesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoDeposito_InsurancePolicy_InsurancePolicyId",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicy",
                        principalColumn: "InsurancePolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceEndorsement",
                columns: table => new
                {
                    InsuranceEndorsementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateGenerated = table.Column<DateTime>(nullable: false),
                    InsurancePolicyId = table.Column<long>(nullable: false),
                    CostCenterId = table.Column<long>(nullable: false),
                    ProductdId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    TotalAmountLp = table.Column<decimal>(nullable: false),
                    TotalAmountDl = table.Column<decimal>(nullable: false),
                    TotalCertificateBalalnce = table.Column<decimal>(nullable: false),
                    TotalAssuredDifernce = table.Column<decimal>(nullable: false),
                    EstadoId = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceEndorsement", x => x.InsuranceEndorsementId);
                    table.ForeignKey(
                        name: "FK_InsuranceEndorsement_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceEndorsement_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceEndorsement_InsurancePolicy_InsurancePolicyId",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicy",
                        principalColumn: "InsurancePolicyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceEndorsement_Product_ProductdId",
                        column: x => x.ProductdId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineNumber = table.Column<int>(nullable: true),
                    PurchaseOrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    QtyOrdered = table.Column<decimal>(nullable: true),
                    QtyReceived = table.Column<decimal>(nullable: true),
                    QtyReceivedToDate = table.Column<decimal>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    TaxName = table.Column<string>(nullable: true),
                    TaxRate = table.Column<decimal>(nullable: false),
                    TaxId = table.Column<long>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<int>(nullable: false),
                    DiscountPercentage = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderLine_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorInvoice",
                columns: table => new
                {
                    VendorInvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorInvoiceName = table.Column<string>(nullable: true),
                    ShipmentId = table.Column<int>(nullable: false),
                    PurchaseOrderId = table.Column<int>(nullable: true),
                    VendorInvoiceDate = table.Column<DateTime>(nullable: false),
                    VendorInvoiceDueDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    VendorInvoiceTypeId = table.Column<int>(nullable: false),
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
                    VendorId = table.Column<long>(nullable: false),
                    VendorName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyName = table.Column<string>(nullable: true),
                    Currency = table.Column<decimal>(nullable: false),
                    VendorRefNumber = table.Column<string>(nullable: true),
                    SalesTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "Money", nullable: false),
                    Discount = table.Column<decimal>(type: "Money", nullable: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: false),
                    Tax18 = table.Column<decimal>(type: "Money", nullable: false),
                    Freight = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExento = table.Column<decimal>(type: "Money", nullable: false),
                    TotalExonerado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado = table.Column<decimal>(type: "Money", nullable: false),
                    TotalGravado18 = table.Column<decimal>(type: "Money", nullable: false),
                    Total = table.Column<decimal>(type: "Money", nullable: false),
                    TotalLetras = table.Column<string>(nullable: true),
                    IdEstado = table.Column<long>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    AccountId = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioCreacion = table.Column<string>(nullable: true),
                    UsuarioModificacion = table.Column<string>(nullable: true),
                    Impreso = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInvoice", x => x.VendorInvoiceId);
                    table.ForeignKey(
                        name: "FK_VendorInvoice_Accounting_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoice_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoice_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransferLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventoryTransferId = table.Column<int>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    QtyStock = table.Column<decimal>(nullable: false),
                    QtyOut = table.Column<decimal>(nullable: false),
                    QtyIn = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    UnitOfMeasureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransferLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransferLine_InventoryTransfer_InventoryTransferId",
                        column: x => x.InventoryTransferId,
                        principalTable: "InventoryTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransferLine_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransferLine_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "UnitOfMeasureId",
                        onDelete: ReferentialAction.Restrict);
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
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Merma = table.Column<decimal>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    WarehouseId = table.Column<long>(nullable: false),
                    WarehouseName = table.Column<string>(nullable: true),
                    TotalCantidad = table.Column<decimal>(nullable: false),
                    SaldoEndoso = table.Column<decimal>(nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorInvoiceLine",
                columns: table => new
                {
                    VendorInvoiceLineId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorInvoiceId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    CostCenterId = table.Column<long>(nullable: true),
                    CostCenterName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    TaxId = table.Column<long>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    AccountId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInvoiceLine", x => x.VendorInvoiceLineId);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_Accounting_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounting",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "CostCenterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_VendorInvoice_VendorInvoiceId",
                        column: x => x.VendorInvoiceId,
                        principalTable: "VendorInvoice",
                        principalColumn: "VendorInvoiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategoriasPlanillas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1L, "NOMINA" },
                    { 2L, "NOMINA CONFIDENCIAL" },
                    { 3L, "NOMINA EXTRAORDINARIA" },
                    { 4L, "OTRO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_CompanyInfoId",
                table: "Accounting",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_ParentAccountAccountId",
                table: "Accounting",
                column: "ParentAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountManagement_TypeAccountId",
                table: "AccountManagement",
                column: "TypeAccountId");

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
                name: "IX_Biometricos_IdEstado",
                table: "Biometricos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraCierreProceso_IdBitacoraCierre",
                table: "BitacoraCierreProceso",
                column: "IdBitacoraCierre");

            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_EmpleadoId",
                table: "Bonificaciones",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_EstadoId",
                table: "Bonificaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonificaciones_TipoId",
                table: "Bonificaciones",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_BranchCode",
                table: "Branch",
                column: "BranchCode",
                unique: true,
                filter: "[BranchCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CityId",
                table: "Branch",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CountryId",
                table: "Branch",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_CurrencyId",
                table: "Branch",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_IdEstado",
                table: "Branch",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_StateId",
                table: "Branch",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_InsuranceId",
                table: "CertificadoDeposito",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDeposito_InsurancePolicyId",
                table: "CertificadoDeposito",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoLine_IdCD",
                table: "CertificadoLine",
                column: "IdCD");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_AccountManagementId",
                table: "CheckAccount",
                column: "AccountManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_CheckAccountNo",
                table: "CheckAccount",
                column: "CheckAccountNo",
                unique: true,
                filter: "[CheckAccountNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccount_IdEstado",
                table: "CheckAccount",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_CheckAccountId",
                table: "CheckAccountLines",
                column: "CheckAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_IdEstado",
                table: "CheckAccountLines",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_JournalEntrId",
                table: "CheckAccountLines",
                column: "JournalEntrId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckAccountLines_RetencionId",
                table: "CheckAccountLines",
                column: "RetencionId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_BitacoraCierreContableId",
                table: "CierresAccounting",
                column: "BitacoraCierreContableId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_CompanyInfoId",
                table: "CierresAccounting",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresAccounting_ParentAccountAccountId",
                table: "CierresAccounting",
                column: "ParentAccountAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_BitacoraCierreContableId",
                table: "CierresJournal",
                column: "BitacoraCierreContableId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_GeneralLedgerHeaderId1",
                table: "CierresJournal",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournal_PartyId1",
                table: "CierresJournal",
                column: "PartyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_BitacoraCierreContableId",
                table: "CierresJournalEntryLine",
                column: "BitacoraCierreContableId");

            migrationBuilder.CreateIndex(
                name: "IX_CierresJournalEntryLine_CierresJournalEntryId",
                table: "CierresJournalEntryLine",
                column: "CierresJournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_IdEstado",
                table: "City",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionLinea_ConciliacionId",
                table: "ConciliacionLinea",
                column: "ConciliacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionVacaciones_EstadoId",
                table: "ConfiguracionVacaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_BranchId",
                table: "Contrato",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CustomerId",
                table: "Contrato",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_detalle_ContratoId",
                table: "Contrato_detalle",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_BranchId",
                table: "Contrato_movimientos",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_ContratoId",
                table: "Contrato_movimientos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_movimientos_EmployeesId",
                table: "Contrato_movimientos",
                column: "EmployeesId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlAsistencias_IdEmpleado",
                table: "ControlAsistencias",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_ControlPalletsLine_ControlPalletsId",
                table: "ControlPalletsLine",
                column: "ControlPalletsId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNoteLine_CreditNoteId",
                table: "CreditNoteLine",
                column: "CreditNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CityId",
                table: "Customer",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CountryId",
                table: "Customer",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerTypeId",
                table: "Customer",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdEstado",
                table: "Customer",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RTN",
                table: "Customer",
                column: "RTN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StateId",
                table: "Customer",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAreaProduct_CustomerAreaId",
                table: "CustomerAreaProduct",
                column: "CustomerAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContract_IdEstado",
                table: "CustomerContract",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersOfCustomer_CustomerId",
                table: "CustomersOfCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitNoteLine_DebitNoteId",
                table: "DebitNoteLine",
                column: "DebitNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesEmpleados_DeductionId",
                table: "DeduccionesEmpleados",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesEmpleados_EmpleadoId",
                table: "DeduccionesEmpleados",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeduccionesPlanilla_DetallePlanillaId",
                table: "DeduccionesPlanilla",
                column: "DetallePlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_NombreDepartamento",
                table: "Departamento",
                column: "NombreDepartamento",
                unique: true,
                filter: "[NombreDepartamento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePlanillas_PlanillaId",
                table: "DetallePlanillas",
                column: "PlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesBiometricos_IdBiometrico",
                table: "DetallesBiometricos",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesBiometricos_IdEmpleado",
                table: "DetallesBiometricos",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_AccountId",
                table: "Doc_CP",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_CurrencyId",
                table: "Doc_CP",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_DocTipoId",
                table: "Doc_CP",
                column: "DocTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_PaymentTypeId",
                table: "Doc_CP",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doc_CP_TaxId",
                table: "Doc_CP",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementoConfiguracion_Idconfiguracion",
                table: "ElementoConfiguracion",
                column: "Idconfiguracion");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_EmpleadoId",
                table: "EmpleadoHorarios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_HorarioId",
                table: "EmpleadoHorarios",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoHorarios_IdEstado",
                table: "EmpleadoHorarios",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHours_CustomerId",
                table: "EmployeeExtraHours",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExtraHours_EmployeeId",
                table: "EmployeeExtraHours",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBank",
                table: "Employees",
                column: "IdBank");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdBranch",
                table: "Employees",
                column: "IdBranch");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCity",
                table: "Employees",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCountry",
                table: "Employees",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdCurrency",
                table: "Employees",
                column: "IdCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdDepartamento",
                table: "Employees",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdEstado",
                table: "Employees",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdPuesto",
                table: "Employees",
                column: "IdPuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdState",
                table: "Employees",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdTipoContrato",
                table: "Employees",
                column: "IdTipoContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdTipoPlanilla",
                table: "Employees",
                column: "IdTipoPlanilla");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_IdEmpleado",
                table: "EmployeeSalary",
                column: "IdEmpleado");

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
                name: "IX_ExchangeRate_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_IdEstado",
                table: "Feriados",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_DepreciationAccountingId",
                table: "FixedAssetGroup",
                column: "DepreciationAccountingId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_FixedAssetAccountingId",
                table: "FixedAssetGroup",
                column: "FixedAssetAccountingId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedAssetGroup_FixedAssetGroupId1",
                table: "FixedAssetGroup",
                column: "FixedAssetGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_CostCenterId",
                table: "GarantiaBancaria",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_CurrencyId",
                table: "GarantiaBancaria",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GarantiaBancaria_IdEstado",
                table: "GarantiaBancaria",
                column: "IdEstado");

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
                name: "IX_Horarios_IdEstado",
                table: "Horarios",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdBiometrico",
                table: "HorasExtrasBiometrico",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdEmpleado",
                table: "HorasExtrasBiometrico",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_HorasExtrasBiometrico_IdEstado",
                table: "HorasExtrasBiometrico",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_HoursWorkedDetail_IdHorasTrabajadas",
                table: "HoursWorkedDetail",
                column: "IdHorasTrabajadas");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_IdEmpleado",
                table: "Inasistencias",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_IdEstado",
                table: "Inasistencias",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Inasistencias_TipoInasistencia",
                table: "Inasistencias",
                column: "TipoInasistencia");

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
                name: "IX_IngresosAnuales_EmpleadoId",
                table: "IngresosAnuales",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsement_CostCenterId",
                table: "InsuranceEndorsement",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsement_EstadoId",
                table: "InsuranceEndorsement",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsement_InsurancePolicyId",
                table: "InsuranceEndorsement",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceEndorsement_ProductdId",
                table: "InsuranceEndorsement",
                column: "ProductdId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicy_CustomerId",
                table: "InsurancePolicy",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicy_EstadoId",
                table: "InsurancePolicy",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_EstadoId",
                table: "Insurances",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_CarriedByEmployeeId",
                table: "InventoryTransfer",
                column: "CarriedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_CurrencyId",
                table: "InventoryTransfer",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_EstadoId",
                table: "InventoryTransfer",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_GeneratedbyEmployeeId",
                table: "InventoryTransfer",
                column: "GeneratedbyEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_NumeracionSARId",
                table: "InventoryTransfer",
                column: "NumeracionSARId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_ReceivedByEmployeeId",
                table: "InventoryTransfer",
                column: "ReceivedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_SourceBranchId",
                table: "InventoryTransfer",
                column: "SourceBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TargetBranchId",
                table: "InventoryTransfer",
                column: "TargetBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransfer_TipoDocumentoId",
                table: "InventoryTransfer",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_InventoryTransferId",
                table: "InventoryTransferLine",
                column: "InventoryTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_ProductId",
                table: "InventoryTransferLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransferLine_UnitOfMeasureId",
                table: "InventoryTransferLine",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_GeneralLedgerHeaderId1",
                table: "JournalEntry",
                column: "GeneralLedgerHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntry_PartyId1",
                table: "JournalEntry",
                column: "PartyId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryConfigurationLine_JournalEntryConfigurationId",
                table: "JournalEntryConfigurationLine",
                column: "JournalEntryConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_AccountId1",
                table: "JournalEntryLine",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLine_JournalEntryId",
                table: "JournalEntryLine",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_KardexLine_KardexId",
                table: "KardexLine",
                column: "KardexId");

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdBiometrico",
                table: "LlegadasTardeBiometrico",
                column: "IdBiometrico");

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdEmpleado",
                table: "LlegadasTardeBiometrico",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_LlegadasTardeBiometrico_IdEstado",
                table: "LlegadasTardeBiometrico",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_CustomerId",
                table: "MatrizRiesgoCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_IdContextoRiesgo",
                table: "MatrizRiesgoCustomers",
                column: "IdContextoRiesgo");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_ProductId",
                table: "MatrizRiesgoCustomers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizRiesgoCustomers_TipodeAccionderiesgoId",
                table: "MatrizRiesgoCustomers",
                column: "TipodeAccionderiesgoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosISR_EmpleadoId",
                table: "PagosISR",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagosISR_EstadoId",
                table: "PagosISR",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHistory_UserId1",
                table: "PasswordHistory",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_AccountingAccountId",
                table: "PaymentTerms",
                column: "AccountingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_PaymentTypesId",
                table: "PaymentTerms",
                column: "PaymentTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Periodo_IdEstado",
                table: "Periodo",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLines_IdBranch",
                table: "PhoneLines",
                column: "IdBranch");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_EstadoId",
                table: "Planillas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Planillas_TipoPlanillaId",
                table: "Planillas",
                column: "TipoPlanillaId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_IdPolicy",
                table: "PolicyRoles",
                column: "IdPolicy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_IdRol",
                table: "PolicyRoles",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_PrecioCafe_ExchangeRateId",
                table: "PrecioCafe",
                column: "ExchangeRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_AccountigId",
                table: "Presupuesto",
                column: "AccountigId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_CostCenterId",
                table: "Presupuesto",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_PeriodoId",
                table: "Presupuesto",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BranchId",
                table: "Product",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CurrencyId",
                table: "Product",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FundingInterestRateId",
                table: "Product",
                column: "FundingInterestRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCode",
                table: "Product",
                column: "ProductCode",
                unique: true,
                filter: "[ProductCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

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
                name: "IX_PurchaseOrder_BranchId",
                table: "PurchaseOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CurrencyId",
                table: "PurchaseOrder",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_EstadoId",
                table: "PurchaseOrder",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ShippingTypeId",
                table: "PurchaseOrder",
                column: "ShippingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_TaxId",
                table: "PurchaseOrder",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_ProductId",
                table: "PurchaseOrderLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_PurchaseOrderId",
                table: "PurchaseOrderLine",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_TaxId",
                table: "PurchaseOrderLine",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLine_UnitOfMeasureId",
                table: "PurchaseOrderLine",
                column: "UnitOfMeasureId");

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
                name: "IX_TipoPlanillas_CategoriaId",
                table: "TipoPlanillas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPlanillas_EstadoId",
                table: "TipoPlanillas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposBonificaciones_EstadoId",
                table: "TiposBonificaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_UnitOfMeasureName",
                table: "UnitOfMeasure",
                column: "UnitOfMeasureName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_UserId",
                table: "UserBranch",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_BranchId_UserId",
                table: "UserBranch",
                columns: new[] { "BranchId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CityId",
                table: "Vendor",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CountryId",
                table: "Vendor",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CurrencyId",
                table: "Vendor",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_IdEstado",
                table: "Vendor",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StateId",
                table: "Vendor",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorTypeId",
                table: "Vendor",
                column: "VendorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_AccountId",
                table: "VendorInvoice",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_PurchaseOrderId",
                table: "VendorInvoice",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_VendorId",
                table: "VendorInvoice",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_AccountId",
                table: "VendorInvoiceLine",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_CostCenterId",
                table: "VendorInvoiceLine",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_TaxId",
                table: "VendorInvoiceLine",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_VendorInvoiceId",
                table: "VendorInvoiceLine",
                column: "VendorInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorOfCustomer_CustomerId",
                table: "VendorOfCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProduct_ProductId_VendorId",
                table: "VendorProduct",
                columns: new[] { "ProductId", "VendorId" },
                unique: true);
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
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "BitacoraCierreProceso");

            migrationBuilder.DropTable(
                name: "BlackListCustomers");

            migrationBuilder.DropTable(
                name: "BoletaDeSalida");

            migrationBuilder.DropTable(
                name: "Boleto_Sal");

            migrationBuilder.DropTable(
                name: "Bonificaciones");

            migrationBuilder.DropTable(
                name: "BranchPorDepartamento");

            migrationBuilder.DropTable(
                name: "CAI");

            migrationBuilder.DropTable(
                name: "CalculoPlanilla");

            migrationBuilder.DropTable(
                name: "CalculoPlanillaDetalle");

            migrationBuilder.DropTable(
                name: "CertificadoLine");

            migrationBuilder.DropTable(
                name: "CheckAccountLines");

            migrationBuilder.DropTable(
                name: "CierresAccounting");

            migrationBuilder.DropTable(
                name: "CierresJournalEntryLine");

            migrationBuilder.DropTable(
                name: "Comision");

            migrationBuilder.DropTable(
                name: "Concept");

            migrationBuilder.DropTable(
                name: "ConciliacionLinea");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "ConfiguracionVacaciones");

            migrationBuilder.DropTable(
                name: "ConfigurationVendor");

            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.DropTable(
                name: "Contrato_detalle");

            migrationBuilder.DropTable(
                name: "Contrato_movimientos");

            migrationBuilder.DropTable(
                name: "Contrato_plan_pagos");

            migrationBuilder.DropTable(
                name: "ControlAsistencias");

            migrationBuilder.DropTable(
                name: "ControlPalletsLine");

            migrationBuilder.DropTable(
                name: "CostListItem");

            migrationBuilder.DropTable(
                name: "CreditNoteLine");

            migrationBuilder.DropTable(
                name: "CuentaBancoEmpleados");

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
                name: "DebitNoteLine");

            migrationBuilder.DropTable(
                name: "DeduccionesEmpleados");

            migrationBuilder.DropTable(
                name: "DeduccionesPlanilla");

            migrationBuilder.DropTable(
                name: "Dependientes");

            migrationBuilder.DropTable(
                name: "DepreciationFixedAsset");

            migrationBuilder.DropTable(
                name: "DESIGNATIONM");

            migrationBuilder.DropTable(
                name: "DetallesBiometricos");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "Doc_CP");

            migrationBuilder.DropTable(
                name: "EmpleadoHorarios");

            migrationBuilder.DropTable(
                name: "EmpleadosBiometrico");

            migrationBuilder.DropTable(
                name: "EmployeeAbsence");

            migrationBuilder.DropTable(
                name: "EmployeeDocument");

            migrationBuilder.DropTable(
                name: "EmployeeExtraHours");

            migrationBuilder.DropTable(
                name: "EmployeeExtraHoursDetail");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

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
                name: "Feriados");

            migrationBuilder.DropTable(
                name: "FixedAsset");

            migrationBuilder.DropTable(
                name: "FixedAssetGroup");

            migrationBuilder.DropTable(
                name: "Formula");

            migrationBuilder.DropTable(
                name: "FormulasAplicadas");

            migrationBuilder.DropTable(
                name: "FormulasConcepto");

            migrationBuilder.DropTable(
                name: "FormulasConFormulas");

            migrationBuilder.DropTable(
                name: "GarantiaBancaria");

            migrationBuilder.DropTable(
                name: "GoodsDeliveredLine");

            migrationBuilder.DropTable(
                name: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropTable(
                name: "GoodsReceivedLine");

            migrationBuilder.DropTable(
                name: "GrupoEstado");

            migrationBuilder.DropTable(
                name: "HorasExtrasBiometrico");

            migrationBuilder.DropTable(
                name: "HoursWorkedDetail");

            migrationBuilder.DropTable(
                name: "ImpuestoVecinalConfiguraciones");

            migrationBuilder.DropTable(
                name: "Inasistencias");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "IncomeAndExpenseAccountLine");

            migrationBuilder.DropTable(
                name: "IncomeAndExpensesAccount");

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
                name: "IngresosAnuales");

            migrationBuilder.DropTable(
                name: "InstallmentDelivery");

            migrationBuilder.DropTable(
                name: "InsuranceEndorsement");

            migrationBuilder.DropTable(
                name: "InventoryTransferLine");

            migrationBuilder.DropTable(
                name: "InvoiceCalculation");

            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "InvoiceTransReport");

            migrationBuilder.DropTable(
                name: "ISRConfiguracion");

            migrationBuilder.DropTable(
                name: "JournalEntryCanceled");

            migrationBuilder.DropTable(
                name: "JournalEntryConfigurationLine");

            migrationBuilder.DropTable(
                name: "JournalEntryLine");

            migrationBuilder.DropTable(
                name: "KardexLine");

            migrationBuilder.DropTable(
                name: "LAST_DAY_UPDATEDM");

            migrationBuilder.DropTable(
                name: "LlegadasTardeBiometrico");

            migrationBuilder.DropTable(
                name: "MantenimientoImpacto");

            migrationBuilder.DropTable(
                name: "MatrizRiesgoCustomers");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "MotivoConciliacion");

            migrationBuilder.DropTable(
                name: "NATIONALITYM");

            migrationBuilder.DropTable(
                name: "OrdenFormula");

            migrationBuilder.DropTable(
                name: "PagosISR");

            migrationBuilder.DropTable(
                name: "PasswordHistory");

            migrationBuilder.DropTable(
                name: "PaymentScheduleRulesByCustomer");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "PayrollDeduction");

            migrationBuilder.DropTable(
                name: "PayrollEmployee");

            migrationBuilder.DropTable(
                name: "PEPS");

            migrationBuilder.DropTable(
                name: "PeriodicidadPago");

            migrationBuilder.DropTable(
                name: "PhoneLines");

            migrationBuilder.DropTable(
                name: "PolicyClaims");

            migrationBuilder.DropTable(
                name: "PolicyRoles");

            migrationBuilder.DropTable(
                name: "PrecioCafe");

            migrationBuilder.DropTable(
                name: "Presupuesto");

            migrationBuilder.DropTable(
                name: "ProbabilidadRiesgo");

            migrationBuilder.DropTable(
                name: "ProductRelation");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ProductUserRelation");

            migrationBuilder.DropTable(
                name: "ProformaInvoiceLine");

            migrationBuilder.DropTable(
                name: "PuntoEmision");

            migrationBuilder.DropTable(
                name: "PurchaseOrderLine");

            migrationBuilder.DropTable(
                name: "RecibosCertificado");

            migrationBuilder.DropTable(
                name: "RecipeDetail");

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
                name: "ScheduleSubservices");

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
                name: "SeveridadRiesgo");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "ShipmentType");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoLine");

            migrationBuilder.DropTable(
                name: "SubServicesWareHouse");

            migrationBuilder.DropTable(
                name: "Substratum");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "TITLEM");

            migrationBuilder.DropTable(
                name: "TypeAccount");

            migrationBuilder.DropTable(
                name: "TypeJournal");

            migrationBuilder.DropTable(
                name: "UserBranch");

            migrationBuilder.DropTable(
                name: "VendorDocument");

            migrationBuilder.DropTable(
                name: "VendorInvoiceLine");

            migrationBuilder.DropTable(
                name: "VendorOfCustomer");

            migrationBuilder.DropTable(
                name: "VendorProduct");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Boleto_Ent");

            migrationBuilder.DropTable(
                name: "TiposBonificaciones");

            migrationBuilder.DropTable(
                name: "CertificadoDeposito");

            migrationBuilder.DropTable(
                name: "CheckAccount");

            migrationBuilder.DropTable(
                name: "RetentionReceipt");

            migrationBuilder.DropTable(
                name: "CierresJournal");

            migrationBuilder.DropTable(
                name: "Conciliacion");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "ControlPallets");

            migrationBuilder.DropTable(
                name: "CreditNote");

            migrationBuilder.DropTable(
                name: "CustomerArea");

            migrationBuilder.DropTable(
                name: "DebitNote");

            migrationBuilder.DropTable(
                name: "Deduction");

            migrationBuilder.DropTable(
                name: "DetallePlanillas");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "EndososBono");

            migrationBuilder.DropTable(
                name: "EndososCertificados");

            migrationBuilder.DropTable(
                name: "EndososTalon");

            migrationBuilder.DropTable(
                name: "ENTITYM");

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
                name: "InventoryTransfer");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "JournalEntryConfiguration");

            migrationBuilder.DropTable(
                name: "JournalEntry");

            migrationBuilder.DropTable(
                name: "Kardex");

            migrationBuilder.DropTable(
                name: "Biometricos");

            migrationBuilder.DropTable(
                name: "ContextoRiesgo");

            migrationBuilder.DropTable(
                name: "TipodeAccionderiesgo");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ExchangeRate");

            migrationBuilder.DropTable(
                name: "Periodo");

            migrationBuilder.DropTable(
                name: "SubProduct");

            migrationBuilder.DropTable(
                name: "ProformaInvoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntry");

            migrationBuilder.DropTable(
                name: "SolicitudCertificadoDeposito");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "VendorInvoice");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "InsurancePolicy");

            migrationBuilder.DropTable(
                name: "AccountManagement");

            migrationBuilder.DropTable(
                name: "BitacoraCierreContable");

            migrationBuilder.DropTable(
                name: "Planillas");

            migrationBuilder.DropTable(
                name: "ENTITIESM");

            migrationBuilder.DropTable(
                name: "CONSOLIDATED_LISTM");

            migrationBuilder.DropTable(
                name: "INDIVIDUALSM");

            migrationBuilder.DropTable(
                name: "LIST_TYPEM");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "NumeracionSAR");

            migrationBuilder.DropTable(
                name: "TiposDocumento");

            migrationBuilder.DropTable(
                name: "GeneralLedgerHeader");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "FundingInterestRate");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropTable(
                name: "sdnList");

            migrationBuilder.DropTable(
                name: "sdnListSdnEntryVesselInfo");

            migrationBuilder.DropTable(
                name: "Accounting");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Puesto");

            migrationBuilder.DropTable(
                name: "TipoContrato");

            migrationBuilder.DropTable(
                name: "TipoPlanillas");

            migrationBuilder.DropTable(
                name: "sdnListPublshInformation");

            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "ElementoConfiguracion");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "CategoriasPlanillas");

            migrationBuilder.DropTable(
                name: "GrupoConfiguracion");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "VendorType");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
