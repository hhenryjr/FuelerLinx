GO
/****** Object:  StoredProcedure [dbo].[up_Select_ContactsAll]    Script Date: 09/15/2016 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_ContactsAll]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_ContactsAll]
-- Date Generated: Tuesday, June 7, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_ContactsAll]
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT	[Id],
	ContactType,
	[AdminClientID],
	[CustClientID],
	[Name],
	Title,
	[Phone],
	[Email],
	[DateAdded],
	Distribute
FROM
	[dbo].[Contacts]

--endregion

