CREATE TABLE [dbo].[Cart]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Product_id] INT references Product(Id),
	[Amount] INT
)
