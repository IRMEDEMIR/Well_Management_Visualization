using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParsingProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class IsimDegisiklikleri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KuyuGruplari_Sahalar_SahaId",
                table: "KuyuGruplari");

            migrationBuilder.DropForeignKey(
                name: "FK_Kuyular_KuyuGruplari_KuyuGrubuId",
                table: "Kuyular");

            migrationBuilder.DropForeignKey(
                name: "FK_Wellborelar_Kuyular_KuyuId",
                table: "Wellborelar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wellborelar",
                table: "Wellborelar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sahalar",
                table: "Sahalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kuyular",
                table: "Kuyular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KuyuGruplari",
                table: "KuyuGruplari");

            migrationBuilder.RenameTable(
                name: "Wellborelar",
                newName: "Wellbore");

            migrationBuilder.RenameTable(
                name: "Sahalar",
                newName: "Saha");

            migrationBuilder.RenameTable(
                name: "Kuyular",
                newName: "Kuyu");

            migrationBuilder.RenameTable(
                name: "KuyuGruplari",
                newName: "KuyuGrubu");

            migrationBuilder.RenameColumn(
                name: "WellboreId",
                table: "Wellbore",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Wellborelar_KuyuId",
                table: "Wellbore",
                newName: "IX_Wellbore_KuyuId");

            migrationBuilder.RenameColumn(
                name: "SahaId",
                table: "Saha",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "KuyuId",
                table: "Kuyu",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Kuyular_KuyuGrubuId",
                table: "Kuyu",
                newName: "IX_Kuyu_KuyuGrubuId");

            migrationBuilder.RenameColumn(
                name: "KuyuGrubuId",
                table: "KuyuGrubu",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_KuyuGruplari_SahaId",
                table: "KuyuGrubu",
                newName: "IX_KuyuGrubu_SahaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wellbore",
                table: "Wellbore",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Saha",
                table: "Saha",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kuyu",
                table: "Kuyu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KuyuGrubu",
                table: "KuyuGrubu",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kuyu_KuyuGrubu_KuyuGrubuId",
                table: "Kuyu",
                column: "KuyuGrubuId",
                principalTable: "KuyuGrubu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KuyuGrubu_Saha_SahaId",
                table: "KuyuGrubu",
                column: "SahaId",
                principalTable: "Saha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wellbore_Kuyu_KuyuId",
                table: "Wellbore",
                column: "KuyuId",
                principalTable: "Kuyu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kuyu_KuyuGrubu_KuyuGrubuId",
                table: "Kuyu");

            migrationBuilder.DropForeignKey(
                name: "FK_KuyuGrubu_Saha_SahaId",
                table: "KuyuGrubu");

            migrationBuilder.DropForeignKey(
                name: "FK_Wellbore_Kuyu_KuyuId",
                table: "Wellbore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wellbore",
                table: "Wellbore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Saha",
                table: "Saha");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KuyuGrubu",
                table: "KuyuGrubu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kuyu",
                table: "Kuyu");

            migrationBuilder.RenameTable(
                name: "Wellbore",
                newName: "Wellborelar");

            migrationBuilder.RenameTable(
                name: "Saha",
                newName: "Sahalar");

            migrationBuilder.RenameTable(
                name: "KuyuGrubu",
                newName: "KuyuGruplari");

            migrationBuilder.RenameTable(
                name: "Kuyu",
                newName: "Kuyular");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Wellborelar",
                newName: "WellboreId");

            migrationBuilder.RenameIndex(
                name: "IX_Wellbore_KuyuId",
                table: "Wellborelar",
                newName: "IX_Wellborelar_KuyuId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sahalar",
                newName: "SahaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "KuyuGruplari",
                newName: "KuyuGrubuId");

            migrationBuilder.RenameIndex(
                name: "IX_KuyuGrubu_SahaId",
                table: "KuyuGruplari",
                newName: "IX_KuyuGruplari_SahaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kuyular",
                newName: "KuyuId");

            migrationBuilder.RenameIndex(
                name: "IX_Kuyu_KuyuGrubuId",
                table: "Kuyular",
                newName: "IX_Kuyular_KuyuGrubuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wellborelar",
                table: "Wellborelar",
                column: "WellboreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sahalar",
                table: "Sahalar",
                column: "SahaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KuyuGruplari",
                table: "KuyuGruplari",
                column: "KuyuGrubuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kuyular",
                table: "Kuyular",
                column: "KuyuId");

            migrationBuilder.AddForeignKey(
                name: "FK_KuyuGruplari_Sahalar_SahaId",
                table: "KuyuGruplari",
                column: "SahaId",
                principalTable: "Sahalar",
                principalColumn: "SahaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kuyular_KuyuGruplari_KuyuGrubuId",
                table: "Kuyular",
                column: "KuyuGrubuId",
                principalTable: "KuyuGruplari",
                principalColumn: "KuyuGrubuId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wellborelar_Kuyular_KuyuId",
                table: "Wellborelar",
                column: "KuyuId",
                principalTable: "Kuyular",
                principalColumn: "KuyuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
