GO
/****** Object:  StoredProcedure [dbo].[up_DistributionEmails_Get]    Script Date: 6/13/2017 4:36:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[up_DistributionEmails_GetLogoURL]
(@AdminClientID int)

AS
BEGIN
	SELECT LogoURL
	FROM ApplicationDomain
	WHERE AdminClientID = @AdminClientID
END
