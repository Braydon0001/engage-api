delimiter |

-- DROP EVENT event_stats_storesbyregion;
CREATE EVENT event_stats_storesbyregion
ON SCHEDULE EVERY 1 DAY
STARTS TIMESTAMP(CURRENT_DATE) + INTERVAL 1 DAY
DO
BEGIN
	SET SQL_SAFE_UPDATES = 0;
	DELETE FROM statsstoresbyregions;
	INSERT INTO statsstoresbyregions (EngageRegionId, Stores, CreatedDate)
	SELECT DISTINCT e.Id, COUNT(s.EngageRegionId) AS Stores, CURRENT_TIMESTAMP AS CreatedDate
	FROM opt_engageregions AS e
	LEFT OUTER JOIN stores AS s
	ON e.Id = s.EngageRegionId
	GROUP BY e.Name;
    SET SQL_SAFE_UPDATES = 1;
END |

delimiter ;

delimiter |

-- DROP EVENT event_stats_ordersbyregion;
CREATE EVENT event_stats_ordersbyregion
ON SCHEDULE EVERY 1 DAY
STARTS TIMESTAMP(CURRENT_DATE) + INTERVAL 1 DAY
DO
BEGIN
	SET SQL_SAFE_UPDATES = 0;
	DELETE FROM statsordersbyregions;
	INSERT INTO statsordersbyregions (EngageRegionId, OrdersLast1Day, OrdersLast7Days, OrdersAll, CreatedDate)
	SELECT DISTINCT e.id,
		IFNULL((
			SELECT COUNT(o1.OrderId)
			FROM stores AS s1
			LEFT OUTER JOIN orders AS o1
			ON o1.StoreId = s1.StoreId
			WHERE OrderDate > DATE_SUB(NOW(), INTERVAL 1 DAY)
			AND s1.EngageRegionId = s.EngageRegionId
		), 0) AS Last1Day,
		IFNULL((
			SELECT COUNT(o1.OrderId)
			FROM stores AS s1
			LEFT OUTER JOIN orders AS o1
			ON o1.StoreId = s1.StoreId
			WHERE OrderDate > DATE_SUB(NOW(), INTERVAL 7 DAY)
			AND s1.EngageRegionId = s.EngageRegionId
		), 0) AS Last7Days,
		COUNT(o.OrderDate) AS AllOrders,
		CURRENT_TIMESTAMP AS CreatedDate
	FROM opt_engageregions AS e
	LEFT OUTER JOIN stores AS s
	ON s.EngageRegionId = e.Id
	LEFT OUTER JOIN orders AS o
	ON o.StoreId = s.StoreId
	GROUP BY e.Name;
    SET SQL_SAFE_UPDATES = 1;
END |

delimiter ;

SELECT * FROM statsstoresbyregions;
SELECT * FROM statsordersbyregions;
SHOW EVENTS FROM `engage-dev`;