DROP PROCEDURE IF EXISTS [dbo].[sp_SaveLocalizedContentFieldRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveLocalizedContentFieldRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveLocalizedContentFieldRecord] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_SaveLocalizedContentFieldRecord] 
	@id bigint
	,@groupkey varchar(50)
	,@fieldkey varchar(100)
	,@fieldtype varchar(20)
	,@fielddata varchar(MAX)
	,@fieldclass varchar(1024)
	,@parentkey varchar(100)
	,@defaultlabel varchar(100)
	,@wrapperclass varchar(255)
	,@wrapperhtmlstart varchar(4096)
	,@wrapperhtmlend varchar(4096)
	,@auditchangeby varchar(100)
	,@order int
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
	DECLARE @_insertid bigint = @id; 
 
    IF((SELECT @groupkey WHERE (@groupfilter IS NULL OR @groupfilter = '' OR @groupkey LIKE @groupfilter {ESCAPE '\'})) IS NULL)
    BEGIN
        PRINT 'Permission Denied - GroupFilter does not match GroupKey'
        SELECT 'ERROR' AS AuditChangeType, '403' as FieldType, 'Permission denied for wizard with the name "' + @groupkey + '"' as FieldData 
        RETURN 403; -- 
    END

	UPDATE  
		[dbo].[LocalizedContentFields]  
	SET  
		[GroupKey] = @groupkey
		,[FieldKey] = @fieldkey
		,[FieldType] = @fieldtype
		,[FieldData] = @fielddata
		,[FieldClass] = @fieldclass
		,[ParentKey] = @parentkey
		,[DefaultLabel] = @defaultlabel
		,[WrapperClass] = @wrapperclass
		,[WrapperHtmlStart] = @wrapperhtmlstart
		,[WrapperHtmlEnd] = @wrapperhtmlend
		,[AuditChangeType] = 'UPDATE'
		,[AuditChangeBy] = @auditchangeby
		,[AuditChangeDate] = GETDATE()
		,[Order] = @order
	WHERE          
		Id=@id
        AND 
        (
            @groupfilter IS NULL
            OR
            [GroupKey] LIKE @groupfilter
        )
  
	IF @@ROWCOUNT=0 
	BEGIN 
		INSERT INTO [dbo].LocalizedContentFields  
		(  
		[GroupKey]
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
		)  
		VALUES   
		(  
			@groupkey
			,@fieldkey
			,@fieldtype
			,@fielddata
			,@fieldclass
			,@parentkey
			,@defaultlabel
			,@wrapperclass
			,@wrapperhtmlstart
			,@wrapperhtmlend
			,'INSERT'
			,@auditchangeby
			,GETDATE()
			,@order
		); 
 
		SET @_insertid = SCOPE_IDENTITY(); 
	END; 
 
    EXEC [dbo].[sp_AuditTrailSave_LocalizedContentField] @id = @_insertid; 
	SELECT * FROM [dbo].[LocalizedContentFields] WHERE [Id] = @_insertid; 
 
END 
 
GO
