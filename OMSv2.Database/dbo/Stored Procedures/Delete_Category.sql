CREATE PROCEDURE [dbo].[Delete_Category]
@CategoryID int,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE Category
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE CategoryID=@CategoryID
END
