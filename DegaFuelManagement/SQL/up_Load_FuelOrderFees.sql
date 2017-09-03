GO
/****** Object:  StoredProcedure [dbo].[up_Load_OtherFees]    Script Date: 10/20/2016 15:40:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Load_FuelOrderFees] (
	@FuelOrderID int
	)
AS
BEGIN
	SELECT FuelOrderID
      ,[FeeDesc] AS FeeDesc
      ,[FeeAmount] AS FeeAmount
	FROM FuelOrderFees
	WHERE FuelOrderID = @FuelOrderID
END
