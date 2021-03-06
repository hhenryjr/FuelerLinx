GO
/****** Object:  StoredProcedure [dbo].[up_Select_SupplierFuelsPricesByAndAdminClientID]    Script Date: 7/10/2017 3:13:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_SupplierFuelsPricesByAndAdminClientID]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_SupplierFuelsPricesByAndAdminClientID]
-- Date Generated: Monday, June 20, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_SupplierFuelsPricesByAndAdminClientID]--2
    @AdminClientID int

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	[Id],
	[Vendor],
	[IATA],
	[ICAO],
	Replace(FBOName, ',','') AS FBOName,
	[Min],
	[Max],
	[TotalWithTax],
	DATEADD(dd, 1, Expires) AS Expires,
	[Product],
	Replace([Notes], ',','') AS Notes,
	[AdminClientID],
	[SupplierID],
    [DateUploaded],
	EffectiveDate,
	VendorEmail
FROM
	[dbo].[SupplierFuelsPrices]
WHERE
    (AdminClientID = @AdminClientID or ISNULL(@AdminClientID, 0) = 0)
ORDER BY
	Vendor
