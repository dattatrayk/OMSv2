CREATE PROCEDURE [dbo].[Update_SaleDetails](	@SaleDetailsID INT , 
    @SaleID INT , 
    @ItemID INT  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,	@ModifiedBy	    UNIQUEIDENTIFIER)ASBEGIN       UPDATE SaleDetails	   SET	    ModifiedBy=@ModifiedBy,		SaleID=@SaleID , 
		ItemID =@ItemID, 
		Price=@Price,		Quantity=@Quantity ,	    ModifiedOn=GETUTCDATE()	   WHERE SaleDetailsID=@SaleDetailsIDEND