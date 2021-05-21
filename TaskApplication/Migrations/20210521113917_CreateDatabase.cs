using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskApplication.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toDoTasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "BeginDate", "EndDate", "Name", "Notes", "Status" },
                values: new object[] { 1, new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "Schoonmaken", "Opruimen en netjes maken", 0 });

            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "BeginDate", "EndDate", "Name", "Notes", "Status" },
                values: new object[] { 2, new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "Schilderen", "Muren schilderen", 0 });

            migrationBuilder.InsertData(
                table: "ToDoTasks",
                columns: new[] { "Id", "BeginDate", "EndDate", "Name", "Notes", "Status" },
                values: new object[] { 3, new DateTime(2021, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "Meubels bouwen", "Ikea meubels in elkaar zetten", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoTasks");
        }
    }
}
