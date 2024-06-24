﻿CREATE PROCEDURE [dbo].[Get_CategoryByID]
@CategoryID UNIQUEIDENTIFIER
AS
BEGIN
      SELECT 
	  FROM
	     Category
	  WHERE CategoryID=@CategoryID
END