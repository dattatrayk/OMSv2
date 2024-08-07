﻿CREATE PROCEDURE [dbo].[Insert_Item]
(
	--@ItemID int,
	@ClientID UNIQUEIDENTIFIER , 
	@Name NVARCHAR(250) ,
	@Code NVARCHAR(250) ,
    @Description NVARCHAR(MAX) ,
    @Price DECIMAL(10, 2) ,
    @ImgURL NVARCHAR(512) ,
    @Stock INT ,
    @CategoryID int ,
    @BrandID int ,
    @CreatedBy UNIQUEIDENTIFIER 
    
)

AS
BEGIN
       INSERT INTO Item 
	   (
	   
		--ItemID,
		ClientID,
		Name  ,
		Code  ,
		Description  ,
		Price  ,
		ImgURL ,
		Stock  ,
		CategoryID  ,
		BrandID  ,
		CreatedBy,
		CreatedOn,
		ModifiedBy,
		ModifiedOn
	   )
       VALUES 
	   (
	   
	    --@ItemID,
		@ClientID,
		@Name  ,
		@Code ,
		@Description  ,
		@Price  ,
		@ImgURL ,
		@Stock  ,
		@CategoryID  ,
		@BrandID  ,
       @CreatedBy,
	   GETUTCDATE(), 
	   @CreatedBy,
	   GETUTCDATE()
	   )
END
