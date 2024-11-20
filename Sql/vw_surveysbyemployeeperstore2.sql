CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `insight`@`%` 
    SQL SECURITY DEFINER
VIEW `vw_surveysbyemployeeperstore2` AS
    SELECT 
        `s`.`SurveyId` AS `SurveyId`,
        `ss`.`StoreId` AS `StoreId`,
        `es`.`EmployeeId` AS `EmployeeId`,
        `es`.`EngageSubGroupId` AS `EngageSubGroupId`
    FROM
        ((`surveystores` `ss`
        JOIN `surveys` `s` ON ((`ss`.`SurveyId` = `s`.`SurveyId`)))
        JOIN `vw_employeeengagesubgroups` `es` ON ((`s`.`EngageSubGroupId` = `es`.`EngageSubGroupId`)))
    WHERE s.SurveyId NOT IN (SELECT se.SurveyId
                               FROM SurveyEmployees se
                              WHERE  se.SurveyId = s.SurveyId)      