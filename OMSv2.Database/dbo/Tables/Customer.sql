CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT IDENTITY(1,1) NOT NULL , 
	ClientID UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(250) NULL, 
    [ContactNo] NVARCHAR(250) NULL, 
    [Email] NVARCHAR(250) NULL, 
    [AddressDetails] NVARCHAR(MAX) NULL,
	CreatedBy UNIQUEIDENTIFIER NULL,
    CreatedOn DATETIME NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    ModifiedOn DATETIME NULL,
    [IsDeleted] BIT NULL DEFAULT 0, 
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerID]),
)
