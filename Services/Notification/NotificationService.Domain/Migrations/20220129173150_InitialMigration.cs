using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailService.Domain.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHistories");
        }
    }
}
