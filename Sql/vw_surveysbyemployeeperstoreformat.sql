CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeeperstoreformat` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `st`.`StoreId` AS `StoreId`,
        `se`.`EmployeeId` AS `EmployeeId`,
        `s`.`EngageSubGroupId` AS `EngageSubGroupId`,
        1 AS `EmployeeTargetingType`,
        2 AS `StoreTargetingType`
    FROM
        (((`surveystoreformats` `sf`
        JOIN `surveys` `s` ON ((`sf`.`SurveyId` = `s`.`SurveyId`)))
        JOIN `stores` `st` ON ((`sf`.`StoreFormatId` = `st`.`StoreFormatId`)))
        JOIN `surveyemployees` `se` ON ((`s`.`SurveyId` = `se`.`SurveyId`)))