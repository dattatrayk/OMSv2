CREATE PROCEDURE [dbo].[Insert_Brand](	@BrandID UNIQUEIDENTIFIER , 
	@BrandName NVARCHAR(250) ,
    @CreatedBy UNIQUEIDENTIFIER 
)ASBEGIN       INSERT INTO Brand 	   (		BrandName ,
		BrandID  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	    @BrandName ,
		@BrandID  ,		@CreatedBy,		GETUTCDATE(), 		@CreatedBy,		GETUTCDATE()	   )END