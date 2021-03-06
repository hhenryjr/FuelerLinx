GO
/****** Object:  StoredProcedure [dbo].[up_Load_OtherTaxes]    Script Date: 10/20/2016 15:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Load_FuelOrderTaxes] (
	@FuelOrderID int
	)
AS
BEGIN
	SELECT FuelOrderID,
	ClientID
      ,[TaxDesc] AS TaxDesc
      ,[TaxGal] AS TaxGal
      ,[TaxAmt] AS TaxAmt
      , OmitPPG
      , IsCustomizable
	FROM FuelOrderTaxes
	WHERE FuelOrderID = @FuelOrderID
END
