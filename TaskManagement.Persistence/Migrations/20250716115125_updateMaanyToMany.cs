using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateMaanyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignments",
                schema: "taskManagement_schema");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PriorityStatus",
                schema: "taskManagement_schema",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskStatus",
                schema: "taskManagement_schema",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "TaskAssignee",
                schema: "taskManagement_schema",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignee", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskAssignee_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "taskManagement_schema",
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignee_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "taskManagement_schema",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignee_UserId",
                schema: "taskManagement_schema",
                table: "TaskAssignee",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignee",
                schema: "taskManagement_schema");

            migrationBuilder.AlterColumn<string>(
                name: "TaskStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                schema: "taskManagement_schema",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "taskManagement_schema",
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "taskManagement_schema",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                column: "PriorityStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskStatus",
                schema: "taskManagement_schema",
                table: "Tasks",
                column: "TaskStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_UserId",
                schema: "taskManagement_schema",
                table: "TaskAssignments",
                column: "UserId");
        }
    }
}
