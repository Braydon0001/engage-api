CREATE DEFINER=`insight`@`%` PROCEDURE `sp_EmployeeBirthdayWeek`(IN regionId INT)
BEGIN

    SET SESSION group_concat_max_len = 10000;

	INSERT INTO notifications (disabled, deleted, created, notificationTypeId, title, message, startDate, endDate, files ) 
		SELECT  
		0, 0, CURDATE(), 2, "Upcoming birthdays", 
		GROUP_CONCAT(CONVERT(DATE_FORMAT(e.dateOfBirth , '%d/%m'), CHAR)  , " - " , CAP_FIRST(e.firstName) , " " , CAP_FIRST(e.lastName) , "<br />" ORDER BY DATE_FORMAT(e.dateOfBirth , '%d/%m') ASC SEPARATOR " "), 
		CURDATE(), CURDATE() + INTERVAL 6 DAY , ('[{"Url": "/img/notification/Happy-Birthday-Msg_Intranet.jpg", "Name": "Happy-Birthday-Msg_PaySpace.JPG", "Type": null}]')
		FROM Employees e INNER JOIN employeeregions er ON e.EmployeeId = er.EmployeeId
		WHERE DATE_ADD(e.dateOfBirth, 
					INTERVAL YEAR(CURDATE())-YEAR(e.dateOfBirth)
							 + IF(DAYOFYEAR(CURDATE()) > DAYOFYEAR(e.dateOfBirth),1,0)
					YEAR) 
				BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)
		 AND (MONTH(e.dateOfBirth) <> MONTH(CURDATE()) OR DAY(e.dateOfBirth) <> DAY(CURDATE())) AND er.EngageRegionId = regionId;
     
     INSERT INTO notificationregions (NotificationId, EngageRegionId) 
     SELECT n.NotificationId, regionId FROM notifications n WHERE notificationId=(SELECT LAST_INSERT_ID()); 
		
END