GO
/****** Object:  StoredProcedure [dbo].[up_CheckUsername]    Script Date: 5/16/2017 9:17:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_CheckUsername]
(
@Id int = null,
@Username nvarchar(50),
@AdminClientID int = 0
)
as
begin

IF EXISTS(
	SELECT R.Username
	FROM Registration R
	INNER JOIN Users U ON R.Id = U.RegistrationID
	INNER JOIN Clients C ON C.Id = U.ClientID
	INNER JOIN CustomerDetails CD ON CD.CustClientID = C.Id AND CD.AdminClientID = @AdminClientID
	WHERE R.Username = @Username AND R.ID <> @Id
	)
	BEGIN
		SELECT 1 AS IsDuplicateUsername
	END

--ELSE
--BEGIN
--SELECT 0 AS IsDuplicateUsername
--	FROM Registration R
--	INNER JOIN Users U ON R.Id = U.RegistrationID
--	INNER JOIN Clients C ON C.Id = U.ClientID
--	INNER JOIN CustomerDetails CD ON CD.CustClientID = C.Id AND CD.AdminClientID = @AdminClientID
--	WHERE R.Id = @Id and R.Username = @Username
--END
end