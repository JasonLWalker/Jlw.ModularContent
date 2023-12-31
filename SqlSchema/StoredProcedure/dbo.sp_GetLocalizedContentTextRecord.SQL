DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedContentTextRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedContentTextRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedContentTextRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentText] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedContentTextRecord] 
	@groupKey [varchar](50),
	@fieldKey [varchar](100),
	@language [varchar](5),
	@parentKey [varchar](100) = ''
AS 
BEGIN 
	SELECT TOP 1  
		[GroupKey]
		,[FieldKey]
		,[ParentKey]
		,[Language]
		,[Text]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditChangeDate]
	FROM  
		[LocalizedContentText] 
	WHERE 
		[GroupKey] = @groupKey
		AND
		[FieldKey] = @fieldKey
		AND
		[ParentKey] = @parentKey
		AND
		[Language] = @language
END 
 
GO
