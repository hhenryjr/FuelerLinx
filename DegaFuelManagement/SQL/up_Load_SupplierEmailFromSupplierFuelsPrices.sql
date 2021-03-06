GO
/****** Object:  StoredProcedure [dbo].[up_Load_SupplierEmailFromSupplierFuelsPrices]    Script Date: 7/3/2017 1:13:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 06/15/2017
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[up_Load_SupplierEmailFromSupplierFuelsPrices] (
	@AdminClientID int,
	@SupplierID int,
	@ICAO varchar(4),
	@FBO varchar(50),
	@Product varchar(50)
	)
AS
BEGIN
	SELECT VendorEmail
	FROM SupplierFuelsPrices
	WHERE AdminClientID = @AdminClientID AND SupplierID = @SupplierID AND ICAO = @ICAO AND FBOName = @FBO AND Product = @Product
END
