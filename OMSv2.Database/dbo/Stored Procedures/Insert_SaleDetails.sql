CREATE PROCEDURE [dbo].[Insert_SaleDetails]
(
	--@SaleDetailID int , 
    @SaleID int , 
    @ItemID int  , 
    @Price DECIMAL(10, 2) ,
    @Quantity INT,
    @CreatedBy UNIQUEIDENTIFIER 
    
)

AS
BEGIN
       INSERT INTO SaleDetails 
	   (
	   
		--SaleDetailID , 
		SaleID , 
		ItemID , 
		Price,
		Quantity ,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn
	   )
       VALUES 
	   (
	   
	    --@SaleDetailID , 
		@SaleID , 
		@ItemID , 
		@Price,
		@Quantity ,
       @CreatedBy,
	   GETUTCDATE(), 
	   @CreatedBy,
	   GETUTCDATE()
	   )

	   Update Item 
	   set
	   Stock=Stock-@Quantity
	   where ItemID=@ItemID
END
