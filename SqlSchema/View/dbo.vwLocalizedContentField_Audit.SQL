SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve the Audit records for the table [LocalizedContentFields] 
-- ============================================= 
CREATE VIEW [dbo].[vwLocalizedContentField_Audit] AS 
SELECT  
    jData.*  
FROM  
    DatabaseAuditTrail aud  
CROSS APPLY  
    OPENJSON(IIF(ISJSON(JsonData) > 0, JsonData, '[{}]')) 
    WITH ( 
		[Id] bigint N'$.Id'
		,[GroupKey] varchar(50) N'$.GroupKey'
		,[FieldKey] varchar(100) N'$.FieldKey'
		,[FieldType] varchar(20) N'$.FieldType'
		,[FieldData] varchar(MAX) N'$.FieldData'
		,[FieldClass] varchar(1024) N'$.FieldClass'
		,[ParentKey] varchar(100) N'$.ParentKey'
		,[DefaultLabel] varchar(100) N'$.DefaultLabel'
		,[WrapperClass] varchar(255) N'$.WrapperClass'
		,[WrapperHtmlStart] varchar(2048) N'$.WrapperHtmlStart'
		,[WrapperHtmlEnd] varchar(2048) N'$.WrapperHtmlEnd'
		,[Order] int N'$.Order'
		,[AuditChangeType] varchar(20) N'$.AuditChangeType'
		,[AuditChangeBy] varchar(100) N'$.AuditChangeBy'
		,[AuditChangeDate] DateTime N'$.AuditChangeDate'
 
    ) jData  
WHERE  
    aud.TableName = 'LocalizedContentFields'
GO
