CREATE PROCEDURE [dbo].[Delete_SaleDetails]
@SaleID UNIQUEIDENTIFIER
AS
BEGIN
      Delete 
	  From 
	  SaleDetails
	   WHERE SaleID=@SaleID
END
