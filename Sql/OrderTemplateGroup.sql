SELECT `o`.`OrderTemplateGroupId`, COALESCE(`o`.`Name`, ''), COALESCE(`o`.`Description`, ''), `o`.`Order`, `t5`.`OrderTemplateProductId`, `t5`.`c`, `t5`.`OrderTemplateGroupId`, `t5`.`c0`, `t5`.`c1`, `t5`.`c2`, `t5`.`DCProductId`, `t5`.`EngageVariantProductId`, `t5`.`Name`, `t5`.`DistributionCenterId`, `t5`.`Name0`, `t5`.`VendorId`, `t5`.`Name1`, `t5`.`c3`, `t5`.`ProductClassId`, `t5`.`Name2`, `t5`.`UnitTypeId`, `t5`.`Name3`, `t5`.`ProductActiveStatusId`, `t5`.`Name4`, `t5`.`ProductStatusId`, `t5`.`Name5`, `t5`.`ProductWarehouseStatusId`, `t5`.`Name6`, `t5`.`ProductSubWarehouseId`, `t5`.`Name7`, `t5`.`c4`, `t5`.`c5`, `t5`.`c6`, `t5`.`Size`, `t5`.`PackSize`, `t5`.`c7`, `t5`.`c8`, `t5`.`Files`, `t5`.`Disabled`, `t5`.`Order`, `t5`.`Quantity`, `t5`.`Price`, `t5`.`PromotionPrice`, `t5`.`RecommendedPrice`, `t5`.`GrossProfitPercent`, `t5`.`c9`, `t5`.`c10`, `t5`.`Files0`, `t5`.`Files1`, `t5`.`c11`, `t5`.`EngageVariantProductId0`, `t5`.`DistributionCenterId0`, `t5`.`VendorId0`, `t5`.`Id`, `t5`.`Id0`, `t5`.`Id1`, `t5`.`Id2`, `t5`.`Id3`, `t5`.`SubWarehouseId`
FROM `OrderTemplateGroups` AS `o`
LEFT JOIN (
    SELECT `o0`.`OrderTemplateProductId`, FALSE AS `c`, `t`.`OrderTemplateGroupId`, COALESCE(`t`.`Name`, '') AS `c0`, COALESCE(`t`.`Description`, '') AS `c1`, FALSE AS `c2`, `t0`.`DCProductId`, `t0`.`EngageVariantProductId`, `t1`.`Name`, `t0`.`DistributionCenterId`, `t2`.`Name` AS `Name0`, `t0`.`VendorId`, `t3`.`Name` AS `Name1`, `t0`.`ManufacturerId` IS NOT NULL AS `c3`, `t0`.`ProductClassId`, `o1`.`Name` AS `Name2`, `t0`.`UnitTypeId`, `o2`.`Name` AS `Name3`, `t0`.`ProductActiveStatusId`, `o3`.`Name` AS `Name4`, `t0`.`ProductStatusId`, `o4`.`Name` AS `Name5`, `t0`.`ProductWarehouseStatusId`, `o5`.`Name` AS `Name6`, `t0`.`ProductSubWarehouseId`, `t4`.`Name` AS `Name7`, COALESCE(`t0`.`Code`, '') AS `c4`, COALESCE(`t0`.`Name`, '') AS `c5`, COALESCE(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(`t0`.`Code`, ' / '), `t0`.`Name`), ' / '), CAST(`t0`.`Size` AS char)), ' '), COALESCE(`o2`.`Name`, '')), '') AS `c6`, `t0`.`Size`, `t0`.`PackSize`, COALESCE(`t0`.`EANNumber`, '') AS `c7`, COALESCE(`t0`.`SubWarehouse`, '') AS `c8`, `t0`.`Files`, `t0`.`Disabled`, `o0`.`Order`, `o0`.`Quantity`, `o0`.`Price`, `o0`.`PromotionPrice`, `o0`.`RecommendedPrice`, `o0`.`GrossProfitPercent`, COALESCE(`o0`.`Suffix`, '') AS `c9`, COALESCE(`o0`.`Note`, '') AS `c10`, `o0`.`Files` AS `Files0`, `t1`.`Files` AS `Files1`, (
        SELECT COUNT(*)
        FROM `OrderSkus` AS `o6`
        WHERE NOT (`o6`.`Deleted`) AND (`o0`.`OrderTemplateProductId` = `o6`.`OrderTemplateProductId`)) AS `c11`, `t1`.`EngageVariantProductId` AS `EngageVariantProductId0`, `t2`.`DistributionCenterId` AS `DistributionCenterId0`, `t3`.`VendorId` AS `VendorId0`, `o1`.`Id`, `o2`.`Id` AS `Id0`, `o3`.`Id` AS `Id1`, `o4`.`Id` AS `Id2`, `o5`.`Id` AS `Id3`, `t4`.`SubWarehouseId`, `o0`.`OrderTemplateGroupId` AS `OrderTemplateGroupId0`
    FROM `OrderTemplateProducts` AS `o0`
    INNER JOIN (
        SELECT `o7`.`OrderTemplateGroupId`, `o7`.`Description`, `o7`.`Name`
        FROM `OrderTemplateGroups` AS `o7`
        WHERE NOT (`o7`.`Deleted`)
    ) AS `t` ON `o0`.`OrderTemplateGroupId` = `t`.`OrderTemplateGroupId`
    INNER JOIN (
        SELECT `d`.`DCProductId`, `d`.`Code`, `d`.`Disabled`, `d`.`DistributionCenterId`, `d`.`EANNumber`, `d`.`EngageVariantProductId`, `d`.`Files`, `d`.`ManufacturerId`, `d`.`Name`, `d`.`PackSize`, `d`.`ProductActiveStatusId`, `d`.`ProductClassId`, `d`.`ProductStatusId`, `d`.`ProductSubWarehouseId`, `d`.`ProductWarehouseStatusId`, `d`.`Size`, `d`.`SubWarehouse`, `d`.`UnitTypeId`, `d`.`VendorId`
        FROM `DCProducts` AS `d`
        WHERE NOT (`d`.`Deleted`)
    ) AS `t0` ON `o0`.`DCProductId` = `t0`.`DCProductId`
    LEFT JOIN (
        SELECT `e`.`EngageVariantProductId`, `e`.`Files`, `e`.`Name`
        FROM `EngageVariantProducts` AS `e`
        WHERE NOT (`e`.`Deleted`)
    ) AS `t1` ON `t0`.`EngageVariantProductId` = `t1`.`EngageVariantProductId`
    INNER JOIN (
        SELECT `d0`.`DistributionCenterId`, `d0`.`Name`
        FROM `DistributionCenters` AS `d0`
        WHERE NOT (`d0`.`Deleted`)
    ) AS `t2` ON `t0`.`DistributionCenterId` = `t2`.`DistributionCenterId`
    INNER JOIN (
        SELECT `v`.`VendorId`, `v`.`Name`
        FROM `Vendors` AS `v`
        WHERE NOT (`v`.`Deleted`)
    ) AS `t3` ON `t0`.`VendorId` = `t3`.`VendorId`
    INNER JOIN `opt_DCProductClasses` AS `o1` ON `t0`.`ProductClassId` = `o1`.`Id`
    INNER JOIN `opt_UnitTypes` AS `o2` ON `t0`.`UnitTypeId` = `o2`.`Id`
    INNER JOIN `opt_ProductActiveStatuses` AS `o3` ON `t0`.`ProductActiveStatusId` = `o3`.`Id`
    INNER JOIN `opt_ProductStatuses` AS `o4` ON `t0`.`ProductStatusId` = `o4`.`Id`
    INNER JOIN `opt_ProductWarehouseStatuses` AS `o5` ON `t0`.`ProductWarehouseStatusId` = `o5`.`Id`
    INNER JOIN (
        SELECT `s`.`SubWarehouseId`, `s`.`Name`
        FROM `SubWarehouses` AS `s`
        WHERE NOT (`s`.`Deleted`)
    ) AS `t4` ON `t0`.`ProductSubWarehouseId` = `t4`.`SubWarehouseId`
    WHERE NOT (`o0`.`Deleted`)
) AS `t5` ON `o`.`OrderTemplateGroupId` = `t5`.`OrderTemplateGroupId0`
WHERE NOT (`o`.`Deleted`) AND (`o`.`OrderTemplateId` = 2)
ORDER BY `o`.`OrderTemplateGroupId`, `t5`.`OrderTemplateProductId`, `t5`.`OrderTemplateGroupId`, `t5`.`DCProductId`, `t5`.`EngageVariantProductId0`, `t5`.`DistributionCenterId0`, `t5`.`VendorId0`, `t5`.`Id`, `t5`.`Id0`, `t5`.`Id1`, `t5`.`Id2`, `t5`.`Id3`;

