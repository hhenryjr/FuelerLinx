GO
/****** Object:  StoredProcedure [dbo].[up_Select_LoginByUsernameAndPassword]    Script Date: 5/24/2017 6:58:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[up_Select_LoginByUsernameAndPassword]
@Username nvarchar(50),
@Password nvarchar(50),
@Domain varchar(50) = ''

as
begin
/**/
select u.Id,
		u.RegistrationID,
	   r.Username,
	   r.[Password],
	   u.IsActive,
	    u.ClientID,
	    u.DateAdded
  
from users u
inner join Registration r on r.Id = u.RegistrationID
inner join clients c on u.clientid=c.Id and clienttype=1
inner join ApplicationDomain AD on @Domain = '' or AD.Domain = @Domain and c.Id = AD.AdminClientID
where r.Username = @Username 
and r.[Password] = @Password 

union

select u.Id,
		u.RegistrationID,
	   r.Username,
	   r.[Password],
	   u.IsActive,
	    u.ClientID,
	    u.DateAdded
  
from users u
inner join Registration r on r.Id = u.RegistrationID
inner join clients c on u.clientid=c.Id and clienttype=2
inner join ApplicationDomain AD on @Domain = '' or AD.Domain = @Domain
inner JOIN CustomerDetails CD ON CD.CustClientID = C.Id
where r.Username = @Username 
and r.[Password] = @Password 
and CD.AdminClientID = AD.AdminClientID

end