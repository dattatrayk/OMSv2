CREATE PROCEDURE [dbo].[Validate_ItemCode]
	@ClientID UNIQUEIDENTIFIER,
    @Code NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Item WHERE ClientID = @ClientID AND [Code] = @Code AND [IsDeleted] = 0)
    BEGIN
        SELECT 1 AS CodeExists;
    END
    ELSE
    BEGIN
        SELECT 0 AS CodeExists;
    END
END
