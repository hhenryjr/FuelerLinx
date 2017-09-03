GO
/****** Object:  StoredProcedure [dbo].[up_CompanyFees_Update]    Script Date: 10/20/2016 15:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_ClientFees_Update]
	@CommaDelimtedListOfFees varchar(max) = '',
	@ClientID int
AS
BEGIN
	delete cf
	from ClientFees cf
	where cf.ClientID = @ClientID
	
	insert into ClientFees (ClientID, FeeDesc)
	select @ClientID, a.Value
	from dbo.fn_Split(@CommaDelimtedListOfFees, ',') a
END
