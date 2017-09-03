GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrdersByAndAdminClientID]    Script Date: 11/10/2016 4:29:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_FuelOrdersByAndAdminClientID]

ALTER PROCEDURE [dbo].[up_Select_FuelOrdersByAndAdminClientID]
	@AdminClientID int,
	@StartDate datetime = null,
	@EndDate datetime = null
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT f.[Id]
      ,f.[AdminClientID]
      ,f.[CustClientID]
      ,f.[OrderedByUserID]
      ,f.[AircraftID]
      ,f.[TailNumber]
      ,f.[ICAO]
      ,f.[FBO]
      ,f.[DateRequested]
      ,f.[AdminStatus]
      ,f.[CustStatus]
      ,f.[IsArchived]
      ,f.[IsDirectlyEntered]
      ,f.[QuotedPPG]
      ,f.[InvoicedPPG]
      ,f.[QuotedVolume]
      ,f.[InvoicedVolume]
      ,f.[FuelingDateTime]
      ,f.[DateAdded]
      ,f.[WholesaleQuoted]
      ,f.[WholesaleInvoiced]
      ,f.[InvoiceNumber]
      ,f.[BasePPG]
      ,f.[NoFuel]
      ,f.[TripID]
      ,f.[LegNumber]
      ,f.[CertificateType]
      ,f.[FuelOn]
      ,f.[PostedRetail]
      ,f.[RampFee]
      ,f.[RampFeeWaivedAt]
      ,f.[RequestedBy]
      ,f.[InvoiceStatus]
      ,V.Id AS SupplierID
      ,f.[PlattsPrice]
      ,f.[AdminNotes]
      ,f.[CustNotes]
      ,f.[Vendor]
      ,f.[Product], 
	  f.Total,
	   case when isnull(f.WholesaleInvoiced, 0) > 0 then f.WholesaleInvoiced else isnull(f.WholesaleQuoted, 0) end as PPG,
	   --a.TailNumber,
	   c.Name,
	   case when isnull(i.TotalAttachments, 0) = 0 then 0 else 1 end as HasAttachments
FROM
	[dbo].[FuelOrders] f
--inner join Aircrafts a on f.AircraftID = a.Id
left join Vendors V on V.Name = f.Vendor
left join Clients c on f.CustClientID = c.Id
left join (select COUNT(foi.FuelOrderID) as TotalAttachments, foi.FuelOrderID from FuelOrderInvoices foi group by foi.FuelOrderID) i on i.FuelOrderID = f.Id
WHERE
	f.[AdminClientID] = @AdminClientID
	and (@StartDate is null OR @StartDate <= f.FuelingDateTime)
	and (@EndDate is null OR DATEADD(d, 1, @EndDate) > f.FuelingDateTime)
order by f.FuelingDateTime desc

GO
--endregion
