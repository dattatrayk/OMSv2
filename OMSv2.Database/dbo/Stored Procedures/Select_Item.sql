CREATE PROCEDURE [dbo].[Select_Item]
(
@ClientID UNIQUEIDENTIFIER,
@BrandID UNIQUEIDENTIFIER,
@CategoryID UNIQUEIDENTIFIER
)
AS
BEGIN
      SELECT 
	     i.ItemID,
	     i.ClientID,
		 i.Name,
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
	  AND (@ClientID IS NULL OR @ClientID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR i.ClientID = @ClientID)
	  AND (@BrandID IS NULL OR @BrandID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR i.BrandID = @BrandID)
	  AND (@CategoryID IS NULL OR @CategoryID = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) OR i.CategoryID = @CategoryID)
    Order By i.CreatedOn desc
END
