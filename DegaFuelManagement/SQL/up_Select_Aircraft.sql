GO
/****** Object:  StoredProcedure [dbo].[up_Select_Aircraft]    Script Date: 2/20/2017 2:47:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_Select_Aircraft]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_Select_Aircraft]
-- Date Generated: Thursday, May 26, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_Select_Aircraft]
	@Id int
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SELECT
	[Id],
		[MakeModelID],
		[AdminClientID],
		[CustClientID],
		[TailNumber],
		[AccountNumber],
		[CardNumber],
		[IsMarginEnabled],
		[Margin],
	[DateAdded]
FROM
	[dbo].[Aircrafts]
WHERE
	[Id] = @Id

--endregion

