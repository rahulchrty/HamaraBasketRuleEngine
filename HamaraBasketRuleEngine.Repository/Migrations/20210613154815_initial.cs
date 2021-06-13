using Microsoft.EntityFrameworkCore.Migrations;

namespace HamaraBasketRuleEngine.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "dbo",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellBy = table.Column<int>(type: "int", nullable: true),
                    Quality = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "RuleByItems",
                schema: "dbo",
                columns: table => new
                {
                    ReleByItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    QualityChangesOverApproachingSellByDay = table.Column<int>(type: "int", nullable: false),
                    QualityChangesAfterSellByDay = table.Column<int>(type: "int", nullable: false),
                    MaxQualityValue = table.Column<int>(type: "int", nullable: false),
                    MinQualityValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleByItems", x => x.ReleByItemId);
                    table.ForeignKey(
                        name: "FK_RuleByItem_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "dbo",
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RuleByNumberOfDaysLefts",
                schema: "dbo",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleByItemId = table.Column<int>(type: "int", nullable: false),
                    DaysLeftToSellByDate = table.Column<int>(type: "int", nullable: false),
                    QualityChangesByDaysLeft = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleByNumberOfDaysLefts", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_RuleByNumberOfDaysLeft_RuleByItemId",
                        column: x => x.RuleByItemId,
                        principalSchema: "dbo",
                        principalTable: "RuleByItems",
                        principalColumn: "ReleByItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RuleByItems_ItemId",
                schema: "dbo",
                table: "RuleByItems",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuleByNumberOfDaysLefts_RuleByItemId",
                schema: "dbo",
                table: "RuleByNumberOfDaysLefts",
                column: "RuleByItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RuleByNumberOfDaysLefts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RuleByItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "dbo");
        }
    }
}
