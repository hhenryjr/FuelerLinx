GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrdersAll]    Script Date: 2/8/2017 4:55:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[up_Select_FutureFuelOrders]
@AdminClientID int
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
	f.InvoiceStatus,
    f.[SupplierID],
    f.[PlattsPrice],
    f.[AdminNotes],
    f.[CustNotes],
	f.Vendor
FROM
	[dbo].[FuelOrders] f
inner join Aircrafts a on f.AircraftID = a.Id and f.AdminClientID = @AdminClientID and f.FuelingDateTime>=convert(date, getdate())

--endregion

