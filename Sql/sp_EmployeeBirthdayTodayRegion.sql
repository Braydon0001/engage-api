CREATE DEFINER=`insight`@`%` PROCEDURE `sp_EmployeeBirthdayTodayRegion`()
BEGIN

	-- init loop variables
	DECLARE r_done INT DEFAULT FALSE;
	DECLARE r_id INT;
    DECLARE cursor_r CURSOR FOR SELECT DISTINCT Id FROM opt_engageregions op_er INNER JOIN employeeregions er ON op_er.Id = er.engageRegionId
    INNER JOIN Employees e ON e.EmployeeId = er.EmployeeId
    WHERE DATE_FORMAT(e.dateOfBirth,'%m-%d') = DATE_FORMAT(NOW(),'%m-%d')
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
    
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET r_done = TRUE;
    
    OPEN cursor_r;

    read_loop: LOOP
        FETCH cursor_r INTO r_id;
        IF r_done THEN
          LEAVE read_loop;
        END IF;
        
         -- run birthdays for today for each region sp
        call sp_EmployeeBirthdayToday(r_id);
        
    END LOOP;

    CLOSE cursor_r;
  
END