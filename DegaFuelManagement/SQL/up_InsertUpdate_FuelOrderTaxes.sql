GO
/****** Object:  StoredProcedure [dbo].[up_InsertUpdate_OtherTaxes]    Script Date: 10/20/2016 14:53:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	Insert Fuel Order Taxes
-- =============================================
create PROCEDURE [dbo].[up_InsertUpdate_FuelOrderTaxes] (
	@FuelOrderID int,
	@ClientID int,
	@TaxDesc varchar(255) = '',
	@TaxGal float = 0,
	@TaxAmt float = 0,
	@OmitPPG bit = 0,
	@IsCustomizable bit = 0
	)
AS
BEGIN
	INSERT INTO FuelOrderTaxes(FuelOrderID, TaxDesc, TaxGal, TaxAmt, OmitPPG, IsCustomizable)
	VALUES (@FuelOrderID, @TaxDesc, @TaxGal, @TaxAmt, @OmitPPG, @IsCustomizable)
END
