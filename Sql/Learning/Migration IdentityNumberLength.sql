START TRANSACTION;

ALTER TABLE `Staffs` MODIFY COLUMN `IdentityNumber` varchar(200) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221025102522_IdentityNumberLength', '6.0.2');

COMMIT;