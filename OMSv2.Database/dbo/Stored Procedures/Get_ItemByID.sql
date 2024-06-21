CREATE PROCEDURE [dbo].[Get_ItemByID]
@ItemID INT
AS
BEGIN
      SELECT 		i.Name  ,
		i.Description  ,
		i.price  ,
		i.ImgURL ,
		i.stock  ,
		i.CategoryID  ,
		i.BrandID  ,		i.CreatedBy,		i.CreatedOn,
		 B.BrandName as BrandName,
		 C.CategoryName as CategoryName
	  FROM
	     Item i
		 left outer join Brand B on B.BrandID = i.BrandID
	    left outer join Category C on C.CategoryID = i.CategoryID
	  WHERE ItemID=@ItemID
END
