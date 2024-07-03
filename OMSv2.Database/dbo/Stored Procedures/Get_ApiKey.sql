CREATE PROCEDURE [dbo].[Get_ApiKey]
@ClientID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
		ApiKey 
	  FROM
	     Client
	  WHERE ClientID=@ClientID
	  and IsDeleted!=1
END
