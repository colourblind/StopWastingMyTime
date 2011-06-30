BEGIN TRAN

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Job](
	[JobId] [nvarchar](50) NOT NULL,
	[Billable] [bit] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TimeBlock](
	[TimeBlockId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_TimeBlock_TimeBlockId]  DEFAULT (newid()),
	[UserId] [nvarchar](50) NOT NULL,
	[JobId] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Time] [numeric](6, 2) NOT NULL,
 CONSTRAINT [PK_TimeBlock] PRIMARY KEY CLUSTERED 
(
	[TimeBlockId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TimeBlock]  WITH CHECK ADD  CONSTRAINT [FK_TimeBlock_Job] FOREIGN KEY([JobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[TimeBlock] CHECK CONSTRAINT [FK_TimeBlock_Job]
GO
ALTER TABLE [dbo].[TimeBlock]  WITH CHECK ADD  CONSTRAINT [FK_TimeBlock_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[TimeBlock] CHECK CONSTRAINT [FK_TimeBlock_User]
GO

ROLLBACK TRAN -- Replace me with COMMIT TRAN to run!