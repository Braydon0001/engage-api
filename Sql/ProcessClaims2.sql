SELECT c.*
FROM `Claims` AS `c`
   LEFT JOIN (SELECT  `s`.`StoreId`, `s`.`EngageRegionId`, `s`.`Name` FROM `Stores` AS `s` WHERE NOT (`s`.`Deleted`)) AS `t` ON `c`.`StoreId` = `t`.`StoreId`
   -- INNER JOIN `opt_EngageRegions` AS `o0` ON `t`.`EngageRegionId` = `o0`.`Id`
   LEFT JOIN (SELECT  `d`.`DistributionCenterId`, `d`.`Name` FROM `DistributionCenters` AS `d` WHERE NOT (`d`.`Deleted`)) AS `t4` ON `c`.`DistributionCenterId` = `t4`.`DistributionCenterId`
  
WHERE
    ((((NOT (`c`.`Deleted`) AND `c`.`IsPayStore`) AND (`c`.`ClaimStatusId` = 3)) AND `c`.`ClaimClassificationId` IN (1)) 
      -- AND (`t`.`EngageRegionId` = 1)
       )
        AND CASE WHEN `c`.`ClaimStatusId` = 2 THEN  (CONVERT( `c`.`UnapprovedDate` , DATE) >= '2023-07-16T00:00:00.0000000')
                AND (CONVERT( `c`.`UnapprovedDate` , DATE) <= '2023-08-15T00:00:00.0000000')
        ELSE (CONVERT( `c`.`ApprovedDate` , DATE) >= '2023-07-16T00:00:00.0000000')
            AND (CONVERT( `c`.`ApprovedDate` , DATE) <= '2023-08-15T00:00:00.0000000')
    END