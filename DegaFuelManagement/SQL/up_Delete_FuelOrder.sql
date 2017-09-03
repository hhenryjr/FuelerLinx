--[up_Delete_FuelOrder]
ALTER PROCEDURE [dbo].[up_Delete_FuelOrder]
	@Id int
AS

SET NOCOUNT ON

DELETE FROM FuelOrderInvoices
WHERE
	FuelOrderID = @Id

DELETE FROM FuelOrderTaxes
WHERE
	FuelOrderID = @Id

DELETE FROM FuelOrderFees
WHERE
	FuelOrderID = @Id

DELETE FROM FuelOrderCustomerPricings
WHERE
	FuelOrderID = @Id

DELETE FROM FuelOrderPricings
WHERE
	FuelOrderID = @Id

DELETE FROM [dbo].[FuelOrders]
WHERE
	[Id] = @Id

GO
--endregion
