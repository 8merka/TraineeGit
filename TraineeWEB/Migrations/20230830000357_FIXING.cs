using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeWEB.Migrations
{
    /// <inheritdoc />
    public partial class FIXING : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TABLE ActorRolesTemp (Id int NOT NULL, FilmId int NOT NULL, ActorId int NOT NULL, CharacterName nvarchar(MAX) NOT NULL);");

            migrationBuilder.Sql("INSERT INTO ActorRolesTemp SELECT Id, FilmId, ActorId, CharacterName FROM ActorRole;");

            migrationBuilder.Sql("DROP TABLE ActorRole;");

            migrationBuilder.Sql("EXEC sp_rename 'ActorRolesTemp', 'ActorRoles';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TABLE ActorRolesTemp (Id int NOT NULL, FilmId int NOT NULL, ActorId int NOT NULL, CharacterName nvarchar(MAX) NOT NULL);");

            migrationBuilder.Sql("INSERT INTO ActorRolesTemp SELECT Id, FilmId, ActorId, CharacterName FROM ActorRole;");

            migrationBuilder.Sql("DROP TABLE ActorRole;");

            migrationBuilder.Sql("EXEC sp_rename 'ActorRolesTemp', 'ActorRoles';");
        }
    }
}

