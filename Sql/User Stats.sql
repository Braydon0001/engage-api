SELECT count(*) from users;
SELECT a.SupplierId, b.Name, COUNT(*) as Count  FROM Users a LEFT JOIN suppliers b ON a.SupplierId = b.SupplierID  GROUP BY a.SupplierId, b.Name;
SELECT a.OrganizationId, b.Name, COUNT(*) as Count FROM Users a LEFT JOIN organizations b ON a.OrganizationId = b.OrganizationId GROUP BY a.OrganizationId, b.Name;
SELECT a.RoleId, b.Name, COUNT(*) as Count FROM Users a LEFT JOIN userroles b ON a.UserRoleId = b.UserRoleId GROUP BY a.UserRoleId, b.Name;
