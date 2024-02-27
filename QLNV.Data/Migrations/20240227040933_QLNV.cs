using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLNV.Data.Migrations
{
    public partial class QLNV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nhanviens",
                columns: table => new
                {
                    MANV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TENNV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NGAYSINH = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhanviens", x => x.MANV);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nhanviens");
        }
    }
}
