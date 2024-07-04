CREATE PROCEDURE [dbo].[Select_Item]
(
@ClientID UNIQUEIDENTIFIER,
@BrandID int,
@CategoryID int
)
AS
BEGIN
      SELECT 
	     i.ItemID,
	     i.ClientID,
		 i.Name,
		 i.Code,
		 i.Description,
		 i.Price,
		 i.ImgURL,
		 i.Stock,
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
	  AND (i.ClientID = @ClientID)
	  AND (@BrandID IS NULL OR @BrandID = 0 OR i.BrandID = @BrandID)
	  AND (@CategoryID IS NULL OR @CategoryID = 0 OR i.CategoryID = @CategoryID)
    Order By i.CreatedOn desc
END
