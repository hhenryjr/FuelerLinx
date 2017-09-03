CREATE TABLE [dbo].[CustomerNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdminClientID] [int] NOT NULL,
	[CustClientID] [int] NOT NULL,
	[Note] [nvarchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[DateAdded] [datetime2](7) NULL,
	[Username] [nvarchar](50) NULL,
	[AddedByUserID] [int] NULL,
	[UpdatedByUserID] [int] NULL,
 CONSTRAINT [PK_ClientNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerNotes] ADD  CONSTRAINT [DF_ClientNotes_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO

ALTER TABLE [dbo].[CustomerNotes]  WITH CHECK ADD  CONSTRAINT [FK_CustomerNotes_AdminClients] FOREIGN KEY([AdminClientID])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[CustomerNotes] CHECK CONSTRAINT [FK_CustomerNotes_AdminClients]
GO

ALTER TABLE [dbo].[CustomerNotes]  WITH CHECK ADD  CONSTRAINT [FK_CustomerNotes_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[CustomerNotes] CHECK CONSTRAINT [FK_CustomerNotes_Users]
GO


