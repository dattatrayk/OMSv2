CREATE PROCEDURE [dbo].[Get_CategoryByID]
@CategoryID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 		CategoryID  ,		CategoryName  ,		CreatedBy,		CreatedOn
	  FROM
	     Category
	  WHERE CategoryID=@CategoryID
	  and IsDeleted!=1
END
