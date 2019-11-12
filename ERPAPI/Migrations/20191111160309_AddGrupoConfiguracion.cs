using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddGrupoConfiguracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("INSERT INTO GrupoConfiguracion(Nombreconfiguracion, IdZona, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES('Correo Principal', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'erp@bi-dss.com', 'erp@bi-dss.com'), ('Correo Secundario', 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("DELETE FROM GrupoConfiguracion WHERE Nombreconfiguracion IN ('Correo Principal', 'Correo Secundario')");
        }
    }
}
