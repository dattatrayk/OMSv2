CREATE PROCEDURE [dbo].[Insert_SaleDetails](	 --@SaleDetailsID INT , 
    @SaleID INT , 
    @ItemID INT  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO SaleDetails 	   (	   		--SaleDetailsID , 
		SaleID , 
		ItemID , 
		Price,		Quantity ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    --@SaleDetailsID , 
		@SaleID , 
		@ItemID , 
		@Price,		@Quantity ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END