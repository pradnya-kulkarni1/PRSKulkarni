use PRSKulkarni

select * from LineItem

Select * from Request

select											
r.Id, Description, li.Quantity, p.Price, li.Quantity*p.Price,r.Total
from Request r join 
LineItem li on li.RequestId = r.Id
Join Product p on p.ID = li.ProductId
Where r.Id = 2;

select 
sum(li.Quantity*p.Price)
from Request r join 
LineItem li on li.RequestId = r.Id
Join Product p on p.ID = li.ProductId
Where r.Id = 2;