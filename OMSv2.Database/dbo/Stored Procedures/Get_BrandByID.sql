CREATE PROCEDURE [dbo].[Get_BrandByID]
@BrandID int
AS
BEGIN
      SELECT 
		BrandID,  
		BrandName,  
		CreatedBy,
		CreatedOn
	  FROM
	     Brand
	  WHERE BrandID=@BrandID
	  and IsDeleted!=1
END
