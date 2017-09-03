GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrders_TotalByStatus]    Script Date: 6/27/2017 4:54:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mike Mieglitz
-- Create date: 10/12/2016
-- Description:	Get the total number of dispatches, organized by status
-- =============================================
ALTER PROCEDURE [dbo].[up_Select_FuelOrders_TotalByStatus]
	@StartDate datetime,
	@EndDate datetime,
	@AdminClientID int
AS
BEGIN
	select COUNT(*) as TotalDispatches,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 7 then 1 end) as TotalPending,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 8 then 1 else 0 end) as TotalReconciled,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 5 then 1 else 0  end) as TotalDiscrepancies,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 9 then 1 else 0  end) as TotalCancelled	     
	from FuelOrders fo
	where fo.AdminClientID = @AdminClientID
	and fo.FuelingDateTime >= @StartDate
	and fo.FuelingDateTime < DATEADD(D, 1, @EndDate)
	--group by fo.InvoiceStatus
END
