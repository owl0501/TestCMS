﻿CREATE TABLE [dbo].[CartTable]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT references ProductTable(Id) NOT NULL,
	[Amount] INT NOT NULL, 
    [ShipStatus] NCHAR(2) NOT NULL DEFAULT 'no'
)
