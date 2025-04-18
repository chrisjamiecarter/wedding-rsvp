using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRsvp.Application.Database.Migrations
{
    /// <inheritdoc />
    public partial class TokenColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniqueLinkToken",
                schema: "core",
                table: "Invite",
                newName: "Token");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "core",
                table: "Invite",
                newName: "UniqueLinkToken");
        }
    }
}
