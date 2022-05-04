--訂單
Create table ShippingTable
(
	Id int not null primary key identity,
	Product_id int references ProductTable(Id) NOT NULL,
	Amount int NOT NULL,
	ShipID NCHAR(15) NOT NULL
)
