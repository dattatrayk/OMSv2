﻿CREATE PROCEDURE [dbo].[Select_Sale]
@ClientID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
	     SaleID  , 
		SaleDate,
		CustomerName ,
		ContactNo  ,
		Email  ,
		Address  ,
		TotalAmount  ,
		Quantity ,
		CreatedBy,
		CreatedOn
	  FROM Sale 
	  WHERE 
	  IsDeleted != 1
	  and ClientID=@ClientID
    Order By CreatedOn desc
END
