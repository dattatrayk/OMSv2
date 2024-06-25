CREATE PROCEDURE [dbo].[Get_CustomerByID]
@CustomerID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
		Name ,
		ContactNo,
		Email ,
		AddressDetails,
		CreatedBy,
		CreatedOn
	  FROM
	     Customer
	  WHERE CustomerID=@CustomerID
	  and IsDeleted!=1
END

