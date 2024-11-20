CREATE DEFINER=`insight`@`%` PROCEDURE `sp_getsurveysbyemployeestore`(
IN employeeid int,
IN storeid int)
BEGIN
	CREATE TEMPORARY TABLE tmp_surveys(
		SurveyId INT NOT NULL,
		StoreId INT NOT NULL,
		EmployeeId INT NOT NULL,
		EngageSubGroupId INT NOT NULL
	);
    
    -- STEP 1 - Get surveys dedicated for the store.
    insert into tmp_surveys
	select distinct
		  s.SurveyId
		, ss.StoreId
		, es.EmployeeId
		, es.EngageSubGroupId
	from employeestores es
		join surveys s
			on es.EngageSubGroupId = s.EngageSubGroupId
		join surveystores ss
			on s.SurveyId = ss.SurveyId
	where es.EmployeeId = employeeid
		 and ss.StoreId =storeid
   
   union
   
   -- STEP 2 - Get surveys dedicated for the store format.
    select distinct
		  s.SurveyId
		, st.StoreId
		, es.EmployeeId
		, es.EngageSubGroupId
	from employeestores es
		join surveys s
			on es.EngageSubGroupId = s.EngageSubGroupId
		join surveystoreformats sf
			on s.SurveyId = sf.SurveyId
        join stores st 
            on sf.StoreId = st.StoreId
	where es.EmployeeId = employeeid
		 and st.StoreId = storeid      
         
    union
    -- STEP 2 - Get surveys by region.
	select distinct
		s.SurveyId
		, st.StoreID
		, es.EmployeeId
		, s.EngageSubGroupId
	from surveys s 
		left join surveystores ss			-- get stores linked to surveys, used to eliminate direct links
			on s.SurveyId = ss.SurveyId
		left join surveystoreformats sf     -- get stores linked to surveys by store format, used to eliminate direct links 
			on s.SurveyId = sf.SurveyId
        join stores stf 
            on sf.StoreId = stf.StoreId
		left join surveyengageregions sr	-- link table to regions
			on s.SurveyId = sr.SurveyId
		left join stores st					-- get stores linked to regions assigned to the survey
			on sr.EngageRegionId = st.EngageRegionId
		left join employeestores es
			on s.EngageSubGroupId = es.EngageSubGroupId
	where st.StoreId = storeid
		and es.EmployeeId = employeeid
		and ss.StoreId is null 			-- These surveys are not linked to stores, only regions.
        and stf.StoreId is null; 		-- These surveys are not linked to store formats, only regions.
    
    select 
		tmp.EmployeeId
		,tmp.StoreId
        ,st.Name as StoreName
        ,s.SupplierId
        ,sp.Name as SupplierName
        ,tmp.SurveyId
        ,s.Title as SurveyTitle
        ,tmp.EngageSubGroupId
		,sg.Name as EngageSubGroupName
        ,b.Id as EngageBrandId
        ,b.Name as EngageBrandName
    from tmp_surveys tmp
		left join surveys s
			on s.SurveyId = tmp.SurveyId
		left join stores st
			on tmp.StoreId = st.StoreId
		left join suppliers sp
			on s.SupplierId = sp.SupplierId
		left join opt_engagesubgroups sg
			on s.EngageSubGroupId = sg.Id
		left join opt_engagebrands b
			on s.EngageBrandId = b.Id
	where 
		s.Deleted = false
        and s.Disabled = false
		and (s.StartDate <= current_date() and s.EndDate >= current_date()) -- TODO: Add check for when date is null. IFNULL()
        and s.SurveyId not in (select s.SurveyId
									from surveyinstances si
										left join surveys s
											on si.SurveyId = s.SurveyId
									where 
										((s.IsRecurring = true and si.SurveyDate = current_date()) or (s.IsRecurring = false))
										and si.EmployeeId = employeeid
										and si.StoreId = storeid);
    
    DROP TEMPORARY TABLE tmp_surveys;
	
END