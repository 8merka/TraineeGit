using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeWEB.Migrations
{
    /// <inheritdoc />
    public partial class FixIdColumnMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.RenameIndex(
                name: "IX_ActorRole_FilmId",
                table: "ActorRoles",
                newName: "IX_ActorRoles_FilmId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ActorRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRoles1",
                table: "ActorRoles",
                columns: new[] { "ActorId", "FilmId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ActorRoles_Actors_ActorId",
                table: "ActorRoles",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorRoles_Films_FilmId",
                table: "ActorRoles",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.RenameIndex(
                name: "IX_ActorRoles_FilmId",
                table: "ActorRole",
                newName: "IX_ActorRole_FilmId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ActorRole",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRole",
                table: "ActorRole",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActorRole_ActorId",
                table: "ActorRole",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorRole_Actors_ActorId",
                table: "ActorRole",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorRole_Films_FilmId",
                table: "ActorRole",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}


