DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedContentFieldRecordByName]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedContentFieldRecordByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedContentFieldRecordByName] AS' 
END
GO

-- ============================================= 
-- Author: Jason L. Walker
-- Create date: 07/06/2022
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_GetLocalizedContentFieldRecordByName] 
    @fieldKey varchar(100),
    @fieldType varchar(20),
    @groupKey varchar(50),
	@parentKey varchar(100),
    @groupfilter varchar(50) = null
AS 
BEGIN 
	SELECT TOP 1  
		[Id]
		,[GroupKey]
		,[FieldKey]
		,[FieldType]
		,[FieldData]
		,[FieldClass]
		,[ParentKey]
		,[DefaultLabel]
		,[WrapperClass]
		,[WrapperHtmlStart]
		,[WrapperHtmlEnd]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditChangeDate]
		,[Order]
	FROM  
		[LocalizedContentFields] 
	WHERE 
		FieldKey = @fieldKey
        --AND
        --FieldType = @fieldType
        AND
        GroupKey = @groupKey
        AND
        ParentKey = @parentKey
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            [GroupKey] LIKE @groupfilter
        )
 
END 
 
GO
