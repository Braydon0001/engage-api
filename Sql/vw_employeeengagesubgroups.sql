CREATE  VIEW `vw_employeeengagesubgroups` AS
    SELECT DISTINCT
        `employeestores`.`EmployeeId` AS `EmployeeId`,
        `employeestores`.`EngageSubGroupId` AS `EngageSubGroupId`
    FROM
        `employeestores`