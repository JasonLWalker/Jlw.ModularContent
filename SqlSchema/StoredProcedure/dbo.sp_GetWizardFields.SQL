DROP PROCEDURE IF EXISTS [dbo].[sp_GetWizardFields]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetWizardFields]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetWizardFields] AS' 
END
GO


-- =============================================
-- Author:		Jason L Walker
-- Create date: 2020-10-01
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey, FieldKey and ParentKey
-- Description:	Used to retrieve a grouped list of wizard fields with localized text
ALTER PROCEDURE [dbo].[sp_GetWizardFields] 
	-- Add the parameters for the stored procedure here
    @groupKey varchar(50) = null,
	@parentKey varchar(100) = null,
    @language varchar(5) = null,
    @groupfilter varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
        f.*,
        ISNULL(l.[Text], ISNULL(d.[Text], DefaultLabel)) as Label
    FROM
        [LocalizedContentFields] f -- [FormFields] f
    LEFT JOIN
        [LocalizedContentText] l --[LocalizationResources] l
    ON
        f.GroupKey = l.GroupKey 
        AND
        f.FieldKey = l.FieldKey
		AND
		f.ParentKey = l.ParentKey
        AND
        l.[Language] = @language
    LEFT JOIN
        --[LocalizationResources] d
        [LocalizedContentText] d
    ON
        f.GroupKey = d.GroupKey 
        AND
        f.FieldKey = d.FieldKey
		AND
		f.ParentKey = d.ParentKey
        AND
        d.[Language] = 'EN'
    WHERE
	(
        @groupKey IS NULL
        OR
        @groupKey = ''
        OR
        f.[GroupKey] = @groupKey
    )
    AND 
    (
        @groupfilter IS NULL
        OR
        f.[GroupKey] LIKE @groupfilter
    )
    AND
    (
        @parentKey IS NULL
        OR
        @parentKey = ''
        OR
        (
            f.[ParentKey] = @parentKey
            OR
            f.[FieldKey] = @parentKey
        )
	)
    ORDER BY
        f.[GroupKey], f.[ParentKey], [Order]

END


GO
