DROP PROCEDURE IF EXISTS [dbo].[sp_AuditTrailSave_LocalizedContentText]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_AuditTrailSave_LocalizedContentText]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedContentText] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to insert or update an audit trail record for [LocalizedContentText] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedContentText]  
	@groupkey varchar(50) = ''  
	,@fieldkey varchar(100) = ''  
	,@language varchar(5) = ''  
	,@parentkey varchar(100) = ''  
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements. 
	SET NOCOUNT ON; 
 
    DECLARE @tablename varchar (255) = 'LocalizedContentText', 
	@jsondata nvarchar(MAX) = ( 
	SELECT  
		TOP 1 *  
	FROM  
		LocalizedContentText  
	WHERE  
		[GroupKey] = @groupkey
		AND
		[FieldKey] = @fieldkey
		AND
		[ParentKey] = @parentkey
		AND
		[Language] = @language
	FOR JSON AUTO 
	); 
 
    INSERT INTO  
        DatabaseAuditTrail 
    ( 
        [TableName] 
        ,[JsonData] 
        ,[Timestamp] 
    ) 
    VALUES 
    ( 
        @tablename 
        ,@jsondata 
        ,GETDATE() 
    ) 
END 
 
GO
