DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteLocalizedGroupDataItemRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DeleteLocalizedGroupDataItemRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_DeleteLocalizedGroupDataItemRecord] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Description:	Used to remove matching record from [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_DeleteLocalizedGroupDataItemRecord] 
	@id bigint
	,@auditchangeby varchar(100) = ''	
AS 
BEGIN 
 
	UPDATE 
		[LocalizedGroupDataItems] 
	SET  
		[AuditChangeType] = 'DELETE', 
		[AuditChangeBy] = @auditchangeby, 
		[AuditChangeDate] = GETDATE() 
	WHERE 
		[Id] = @id
	; 
 
	EXEC sp_AuditTrailSave_LocalizedGroupDataItems @id; 
 
	DELETE 
	FROM  
		[LocalizedGroupDataItems] 
	WHERE 
		[Id] = @id
	; 
 
 
	SELECT TOP 1 
		[Id]
		,[Language]
		,[GroupKey]
		,[Key]
		,[Value]
		,[Order]
		,[Description]
		,[Data]
	FROM  
		[LocalizedGroupDataItems] 
	WHERE 
		[Id] = @id
	; 
END 
 
GO
