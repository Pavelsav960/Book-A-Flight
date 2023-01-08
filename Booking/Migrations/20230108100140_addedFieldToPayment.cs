using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class addedFieldToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardHolderLastName",
                table: "payments",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CardHolderFirstName",
                table: "payments",
                newName: "CardHolderName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "payments",
                newName: "CardHolderLastName");

            migrationBuilder.RenameColumn(
                name: "CardHolderName",
                table: "payments",
                newName: "CardHolderFirstName");
        }
    }
}
