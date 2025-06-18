using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDS.Migrations
{
    /// <inheritdoc />
    public partial class addingHistoryAndRefferedTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalHistoryId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReferredToId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeartTrouble = table.Column<bool>(type: "bit", nullable: false),
                    Hyperttention = table.Column<bool>(type: "bit", nullable: false),
                    Pregnancy = table.Column<bool>(type: "bit", nullable: false),
                    Diabetes = table.Column<bool>(type: "bit", nullable: false),
                    Hepatitis = table.Column<bool>(type: "bit", nullable: false),
                    AIDs = table.Column<bool>(type: "bit", nullable: false),
                    Tuberculosis = table.Column<bool>(type: "bit", nullable: false),
                    Allergies = table.Column<bool>(type: "bit", nullable: false),
                    Anemia = table.Column<bool>(type: "bit", nullable: false),
                    Rheumatism = table.Column<bool>(type: "bit", nullable: false),
                    RadTherapy = table.Column<bool>(type: "bit", nullable: false),
                    Haemophilia = table.Column<bool>(type: "bit", nullable: false),
                    AspirinIntake = table.Column<bool>(type: "bit", nullable: false),
                    KidneyTroubles = table.Column<bool>(type: "bit", nullable: false),
                    Asthma = table.Column<bool>(type: "bit", nullable: false),
                    HayFever = table.Column<bool>(type: "bit", nullable: false),
                    MedicalHistoryText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferredTo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oral = table.Column<bool>(type: "bit", nullable: false),
                    RemovableProsth = table.Column<bool>(type: "bit", nullable: false),
                    Operative = table.Column<bool>(type: "bit", nullable: false),
                    Endodontic = table.Column<bool>(type: "bit", nullable: false),
                    Ortho = table.Column<bool>(type: "bit", nullable: false),
                    CrownAndBridge = table.Column<bool>(type: "bit", nullable: false),
                    Surgery = table.Column<bool>(type: "bit", nullable: false),
                    Pedo = table.Column<bool>(type: "bit", nullable: false),
                    XRay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferredTo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MedicalHistoryId",
                table: "Tickets",
                column: "MedicalHistoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ReferredToId",
                table: "Tickets",
                column: "ReferredToId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_MedicalHistories_MedicalHistoryId",
                table: "Tickets",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ReferredTo_ReferredToId",
                table: "Tickets",
                column: "ReferredToId",
                principalTable: "ReferredTo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_MedicalHistories_MedicalHistoryId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ReferredTo_ReferredToId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "ReferredTo");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MedicalHistoryId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ReferredToId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ReferredToId",
                table: "Tickets");
        }
    }
}
