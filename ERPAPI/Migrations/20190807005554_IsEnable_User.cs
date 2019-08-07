using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class IsEnable_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdCD",
            //    table: "SolicitudCertificadoLine");

            //migrationBuilder.RenameColumn(
            //    name: "IdCD",
            //    table: "SolicitudCertificadoLine",
            //    newName: "IdSCD");

            //migrationBuilder.RenameIndex(
            //    name: "IX_SolicitudCertificadoLine_IdCD",
            //    table: "SolicitudCertificadoLine",
            //    newName: "IX_SolicitudCertificadoLine_IdSCD");

            //migrationBuilder.RenameColumn(
            //    name: "IdCD",
            //    table: "SolicitudCertificadoDeposito",
            //    newName: "IdSCD");

            //migrationBuilder.AddColumn<double>(
            //    name: "Price",
            //    table: "GoodsDeliveryAuthorizationLine",
            //    nullable: false,
            //    defaultValue: 0.0);

            //migrationBuilder.AddColumn<double>(
            //    name: "ValorImpuestos",
            //    table: "GoodsDeliveryAuthorizationLine",
            //    nullable: false,
            //    defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "AspNetUsers",
                nullable: true);

          //  migrationBuilder.CreateIndex(
                //name: "IX_Dimensions_Num_DimCode",
                //table: "Dimensions",
                //columns: new[] { "Num", "DimCode" },
                //unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
            //    table: "SolicitudCertificadoLine",
            //    column: "IdSCD",
            //    principalTable: "SolicitudCertificadoDeposito",
            //    principalColumn: "IdSCD",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdSCD",
                table: "SolicitudCertificadoLine");

            migrationBuilder.DropIndex(
                name: "IX_Dimensions_Num_DimCode",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "ValorImpuestos",
                table: "GoodsDeliveryAuthorizationLine");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IdSCD",
                table: "SolicitudCertificadoLine",
                newName: "IdCD");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitudCertificadoLine_IdSCD",
                table: "SolicitudCertificadoLine",
                newName: "IX_SolicitudCertificadoLine_IdCD");

            migrationBuilder.RenameColumn(
                name: "IdSCD",
                table: "SolicitudCertificadoDeposito",
                newName: "IdCD");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudCertificadoLine_SolicitudCertificadoDeposito_IdCD",
                table: "SolicitudCertificadoLine",
                column: "IdCD",
                principalTable: "SolicitudCertificadoDeposito",
                principalColumn: "IdCD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
