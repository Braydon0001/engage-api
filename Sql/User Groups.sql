select b.UserId, b.FirstName, b.LastName, b.Email
,e.EmployeeId, e.Code
,g.Name as JobTitle
,i.Name as EngageRegion 
, k.Name as EngageDepartment
from userusergroups a
join users b on a.UserId = b.UserId 
join usergroups c on a.UserGroupId = c.UserGroupId 
right join employees e on b.UserId = e.UserId 
left join employeeemployeejobtitles f on e.EmployeeId = f.EmployeeId
left join employeejobtitles g on f.EmployeeJobTitleId = g.EmployeeJobTitleId
left join employeeregions h on e.EmployeeId = h.EmployeeId
left join opt_engageregions i on h.EngageRegionId = i.Id
left join employeedepartments j on e.EmployeeId = j.EmployeeId
left join opt_engagedepartments k on j.EngageDepartmentId = k.Id
where c.UserGroupId IN (26) 
and e.EndDate Is Null
order by b.Email;  
   
 