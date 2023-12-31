DROP PROCEDURE IF EXISTS [dbo].[sp_AuditTrailSave_LocalizedContentField]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_AuditTrailSave_LocalizedContentField]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedContentField] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/05/2021 02:22 PM 
-- Description:	Used to insert or update an audit trail record for [LocalizedContentFields] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_AuditTrailSave_LocalizedContentField]  
	@id bigint
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements. 
	SET NOCOUNT ON; 
 
    DECLARE @tablename varchar (255) = 'LocalizedContentFields', 
	@jsondata nvarchar(MAX) = ( 
	SELECT  
		TOP 1 *  
	FROM  
		LocalizedContentFields
	WHERE  
		Id=@id
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
