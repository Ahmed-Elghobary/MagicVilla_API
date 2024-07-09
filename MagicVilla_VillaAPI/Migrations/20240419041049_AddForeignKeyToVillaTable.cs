using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 6, 10, 49, 397, DateTimeKind.Local).AddTicks(399));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 6, 10, 49, 397, DateTimeKind.Local).AddTicks(446));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 6, 10, 49, 397, DateTimeKind.Local).AddTicks(450));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 6, 10, 49, 397, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 6, 10, 49, 397, DateTimeKind.Local).AddTicks(456));

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaID",
                table: "villaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_villas_VillaID",
                table: "villaNumbers",
                column: "VillaID",
                principalTable: "villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_villas_VillaID",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VillaID",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 5, 3, 6, 392, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 5, 3, 6, 392, DateTimeKind.Local).AddTicks(5089));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 5, 3, 6, 392, DateTimeKind.Local).AddTicks(5096));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 5, 3, 6, 392, DateTimeKind.Local).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 19, 5, 3, 6, 392, DateTimeKind.Local).AddTicks(5109));
        }
    }
}
