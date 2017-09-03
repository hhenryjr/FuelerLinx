GO
/****** Object:  StoredProcedure [dbo].[up_Load_EmailDispatches]    Script Date: 6/15/2017 2:13:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 06/15/2017
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_Load_SupplierEmail] (
	@AdminClientID int,
	@SupplierID int
	)
AS
BEGIN
	SELECT SupplierEmail
	FROM Suppliers
	WHERE AdminClientID = @AdminClientID AND Id = @SupplierID
END
