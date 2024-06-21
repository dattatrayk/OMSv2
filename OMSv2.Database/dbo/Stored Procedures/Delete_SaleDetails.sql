CREATE PROCEDURE [dbo].[Delete_SaleDetails]
@SaleDetailsID INT,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE SaleDetails
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE SaleDetailsID=@SaleDetailsID
END
