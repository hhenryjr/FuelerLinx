GO
/****** Object:  StoredProcedure [dbo].[up_Transactions_FuelOrderTaxesClientDefaultGet]    Script Date: 10/20/2016 15:44:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[up_Transactions_FuelOrderTaxesClientDefaultGet] 
	@ClientID int
AS
BEGIN
	SELECT *
	FROM ClientTaxes
	WHERE ClientID = @ClientID
END
