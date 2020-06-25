using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.DAL.Migrations
{
    public partial class v31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OriginalName = table.Column<string>(nullable: true),
                    CzechName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonsDirectMovies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DirectorId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsDirectMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonsDirectMovies_Persons_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsDirectMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonsPlayMovies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsPlayMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonsPlayMovies_Persons_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsPlayMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Country", "CzechName", "Description", "Duration", "Genre", "OriginalName", "Photo" },
                values: new object[,]
                {
                    { new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7"), null, "Pán prstenů: Návrat krále", "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?", new TimeSpan(0, 3, 21, 0, 0), "Fantasy, Dobrodružný, Akční", "The Lord of the Rings: The Return of the King", "photo.png" },
                    { new Guid("20329257-c9b4-4344-8700-2816a2c8beb7"), "USA", "Harry Potter a Relikvie smrti", "Harry, Ron, and Hermione search for Voldemort's remaining Horcruxes in their effort to destroy the Dark Lord as the final battle rages on at Hogwarts", new TimeSpan(0, 2, 10, 0, 0), "Fantasy, Dobrodružný, Akční", "Harry Potter and the Deathly Hallows", "photo.png" },
                    { new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e"), "USA", "Pán prstenů: Návrat krále", "Nadchází čas rozhodující bitvy o přežití Středozemě. Putování jednotlivých členů Společenstva prstenu se dostává do poslední a rozhodující fáze. Čaroděj Gandalf, elf Legolas a trpaslík Gimli spěchají s dědicem trůnu Aragornem na pomoc zemi Gondor, která odolává ohromnému Sauronovu vojsku. Hobiti Frodo a Sam se v doprovodu Gluma snaží dostat hluboko do země Mordor, kde musí v ohních Hory osudu zničit magický Prsten moci. Jedině tak bude síla mocného pána temnot Saurona zlomena. Podaří se jim naplnit poslání Společenstva a zachránit Středozem? A za jakou cenu?", new TimeSpan(0, 3, 21, 0, 0), "Fantasy, Dobrodružný, Akční", "The Lord of the Rings: The Return of the King", "photo.png" },
                    { new Guid("59b9cb7d-864e-4894-ab68-796f6b170ca5"), "USA", "Spatnej film", "Self describing", new TimeSpan(0, 0, 10, 0, 0), "Bad films", "Bad film", "Photo.png" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Age", "FirstName", "LastName", "Photo" },
                values: new object[,]
                {
                    { new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"), 61, "Viggo", "Mortensen", "photo.png" },
                    { new Guid("b9767dba-6489-485e-bf61-4b2f2ba1a094"), 47, "David", "Yates", "photo.png" },
                    { new Guid("cc767aaa-6489-485e-bf61-4bff2ba1a033"), 30, "Daniel", "radcliffe", "photo.png" },
                    { new Guid("e64714bc-3eeb-4966-bb97-e7ce92daa36f"), 61, "Viggo", "Mortensen", "photo.png" },
                    { new Guid("e52ec127-1834-4ca6-8a7a-9936de02c780"), 30, "Peter", "NoName", null }
                });

            migrationBuilder.InsertData(
                table: "PersonsDirectMovies",
                columns: new[] { "Id", "DirectorId", "MovieId" },
                values: new object[] { new Guid("d184f72b-93ba-4492-8a83-d89df1555147"), new Guid("b9767dba-6489-485e-bf61-4b2f2ba1a094"), new Guid("20329257-c9b4-4344-8700-2816a2c8beb7") });

            migrationBuilder.InsertData(
                table: "PersonsPlayMovies",
                columns: new[] { "Id", "ActorId", "MovieId" },
                values: new object[,]
                {
                    { new Guid("d16de9a1-ce78-4914-89e4-2b400731a4c0"), new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"), new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7") },
                    { new Guid("967491f3-45bd-43f4-9553-dc2874256f79"), new Guid("481c97bb-4cbd-46ef-ab93-91d1b2e46128"), new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e") },
                    { new Guid("06a1e834-30fd-44d7-b9ea-3f387edcc48d"), new Guid("cc767aaa-6489-485e-bf61-4bff2ba1a033"), new Guid("20329257-c9b4-4344-8700-2816a2c8beb7") },
                    { new Guid("66dfc671-a6da-4332-a5ba-8564226f7510"), new Guid("e64714bc-3eeb-4966-bb97-e7ce92daa36f"), new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7") }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "Rating", "Text" },
                values: new object[,]
                {
                    { new Guid("d3ea76e3-c308-4c79-af81-37e86c4b79d0"), new Guid("23488cac-1f8b-4b8f-b9b5-66672838e8e7"), 1000, "Fakt dobrý hodnocení" },
                    { new Guid("cf50ff38-e568-4cdd-9508-3065b0370aa2"), new Guid("20329257-c9b4-4344-8700-2816a2c8beb7"), 1000, "Chapu, ze ne tak dobry jako LOR, ale je to hned za nim." },
                    { new Guid("f7807d79-c1e7-4f3d-a709-428f11a5787c"), new Guid("0c766d7b-916d-400a-a14d-8ec68ada8a1e"), 1000, "Fakt dobrý hodnocení" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonsDirectMovies_DirectorId",
                table: "PersonsDirectMovies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsDirectMovies_MovieId",
                table: "PersonsDirectMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsPlayMovies_ActorId",
                table: "PersonsPlayMovies",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsPlayMovies_MovieId",
                table: "PersonsPlayMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsDirectMovies");

            migrationBuilder.DropTable(
                name: "PersonsPlayMovies");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
