SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedGroupDataItems](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Language] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GroupKey] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Key] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Value] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Order] [int] NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Data] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeType] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeDate] [datetime] NOT NULL,
 CONSTRAINT [pkLocalizedGroupDataItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [unqLanguageGroupKeyKeyValue] UNIQUE NONCLUSTERED 
(
	[Language] ASC,
	[GroupKey] ASC,
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('EN') FOR [Language]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [GroupKey]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [Key]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [Value]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ((0)) FOR [Order]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [Data]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [AuditChangeType]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT ('') FOR [AuditChangeBy]
GO
ALTER TABLE [dbo].[LocalizedGroupDataItems] ADD  DEFAULT (getdate()) FOR [AuditChangeDate]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Used to store sets of data that may be grouped and sorted logically' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalizedGroupDataItems'
GO
