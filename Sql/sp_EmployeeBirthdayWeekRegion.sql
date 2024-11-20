CREATE DEFINER=`insight`@`%` PROCEDURE `sp_EmployeeBirthdayWeekRegion`()
BEGIN

	-- init loop variables
	DECLARE r_done INT DEFAULT FALSE;
	DECLARE r_id INT;
    DECLARE cursor_r CURSOR FOR SELECT DISTINCT Id FROM opt_engageregions op_er INNER JOIN employeeregions er ON op_er.Id = er.engageRegionId
    INNER JOIN employees e ON e.EmployeeId = er.EmployeeId
		WHERE DATE_ADD(e.dateOfBirth, 
					INTERVAL YEAR(CURDATE())-YEAR(e.dateOfBirth)
							 + IF(DAYOFYEAR(CURDATE()) > DAYOFYEAR(e.dateOfBirth),1,0)
					YEAR) 
				BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)
		 AND (MONTH(e.dateOfBirth) <> MONTH(CURDATE()) OR DAY(e.dateOfBirth) <> DAY(CURDATE()));
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET r_done = TRUE;
    
    OPEN cursor_r;

    read_loop: LOOP
        FETCH cursor_r INTO r_id;
        IF r_done THEN
          LEAVE read_loop;
        END IF;
        
         -- run birthdays for the week sp
        call sp_EmployeeBirthdayWeek(r_id);
        
    END LOOP;

    CLOSE cursor_r;
  
END