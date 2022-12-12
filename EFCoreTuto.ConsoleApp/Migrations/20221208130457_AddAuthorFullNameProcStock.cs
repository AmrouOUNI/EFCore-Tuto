using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTuto.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorFullNameProcStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    EXEC ('CREATE PROCEDURE getFullName
                        @LastName nvarchar(50),
                        @FirstName nvarchar(50)
                    AS
                        RETURN @LastName + @FirstName;')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    EXEC ('DROP PROCEDURE getFullName')");

        }
    }
}
