CREATE PROCEDURE [dbo].[Update_Item](	@ItemID INT,	@Name NVARCHAR(250) ,
    @Description NVARCHAR(MAX) ,
    @price DECIMAL(10, 2) ,
    @ImgURL NVARCHAR(512) ,
    @stock INT ,
    @CategoryID INT ,
    @BrandID INT ,	@ModifiedBy	    UNIQUEIDENTIFIER)ASBEGIN       UPDATE Item	   SET	    ModifiedBy=@ModifiedBy,	    Name=@Name  ,
		Description=@Description  ,
		price=@price  ,
		ImgURL=@ImgURL ,
		stock=@stock  ,
		CategoryID=@CategoryID  ,
		BrandID=@BrandID  ,	    ModifiedOn=GETUTCDATE()	   WHERE ItemID=@ItemIDEND