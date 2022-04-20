--訂單
Create table OrderList
(
	Id int not null primary key identity,
	Product_id int references Product(Id),
	Amount int,
	OrderID char(15)
)
