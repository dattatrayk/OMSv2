CREATE PROCEDURE [dbo].[Update_Customer](	@CustomerID UNIQUEIDENTIFIER , 
    @Name NVARCHAR(250) ,
    @ContactNo NVARCHAR(250) ,
    @Email NVARCHAR(250) ,
    @AddressDetails NVARCHAR(MAX) ,
	@ModifiedBy	    UNIQUEIDENTIFIER)ASBEGIN       UPDATE Customer	   SET	    ModifiedBy=@ModifiedBy,		Name=@Name,
		ContactNo=@ContactNo  ,
		Email=@Email  ,
		AddressDetails=@AddressDetails  ,
	    ModifiedOn=GETUTCDATE()	   WHERE CustomerID=@CustomerIDEND