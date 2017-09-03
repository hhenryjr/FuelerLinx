GO
/****** Object:  StoredProcedure [dbo].[up_CompanyTaxes_Update]    Script Date: 10/20/2016 15:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_ClientTaxes_Update]
	@CommaDelimtedListOfTaxes varchar(max) = '',
	@ClientID int
AS
BEGIN
	delete cf
	from ClientTaxes cf
	where cf.ClientID = @ClientID
	
	insert into ClientTaxes (ClientID, TaxDesc)
	select @ClientID, a.Value
	from dbo.fn_Split(@CommaDelimtedListOfTaxes, ',') a
END
