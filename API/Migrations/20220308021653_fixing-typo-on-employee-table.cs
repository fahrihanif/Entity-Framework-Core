using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fixingtypoonemployeetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrithDate",
                table: "tb_m_employee",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "tb_m_employee",
                newName: "BrithDate");
        }
    }
}
