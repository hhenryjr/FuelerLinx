GO
/****** Object:  StoredProcedure [dbo].[up_Select_AcukwikAirportsAndMarginsByAndAdminClientID]    Script Date: 8/1/2017 3:10:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[up_Select_AcukwikAirportsAndMarginsByAndAdminClientID]
@AdminClientID int
as
select
ac.Region,
aa.Country,
aa.[State/Subdivision] as [StateSubdivision],
aa.ICAO,
aa.AirportType,
aa.FullAirportName,
aa.FuelType,
aa.Customs,
aa.ApproachList,
aa.Airport_ID,
aa.AirportCity,
--apm.Margin,
--f.FBO as FBOName,
--f.Margin as FBOMargin,
apm.AdminClientID,
apm.IsInactive
--,apm.IsDisabled
from AcukwikAirports aa
left join AcukwikCountries ac on aa.Country = ac.FullCountryName
left join AirportPriceMargins apm on aa.ICAO = apm.ICAO and apm.AdminClientID = @AdminClientID
--left join FBOPriceMargins f on aa.ICAO = f.ICAO and f.AdminClientID = @AdminClientID
where aa.ICAO is not null and aa.airporttype != 'Military'