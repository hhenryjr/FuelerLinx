GO
/****** Object:  StoredProcedure [dbo].[up_Update_SupplierFuelsPricesMax]    Script Date: 6/16/2017 1:46:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[up_Update_SupplierFuelsPricesMax]
(
@AdminClientID int,
@SupplierID int
)
as
begin
DELETE FROM SupplierFuelsPrices
WHERE TotalWithTax = 0

UPDATE SupplierFuelsPrices
SET [Max] = 99999
WHERE ([Max] is null or [Max] = 0) and AdminClientID = @AdminClientID and SupplierID = @SupplierID

UPDATE SupplierFuelsPrices
SET [Min] = 1
WHERE ([Min] is null or [Min] = 0) and AdminClientID = @AdminClientID and SupplierID = @SupplierID

UPDATE SFP
SET SFP.ICAO = A.ICAO
FROM SupplierFuelsPrices SFP
INNER JOIN AcukwikAirports A
       ON A.IATA = SFP.IATA
WHERE SFP.ICAO is null and AdminClientID = @AdminClientID and SupplierID = @SupplierID

UPDATE SFP
SET SFP.Vendor = S.SupplierName
FROM SupplierFuelsPrices SFP
INNER JOIN Suppliers S
       ON S.Id = SFP.SupplierID
WHERE SFP.Vendor is null and SFP.AdminClientID = @AdminClientID and SupplierID = @SupplierID
end