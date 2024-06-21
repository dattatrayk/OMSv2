CREATE PROCEDURE [dbo].[Insert_Item](	--@ItemID INT,	@Name NVARCHAR(250) ,
    @Description NVARCHAR(MAX) ,
    @price DECIMAL(10, 2) ,
    @ImgURL NVARCHAR(512) ,
    @stock INT ,
    @CategoryID INT ,
    @BrandID INT ,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO Item 	   (	   		--ItemID,		Name  ,
		Description  ,
		price  ,
		ImgURL ,
		stock  ,
		CategoryID  ,
		BrandID  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    --@ItemID,		@Name  ,
		@Description  ,
		@price  ,
		@ImgURL ,
		@stock  ,
		@CategoryID  ,
		@BrandID  ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END