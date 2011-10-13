BEGIN TRAN

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[ClientId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Client_ClientId]  DEFAULT (newid()),
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[User](
	[UserId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
    [Active] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Job](
	[JobId] [nvarchar](50) NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Billable] [bit] NOT NULL,
	[QuotedHours] [decimal](6, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Client]

CREATE TABLE [dbo].[TimeBlock](
	[TimeBlockId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_TimeBlock_TimeBlockId]  DEFAULT (newid()),
	[UserId] [nvarchar](50) NOT NULL,
	[JobId] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Time] [numeric](6, 2) NOT NULL,
	[Comment] [nvarchar](2000) NULL,
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


CREATE TABLE [dbo].[Permission](
	[PermissionId] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[UserPermissionJoin](
	[UserId] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[PermissionId] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_UserPermissionJoin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserPermissionJoin]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissionJoin_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([PermissionId])
GO
ALTER TABLE [dbo].[UserPermissionJoin] CHECK CONSTRAINT [FK_UserPermissionJoin_Permission]
GO
ALTER TABLE [dbo].[UserPermissionJoin]  WITH CHECK ADD  CONSTRAINT [FK_UserPermissionJoin_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserPermissionJoin] CHECK CONSTRAINT [FK_UserPermissionJoin_User]




INSERT INTO [User] (UserId, Password, Name, Active) VALUES ('admin', 'LQ4/uCiyHKMAL4i6shtWMcWiOuvz4Vs=', 'Admin', 1)
GO

INSERT INTO [Permission] (PermissionId, Description) VALUES ('USER_ADMIN', 'Create, edit and delete users')
INSERT INTO [Permission] (PermissionId, Description) VALUES ('REPORTS', 'Download reports')
GO

INSERT INTO [UserPermissionJoin] (UserId, PermissionId) VALUES ('admin', 'USER_ADMIN')
INSERT INTO [UserPermissionJoin] (UserId, PermissionId) VALUES ('admin', 'REPORTS')
GO

ROLLBACK TRAN -- Replace me with COMMIT TRAN to run!
