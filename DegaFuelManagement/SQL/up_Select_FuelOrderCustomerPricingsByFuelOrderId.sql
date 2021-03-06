GO
/****** Object:  StoredProcedure [dbo].[up_Select_FuelOrderCustomerPricingsByFuelOrderId]    Script Date: 1/12/2017 3:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mike Mieglitz
-- Create date: 1/5/2017
-- Description:	Grab FuelOrderSupplierPricings by the fuel order ID
-- =============================================
ALTER PROCEDURE [dbo].[up_Select_FuelOrderCustomerPricingsByFuelOrderId] 
	@fuelOrderId int
AS
BEGIN
	select *
	from FuelOrderCustomerPricings
	where FuelOrderId = @fuelOrderId
	order by PriceTierMin
END
