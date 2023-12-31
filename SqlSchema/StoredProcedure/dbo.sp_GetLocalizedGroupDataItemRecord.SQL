DROP PROCEDURE IF EXISTS [dbo].[sp_GetLocalizedGroupDataItemRecord]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetLocalizedGroupDataItemRecord]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_GetLocalizedGroupDataItemRecord] AS' 
END
GO
 
 
-- ============================================= 
-- Author: Web App Builder (Generated) 
-- Create date: 12/23/2020 01:38 PM 
-- Description:	Used to retrieve the first matching record from [LocalizedGroupDataItems] 
-- ============================================= 
 
ALTER procedure [dbo].[sp_GetLocalizedGroupDataItemRecord] 
	@id bigint
AS 
BEGIN 
	SELECT TOP 1  
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
		Id=@id
 
END 
 
GO
