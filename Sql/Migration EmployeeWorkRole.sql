START TRANSACTION;

ALTER TABLE `EmployeeWorkRoles` MODIFY COLUMN `Title` varchar(300) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241002083318_EmployeeWorkRole', '8.0.2');

COMMIT;