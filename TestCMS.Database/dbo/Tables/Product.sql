--產品
Create table Product
(
	Id int not null identity,
	Name nvarchar(20) not null,
	Intro nvarchar(1000),
	CategoryId int NOT null,
	Image nvarchar(300),
	ReleaseDate date,
	primary key(Id),
	foreign key(CategoryId) references Category(Id)
)
