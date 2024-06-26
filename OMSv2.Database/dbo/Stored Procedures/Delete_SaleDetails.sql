CREATE PROCEDURE [dbo].[Delete_SaleDetails]
@SaleID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Step 1: Retrieve affected ItemIDs and Quantities from SaleDetails
        DECLARE @ItemUpdates TABLE
        (
            ItemID INT,
            Quantity INT
        );

        INSERT INTO @ItemUpdates (ItemID, Quantity)
        SELECT sd.ItemID, sd.Quantity
        FROM SaleDetails sd
        WHERE sd.SaleID = @SaleID;

        -- Step 2: Delete SaleDetails records
        DELETE FROM SaleDetails
        WHERE SaleID = @SaleID;

        -- Step 3: Revert stock quantities in Item table
        UPDATE i
        SET i.Stock = i.Stock + u.Quantity
        FROM Item i
        JOIN @ItemUpdates u ON i.ItemID = u.ItemID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        -- Handle errors as needed
        THROW;
    END CATCH;
END
