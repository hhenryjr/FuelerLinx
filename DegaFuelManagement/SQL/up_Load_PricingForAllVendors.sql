GO
/****** Object:  StoredProcedure [dbo].[up_Load_PricingForAllVendors]    Script Date: 8/1/2017 3:34:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_Load_PricingForAllVendors]--2,'kbtr', 'N12345',74, 0, 0, ''
(
@AdminClientID int = 0,
@ICAO varchar(4) = '',
@TailNumber varchar(50) = '',
@CustClientID int = 0,
@PriceMargin int = 0,
@ContactID int = 0,
@Mode varchar(50) = ''
)
as
begin

SELECT @CustClientID = CustClientID
FROM Contacts
WHERE Id = @ContactID

CREATE TABLE #Prices (Id int, Vendor nvarchar(max), IATA nvarchar(4), ICAO nvarchar(4), FBOName nvarchar(max),
									[SupplierMin] int, SupplierMax int, SupplierTotalWithTax decimal(18, 5), Expires datetime2(7), 
									Product nvarchar(max), Notes nvarchar(max), AdminClientID int, 
									SupplierID int, CustClientID int, PriceTierMargin decimal(18,5), FBOMargin float, 
									Price decimal(18,5), TailNumber nvarchar (50), PriceMarginID int, 
									PriceTierMin int, PriceTierMax int, PrimaryPriceMargin decimal(18,5),
									EffectiveDate datetime2(7))
		
INSERT INTO #Prices
select sfp.[Id]
      ,sfp.[Vendor]
      ,sfp.[IATA]
      ,sfp.[ICAO]
      ,sfp.[FBOName]
      ,sfp.[Min] as SupplierMin
      ,case when isnull(sfp.[Max], 0) = 0 then 99999 else sfp.[Max] end as SupplierMax
      ,sfp.[TotalWithTax] as SupplierTotalWithTax
      ,sfp.[Expires]
      ,sfp.[Product]
      ,sfp.[Notes]
      ,sfp.[AdminClientID]
      ,sfp.[SupplierID], CD.CustClientID AS CustClientID, 
	  0 AS PriceTierMargin, 
      ISNULL(AE.Margin, ISNULL(AEAll.Margin, 0)) AS FBOMargin, 
		TotalWithTax + PM.PrimaryMargin 
		+ 0
		+ ISNULL(A.Margin, 0) + ISNULL(AE.Margin, ISNULL(AEAll.Margin, 0)) as Price, 
		A.TailNumber, PM.Id AS PriceMarginID, 
		sfp.[Min] as PriceTierMin,
        case when isnull(sfp.[Max], 0) = 0 then 99999 else sfp.[Max] end as PriceTierMax,
		PM.PrimaryMargin AS PrimaryPriceMargin,
		sfp.EffectiveDate

from
    --Find the lowest supplier at each FBO
    (select MIN(sfpLowestVendor.Vendor) as Vendor, sfpLowestVendor.ICAO, sfpLowestVendor.FBO, sfpLowestVendor.Product, sfpLowestVendor.AdminClientID
    from 
        (select sfa.ICAO, sfa.FBO, sfa.Product, MIN(AvePrice) as LowestAvePrice, sfa.AdminClientID
        from SupplierFuelPriceAverages sfa
        left join Vendors v on v.Name = sfa.Vendor
                               and v.AdminClientID = sfa.AdminClientID
        left join FBODetailVendorExclusions fv on fv.VendorID = v.Id
        left join FBOPriceMargins fp on fp.Id = fv.FBOID
                                                and fp.ICAO = sfa.ICAO
                                                and dbo.fn_FBOs_Equivalent(fp.FBO, sfa.FBO) = 1
        left join CustomerDetailVendorExclusions cv on cv.VendorID = v.Id
                                                    and cv.CustClientID = isnull(@CustClientID, 0)
													and (isnull(cv.TailNumbers, '') = '' or cv.TailNumbers like '%' + @TailNumber + '%')
        where (@AdminClientID <> 14144 and sfa.AdminClientID = @AdminClientID or ISNULL(@AdminClientID, 0) = 0)
            AND (ISNULL(@ICAO, '') = '' OR sfa.ICAO = @ICAO)
            and fv.Id is null
            and cv.Id is null
			and ISNULL(v.IsDeactivated, 0) = 0
        group by sfa.ICAO, sfa.FBO, Product, sfa.AdminClientID) sfpAveResults

        inner join

        SupplierFuelPriceAverages sfpLowestVendor on sfpLowestVendor.AdminClientID = sfpAveResults.AdminClientID
                                            and sfpLowestVendor.ICAO = sfpAveResults.ICAO
                                            and sfpLowestVendor.AvePrice = sfpAveResults.LowestAvePrice
                                            and dbo.fn_FBOs_Equivalent(sfpLowestVendor.FBO, sfpAveResults.FBO) = 1
                                            and CHARINDEX('additive', isnull(sfpLowestVendor.Product, '')) = CHARINDEX('additive', isnull(sfpAveResults.Product, ''))
        group by sfpLowestVendor.ICAO, sfpLowestVendor.FBO, sfpLowestVendor.Product, sfpLowestVendor.AdminClientID) lowestSuppliers

inner join SupplierFuelsPrices sfp on sfp.AdminClientID = lowestSuppliers.AdminClientID
                                        and sfp.ICAO = lowestSuppliers.ICAO
                                        and dbo.fn_FBOs_Equivalent(sfp.FBOName, lowestSuppliers.FBO) = 1
                                        and sfp.Vendor = lowestSuppliers.Vendor
                                        and CHARINDEX('additive', isnull(sfp.Product, '')) = CHARINDEX('additive', isnull(lowestSuppliers.Product, ''))

inner join Aircrafts A ON (isnull(@TailNumber, '') = '' OR @TailNumber = A.TailNumber)
                        AND (ISNULL(@CustClientID, 0) = 0 OR @CustClientID = A.CustClientID)
left join Aircrafts A2 ON ISNULL(A.IsMarginEnabled, 0) = 1 AND A2.Id = A.Id
inner join CustomerDetails CD ON CD.CustClientID = A.CustClientID 
                                    AND (CD.AdminClientID = @AdminClientID or ISNULL(@AdminClientID, 0) = 0)
									AND ISNULL(CD.IsActive, 0) = 1
inner join (select PM.*, CPM.CustClientID
            from PriceMargins PM
            inner join CustomerPriceMargins CPM ON CPM.PriceMarginID = PM.Id
            where CPM.CustClientID = @CustClientID
                and PM.AdminClientID = @AdminClientID) PM on PM.AdminClientID = CD.AdminClientID and PM.CustClientID = CD.CustClientID
--inner join PriceMarginTiers PMT ON PMT.PriceMarginID = PM.Id AND PMT.MinGallon >= sfp.[Min] AND (PMT.MinGallon <= sfp.[max] or ISNULL(sfp.[max], 0) = 0)
left join FBOPriceMargins FPM ON FPM.AdminClientID = PM.AdminClientID
									AND FPM.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(fpm.FBO, sfp.FBOName) = 1
left join AircraftExclusions AEAll ON (isnull(AEAll.AircraftID, 0) = 0 AND AEAll.TailNumbers = 'APPLY TO ALL') 
									AND AEAll.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(AEAll.FBO, sfp.FBOName) = 1
									AND AEAll.AdminClientID = CD.AdminClientID
									AND (AEAll.CustClientID = CD.CustClientID OR ISNULL(AEAll.CustClientID, 0) = 0)
left join AircraftExclusions AE ON ((AE.AircraftID = A.Id) or AE.TailNumbers like '%' + A.TailNumber + '%') 
									AND AE.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(AE.FBO, sfp.FBOName) = 1
									AND AE.AdminClientID = CD.AdminClientID
									AND AE.CustClientID = CD.CustClientID
WHERE  (sfp.Expires IS NULL OR DATEADD(dd, 1, sfp.Expires) >= GETDATE())
									AND (sfp.EffectiveDate is null or sfp.EffectiveDate <= GETDATE())
		                            AND 
		                            isnull(sfp.FBOName,'') not like '%lgr only%'
		                            AND (isnull(sfp.FBOName,'') NOT LIKE '%DC9%')
		                            AND (isnull(sfp.FBOName,'') NOT LIKE '%and larger%')
									AND ((AE.Id is null AND (AEAll.Id is null OR ISNULL(AEAll.IsExcluded, 1) = 0)) OR ISNULL(AE.IsExcluded, 1) = 0)
									AND (isnull(FPM.IsOmitted, 0) = 0)
									AND (@ICAO = '' OR sfp.ICAO = @ICAO)
									AND ((@PriceMargin = 0)
									            OR (@PriceMargin > 0 AND PM.Id = @PriceMargin)) 

INSERT INTO #Prices
select sfp.[Id]
      ,sfp.[Vendor]
      ,sfp.[IATA]
      ,sfp.[ICAO]
      ,sfp.[FBOName]
      ,sfp.[Min] as [SupplierMin]
      ,case when isnull(sfp.[Max], 0) = 0 then 99999 else sfp.[Max] end as SupplierMax
      ,sfp.[TotalWithTax] as SupplierTotalWithTax
      ,sfp.[Expires]
      ,sfp.[Product]
      ,sfp.[Notes]
      ,sfp.[AdminClientID]
      ,sfp.[SupplierID], CD.CustClientID AS CustClientID, ISNULL(PMT.Margin, 0) AS PriceTierMargin, 
      ISNULL(AE.Margin, ISNULL(AEAll.Margin, 0)) AS FBOMargin, 
		TotalWithTax + PM.PrimaryMargin + ISNULL(PMT.Margin, 0) + ISNULL(A.Margin, 0) + ISNULL(AE.Margin, ISNULL(AEAll.Margin, 0)) as Price, 
		A.TailNumber, PM.Id AS PriceMarginID, 
		case when isnull(PMT.MinGallon, 0) = 0 then 1 else PMT.MinGallon end AS PriceTierMin, 
		case when ISNULL(PMT.MaxGallon, 0) = 0 then 99999 else PMT.MaxGallon end AS PriceTierMax,
		PM.PrimaryMargin AS PrimaryPriceMargin,
		sfp.EffectiveDate
from
    
    SupplierFuelsPrices sfp
    --START: join section for vendor exclusions
left join Vendors v on v.Name = sfp.Vendor
                           and v.AdminClientID = sfp.AdminClientID
    left join FBODetailVendorExclusions fv on fv.VendorID = v.Id
    left join FBOPriceMargins fp on fp.Id = fv.FBOID
                                            and fp.ICAO = sfp.ICAO
                                            and dbo.fn_FBOs_Equivalent(fp.FBO, sfp.FBOName) = 1
    left join CustomerDetailVendorExclusions cv on cv.VendorID = v.Id
                                                and cv.CustClientID = isnull(@CustClientID, 0)
    --END: join section for vendor exclusions

inner join Aircrafts A ON (isnull(@TailNumber, '') = '' OR @TailNumber = A.TailNumber)
                        AND (ISNULL(@CustClientID, 0) = 0 OR @CustClientID = A.CustClientID)
left join Aircrafts A2 ON ISNULL(A.IsMarginEnabled, 0) = 1 AND A2.Id = A.Id
inner join CustomerDetails CD ON CD.CustClientID = A.CustClientID 
                                    AND (CD.AdminClientID = @AdminClientID or ISNULL(@AdminClientID, 0) = 0)
									AND ISNULL(CD.IsActive, 0) = 1
inner join CustomerPriceMargins CPM ON CPM.CustClientID = A.CustClientID
inner join PriceMargins PM ON ((@PriceMargin = 0)
									OR (@PriceMargin > 0 AND PM.Id = @PriceMargin)) 
								AND (PM.AdminClientID = CD.AdminClientID) 
								AND CPM.PriceMarginID = PM.Id
inner join PriceMarginTiers PMT ON PMT.PriceMarginID = PM.Id AND PMT.MinGallon >= sfp.[Min] AND (PMT.MinGallon <= sfp.[max] or ISNULL(sfp.[max], 0) = 0)
left join FBOPriceMargins FPM ON FPM.AdminClientID = PM.AdminClientID
									AND FPM.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(fpm.FBO, sfp.FBOName) = 1
left join AircraftExclusions AEAll ON (isnull(AEAll.AircraftID, 0) = 0 AND AEAll.TailNumbers = 'APPLY TO ALL') 
									AND AEAll.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(AEAll.FBO, sfp.FBOName) = 1
									AND AEAll.AdminClientID = CD.AdminClientID
									AND (AEAll.CustClientID = CD.CustClientID OR ISNULL(AEAll.CustClientID, 0) = 0)
left join AircraftExclusions AE ON ((AE.AircraftID = A.Id) or AE.TailNumbers like '%' + A.TailNumber + '%') 
									AND AE.ICAO = sfp.ICAO 
									AND dbo.fn_FBOs_Equivalent(AE.FBO, sfp.FBOName) = 1
									AND AE.AdminClientID = CD.AdminClientID
									AND AE.CustClientID = CD.CustClientID
WHERE @AdminClientID = 14144 and sfp.AdminClientID = @AdminClientID
      and (sfp.Expires IS NULL OR DATEADD(dd, 1, sfp.Expires) >= GETDATE())
									AND (sfp.EffectiveDate is null or sfp.EffectiveDate <= GETDATE())
		                            AND isnull(sfp.FBOName,'') not like '%lgr only%'
		                            AND (isnull(sfp.FBOName,'') NOT LIKE '%DC9%')
		                            AND (isnull(sfp.FBOName,'') NOT LIKE '%and larger%')
									AND ((AE.Id is null AND (AEAll.Id is null OR ISNULL(AEAll.IsExcluded, 1) = 0)) OR ISNULL(AE.IsExcluded, 1) = 0)
									AND (isnull(FPM.IsOmitted, 0) = 0)
									AND (@ICAO = '' OR sfp.ICAO = @ICAO)
									--START: Vendor exlusions logic
									and fv.Id is null
                                    and cv.Id is null
                                    --END: Vendor exlusions logic
	select * from #Prices
	order by Price

drop table #Prices
end