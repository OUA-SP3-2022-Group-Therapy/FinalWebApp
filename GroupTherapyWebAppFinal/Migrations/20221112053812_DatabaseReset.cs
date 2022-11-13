using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTherapyWebAppFinal.Migrations
{
    public partial class DatabaseReset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilyGroups",
                columns: table => new
                {
                    FamilyGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyGroups", x => x.FamilyGroupID);
                });

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    UserModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.UserModelID);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FamilyGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetID);
                    table.ForeignKey(
                        name: "FK_Pets_FamilyGroups_FamilyGroupID",
                        column: x => x.FamilyGroupID,
                        principalTable: "FamilyGroups",
                        principalColumn: "FamilyGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduleType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Dose = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FamilyGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedules_FamilyGroups_FamilyGroupID",
                        column: x => x.FamilyGroupID,
                        principalTable: "FamilyGroups",
                        principalColumn: "FamilyGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    UserModelID = table.Column<int>(type: "int", nullable: false),
                    FamilyGroupID = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => new { x.UserModelID, x.FamilyGroupID });
                    table.ForeignKey(
                        name: "FK_Memberships_FamilyGroups_FamilyGroupID",
                        column: x => x.FamilyGroupID,
                        principalTable: "FamilyGroups",
                        principalColumn: "FamilyGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_UserModels_UserModelID",
                        column: x => x.UserModelID,
                        principalTable: "UserModels",
                        principalColumn: "UserModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trends",
                columns: table => new
                {
                    PetID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trends", x => new { x.PetID, x.Date });
                    table.ForeignKey(
                        name: "FK_Trends_Pets_PetID",
                        column: x => x.PetID,
                        principalTable: "Pets",
                        principalColumn: "PetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompletedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => new { x.ScheduleID, x.EventName });
                    table.ForeignKey(
                        name: "FK_Events_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_FamilyGroupID",
                table: "Memberships",
                column: "FamilyGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FamilyGroupID",
                table: "Pets",
                column: "FamilyGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FamilyGroupID",
                table: "Schedules",
                column: "FamilyGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Trends");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "UserModels");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "FamilyGroups");
        }
    }
}
