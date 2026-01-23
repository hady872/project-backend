using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreDonationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CenterName",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonatedBefore",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecentInfection",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecentSurgery",
                table: "donations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "donations",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterName",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "DonatedBefore",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "Medications",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "RecentInfection",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "RecentSurgery",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "donations");
        }
    }
}
