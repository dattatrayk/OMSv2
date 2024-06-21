CREATE PROCEDURE [dbo].[Select_Category]
AS
BEGIN
      SELECT 
	    CategoryName ,
		CategoryID  ,
		CreatedOn
	  FROM Category 
	  WHERE 
	  IsDeleted != 1
    Order By CreatedOn desc
END
