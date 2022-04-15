--產品
Create table Product
(
	Id int not null identity,
	Name varchar(20) not null,
	Intro varchar(1000),
	CategoryId int NOT null,
	Image varchar(300),
	ReleaseDate date,
	primary key(Id),
	foreign key(CategoryId) references Category(Id)
)
