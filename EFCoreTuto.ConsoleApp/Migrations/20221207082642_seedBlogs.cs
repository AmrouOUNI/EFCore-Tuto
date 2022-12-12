using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTuto.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class seedBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Url" },
                values: new object[] { 2, "https://learn.microsoft.com/en-us/ef/efcore-and-ef6/support" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
