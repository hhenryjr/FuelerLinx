SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[up_Select_SchedulingImports]
(
@SchedulingPlatform int,
@AdminClientID int,
@CustClientID int
)
as
begin
SELECT *
FROM SchedulingImports
WHERE SchedulingPlatform = @SchedulingPlatform 
		AND AdminClientID = @AdminClientID 
		AND CustClientID = @CustClientID 
		AND ImportDate < CURRENT_TIMESTAMP 
		AND ImportDate > DateADD(mi, -3, Current_TimeStamp)
		AND ISNULL(IsReleased, 0) = 0

UPDATE SchedulingImports
SET IsReleased = 1
WHERE SchedulingPlatform = @SchedulingPlatform 
		AND AdminClientID = @AdminClientID 
		AND CustClientID = @CustClientID 
		AND ImportDate < CURRENT_TIMESTAMP 
		AND ImportDate > DateADD(mi, -3, Current_TimeStamp)
		AND ISNULL(IsReleased, 0) = 0
end