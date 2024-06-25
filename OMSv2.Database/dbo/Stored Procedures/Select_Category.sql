CREATE PROCEDURE [dbo].[Select_Category]
@ClientID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
	    CategoryName ,
		CategoryID  ,
		CreatedOn
	  FROM Category 
	  WHERE 
	  IsDeleted != 1
	  and ClientID=@ClientID
    Order By CreatedOn desc
END
