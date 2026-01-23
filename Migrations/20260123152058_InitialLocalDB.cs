using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class InitialLocalDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bloodbanks",
                columns: table => new
                {
                    BankID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BankName = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bloodbanks", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "hospitalrequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HospitalUserID = table.Column<int>(type: "INTEGER", nullable: false),
                    HospitalName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Urgency = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Contact = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitalrequests", x => x.RequestID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountType = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: true),
                    Medications = table.Column<string>(type: "TEXT", nullable: true),
                    RecentSurgery = table.Column<string>(type: "TEXT", nullable: true),
                    DonatedBefore = table.Column<string>(type: "TEXT", nullable: true),
                    RecentInfection = table.Column<string>(type: "TEXT", nullable: true),
                    CenterName = table.Column<string>(type: "TEXT", nullable: true),
                    HospitalRequestID = table.Column<int>(type: "INTEGER", nullable: true),
                    BloodBankBankID = table.Column<int>(type: "INTEGER", nullable: true),
                    BloodBankBankID1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_donations", x => x.DonationID);
                    table.ForeignKey(
                        name: "FK_donations_bloodbanks_BloodBankBankID",
                        column: x => x.BloodBankBankID,
                        principalTable: "bloodbanks",
                        principalColumn: "BankID");
                    table.ForeignKey(
                        name: "FK_donations_bloodbanks_BloodBankBankID1",
                        column: x => x.BloodBankBankID1,
                        principalTable: "bloodbanks",
                        principalColumn: "BankID");
                    table.ForeignKey(
                        name: "FK_donations_hospitalrequests_HospitalRequestID",
                        column: x => x.HospitalRequestID,
                        principalTable: "hospitalrequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_donations_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "otps",
                columns: table => new
                {
                    OTPID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    OTPCode = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otps", x => x.OTPID);
                    table.ForeignKey(
                        name: "FK_otps_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_donations_BloodBankBankID",
                table: "donations",
                column: "BloodBankBankID");

            migrationBuilder.CreateIndex(
                name: "IX_donations_BloodBankBankID1",
                table: "donations",
                column: "BloodBankBankID1");

            migrationBuilder.CreateIndex(
                name: "IX_donations_HospitalRequestID",
                table: "donations",
                column: "HospitalRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_donations_UserID",
                table: "donations",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_otps_UserID",
                table: "otps",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donations");

            migrationBuilder.DropTable(
                name: "otps");

            migrationBuilder.DropTable(
                name: "bloodbanks");

            migrationBuilder.DropTable(
                name: "hospitalrequests");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
