--region [dbo].[up_InsertUpdate_Aircraft]
ALTER PROCEDURE [dbo].[up_InsertUpdate_AircraftExclusion]
	@Id int,
	@AdminClientID int,
	@CustClientID int = null,
	@ICAO nvarchar(4),
	@FBO nvarchar(max),
	@AircraftID int,
	@TailNumbers nvarchar(max),
	@Margin decimal(18,5),
	@IsExcluded bit
AS

SET NOCOUNT ON

IF EXISTS(SELECT [Id] FROM [dbo].[AircraftExclusions] WHERE [Id] = @Id)
BEGIN
	UPDATE [dbo].[AircraftExclusions] SET
		[AdminClientID] = @AdminClientID,
		[CustClientID] = @CustClientID,
		[ICAO] = @ICAO,
		[FBO] = @FBO,
		[AircraftID] = @AircraftID,
		TailNumbers = @TailNumbers,
		Margin = @Margin,
		IsExcluded = @IsExcluded
	WHERE
		[Id] = @Id
END
ELSE
BEGIN
	INSERT INTO [dbo].[AircraftExclusions] (
		[AdminClientID],
		[CustClientID],
		[ICAO],
		[FBO],
		[AircraftID],
		TailNumbers,
		Margin,
		IsExcluded
	) VALUES (
		@AdminClientID,
		@CustClientID,
		@ICAO,
		@FBO,
		@AircraftID,
		@TailNumbers,
		@Margin,
		@IsExcluded
	)
SET @Id = @@IDENTITY
END


SELECT @Id AS Id 

--endregion
