CREATE PROCEDURE [dbo].[Get_SaleByID]
@SaleID int
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


	  SELECT 
	     sd.SaleDetailID , 
		sd.SaleID , 
		sd.ItemID , 
		sd.Price,
		sd.Quantity ,
		i.Name as ItemName
	  FROM SaleDetails sd
	  left outer join Item i on i.ItemID = sd.ItemID
	  WHERE 
	  sd.IsDeleted != 1
	  and sd.SaleID=@SaleID
    Order By sd.CreatedOn desc 
END
