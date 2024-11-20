CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeeperregion` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `st`.`StoreId` AS `StoreId`,
        `se`.`EmployeeId` AS `EmployeeId`,
        `s`.`EngageSubGroupId` AS `EngageSubGroupId`,
        1 AS `EmployeeTargetingType`,
        3 AS `StoreTargetingType`
    FROM
        ((((((`surveyemployees` `se`
        LEFT JOIN `surveys` `s` ON ((`se`.`SurveyId` = `s`.`SurveyId`)))
        LEFT JOIN `surveystores` `ss` ON ((`s`.`SurveyId` = `ss`.`SurveyId`)))
        LEFT JOIN `surveystoreformats` `sf` ON ((`s`.`SurveyId` = `sf`.`SurveyId`)))
        LEFT JOIN `stores` `stf` ON ((`sf`.`StoreFormatId` = `stf`.`StoreFormatId`)))
        LEFT JOIN `surveyengageregions` `sr` ON ((`s`.`SurveyId` = `sr`.`SurveyId`)))
        LEFT JOIN `stores` `st` ON ((`sr`.`EngageRegionId` = `st`.`EngageRegionId`)))
    WHERE
        ((`ss`.`StoreId` IS NULL)
            AND (`stf`.`StoreId` IS NULL)
            AND (`st`.`StoreId` IS NOT NULL))