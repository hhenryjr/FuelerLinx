GO
/****** Object:  StoredProcedure [dbo].[up_Update_FuelOrdersAdminStatus]    Script Date: 3/29/2017 12:58:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[up_Insert_SchedulingImports]
(
@SchedulingPlatform smallint,
@AdminClientID int,
@CustClientID int,
@TripNumber varchar(50),
@TailNumber varchar(10),
@ICAO varchar(4),
@FBO varchar(50),
@Arrival datetime
)
as
begin
	INSERT INTO [dbo].[SchedulingImports]
           ([SchedulingPlatform]
           ,[AdminClientID]
           ,[CustClientID]
           ,[TripNumber]
           ,[TailNumber]
           ,[ICAO]
           ,[FBO]
           ,[Arrival])
     VALUES
           (@SchedulingPlatform,
		   @AdminClientID,
		   @CustClientID,
		   @TripNumber,
		   @TailNumber,
		   @ICAO,
		   @FBO,
		   @Arrival)
end