CREATE PROCEDURE [dbo].[Insert_Sale](	@SaleID UNIQUEIDENTIFIER ,
	@ClientID UNIQUEIDENTIFIER , 
	@SaleDate DATETIME ,
    @CustomerID UNIQUEIDENTIFIER,
    @CustomerName NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @Address NVARCHAR(MAX) ,
    @TotalAmount DECIMAL(10, 2) ,
    @Quantity INT ,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO Sale 	   (	   		SaleID  , 
		ClientID,
		SaleDate,
		CustomerID ,
		CustomerName ,
		ContactNo  ,
		Email  ,
		Address  ,
		TotalAmount  ,
		Quantity  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    @SaleID  , 
		@ClientID,
		@SaleDate,
		@CustomerID,
		@CustomerName ,
		@ContactNo  ,
		@Email  ,
		@Address  ,
		@TotalAmount  ,
		@Quantity  ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END