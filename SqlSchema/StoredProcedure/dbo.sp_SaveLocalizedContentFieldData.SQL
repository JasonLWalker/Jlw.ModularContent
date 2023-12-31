DROP PROCEDURE IF EXISTS [dbo].[sp_SaveLocalizedContentFieldData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveLocalizedContentFieldData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveLocalizedContentFieldData] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_SaveLocalizedContentFieldData] 
	@id bigint
	,@fieldName varchar(100)
    ,@fieldValue varchar(MAX)
	,@auditchangeby varchar(100)
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
	DECLARE @_insertid bigint = @id
        ,@fieldkey varchar(100)
        ,@fieldtype varchar(20)
        ,@fielddata varchar(MAX)
        ,@fieldclass varchar(1024)
        ,@parentkey varchar(100)
        ,@defaultlabel varchar(100)
        ,@wrapperclass varchar(1024)
        ,@wrapperhtmlstart varchar(MAX)
        ,@wrapperhtmlend varchar(MAX)

    SELECT 
        @fieldkey = FieldKey
        ,@fieldtype = FieldType
        ,@fielddata = FieldData
        ,@fieldclass = FieldClass
        ,@parentkey = ParentKey
        ,@defaultlabel = DefaultLabel
        ,@wrapperclass = WrapperClass
        ,@wrapperhtmlstart = WrapperHtmlStart
        ,@wrapperhtmlend = WrapperHtmlEnd
    FROM
		[dbo].[LocalizedContentFields]  
	WHERE          
		Id=@id
        AND 
        (
            @groupfilter IS NULL
            OR
            [GroupKey] LIKE @groupfilter
        );

	UPDATE  
		[dbo].[LocalizedContentFields]  
	SET  
		[FieldKey] = IIF(@fieldName = 'FieldKey', @fieldValue, @fieldkey)
		,[FieldType] = IIF(@fieldName = 'FieldType', @fieldValue, @fieldtype)
		,[FieldData] = IIF(@fieldName = 'FieldData', @fieldValue, @fielddata)
		,[FieldClass] = IIF(@fieldName = 'FieldClass', @fieldValue, @fieldclass)
		,[DefaultLabel] = IIF(@fieldName = 'DefaultLabel', @fieldValue, @defaultlabel)
		,[WrapperClass] = IIF(@fieldName = 'WrapperClass', @fieldValue, @wrapperclass)
		,[WrapperHtmlStart] = IIF(@fieldName = 'WrapperHtmlStart', @fieldValue, @wrapperhtmlstart)
		,[WrapperHtmlEnd] = IIF(@fieldName = 'WrapperHtmlEnd', @fieldValue, @wrapperhtmlend)
		,[AuditChangeType] = 'UPDATE'
		,[AuditChangeBy] = @auditchangeby
		,[AuditChangeDate] = GETDATE()
	WHERE          
		Id=@id
        AND 
        (
            @groupfilter IS NULL
            OR
            [GroupKey] LIKE @groupfilter
        )

    EXEC [dbo].[sp_AuditTrailSave_LocalizedContentField] @id = @_insertid; 
	SELECT * FROM [dbo].[LocalizedContentFields] WHERE [Id] = @_insertid; 
 
END 
 
GO
