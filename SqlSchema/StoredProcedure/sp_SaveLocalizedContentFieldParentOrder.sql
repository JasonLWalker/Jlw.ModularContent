SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Description:	Used to retrieve the first matching record from [LocalizedContentFields] 
-- ============================================= 
 
CREATE PROCEDURE [dbo].[sp_SaveLocalizedContentFieldParentOrder] 
	@id bigint
	,@parentkey varchar(20)
	,@order int
	,@auditchangeby varchar(100)
    ,@groupfilter varchar(40) = null
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
