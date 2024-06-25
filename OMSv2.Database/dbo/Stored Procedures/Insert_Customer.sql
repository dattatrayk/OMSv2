CREATE PROCEDURE [dbo].[Insert_Customer](	@CustomerID UNIQUEIDENTIFIER ,
	@ClientID UNIQUEIDENTIFIER , 
    @Name NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @AddressDetails NVARCHAR(MAX) ,
    @CreatedBy UNIQUEIDENTIFIER 
)ASBEGIN       INSERT INTO Customer 	   (		CustomerID  , 
		ClientID,
		Name ,
		ContactNo  ,
		Email  ,
		AddressDetails  ,
		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    @CustomerID  , 
		@ClientID,
		@Name ,
		@ContactNo  ,
		@Email  ,
		@AddressDetails  ,
       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END