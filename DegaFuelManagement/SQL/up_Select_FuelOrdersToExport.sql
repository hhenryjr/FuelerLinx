--[up_Select_FuelOrdersToExport]

ALTER PROCEDURE [dbo].[up_Select_FuelOrdersToExport]
	@AdminClientID int,
	@ListOfFuelOrderIDs varchar(max),
	@StartDate datetime = null,
	@EndDate datetime = null
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT f.*, 
	   case when isnull(f.WholesaleInvoiced, 0) > 0 then f.WholesaleInvoiced else isnull(f.WholesaleQuoted, 0) end as PPG,
	   --a.TailNumber,
	   c.Name,
	   case when isnull(i.TotalAttachments, 0) = 0 then 0 else 1 end as HasAttachments
FROM
	[dbo].[FuelOrders] f
	
inner join dbo.fn_Split(@ListOfFuelOrderIDs, ',') s on s.Value = f.Id
--inner join Aircrafts a on f.AircraftID = a.Id
left join Clients c on f.CustClientID = c.Id
left join (select COUNT(foi.FuelOrderID) as TotalAttachments, foi.FuelOrderID from FuelOrderInvoices foi group by foi.FuelOrderID) i on i.FuelOrderID = f.Id
WHERE
	f.[AdminClientID] = @AdminClientID
	and (@StartDate is null OR @StartDate <= f.FuelingDateTime)
	and (@EndDate is null OR DATEADD(d, 1, @EndDate) > f.FuelingDateTime)
order by f.FuelingDateTime desc
GO
--endregion
