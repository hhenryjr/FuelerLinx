GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrdersAll]    Script Date: 11/10/2016 4:29:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_FuelOrdersAll]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_FuelOrdersAll]
-- Date Generated: Monday, June 20, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_FuelOrdersAll]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	f.[Id],
	f.[AdminClientID],
	f.[CustClientID],
	f.[OrderedByUserID],
	f.[AircraftID],
	a.TailNumber,
	f.[ICAO],
	f.[FBO],
	f.[DateRequested],
	f.[AdminStatus],
	f.[CustStatus],
	f.[IsArchived],
	f.[IsDirectlyEntered],
	f.[QuotedPPG],
	f.[InvoicedPPG],
	f.[QuotedVolume],
	f.[InvoicedVolume],
	f.[FuelingDateTime],
	f.[DateAdded],
	f.WholesaleQuoted,
	f.WholesaleInvoiced,
	f.BasePPG,
	f.NoFuel,
	f.TripID,
	f.LegNumber,
	f.CertificateType,
	f.FuelOn,
	f.PostedRetail,
	f.RampFee,
	f.RampFeeWaivedAt,
	f.RequestedBy,
	f.InvoiceStatus
FROM
	[dbo].[FuelOrders] f
inner join Aircrafts a on f.AircraftID = a.Id

--endregion

