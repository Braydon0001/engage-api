select * from ordertemplates;
select * from ordertemplategroups;
select * from ordertemplateproducts;

-- order 197
-- order with template 196

select OrderTemplateId, orders.* from orders where OrderTemplateId is not null;
select OrderTemplateProductId, orderskus.* from orderskus where OrderTemplateProductId is not null;