CREATE PROCEDURE [dbo].[Update_SaleDetails](	@SaleDetailID UNIQUEIDENTIFIER , 
    @SaleID UNIQUEIDENTIFIER , 
    @ItemID UNIQUEIDENTIFIER  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,	@ModifiedBy	    UNIQUEIDENTIFIER)ASBEGIN       UPDATE SaleDetails	   SET	    ModifiedBy=@ModifiedBy,		SaleID=@SaleID , 
		ItemID =@ItemID, 
		Price=@Price,		Quantity=@Quantity ,	    ModifiedOn=GETUTCDATE()	   WHERE SaleDetailID=@SaleDetailIDEND