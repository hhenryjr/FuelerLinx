GO
/****** Object:  StoredProcedure [dbo].[up_DistributionEmails_Get]    Script Date: 6/13/2017 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[up_DistributionEmails_Get]
(@AdminClientID int,
@CustClientID int = 0)

AS
BEGIN
	SELECT CD.CustClientID, C.Email
	FROM CustomerDetails CD
	INNER JOIN CustomerAccountingInfo CAI ON CAI.AdminClientID = @AdminClientID
												AND (@CustClientID = 0 OR CAI.CustClientID = @CustClientID) 
												AND ISNULL(IsFuelerLinxCustomer, 0) = 0
	INNER JOIN Contacts C ON C.CustClientID = CAI.CustClientID
								AND C.AdminClientID = @AdminClientID AND C.CustClientID = CD.CustClientID 
								AND ISNULL(CD.IsActive, 0) = 1
								AND ISNULL(C.Distribute, 0) = 1
								AND ISNULL(C.Email, '') <> ''	
								AND (@CustClientID = 0 OR C.CustClientID = @CustClientID)							
	ORDER BY C.CustClientID
	
	SELECT Id, Note FROM PriceMargins WHERE AdminClientID = @AdminClientID
	
	SELECT CustClientID, Name FROM CustomerDetails WHERE AdminClientID = @AdminClientID 
															AND (@CustClientID = 0 OR CustClientID = @CustClientID)
END
