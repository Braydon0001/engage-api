CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeeperregion2` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `st`.`StoreId` AS `StoreId`,
        `g`.`EmployeeId` AS `EmployeeId`,
        `s`.`EngageSubGroupId` AS `EngageSubGroupId`
    FROM
        ((((`vw_employeeengagesubgroups` `g`
        LEFT JOIN `surveys` `s` ON ((`g`.`EngageSubGroupId` = `s`.`EngageSubGroupId`)))
        LEFT JOIN `surveystores` `ss` ON ((`s`.`SurveyId` = `ss`.`SurveyId`)))
        LEFT JOIN `surveyengageregions` `sr` ON ((`s`.`SurveyId` = `sr`.`SurveyId`)))
        LEFT JOIN `stores` `st` ON ((`sr`.`EngageRegionId` = `st`.`EngageRegionId`)))
    WHERE
        ((`ss`.`StoreId` IS NULL)
            AND (`st`.`StoreId` IS NOT NULL))