/****** Object:  Table [dbo].[AircraftExclusions]    Script Date: 6/24/2017 12:29:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AircraftExclusions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdminClientID] [int] NOT NULL,
	[CustClientID] [int] NULL,
	[ICAO] [varchar](4) NOT NULL,
	[FBO] [nvarchar](max) NOT NULL,
	[AircraftID] [int] NULL,
	[TailNumbers] [nvarchar](max) NOT NULL,
	[Margin] [decimal](18, 5) NULL,
	[IsExcluded] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AircraftExclusions]  WITH CHECK ADD  CONSTRAINT [FK_AircraftExclusions_Clients_Admin] FOREIGN KEY([AdminClientID])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[AircraftExclusions] CHECK CONSTRAINT [FK_AircraftExclusions_Clients_Admin]
GO
----
