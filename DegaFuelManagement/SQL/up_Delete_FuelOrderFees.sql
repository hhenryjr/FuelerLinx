GO
/****** Object:  StoredProcedure [dbo].[up_Delete_OtherFees]    Script Date: 10/20/2016 15:39:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Delete_FuelOrderFees] (
	@FuelOrderID int
	)
AS
BEGIN
	DELETE
	FROM FuelOrderFees
	WHERE FuelOrderID = @FuelOrderID
END
