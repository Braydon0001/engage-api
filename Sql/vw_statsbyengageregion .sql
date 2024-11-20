ALTER VIEW vw_statsbyengageregion AS
SELECT r.Id as EngageRegionId 
	  ,r.Name as EngageRegionName
      ,(SELECT count(*) FROM Stores s  WHERE r.Id = s.EngageRegionId AND s.Deleted = 0 AND s.Disabled = 0) as StoresCount
      ,(SELECT count(*) FROM Orders o JOIN Stores s ON o.StoreId = s.StoreId 			
            WHERE o.Created <= NOW() - INTERVAL 1 DAY AND o.OrderStatusId = 1 AND r.Id = s.EngageRegionId  AND o.Deleted = 0 AND o.Disabled = 0) as OverdueUnsubmittedOrdersCount
      ,(SELECT count(*) FROM Orders o JOIN Stores s ON o.StoreId = s.StoreId 			
            WHERE o.OrderStatusId = 1 AND r.Id = s.EngageRegionId  AND o.Deleted = 0 AND o.Disabled = 0) as UnsubmittedOrdersCount
      ,(SELECT count(*) FROM Orders o JOIN Stores s ON o.StoreId = s.StoreId 
			WHERE o.OrderStatusId = 2 AND r.Id = s.EngageRegionId  AND o.Deleted = 0 AND o.Disabled = 0) as SubmittedOrdersCount      
  FROM opt_EngageRegions r  
 WHERE r.Deleted = 0 AND r.Disabled = 0
   AND r.Id NOT IN (7, 10);  -- Exclude Central/Soouth West Africa. They have no stores.
  