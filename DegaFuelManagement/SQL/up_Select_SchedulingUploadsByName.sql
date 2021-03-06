/****** Object:  StoredProcedure [dbo].[up_Select_SchedulingUploadsByName]    Script Date: 4/12/2017 2:15:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[up_Select_SchedulingUploadsByName] 
@Name nvarchar(50),
@CustClientID int,
@AdminClientID int
as

SELECT TOP 1 @Name AS Name, CustClientID, ImportDate AS DateUploaded, AdminClientID
FROM SchedulingImports SI
INNER JOIN SchedulingPlatforms SP ON SI.SchedulingPlatform = SP.ID AND LOWER(SP.[Platform]) = LOWER(@Name) AND SI.CustClientID = @CustClientID AND SI.AdminClientID = @AdminClientID
ORDER BY DateUploaded DESC



--SELECT [Name]
--      ,[CustclientID]
--      ,[DateUploaded]
--	  ,AdminClientID
--	  FROM [dbo].[SchedulingUploads] 

--where Name = @Name and CustClientID = @CustClientID and AdminClientID = @AdminClientID