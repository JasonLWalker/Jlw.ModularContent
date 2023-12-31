SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatabaseAuditTrail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[JsonData] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [pkDatabaseAuditTrail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Used to store an audit trail of records in JSON format' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DatabaseAuditTrail'
GO
