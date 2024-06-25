CREATE PROCEDURE [dbo].[Insert_Item](	@ItemID UNIQUEIDENTIFIER,	@ClientID UNIQUEIDENTIFIER , 
	@Name NVARCHAR(250) ,
    @Description NVARCHAR(MAX) ,
    @Price DECIMAL(10, 2) ,
    @ImgURL NVARCHAR(512) ,
    @Stock INT ,
    @CategoryID UNIQUEIDENTIFIER ,
    @BrandID UNIQUEIDENTIFIER ,
    @CreatedBy UNIQUEIDENTIFIER 
    )ASBEGIN       INSERT INTO Item 	   (	   		ItemID,		ClientID,		Name  ,
		Description  ,
		Price  ,
		ImgURL ,
		Stock  ,
		CategoryID  ,
		BrandID  ,		CreatedBy,		CreatedOn,		ModifiedBy,		ModifiedOn	   )       VALUES 	   (	   	    @ItemID,		@ClientID,		@Name  ,
		@Description  ,
		@Price  ,
		@ImgURL ,
		@Stock  ,
		@CategoryID  ,
		@BrandID  ,       @CreatedBy,	   GETUTCDATE(), 	   @CreatedBy,	   GETUTCDATE()	   )END