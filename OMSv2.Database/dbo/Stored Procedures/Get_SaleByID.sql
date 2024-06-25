CREATE PROCEDURE [dbo].[Get_SaleByID]
@SaleID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
		
		SaleDate ,
		CustomerName ,
		ContactNo,
		Email ,
		Address ,
		TotalAmount ,
		Quantity,
		CreatedBy,
		CreatedOn
	  FROM
	     Sale
	  WHERE SaleID=@SaleID
	  and IsDeleted!=1
END
