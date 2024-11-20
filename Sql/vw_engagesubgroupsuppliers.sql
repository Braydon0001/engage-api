CREATE 
VIEW `vw_engagesubgroupsuppliers` AS
    SELECT DISTINCT
        `d`.`Id` AS `SubGroupId`,
        `d`.`Name` AS `SubGroupName`,
        `e`.`SupplierId` AS `SupplierId`,
        `e`.`Name` AS `SupplierName`
    FROM
        ((((`engagemasterproducts` `a`
        JOIN `opt_engagesubcategories` `b` ON ((`a`.`EngageSubCategoryId` = `b`.`Id`)))
        JOIN `opt_engagecategories` `c` ON ((`b`.`EngageCategoryId` = `c`.`Id`)))
        JOIN `opt_engagesubgroups` `d` ON ((`c`.`EngageSubGroupId` = `d`.`Id`)))
        JOIN `suppliers` `e` ON ((`a`.`SupplierId` = `e`.`SupplierId`)))
