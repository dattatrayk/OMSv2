CREATE PROCEDURE [dbo].[Update_Brand]
(
	@BrandID int , 
	@BrandName NVARCHAR(250) ,
	@ModifiedBy	    UNIQUEIDENTIFIER
)

AS
BEGIN
       UPDATE Brand
	   SET
	    ModifiedBy=@ModifiedBy,
	    BrandName=@BrandName ,
	    ModifiedOn=GETUTCDATE()
	   WHERE BrandID=@BrandID
END
