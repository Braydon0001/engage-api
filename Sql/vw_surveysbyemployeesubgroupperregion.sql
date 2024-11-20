CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeesubgroupperregion` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `st`.`StoreId` AS `StoreId`,
        `g`.`EmployeeId` AS `EmployeeId`,
        `s`.`EngageSubGroupId` AS `EngageSubGroupId`,
        2 AS `EmployeeTargetingType`,
        3 AS `StoreTargetingType`
    FROM
        (((((((`vw_employeeengagesubgroups` `g`
        LEFT JOIN `surveys` `s` ON ((`g`.`EngageSubGroupId` = `s`.`EngageSubGroupId`)))
        LEFT JOIN `surveystores` `ss` ON ((`s`.`SurveyId` = `ss`.`SurveyId`)))
        LEFT JOIN `surveystoreformats` `sf` ON ((`s`.`SurveyId` = `sf`.`SurveyId`)))
        LEFT JOIN `stores` `stf` ON ((`sf`.`StoreFormatId` = `stf`.`StoreFormatId`)))
        LEFT JOIN `surveyengageregions` `sr` ON ((`s`.`SurveyId` = `sr`.`SurveyId`)))
        LEFT JOIN `stores` `st` ON ((`sr`.`EngageRegionId` = `st`.`EngageRegionId`)))
        LEFT JOIN `surveyemployees` `se` ON ((`s`.`SurveyId` = `sr`.`SurveyId`)))
    WHERE
        ((`ss`.`StoreId` IS NULL)
            AND (`stf`.`StoreId` IS NULL)
            AND (`st`.`StoreId` IS NOT NULL)
            AND (`se`.`EmployeeId` IS NULL))