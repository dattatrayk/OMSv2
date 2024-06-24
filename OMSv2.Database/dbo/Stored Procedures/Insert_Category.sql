CREATE PROCEDURE [dbo].[Insert_Category](	@CategoryID UNIQUEIDENTIFIER,	@CategoryName NVARCHAR(250) ,
    @CreatedBy UNIQUEIDENTIFIER 
)ASBEGIN       INSERT INTO Category 	   (	   CategoryID,		CategoryName ,
		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   @CategoryID,	    @CategoryName ,
       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END