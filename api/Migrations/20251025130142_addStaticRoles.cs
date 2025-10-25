using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addStaticRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if roles already exist before inserting
            migrationBuilder.Sql(@"
                INSERT INTO ""AspNetRoles"" (""Id"", ""ConcurrencyStamp"", ""Discriminator"", ""Name"", ""NormalizedName"")
                SELECT '1', 'admin-stamp-123', 'IdentityRole', 'Admin', 'ADMIN'
                WHERE NOT EXISTS (SELECT 1 FROM ""AspNetRoles"" WHERE ""Name"" = 'Admin');

                INSERT INTO ""AspNetRoles"" (""Id"", ""ConcurrencyStamp"", ""Discriminator"", ""Name"", ""NormalizedName"")
                SELECT '2', 'user-stamp-123', 'IdentityRole', 'User', 'USER'
                WHERE NOT EXISTS (SELECT 1 FROM ""AspNetRoles"" WHERE ""Name"" = 'User');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
