/****** Object:  StoredProcedure [dbo].[up_Select_Aircrafts_RemainingByCustClientID]    Script Date: 6/24/2017 12:55:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[up_Select_Aircrafts_RemainingByCustClientID] 
@ICAO nvarchar(4),
@FBO nvarchar(50),
@AdminClientID int = 0,
@CustClientID int

as

declare @TailNumbers varchar(max) = ''

select @TailNumbers = ae.TailNumbers + ',' + @TailNumbers
from AircraftExclusions ae
where ae.CustClientID = @CustClientID
and ae.AdminClientID = @AdminClientID
and ae.FBO = @FBO
and ae.ICAO = @ICAO

SELECT
a.[Id],
a.[MakeModelID],
a.[AdminClientID],
a.[CustClientID],
a.[TailNumber],
a.[AccountNumber],
a.[CardNumber],
a.[IsMarginEnabled],
a.[Margin],
ad.Make,
ad.Model,
ad.Size

FROM
[dbo].[Aircrafts] a
left join dbo.fn_Split(@TailNumbers, ',') tails on tails.Value = a.TailNumber
left join AircraftData ad on a.MakeModelID = ad.AircraftID
WHERE
a.[CustClientID] = @CustClientID
and a.[AdminClientID] = @AdminClientID
and tails.Id is null 
