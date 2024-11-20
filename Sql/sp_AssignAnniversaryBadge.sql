CREATE DEFINER=`insight`@`%` PROCEDURE `sp_AssignAnniversaryBadge`(IN years INT, IN badgeId INT)
BEGIN

	INSERT IGNORE INTO employeeemployeebadges (EmployeeId, EmployeeBadgeId)
	SELECT e.EmployeeId, badgeId FROM employees e 
	WHERE Year(CURDATE()) -  Year(e.StartingDate) > years 
	OR 
	Year(CURDATE()) -  Year(e.StartingDate) = years 
	AND MONTH(CURDATE()) -  MONTH(e.StartingDate) >= 0
	AND DAY(CURDATE()) -  DAY(e.StartingDate) >= 0;
    
END