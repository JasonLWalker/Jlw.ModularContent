SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedContentFields](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupKey] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FieldKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FieldType] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FieldData] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FieldClass] [varchar](1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParentKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DefaultLabel] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WrapperClass] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WrapperHtmlStart] [varchar](4096) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[WrapperHtmlEnd] [varchar](4096) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Order] [int] NOT NULL,
	[AuditChangeType] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeDate] [datetime] NOT NULL,
 CONSTRAINT [pkLocalizedContentFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE UNIQUE NONCLUSTERED INDEX [unqFormFields] ON [dbo].[LocalizedContentFields]
(
	[GroupKey] ASC,
	[FieldKey] ASC,
	[ParentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocalizedContentFields] ADD  DEFAULT ((0)) FOR [Order]
GO
ALTER TABLE [dbo].[LocalizedContentFields] ADD  DEFAULT ('') FOR [AuditChangeType]
GO
ALTER TABLE [dbo].[LocalizedContentFields] ADD  DEFAULT ('') FOR [AuditChangeBy]
GO
ALTER TABLE [dbo].[LocalizedContentFields] ADD  DEFAULT ('') FOR [AuditChangeDate]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Used to store individual elements and metadata to be used by a wizard screen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalizedContentFields'
GO
