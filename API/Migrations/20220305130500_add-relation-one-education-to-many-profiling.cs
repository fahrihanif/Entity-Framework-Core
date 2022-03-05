using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrelationoneeducationtomanyprofiling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profiling_EducationId",
                table: "tb_tr_profiling",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_EducationId",
                table: "tb_tr_profiling",
                column: "EducationId",
                principalTable: "tb_m_education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_profiling_tb_m_education_EducationId",
                table: "tb_tr_profiling");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_profiling_EducationId",
                table: "tb_tr_profiling");
        }
    }
}
