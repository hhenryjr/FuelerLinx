create proc [dbo].[up_Select_RegistrationByEmailAndUsername]
@AdminClientID int,
@Username nvarchar(50),
@Email nvarchar(50)
as
select r.Username, r.Password, cc.Email
from users u
inner join Registration r on r.Id = u.RegistrationID
inner join Clients c on c.id = u.ClientID
inner join CustomerDetails cd on cd.CustClientID = c.Id
inner join Contacts cc on cc.CustClientID = c.Id and cc.ContactType = 'Primary'
where r.Username = @UserName
and cc.Email = @Email
and cc.AdminClientID = @AdminClientID 
