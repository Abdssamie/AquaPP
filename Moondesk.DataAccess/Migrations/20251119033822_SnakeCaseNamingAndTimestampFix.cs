using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moondesk.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SnakeCaseNamingAndTimestampFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMemberships_Organizations_OrganizationId",
                table: "OrganizationMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMemberships_Users_UserId",
                table: "OrganizationMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sensors",
                table: "sensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_readings",
                table: "readings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_commands",
                table: "commands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_assets",
                table: "assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alerts",
                table: "alerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationMemberships",
                table: "OrganizationMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionCredentials",
                table: "ConnectionCredentials");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "organizations");

            migrationBuilder.RenameTable(
                name: "OrganizationMemberships",
                newName: "organization_memberships");

            migrationBuilder.RenameTable(
                name: "ConnectionCredentials",
                newName: "connection_credentials");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "users",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IsOnboarded",
                table: "users",
                newName: "is_onboarded");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "users",
                newName: "ix_users_username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "users",
                newName: "ix_users_email");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "sensors",
                newName: "unit");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "sensors",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Protocol",
                table: "sensors",
                newName: "protocol");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "sensors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "sensors",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "sensors",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sensors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ThresholdLow",
                table: "sensors",
                newName: "threshold_low");

            migrationBuilder.RenameColumn(
                name: "ThresholdHigh",
                table: "sensors",
                newName: "threshold_high");

            migrationBuilder.RenameColumn(
                name: "SamplingIntervalMs",
                table: "sensors",
                newName: "sampling_interval_ms");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "sensors",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "MinValue",
                table: "sensors",
                newName: "min_value");

            migrationBuilder.RenameColumn(
                name: "MaxValue",
                table: "sensors",
                newName: "max_value");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "sensors",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "sensors",
                newName: "asset_id");

            migrationBuilder.RenameIndex(
                name: "IX_sensors_OrganizationId_IsActive",
                table: "sensors",
                newName: "ix_sensors_organization_id_is_active");

            migrationBuilder.RenameIndex(
                name: "IX_sensors_OrganizationId_AssetId",
                table: "sensors",
                newName: "ix_sensors_organization_id_asset_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "readings",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "readings",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "readings",
                newName: "quality");

            migrationBuilder.RenameColumn(
                name: "Protocol",
                table: "readings",
                newName: "protocol");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "readings",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "readings",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "readings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "readings",
                newName: "sensor_id");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "readings",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "organizations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "organizations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SubscriptionPlan",
                table: "organizations",
                newName: "subscription_plan");

            migrationBuilder.RenameColumn(
                name: "StorageLimitGB",
                table: "organizations",
                newName: "storage_limit_gb");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "organizations",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "organizations",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_OwnerId",
                table: "organizations",
                newName: "ix_organizations_owner_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "commands",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "commands",
                newName: "response");

            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "commands",
                newName: "payload");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "commands",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "commands",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "commands",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "commands",
                newName: "sensor_id");

            migrationBuilder.RenameColumn(
                name: "RetryCount",
                table: "commands",
                newName: "retry_count");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "commands",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "MaxRetries",
                table: "commands",
                newName: "max_retries");

            migrationBuilder.RenameColumn(
                name: "ExecutedAt",
                table: "commands",
                newName: "executed_at");

            migrationBuilder.RenameColumn(
                name: "ErrorMessage",
                table: "commands",
                newName: "error_message");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "commands",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "commands",
                newName: "completed_at");

            migrationBuilder.RenameColumn(
                name: "CommandType",
                table: "commands",
                newName: "command_type");

            migrationBuilder.RenameIndex(
                name: "IX_commands_OrganizationId_UserId",
                table: "commands",
                newName: "ix_commands_organization_id_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_commands_OrganizationId_Status_CreatedAt",
                table: "commands",
                newName: "ix_commands_organization_id_status_created_at");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "assets",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "assets",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "assets",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "assets",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "assets",
                newName: "manufacturer");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "assets",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "assets",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "assets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "assets",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "ModelNumber",
                table: "assets",
                newName: "model_number");

            migrationBuilder.RenameColumn(
                name: "LastSeen",
                table: "assets",
                newName: "last_seen");

            migrationBuilder.RenameColumn(
                name: "InstallationDate",
                table: "assets",
                newName: "installation_date");

            migrationBuilder.RenameIndex(
                name: "IX_assets_OrganizationId",
                table: "assets",
                newName: "ix_assets_organization_id");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "alerts",
                newName: "timestamp");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "alerts",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "Protocol",
                table: "alerts",
                newName: "protocol");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "alerts",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "alerts",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "alerts",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Acknowledged",
                table: "alerts",
                newName: "acknowledged");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "alerts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TriggerValue",
                table: "alerts",
                newName: "trigger_value");

            migrationBuilder.RenameColumn(
                name: "ThresholdValue",
                table: "alerts",
                newName: "threshold_value");

            migrationBuilder.RenameColumn(
                name: "SensorId",
                table: "alerts",
                newName: "sensor_id");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "alerts",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "AcknowledgedBy",
                table: "alerts",
                newName: "acknowledged_by");

            migrationBuilder.RenameColumn(
                name: "AcknowledgedAt",
                table: "alerts",
                newName: "acknowledged_at");

            migrationBuilder.RenameIndex(
                name: "IX_alerts_OrganizationId_Acknowledged_Timestamp",
                table: "alerts",
                newName: "ix_alerts_organization_id_acknowledged_timestamp");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "organization_memberships",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Metadata",
                table: "organization_memberships",
                newName: "metadata");

            migrationBuilder.RenameColumn(
                name: "JoinedAt",
                table: "organization_memberships",
                newName: "joined_at");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "organization_memberships",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "organization_memberships",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationMemberships_OrganizationId",
                table: "organization_memberships",
                newName: "ix_organization_memberships_organization_id");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "connection_credentials",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Protocol",
                table: "connection_credentials",
                newName: "protocol");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "connection_credentials",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "connection_credentials",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "connection_credentials",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "LastUsedAt",
                table: "connection_credentials",
                newName: "last_used_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "connection_credentials",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "EndpointUri",
                table: "connection_credentials",
                newName: "endpoint_uri");

            migrationBuilder.RenameColumn(
                name: "EncryptionIV",
                table: "connection_credentials",
                newName: "encryption_iv");

            migrationBuilder.RenameColumn(
                name: "EncryptedPassword",
                table: "connection_credentials",
                newName: "encrypted_password");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "connection_credentials",
                newName: "created_at");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                table: "alerts",
                type: "timestamptz",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "acknowledged_at",
                table: "alerts",
                type: "timestamptz",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sensors",
                table: "sensors",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_readings",
                table: "readings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organizations",
                table: "organizations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_commands",
                table: "commands",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_assets",
                table: "assets",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_alerts",
                table: "alerts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organization_memberships",
                table: "organization_memberships",
                columns: new[] { "user_id", "organization_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_connection_credentials",
                table: "connection_credentials",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_organization_memberships_organizations_organization_id",
                table: "organization_memberships",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_organization_memberships_users_user_id",
                table: "organization_memberships",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_organization_memberships_organizations_organization_id",
                table: "organization_memberships");

            migrationBuilder.DropForeignKey(
                name: "fk_organization_memberships_users_user_id",
                table: "organization_memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sensors",
                table: "sensors");

            migrationBuilder.DropPrimaryKey(
                name: "pk_readings",
                table: "readings");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organizations",
                table: "organizations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_commands",
                table: "commands");

            migrationBuilder.DropPrimaryKey(
                name: "pk_assets",
                table: "assets");

            migrationBuilder.DropPrimaryKey(
                name: "pk_alerts",
                table: "alerts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organization_memberships",
                table: "organization_memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pk_connection_credentials",
                table: "connection_credentials");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "organizations",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "organization_memberships",
                newName: "OrganizationMemberships");

            migrationBuilder.RenameTable(
                name: "connection_credentials",
                newName: "ConnectionCredentials");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "Users",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_onboarded",
                table: "Users",
                newName: "IsOnboarded");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_users_username",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.RenameIndex(
                name: "ix_users_email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameColumn(
                name: "unit",
                table: "sensors",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "sensors",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "protocol",
                table: "sensors",
                newName: "Protocol");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "sensors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "sensors",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "sensors",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "sensors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "threshold_low",
                table: "sensors",
                newName: "ThresholdLow");

            migrationBuilder.RenameColumn(
                name: "threshold_high",
                table: "sensors",
                newName: "ThresholdHigh");

            migrationBuilder.RenameColumn(
                name: "sampling_interval_ms",
                table: "sensors",
                newName: "SamplingIntervalMs");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "sensors",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "min_value",
                table: "sensors",
                newName: "MinValue");

            migrationBuilder.RenameColumn(
                name: "max_value",
                table: "sensors",
                newName: "MaxValue");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "sensors",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "sensors",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "ix_sensors_organization_id_is_active",
                table: "sensors",
                newName: "IX_sensors_OrganizationId_IsActive");

            migrationBuilder.RenameIndex(
                name: "ix_sensors_organization_id_asset_id",
                table: "sensors",
                newName: "IX_sensors_OrganizationId_AssetId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "readings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "readings",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "quality",
                table: "readings",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "protocol",
                table: "readings",
                newName: "Protocol");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "readings",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "readings",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "readings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "sensor_id",
                table: "readings",
                newName: "SensorId");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "readings",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Organizations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Organizations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "subscription_plan",
                table: "Organizations",
                newName: "SubscriptionPlan");

            migrationBuilder.RenameColumn(
                name: "storage_limit_gb",
                table: "Organizations",
                newName: "StorageLimitGB");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "Organizations",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Organizations",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_organizations_owner_id",
                table: "Organizations",
                newName: "IX_Organizations_OwnerId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "commands",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "response",
                table: "commands",
                newName: "Response");

            migrationBuilder.RenameColumn(
                name: "payload",
                table: "commands",
                newName: "Payload");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "commands",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "commands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "commands",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "sensor_id",
                table: "commands",
                newName: "SensorId");

            migrationBuilder.RenameColumn(
                name: "retry_count",
                table: "commands",
                newName: "RetryCount");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "commands",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "max_retries",
                table: "commands",
                newName: "MaxRetries");

            migrationBuilder.RenameColumn(
                name: "executed_at",
                table: "commands",
                newName: "ExecutedAt");

            migrationBuilder.RenameColumn(
                name: "error_message",
                table: "commands",
                newName: "ErrorMessage");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "commands",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "completed_at",
                table: "commands",
                newName: "CompletedAt");

            migrationBuilder.RenameColumn(
                name: "command_type",
                table: "commands",
                newName: "CommandType");

            migrationBuilder.RenameIndex(
                name: "ix_commands_organization_id_user_id",
                table: "commands",
                newName: "IX_commands_OrganizationId_UserId");

            migrationBuilder.RenameIndex(
                name: "ix_commands_organization_id_status_created_at",
                table: "commands",
                newName: "IX_commands_OrganizationId_Status_CreatedAt");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "assets",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "assets",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "assets",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "assets",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "manufacturer",
                table: "assets",
                newName: "Manufacturer");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "assets",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "assets",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "assets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "assets",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "model_number",
                table: "assets",
                newName: "ModelNumber");

            migrationBuilder.RenameColumn(
                name: "last_seen",
                table: "assets",
                newName: "LastSeen");

            migrationBuilder.RenameColumn(
                name: "installation_date",
                table: "assets",
                newName: "InstallationDate");

            migrationBuilder.RenameIndex(
                name: "ix_assets_organization_id",
                table: "assets",
                newName: "IX_assets_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "alerts",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "alerts",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "protocol",
                table: "alerts",
                newName: "Protocol");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "alerts",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "alerts",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "alerts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "acknowledged",
                table: "alerts",
                newName: "Acknowledged");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "alerts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "trigger_value",
                table: "alerts",
                newName: "TriggerValue");

            migrationBuilder.RenameColumn(
                name: "threshold_value",
                table: "alerts",
                newName: "ThresholdValue");

            migrationBuilder.RenameColumn(
                name: "sensor_id",
                table: "alerts",
                newName: "SensorId");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "alerts",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "acknowledged_by",
                table: "alerts",
                newName: "AcknowledgedBy");

            migrationBuilder.RenameColumn(
                name: "acknowledged_at",
                table: "alerts",
                newName: "AcknowledgedAt");

            migrationBuilder.RenameIndex(
                name: "ix_alerts_organization_id_acknowledged_timestamp",
                table: "alerts",
                newName: "IX_alerts_OrganizationId_Acknowledged_Timestamp");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "OrganizationMemberships",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "metadata",
                table: "OrganizationMemberships",
                newName: "Metadata");

            migrationBuilder.RenameColumn(
                name: "joined_at",
                table: "OrganizationMemberships",
                newName: "JoinedAt");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "OrganizationMemberships",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "OrganizationMemberships",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_organization_memberships_organization_id",
                table: "OrganizationMemberships",
                newName: "IX_OrganizationMemberships_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "ConnectionCredentials",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "protocol",
                table: "ConnectionCredentials",
                newName: "Protocol");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ConnectionCredentials",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ConnectionCredentials",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "ConnectionCredentials",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "last_used_at",
                table: "ConnectionCredentials",
                newName: "LastUsedAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "ConnectionCredentials",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "endpoint_uri",
                table: "ConnectionCredentials",
                newName: "EndpointUri");

            migrationBuilder.RenameColumn(
                name: "encryption_iv",
                table: "ConnectionCredentials",
                newName: "EncryptionIV");

            migrationBuilder.RenameColumn(
                name: "encrypted_password",
                table: "ConnectionCredentials",
                newName: "EncryptedPassword");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ConnectionCredentials",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "alerts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AcknowledgedAt",
                table: "alerts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sensors",
                table: "sensors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_readings",
                table: "readings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_commands",
                table: "commands",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_assets",
                table: "assets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alerts",
                table: "alerts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationMemberships",
                table: "OrganizationMemberships",
                columns: new[] { "UserId", "OrganizationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionCredentials",
                table: "ConnectionCredentials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMemberships_Organizations_OrganizationId",
                table: "OrganizationMemberships",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMemberships_Users_UserId",
                table: "OrganizationMemberships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
