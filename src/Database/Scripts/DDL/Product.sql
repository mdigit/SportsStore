use SportsStore
go

CREATE TABLE [dbo].[Products]
(
	[ProductID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(500) NOT NULL,
	[Catagory] NVARCHAR(50) NOT NULL,
	[Price] DECIMAL(16,2) NOT NULL
)
