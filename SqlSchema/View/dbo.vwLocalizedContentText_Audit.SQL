SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the Audit records for the table [LocalizedContentText] 
-- ============================================= 
CREATE VIEW [dbo].[vwLocalizedContentText_Audit] AS  
SELECT  
    jData.*  
FROM  
    DatabaseAuditTrail aud  
CROSS APPLY  
    OPENJSON(IIF(ISJSON(JsonData) > 0, JsonData, '[{}]')) 
    WITH ( 
		[GroupKey] varchar(50) N'$.GroupKey'
		,[FieldKey] varchar(100) N'$.FieldKey'
		,[ParentKey] varchar(100) N'$.ParentKey'
		,[Language] varchar(5) N'$.Language'
		,[Text] nvarchar(MAX) N'$.Text'
		,[AuditChangeType] varchar(100) N'$.AuditChangeType'
		,[AuditChangeBy] varchar(100) N'$.AuditChangeBy'
		,[AuditChangeDate] datetime N'$.AuditChangeDate'
 
    ) jData  
WHERE  
    aud.TableName = 'LocalizedContentText' 
GO
