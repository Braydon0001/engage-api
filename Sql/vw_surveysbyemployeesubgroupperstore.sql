CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeesubgroupperstore` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `ss`.`StoreId` AS `StoreId`,
        `es`.`EmployeeId` AS `EmployeeId`,
        `es`.`EngageSubGroupId` AS `EngageSubGroupId`,
        2 AS `EmployeeTargetingType`,
        1 AS `StoreTargetingType`
    FROM
        (((`surveystores` `ss`
        JOIN `surveys` `s` ON ((`ss`.`SurveyId` = `s`.`SurveyId`)))
        JOIN `vw_employeeengagesubgroups` `es` ON ((`s`.`EngageSubGroupId` = `es`.`EngageSubGroupId`)))
        LEFT JOIN `surveyemployees` `se` ON ((`s`.`SurveyId` = `se`.`SurveyId`)))
    WHERE
        (`se`.`EmployeeId` IS NULL)