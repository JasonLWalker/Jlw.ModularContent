DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedContentTextDt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedContentTextDt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedContentTextDt] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to generate a DataTable list of matching records from [LocalizedContentText] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedContentTextDt] 
    @sSearch varchar(MAX) = '%%', 
    @nRowStart int = 0, 
    @nPageSize int = 10, 
    @sSortCol varchar(20) = null, 
    @sSortDir varchar(4) = 'ASC',
	@sGroupKey [varchar](50) = null,
	@sFieldKey [varchar](100) = null,
	@sLanguage [varchar](5) =  null,
	@sParentKey [varchar](100) = null
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
 
	-- Retrieve count of total records without search filter. 
    DECLARE @jlwDtTotalCount int = ( 
    SELECT COUNT(*) FROM  
        LocalizedContentText 
    ); 
 
    SELECT 
		[GroupKey]
		,[FieldKey]
		,[Language]
		,[Text]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditChangeDate]
		,[ParentKey] 
        ,@jlwDtTotalCount AS jlwDtTotalCount 
        ,COUNT(*) OVER () AS jlwDtFilteredCount  
	FROM 
		[LocalizedContentText] 
    WHERE  
        CONCAT(' ', ' ') LIKE @sSearch Collate SQL_Latin1_General_CP1_CI_AS 
        -- 
        AND
        (
            @sGroupKey IS NULL
            OR
            @sGroupKey = ''
            OR
            [GroupKey] = @sGroupKey
        )
        AND
        (
            @sFieldKey IS NULL
            OR
            @sFieldKey = ''
            OR
            [FieldKey] = @sFieldKey
        )
        AND
        (
            @sParentKey IS NULL
            OR
            @sParentKey = ''
            OR
            [ParentKey] = @sParentKey
        )
        AND
        (
            @sLanguage IS NULL
            OR
            @sLanguage = ''
            OR
            [Language] = @sLanguage
        )

    ORDER BY  
        CASE @sSortDir WHEN 'DESC' THEN  
            CASE @sSortCol  
                WHEN '' THEN ''  
            END  
        END DESC, 
        CASE @sSortDir WHEN 'ASC' THEN 
            CASE @sSortCol  
                WHEN '' THEN '' 
            END 
        END ASC 
    OFFSET  
        @nRowStart ROWS 
    FETCH NEXT  
        @nPageSize ROWS ONLY 
 
END 
 
GO
