CREATE PROCEDURE [dbo].[Insert_SaleDetails](	@SaleDetailID UNIQUEIDENTIFIER , 
    @SaleID UNIQUEIDENTIFIER , 
    @ItemID UNIQUEIDENTIFIER  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO SaleDetails 	   (	   		SaleDetailID , 
		SaleID , 
		ItemID , 
		Price,		Quantity ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    @SaleDetailID , 
		@SaleID , 
		@ItemID , 
		@Price,		@Quantity ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END