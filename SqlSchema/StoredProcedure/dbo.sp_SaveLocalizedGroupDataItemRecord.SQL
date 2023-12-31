DROP PROCEDURE IF EXISTS [dbo].[sp_SaveLocalizedGroupDataItemRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveLocalizedGroupDataItemRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveLocalizedGroupDataItemRecord] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 01:20 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey and Key
-- Description:	Used to insert or update a matching record in [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_SaveLocalizedGroupDataItemRecord] 
	@id bigint
	,@language varchar(5)
	,@groupkey nvarchar(50)
	,@key nvarchar(100)
	,@value nvarchar(255)
	,@order int
	,@description nvarchar(100)
	,@data nvarchar(MAX)
	,@auditchangeby nvarchar(40)
AS 
BEGIN 
	DECLARE @_insertid bigint = @id; 
 
	UPDATE  
		[dbo].[LocalizedGroupDataItems]  
	SET  
		[Language] = @language
		,[GroupKey] = @groupkey
		,[Key] = @key
		,[Value] = @value
		,[Order] = @order
		,[Description] = @description
		,[Data] = @data
		,[AuditChangeType] = 'UPDATE'
		,[AuditChangeBy] = @auditchangeby
		,[AuditChangeDate] = GETDATE()
	WHERE          
		Id=@id
  
	IF @@ROWCOUNT=0 
	BEGIN 
		INSERT INTO [dbo].LocalizedGroupDataItems  
		(  
		[Language]
		,[GroupKey]
		,[Key]
		,[Value]
		,[Order]
		,[Description]
		,[Data]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditchangeDate]
		)  
		VALUES   
		(  
			@language
			,@groupkey
			,@key
			,@value
			,@order
			,@description
			,@data
			,'INSERT'
			,@auditchangeby
			,GETDATE()
		); 
 
		SET @_insertid = SCOPE_IDENTITY(); 
	END; 

	EXEC sp_AuditTrailSave_LocalizedGroupDataItems @_insertid; 

	--SELECT @@ROWCOUNT; 
	SELECT * FROM [dbo].LocalizedGroupDataItems WHERE Id=@_insertid;

END 
 
GO
