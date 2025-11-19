-- Fix the migrations history table for snake_case naming
-- Run this before applying the snake_case migration

-- Rename the migrations history table columns to snake_case
ALTER TABLE "__EFMigrationsHistory" 
RENAME COLUMN "MigrationId" TO migration_id;

ALTER TABLE "__EFMigrationsHistory" 
RENAME COLUMN "ProductVersion" TO product_version;
