GO
/****** Object:  StoredProcedure [dbo].[up_Select_SiteSettings_All]    Script Date: 6/23/2017 12:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[up_Select_ClientNameByDomain]
@Domain varchar(50)

as
select Name
from ApplicationDomain AD
inner join Clients C ON C.Id = AD.AdminClientID
where Domain = @Domain