SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ============================================= 
-- Author: Jason L Walker
-- Create date: 07/06/2022
-- Description:	Used to recursively find the matching records from [LocalizedContentFields] by walking the tree of nodes, starting with @id
-- ============================================= 
CREATE FUNCTION [dbo].[fnGetWizardTreeNodes](
    @id int                             -- The unique [Id] of the root node.
	,@baseType varchar(20) = null       -- The [FieldType] of the root node. Used to double-check that we are matching the correct record.
    ,@groupfilter varchar(40) = null    -- used to filter out groups that do not match, thereby forcing the system to stay within a set of matching Group Keys (optional)
    ,@recurseDepth int = 1              -- only retrieve base node, with no children by default
) RETURNS @tTreeNodes TABLE (
    Id int,
    FieldType varchar(20),
    FieldKey varchar(40),
    ParentKey varchar(40)
) AS
BEGIN

    -- Declare local variables to be used
    DECLARE @groupKey varchar(40);
    DECLARE @fieldKey varchar(40);
    DECLARE @fieldType varchar(20);
    DECLARE @parentKey varchar(40);
    DECLARE @childId bigint;
    DECLARE @childCount int = 0;
    DECLARE @childMax int = 1000;

    IF (@recurseDepth > 0 AND @id > 0) 
    BEGIN
        -- Initialize local variables for root element
        SELECT 
            TOP 1 
            @groupKey=GroupKey, 
            @fieldType=FieldType, 
            @fieldKey=FieldKey, 
            @parentKey=ParentKey 
        FROM 
            LocalizedContentFields 
        WHERE 
            Id = @id 
            AND
            (
                @baseType IS NULL
                OR
                @baseType = ''
                OR
                FieldType = @baseType
            )
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                [GroupKey] LIKE @groupfilter
            )

        INSERT INTO @tTreeNodes
        SELECT f.Id, f.FieldType, f.FieldKey, f.ParentKey 
        FROM 
            LocalizedContentFields f
        LEFT JOIN
            @tTreeNodes t
        ON 
            f.Id = t.Id 
        WHERE 
            f.Id = @id
            AND
            t.Id IS NULL
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                f.[GroupKey] LIKE @groupfilter
            )

        -- Retrieve first child element
        SELECT 
            TOP 1
            @childId = f.Id
            ,@fieldType = f.FieldType
        FROM 
            LocalizedContentFields f
        LEFT JOIN
            @tTreeNodes t
        ON 
            f.Id = t.Id 
        WHERE
            f.GroupKey = @groupKey
            AND
            f.ParentKey = @fieldKey
            AND
            f.ParentKey != ''
            AND 
            f.ParentKey IS NOT NULL
            AND
            t.Id IS NULL
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                f.[GroupKey] LIKE @groupfilter
            )

        -- loop through child elements
        WHILE (@childId > 0 AND @childCount < @childMax)
        BEGIN

            -- Recurse into child element
            INSERT INTO @tTreeNodes
                SELECT n.Id, n.FieldType, n.FieldKey, n.ParentKey FROM [dbo].fnGetWizardTreeNodes(@childId, @fieldType, @groupfilter, @recurseDepth - 1) n
            LEFT JOIN
                @tTreeNodes t
            ON 
                n.Id = t.Id 
            WHERE
                t.Id IS NULL
            
            -- increment child counter
            SET @childCount += 1;

            -- force @childId to null
            SET @childId = null;
            -- Retrieve next child element
            SELECT 
                TOP 1
                @childId = f.Id
                ,@fieldType = f.FieldType
            FROM 
                LocalizedContentFields f
            LEFT JOIN
                @tTreeNodes t
            ON 
                f.Id = t.Id 
            WHERE
                f.GroupKey = @groupKey
                AND
                f.ParentKey = @fieldKey
                AND
                f.ParentKey != ''
                AND 
                f.ParentKey IS NOT NULL
                AND
                t.Id IS NULL
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                f.[GroupKey] LIKE @groupfilter
            )
        END


    END

    RETURN;
END
GO
