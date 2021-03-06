USE [quartz]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QRTZ_JOB_HISTORY]') AND OBJECTPROPERTY(id, N'ISUSERTABLE') = 1)
ALTER TABLE [dbo].[QRTZ_JOB_HISTORY] DROP CONSTRAINT [FK_QRTZ_JOB_HISTORY_JOB_HISTORY]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QRTZ_JOB_HISTORY]') AND OBJECTPROPERTY(id, N'ISUSERTABLE') = 1)
ALTER TABLE [dbo].[QRTZ_JOB_HISTORY] DROP CONSTRAINT [DF_QRTZ_JOB_HISTORY_JOB_HISTORY_ID]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QRTZ_JOB_HISTORY]') AND OBJECTPROPERTY(id, N'ISUSERTABLE') = 1)
DROP TABLE [dbo].[QRTZ_JOB_HISTORY]
GO

/****** Object:  Table [dbo].[QRTZ_JOB_HISTORY]    Script Date: 10/26/2016 6:48:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[QRTZ_JOB_HISTORY](
	[JOB_HISTORY_ID] [uniqueidentifier] NOT NULL,
	[JOB_DEFINITION_ID] [int] NOT NULL,
	[JOB_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[START_DATETIME] [datetime] NOT NULL,
	[END_DATETIME] [datetime] NULL,
	[JOB_TYPE] [nvarchar](150) NOT NULL,
	[PARENT_JOB_HISTORY_ID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_QRTZ_JOB_HISTORY] PRIMARY KEY CLUSTERED 
(
	[JOB_HISTORY_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[QRTZ_JOB_HISTORY] ADD  CONSTRAINT [DF_QRTZ_JOB_HISTORY_JOB_HISTORY_ID]  DEFAULT (newsequentialid()) FOR [JOB_HISTORY_ID]
GO

ALTER TABLE [dbo].[QRTZ_JOB_HISTORY]  WITH CHECK ADD  CONSTRAINT [FK_QRTZ_JOB_HISTORY_JOB_HISTORY] FOREIGN KEY([PARENT_JOB_HISTORY_ID])
REFERENCES [dbo].[QRTZ_JOB_HISTORY] ([JOB_HISTORY_ID])
GO

ALTER TABLE [dbo].[QRTZ_JOB_HISTORY] CHECK CONSTRAINT [FK_QRTZ_JOB_HISTORY_JOB_HISTORY]
GO


