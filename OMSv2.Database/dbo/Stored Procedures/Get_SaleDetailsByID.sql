CREATE PROCEDURE [dbo].[Get_SaleDetailsByID]
@SaleDetailsID INT
AS
BEGIN
      SELECT 		
		sd.SaleDetailsID , 
		sd.SaleID , 
		sd.ItemID , 
		sd.Price,
		sd.Quantity ,		sd.CreatedBy,		sd.CreatedOn,
		S.CustomerName AS SaleName,
		i.Name as ItemName
	  FROM
	     SaleDetails sd
	  left outer join sale S on S.SaleID = sd.SaleID
	  left outer join Item i on i.ItemID = sd.ItemID
	  WHERE sd.SaleDetailsID=@SaleDetailsID
END
