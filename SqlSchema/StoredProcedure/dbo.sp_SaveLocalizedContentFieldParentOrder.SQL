DROP PROCEDURE IF EXISTS [dbo].[sp_SaveLocalizedContentFieldParentOrder]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveLocalizedContentFieldParentOrder]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveLocalizedContentFieldParentOrder] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_SaveLocalizedContentFieldParentOrder] 
	@id bigint
	,@parentkey varchar(100)
	,@order int
	,@auditchangeby varchar(100)
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
	DECLARE @_insertid bigint = @id; 
 
	UPDATE  
		[dbo].[LocalizedContentFields]  
	SET  
		[ParentKey] = @parentkey
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
   
    EXEC [dbo].[sp_AuditTrailSave_LocalizedContentField] @id = @_insertid; 
	SELECT * FROM [dbo].[LocalizedContentFields] WHERE [Id] = @_insertid; 
 
END 
 
GO
