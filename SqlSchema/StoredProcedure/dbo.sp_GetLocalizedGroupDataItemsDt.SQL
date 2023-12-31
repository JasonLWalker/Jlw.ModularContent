DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedGroupDataItemsDt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedGroupDataItemsDt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedGroupDataItemsDt] AS' 
END
GO
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/20/2020 07:56 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey
-- Description:	Used to generate a DataTable list of matching records from [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedGroupDataItemsDt] 
    @sSearch varchar(MAX) = '%%', 
    @nRowStart int = 0, 
    @nPageSize int = 10, 
    @sSortCol varchar(20) = null, 
    @sSortDir varchar(4) = 'ASC' ,
    @sGroupKey varchar(50) = null,
    @sGroupFilter varchar(50) = null
AS 
BEGIN 
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
 
	-- Retrieve count of total records without search filter. 
    DECLARE @jlwDtTotalCount int = ( 
    SELECT COUNT(*) FROM  
        LocalizedGroupDataItems 
    ); 
 
    

    SELECT 
		[Id]
		,[Language]
		,[GroupKey]
		,[Key]
		,[Value]
		,[Order]
		,[Description]
		,[Data]
 
        ,@jlwDtTotalCount AS jlwDtTotalCount 
        ,COUNT(*) OVER () AS jlwDtFilteredCount  
	FROM 
		[LocalizedGroupDataItems] 
    WHERE  
        CONCAT(' ', ' ') LIKE @sSearch Collate SQL_Latin1_General_CP1_CI_AS 
        --		Id=@id
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
            @sGroupFilter IS NULL
            OR
            @sGroupFilter = ''
            OR
            [GroupKey] LIKE @sGroupFilter
        )
    ORDER BY 
        CASE @sSortDir WHEN 'DESC' THEN 
            CASE @sSortCol 
                WHEN 'Id' THEN Id 
            END 
        END DESC,
        CASE @sSortDir WHEN 'ASC' THEN
            CASE @sSortCol 
                WHEN 'Id' THEN Id
            END
        END ASC
    OFFSET  
        @nRowStart ROWS 
    FETCH NEXT  
        IIF (@nPageSize < 1, 1000, @nPageSize) ROWS ONLY 
 
END 
 
GO
