using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoDia.Migrations
{
    public partial class ActivoCampodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Medico",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Medico");
        }
    }
}
