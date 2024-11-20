      SELECT 
    `c`.`ClaimId`, `c`.`ClientTypeId`, `c`.`ClaimTypeId`,
--     COALESCE(`t0`.`Name`, ''),
     `c`.`ClaimClassificationId`,
--     COALESCE(`t1`.`Name`, ''),
     `c`.`VatId`,
--     `t2`.`Code`,
--     `t2`.`Name`,
     `c`.`SupplierId`,
--     COALESCE(`t3`.`Name`, ''),
     `c`.`StoreId`,
     `c`.`DistributionCenterId`,
--     COALESCE(`t4`.`Name`, ''),
--     COALESCE(`t`.`Name`, ''),
--     COALESCE(`o0`.`Name`, ''),
     `c`.`ClaimAccountManagerId`,
--     COALESCE(CASE
--                 WHEN `c`.`ClaimAccountManagerId` IS NOT NULL THEN `t5`.`FullName`
--                 ELSE ''
--             END,
--             ''),
     `c`.`ClaimManagerId`,
--     COALESCE(CASE
--                 WHEN `c`.`ClaimManagerId` IS NOT NULL THEN `t6`.`FullName`
--                 ELSE ''
--             END,
--             ''),
     `c`.`ClaimFloatId`,
--     COALESCE(`t7`.`Title`, ''),
     `c`.`ClaimStatusId`,
--     COALESCE(`o1`.`Name`, ''),
     `c`.`ClaimSupplierStatusId`,
--     COALESCE(`o2`.`Name`, ''),
--     COALESCE(`c`.`ClaimNumber`, ''),
     `c`.`IsPayStore`,
     `c`.`IsClaimFromSupplier`,
     `c`.`IsVatInclusive`,
     `c`.`IsDairy`,
     `c`.`ClaimDate`,
     COALESCE(`c`.`ClaimReference`, ''),
     COALESCE(`c`.`Comment`, ''),
     `c`.`ApprovedDate`,
     COALESCE(`c`.`ApprovedBy`, ''),
     `c`.`ClaimRejectedReasonId`,
     `c`.`RejectedDate`,
     COALESCE(`c`.`RejectedBy`, ''),
     COALESCE(`c`.`RejectedReason`, ''),
     `c`.`PendingDate`,
     COALESCE(`c`.`PendingBy`, ''),
     `c`.`ClaimPendingReasonId`,
     COALESCE(`c`.`PendingReason`, ''),
     `c`.`PaidDate`,
     COALESCE(`c`.`PaidBy`, ''),
     `c`.`Created`,
     COALESCE(`c`.`CreatedBy`, '') -- , 
