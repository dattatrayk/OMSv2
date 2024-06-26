﻿CREATE PROCEDURE [dbo].[Update_Category]
(
	@CategoryID int , 
	@CategoryName NVARCHAR(250) ,
	@ModifiedBy	    UNIQUEIDENTIFIER
)

AS
BEGIN
       UPDATE Category
	   SET
	    ModifiedBy=@ModifiedBy,
	    CategoryName=@CategoryName ,
	    ModifiedOn=GETUTCDATE()
	   WHERE CategoryID=@CategoryID
END
