
/****** Object:  StoredProcedure [dbo].[up_Select_Analysis_RegionalDispatches]    Script Date: 04/21/2017 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mike Mieglitz
-- Create date: 11/18/2016
-- Description:	Fetch dispatches by region
-- =============================================
ALTER PROCEDURE [dbo].[up_Select_Analysis_RegionalDispatches]
	@ClientID int,
	@MapRegion varchar(255) = 'united states',
	@StartDateFilter datetime = null,
	@EndDateFilter datetime = null,
	@State varchar(255) = ''
AS
BEGIN
	select COUNT(fr.Id) as Value,
	    case when @MapRegion = 'world' 
                                    then ap.Country 
                                    when @MapRegion = 'county'
                                    then replace(ac.County, ' county', '')
                                    else isnull(sr.abbreviation, ap.[State/Subdivision]) end as Label
	from FuelOrders fr
	inner join AcukwikAirports ap on ap.ICAO = fr.ICAO
	left join AirportCounty ac on ac.ICAO = ap.ICAO
	left join StateRegion sr on sr.name = ap.[State/Subdivision]
	where (fr.AdminClientID = @ClientID OR fr.CustClientID = @ClientID)
	      and (@StartDateFilter is null or fr.FuelingDateTime >= @StartDateFilter)
          and (@EndDateFilter is null or fr.FuelingDateTime < DATEADD(dd, 1, @EndDateFilter))
          and (ISNULL(@State, '') = '' or sr.abbreviation = @State)
          and (isnull(fr.CustStatus, 0) <> 3)
          and (isnull(case when @MapRegion = 'world' 
                                    then ap.Country 
                                    when @MapRegion = 'county'
                                    then replace(ac.County, ' county', '')
                                    else isnull(sr.abbreviation, ap.[State/Subdivision]) end, '') <> '')
    group by case when @MapRegion = 'world' 
                                    then ap.Country 
                                    when @MapRegion = 'county'
                                    then replace(ac.County, ' county', '')
                                    else isnull(sr.abbreviation, ap.[State/Subdivision]) end
END
