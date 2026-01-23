using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class MakeBloodBankOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID",
                table: "donations");

            migrationBuilder.AlterColumn<int>(
                name: "BloodBankBankID",
                table: "donations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BloodBankBankID1",
                table: "donations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_donations_BloodBankBankID1",
                table: "donations",
                column: "BloodBankBankID1");

            migrationBuilder.AddForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID",
                table: "donations",
                column: "BloodBankBankID",
                principalTable: "bloodbanks",
                principalColumn: "BankID");

            migrationBuilder.AddForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID1",
                table: "donations",
                column: "BloodBankBankID1",
                principalTable: "bloodbanks",
                principalColumn: "BankID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID",
                table: "donations");

            migrationBuilder.DropForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID1",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "IX_donations_BloodBankBankID1",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "BloodBankBankID1",
                table: "donations");

            migrationBuilder.AlterColumn<int>(
                name: "BloodBankBankID",
                table: "donations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_donations_bloodbanks_BloodBankBankID",
                table: "donations",
                column: "BloodBankBankID",
                principalTable: "bloodbanks",
                principalColumn: "BankID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
