using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaBackend.Migrations
{
    /// <inheritdoc />
    public partial class addnullableUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_AspNetUsers_UsuarioId",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "TipoRolId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Permisos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_AspNetUsers_UsuarioId",
                table: "Permisos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_AspNetUsers_UsuarioId",
                table: "Permisos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Permisos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoRolId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_AspNetUsers_UsuarioId",
                table: "Permisos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
