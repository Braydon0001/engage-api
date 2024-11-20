CREATE VIEW `vw_surveysbyemployeestore` AS
    SELECT 
        `a`.`SurveyId` AS `SurveyId`,
        `a`.`StoreId` AS `StoreId`,
        `c`.`EmployeeId` AS `EmployeeId`,
        `c`.`EngageSubGroupId` AS `EngageSubGroupId`
    FROM
        ((`surveystores` `a`
        JOIN `surveys` `b` ON ((`a`.`SurveyId` = `b`.`SurveyId`)))
        JOIN `vw_employeeengagesubgroups` `c` ON ((`b`.`EngageSubGroupId` = `c`.`EngageSubGroupId`)))