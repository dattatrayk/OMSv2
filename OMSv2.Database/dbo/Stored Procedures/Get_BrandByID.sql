CREATE PROCEDURE [dbo].[Get_BrandByID]
@BrandID INT
AS
BEGIN
      SELECT 		BrandName,  		CreatedBy,		CreatedOn
	  FROM
	     Brand
	  WHERE BrandID=@BrandID
END
