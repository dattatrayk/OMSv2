CREATE PROCEDURE [dbo].[Insert_User]
(
	@UserID UNIQUEIDENTIFIER ,
	@ClientID UNIQUEIDENTIFIER , 
    @UserName NVARCHAR(100) ,
    @PasswordHash NVARCHAR(64) ,
    @IsActive BIT 
)

AS
BEGIN
       INSERT INTO Users 
	   (
		UserID  , 
		ClientID,
		UserName ,
		PasswordHash  ,
		IsActive
	   )
       VALUES 
	   (
	    @UserID  , 
		@ClientID,
		@UserName ,
		@PasswordHash  ,
		@IsActive 
	   )
END
