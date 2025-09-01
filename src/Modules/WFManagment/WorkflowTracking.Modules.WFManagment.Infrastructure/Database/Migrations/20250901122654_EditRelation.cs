using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class EditRelation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_workflow_steps_workflows_workflow_id",
            schema: "workflows",
            table: "workflow_steps");

        migrationBuilder.AddForeignKey(
            name: "fk_workflow_steps_workflows_workflow_id",
            schema: "workflows",
            table: "workflow_steps",
            column: "workflow_id",
            principalSchema: "workflows",
            principalTable: "workflows",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_workflow_steps_workflows_workflow_id",
            schema: "workflows",
            table: "workflow_steps");

        migrationBuilder.AddForeignKey(
            name: "fk_workflow_steps_workflows_workflow_id",
            schema: "workflows",
            table: "workflow_steps",
            column: "workflow_id",
            principalSchema: "workflows",
            principalTable: "workflows",
            principalColumn: "id");
    }
}