--     `t`.`StoreId`,
--     `o`.`Id`,
--     `t0`.`ClaimTypeId`,
--     `t1`.`ClaimClassificationId`,
--     `t2`.`VatId`,
--     `t3`.`SupplierId`,
--     `t4`.`DistributionCenterId`,
--     `o0`.`Id`,
--     `t5`.`UserId`,
--     `t6`.`UserId`,
--     `t7`.`ClaimFloatId`,
--     `o1`.`Id`,
--     `o2`.`Id`,
--     `t8`.`EntityBlobId`,
--     `t8`.`Created`,
--     `t8`.`CreatedBy`,
--     `t8`.`Deleted`,
--     `t8`.`DeletedBy`,
--     `t8`.`DeletedDate`,
--     `t8`.`Disabled`,
--     `t8`.`Discriminator`,
--     `t8`.`FileName`,
--     `t8`.`FolderName`,
--     `t8`.`LastModified`,
--     `t8`.`LastModifiedBy`,
--     `t8`.`OriginalFileName`,
--     `t8`.`Url`,
--     `t8`.`ClaimId`,
    -- (SELECT 
--             COALESCE(SUM(`c3`.`Amount`), 0.0)
--         FROM
--             `ClaimSkus` AS `c3`
--         WHERE
--             (NOT (`c3`.`Deleted`)
--                 AND (`c`.`ClaimId` = `c3`.`ClaimId`))
--                 AND NOT (`c3`.`Deleted`)),
--     (SELECT 
--             COALESCE(SUM(`c4`.`VatAmount`), 0.0)
--         FROM
--             `ClaimSkus` AS `c4`
--         WHERE
--             (NOT (`c4`.`Deleted`)
--                 AND (`c`.`ClaimId` = `c4`.`ClaimId`))
--                 AND NOT (`c4`.`Deleted`)),
--     (SELECT 
--             COALESCE(SUM(`c5`.`TotalAmount`), 0.0)
--         FROM
--             `ClaimSkus` AS `c5`
--         WHERE
--             (NOT (`c5`.`Deleted`)
--                 AND (`c`.`ClaimId` = `c5`.`ClaimId`))
--                 AND NOT (`c5`.`Deleted`))
FROM `Claims` AS `c`
   LEFT JOIN (SELECT  `s`.`StoreId`, `s`.`EngageRegionId`, `s`.`Name` FROM `Stores` AS `s` WHERE NOT (`s`.`Deleted`)) AS `t` ON `c`.`StoreId` = `t`.`StoreId`   
   LEFT JOIN `opt_EngageRegions` AS `o0` ON `t`.`EngageRegionId` = `o0`.`Id`
   
   LEFT JOIN (SELECT  `d`.`DistributionCenterId`, `d`.`Name` FROM `DistributionCenters` AS `d` WHERE NOT (`d`.`Deleted`)) AS `t4` ON `c`.`DistributionCenterId` = `t4`.`DistributionCenterId`
      
   LEFT JOIN `opt_ClientTypes` AS `o` ON `c`.`ClientTypeId` = `o`.`Id`
   LEFT JOIN (SELECT `c0`.`ClaimTypeId`, `c0`.`Name` FROM `ClaimTypes` AS `c0` WHERE NOT (`c0`.`Deleted`)) AS `t0` ON `c`.`ClaimTypeId` = `t0`.`ClaimTypeId`
   LEFT JOIN (SELECT `c1`.`ClaimClassificationId`, `c1`.`Name` FROM `ClaimClassifications` AS `c1` WHERE NOT (`c1`.`Deleted`)) AS `t1` ON `c`.`ClaimClassificationId` = `t1`.`ClaimClassificationId`
   LEFT JOIN (SELECT `v`.`VatId`, `v`.`Code`, `v`.`Name` FROM `Vat` AS `v` WHERE NOT (`v`.`Deleted`)) AS `t2` ON `c`.`VatId` = `t2`.`VatId`
   LEFT JOIN (SELECT `s0`.`SupplierId`, `s0`.`Name` FROM `Suppliers` AS `s0` WHERE NOT (`s0`.`Deleted`)) AS `t3` ON `c`.`SupplierId` = `t3`.`SupplierId`
   LEFT JOIN `opt_ClaimStatuses` AS `o1` ON `c`.`ClaimStatusId` = `o1`.`Id`
   LEFT JOIN `opt_ClaimSupplierStatuses` AS `o2` ON `c`.`ClaimSupplierStatusId` = `o2`.`Id`
   
   LEFT JOIN (SELECT `u`.`UserId`, `u`.`FullName` FROM `Users` AS `u` WHERE NOT (`u`.`Deleted`)) AS `t5` ON `c`.`ClaimAccountManagerId` = `t5`.`UserId`
   LEFT JOIN (SELECT  `u0`.`UserId`, `u0`.`FullName` FROM `Users` AS `u0` WHERE NOT (`u0`.`Deleted`)) AS `t6` ON `c`.`ClaimManagerId` = `t6`.`UserId`
   LEFT JOIN (SELECT `c2`.`ClaimFloatId`, `c2`.`Title` FROM `ClaimFloats` AS `c2` WHERE NOT (`c2`.`Deleted`)) AS `t7` ON `c`.`ClaimFloatId` = `t7`.`ClaimFloatId`  
   LEFT JOIN (SELECT 
            `e`.`EntityBlobId`,
            `e`.`Created`,
            `e`.`CreatedBy`,
            `e`.`Deleted`,
            `e`.`DeletedBy`,
            `e`.`DeletedDate`,
            `e`.`Disabled`,
            `e`.`Discriminator`,
            `e`.`FileName`,
            `e`.`FolderName`,
            `e`.`LastModified`,
            `e`.`LastModifiedBy`,
            `e`.`OriginalFileName`,
            `e`.`Url`,
            `e`.`ClaimId`
    FROM `EntityBlobs` AS `e` 
    WHERE (`e`.`Discriminator` = 'ClaimBlob')
    AND NOT (`e`.`Deleted`)) AS `t8` ON `c`.`ClaimId` = `t8`.`ClaimId`
WHERE
    ((((NOT (`c`.`Deleted`) AND `c`.`IsPayStore`) AND (`c`.`ClaimStatusId` = 3)) AND `c`.`ClaimClassificationId` IN (1)) 
       AND (`t`.`EngageRegionId` = 1)
       )
        AND CASE WHEN `c`.`ClaimStatusId` = 2 THEN  (CONVERT( `c`.`UnapprovedDate` , DATE) >= '2023-07-16T00:00:00.0000000')
                AND (CONVERT( `c`.`UnapprovedDate` , DATE) <= '2023-08-15T00:00:00.0000000')
        ELSE (CONVERT( `c`.`ApprovedDate` , DATE) >= '2023-07-16T00:00:00.0000000')
            AND (CONVERT( `c`.`ApprovedDate` , DATE) <= '2023-08-15T00:00:00.0000000')
    END