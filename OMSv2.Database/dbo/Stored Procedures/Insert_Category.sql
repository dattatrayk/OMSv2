CREATE PROCEDURE [dbo].[Insert_Category]
(
	--@CategoryID int,
	@ClientID UNIQUEIDENTIFIER , 
	@CategoryName NVARCHAR(250) ,
    @CreatedBy UNIQUEIDENTIFIER 
)

AS
BEGIN
       INSERT INTO Category 
	   (
		--CategoryID,
		ClientID,
		CategoryName ,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn
	   )
       VALUES 
	   (
	   --@CategoryID,
	   @ClientID,
	   @CategoryName ,
       @CreatedBy,
	   GETUTCDATE(), 
	   @CreatedBy,
	   GETUTCDATE()
	   )
END
