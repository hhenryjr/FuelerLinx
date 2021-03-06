GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrders_CustTotalByStatus]    Script Date: 6/28/2017 12:41:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mike Mieglitz
-- Create date: 10/12/2016
-- Description:	Get the total number of dispatches, organized by status
-- =============================================
ALTER PROCEDURE [dbo].[up_Select_FuelOrders_CustTotalByStatus] 
	@StartDate datetime,
	@EndDate datetime,
	@CustClientID int,
	@Domain varchar(50) = ''
AS
BEGIN
select COUNT(*) as TotalDispatches,
				SUM(case when ISNULL(fo.InvoiceStatus, 0) = 7 then 1 end) as TotalPending,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 8 then 1 end) as TotalReconciled,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 5 then 1 end) as TotalDiscrepancies,
	            SUM(case when ISNULL(fo.InvoiceStatus, 0) = 9 then 1 end) as TotalCancelled	            
	from FuelOrders fo
	INNER JOIN ApplicationDomain AD ON 
	((AD.Domain = @Domain AND fo.AdminClientID = AD.AdminClientID)
	)
	where fo.CustClientID = @CustClientID
	and fo.FuelingDateTime >= @StartDate
	and fo.FuelingDateTime < DATEADD(D, 1, @EndDate)
END
