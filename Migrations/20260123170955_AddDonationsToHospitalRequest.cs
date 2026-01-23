using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationsToHospitalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalRequestRequestID",
                table: "donations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_donations_HospitalRequestRequestID",
                table: "donations",
                column: "HospitalRequestRequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_donations_hospitalrequests_HospitalRequestRequestID",
                table: "donations",
                column: "HospitalRequestRequestID",
                principalTable: "hospitalrequests",
                principalColumn: "RequestID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donations_hospitalrequests_HospitalRequestRequestID",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "IX_donations_HospitalRequestRequestID",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "HospitalRequestRequestID",
                table: "donations");
        }
    }
}
