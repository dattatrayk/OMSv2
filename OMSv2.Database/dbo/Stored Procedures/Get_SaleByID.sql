CREATE PROCEDURE [dbo].[Get_SaleByID]
@SaleID INT
AS
BEGIN
      SELECT 		
		SaleDate ,
		CustomerName ,
		ContactNo,
		Email ,
		Address ,
		TotalAmount ,
		Quantity,		CreatedBy,		CreatedOn
	  FROM
	     Sale
	  WHERE SaleID=@SaleID
END
