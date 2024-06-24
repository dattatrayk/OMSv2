CREATE PROCEDURE [dbo].[Delete_Item]
@ItemID UNIQUEIDENTIFIER,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE Item
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE ItemID=@ItemID
END
