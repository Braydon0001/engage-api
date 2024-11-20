CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `vw_surveystores` AS
    SELECT 
        `a`.`SurveyId` AS `SurveyId`, `b`.`StoreId` AS `StoreId`
    FROM
        (`surveys` `a`
        JOIN `surveystores` `b` ON ((`a`.`SurveyId` = `b`.`SurveyId`))) 
    UNION SELECT 
        `a`.`SurveyId` AS `SurveyId`, `c`.`StoreId` AS `StoreId`
    FROM
        ((`surveys` `a`
        JOIN `surveyengageregions` `b` ON ((`b`.`SurveyId` = `a`.`SurveyId`)))
        JOIN `stores` `c` ON ((`c`.`EngageRegionId` = `b`.`EngageRegionId`)))