﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Data.IpAddress.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IpAddress",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    StartAddress_Start = table.Column<long>(type: "INTEGER", nullable: false),
                    StartAddress_End = table.Column<long>(type: "INTEGER", nullable: false),
                    EndAddress_Start = table.Column<long>(type: "INTEGER", nullable: false),
                    EndAddress_End = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpAddress", x => x.EntityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpAddress");
        }
    }
}