SELECT `t1`.`OrderTemplateId`, `t1`.`Created`, `t1`.`CreatedBy`, `t1`.`Deleted`, `t1`.`DeletedBy`, `t1`.`DeletedDate`, `t1`.`Disabled`, `t1`.`DistributionCenterId`, `t1`.`EndDate`, `t1`.`Files`, `t1`.`LastModified`, `t1`.`LastModifiedBy`, `t1`.`Name`, `t1`.`Note`, `t1`.`OrderTemplateStatusId`, `t1`.`StartDate`, `t1`.`OrderTemplateStatusId0`, `t1`.`Created0`, `t1`.`CreatedBy0`, `t1`.`Deleted0`, `t1`.`DeletedBy0`, `t1`.`DeletedDate0`, `t1`.`Disabled0`, `t1`.`LastModified0`, `t1`.`LastModifiedBy0`, `t1`.`Name0`, `t1`.`DistributionCenterId0`, `t1`.`Code`, `t1`.`Created1`, `t1`.`CreatedBy1`, `t1`.`Deleted1`, `t1`.`DeletedBy1`, `t1`.`DeletedDate1`, `t1`.`Disabled1`, `t1`.`LastModified1`, `t1`.`LastModifiedBy1`, `t1`.`Name1`, `t2`.`OrderTemplateGroupId`, `t2`.`Created`, `t2`.`CreatedBy`, `t2`.`Deleted`, `t2`.`DeletedBy`, `t2`.`DeletedDate`, `t2`.`Description`, `t2`.`Disabled`, `t2`.`LastModified`, `t2`.`LastModifiedBy`, `t2`.`Name`, `t2`.`Order`, `t2`.`OrderTemplateId`
FROM (
    SELECT `o`.`OrderTemplateId`, `o`.`Created`, `o`.`CreatedBy`, `o`.`Deleted`, `o`.`DeletedBy`, `o`.`DeletedDate`, `o`.`Disabled`, `o`.`DistributionCenterId`, `o`.`EndDate`, `o`.`Files`, `o`.`LastModified`, `o`.`LastModifiedBy`, `o`.`Name`, `o`.`Note`, `o`.`OrderTemplateStatusId`, `o`.`StartDate`, `t`.`OrderTemplateStatusId` AS `OrderTemplateStatusId0`, `t`.`Created` AS `Created0`, `t`.`CreatedBy` AS `CreatedBy0`, `t`.`Deleted` AS `Deleted0`, `t`.`DeletedBy` AS `DeletedBy0`, `t`.`DeletedDate` AS `DeletedDate0`, `t`.`Disabled` AS `Disabled0`, `t`.`LastModified` AS `LastModified0`, `t`.`LastModifiedBy` AS `LastModifiedBy0`, `t`.`Name` AS `Name0`, `t0`.`DistributionCenterId` AS `DistributionCenterId0`, `t0`.`Code`, `t0`.`Created` AS `Created1`, `t0`.`CreatedBy` AS `CreatedBy1`, `t0`.`Deleted` AS `Deleted1`, `t0`.`DeletedBy` AS `DeletedBy1`, `t0`.`DeletedDate` AS `DeletedDate1`, `t0`.`Disabled` AS `Disabled1`, `t0`.`LastModified` AS `LastModified1`, `t0`.`LastModifiedBy` AS `LastModifiedBy1`, `t0`.`Name` AS `Name1`
    FROM `OrderTemplates` AS `o`
    INNER JOIN (
        SELECT `o0`.`OrderTemplateStatusId`, `o0`.`Created`, `o0`.`CreatedBy`, `o0`.`Deleted`, `o0`.`DeletedBy`, `o0`.`DeletedDate`, `o0`.`Disabled`, `o0`.`LastModified`, `o0`.`LastModifiedBy`, `o0`.`Name`
        FROM `OrderTemplateStatuses` AS `o0`
        WHERE NOT (`o0`.`Deleted`)
    ) AS `t` ON `o`.`OrderTemplateStatusId` = `t`.`OrderTemplateStatusId`
    INNER JOIN (
        SELECT `d`.`DistributionCenterId`, `d`.`Code`, `d`.`Created`, `d`.`CreatedBy`, `d`.`Deleted`, `d`.`DeletedBy`, `d`.`DeletedDate`, `d`.`Disabled`, `d`.`LastModified`, `d`.`LastModifiedBy`, `d`.`Name`
        FROM `DistributionCenters` AS `d`
        WHERE NOT (`d`.`Deleted`)
    ) AS `t0` ON `o`.`DistributionCenterId` = `t0`.`DistributionCenterId`
    WHERE NOT (`o`.`Deleted`) AND (`o`.`OrderTemplateId` = 2)
    LIMIT 2
) AS `t1`
LEFT JOIN (
    SELECT `o1`.`OrderTemplateGroupId`, `o1`.`Created`, `o1`.`CreatedBy`, `o1`.`Deleted`, `o1`.`DeletedBy`, `o1`.`DeletedDate`, `o1`.`Description`, `o1`.`Disabled`, `o1`.`LastModified`, `o1`.`LastModifiedBy`, `o1`.`Name`, `o1`.`Order`, `o1`.`OrderTemplateId`
    FROM `OrderTemplateGroups` AS `o1`
    WHERE NOT (`o1`.`Deleted`)
) AS `t2` ON `t1`.`OrderTemplateId` = `t2`.`OrderTemplateId`
ORDER BY `t1`.`OrderTemplateId`, `t1`.`OrderTemplateStatusId0`, `t1`.`DistributionCenterId0`;