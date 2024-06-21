CREATE PROCEDURE [dbo].[Select_Item]
AS
BEGIN
      SELECT 
	     i.ItemID,
		 i.Name,
		 i.Description,
		 i.price,
		 i.ImgURL,
		 i.stock,
		 i.ImgURL,
		 i.CategoryID,
		 i.BrandID,
		 B.BrandName as BrandName,
		 C.CategoryName as CategoryName,
		 i.CreatedOn
	  FROM Item i
	  left outer join Brand B on B.BrandID = i.BrandID
	  left outer join Category C on C.CategoryID = i.CategoryID
	  WHERE 
	  i.IsDeleted != 1
    Order By i.CreatedOn desc
END
