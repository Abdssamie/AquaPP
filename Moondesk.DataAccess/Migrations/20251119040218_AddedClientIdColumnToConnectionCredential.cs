using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moondesk.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientIdColumnToConnectionCredential : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_id",
                table: "connection_credentials",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_id",
                table: "connection_credentials");
        }
    }
}
