GO
/****** Object:  StoredProcedure [dbo].[up_Transactions_OtherFeesCompanyDefaultGet]    Script Date: 10/20/2016 15:43:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Transactions_FuelOrderFeesClientDefaultGet] 
	@ClientID int
AS
BEGIN
	SELECT *
	FROM ClientFees
	WHERE ClientID = @ClientID
END
