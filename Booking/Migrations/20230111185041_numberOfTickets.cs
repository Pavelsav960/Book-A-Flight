using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class numberOfTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfTickets",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfTickets",
                table: "Bookings");
        }
    }
}
