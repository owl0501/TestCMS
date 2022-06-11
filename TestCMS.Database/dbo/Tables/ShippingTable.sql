--訂單
Create table ShippingTable
(
	[Id] int not null primary key identity,
	[ShipID] NCHAR(15) NOT NULL,
	[CategoryId] int NOT null,
    [CreateTime] DATETIME NOT NULL,
	foreign key(CategoryId) references CategoryTable(Id)
)
