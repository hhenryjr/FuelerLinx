GO
/****** Object:  StoredProcedure [dbo].[up_Load_EmailDispatches]    Script Date: 1/6/2017 12:08:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 01/05/2017
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[up_Load_EmailDispatches] (
	@AdminClientID int,
	@Purpose smallint
	)
AS
BEGIN
	SELECT *
	FROM EmailDispatches
	WHERE AdminClientID = @AdminClientID AND Purpose = @Purpose
END
