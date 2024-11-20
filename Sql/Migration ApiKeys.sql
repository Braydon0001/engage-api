START TRANSACTION;

CREATE TABLE `ApiKeys` (
    `ApiKeyId` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Value` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `AssignedTo` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `TenantId` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `Disabled` tinyint(1) NOT NULL,
    `Deleted` tinyint(1) NOT NULL,
    `CreatedBy` longtext CHARACTER SET utf8mb4 NULL,
    `Created` datetime(6) NOT NULL,
    `LastModifiedBy` longtext CHARACTER SET utf8mb4 NULL,
    `LastModified` datetime(6) NULL,
    `DeletedBy` longtext CHARACTER SET utf8mb4 NULL,
    `DeletedDate` datetime(6) NULL,
    CONSTRAINT `PK_ApiKeys` PRIMARY KEY (`ApiKeyId`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241003072337_ApiKeys', '8.0.2');

COMMIT;

