GO
/****** Object:  StoredProcedure [dbo].[up_Update_FuelOrdersAdminStatus]    Script Date: 3/22/2017 10:32:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[up_Update_FuelOrdersAdminStatus]
(
@Id int,
@AdminStatus smallint
)
as
begin
	UPDATE FuelOrders
	SET AdminStatus = @AdminStatus
	WHERE Id = @Id
end