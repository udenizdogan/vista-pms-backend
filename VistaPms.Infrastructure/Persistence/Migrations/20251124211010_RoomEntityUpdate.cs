using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VistaPms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoomEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Rooms_RoomId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RatePlan_ActiveRatePlanId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ActiveRatePlanId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TenantId_Number",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Product_RoomId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ActiveRatePlanId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HousekeepingStatus",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Rooms",
                newName: "RoomNumber");

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "Rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Rooms",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorId",
                table: "Rooms",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "FloorNumber",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "Rooms",
                newName: "Number");

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "Rooms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Rooms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Rooms",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorId",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveRatePlanId",
                table: "Rooms",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousekeepingStatus",
                table: "Rooms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Rooms",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ActiveRatePlanId",
                table: "Rooms",
                column: "ActiveRatePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TenantId_Number",
                table: "Rooms",
                columns: new[] { "TenantId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_RoomId",
                table: "Product",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Rooms_RoomId",
                table: "Product",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floor_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RatePlan_ActiveRatePlanId",
                table: "Rooms",
                column: "ActiveRatePlanId",
                principalTable: "RatePlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
