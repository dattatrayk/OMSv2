CREATE PROCEDURE [dbo].[Insert_Category](	--@CategoryID INT , 
	@CategoryName NVARCHAR(250) ,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO Category 	   (	   		CategoryName ,
		--CategoryID  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    @CategoryName ,
		--@CategoryID  ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END