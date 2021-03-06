GO
/****** Object:  StoredProcedure [dbo].[up_InsertUpdate_OtherFees]    Script Date: 10/20/2016 15:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chau Ly
-- Create date: 10/20/2016
-- Description:	
-- =============================================
create PROCEDURE [dbo].[up_InsertUpdate_FuelOrderFees] (
	@FuelOrderID int,
	@FeeDesc varchar(255) = '',
	@FeeAmount float = 0
	)
AS
BEGIN
	IF (EXISTS (SELECT * FROM FuelOrderFees WHERE FuelOrderID = @FuelOrderID AND FeeDesc = @FeeDesc))
		BEGIN
			UPDATE FuelOrderFees
			SET FeeAmount = @FeeAmount
			WHERE FuelOrderID = @FuelOrderID AND
			FeeDesc = @FeeDesc
		END
	ELSE
		BEGIN
			INSERT INTO FuelOrderFees(FuelOrderID, FeeDesc, FeeAmount)
			VALUES (@FuelOrderID, @FeeDesc, @FeeAmount)
		END
END
