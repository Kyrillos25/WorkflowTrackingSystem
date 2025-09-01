using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowTracking.Modules.WFProcessor.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class CreateProcessorTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "processors");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "processors",
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
            schema: "processors",
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
            schema: "processors",
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
            schema: "processors",
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
            name: "processes",
            schema: "processors",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                workflow_id = table.Column<Guid>(type: "uuid", nullable: false),
                initiator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_processes", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "process_step_executions",
            schema: "processors",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                step_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                performed_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                performed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                process_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_process_step_executions", x => x.id);
                table.ForeignKey(
                    name: "fk_process_step_executions_processes_process_id",
                    column: x => x.process_id,
                    principalSchema: "processors",
                    principalTable: "processes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_process_step_executions_process_id",
            schema: "processors",
            table: "process_step_executions",
            column: "process_id");

        migrationBuilder.CreateIndex(
            name: "ix_processes_workflow_id",
            schema: "processors",
            table: "processes",
            column: "workflow_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "processors");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "processors");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "processors");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "processors");

        migrationBuilder.DropTable(
            name: "process_step_executions",
            schema: "processors");

        migrationBuilder.DropTable(
            name: "processes",
            schema: "processors");
    }
}
