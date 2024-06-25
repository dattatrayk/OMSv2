CREATE PROCEDURE [dbo].[Select_Customer]
@ClientID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
	     CustomerID  , 
		Name ,
		ContactNo  ,
		Email  ,
		AddressDetails  ,
		CreatedBy,
		CreatedOn
	  FROM Customer 
	  WHERE 
	  IsDeleted != 1
	  and ClientID=@ClientID
    Order By CreatedOn desc
END
