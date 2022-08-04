SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
-- ============================================= 
-- Author: Jason L Walker
-- Create date: 08/01/2022 12:22 PM 
-- Description:	Used to retrieve a list of the Model Fields that will be passed by the API during wizard interactions.
-- ============================================= 
 
CREATE procedure [dbo].[sp_GetWizardModelFields] 
	@groupkey varchar(40)
    ,@groupfilter varchar(40) = null
AS 
BEGIN 
	SELECT
        jData.value
	FROM  
		[LocalizedContentFields] f
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
            f.[GroupKey] LIKE @groupfilter
        )
END 
 
GO
