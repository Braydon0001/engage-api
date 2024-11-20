START TRANSACTION;

DROP TABLE `Stats_OrdersByRegion`;

DROP TABLE `Stats_StoresByRegion`;

CREATE TABLE `StatsOrdersByRegions` (
    `StatsOrdersByRegionId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `OrdersLast1Day` int NOT NULL,
    `OrdersLast7Days` int NOT NULL,
    `OrdersAll` int NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_StatsOrdersByRegions` PRIMARY KEY (`StatsOrdersByRegionId`),
    CONSTRAINT `FK_StatsOrdersByRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `StatsStoresByRegions` (
    `StatsStoresByRegionId` int NOT NULL AUTO_INCREMENT,
    `EngageRegionId` int NOT NULL,
    `Stores` int NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_StatsStoresByRegions` PRIMARY KEY (`StatsStoresByRegionId`),
    CONSTRAINT `FK_StatsStoresByRegions_opt_EngageRegions_EngageRegionId` FOREIGN KEY (`EngageRegionId`) REFERENCES `opt_EngageRegions` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_StatsOrdersByRegions_EngageRegionId` ON `StatsOrdersByRegions` (`EngageRegionId`);

CREATE INDEX `IX_StatsStoresByRegions_EngageRegionId` ON `StatsStoresByRegions` (`EngageRegionId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210608124943_StatsTables', '5.0.4');

COMMIT;

