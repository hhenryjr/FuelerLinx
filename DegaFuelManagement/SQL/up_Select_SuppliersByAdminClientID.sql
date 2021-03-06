GO
/****** Object:  StoredProcedure [dbo].[up_Select_SuppliersByAdminClientID]    Script Date: 7/10/2017 3:19:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_SuppliersAll]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_SuppliersAll]
-- Date Generated: Monday, June 20, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_SuppliersByAdminClientID]--2
@AdminClientID int

AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	s.[Id],
	s.AdminClientID,
	s.[SupplierName],
	Max(sfp.DateUploaded) as LastUpdate,
	case when count(sfp.SupplierID) = 0
		then 0 
		else (SUM((case when dateadd(dd, 1, isnull(sfp.Expires, GETDATE())) < GETDATE() then 0 else 1 end)) * 100.0) / COUNT(sfp.SupplierID) end as ValidPricingPercentage,
	s.SupplierEmail,
	s.SupplierPhone
		--sfp.AdminClientID
FROM
	[dbo].[Suppliers] s
	left join SupplierFuelsPrices sfp on sfp.SupplierID = s.Id and sfp.AdminClientID = s.AdminClientID--@AdminClientID 
	where @AdminClientID = s.AdminClientID--s.Id <> 10 and s.Id <> 11
	group by s.Id, s.AdminClientID, s.SupplierName, s.SupplierEmail, s.SupplierPhone--, sfp.AdminClientID

	order by s.SupplierName

--endregion

