using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentWebApp.Repository.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "PaymentDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "PaymentDetail");
        }
    }
}
