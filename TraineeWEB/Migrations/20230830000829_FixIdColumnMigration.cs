using Microsoft.EntityFrameworkCore.Migrations;

namespace TraineeWEB.Migrations
{
    public partial class FixIdColumnMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActorRoles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActorRoles",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles",
                column: "Id");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActorRoles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActorRoles",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorRoles",
                table: "ActorRoles",
                column: "Id");
        }
    }
}
