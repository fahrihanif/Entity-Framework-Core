using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changeondeletecascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_tr_account_AccountNIK",
                table: "tb_tr_account_role");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_tr_account_AccountNIK",
                table: "tb_tr_account_role",
                column: "AccountNIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_role_tb_tr_account_AccountNIK",
                table: "tb_tr_account_role");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_role_tb_tr_account_AccountNIK",
                table: "tb_tr_account_role",
                column: "AccountNIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
