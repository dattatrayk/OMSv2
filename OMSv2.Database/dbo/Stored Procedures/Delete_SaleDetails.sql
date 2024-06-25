CREATE PROCEDURE [dbo].[Delete_SaleDetails]
@SaleID int
AS
BEGIN
      Delete 
	  From 
	  SaleDetails
	   WHERE SaleID=@SaleID
END
