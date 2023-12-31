SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedContentText](
	[GroupKey] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FieldKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParentKey] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Language] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Text] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeType] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AuditChangeDate] [datetime] NOT NULL,
 CONSTRAINT [pkLocalizedContentText] PRIMARY KEY CLUSTERED 
(
	[GroupKey] ASC,
	[FieldKey] ASC,
	[Language] ASC,
	[ParentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[LocalizedContentText] ADD  DEFAULT ('') FOR [ParentKey]
GO
ALTER TABLE [dbo].[LocalizedContentText] ADD  DEFAULT ('') FOR [AuditChangeType]
GO
ALTER TABLE [dbo].[LocalizedContentText] ADD  DEFAULT ('') FOR [AuditChangeBy]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Used to store text, label, and error metadata to be used by individual fields on a wizard screen' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LocalizedContentText'
GO
