GO
/****** Object:  StoredProcedure [dbo].[up_Insert_SchedulingImports]    Script Date: 3/29/2017 1:36:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[up_Load_SchedulingImports]
(
@SchedulingPlatform int,
@AdminClientID int,
@CustClientID int
)
as
begin
	SELECT * FROM SchedulingImports
	WHERE SchedulingPlatform = @SchedulingPlatform AND AdminClientID = @AdminClientID AND CustClientID = @CustClientID
end