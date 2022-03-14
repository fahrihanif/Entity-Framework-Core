using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmodelandrelationmanyaccountswithmanyroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account_role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_role_tb_m_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_role_tb_tr_account_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "tb_tr_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_role_AccountNIK",
                table: "tb_tr_account_role",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_role_RoleId",
                table: "tb_tr_account_role",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_account_role");

            migrationBuilder.DropTable(
                name: "tb_m_roles");
        }
    }
}
