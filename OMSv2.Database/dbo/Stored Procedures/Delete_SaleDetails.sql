CREATE PROCEDURE [dbo].[Delete_SaleDetails]
@SaleDetailID UNIQUEIDENTIFIER,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE SaleDetails
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE SaleDetailID=@SaleDetailID
END
