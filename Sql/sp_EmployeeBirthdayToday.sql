CREATE DEFINER=`insight`@`%` PROCEDURE `sp_EmployeeBirthdayToday`(IN regionId INT)
BEGIN

SET SESSION group_concat_max_len = 10000;

	INSERT INTO notifications (disabled, deleted, created, notificationTypeId, title, message, startDate, endDate, files ) 
	SELECT  
	0, 0, CURDATE(), 2, "Happy Birthday!", 
    CONCAT("From all of us at Engage, we'd like to wish  <br/><br />" , GROUP_CONCAT( CAP_FIRST(e.firstName) , " " , CAP_FIRST(e.lastName) SEPARATOR ", <br/>") , "<br /><br /> a very happy birthday!"), 
    CURDATE(), CURDATE(),  ('[{"Url": "https://kodelabengagewebdev.blob.core.windows.net/generic/Happy-Birthday-Msg_Intranet.jpg", "Name": "Happy-Birthday-Msg_PaySpace.JPG", "Type": null}]')
	FROM Employees e INNER JOIN employeeregions er ON e.EmployeeId = er.EmployeeId
    WHERE er.EngageRegionId = regionId AND DATE_FORMAT(e.dateOfBirth,'%m-%d') = DATE_FORMAT(NOW(),'%m-%d')
     OR (
            (
                DATE_FORMAT(NOW(),'%Y') % 4 <> 0
                OR (
                        DATE_FORMAT(NOW(),'%Y') % 100 = 0
                        AND DATE_FORMAT(NOW(),'%Y') % 400 <> 0
                    )
            )
            AND DATE_FORMAT(NOW(),'%m-%d') = '03-01'
            AND DATE_FORMAT(e.dateOfBirth,'%m-%d') = '02-29'
        );
        
     INSERT INTO notificationregions (NotificationId, EngageRegionId) 
     SELECT n.NotificationId, regionId FROM notifications n WHERE notificationId=(SELECT LAST_INSERT_ID());
     
END