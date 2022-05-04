CREATE TABLE [dbo].[CartTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Product_id] INT references ProductTable(Id),
	[Amount] INT
)
