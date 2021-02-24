using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentWebApp.Repository.Migrations
{
    public partial class PaymentStateCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentState",
                columns: table => new
                {
                    PaymentStateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentState", x => x.PaymentStateId);
                    table.ForeignKey(
                        name: "FK_PaymentState_PaymentDetail_PaymentDetailId",
                        column: x => x.PaymentDetailId,
                        principalTable: "PaymentDetail",
                        principalColumn: "PaymentDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentState_PaymentDetailId",
                table: "PaymentState",
                column: "PaymentDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentState");
        }
    }
}
