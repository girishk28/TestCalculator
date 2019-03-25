USE ResultDB
GO

CREATE PROCEDURE dbo.uspAddResult @Result nvarchar(100)
AS
BEGIN
 INSERT INTO Results (CalculationResult) values (@Result)
END
GO
