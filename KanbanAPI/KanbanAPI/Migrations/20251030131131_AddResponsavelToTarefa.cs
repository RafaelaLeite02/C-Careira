using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddResponsavelToTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Respnsavel",
                table: "Tarefas",
                newName: "Responsavel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Responsavel",
                table: "Tarefas",
                newName: "Respnsavel");
        }
    }
}
