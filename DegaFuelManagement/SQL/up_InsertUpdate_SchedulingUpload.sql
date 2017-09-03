GO
/****** Object:  StoredProcedure [dbo].[up_InsertUpdate_SchedulingUpload]    Script Date: 3/31/2017 11:35:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_InsertUpdate_SchedulingUpload]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_InsertUpdate_SchedulingUpload]
-- Date Generated: Thursday, May 26, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertUpdate_SchedulingUpload]
	@Name nvarchar(50),
	@CustClientID int,
	@AdminClientID int
AS

SET NOCOUNT ON

IF EXISTS(SELECT [Name] FROM [dbo].[SchedulingUploads] WHERE [Name] = @Name AND CustclientID = @CustClientID AND AdminClientID = @AdminClientID)
BEGIN
	UPDATE [dbo].[SchedulingUploads] SET
		Name = @Name,
		CustclientID= @CustClientID,
		DateUploaded = getdate()
	WHERE
		Name = @Name and CustClientID = @CustClientID and AdminClientID = @AdminClientID
END
ELSE
BEGIN
	INSERT INTO [dbo].[SchedulingUploads] (
		[Name],
		CustClientID,
		AdminClientID,
		DateUploaded
	) VALUES (
		@Name,
		@CustClientID,
		@AdminClientID,
		GETDATE()
	)
END




--endregion

