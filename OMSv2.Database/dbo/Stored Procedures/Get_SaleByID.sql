﻿CREATE PROCEDURE [dbo].[Get_SaleByID]
@SaleID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
		SaleDate ,
		CustomerName ,
		ContactNo,
		Email ,
		Address ,
		TotalAmount ,
		Quantity,
	  FROM
	     Sale
	  WHERE SaleID=@SaleID
END