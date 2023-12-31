DROP PROCEDURE IF EXISTS [dbo].[sp_GetWizardModelFields]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetWizardModelFields]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetWizardModelFields] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Jason L Walker
-- Create date: 08/01/2022 12:22 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve a list of the Model Fields that will be passed by the API during wizard interactions.
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetWizardModelFields] 
	@groupkey varchar(50)
    ,@groupfilter varchar(50) = null
AS 
BEGIN 
    

    SELECT DISTINCT u.[value] FROM (
        -- Retrieve Fields specified directly in the wizard.
        SELECT
            jData.value
        FROM  
            [LocalizedContentFields] jf
        CROSS APPLY
            OPENJSON(IIF(ISJSON(FieldData)>0, FieldData, '{}'), '$.ModelFields') jData
        WHERE 
            GroupKey=@groupKey
            AND
            FieldType = 'WIZARD'
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                jf.[GroupKey] LIKE @groupfilter
            )
        -- UNION result set together with fields from database
        UNION
        -- Retrieve form fields from database
        SELECT 
            f.FieldKey as [value] FROM [dbo].[LocalizedContentFields] f
        WHERE
            GroupKey=@groupKey
            AND
            FieldType IN ('INPUT', 'SELECT', 'TEXTAREA')
            AND 
            (
                @groupfilter IS NULL
                OR
                @groupfilter = ''
                OR
                f.[GroupKey] LIKE @groupfilter
            )
            AND f.FieldKey NOT IN (
                SELECT
                    jData.value
                FROM  
                    [LocalizedContentFields] jf
                CROSS APPLY
                    OPENJSON(IIF(ISJSON(FieldData)>0, FieldData, '{}'), '$.ExcludeModelFields') jData
                WHERE 
                    GroupKey=@groupKey
                    AND
                    FieldType = 'WIZARD'
                    AND 
                    (
                        @groupfilter IS NULL
                        OR
                        @groupfilter = ''
                        OR
                        jf.[GroupKey] LIKE @groupfilter
                    )
            )
    ) u
END 
 
GO
