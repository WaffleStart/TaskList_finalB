using System;
using System.Data;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using TaskList.Models;

#nullable disable

namespace TaskList.Migrations
{
    /// <inheritdoc />
    public partial class booleanToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
            name: "IsAdmin",
            table: "Users",
            type: "bit",
            nullable: true);

            migrationBuilder.AlterColumn<bool>(
            name: "IsAdmin",
            table: "Users",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldNullable: true);


            migrationBuilder.Sql("UPDATE Users SET IsAdmin = 0 WHERE IsAdmin IS NULL");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");
        }
    }
}
