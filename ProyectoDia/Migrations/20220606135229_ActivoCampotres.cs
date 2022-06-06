using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoDia.Migrations
{
    public partial class ActivoCampotres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "VisitaMedica",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "VisitaMedica");
        }
    }
}
