CREATE PROCEDURE [dbo].[Select_Brand]
@ClientID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
	    BrandName ,
		BrandID ,
		CreatedOn
	  FROM Brand 
	  WHERE 
	  IsDeleted != 1
	  and ClientID=@ClientID
    Order By CreatedOn desc
END
