GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrdersByAndCustClientID]    Script Date: 7/17/2017 3:54:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[up_Select_HighestTransactionID]
	@ClientID int,
	@Domain varchar(50) = ''
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT MAX(f.ID)
FROM
	FuelOrders f
INNER JOIN ApplicationDomain AD ON 
	((@Domain = '' AND f.AdminClientID = AD.AdminClientID AND f.AdminClientID = @ClientID) OR
		(AD.Domain = @Domain AND f.AdminClientID = AD.AdminClientID AND f.[CustClientID] = @ClientID)
	)


--endregion

