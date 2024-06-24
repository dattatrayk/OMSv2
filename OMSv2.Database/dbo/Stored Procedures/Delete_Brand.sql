﻿CREATE PROCEDURE [dbo].[Delete_Brand]
@BrandID UNIQUEIDENTIFIER,
@ModifiedBy	    UNIQUEIDENTIFIER
AS
BEGIN
      UPDATE Brand
	   SET
	     IsDeleted=1,
		 ModifiedBy=@ModifiedBy,
		 ModifiedOn=GETUTCDATE()
	   WHERE BrandID=@BrandID
END