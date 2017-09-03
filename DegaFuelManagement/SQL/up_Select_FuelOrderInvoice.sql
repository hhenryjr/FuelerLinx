--[up_Select_FuelOrderInvoice]
ALTER proc [dbo].[up_Select_FuelOrderInvoice]
@Id int

as
SELECT [Id]
      ,[FuelOrderID]
      ,[InvoiceData]
      ,[InvoiceName]
      ,[ContentType]
  FROM [FuelOrderInvoices]

  where @Id = Id
GO
--endregion
