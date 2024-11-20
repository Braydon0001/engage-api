USE `engage-dev`;

START TRANSACTION;

ALTER TABLE `Suppliers` ADD `JsonTheme` json NULL;

ALTER TABLE `Suppliers` ADD `ThemeColor` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Suppliers` ADD `ThemeCustomColor` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Organizations` ADD `Files` json NULL;

ALTER TABLE `Organizations` ADD `JsonTheme` json NULL;

ALTER TABLE `Organizations` ADD `ThemeColor` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Organizations` ADD `ThemeCustomColor` longtext CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240821122728_OrganizationTheme', '8.0.2');

COMMIT;