﻿--產品
Create table ProductTable
(
	Id int not null identity,
	Name nvarchar(20) not null,
	Intro nvarchar(1000),
	CategoryId int NOT null,
	[SaveImageUrl] nvarchar(300) NOT NULL,
	[CreateTime] datetime NOT NULL,
	[SupplyStatus] NVARCHAR(2) NOT NULL, 
    primary key(Id),
	foreign key(CategoryId) references CategoryTable(Id)
)
