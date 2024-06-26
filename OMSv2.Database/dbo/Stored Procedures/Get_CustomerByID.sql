CREATE PROCEDURE [dbo].[Get_CustomerByID]
@CustomerID int
AS
BEGIN
      SELECT 
		CustomerID ,
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

