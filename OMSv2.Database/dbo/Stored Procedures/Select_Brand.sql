CREATE PROCEDURE [dbo].[Select_Brand]
AS
BEGIN
      SELECT 
	    BrandName ,
		BrandID ,
		CreatedOn
	  FROM Brand 
	  WHERE 
	  IsDeleted != 1
    Order By CreatedOn desc
END
