
GO

/****** Object:  Table [dbo].[LoginPermissions]    Script Date: 5/17/2017 7:03:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LoginPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[IsMainAdmin] [bit] NULL,
	[IsMarginEnabled] [bit] NULL,
	[IsDistributionEnabled] [bit] NULL,
	[Dashboard] [smallint] NULL,
	[CompanyGrid] [smallint] NULL,
	[CompanyDetail] [smallint] NULL,
	[ContactGrid] [smallint] NULL,
	[ContactDetail] [smallint] NULL,
	[AirportGrid] [smallint] NULL,
	[AirportDetail] [smallint] NULL,
	[VendorGrid] [smallint] NULL,
	[VendorDetail] [smallint] NULL,
	[MarginMgr] [smallint] NULL,
	[DropZone] [smallint] NULL,
	[Transactions] [smallint] NULL,
	[TaskScheduler] [smallint] NULL,
	[Analysis] [smallint] NULL,
 CONSTRAINT [PK_LoginPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


