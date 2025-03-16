using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRsvp.Application.Database.Migrations
{
    /// <inheritdoc />
    public partial class MissingForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InviteId",
                schema: "core",
                table: "Guest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Guest_InviteId",
                schema: "core",
                table: "Guest",
                column: "InviteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_Invite_InviteId",
                schema: "core",
                table: "Guest",
                column: "InviteId",
                principalSchema: "core",
                principalTable: "Invite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_Invite_InviteId",
                schema: "core",
                table: "Guest");

            migrationBuilder.DropIndex(
                name: "IX_Guest_InviteId",
                schema: "core",
                table: "Guest");

            migrationBuilder.DropColumn(
                name: "InviteId",
                schema: "core",
                table: "Guest");
        }
    }
}
