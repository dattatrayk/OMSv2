CREATE PROCEDURE [dbo].[Update_Sale]
(
	@SaleID int , 
	@SaleDate DATETIME ,
    @CustomerName NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @Address NVARCHAR(MAX) ,
    @TotalAmount DECIMAL(10, 2) ,
    @Quantity INT ,
	@ModifiedBy	    UNIQUEIDENTIFIER
)

AS
BEGIN
       UPDATE Sale
	   SET
	    ModifiedBy=@ModifiedBy,
		SaleDate=@SaleDate,
		CustomerName=@CustomerName,
		ContactNo=@ContactNo  ,
		Email=@Email  ,
		Address=@Address  ,
		TotalAmount=@TotalAmount  ,
		Quantity =@Quantity, 
	    ModifiedOn=GETUTCDATE()
	   WHERE SaleID=@SaleID
END
