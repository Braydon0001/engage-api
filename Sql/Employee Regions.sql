select a.EmailAddress1, c.Name, a.EmployeeId, a.FirstName, a.LastName,  b.EngageRegionId from employees a 
left join employeeregions b on a.EmployeeId = b.EmployeeId
left join opt_engageregions c on b.EngageRegionId = c.Id;