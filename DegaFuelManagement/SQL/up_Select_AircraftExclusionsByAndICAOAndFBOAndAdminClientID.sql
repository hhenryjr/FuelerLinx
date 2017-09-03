/****** Object:  StoredProcedure [dbo].[up_Select_AircraftExclusionsByAndICAOAndFBOAndAdminClientID]    Script Date: 6/24/2017 12:49:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER proc [dbo].[up_Select_AircraftExclusionsByAndICAOAndFBOAndAdminClientID]
@ICAO varchar(4),
@FBO nvarchar(MAX),
@AdminClientID int

as

SELECT c.Name
,ae.Id
,ae.[ICAO]
,ae.[FBO]
,ae.AdminClientID
,ae.CustClientID
,ae.TailNumbers
,ae.Margin
,ae.IsExcluded
,ad.Make
,ad.Model
,ad.Size

FROM [AircraftExclusions] ae 
left join Clients c on c.Id = ae.CustClientID
left join Aircrafts a on a.Id = ae.AircraftID
left join AircraftData ad on ad.AircraftID = a.MakeModelID
where ae.ICAO = @ICAO and ae.FBO = @FBO
and ae.AdminClientID = @AdminClientID
order by ae.Id 
