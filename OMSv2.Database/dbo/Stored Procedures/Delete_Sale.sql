CREATE PROCEDURE [dbo].[Delete_Sale]
@SaleID int,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE Sale
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE SaleID=@SaleID
END
