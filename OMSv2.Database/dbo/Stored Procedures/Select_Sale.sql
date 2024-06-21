CREATE PROCEDURE [dbo].[Select_Sale]
AS
BEGIN
      SELECT 
	     SaleID  , 
		SaleDate,
		CustomerName ,
		ContactNo  ,
		Email  ,
		Address  ,
		TotalAmount  ,
		Quantity ,
		CreatedBy,
		CreatedOn
	  FROM Sale 
	  WHERE 
	  IsDeleted != 1
    Order By CreatedOn desc
END
