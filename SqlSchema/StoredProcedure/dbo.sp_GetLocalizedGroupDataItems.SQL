DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedGroupDataItems]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedGroupDataItems]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedGroupDataItems] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/23/2020 01:38 PM 
-- Changes:
--      08/07/2023 - jlwalker - Increased length of GroupKey
-- Description:	Used to retrieve the matching records from [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedGroupDataItems] 
	@groupKey varchar(50),
	@lang varchar(5) = null
AS 
BEGIN 
	SELECT 
		[Id]
		,[Language]
		,[GroupKey]
		,[Key]
		,[Value]
		,[Order]
		,[Description]
		,[Data]
	FROM  
		[LocalizedGroupDataItems] 
	WHERE 
		(
			@lang IS NULL
			OR
			[Language]=@lang
		)
		AND
		[GroupKey] = @groupKey
 
END 
 
GO
