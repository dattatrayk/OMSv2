﻿CREATE TABLE SaleDetails
(
    SaleDetailID UNIQUEIDENTIFIER  NOT NULL, 
    SaleID UNIQUEIDENTIFIER  NOT NULL, 
    ItemID UNIQUEIDENTIFIER  NOT NULL, 
    Price DECIMAL(10, 2) NULL,
    Quantity INT NULL,
    CreatedBy UNIQUEIDENTIFIER NULL,
    CreatedOn DATETIME NULL,
    ModifiedBy UNIQUEIDENTIFIER NULL,
    ModifiedOn DATETIME NULL,
    [IsDeleted] BIT NULL DEFAULT 0,
	CONSTRAINT [FK_SaleDetails_SaleID] FOREIGN KEY (SaleID)  REFERENCES [Sale](SaleID),
	CONSTRAINT [FK_SaleDetails_ItemID] FOREIGN KEY (ItemID)  REFERENCES [Item](ItemID)
);

