using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPAPI.Migrations
{
    public partial class AddElementoConfiguracion_TiempoBloqueoUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ElementoConfiguracion(Nombre, Descripcion, IdEstado, Estado, Idconfiguracion, Valordecimal, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion) VALUES('TiempoBloqueoUsuarios', 'Tiempo de Bloqueo en minutos de usuario por intentos fallidos', 1, 'Activo', 8, 5, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'erp@bi-dss.com', 'erp@bi-dss.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ElementoConfiguracion WHERE Nombre = 'TiempoBloqueoUsuarios' AND Idconfiguracion = 8");
        }
    }
}
