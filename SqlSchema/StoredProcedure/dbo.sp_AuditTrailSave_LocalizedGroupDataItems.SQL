DROP PROCEDURE IF EXISTS [dbo].[sp_AuditTrailSave_LocalizedGroupDataItems]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_AuditTrailSave_LocalizedGroupDataItems]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedGroupDataItems] AS' 
END
GO

-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Description:	Used to insert or update an audit trail record for [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedGroupDataItems]  
	@id bigint
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements. 
	SET NOCOUNT ON; 
 
    DECLARE @tablename varchar (255) = 'LocalizedGroupDataItems', 
	@jsondata nvarchar(MAX) = ( 
	SELECT  
		TOP 1 *  
	FROM  
		LocalizedGroupDataItems  
	WHERE  
		[Id] = @id
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
