CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeesubgroupperstoreformat` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `st`.`StoreId` AS `StoreId`,
        `es`.`EmployeeId` AS `EmployeeId`,
        `es`.`EngageSubGroupId` AS `EngageSubGroupId`,
        2 AS `EmployeeTargetingType`,
        2 AS `StoreTargetingType`
    FROM
        (((((`surveystoreformats` `sf`
        JOIN `surveys` `s` ON ((`sf`.`SurveyId` = `s`.`SurveyId`)))
        JOIN `stores` `st` ON ((`sf`.`StoreFormatId` = `st`.`StoreFormatId`)))
        JOIN `vw_employeeengagesubgroups` `es` ON ((`s`.`EngageSubGroupId` = `es`.`EngageSubGroupId`)))
        LEFT JOIN `surveystores` `sust` ON ((`s`.`SurveyId` = `sust`.`SurveyId`)))
        LEFT JOIN `surveyemployees` `suem` ON ((`s`.`SurveyId` = `suem`.`SurveyId`)))
    WHERE
        ((`sust`.`StoreId` IS NULL)
            AND (`suem`.`EmployeeId` IS NULL))