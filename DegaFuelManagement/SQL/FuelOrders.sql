--FUEL ORDERS TABLE--
ALTER TABLE [dbo].[FuelOrders] ADD  CONSTRAINT [DF_FuelOrders_IsArchived]  DEFAULT ((0)) FOR [IsArchived]
GO

ALTER TABLE [dbo].[FuelOrders] ADD  CONSTRAINT [DF_FuelOrders_IsDirectlyEntered]  DEFAULT ((0)) FOR [IsDirectlyEntered]
GO

ALTER TABLE [dbo].[FuelOrders] ADD  CONSTRAINT [DF_FuelOrders_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO

ALTER TABLE [dbo].[FuelOrders]  WITH CHECK ADD  CONSTRAINT [FK_FuelOrders_AdminClients] FOREIGN KEY([AdminClientID])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[FuelOrders] CHECK CONSTRAINT [FK_FuelOrders_AdminClients]
GO

ALTER TABLE [dbo].[FuelOrders]  WITH CHECK ADD  CONSTRAINT [FK_FuelOrders_CustClients] FOREIGN KEY([CustClientID])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[FuelOrders] CHECK CONSTRAINT [FK_FuelOrders_CustClients]
GO

ALTER TABLE [dbo].[FuelOrders]  WITH CHECK ADD  CONSTRAINT [FK_FuelOrders_FuelOrders] FOREIGN KEY([Id])
REFERENCES [dbo].[FuelOrders] ([Id])
GO

ALTER TABLE [dbo].[FuelOrders] CHECK CONSTRAINT [FK_FuelOrders_FuelOrders]
GO
----------
