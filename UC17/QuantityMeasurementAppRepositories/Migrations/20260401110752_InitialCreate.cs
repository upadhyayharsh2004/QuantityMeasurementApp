using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementAppRepositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quantity_measurements_tables_conversion",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entity_result_value = table.Column<double>(type: "float", nullable: false),
                    entity_measurement_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    entity_operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entity_second_unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    entity_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    entity_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    entity_is_error = table.Column<bool>(type: "bit", nullable: false),
                    entity_error_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    entity_first_value = table.Column<double>(type: "float", nullable: false),
                    entity_first_unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    entity_second_value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quantity_measurements_tables_conversion", x => x.EntityId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quantity_measurements_tables_conversion");
        }
    }
}
