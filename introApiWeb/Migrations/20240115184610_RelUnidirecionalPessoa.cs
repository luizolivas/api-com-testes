using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace introApiWeb.Migrations
{
    /// <inheritdoc />
    public partial class RelUnidirecionalPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Pessoas_PessoaId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_PessoaId",
                table: "Pedidos",
                newName: "IX_Pedidos_PessoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pessoas_PessoaId",
                table: "Pedidos",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pessoas_PessoaId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_PessoaId",
                table: "Pedido",
                newName: "IX_Pedido_PessoaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Pessoas_PessoaId",
                table: "Pedido",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
