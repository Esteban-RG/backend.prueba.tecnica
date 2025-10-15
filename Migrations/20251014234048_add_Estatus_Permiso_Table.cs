using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaBackend.Migrations
{
    /// <inheritdoc />
    public partial class add_Estatus_Permiso_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComentariosSupervisor",
                table: "Permisos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRevision",
                table: "Permisos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEstatusPermiso",
                table: "Permisos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstatusPermisos",
                columns: table => new
                {
                    IdEstatusPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusPermisos", x => x.IdEstatusPermiso);
                });

            migrationBuilder.InsertData(
                table: "EstatusPermisos",
                columns: new[] { "IdEstatusPermiso", "Activo", "Descripcion" },
                values: new object[,]
                {
                    { 1, true, "Pendiente" },
                    { 2, true, "Aprobado" },
                    { 3, true, "Rechazado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_IdEstatusPermiso",
                table: "Permisos",
                column: "IdEstatusPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_EstatusPermisos_Descripcion",
                table: "EstatusPermisos",
                column: "Descripcion",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_EstatusPermisos_IdEstatusPermiso",
                table: "Permisos",
                column: "IdEstatusPermiso",
                principalTable: "EstatusPermisos",
                principalColumn: "IdEstatusPermiso",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_EstatusPermisos_IdEstatusPermiso",
                table: "Permisos");

            migrationBuilder.DropTable(
                name: "EstatusPermisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_IdEstatusPermiso",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "ComentariosSupervisor",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "FechaRevision",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "IdEstatusPermiso",
                table: "Permisos");
        }
    }
}
