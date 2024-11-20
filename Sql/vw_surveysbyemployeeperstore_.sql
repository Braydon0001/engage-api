CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeeperstore_` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `ss`.`StoreId` AS `StoreId`,
        `se`.`EmployeeId` AS `EmployeeId`,
        `s`.`EngageSubGroupId` AS `EngageSubGroupId`,
        1 AS `EmployeeTargetingType`,
        1 AS `StoreTargetingType`
    FROM
        ((`surveystores` `ss`
        JOIN `surveys` `s` ON ((`ss`.`SurveyId` = `s`.`SurveyId`)))
        JOIN `surveyemployees` `se` ON ((`s`.`SurveyId` = `se`.`SurveyId`)))