CREATE PROCEDURE [dbo].[Get_BrandByID]
@BrandID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 		BrandID,  		BrandName,  		CreatedBy,		CreatedOn
	  FROM
	     Brand
	  WHERE BrandID=@BrandID
END
