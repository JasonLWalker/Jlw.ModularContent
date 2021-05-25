SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/20/2020 08:51 PM 
-- Description:	Used to generate a DataTable list of matching records from [LocalizedContentFields] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedContentFieldsDt] 
    @sSearch varchar(MAX) = null, 
    @nRowStart int = 0, 
    @nPageSize int = 10, 
    @sSortCol varchar(20) = null, 
    @sSortDir varchar(4) = 'ASC',
    @sFieldType varchar(20) = null,
    @sFieldKey varchar(40) = null,
    @sGroupKey varchar(40) = null,
    @sParentKey varchar(40) = null,
    @sGroupFilter varchar(40) = null
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
		,[GroupKey]
		,[FieldKey]
		,[FieldType]
		,[FieldData]
		,[FieldClass]
		,[ParentKey]
		,[DefaultLabel]
		,[WrapperClass]
		,[WrapperHtmlStart]
		,[WrapperHtmlEnd]
		,[AuditChangeType]
		,[AuditChangeBy]
		,[AuditChangeDate]
		,[Order]
        ,(Select COUNT(*) FROM [LocalizedContentText] l WHERE l.GroupKey = f.GroupKey AND l.FieldKey = f.FieldKey) as [LocalizedCount]
        ,@jlwDtTotalCount AS jlwDtTotalCount 
        ,COUNT(*) OVER () AS jlwDtFilteredCount  
	FROM 
		[LocalizedContentFields] f
    WHERE  
        (
            @sSearch IS NULL 
            OR 
            CONCAT([GroupKey], ' ', [FieldKey], ' ', [FieldType], ' ', [ParentKey], ' ', [DefaultLabel]) LIKE @sSearch Collate SQL_Latin1_General_CP1_CI_AS 
            --		Id=@id
        )
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
            @sFieldType IS NULL
            OR
            @sFieldType = ''
            OR
            [FieldType] = @sFieldType
            OR
            @sParentKey IS NOT NULL 
            OR
            @sParentKey <> ''
            OR
            @sGroupKey IS NOT NULL
            OR 
            @sGroupKey <> ''
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
            @sGroupFilter IS NULL
            OR
            @sGroupFilter = ''
            OR
            [GroupKey] LIKE @sGroupFilter
        )
    ORDER BY  
        CASE @sSortDir WHEN 'DESC' THEN  
            CASE @sSortCol  
                WHEN 'GroupKey' THEN [GroupKey]
            END  
        END DESC, 
        CASE @sSortDir WHEN 'ASC' THEN 
            CASE @sSortCol  
                WHEN 'GroupKey' THEN [GroupKey]  
            END 
        END ASC,
        [Order]
    OFFSET  
        IIF(@nPageSize < 1, 0m @nRowStart) ROWS 
    FETCH NEXT  
        IIF(@nPageSize < 1, 1000, @nPageSize) ROWS ONLY 
 
END 
 
GO
