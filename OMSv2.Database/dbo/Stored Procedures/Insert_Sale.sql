CREATE PROCEDURE [dbo].[Insert_Sale](	--@SaleID INT , 
	@SaleDate DATETIME ,
    @CustomerName NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @Address NVARCHAR(MAX) ,
    @TotalAmount DECIMAL(10, 2) ,
    @Quantity INT ,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO Sale 	   (	   		--SaleID  , 
		SaleDate,
		CustomerName ,
		ContactNo  ,
		Email  ,
		Address  ,
		TotalAmount  ,
		Quantity  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    --@SaleID  , 
		@SaleDate,
		@CustomerName ,
		@ContactNo  ,
		@Email  ,
		@Address  ,
		@TotalAmount  ,
		@Quantity  ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END