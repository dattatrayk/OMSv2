CREATE PROCEDURE [dbo].[Insert_Brand]
(
	--@BrandID int , 
	@ClientID UNIQUEIDENTIFIER , 
	@BrandName NVARCHAR(250) ,
    @CreatedBy UNIQUEIDENTIFIER 
)

AS
BEGIN
       INSERT INTO Brand 
	   (
		BrandName ,
		--BrandID  ,
		ClientID  ,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn
	   )
       VALUES 
	   (
	    @BrandName,
		--@BrandID,
		@ClientID,
		@CreatedBy,
		GETUTCDATE(), 
		@CreatedBy,
		GETUTCDATE()
	   )
END
