DROP PROCEDURE IF EXISTS [dbo].[sp_GetWizardContentFieldRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetWizardContentFieldRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetWizardContentFieldRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetWizardContentFieldRecord] 
	@id bigint
    ,@language varchar(40) = null
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
	SELECT TOP 1  
		[Id]
		,f.[GroupKey]
		,f.[FieldKey]
		,[FieldType]
		,[FieldData]
		,[FieldClass]
		,f.[ParentKey]
		,[DefaultLabel]
		,[WrapperClass]
		,[WrapperHtmlStart]
		,[WrapperHtmlEnd]
		,f.[AuditChangeType]
		,f.[AuditChangeBy]
		,f.[AuditChangeDate]
		,[Order]
        ,t.[Text] as [Label]
        ,t.[Language]
	FROM  
		[LocalizedContentFields] f
        LEFT JOIN
        [LocalizedContentText] t
        ON 
            t.GroupKey = f.GroupKey
            AND
            t.FieldKey = f.FieldKey
            AND
            t.ParentKey = f.ParentKey
            AND
            t.[Language] = @language
	WHERE 
		Id=@id
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            f.[GroupKey] LIKE @groupfilter
        )
 
END 
 
GO
