CREATE PROCEDURE [dbo].[Get_SaleDetailsByID]
@SaleDetailID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 		
		sd.SaleDetailID , 
		sd.SaleID , 
		sd.ItemID , 
		sd.Price,
		sd.Quantity ,		sd.CreatedBy,		sd.CreatedOn,
		S.CustomerName AS SaleName,
		i.Name as ItemName
	  FROM
	     SaleDetails sd
	  left outer join Sale S on S.SaleID = sd.SaleID
	  left outer join Item i on i.ItemID = sd.ItemID
	  WHERE sd.SaleDetailID=@SaleDetailID
END
