﻿CREATE PROCEDURE [dbo].[Update_Item](	@ItemID int,	@Name NVARCHAR(250) ,	@Code NVARCHAR(250) ,    @Description NVARCHAR(MAX) ,    @Price DECIMAL(10, 2) ,    @ImgURL NVARCHAR(512) ,    @Stock INT ,    @CategoryID int ,    @BrandID int ,	@ModifiedBy	    UNIQUEIDENTIFIER)ASBEGIN       UPDATE Item	   SET	    ModifiedBy=@ModifiedBy,	    Name=@Name  ,		Code=@Code  ,		Description=@Description  ,		Price=@Price  ,		ImgURL=@ImgURL ,		Stock=@Stock  ,		CategoryID=@CategoryID  ,		BrandID=@BrandID  ,	    ModifiedOn=GETUTCDATE()	   WHERE ItemID=@ItemIDEND