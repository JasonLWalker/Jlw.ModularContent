DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedGroupDataItemValue]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedGroupDataItemValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedGroupDataItemValue] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/23/2020 01:38 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey and Key
-- Description:	Used to retrieve the first matching record from [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedGroupDataItemValue] 
    @groupKey [varchar](50),
    @key [varchar](100),
	@language [varchar](5) = 'EN'
AS 
BEGIN 
    SELECT TOP 1  
        [Value]
    FROM  
        [LocalizedGroupDataItems] 
    WHERE 
        [Language] = @language
        AND
        [GroupKey] = @groupKey
        AND
        [Key] = @key;
END 
 
GO
