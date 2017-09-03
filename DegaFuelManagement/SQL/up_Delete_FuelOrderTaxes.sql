GO
/****** Object:  StoredProcedure [dbo].[up_Delete_OtherTaxes]    Script Date: 10/20/2016 15:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Delete_FuelOrderTaxes] (
	@FuelOrderID int
	)
AS
BEGIN
	DELETE
	FROM FuelOrderTaxes
	WHERE FuelOrderID = @FuelOrderID
END
