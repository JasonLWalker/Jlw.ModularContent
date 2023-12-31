DROP PROCEDURE IF EXISTS [dbo].[sp_GetFormFields]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetFormFields]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetFormFields] AS' 
END
GO


-- =============================================
-- Author:		Jason L Walker
-- Create date: 2020-10-01
-- Description:	Used to retrieve all matching form fields
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetFormFields] 
	-- Add the parameters for the stored procedure here
    @groupKey varchar(40),
    @language varchar(5) = null
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
        f.[GroupKey] = @groupKey
    ORDER BY
        f.[ParentKey], [Order]

END


GO
