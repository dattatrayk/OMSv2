CREATE PROCEDURE [dbo].[Get_ItemByID]
@ItemID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
		i.ItemID,
		i.Name,
		i.Description,
		i.Price,
		i.ImgURL,
		i.Stock,
		i.CategoryID,
		i.BrandID,
		i.CreatedBy,
		i.CreatedOn,
		 B.BrandName as BrandName,
		 C.CategoryName as CategoryName
	  FROM
	     Item i
		 left outer join Brand B on B.BrandID = i.BrandID
	    left outer join Category C on C.CategoryID = i.CategoryID
	  WHERE ItemID=@ItemID
END
