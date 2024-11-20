select * from productyears;
select * from productperiods where ProductYearId = 1; 
select distinct ProductId, ProductWarehouseId from productwarehousesummaries;

select a.ProductId,
       b.Name as Product,        
       a.ProductPeriodId, 
       sum(a.Quantity) 
  from productwarehousesummaries a
 left join products b on a.ProductId = b.ProductId    
 where a.ProductId IN (1) and a.ProductPeriodId IN (select ProductPeriodId from productperiods where ProductYearId = 1)
 group by a.ProductId, ProductPeriodId;
 
select * 
 from productwarehousesummaries 
 where ProductId = 1 and ProductPeriodId = 1;
 

