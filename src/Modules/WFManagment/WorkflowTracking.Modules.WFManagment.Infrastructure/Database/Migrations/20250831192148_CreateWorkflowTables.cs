using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowTracking.Modules.WFManagment.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateWorkflowTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "workflows");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "workflows",
            columns: table => new
            {
                inbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_message_consumers", x => new { x.inbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "inbox_messages",
            schema: "workflows",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "outbox_message_consumers",
            schema: "workflows",
            columns: table => new
            {
                outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "workflows",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "workflows",
            schema: "workflows",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_workflows", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "workflow_steps",
            schema: "workflows",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                step_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                assigned_to = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                action_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                next_step = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                workflow_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_workflow_steps", x => x.id);
                table.ForeignKey(
                    name: "fk_workflow_steps_workflows_workflow_id",
                    column: x => x.workflow_id,
                    principalSchema: "workflows",
                    principalTable: "workflows",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_workflow_steps_workflow_id",
            schema: "workflows",
            table: "workflow_steps",
            column: "workflow_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "workflows");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "workflows");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "workflows");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "workflows");

        migrationBuilder.DropTable(
            name: "workflow_steps",
            schema: "workflows");

        migrationBuilder.DropTable(
            name: "workflows",
            schema: "workflows");
    }
}
