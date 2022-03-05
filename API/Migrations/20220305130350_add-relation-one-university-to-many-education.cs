using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrelationoneuniversitytomanyeducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tb_m_education_UniversityId",
                table: "tb_m_education",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_education_tb_m_university_UniversityId",
                table: "tb_m_education",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_education_tb_m_university_UniversityId",
                table: "tb_m_education");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_education_UniversityId",
                table: "tb_m_education");
        }
    }
}
