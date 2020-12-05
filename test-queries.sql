select * from Customers
select * from Locations
select * from Orders
select * from Products
select * from InventoryItems
select * from OrderItems

insert into Products (Name, Price)
values
('lollipop', 1.00),
('muffin', 2.00),
('TShirt', 5.00)

insert into InventoryItems (LocationId, ProductId, Amount)
values
(3, 7, 10),
(3, 8, 5),
(4, 9, 5),
(4, 7, 20)

insert into Orders