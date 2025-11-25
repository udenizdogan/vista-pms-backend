using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VistaPms.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomTypeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatePlan_RoomType_RoomTypeId",
                table: "RatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_RatePlan_RoomType_RoomTypeId1",
                table: "RatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomType_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.RenameTable(
                name: "RoomType",
                newName: "RoomTypes");

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Rooms",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Reservations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "RatePlan",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "ProductCategory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "POSOrderItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "POSOrder",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "MaintenanceTicket",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "HousekeepingTask",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Guests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "FolioPayment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "FolioCharge",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Folio",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Floor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Building",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "RoomTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomTypeAmenities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeAmenities", x => new { x.RoomTypeId, x.Name });
                    table.ForeignKey(
                        name: "FK_RoomTypeAmenities_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false),
                    BranchId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTypeImages_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchId",
                table: "Rooms",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BranchId",
                table: "Reservations",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TenantId",
                table: "Reservations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RatePlan_BranchId",
                table: "RatePlan",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RatePlan_TenantId",
                table: "RatePlan",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_BranchId",
                table: "ProductCategory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_TenantId",
                table: "ProductCategory",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BranchId",
                table: "Product",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TenantId",
                table: "Product",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_POSOrderItem_BranchId",
                table: "POSOrderItem",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_POSOrderItem_TenantId",
                table: "POSOrderItem",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_POSOrder_BranchId",
                table: "POSOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_POSOrder_TenantId",
                table: "POSOrder",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceTicket_BranchId",
                table: "MaintenanceTicket",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceTicket_TenantId",
                table: "MaintenanceTicket",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_HousekeepingTask_BranchId",
                table: "HousekeepingTask",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HousekeepingTask_TenantId",
                table: "HousekeepingTask",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BranchId",
                table: "Guests",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_TenantId",
                table: "Guests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioPayment_BranchId",
                table: "FolioPayment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioPayment_TenantId",
                table: "FolioPayment",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioCharge_BranchId",
                table: "FolioCharge",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_FolioCharge_TenantId",
                table: "FolioCharge",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Folio_BranchId",
                table: "Folio",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Folio_TenantId",
                table: "Folio",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_BranchId",
                table: "Floor",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_TenantId",
                table: "Floor",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_BranchId",
                table: "Building",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_TenantId",
                table: "Building",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_BranchId",
                table: "RoomTypes",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_TenantId",
                table: "RoomTypes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeImages_BranchId",
                table: "RoomTypeImages",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeImages_RoomTypeId",
                table: "RoomTypeImages",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeImages_TenantId",
                table: "RoomTypeImages",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatePlan_RoomTypes_RoomTypeId",
                table: "RatePlan",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatePlan_RoomTypes_RoomTypeId1",
                table: "RatePlan",
                column: "RoomTypeId1",
                principalTable: "RoomTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatePlan_RoomTypes_RoomTypeId",
                table: "RatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_RatePlan_RoomTypes_RoomTypeId1",
                table: "RatePlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomTypeAmenities");

            migrationBuilder.DropTable(
                name: "RoomTypeImages");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BranchId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BranchId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TenantId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_RatePlan_BranchId",
                table: "RatePlan");

            migrationBuilder.DropIndex(
                name: "IX_RatePlan_TenantId",
                table: "RatePlan");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_BranchId",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategory_TenantId",
                table: "ProductCategory");

            migrationBuilder.DropIndex(
                name: "IX_Product_BranchId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_TenantId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_POSOrderItem_BranchId",
                table: "POSOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_POSOrderItem_TenantId",
                table: "POSOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_POSOrder_BranchId",
                table: "POSOrder");

            migrationBuilder.DropIndex(
                name: "IX_POSOrder_TenantId",
                table: "POSOrder");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceTicket_BranchId",
                table: "MaintenanceTicket");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceTicket_TenantId",
                table: "MaintenanceTicket");

            migrationBuilder.DropIndex(
                name: "IX_HousekeepingTask_BranchId",
                table: "HousekeepingTask");

            migrationBuilder.DropIndex(
                name: "IX_HousekeepingTask_TenantId",
                table: "HousekeepingTask");

            migrationBuilder.DropIndex(
                name: "IX_Guests_BranchId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_TenantId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_FolioPayment_BranchId",
                table: "FolioPayment");

            migrationBuilder.DropIndex(
                name: "IX_FolioPayment_TenantId",
                table: "FolioPayment");

            migrationBuilder.DropIndex(
                name: "IX_FolioCharge_BranchId",
                table: "FolioCharge");

            migrationBuilder.DropIndex(
                name: "IX_FolioCharge_TenantId",
                table: "FolioCharge");

            migrationBuilder.DropIndex(
                name: "IX_Folio_BranchId",
                table: "Folio");

            migrationBuilder.DropIndex(
                name: "IX_Folio_TenantId",
                table: "Folio");

            migrationBuilder.DropIndex(
                name: "IX_Floor_BranchId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Floor_TenantId",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_Building_BranchId",
                table: "Building");

            migrationBuilder.DropIndex(
                name: "IX_Building_TenantId",
                table: "Building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_BranchId",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_TenantId",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "RatePlan");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "POSOrderItem");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "POSOrder");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "MaintenanceTicket");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "HousekeepingTask");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "FolioPayment");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "FolioCharge");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Folio");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Floor");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "RoomTypes");

            migrationBuilder.RenameTable(
                name: "RoomTypes",
                newName: "RoomType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomAmenity",
                columns: table => new
                {
                    RoomTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenity", x => new { x.RoomTypeId, x.Id });
                    table.ForeignKey(
                        name: "FK_RoomAmenity_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RatePlan_RoomType_RoomTypeId",
                table: "RatePlan",
                column: "RoomTypeId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatePlan_RoomType_RoomTypeId1",
                table: "RatePlan",
                column: "RoomTypeId1",
                principalTable: "RoomType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomType_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
