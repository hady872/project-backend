using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalRequestToDonation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankID",
                table: "donations");

            migrationBuilder.AddColumn<int>(
                name: "HospitalRequestID",
                table: "donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_donations_HospitalRequestID",
                table: "donations",
                column: "HospitalRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_donations_hospitalrequests_HospitalRequestID",
                table: "donations",
                column: "HospitalRequestID",
                principalTable: "hospitalrequests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donations_hospitalrequests_HospitalRequestID",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "IX_donations_HospitalRequestID",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "HospitalRequestID",
                table: "donations");

            migrationBuilder.AddColumn<int>(
                name: "BankID",
                table: "donations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
