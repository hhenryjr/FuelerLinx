ALTER PROCEDURE [dbo].[up_Select_AdminUsersByAndClientID]
	@ClientID int
AS

SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

select 
u.Id,
u.RegistrationID,
r.FirstName,
r.LastName,
r.Company,
r.Username,
r.[Password],
u.IsActive
from Users u
inner join Registration r on u.RegistrationID = r.ID
 where u.ClientID = @ClientID
 
 GO
