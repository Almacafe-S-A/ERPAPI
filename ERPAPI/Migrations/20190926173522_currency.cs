using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate");

            //migrationBuilder.RenameColumn(
            //    name: "Telefono",
            //    table: "Dependientes",
            //    newName: "TelefonoDependientes");

            //migrationBuilder.RenameColumn(
            //    name: "Direccion",
            //    table: "Dependientes",
            //    newName: "DireccionDependientes");

            migrationBuilder.AddColumn<string>(
                name: "WareHouseName",
                table: "ProformaInvoiceLine",
                nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "DebitCreditId",
            //    table: "JournalEntryConfigurationLine",
            //    nullable: false,
            //    defaultValue: 0L);

            //migrationBuilder.AlterColumn<int>(
            //    name: "CurrencyId",
            //    table: "ExchangeRate",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "FechaNacimiento",
            //    table: "Dependientes",
            //    nullable: true,
            //    oldClrType: typeof(DateTime));

            //migrationBuilder.AlterColumn<int>(
            //    name: "Edad",
            //    table: "Dependientes",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddColumn<bool>(
            //    name: "AplicaComision",
            //    table: "Departamento",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "ComisionId",
            //    table: "Departamento",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "PeridicidadId",
            //    table: "Departamento",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "BranchPorDepartamento",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        BranchId = table.Column<int>(nullable: false),
            //        BranchName = table.Column<string>(nullable: true),
            //        IdDepartamento = table.Column<long>(nullable: false),
            //        NombreDepartamento = table.Column<string>(nullable: true),
            //        UsuarioCreacion = table.Column<string>(nullable: true),
            //        UsuarioModificacion = table.Column<string>(nullable: true),
            //        FechaCreacion = table.Column<DateTime>(nullable: false),
            //        FechaModificacion = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BranchPorDepartamento", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Comision",
            //    columns: table => new
            //    {
            //        ComisionId = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        TipoComision = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        EstadoId = table.Column<long>(nullable: false),
            //        FechaCreacion = table.Column<DateTime>(nullable: false),
            //        FechaModificacion = table.Column<DateTime>(nullable: false),
            //        UsuarioModificacion = table.Column<string>(nullable: true),
            //        UsuarioCreacion = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comision", x => x.ComisionId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Conciliacion",
            //    columns: table => new
            //    {
            //        ConciliacionId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        IdBanco = table.Column<long>(nullable: true),
            //        BankName = table.Column<string>(nullable: false),
            //        FechaConciliacion = table.Column<DateTime>(nullable: false),
            //        SaldoConciliado = table.Column<double>(nullable: false),
            //        NombreArchivo = table.Column<string>(nullable: false),
            //        FechaCreacion = table.Column<DateTime>(nullable: false),
            //        FechaModificacion = table.Column<DateTime>(nullable: false),
            //        UsuarioCreacion = table.Column<string>(nullable: false),
            //        UsuarioModificacion = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Conciliacion", x => x.ConciliacionId);
            //        table.ForeignKey(
            //            name: "FK_Conciliacion_Bank_IdBanco",
            //            column: x => x.IdBanco,
            //            principalTable: "Bank",
            //            principalColumn: "BankId",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ConciliacionLinea",
            //    columns: table => new
            //    {
            //        ConciliacionLineaId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ElementoConfiguracion = table.Column<long>(nullable: true),
            //        Monto = table.Column<double>(nullable: false),
            //        ReferenciaBancaria = table.Column<string>(nullable: false),
            //        IdMoneda = table.Column<int>(nullable: true),
            //        MonedaName = table.Column<string>(nullable: false),
            //        FechaCreacion = table.Column<DateTime>(nullable: false),
            //        FechaModificacion = table.Column<DateTime>(nullable: false),
            //        UsuarioCreacion = table.Column<string>(nullable: false),
            //        UsuarioModificacion = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ConciliacionLinea", x => x.ConciliacionLineaId);
            //        table.ForeignKey(
            //            name: "FK_ConciliacionLinea_ElementoConfiguracion_ElementoConfiguracion",
            //            column: x => x.ElementoConfiguracion,
            //            principalTable: "ElementoConfiguracion",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ConciliacionLinea_Currency_IdMoneda",
            //            column: x => x.IdMoneda,
            //            principalTable: "Currency",
            //            principalColumn: "CurrencyId",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PeriodicidadPago",
            //    columns: table => new
            //    {
            //        PeriodicidadId = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Nombre = table.Column<string>(nullable: true),
            //        Description = table.Column<string>(nullable: true),
            //        EstadoId = table.Column<long>(nullable: false),
            //        FechaCreacion = table.Column<DateTime>(nullable: false),
            //        FechaModificacion = table.Column<DateTime>(nullable: false),
            //        UsuarioModificacion = table.Column<string>(nullable: true),
            //        UsuarioCreacion = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PeriodicidadPago", x => x.PeriodicidadId);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Conciliacion_IdBanco",
            //    table: "Conciliacion",
            //    column: "IdBanco");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ConciliacionLinea_ElementoConfiguracion",
            //    table: "ConciliacionLinea",
            //    column: "ElementoConfiguracion");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ConciliacionLinea_IdMoneda",
            //    table: "ConciliacionLinea",
            //    column: "IdMoneda");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate");

            migrationBuilder.DropTable(
                name: "BranchPorDepartamento");

            migrationBuilder.DropTable(
                name: "Comision");

            migrationBuilder.DropTable(
                name: "Conciliacion");

            migrationBuilder.DropTable(
                name: "ConciliacionLinea");

            migrationBuilder.DropTable(
                name: "PeriodicidadPago");

            migrationBuilder.DropColumn(
                name: "WareHouseName",
                table: "ProformaInvoiceLine");

            migrationBuilder.DropColumn(
                name: "DebitCreditId",
                table: "JournalEntryConfigurationLine");

            migrationBuilder.DropColumn(
                name: "AplicaComision",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "ComisionId",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "PeridicidadId",
                table: "Departamento");

            migrationBuilder.RenameColumn(
                name: "TelefonoDependientes",
                table: "Dependientes",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "DireccionDependientes",
                table: "Dependientes",
                newName: "Direccion");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "ExchangeRate",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Dependientes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Edad",
                table: "Dependientes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRate_Currency_CurrencyId",
                table: "ExchangeRate",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
