/****** Object:  StoredProcedure [dbo].[up_Select_FBODetailsByAndICAOAndFBOAndAdminClientID]    Script Date: 6/24/2017 12:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[up_Select_FBODetailsByAndICAOAndFBOAndAdminClientID] 
@ICAO varchar(4),
@FBO nvarchar(max),
@AdminClientID int

as

if exists (select * from AcukwikFBOHandlerDetail af where af.HandlerLongName = @FBO)

select fp.Id, af.HandlerLongName as FBO, aa.ICAO, aa.AirportCity, af.HandlerTelephone, af.HandlerFuelSupply, fp.Margin, fp.AdminClientID, fp.Note 
from AcukwikFBOHandlerDetail af
inner join AcukwikAirports aa on aa.Airport_ID = af.Airport_ID
left join FBOPriceMargins fp on fp.FBO = af.HandlerLongName and fp.icao=@ICAO and fp.AdminClientID = @AdminClientID
where aa.ICAO = @ICAO and af.HandlerLongName = @FBO

else

select fp.Id, fp.FBO, aa.ICAO, aa.AirportCity, fp.Margin, fp.AdminClientID, fp.Note
from FBOPriceMargins fp
inner join AcukwikAirports aa on aa.ICAO = fp.ICAO
where fp.ICAO = @ICAO and fp.FBO = @FBO and fp.AdminClientID = @AdminClientID 
