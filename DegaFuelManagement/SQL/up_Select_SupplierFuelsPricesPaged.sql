GO
/****** Object:  StoredProcedure [dbo].[up_Select_SupplierFuelsPricesPaged]    Script Date: 11/18/2016 4:18:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_SupplierFuelsPricesPaged]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_SupplierFuelsPricesPaged]
-- Date Generated: Monday, June 20, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_SupplierFuelsPricesPaged]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	[Id],
	[Vendor],
	[IATA],
	[ICAO],
	[FBOName],
	[Min],
	[Max],
	[TotalWithTax],
	[Expires],
	[Product],
	[Notes],
	[AdminClientId],
	[SupplierId],
	EffectiveDate
FROM
	[dbo].[SupplierFuelsPrices]

--endregion

