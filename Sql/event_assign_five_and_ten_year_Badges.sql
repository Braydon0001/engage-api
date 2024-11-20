CREATE DEFINER=`insight`@`%` EVENT `assign_five_and_ten_year_Badges` 
	ON SCHEDULE 
    EVERY 1 DAY 
    STARTS '2023-03-24 00:00:00' 
    ON COMPLETION NOT PRESERVE ENABLE 
    DO 
		CALL sp_AssignFiveAndTenYearBadge()