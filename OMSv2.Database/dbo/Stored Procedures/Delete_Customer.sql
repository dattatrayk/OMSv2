CREATE PROCEDURE [dbo].[Delete_Customer]
@CustomerID int,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE Customer
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE CustomerID=@CustomerID
END
