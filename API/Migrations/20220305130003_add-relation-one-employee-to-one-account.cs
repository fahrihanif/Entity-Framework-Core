using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrelationoneemployeetooneaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_tb_m_employee_NIK",
                table: "tb_tr_account",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_tb_m_employee_NIK",
                table: "tb_tr_account");
        }
    }
}
