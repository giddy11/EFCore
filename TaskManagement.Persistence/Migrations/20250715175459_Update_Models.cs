using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToId",
                schema: "taskManagement_schema",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                schema: "taskManagement_schema",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedToId",
                schema: "taskManagement_schema",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId",
                schema: "taskManagement_schema",
                table: "Tasks",
                column: "AssignedToId");
        }
    }
}
