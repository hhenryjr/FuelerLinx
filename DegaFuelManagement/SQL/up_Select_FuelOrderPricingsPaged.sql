GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrderPricingsPaged]    Script Date: 11/18/2016 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_FuelOrderPricingsPaged]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_FuelOrderPricingsPaged]
-- Date Generated: Monday, June 20, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_FuelOrderPricingsPaged]
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
	[AdminClientID],
	[SupplierID],
	EffectiveDate
FROM
	[dbo].[FuelOrderPricings]

--endregion

