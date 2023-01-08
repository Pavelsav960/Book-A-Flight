using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Migrations
{
    /// <inheritdoc />
    public partial class AddPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    IdNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardHolderFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolderLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ExparationMonth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExparationYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaveDetails = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.IdNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payments");
        }
    }
}
