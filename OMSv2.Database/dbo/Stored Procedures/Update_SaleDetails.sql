﻿CREATE PROCEDURE [dbo].[Update_SaleDetails]
    @SaleID UNIQUEIDENTIFIER , 
    @ItemID UNIQUEIDENTIFIER  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,
		ItemID =@ItemID, 
		Price=@Price,