GO
/****** Object:  StoredProcedure [dbo].[up_InsertUpdate_CustomerDetail]    Script Date: 6/16/2017 3:44:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--region [dbo].[up_InsertUpdate_CustomerDetail]

------------------------------------------------------------------------------------------------------------------------
-- Generated By:   Marty using CodeSmith 6.0.0.0
-- Template:       StoredProcedures.cst
-- Procedure Name: [dbo].[up_InsertUpdate_CustomerDetail]
-- Date Generated: Tuesday, June 7, 2016
------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertUpdate_CustomerDetail]
	@Id int,
	@AdminClientID int,
	@CustClientID int,
	@Name nvarchar(50),
	@Phone nvarchar(500) = null,
	@Email nvarchar(500) = null,
	@City nvarchar(50) = null,
	@Country nvarchar(50) = null,
	@BaseICAO nvarchar(4) = null,
	@IsActive bit = null,
	@Note nvarchar(max) = null,
	@ParentName nvarchar(50) = null,
	@CertificateType nvarchar(50) = null,
	@Address1 nvarchar(max) = null,
	@Address2 nvarchar(max) = null,
	@State nvarchar(2) = null,
	@ZipCode nvarchar(50) = null,
	@IsDemoMode bit = false
AS

SET NOCOUNT ON

IF EXISTS(SELECT [Id] FROM [dbo].[CustomerDetails] WHERE [Id] = @Id)
BEGIN
	UPDATE [dbo].[CustomerDetails] SET
		[AdminClientID] = @AdminClientID,
		[CustClientID] = @CustClientID,
		[Name] = @Name,
		[Phone] = @Phone,
		[Email] = @Email,
		[City] = @City,
		[Country] = @Country,
		[BaseICAO] = @BaseICAO, 
		[IsActive] = @IsActive,
		[Note] = @Note,
		[ParentName] = @ParentName,
		[CertificateType] = @CertificateType,
		[Address1] = @Address1,
		[Address2] = @Address2,
		[State] = @State,
		[ZipCode] = @ZipCode,
		IsDemoMode = @IsDemoMode
	WHERE
		[Id] = @Id
END
ELSE
BEGIN
	INSERT INTO [dbo].[CustomerDetails] (
		[AdminClientID],
		[CustClientID],
		[Name],
		[Phone],
		[Email],
		[City],
		[Country],
		[BaseICAO],
		[IsActive],
		[Note],
		[ParentName],
		[CertificateType],
		[Address1],
		[Address2],
		[State],
		[ZipCode],
		IsDemoMode
	) VALUES (
		@AdminClientID,
		@CustClientID,
		@Name,
		@Phone,
		@Email,
		@City,
		@Country,
		@BaseICAO,
		@IsActive,
		@Note,
		@ParentName,
		@CertificateType,
		@Address1,
		@Address2,
		@State,
		@ZipCode,
		@IsDemoMode
	)
SET @Id = @@IDENTITY


insert into CustomerDetailVendorExclusions (CustClientID, VendorID)
select @CustClientID, v.Id
from Vendors v
where v.IsExcluded = 1 

END


SELECT @Id AS Id 

--endregion

