CREATE PROCEDURE [dbo].[Insert_Client]
(
	@ClientID UNIQUEIDENTIFIER , 
    @Name NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @Address NVARCHAR(MAX), 
    @ApiKey NVARCHAR(250) 
)

AS
BEGIN
       INSERT INTO Client 
	   (
		ClientID,
		Name ,
		ContactNo  ,
		Email  ,
		Address,
		ApiKey,
		CreatedOn,
		ModifiedOn
	   )
       VALUES 
	   (
		@ClientID,
		@Name ,
		@ContactNo  ,
		@Email  ,
		@Address  ,
       @ApiKey,
	   GETUTCDATE(), 
	   GETUTCDATE()
	   )
END
