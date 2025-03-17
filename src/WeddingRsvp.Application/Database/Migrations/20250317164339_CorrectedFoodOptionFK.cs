using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRsvp.Application.Database.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedFoodOptionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFoodOption_FoodOption_EventId",
                schema: "core",
                table: "EventFoodOption");

            migrationBuilder.CreateIndex(
                name: "IX_EventFoodOption_FoodOptionId",
                schema: "core",
                table: "EventFoodOption",
                column: "FoodOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFoodOption_FoodOption_FoodOptionId",
                schema: "core",
                table: "EventFoodOption",
                column: "FoodOptionId",
                principalSchema: "core",
                principalTable: "FoodOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFoodOption_FoodOption_FoodOptionId",
                schema: "core",
                table: "EventFoodOption");

            migrationBuilder.DropIndex(
                name: "IX_EventFoodOption_FoodOptionId",
                schema: "core",
                table: "EventFoodOption");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFoodOption_FoodOption_EventId",
                schema: "core",
                table: "EventFoodOption",
                column: "EventId",
                principalSchema: "core",
                principalTable: "FoodOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
