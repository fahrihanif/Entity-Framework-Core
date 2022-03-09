using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changerelationprofilingaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_tb_tr_profiling_NIK",
                table: "tb_tr_account");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_tr_account_NIK",
                table: "tb_tr_profiling",
                column: "NIK",
                principalTable: "tb_tr_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_tr_account_NIK",
                table: "tb_tr_profiling");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_tb_tr_profiling_NIK",
                table: "tb_tr_account",
                column: "NIK",
                principalTable: "tb_tr_profiling",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
