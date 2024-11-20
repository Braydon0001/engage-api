START TRANSACTION;

ALTER TABLE `Creditors` ADD `EvolutionCreditorNumber` varchar(200) CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241013190156_EvolutionCreditorNumber', '8.0.2');

COMMIT;