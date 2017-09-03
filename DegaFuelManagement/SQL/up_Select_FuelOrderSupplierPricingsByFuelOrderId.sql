ALTER PROCEDURE [dbo].[up_Select_FuelOrderSupplierPricingsByFuelOrderId] 
	@fuelOrderId int
AS
BEGIN
	select	FBOName,
			Vendor,
			[Min],
			[Max],
			SupplierID,
			TotalWithTax,
			case when isnull(Product, '')='' then 'Jet' else Product end as Product
	from FuelOrderSupplierPricings fsp
	where FuelOrderId = @fuelOrderId
END
