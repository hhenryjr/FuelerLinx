
/****** Object:  Table [dbo].[SchedulingImports]    Script Date: 4/6/2017 4:42:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SchedulingImports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchedulingPlatform] [smallint] NULL,
	[AdminClientID] [int] NOT NULL,
	[CustClientID] [int] NOT NULL,
	[TripNumber] [varchar](50) NULL,
	[TailNumber] [varchar](10) NULL,
	[ICAO] [varchar](4) NULL,
	[FBO] [varchar](50) NULL,
	[Arrival] [datetime] NULL,
	[ImportDate] [datetime] NULL,
	[IsReleased] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


