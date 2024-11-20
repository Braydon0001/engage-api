CREATE DEFINER=`insight`@`%` PROCEDURE `sp_AssignFiveAndTenYearBadge`()
BEGIN
	
    -- Pass in year anniversary since started and badge id 
    CALL sp_AssignAnniversaryBadge(5, 16);
    CALL sp_AssignAnniversaryBadge(10, 10);
    
END