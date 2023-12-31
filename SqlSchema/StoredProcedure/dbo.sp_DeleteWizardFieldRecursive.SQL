DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteWizardFieldRecursive]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DeleteWizardFieldRecursive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_DeleteWizardFieldRecursive] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 01/06/2021 10:57 AM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to recursively remove the matching records from [LocalizedContentFields] and [LocalizedContentText] 
-- ============================================= 
 
ALTER PROCEDURE [dbo].[sp_DeleteWizardFieldRecursive] 
    @id bigint
	,@baseType varchar(20) = null
	,@auditchangeby varchar(100) = ''  
    ,@groupfilter varchar(50) = null
    ,@recurseDepth int = 0
    ,@langFilter varchar(5) = null
AS 
BEGIN 

	--SET NOCOUNT ON;

    -- Declare local variables to be used
    DECLARE @groupKey varchar(50);
    DECLARE @fieldKey varchar(100);
    DECLARE @fieldType varchar(20);
    DECLARE @parentKey varchar(100);
    DECLARE @language varchar(5);
    DECLARE @nodeId bigint;
    DECLARE @depth int = @recurseDepth - 1;
    DECLARE @childCount int = 0;
    DECLARE @childMax int = 1000;
    -- Declare working table
    DECLARE @tNodes TABLE([Id] bigint);

    IF (@recurseDepth > 0 AND @id > 0) 
    BEGIN
        -- Walk tree and retrieve relevant nodes into @tNodes
        INSERT INTO @tNodes SELECT Id FROM [dbo].[fnGetWizardTreeNodes](@id, @baseType, @groupfilter, @recurseDepth)

        -- Retrieve first node to process
        SELECT 
            TOP 1 
            @nodeId = f.Id
            ,@fieldType = f.FieldType
            ,@fieldKey = f.FieldKey
            ,@groupKey = f.GroupKey
            ,@parentKey = f.ParentKey
        FROM 
            LocalizedContentFields f
        INNER JOIN
            @tNodes n
        ON
            f.Id = n.Id

        -- loop through child elements
        WHILE (@nodeId > 0 AND @childCount < @childMax)
        BEGIN        
            -- Increment counter in case of endless loop
            SET @childCount = @childCount + 1;

            -- Retrieve first language key to delete
            SELECT TOP 1
                @language = [Language]
            FROM 
                LocalizedContentText
            WHERE
                GroupKey = @groupKey
                AND
                FieldKey = @fieldKey
                AND
                ParentKey = @parentKey
                AND
                ParentKey IS NOT NULL
                AND 
                (
                    @langFilter IS NULL
                    OR 
                    @langFilter = ''
                    OR 
                    [Language] = @langFilter
                )

            -- Loop through each language record for node
            WHILE (@language IS NOT NULL)
            BEGIN

                UPDATE 
                    LocalizedContentText 
                SET
                    AuditChangeBy = @auditchangeby
                    ,AuditChangeDate = GETDATE()
                    ,AuditChangeType = 'DELETE'
                WHERE
                    GroupKey = @groupKey
                    AND
                    FieldKey = @fieldKey
                    AND
                    [Language] = @language
                    AND
                    ParentKey = @parentKey
                    AND
                    ParentKey IS NOT NULL;

                EXEC sp_AuditTrailSave_LocalizedContentText @groupkey, @fieldkey, @language, @parentkey; 

                DELETE 
                FROM 
                    LocalizedContentText 
                WHERE
                    GroupKey = @groupKey
                    AND
                    FieldKey = @fieldKey
                    AND
                    [Language] = @language
                    AND
                    ParentKey = @parentKey
                    AND 
                    ParentKey IS NOT NULL;

                SET @language = NULL;

                -- Retrieve next language key to delete
                SELECT TOP 1
                    @language = [Language]
                FROM 
                    LocalizedContentText
                WHERE
                    GroupKey = @groupKey
                    AND
                    FieldKey = @fieldKey
                    AND
                    ParentKey = @parentKey
                    AND 
                    ParentKey IS NOT NULL
                    AND 
                    (
                        @langFilter IS NULL
                        OR 
                        @langFilter = ''
                        OR 
                        [Language] = @langFilter
                    )

                -- loop back to language.
            END;
            

            
            -- IF Language filter is set, then only the language need to be removed, so skip this section, otherwise delete node
            IF (@langFilter IS NULL OR @langFilter = '')
            BEGIN
                UPDATE 
                    [LocalizedContentFields] 
                SET  
                    [AuditChangeType] = 'DELETE', 
                    [AuditChangeBy] = @auditchangeby, 
                    [AuditChangeDate] = GETDATE() 
                WHERE 
                    Id=@nodeId
                    AND 
                    (
                        @groupfilter IS NULL
                        OR
                        @groupfilter = ''
                        OR
                        [GroupKey] LIKE @groupfilter
                    )
                ; 
            
                EXEC sp_AuditTrailSave_LocalizedContentField @id=@nodeId; 
            
                DELETE 
                FROM  
                    [LocalizedContentFields] 
                WHERE 
                    Id=@nodeId
                    AND 
                    (
                        @groupfilter IS NULL
                        OR
                        @groupfilter = ''
                        OR
                        [GroupKey] LIKE @groupfilter
                    )
                ; 
            END  

            -- force @nodeId to null
            SET @nodeId = null;

            -- Retrieve next node to process
            SELECT 
                TOP 1 
                @nodeId = f.Id
                ,@fieldType = f.FieldType
                ,@fieldKey = f.FieldKey
                ,@groupKey = f.GroupKey
                ,@parentKey = f.ParentKey
            FROM 
                LocalizedContentFields f
            INNER JOIN
                @tNodes n
            ON
                f.Id = n.Id
        END        
    END

    SELECT TOP 1
        Id
    FROM 
        [LocalizedContentFields] 
    WHERE 
        Id=@id
        AND 
        (
            @groupfilter IS NULL
            OR
            @groupfilter = ''
            OR
            [GroupKey] LIKE @groupfilter
        )
    ; 

END 
 
GO
