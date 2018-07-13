﻿CREATE TABLE [dbo].[Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Country] NVARCHAR(100) NOT NULL, 
    [State] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(100) NOT NULL, 
    [PostalCode] NVARCHAR(10) NOT NULL, 
    [AddressLine] NVARCHAR(100) NOT NULL
)
