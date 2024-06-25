CREATE PROCEDURE [dbo].[Get_CategoryByID]
@CategoryID int
AS
BEGIN
      SELECT 
		CategoryID  ,
		CategoryName  ,
		CreatedBy,
		CreatedOn
	  FROM
	     Category
	  WHERE CategoryID=@CategoryID
	  and IsDeleted!=1
END
