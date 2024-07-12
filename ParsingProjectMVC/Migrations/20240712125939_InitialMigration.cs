using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParsingProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sahalar",
                columns: table => new
                {
                    SahaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SahaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sahalar", x => x.SahaId);
                });

            migrationBuilder.CreateTable(
                name: "KuyuGruplari",
                columns: table => new
                {
                    KuyuGrubuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KuyuGrubuAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SahaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KuyuGruplari", x => x.KuyuGrubuId);
                    table.ForeignKey(
                        name: "FK_KuyuGruplari_Sahalar_SahaId",
                        column: x => x.SahaId,
                        principalTable: "Sahalar",
                        principalColumn: "SahaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kuyular",
                columns: table => new
                {
                    KuyuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KuyuAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enlem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boylam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KuyuGrubuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kuyular", x => x.KuyuId);
                    table.ForeignKey(
                        name: "FK_Kuyular_KuyuGruplari_KuyuGrubuId",
                        column: x => x.KuyuGrubuId,
                        principalTable: "KuyuGruplari",
                        principalColumn: "KuyuGrubuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wellborelar",
                columns: table => new
                {
                    WellboreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WellboreAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Derinlik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KuyuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wellborelar", x => x.WellboreId);
                    table.ForeignKey(
                        name: "FK_Wellborelar_Kuyular_KuyuId",
                        column: x => x.KuyuId,
                        principalTable: "Kuyular",
                        principalColumn: "KuyuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KuyuGruplari_SahaId",
                table: "KuyuGruplari",
                column: "SahaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kuyular_KuyuGrubuId",
                table: "Kuyular",
                column: "KuyuGrubuId");

            migrationBuilder.CreateIndex(
                name: "IX_Wellborelar_KuyuId",
                table: "Wellborelar",
                column: "KuyuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wellborelar");

            migrationBuilder.DropTable(
                name: "Kuyular");

            migrationBuilder.DropTable(
                name: "KuyuGruplari");

            migrationBuilder.DropTable(
                name: "Sahalar");
        }
    }
}
