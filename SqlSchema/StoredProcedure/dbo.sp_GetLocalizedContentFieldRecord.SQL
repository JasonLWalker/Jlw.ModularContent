DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedContentFieldRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedContentFieldRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedContentFieldRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of @groupfilter
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedContentFieldRecord] 
	@id bigint
    ,@groupfilter varchar(50) = null
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
		Id=@id
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
