DROP PROCEDURE IF EXISTS [dbo].[sp_SaveLocalizedContentTextRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_SaveLocalizedContentTextRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_SaveLocalizedContentTextRecord] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Description:	Used to retrieve the first matching record from [LocalizedContentText] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_SaveLocalizedContentTextRecord] 
	@groupkey varchar(50)
	,@fieldkey varchar(100)
	,@language varchar(5) = 'EN'
	,@text nvarchar(MAX)
	,@auditchangeby varchar(100)
  	,@groupfilter varchar(50) = null
	,@parentkey varchar(100) = ''
AS 
BEGIN 
	IF (@groupFilter is NULL OR @groupFilter='' OR @groupKey LIKE @groupFilter )
	BEGIN
		UPDATE  
			[dbo].[LocalizedContentText]  
		SET  
			[Text] = @text
			,[AuditChangeType] = 'UPDATE'
			,[AuditChangeBy] = @auditchangeby
			,[AuditChangeDate] = GETDATE()
		WHERE          
			[GroupKey] = @groupkey
			AND
			[FieldKey] = @fieldkey
			AND
			[Language] = @language
			AND
			[ParentKey] = @parentkey
			AND 
			(
					@groupfilter IS NULL
					OR
					@groupfilter = ''
					OR
					[GroupKey] LIKE @groupfilter
			)
		
		IF @@ROWCOUNT=0 
		BEGIN 
			INSERT INTO [dbo].LocalizedContentText  
			(  
				[GroupKey]
			,[FieldKey]
			,[ParentKey]
			,[Language]
			,[Text]
			,[AuditChangeType]
			,[AuditChangeBy]
			,[AuditChangeDate]
			)  
			VALUES   
			(  
				@groupkey
				,@fieldkey
				,@parentkey
				,@language
				,@text
				,'INSERT'
				,@auditchangeby
				,GETDATE()
			); 
		END; 
	 
		EXEC [sp_AuditTrailSave_LocalizedContentText] @groupkey, @fieldkey, @language, @parentKey; 
	END;

	SELECT * FROM [dbo].[LocalizedContentText] WHERE 		
		[GroupKey] = @groupkey
		AND
		[FieldKey] = @fieldkey
		AND
		[ParentKey] = @parentkey
		AND
		[Language] = @language
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
