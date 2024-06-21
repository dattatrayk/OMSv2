CREATE PROCEDURE [dbo].[Get_CategoryByID]
@CategoryID INT
AS
BEGIN
      SELECT 		CategoryName  ,		CreatedBy,		CreatedOn
	  FROM
	     Category
	  WHERE CategoryID=@CategoryID
END
