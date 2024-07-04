CREATE PROCEDURE [dbo].[Get_UserByID]
@UserName int
AS
BEGIN
      SELECT 
		UserID ,
		UserName
	  FROM
	     Users
	  WHERE UserName=@UserName
END

