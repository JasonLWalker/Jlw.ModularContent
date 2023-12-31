DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteLocalizedContentFieldRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DeleteLocalizedContentFieldRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_DeleteLocalizedContentFieldRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of @groupfilter
-- Description:	Used to remove matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_DeleteLocalizedContentFieldRecord] 
	@id bigint
	,@auditchangeby varchar(100) = ''  
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
 
	UPDATE 
		[LocalizedContentFields] 
	SET  
		[AuditChangeType] = 'DELETE', 
		[AuditChangeBy] = @auditchangeby, 
		[AuditChangeDate] = GETDATE() 
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
	; 
 
	EXEC sp_AuditTrailSave_LocalizedContentField @id=@id; 
 
	DELETE 
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
	; 
 
 
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
	; 
END 
 
GO
