SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
-- ============================================= 
-- Author: Jason L Walker
-- Create date: 07/06/2022 10:57 AM 
-- Description:	Used to recursively rename the matching records from [LocalizedContentFields] and [LocalizedContentText] by changing the [FieldKey] column
-- ============================================= 
 
CREATE PROCEDURE [dbo].[sp_RenameWizardFieldRecursive] 
    @id bigint
    ,@newFieldKey varchar(40) 
	,@baseType varchar(20) = null
	,@auditchangeby varchar(100) = ''  
    ,@groupfilter varchar(40) = null
    ,@recurseDepth int = 0
    ,@langFilter varchar(5) = null
AS 
BEGIN 

	SET NOCOUNT ON;

    -- Declare local variables to be used
    DECLARE @groupKey varchar(40);
    DECLARE @fieldKey varchar(40) = (SELECT TOP 1 FieldKey FROM [LocalizedContentFields] WHERE Id = @id);
    DECLARE @fieldType varchar(20);
    DECLARE @parentKey varchar(40);
    DECLARE @language varchar(5);
    DECLARE @nodeId bigint;
    DECLARE @depth int = @recurseDepth - 1;
    DECLARE @nodeCount int = 0;
    DECLARE @nodeMax int = 1000;
    DECLARE @langCount int = 0;
    DECLARE @langMax int = 100;
    -- Declare working table
    DECLARE @tNodes TABLE(
        [Id] bigint,
        [FieldKey] varchar(40),
        [FieldType] varchar(20),
        [ParentKey] varchar(40)
    );



    IF (@recurseDepth > 0 AND @id > 0 AND @fieldKey != @newFieldKey) 
    BEGIN
        -- Walk tree and retrieve relevant nodes into @tNodes
        INSERT INTO @tNodes 
        SELECT 
            Id, FieldKey, FieldType, ParentKey 
        FROM 
            [dbo].[fnGetWizardTreeNodes](@id, @baseType, @groupfilter, @recurseDepth)
        WHERE 
        (
            Id != @id
            AND
            ParentKey = @fieldKey
        )
        OR
        (
            Id = @id
            AND 
            FieldKey = @fieldKey
        );

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
            AND
            f.FieldKey = n.FieldKey
            AND
            f.ParentKey = n.ParentKey

        -- loop through nodes
        WHILE (@nodeId > 0 AND @nodeCount < @nodeMax)
        BEGIN        
            -- Increment counter in case of endless loop
            SET @nodeCount = @nodeCount + 1;

            -- Retrieve first language key to update
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

            SET @langCount = 0;


            -- Loop through each language record for root node
            WHILE (@language IS NOT NULL AND @langCount < @langMax)
            BEGIN
                SET @langCount = @langCount + 1;

                UPDATE 
                    LocalizedContentText 
                SET
                    FieldKey = IIF(@nodeId = @id, @newFieldKey, FieldKey)
                    ,ParentKey = IIF(@nodeId = @id, ParentKey, @newFieldKey)
                    ,AuditChangeBy = @auditchangeby
                    ,AuditChangeDate = GETDATE()
                    ,AuditChangeType = 'RENAME FROM ' + @fieldKey
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

                EXEC sp_AuditTrailSave_LocalizedContentText @groupkey, @fieldkey=@newFieldKey, @language=@language, @parentkey=@parentKey; 

                SET @language = NULL;

                -- Retrieve next language key to rename
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
            

            
            -- IF Language filter is set, then only the language need to be renamed, so skip this section, otherwise rename node
            IF (@langFilter IS NULL OR @langFilter = '')
            BEGIN
                UPDATE 
                    [LocalizedContentFields] 
                SET  
                    FieldKey = IIF(@nodeId = @id, @newFieldKey, FieldKey),
                    ParentKey = IIF(@nodeId = @id, ParentKey, @newFieldKey),
                    [AuditChangeType] = 'RENAME FROM ' + @fieldKey, 
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
                AND
                f.FieldKey = n.FieldKey
                AND
                f.ParentKey = n.ParentKey
        END        
    END

    SELECT TOP 1
        *
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
