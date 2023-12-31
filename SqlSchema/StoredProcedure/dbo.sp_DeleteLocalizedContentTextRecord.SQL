DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteLocalizedContentTextRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DeleteLocalizedContentTextRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_DeleteLocalizedContentTextRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to remove the matching record from [LocalizedContentText] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_DeleteLocalizedContentTextRecord] 
	@groupkey varchar(50) = ''  
	,@fieldkey varchar(100) = ''  
	,@language varchar(5) = ''  
	,@auditchangeby varchar(100) = ''  
    ,@groupfilter varchar(50) = null
	,@parentkey varchar(100) = ''  
AS 
BEGIN 
 
	UPDATE 
		[LocalizedContentText] 
	SET  
		[AuditChangeType] = 'DELETE', 
		[AuditChangeBy] = @auditchangeby, 
		[AuditChangeDate] = GETDATE() 
	WHERE 
		[GroupKey] = @groupkey
		AND
		[FieldKey] = @fieldkey
		AND
		[ParentKey] = @parentkey
		AND
		[Language] = @language
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            [GroupKey] LIKE @groupfilter
        )
	; 
 
	EXEC sp_AuditTrailSave_LocalizedContentText @groupkey, @fieldkey, @language, @parentkey; 
 
	DELETE 
	FROM  
		[LocalizedContentText] 
	WHERE 
		[GroupKey] = @groupkey
		AND
		[FieldKey] = @fieldkey
		AND
		[ParentKey] = @parentkey
		AND
		[Language] = @language
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            [GroupKey] LIKE @groupfilter
        )
	; 
 
 
	SELECT TOP 1 
		[GroupKey]
		,[FieldKey]
		,[Language]
		,[Text]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditChangeDate]
        ,[ParentKey]
	FROM  
		[LocalizedContentText] 
	WHERE 
		[GroupKey] = @groupkey
		AND
		[FieldKey] = @fieldkey
		AND
		[ParentKey] = @parentkey
		AND
		[Language] = @language
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            [GroupKey] LIKE @groupfilter
        )
	; 
END 
 
GO
