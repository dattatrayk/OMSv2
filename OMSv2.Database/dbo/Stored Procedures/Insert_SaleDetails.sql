﻿CREATE PROCEDURE [dbo].[Insert_SaleDetails]
    @SaleID UNIQUEIDENTIFIER , 
    @ItemID UNIQUEIDENTIFIER  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,
    @CreatedBy UNIQUEIDENTIFIER 
    
		SaleID , 
		ItemID , 
		Price,
		@SaleID , 
		@ItemID , 
		@Price,