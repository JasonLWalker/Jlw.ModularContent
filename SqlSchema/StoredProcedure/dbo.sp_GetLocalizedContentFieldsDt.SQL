DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedContentFieldsDt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedContentFieldsDt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedContentFieldsDt] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/20/2020 08:51 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to generate a DataTable list of matching records from [LocalizedContentFields] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedContentFieldsDt] 
    @sSearch varchar(MAX) = null, 
    @nRowStart int = 0, 
    @nPageSize int = 10, 
    @sSortCol varchar(20) = null, 
    @sSortDir varchar(4) = 'ASC',
    @sFieldType varchar(20) = null,
    @sFieldKey varchar(100) = null,
    @sGroupKey varchar(50) = null,
    @sParentKey varchar(100) = null,
    @sLanguage varchar(40) = null,
    @sGroupFilter varchar(50) = null
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
 
	-- Retrieve count of total records without search filter. 
    DECLARE @jlwDtTotalCount int = ( 
    SELECT COUNT(*) FROM  
        LocalizedContentFields 
    ); 
 
    SELECT 
		[Id]
		,f.[GroupKey]
		,f.[FieldKey]
		,[FieldType]
		,[FieldData]
		,[FieldClass]
		,f.[ParentKey]
		,[DefaultLabel]
		,[WrapperClass]
		,[WrapperHtmlStart]
		,[WrapperHtmlEnd]
        ,t.[Text] as Label
        ,t.[Language]
		,f.[AuditChangeType]
		,f.[AuditChangeBy]
		,f.[AuditChangeDate]
		,[Order]
        ,(Select COUNT(*) FROM [LocalizedContentText] l WHERE l.GroupKey = f.GroupKey AND l.FieldKey = f.FieldKey) as [LocalizedCount]
        ,@jlwDtTotalCount AS jlwDtTotalCount 
        ,COUNT(*) OVER () AS jlwDtFilteredCount  
	FROM 
		[LocalizedContentFields] f
        LEFT JOIN
        [LocalizedContentText] t
        ON 
            t.GroupKey = f.GroupKey
            AND
            t.FieldKey = f.FieldKey
            AND
            t.ParentKey = f.ParentKey
    WHERE  
        (
            @sSearch IS NULL 
            OR 
            CONCAT(f.[GroupKey], ' ', f.[FieldKey], ' ', [FieldType], ' ', f.[ParentKey], ' ', [DefaultLabel]) LIKE @sSearch Collate SQL_Latin1_General_CP1_CI_AS 
            --		Id=@id
        )
        AND 
        (
            @sGroupKey IS NULL
            OR 
            @sGroupKey = ''
            OR 
            f.[GroupKey] = @sGroupKey
        )
        AND 
        (
            @sLanguage = '*'
            OR
            t.[Language] IS NULL
            OR
            t.[Language] = IIF(ISNULL(@sLanguage, '') = '', 'EN', @sLanguage)
        )
        AND 
        (
            @sFieldType IS NULL
            OR
            @sFieldType = ''
            OR
            [FieldType] = @sFieldType
        )
        /*
        AND
        {
            @sParentKey IS NOT NULL 
            OR
            @sParentKey <> ''
            OR
            @sGroupKey IS NOT NULL
            OR 
            @sGroupKey <> ''
        )*/
        AND 
        (
            @sFieldKey IS NULL
            OR
            @sFieldKey = ''
            OR
            f.[FieldKey] = @sFieldKey
        )
        AND 
        (
            @sParentKey IS NULL
            OR
            @sParentKey = ''
            OR
            f.[ParentKey] = @sParentKey
        )
        AND 
        (
            @sGroupFilter IS NULL
            OR
            @sGroupFilter = ''
            OR
            f.[GroupKey] LIKE @sGroupFilter
        )
    ORDER BY  
        CASE @sSortDir WHEN 'DESC' THEN  
            CASE @sSortCol  
                WHEN 'GroupKey' THEN f.[GroupKey]
            END  
        END DESC, 
        CASE @sSortDir WHEN 'ASC' THEN 
            CASE @sSortCol  
                WHEN 'GroupKey' THEN f.[GroupKey]  
            END 
        END ASC,
        [Order]
    OFFSET  
        IIF(@nPageSize < 1, 0, @nRowStart) ROWS 
    FETCH NEXT  
        IIF(@nPageSize < 1, 1000, @nPageSize) ROWS ONLY 
 
END 
 
GO
