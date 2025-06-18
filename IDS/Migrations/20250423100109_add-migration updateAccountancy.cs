using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDS.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationupdateAccountancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReceptionEmpId",
                table: "TicketAccountancy",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiagnosisDocId",
                table: "TicketAccountancy",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketAccountancy_DiagnosisDocId",
                table: "TicketAccountancy",
                column: "DiagnosisDocId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAccountancy_ReceptionEmpId",
                table: "TicketAccountancy",
                column: "ReceptionEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAccountancy_AspNetUsers_DiagnosisDocId",
                table: "TicketAccountancy",
                column: "DiagnosisDocId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketAccountancy_AspNetUsers_ReceptionEmpId",
                table: "TicketAccountancy",
                column: "ReceptionEmpId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketAccountancy_AspNetUsers_DiagnosisDocId",
                table: "TicketAccountancy");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketAccountancy_AspNetUsers_ReceptionEmpId",
                table: "TicketAccountancy");

            migrationBuilder.DropIndex(
                name: "IX_TicketAccountancy_DiagnosisDocId",
                table: "TicketAccountancy");

            migrationBuilder.DropIndex(
                name: "IX_TicketAccountancy_ReceptionEmpId",
                table: "TicketAccountancy");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionEmpId",
                table: "TicketAccountancy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DiagnosisDocId",
                table: "TicketAccountancy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
