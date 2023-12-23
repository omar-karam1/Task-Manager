using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projactC_2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Fname = table.Column<string>(nullable: false),
            //        Lname = table.Column<string>(nullable: true),
            //        UserName = table.Column<string>(nullable: false),
            //        Email = table.Column<string>(nullable: true),
            //        Password = table.Column<string>(nullable: false),
            //        BirthDate = table.Column<DateTime>(nullable: true),
            //        ImageData = table.Column<byte[]>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.UserId);
            //    });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    TimeStart = table.Column<DateTime>(nullable: true),
                    TimeEnd = table.Column<DateTime>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    TaskType = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    check = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserID",
                table: "Tasks",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            //migrationBuilder.DropTable(
            //    name: "User");
        }
    }
}
