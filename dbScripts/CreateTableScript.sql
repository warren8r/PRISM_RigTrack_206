/****** Object:  Schema [RigTrack]    Script Date: 11/13/2016 1:19:35 PM ******/
CREATE SCHEMA [RigTrack]
GO
/****** Object:  StoredProcedure [dbo].[createProgram]    Script Date: 11/13/2016 1:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createProgram]
(
	@progName varchar(50),
	@typeName NCHAR(25)
)
AS
Declare @typeID int
Declare @procSuccess bit
Declare @progExists bit
EXEC validateType @typeName, @typeID OUTPUT, @procSuccess OUTPUT
EXEC validateProg @progName, @typeID, @progExists OUTPUT

IF(@procSuccess = 'true')
 BEGIN
  IF(@progExists = 'true')
   SELECT 'Program already exists.' as response_MSG
  ELSE
   INSERT INTO touPrograms (progTypeID, progName, isActive) VALUES (@typeID, @progName, 'true')
   SELECT 'Program was successfully created.' as response_MSG
 END
ELSE
 
 BEGIN
  SELECT 'Failure: Type Does Not Exist' as response_MSG
 END
GO
/****** Object:  StoredProcedure [dbo].[createProgramType]    Script Date: 11/13/2016 1:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createProgramType]
(
	@typeName NCHAR(25)
)
AS
Declare @typeID int
Declare @procSuccess bit
EXEC validateType @typeName, @typeID OUTPUT, @procSuccess OUTPUT

IF(@procSuccess = 'true')
 BEGIN
  SELECT 'Program Type already exists.' as response_MSG
 END
ELSE
 
 BEGIN
  INSERT INTO progType (typeName) VALUES (@typeName)
  SELECT 'Program Type was successfully created.' as response_MSG
 END
GO
/****** Object:  StoredProcedure [dbo].[toggleActive]    Script Date: 11/13/2016 1:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[toggleActive]
(
	@id int
)
AS
UPDATE eventCategory SET active = ~ active WHERE id=@id
RETURN

GO
/****** Object:  StoredProcedure [dbo].[toggleFuel]    Script Date: 11/13/2016 1:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[toggleFuel]
(
	@fuelID int
)
AS
UPDATE touFuelAdjust SET isActive = ~ isActive WHERE ID=@fuelID
RETURN

GO
/****** Object:  StoredProcedure [dbo].[toggleProgram]    Script Date: 11/13/2016 1:19:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[toggleProgram]
(
	@progID int
)
AS
UPDATE touPrograms SET isActive = ~ isActive WHERE ID=@progID
RETURN

GO
/****** Object:  StoredProcedure [dbo].[toggleSeason]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[toggleSeason]
(
	@seasonID int
)
AS
UPDATE touSeasons SET isActive = ~ isActive WHERE ID=@seasonID
RETURN

GO
/****** Object:  StoredProcedure [dbo].[toggleTax]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[toggleTax]
(
	@taxID int
)
AS
UPDATE touTaxAdjust SET isActive = ~ isActive WHERE ID=@taxID
RETURN

GO
/****** Object:  StoredProcedure [dbo].[validateProg]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[validateProg]
(
    @progName varchar(50),
    @progType int,
	@doesExist bit OUTPUT
)
AS
BEGIN
SELECT @doesExist = count(*) FROM touPrograms WHERE progName=@progName AND progTypeID=@progType
END
GO
/****** Object:  StoredProcedure [dbo].[validateType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[validateType]
(
    @typeName NCHAR(25),
	@typeID int OUTPUT,
	@qrySuccess bit OUTPUT
)
AS
BEGIN
SELECT @typeID = ID FROM progType WHERE typeName=@typeName

IF @typeID IS NOT NULL
 SET @qrySuccess = 'true'
ELSE
 SET @qrySuccess = 'false'
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_CloseCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_CloseCurve]
(
	@CurveID int = null
)
AS
BEGIN
	UPDATE [RigTrack].[tblCurve]
	SET [isActive] = 0
	WHERE [ID] = @CurveID
	SELECT @CurveID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_CloseJob]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_CloseJob]
(
	  @CurveGroupID int = null
	, @CloseDate datetime = null
	, @Comments nvarchar(500) = null
	, @isAttachment bit = null
)
AS
BEGIN

	UPDATE	[RigTrack].[tblCurveGroup]
	SET		[isActive] = 0
		,	[JobEndDate] = @CloseDate
		,	[LastModifyDate] = GetDate()
		,	[Comments] = @Comments
		,	[isAttachment] = @isAttachment
	WHERE	[ID] = @CurveGroupID
	SELECT	1
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_CurveGroupHasWellPlan]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_CurveGroupHasWellPlan]
	@CurveGroupID int

AS
BEGIN
	UPDATE [RigTrack].[tblCurveGroup]
	SET 
	[HasWellPlan] = 1 
	WHERE [ID] = @CurveGroupID


END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_DeleteCurveGroupAndCurves]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_DeleteCurveGroupAndCurves]
(
	@CurveGroupID int
)
AS
BEGIN

	DELETE FROM [RigTrack].[tblCurve]
	WHERE CurveGroupID = @CurveGroupID

	DELETE FROM [RigTrack].[tblCurveGroup]
	WHERE ID = @CurveGroupID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_DeleteSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_DeleteSurvey] 
(
	@SurveyID int 
)
AS

BEGIN
	delete 
	From [RigTrack].[tblSurvey]
	WHERE ID = @SurveyID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCompanies]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCompanies]
AS
BEGIN
	SELECT [ID], [Company]
	FROM [RigTrack].[tblCurveGroup]
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCompanies2]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCompanies2]
AS
BEGIN
	SELECT   [ID] AS [CompanyID] 
		   , [CompanyName]
	FROM     [RigTrack].[tblCompany]
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCompanyID_Names]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCompanyID_Names]
AS
BEGIN
	SELECT	[ID] AS [CompanyID]
			,[CompanyName] AS [CompanyName]
	FROM	[RigTrack].[tblCompany]
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCompanyID_NamesWithCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCompanyID_NamesWithCurveGroup]
AS
BEGIN
	SELECT	[ID] AS [CompanyID]
			,[CompanyName] AS [CompanyName]
	FROM	[RigTrack].[tblCompany]
	WHERE [ID] IN (SELECT [CompanyID] FROM [RigTrack].[tblCurveGroup])
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCountries]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Countries
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllCountries] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name], [countryCode]
	FROM		[RigTrack].[tlkpCountry]
	ORDER BY [Name] asc
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupID_Names]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupID_Names]
AS
BEGIN
	SELECT	[ID]
			,[CurveGroupName] as 'CurveGroupName'
	FROM	[RigTrack].[tblCurveGroup]
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupNames]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupNames]
AS
BEGIN
	SELECT [ID]
	, [CurveGroupName] as CurveGroupName
	FROM [RigTrack].[tblCurveGroup]
	WHERE [isActive] = 1
	ORDER BY [ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupNamesForSurveys]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupNamesForSurveys]
AS
BEGIN
	SELECT DISTINCT cg.ID
	               , cg.[CurveGroupName] as CurveGroupName
	FROM [RigTrack].[tblCurveGroup] cg
	inner JOIN [RigTrack].[tblSurvey] s
	ON s.CurveGroupID = cg.ID
	--WHERE cg.isActive = 1
	--AND s.isActive = 1

	order by cg.ID desc
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupNamesTargets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupNamesTargets]
AS
BEGIN
	SELECT Distinct cg.[ID]
	,cg.[CurveGroupName] as CurveGroupName
	FROM [RigTrack].[tblCurveGroup] cg

	LEFT OUTER JOIN [RigTrack].[tblTarget] t
	on t.CurveGroupID = cg.ID

	WHERE t.TargetShapeID != 0
	--and cg.isActive = 1
	--and t.isActive = 1
	order by cg.ID desc
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroups]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroups]
AS
BEGIN
	SELECT cg.[ID]
		  ,cg.[CurveGroupName] as 'CurveGroupName'
		  ,cg.[JobNumber]
		  ,cg.[JobLocation]
		  ,cp.CompanyName
		  ,cg.[LeaseWell]
		  ,cg.[RigName]
		  ,cg.[JobStartDate]
		  ,cg.[JobEndDate]
		  , (IIF(cg.[isActive] = 1, 'Open', 'Closed')) AS [Status]
	FROM [RigTrack].[tblCurveGroup] cg
	LEFT OUTER JOIN [RigTrack].[tblCompany] cp
		on cp.ID = cg.CompanyID
	
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupsForCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupsForCompany]
(
	@CompanyID int = 0 
)
as
BEGIN 
	SELECT DISTINCT cg.[ID]
	,cg.[CurveGroupName] as 'Name'
	FROM [RigTrack].[tblCurveGroup] cg

	WHERE cg.CompanyID = @CompanyID 

	ORDER BY cg.ID desc

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupsNotClosed]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupsNotClosed]
AS
BEGIN
	SELECT	[ID]
		  ,	[CurveGroupName]
		  ,	[JobNumber]
		  ,	[JobLocation]
		  ,	[Company]
		  ,	[LeaseWell]
		  ,	[RigName]
		  , [JobStartDate]
		  , [JobEndDate]
		  , (IIF([isActive] = 1, 'Open', 'Closed')) AS [Status]
		  , [Comments]
			  , (IIF([isAttachment] = 1, '<a href="#" onclick="openRadWindow('+CAST([ID] as nvarchar(10))+')">' + 'File Download' + '</a>', 'NA')) AS [IsAttachment]
	FROM	[RigTrack].[tblCurveGroup]
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveGroupsNotClosed_WithParm]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurveGroupsNotClosed_WithParm]
(
	@IsActive int = null
)
AS
BEGIN
	if(ISNULL(@IsActive, -1) = -1)
	BEGIN
		SELECT	cg.[ID]
			  ,	cg.[CurveGroupName]
			  ,	cg.[JobNumber]
			  ,	cg.[JobLocation]
			  ,cg.[CompanyID]
			  ,	cp.[CompanyName]
			  ,	cg.[LeaseWell]
			  ,	cg.[RigName]
			  , cg.[JobStartDate]
			  , cg.[JobEndDate]
			  , (IIF(cg.[isActive] = 1, 'Open', 'Closed')) AS [Status]
			  , cg.[Comments]
			  , (IIF(cg.[isAttachment] = 1, '<a href="#" onclick="openRadWindow('+CAST(cg.[ID] as nvarchar(10))+')">' + 'File Download' + '</a>', 'NA')) AS [IsAttachment]
		FROM	[RigTrack].[tblCurveGroup] cg
		LEFT OUTER JOIN [RigTrack].[tblCompany] cp
		ON cp.ID = cg.CompanyID 
		ORDER BY cg.[ID] DESC
	END
	ELSE
	BEGIN
		SELECT	cg.[ID]
			  ,	cg.[CurveGroupName]
			  ,	cg.[JobNumber]
			  ,	cg.[JobLocation]
			  ,cg.[CompanyID]
			  ,	cp.[CompanyName]
			  ,	cg.[LeaseWell]
			  ,	cg.[RigName]
			  , cg.[JobStartDate]
			  , cg.[JobEndDate]
			  , (IIF(cg.[isActive] = 1, 'Open', 'Closed')) AS [Status]
			  , cg.[Comments]
			  , (IIF(cg.[isAttachment] = 1, '<a href="#" onclick="openRadWindow('+CAST(cg.[ID] as nvarchar(10))+')">' + 'File Download' + '</a>', 'NA')) AS [IsAttachment]
		FROM	[RigTrack].[tblCurveGroup] cg
		LEFT OUTER JOIN [RigTrack].[tblCompany] cp
		on cp.ID = cg.CompanyID
		WHERE	cg.[isActive] = CAST(IIF ( @IsActive = 1, 1, 0 ) AS BIT)
		ORDER BY cg.[ID] DESC
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurvesForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllCurvesForCurveGroup]
	@CurveGroupID int
AS
BEGIN

	SELECT C.ID
		  ,C.CurveGroupID
		  ,C.Number
		  ,C.Name
		  ,ct.Name as 'CurveTypeName'
		  ,C.CurveTypeID
		  ,C.NorthOffset
		  ,C.EastOffset
		  ,C.VSDirection
		  ,C.RKBElevation
	FROM [RigTrack].[tblCurve] C
	INNER JOIN [RigTrack].[tlkpCurveType] ct
	ON c.CurveTypeID = ct.ID
	WHERE C.CurveGroupID = @CurveGroupID 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllCurveTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Curve Types
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllCurveTypes] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpCurveType]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllDogLegs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Measurement Units
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllDogLegs] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpDogLegSeverity]
END


GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllEWNSReferences]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Countries
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllEWNSReferences] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpEWNSReferences]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllGLMSLs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All GLMSLs
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllGLMSLs] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpGLMSL]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllInputOutputDirections]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Input/Output Directions
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllInputOutputDirections] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpInputOutputDirection]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllJobNumbers]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllJobNumbers]
AS
BEGIN
	SELECT [ID], [JobNumber]
	FROM [RigTrack].[tblCurveGroup]
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllLeaseWell]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllLeaseWell]
AS
BEGIN
	SELECT [ID], [LeaseWell]
	FROM [RigTrack].[tblCurveGroup]
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllLocations]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Locations
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllLocations] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpLocation]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllMeasurementUnits]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Measurement Units
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllMeasurementUnits] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpMeasurementUnits]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllMethodsOfCalculation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Methods of Calculation
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllMethodsOfCalculation] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpMethodOfCalculation]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllModeReports]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesse Dudley
-- Create date: 07/30/2016
-- Description:	Return All ModeReports Look Up
-- =============================================
Create PROCEDURE [RigTrack].[sp_GetAllModeReports] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpModeReport]
END


GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllRigNames]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllRigNames]
AS
BEGIN
	SELECT [ID], [RigName]
	FROM [RigTrack].[tblCurveGroup]
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllStates]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All States
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllStates] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpState]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllTargetShapes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAllTargetShapes]
@targetID int = null
AS
BEGIN
	SELECT [TargetShapeID]
	
	
	FROM [RigTrack].[tblTarget] t
	WHERE t.ID = @targetID
	and isActive = 1
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAllVerticalSectionRefs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return All Vertical Section Refs
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetAllVerticalSectionRefs] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[ID], [Name]
	FROM		[RigTrack].[tlkpVerticalSectionRef]
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetAttachments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetAttachments]
(
	@CurveGroupID int = null
)
AS
BEGIN
	SELECT	[ID], [Name], [Attachment]
	FROM	[RigTrack].[tlkpCurveGroupAttachments]
	WHERE	[CurveGroupID] = @CurveGroupID
	AND		[isActive] = 1
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompany]
AS
BEGIN
	SELECT
		c.ID
	   ,c.CompanyName
       ,c.CompanyAddress1
	   ,c.CompanyAddress2
	   ,c.CompanyContactFirstName
	   ,c.CompanyContactLastName
	   ,c.ContactPhone
	   ,c.ContactEmail
	   ,c.City
	   ,c.StateID
	   ,s.Name as StateName
	   ,c.CountryID
	   ,cn.Name as CountryName
	   ,c.Zip
	   ,c.isAttachment
	   ,c.CreateDate
	   ,c.isActive
	   , c.[CompanyName] as 'Name'
	FROM [RigTrack].[tblCompany] c
	LEFT OUTER JOIN [RigTrack].[tlkpState] s
	ON c.StateID = s.ID
	LEFT OUTER JOIN [RigTrack].[tlkpCountry] cn
	ON c.CountryID = cn.ID
	ORDER BY c.CreateDate DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompanyCurveGroupID_Names]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompanyCurveGroupID_Names]
(
	@CompanyID int = null
)
AS
BEGIN
	SELECT	cg.[ID]
			,cg.[CurveGroupName] as 'CurveGroupName'
	FROM	[RigTrack].[tblCurveGroup] cg INNER JOIN
			[RigTrack].[tblCompany] c ON cg.[CompanyID] = c.[ID]
	WHERE	cg.[CompanyID] = @CompanyID
	ORDER BY cg.[ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompanyCurveGroupID_NamesTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompanyCurveGroupID_NamesTarget]
(
	@CompanyID int = null
)
AS
BEGIN
	SELECT	cg.[ID]
			, cg.[CurveGroupName] as 'CurveGroupName'
	FROM	[RigTrack].[tblCurveGroup] cg INNER JOIN
			[RigTrack].[tblCompany] c ON cg.[CompanyID] = c.[ID]
			
	inner join [RigTrack].[tblTarget] t
	on t.CurveGroupID = cg.ID

	WHERE	cg.[CompanyID] = @CompanyID
	ORDER BY cg.[ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompanyID_CompanyName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompanyID_CompanyName]
(
	@CurveGroupID int = null
)
AS
BEGIN
	IF(ISNULL(@CurveGroupID, 0) = 0)
	BEGIN
		SELECT		c.[ID], c.[CompanyName]
		FROM		[RigTrack].[tblCompany] c
		INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[CompanyID] = c.[ID]	
	END
	ELSE
	BEGIN
		SELECT		distinct c.[ID], c.[CompanyName]
		FROM		[RigTrack].[tblCompany] c
		INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[CompanyID] = c.[ID]	
		WHERE		cg.[ID] = @CurveGroupID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompanyID_CompanyNameWithTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompanyID_CompanyNameWithTarget]
(
	@TargetID int = null
)
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT		c.[ID], c.[CompanyName]
		FROM		[RigTrack].[tblCompany] c
		INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[CompanyID] = c.[ID]
		INNER JOIN	[RigTrack].[tblTarget] t ON t.[CurveGroupID] = cg.[ID]
	END
	ELSE
	BEGIN
		SELECT		distinct c.[ID], c.[CompanyName]
		FROM		[RigTrack].[tblCompany] c
		INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[CompanyID] = c.[ID]
		INNER JOIN	[RigTrack].[tblTarget] t ON t.[CurveGroupID] = cg.[ID]
		WHERE		t.[ID] = @TargetID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCompanyWithJob]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCompanyWithJob] 
AS
BEGIN

	SELECT DISTINCT
	c.[CompanyName] as 'Name'
	,cg.[CompanyID] as 'ID' 

	FROM [RigTrack].[tblCompany] c
	LEFT OUTER JOIN [RigTrack].[tblCurveGroup] cg
		on cg.CompanyID = c.ID 
	WHERE cg.CompanyID IS NOT NULL 
	ORDER BY cg.[CompanyID] desc 


END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupFromID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupFromID]
	@CurveGroupID int = null
AS
BEGIN
	SELECT CG.[ID]
		  ,CG.[CurveGroupName]
		  ,CG.[JobNumber]
		  ,CG.[Company]
		  ,CG.[LeaseWell]
		  ,CG.[JobLocation]
		  ,CG.[RigName]
		  ,C.[ID] as CountryID
		  ,S.[ID] as StateID
		  ,CG.[Declination]
		  ,CG.[Grid]
		  ,CG.[RKB]
		  ,GL.[ID] as GLorMSLID
		  ,MC.[ID] as MethodOfCalculationID
		  ,MU.[ID] as MeasurementUnitsID
		  ,CG.[UnitsConvert]
		  ,CG.[OutputDirectionID]
		  ,CG.[InputDirectionID]
		  ,CG.[VerticalSectionReferenceID]
		  ,CG.[DogLegSeverityID]
		  ,CG.[EWOffset]
		  ,CG.[NSOffset]
		 -- ,CG.[WorkNumber]
		--  ,CG.[PlanNumber]
		--  ,CG.[MD]
		  --,CG.[Incl]
		 -- ,CG.[Azimuth]
		 -- ,CG.[TVD]
		--  ,CG.[NSCoord]
		--  ,CG.[EWCoord]
		--  ,CG.[VSection]
		 -- ,CG.[WRate]
		 -- ,CG.[BRate]
		--  ,CG.[DLS]
		--  ,CG.[TFO]
		--  ,CG.[Closure]
		--  ,CG.[LocationID]
		--  ,CG.[BitToSensor]
		--  ,CG.[LeastDistanceText]
		 -- ,CG.[AtHSide]
		--  ,CG.[TVDComp]
		--  ,CG.[ComparisonCurveText]
		--  ,CG.[At]
	--	  ,CG.[LeastDistanceOnOff]
	FROM [RigTrack].[tblCurveGroup] CG
	LEFT JOIN [RigTrack].[tlkpCountry] C
	ON C.ID = CG.CountryID
	LEFT JOIN [RigTrack].[tlkpState] S
	ON S.ID = CG.StateID
	LEFT JOIN [RigTrack].[tlkpGLMSL] GL
	ON GL.ID = CG.GLorMSLID
	LEFT JOIN [RigTrack].[tlkpMethodOfCalculation] MC
	ON MC.ID = CG.MethodOfCalculationID
	LEFT JOIN [RigTrack].[tlkpMeasurementUnits] MU
	ON MU.ID = CG.MeasurementUnitsID
	WHERE CG.[ID] = @CurveGroupID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupID_Names]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupID_Names]
(
	@CompanyID int = null
)
AS
BEGIN
	IF(ISNULL(@CompanyID,0) = 0)
	BEGIN
		SELECT	[ID]
				,ISNULL([CurveGroupName], '') as 'CurveGroupName'
		FROM	[RigTrack].[tblCurveGroup]
	END
	ELSE
	BEGIN
		SELECT	[ID]
				,ISNULL([CurveGroupName], '') as 'CurveGroupName'
		FROM	[RigTrack].[tblCurveGroup]
		WHERE	[CompanyID] = @CompanyID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupID_NamesWithTargetID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupID_NamesWithTargetID]
(
	@TargetID int = null
)
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT	[ID]
				,ISNULL([CurveGroupName], '') as 'CurveGroupName'
		FROM	[RigTrack].[tblCurveGroup]
	END
	ELSE
	BEGIN
		SELECT		distinct cg.[ID]
					, ISNULL(cg.[CurveGroupName], '') as 'CurveGroupName'
		FROM		[RigTrack].[tblCurveGroup] cg
		INNER JOIN	[RigTrack].[tblCurve] c ON c.[CurveGroupID] = cg.[ID]
		INNER JOIN	[RigTrack].[tblTarget] t ON t.[ID] = c.[TargetID]
		WHERE		t.[ID] = @TargetID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupInfo] 
	@CurveGroupID int = null
AS 
BEGIN
	SELECT
	CG.ID
	,CG.CurveGroupName
	,CG.JobNumber
	,MC.Name as 'MethodOfCalculation'
	,MU.Name as 'MeasurementUnits'
	,CG.UnitsConvert
	,_input.Name as 'Input'
	,_output.Name as 'Output'
	,dog.Name as 'Dogleg'
	,VS.Name as 'VerticalSectionRef'
	,CG.EWOffset
	,CG.NSOffset
	,CG.LeaseWell
	,CG.JobLocation
	,CG.RigName
	,coun.Name as 'Country'
	,stateTab.Name as 'State' 
	,c.CompanyName as 'CompanyName'
	
	
	

	FROM [RigTrack].[tblCurveGroup] CG
	LEFT JOIN [RigTrack].[tlkpMethodOfCalculation] MC
	ON MC.ID = CG.MethodOfCalculationID
	LEFT JOIN [RigTrack].[tlkpMeasurementUnits] MU
	ON MU.ID = CG.MeasurementUnitsID
	LEFT JOIN [RigTrack].[tlkpInputOutputDirection] _input
	ON _input.ID = CG.InputDirectionID
	LEFT JOIN [RigTrack].[tlkpInputOutputDirection] _output
	ON _output.ID = CG.OutputDirectionID
	LEFT JOIN [RigTrack].[tlkpDogLegSeverity] dog
	ON  dog.ID = CG.DogLegSeverityID
	LEFT JOIN [RigTrack].[tlkpVerticalSectionRef] VS
	ON VS.ID = CG.VerticalSectionReferenceID
	LEFT JOIN [RigTrack].[tlkpCountry] coun
		on coun.ID = CG.CountryID
	LEFT JOIN [RigTrack].[tlkpState] stateTab
		on stateTab.ID = CG.StateID
	LEFT JOIN [RigTrack].[tblCompany] c
	ON c.ID = CG.CompanyID

	WHERE CG.ID = @CurveGroupID


END


GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupMethodOfCalculation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [RigTrack].[sp_GetCurveGroupMethodOfCalculation]
	@CurveGroupID int
AS

BEGIN

	SELECT 
	MC.Name 

	FROM [RigTrack].[tblCurveGroup] CG

	LEFT JOIN [RigTrack].[tlkpMethodOfCalculation] MC
	ON MC.ID = CG.MethodOfCalculationID
	WHERE CG.ID = @CurveGroupID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupName]
(
	@CurveGroupID int = null
)
AS
BEGIN
	IF (@CurveGroupID IS NOT NULL)
	BEGIN
		SELECT [CurveGroupName]
		FROM [RigTrack].[tblCurveGroup]
		WHERE [ID] = @CurveGroupID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupsCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupsCompany] 
	@CurveGroupID int
AS 
BEGIN


	SELECT 
		cg.CompanyID 

	FROM [RigTrack].[tblCurveGroup] cg
	WHERE cg.ID = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupsForPlotByCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupsForPlotByCompany] 
(
	@CompanyID int = 0
)
AS
BEGIN 

	SELECT DISTINCT cg.[ID]
	, cg.[CurveGroupName] as CurveGroupName
	

	FROM [RigTrack].[tblCurveGroup] cg 

	LEFT OUTER JOIN [RigTrack].[tblTarget] t
	on t.CurveGroupID = cg.ID
	WHERE cg.CompanyID = @CompanyID
	AND t.TargetShapeID != 0

	ORDER BY cg.ID desc 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveGroupStatus]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveGroupStatus]
(
	@CurveGroupID int = null
)
AS
BEGIN
	IF(ISNULL(@CurveGroupID, 0) = 0)
	BEGIN
		SELECT	[ID], (IIF([isActive] = 1, 'Open', 'Closed')) AS [Status]
		FROM	[RigTrack].[tblCurveGroup]	
	END
	ELSE
	BEGIN
		SELECT	[ID], (IIF([isActive] = 1, 'Open', 'Closed')) AS [Status]
		FROM	[RigTrack].[tblCurveGroup]
		WHERE	[ID] = @CurveGroupID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveID_CurveNameForTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveID_CurveNameForTarget]
(
	@TargetID int 
)
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT C.ID
			  ,C.Name as 'CurveID_CurveName' 
		FROM [RigTrack].[tblCurve] c
		INNER JOIN [RigTrack].[tlkpCurveType] ct
		ON c.CurveTypeID = ct.ID
	END
	ELSE
	BEGIN
		SELECT C.ID
			  ,C.Name as 'CurveID_CurveName' 
		FROM [RigTrack].[tblCurve] c
		INNER JOIN [RigTrack].[tlkpCurveType] ct
		ON c.CurveTypeID = ct.ID
		WHERE c.TargetID = @TargetID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurveInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurveInfo]
(
	@CurveID int
)
AS
BEGIN
	SELECT
	c.ID 
	,c.Name
	,ct.Name as 'CurveType'
	,c.Number
	,c.NorthOffset 
	,c.EastOffset
	,c.VSDirection
	,c.RKBElevation
	,c.LocationID
	,c.BitToSensor
	,c.ListDistanceBool
	,c.ComparisonCurve
	,c.AtHSide
	,c.TVDComp
		

	FROM [RigTrack].[tblCurve] c
	LEFT JOIN [RigTrack].[tlkpCurveType] ct
		on ct.ID = c.CurveTypeID
	where c.ID = @CurveID

END


GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurvesForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurvesForCurveGroup]
	@CurveGroupID int
AS
BEGIN
	SELECT c.ID
		  ,c.Name as 'Name'
	FROM [RigTrack].[tblCurve] c
	WHERE c.CurveGroupID = @CurveGroupID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurvesForCurveGroupActive]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurvesForCurveGroupActive]
	@CurveGroupID int 
AS
BEGIN
	SELECT DISTINCT c.ID
		,c.Name as 'Name' 
	
	FROM [RigTrack].[tblCurve] c
	INNER JOIN [RigTrack].[tblSurvey] s
		on s.CurveID = c.ID
	WHERE c.CurveGroupID = @CurveGroupID
	
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurvesForPlot]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurvesForPlot]
	@CurveGroupID int = null
AS
BEGIN
	SELECT DISTINCT c.ID
	      ,c.Name as 'Name'
	FROM [RigTrack].[tblCurve] c
	INNER JOIN [RigTrack].[tblSurvey] s
	ON c.ID = s.CurveID
	WHERE s.CurveGroupID = @CurveGroupID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetCurvesForTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetCurvesForTarget]
	@TargetID int 
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT C.ID
			  ,C.CurveGroupID
			  ,C.Number
			  , C.Name as 'Name' 
			  ,C.Name as 'CurveName'
			  ,ct.Name as 'CurveTypeName'
			  ,C.CurveTypeID
			  ,C.NorthOffset
			  ,C.EastOffset
			  ,C.VSDirection
			  ,C.RKBElevation
			  ,C.Color
		FROM [RigTrack].[tblCurve] c
		INNER JOIN [RigTrack].[tlkpCurveType] ct
		ON c.CurveTypeID = ct.ID
		WHERE c.[isActive] = 1
	END
	ELSE
	BEGIN
		SELECT T.ID as 'TargetID'
			  ,T.Name as 'TargetName'
			  ,C.ID
			  ,C.CurveGroupID
			  ,C.Number
			  ,C.Name as 'Name' 
			  ,C.Name as 'CurveName'
			  ,ct.Name as 'CurveTypeName'
			  ,C.CurveTypeID
			  ,C.NorthOffset
			  ,C.EastOffset
			  ,C.VSDirection
			  ,C.RKBElevation
			  ,C.Color
		FROM [RigTrack].[tblCurve] c
		INNER JOIN [RigTrack].[tlkpCurveType] ct
		ON c.CurveTypeID = ct.ID
		INNER JOIN [RigTrack].[tblTarget] t
		ON c.TargetID = t.ID
		WHERE c.TargetID = @TargetID and c.[IsActive] = 1
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetGLorMSLforCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return State/Country
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetGLorMSLforCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		g.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpGLMSL] g 
		ON	cg.[GLorMSLID] = g.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetInputDirectionForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Input Direction
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetInputDirectionForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		i.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpInputOutputDirection] i
		ON cg.[InputDirectionID] = i.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetIsUnitsConvertForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return True/False for Units Convert for Curve Group
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetIsUnitsConvertForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		[UnitsConvert]
	FROM		[RigTrack].[tblCurveGroup]
	WHERE		[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetLocationForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Vertical Section Reference
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetLocationForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		l.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpLocation] l
		ON cg.[VerticalSectionReferenceID] = l.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetLocationName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/24/2016
-- Description:	Return Location Value give ID
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetLocationName] 
(
	@LocationID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	[Name]
	FROM	[RigTrack].[tlkpLocation]
	WHERE	[ID] = @LocationID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetManageJobsCurveGroups]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetManageJobsCurveGroups]
	--@CurveGroupID int
AS
BEGIN

	SELECT C.ID
		  ,C.CurveGroupName
		  ,C.Company
		  ,C.LeaseWell
		  ,C.JobLocation
		  ,C.RigName
		  ,C.NSOffset
		  ,C.JobNumber
		  ,C.Grid
		  ,C.RKB
		  --,C.CountryID
		  ,CO.Name as 'CountryName'
		  ,S.Name as 'StateName'

		  ,Method.Name as 'MethodName'
		  ,C.UnitsConvert
		  ,MetersFeet.Name 'MetersFeet'
		  ,Dogleg.Name as 'DogLegName'
		  ,Gl.Name as 'GLName'
		  ,C.Declination
		  ,ODirection.Name as 'OutPutName'
		  ,IDirection.Name as 'InputName'
		  ,VSection.Name as 'VSectionName'
		  ,C.EWOffset

		 
	FROM [RigTrack].[tblCurveGroup] C

	LEFT OUTER JOIN [RigTrack].[tlkpCountry] CO
	on CO.ID = C.CountryID

	LEFT OUTER JOIN [RigTrack].[tlkpState] S
	on S.ID = C.StateID

	LEFT OUTER JOIN [RigTrack].[tlkpMethodOfCalculation] Method
	on Method.ID = C.MethodOfCalculationID

	LEFT OUTER JOIN [RigTrack].[tlkpMeasurementUnits] MetersFeet
	on MetersFeet.ID = C.MeasurementUnitsID

	LEFT OUTER JOIN [RigTrack].[tlkpDogLegSeverity] Dogleg
	on Dogleg.ID = C.DogLegSeverityID

	LEFT OUTER JOIN [RigTrack].[tlkpGLMSL] GL
	on GL.ID = C.GLorMSLID

	LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] ODirection
	on ODirection.ID = C.OutputDirectionID

		LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] IDirection
	on IDirection.ID = C.InputDirectionID

	LEFT OUTER JOIN [RigTrack].[tlkpVerticalSectionRef] VSection
	on VSection.ID = C.VerticalSectionReferenceID

	WHERE C.isActive = 1

	ORDER BY C.ID



	
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetManageJobsCurveGroupsFromID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetManageJobsCurveGroupsFromID]
	@CurveGroupID int
AS
BEGIN
SELECT C.ID
          ,C.JobStartDate
		  ,C.JobEndDate
		  ,C.CurveGroupName
		  ,Company.CompanyName
		  ,C.LeaseWell
		  ,C.JobLocation
		  ,C.RigName
		  ,C.NSOffset
		  ,C.JobNumber
		  ,C.Grid
		  ,C.RKB
		  --,C.CountryID
		  ,CO.Name as 'CountryName'
		  ,S.Name as 'StateName'

		  ,Method.Name as 'MethodName'
		
		  ,MetersFeet.Name 'MetersFeet'
		  ,Dogleg.Name as 'DogLegName'
		  ,Gl.Name as 'GLName'
		  ,C.Declination
		  ,ODirection.Name as 'OutPutName'
		  ,IDirection.Name as 'InputName'
		  ,VSection.Name as 'VSectionName'
		  ,C.EWOffset
		 

		 
	FROM [RigTrack].[tblCurveGroup] C

	LEFT OUTER JOIN [RigTrack].[tlkpCountry] CO
	on CO.ID = C.CountryID

	LEFT OUTER JOIN [RigTrack].[tlkpState] S
	on S.ID = C.StateID

	LEFT OUTER JOIN [RigTrack].[tlkpMethodOfCalculation] Method
	on Method.ID = C.MethodOfCalculationID

	LEFT OUTER JOIN [RigTrack].[tlkpMeasurementUnits] MetersFeet
	on MetersFeet.ID = C.MeasurementUnitsID

	LEFT OUTER JOIN [RigTrack].[tlkpDogLegSeverity] Dogleg
	on Dogleg.ID = C.DogLegSeverityID

	LEFT OUTER JOIN [RigTrack].[tlkpGLMSL] GL
	on GL.ID = C.GLorMSLID

	LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] ODirection
	on ODirection.ID = C.OutputDirectionID

		LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] IDirection
	on IDirection.ID = C.InputDirectionID

	LEFT OUTER JOIN [RigTrack].[tlkpVerticalSectionRef] VSection
	on VSection.ID = C.VerticalSectionReferenceID

	LEFT OUTER JOIN [RigTrack].[tblCompany] Company
	ON Company.ID = C.CompanyID
	WHERE C.ID = @CurveGroupID AND C.isActive = 1

	ORDER BY C.ID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetMeasurementUnitsForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Method of Calculation
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetMeasurementUnitsForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		mu.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpMeasurementUnits] mu
		ON cg.[MeasurementUnitsID] = mu.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetMethodOfCalculationForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Method of Calculation
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetMethodOfCalculationForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		m.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpMethodOfCalculation] m 
		ON cg.[MethodOfCalculationID] = m.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetOutputDirectionForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Output Direction
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetOutputDirectionForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		o.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpInputOutputDirection] o
		ON cg.[OutputDirectionID] = o.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetPlotComments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_GetPlotComments]
(
	@CurveGroupID int 
)
AS
BEGIN
	SELECT 
	[PlotComments]
	FROM [RigTrack].[tblCurveGroup]
	WHERE ID = @CurveGroupID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetReport]
(
		@ReportID int = null
)
AS
BEGIN
	DECLARE @SQLQueryString1 nvarchar(1000)
	DECLARE @SQLQueryString2 nvarchar(1000)
	DECLARE @SQLQueryString3 nvarchar(1000)
	DECLARE @SQLQueryString4 nvarchar(1000)
	DECLARE @SQLQueryString5 nvarchar(1000)
	DECLARE @Combo nvarchar(1000)
	SET @SQLQueryString4 = 'SELECT '
	SET @SQLQueryString1 = IIF((SELECT [MeasuredDepth] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[MD] ', '') + IIF((SELECT [Inclination] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[INC] ', '') + IIF((SELECT [Azimuth] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[Azimuth] ', '') + IIF((SELECT [TrueVerticalDepth] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[TVD] ', '') + IIF((SELECT [N_SCoordinates] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[NS] ', '') + IIF((SELECT [E_WCoordinates] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[EW] ', '') + IIF((SELECT [VerticalSection] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[VerticalSection] ', '') + IIF((SELECT [ClosureDistance] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[ClosureDistance] ', '') + IIF((SELECT [ClosureDirection] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[ClosureDirection] ', '') + IIF((SELECT [DogLegSeverity] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[DLS] ', '') + IIF((SELECT [CourseLength] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[CL] ', '') + IIF((SELECT [WalkRate] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[WR] ', '') + IIF((SELECT [BuildRate] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[BR] ', '') + IIF((SELECT [ToolFace] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[TFO] ', '') + IIF((SELECT [Comment] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[SurveyComment] ', '') + IIF((SELECT [SubseaDepth] FROM [RigTrack].[tblReport] WHERE [ID] = @ReportID) = 1, 's.[SubseaTVD] ', '') 
	SET @SQLQueryString2 = ' FROM		[RigTrack].[tblSurvey] s
							INNER JOIN	[RigTrack].[tblCurve] c ON c.[ID] = s.[CurveID]
							INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
							INNER JOIN	[RigTrack].[tblReport] r ON r.[CurveGroupID] = cg.[ID]
							WHERE		r.[ID] = ' + CAST(@ReportID as nvarchar(10))
	SET @SQLQueryString5 = LTRIM(RTRIM(@SQLQueryString1))
	SET @SQLQueryString3 = REPLACE(@SQLQueryString5, ' ', ',')
	SET @Combo = @SQLQueryString4 + @SQLQueryString3 + @SQLQueryString2
	BEGIN TRY
		exec sp_executesql @Combo, N''
	END TRY
	BEGIN CATCH
		SELECT 0
	END CATCH
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetReport_backup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetReport_backup]
(
		@ReportID int = null
	,	@MeasuredDepth bit = false
	,	@Inclination bit = false
	,	@Azimuth bit = false
	,	@TrueVerticalDepth bit = false
	,	@N_SCoordinates bit = false
	,	@E_WCoordinates bit = false
	,	@VerticalSection bit = false
	,	@ClosureDistance bit = false
	,	@ClosureDirection bit = false
	,	@DogLegSeverity bit = false
	,	@CourseLength bit = false
	,	@WalkRate bit = false
	,	@BuildRate bit = false
	,	@ToolFace bit = false
	,	@Comment bit = false
	,	@SubseaDepth bit = false
)
AS
BEGIN
	DECLARE @SQLQueryString1 nvarchar(1000)
	DECLARE @SQLQueryString2 nvarchar(1000)
	DECLARE @SQLQueryString3 nvarchar(1000)
	DECLARE @SQLQueryString4 nvarchar(1000)
	DECLARE @SQLQueryString5 nvarchar(1000)
	DECLARE @Combo nvarchar(1000)
	SET @SQLQueryString4 = 'SELECT '
	SET @SQLQueryString1 = IIF(@MeasuredDepth = 1, 's.[MD] ', '') + IIF(@Inclination = 1, 's.[INC] ', '') + IIF(@Azimuth = 1, 's.[Azimuth] ', '') + IIF(@TrueVerticalDepth = 1, 's.[TVD] ', '') + IIF(@N_SCoordinates = 1, 's.[NS] ', '') + IIF(@E_WCoordinates = 1, 's.[EW] ', '') + IIF(@VerticalSection = 1, 's.[VerticalSection] ', '') + IIF(@ClosureDistance = 1, 's.[ClosureDistance] ', '') + IIF(@ClosureDirection = 1, 's.[ClosureDirection] ', '') + IIF(@DogLegSeverity = 1, 's.[DLS] ', '') + IIF(@CourseLength = 1, 's.[CL] ', '') + IIF(@WalkRate = 1, 's.[WR] ', '') + IIF(@BuildRate = 1, 's.[BR] ', '') + IIF(@ToolFace = 1, 's.[TFO] ', '') + IIF(@Comment = 1, 's.[SurveyComment] ', '') + IIF(@SubseaDepth = 1, 's.[SubseaTVD] ', '') 
	SET @SQLQueryString2 = ' FROM		[RigTrack].[tblSurvey] s
							INNER JOIN	[RigTrack].[tblCurve] c ON c.[ID] = s.[CurveID]
							INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
							INNER JOIN	[RigTrack].[tblReport] r ON r.[CurveGroupID] = cg.[ID]
							WHERE		r.[ID] = ' + CAST(@ReportID as nvarchar(10))
	SET @SQLQueryString5 = LTRIM(RTRIM(@SQLQueryString1))
	SET @SQLQueryString3 = REPLACE(@SQLQueryString5, ' ', ',')
	SET @Combo = @SQLQueryString4 + @SQLQueryString3 + @SQLQueryString2
	BEGIN TRY
		exec sp_executesql @Combo, N''
	END TRY
	BEGIN CATCH
		SELECT 0
	END CATCH
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetReportFromID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetReportFromID]
	@ReportID int = 0
AS
BEGIN
	IF(ISNULL(@ReportID, 0) = 0)
	BEGIN
		SELECT TOP (1)	  at.[Name]
						, at.[Type]
						, at.[Attachment]
						, cg.[JobNumber]
						, co.[CompanyName]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, s.[Name] + '/' + c.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, '' AS [CurveName]
						, r.[HeaderComments]
						, r.[ExtraHeaderComments]
		FROM			  [RigTrack].[tblReport] r
		LEFT OUTER JOIN		  [RigTrack].[tblCurveGroup] cg ON cg.[ID] = r.[CurveGroupID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] c ON c.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpState] s ON s.[ID] = cg.[StateID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCompanyAttachments] at ON at.[CompanyID] = r.[CompanyID]
		LEFT OUTER JOIN		  [RigTrack].[tblCompany] co ON co.[ID] = r.[CompanyID]
		ORDER BY		  at.CreateDate desc
	END
	ELSE
	BEGIN
		SELECT TOP (1)	  at.[Name]
						, at.[Type]
						, at.[Attachment]
						, cg.[JobNumber]
						, co.[CompanyName]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, s.[Name] + '/' + c.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, '' AS [CurveName]
						, r.[HeaderComments]
						, r.[ExtraHeaderComments]
		FROM			  [RigTrack].[tblReport] r
		LEFT OUTER JOIN		  [RigTrack].[tblCurveGroup] cg ON cg.[ID] = r.[CurveGroupID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] c ON c.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpState] s ON s.[ID] = cg.[StateID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCompanyAttachments] at ON at.[CompanyID] = r.[CompanyID]
		LEFT OUTER JOIN		  [RigTrack].[tblCompany] co ON co.[ID] = r.[CompanyID]
		WHERE			  r.[ID] = @ReportID
		ORDER BY		  at.CreateDate desc
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetReportFromID_backup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetReportFromID_backup]
	@ReportID int
AS
BEGIN
	SELECT TOP 1 at.Name
		  ,at.Type
		  ,at.Attachment
	FROM [RigTrack].[tlkpReportAttachments] at
	WHERE at.ReportID = @ReportID
	ORDER BY at.CreateDate desc
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetReportFromID_backup2]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetReportFromID_backup2]
	@ReportID int = 0
AS
BEGIN
	IF(ISNULL(@ReportID, 0) = 0)
	BEGIN
		SELECT TOP (1)	  at.[Name]
						, at.[Type]
						, at.[Attachment]
						, cg.[JobNumber]
						, cg.[Company]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, s.[Name] + '/' + c.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, '' AS [CurveName]
						, r.[HeaderComments]
						, r.[ExtraHeaderComments]
		FROM			  [RigTrack].[tblReport] r
		LEFT OUTER JOIN		  [RigTrack].[tblCurveGroup] cg ON cg.[ID] = r.[CurveGroupID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] c ON c.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpState] s ON s.[ID] = cg.[StateID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpReportAttachments] at ON at.[ReportID] = r.[ID]
		ORDER BY		  at.CreateDate desc
	END
	ELSE
	BEGIN
		SELECT TOP (1)	  at.[Name]
						, at.[Type]
						, at.[Attachment]
						, cg.[JobNumber]
						, cg.[Company]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, s.[Name] + '/' + c.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, '' AS [CurveName]
						, r.[HeaderComments]
						, r.[ExtraHeaderComments]
		FROM			  [RigTrack].[tblReport] r
		LEFT OUTER JOIN		  [RigTrack].[tblCurveGroup] cg ON cg.[ID] = r.[CurveGroupID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] c ON c.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpState] s ON s.[ID] = cg.[StateID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpReportAttachments] at ON at.[ReportID] = r.[ID]
		WHERE			  r.[ID] = @ReportID
		ORDER BY		  at.CreateDate desc
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetStateCountryForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return State/Country
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetStateCountryForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		s.[Name] + '/' + c.[Name] AS [StateCountry]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpState] s 
		ON cg.[StateID] = s.[ID]
	INNER JOIN	[RigTrack].[tlkpCountry] c 
		ON cg.[CountryID] = c.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveyFromCurveID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveyFromCurveID]
	@CurveID int = null
AS
BEGIN
	SELECT s.ID as SurveyID
	FROM [RigTrack].[tblSurvey] s
	WHERE s.CurveID = @CurveID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForCurve]
	@CurveID int
AS 

BEGIN

	SELECT 

	s.MD
	,s.INC as 'Inclination'
	,s.Azimuth
	,s.TVD
	,s.SubseaTVD as 'SubseasTVD'
	,s.NS
	,s.EW
	,s.VerticalSection
	,s.CL
	,s.ClosureDistance
	,s.ClosureDirection
	,s.DLS
	,s.DLA
	,s.BR
	,s.WR
	,s.TFO
	,s.RowNumber
	,s.ID
	,s.Name
	,s.SurveyComment
	,s.TieInSubseaTVD
	,s.TieInNS
	, s.TieInEW
	, s.TieInVerticalSection



	FROM [RigTrack].[tblSurvey] s 

	WHERE s.CurveID = @CurveID
	and s.isActive != 'false' 
	ORDER BY s.RowNumber 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForCurveGroup]
	@CurveGroupID int 
AS
BEGIN

	SELECT 
	s.ID
	,s.MD
	,s.INC as 'Inclination' 
	,s.Azimuth
	, s.TVD
	,s.NS
	,s.EW
	,s.VerticalSection
	,s.CL
	,s.ClosureDistance
	,s.ClosureDirection
	,s.DLS
	,s.DLA
	,s.BR
	,s.WR
	,s.TFO
	,s.SurveyComment
	,s.RowNumber
	,s.CurveID
	,c.Color

	FROM [RigTrack].[tblSurvey] s 
	INNER JOIN [RigTrack].[tblCurve] c
	ON c.ID = S.CurveID

	WHERE s.CurveGroupID = @CurveGroupID
	and s.isActive != 'false'
	--ORDER BY s.MD 
	ORDER BY [ID] ASC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForJobReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForJobReport]
	@CurveID int
AS 

BEGIN

	SELECT 

	s.MD
	,s.INC as 'Inclination'
	,s.Azimuth
	,s.TVD
	,s.SubseaTVD as 'SubseasTVD'
	,s.NS
	,s.EW
	,s.VerticalSection
	,s.CL
	,s.ClosureDistance
	,s.ClosureDirection
	,s.DLS
	,s.DLA
	,s.BR
	,s.WR
	,s.TFO
	



	FROM [RigTrack].[tblSurvey] s 

	WHERE s.CurveID = @CurveID
	
	ORDER BY s.RowNumber 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForPlot]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForPlot]
(
	@CurveGroupID int = null
)
AS
BEGIN
	SELECT s.[ID]
		  ,s.[TVD]
		  ,s.[VerticalSection]
		  ,s.[ClosureDistance]
		  ,s.[SurveyComment]
		  ,s.[NS]
		  ,s.[EW]
		  ,s.[RowNumber]
	FROM [RigTrack].[tblSurvey] s
	WHERE s.[CurveGroupID] = @CurveGroupID
	AND s.isActive != 'False'
	ORDER BY [ID] ASC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForPlotFromCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForPlotFromCurve]
	@CurveID int = null
AS
BEGIN
	SELECT s.[Id]
		,s.[CurveID]
		  ,s.[TVD]
		  ,s.[VerticalSection]
		  ,s.[ClosureDistance]
		  ,s.[SurveyComment]
		  ,s.[EW]
		  ,s.[NS]
		  ,s.[RowNumber]
		  ,c.[Name]
		  ,c.[Color]
	FROM [RigTrack].[tblSurvey] s
	LEFT OUTER JOIN [RigTrack].[tblCurve] c
	ON c.ID = s.CurveID
	WHERE s.[CurveID] = @CurveID
	AND s.isActive != 'False'
	ORDER BY [ID] ASC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetSurveysForTarget]
	@TargetID int = null
AS
BEGIN

	SELECT s.ID as 'SurveyID' 
	      ,s.MD
		  ,s.INC as 'Inclination'
		  ,s.Azimuth 
		  ,s.TVD
		  ,s.NS
		  ,s.EW
		  ,s.VerticalSection
		  ,s.CL
		  ,s.ClosureDistance
		  ,s.ClosureDirection
		  ,s.DLS
		  ,s.DLA
		  ,s.BR
		  ,s.WR
		  ,s.TFO
	FROM [RigTrack].[tblSurvey] s
	INNER JOIN [RigTrack].[tblCurveGroup] cg
	ON cg.ID = s.CurveGroupID
	INNER JOIN [RigTrack].[tblTarget] t
	ON t.CurveGroupID = cg.ID
	WHERE t.ID = @TargetID
	ORDER BY s.MD
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetSurveysForWellPlan]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_GetSurveysForWellPlan]
	@CurveGroupID int 
AS
BEGIN
	Select DISTINCT c.ID
	,c.TargetID
	,
	CG.CompanyID
	


	FROM [RigTrack].[tblSurvey] s
	INNER JOIN [RigTrack].[tblCurve] c 
		ON c.CurveGroupID = @CurveGroupID
	INNER JOIN [RigTrack].[tblCurveGroup] CG
		ON cg.ID = @CurveGroupID

	WHERE s.CurveGroupID = @CurveGroupID
	AND c.Number = 0 


END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetForCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetForCurve]
(
	@CurveID int 
)
AS
BEGIN


	SELECT
		t.ID
		,t.Name
		,ts.Name as 'Shape' 
		,t.TVD


	FROM [RigTrack].[tblTarget] t
	INNER JOIN [RigTrack].[tlkpTargetShape] ts
		ON ts.ID = t.TargetShapeID
	INNER JOIN [RigTrack].[tblCurve] c
		on t.CurveGroupID = c.CurveGroupID
	WHERE c.ID = @CurveID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetForSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetForSurvey] 
(
	@TargetID int 
)
AS
BEGIN


	SELECT 
		t.ID
		,t.Name
		,ts.Name as 'Shape'
		,t.TVD


	FROM [RigTrack].[tblTarget] t
	INNER JOIN [RigTrack].[tlkpTargetShape]ts
		on ts.ID = t.TargetShapeID
	WHERE t.ID = @TargetID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_Names]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_Names]
(
	@CompanyID int = null
)
AS
BEGIN
	IF(ISNULL(@CompanyID, 0) = 0)
	BEGIN
		SELECT	[ID]
				,ISNULL([Name], '') as 'TargetID_TargetName'
		FROM	[RigTrack].[tblTarget]
	END
	ELSE
	BEGIN
		SELECT	t.[ID]
				,ISNULL(t.[Name], '') as 'TargetID_TargetName'
		FROM	[RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
		INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
		WHERE	cg.[CompanyID] = @CompanyID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetName]
(
	@CurveGroupID int = null
)
AS
BEGIN
	IF(ISNULL(@CurveGroupID, 0) = 0)
	BEGIN
		SELECT	[ID], [Name], ISNULL([Name], '') AS [TargetID_TargetName]
		FROM	[RigTrack].[tblTarget]	
	END
	ELSE
	BEGIN
		SELECT	DISTINCT t.[ID], t.[Name],  ISNULL(t.[Name], '') AS [TargetID_TargetName]
		FROM	[RigTrack].[tblTarget] t INNER JOIN
				[RigTrack].[tblCurveGroup] cg on t.[CurveGroupID] = cg.[ID] INNER JOIN				
				[RigTrack].[tblCurve] c on c.[TargetID] = t.[ID]
		WHERE	cg.[ID] = @CurveGroupID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetName_CurveID_CurveName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetName_CurveID_CurveName]
(
	@CurveGroupID int = null
)
AS
BEGIN
	IF(ISNULL(@CurveGroupID, 0) = 0)
	BEGIN
		SELECT c.[ID] AS [ID], 'TargetID=' + (CAST(t.[ID] AS nvarchar(25))) + ' | TargetName=' + t.Name + ' | CurveID=' + (CAST(c.[ID] AS nvarchar(25))) + ' | CurveName=' + c.Name AS [TargetID_TargetName_CurveID_CurveName]
		FROM [RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]	
	END
	ELSE
	BEGIN
		SELECT c.[ID] AS [ID], 'TargetID=' + (CAST(t.[ID] AS nvarchar(25))) + ' | TargetName=' + t.Name + ' | CurveID=' + (CAST(c.[ID] AS nvarchar(25))) + ' | CurveName=' + c.Name AS [TargetID_TargetName_CurveID_CurveName]
		FROM [RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
		WHERE t.[CurveGroupID] = @CurveGroupID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetName_CurveName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetName_CurveName]
(
	@CurveID int = null
)
AS
BEGIN
	IF(ISNULL(@CurveID, 0) = 0)
	BEGIN
		SELECT NULL	
	END
	ELSE
	BEGIN
		SELECT t.[ID] AS [TargetID], t.[Name] AS [TargetName], c.Name AS [CurveName]
		FROM [RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
		WHERE c.[ID] = @CurveID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetNameWithCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetNameWithCompany]
(
	  @CurveGroupID int = null
	, @CompanyName nvarchar(50) = null
)
AS
BEGIN
	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where [ID] = @CurveGroupID
	end

	Declare @CompanyTable table
	(
		company nvarchar(50)
	)
	If(Isnull(@CompanyName,'--Select--') = '--Select--')
	begin
			insert into @CompanyTable
			(company)
			select [Company] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CompanyTable
			(company)
			select [Company] from [RigTrack].[tblCurveGroup] where [Company] = @CompanyName
	end

	SELECT		distinct t.[ID], t.[Name], (CAST(t.[ID] AS nvarchar(25)) + '_' + ISNULL(t.[Name], '')) AS [TargetID_TargetName]--, cg.[ID] as [CurveGroupID], c.[ID] AS [CurveID]
	FROM		[RigTrack].[tblTarget] t
	INNER JOIN	[RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
	INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
	WHERE		cg.[ID] IN (select curvegroupid FROM @CurveGroupIDTable)
	AND			cg.[Company] IN (select company FROM @CompanyTable)
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetNameWithCompanyID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetNameWithCompanyID]
(
	  @CurveGroupID int = null
	, @CompanyID int = null
)
AS
BEGIN
	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where [ID] = @CurveGroupID
	end

	Declare @CompanyIDTable table
	(
		companyid int
	)
	If(Isnull(@CompanyID,0) = 0)
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany]
	end
	else
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany] where [ID] = @CompanyID
	end

	SELECT		distinct t.[ID], t.[Name], ISNULL(t.[Name], '') AS [TargetID_TargetName]--, cg.[ID] as [CurveGroupID], c.[ID] AS [CurveID]
	FROM		[RigTrack].[tblTarget] t
	INNER JOIN	[RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
	INNER JOIN	[RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
	WHERE		cg.[ID] IN (select curvegroupid FROM @CurveGroupIDTable)
	AND			cg.[CompanyID] IN (select companyid FROM @CompanyIDTable)
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetID_TargetNameWithTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetID_TargetNameWithTarget]
(
	@TargetID int = null
)
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT	[ID], [Name], ISNULL([Name], '') AS [TargetID_TargetName]
		FROM	[RigTrack].[tblTarget]	
	END
	ELSE
	BEGIN
		SELECT	[ID], [Name], ISNULL([Name], '') AS [TargetID_TargetName]
		FROM	[RigTrack].[tblTarget]
		WHERE	[ID] = @TargetID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetInfoFromTargetID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetInfoFromTargetID]
(
	@TargetID int = null
)
AS
BEGIN
	SELECT T.TVD
		  ,T.EWCoordinate
		  ,T.NSCoordinate
		  ,T.PolarDirection
		  ,T.PolarDistance
	FROM [RigTrack].[tblTarget] t
	WHERE t.ID = @TargetID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetReportFromTargetID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetReportFromTargetID]
(
	@TargetID int = 0
)
AS
BEGIN
	IF(ISNULL(@TargetID, 0) = 0)
	BEGIN
		SELECT NULL
	END
	ELSE
	BEGIN
		SELECT TOP (1)		  at.[Name]
							, at.[Type]
							, at.[Attachment]
							, cg.[JobNumber]
							, cg.[Company]
							, cg.[LeaseWell]
							, cg.[JobLocation]
							, cg.[RigName]
							, cg.[RKB]
							, gl.[Name] AS [GLorMSL]
							, st.[Name] + '/' + co.[Name] AS [StateCountry]
							, cg.[Declination]
							, cg.[Grid]
							, cg.[CurveGroupName]
							, GetDate() AS [CurrentDateTime]
							, '' AS [CurveName]
							, r.[HeaderComments]
							, r.[ExtraHeaderComments]
		FROM				[RigTrack].[tblTarget] t
		INNER JOIN			[RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
		INNER JOIN			[RigTrack].[tblCurveGroup] cg ON cg.[ID] = c.[CurveGroupID]
		LEFT OUTER JOIN		[RigTrack].[tblReport] r ON r.[TargetID] = t.[ID]
		LEFT OUTER JOIN		[RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		[RigTrack].[tlkpCountry] co ON co.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		[RigTrack].[tlkpState] st ON st.[ID] = cg.[StateID]
		LEFT OUTER JOIN		[RigTrack].[tlkpReportAttachments] at ON at.[ReportID] = r.[ID]
		WHERE				t.[ID] = @TargetID
		ORDER BY			t.[CreateDate] desc
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForAllCurves]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [RigTrack].[sp_GetTargetsForAllCurves]
	@CurveID int = null
AS
BEGIN
	SELECT c.TargetID as 'TargetID'
		  , t.Name as 'TargetName'
	FROM [RigTrack].[tblCurve] c
	INNER JOIN [RigTrack].[tblTarget] t
	ON c.TargetID = t.ID
	WHERE c.ID = @CurveID
	and t.isActive = 1
	and t.TargetShapeID > 0 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsForCurveGroup]
	@CurveGroupID int = null
AS
BEGIN
	SELECT t.ID as 'TargetID'
		  , t.Name as 'TargetName'
	FROM [RigTrack].[tblTarget] t
	WHERE t.CurveGroupID = @CurveGroupID
	and isActive = 1
	and TargetShapeID > 0 
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForCurveGroupID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsForCurveGroupID]
	@CurveGroupID int 
AS
BEGIN
IF(@CurveGroupID > 0)
	BEGIN
		  Select t.[ID]
		  ,t.[Name] as TargetName
		  ,t.[CurveGroupID]
		  ,t.[TargetShapeID]
		  ,ts.[Name] as 'TargetShapeName'
		  ,t.[TVD]
		  ,t.[NSCoordinate]
		  ,t.[EWCoordinate]
		  ,t.[PolarDirection]
		  ,t.[PolarDistance]
		  ,t.[INCFromLastTarget]
		  ,t.[AZMFromLastTarget]
		  ,t.[InclinationAtTarget]
		  ,t.[AzimuthAtTarget]
		  ,t.[NumberVertices]
		  ,t.[Rotation]
		  ,t.[TargetThickness]
		  ,t.[DrawingPattern]
		  ,d.[Name] as 'DrawingPatternName'
		  ,t.[TargetComment]
		  ,t.[TargetOffsetXoffset]
		  ,t.[TargetOffsetYoffset]
		  ,t.[DiameterOfCircleXoffset]
		  ,t.[DiameterOfCircleYoffset]
		  ,t.[Corner1Xofffset]
		  ,t.[Corner1Yoffset]
		  ,t.[Corner2Xoffset]
		  ,t.[Corner2Yoffset]
		  ,t.[Corner3Xoffset]
		  ,t.[Corner3Yoffset]
		  ,t.[Corner4Xoffset]
		  ,t.[Corner4Yoffset]
		  ,t.[Corner5Xoffset]
		  ,t.[Corner5Yoffset]
		  ,t.[Corner6Xoffset]
		  ,t.[Corner6Yoffset]
		  ,t.[Corner7Xoffset]
		  ,t.[Corner7Yoffset]
		  ,t.[Corner8Xoffset]
		  ,t.[Corner8Yoffset]
		  ,t.[ReferenceOptionID]
		FROM [RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
		INNER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		WHERE CurveGroupID = @CurveGroupID
		ORDER BY t.[ID] ASC
	END
	ELSE
	BEGIN
			Select t.[ID]
			,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			,t.[TargetOffsetXoffset]
			,t.[TargetOffsetYoffset]
		  ,t.[DiameterOfCircleXoffset]
		  ,t.[DiameterOfCircleYoffset]
		  ,t.[Corner1Xofffset]
		  ,t.[Corner1Yoffset]
		  ,t.[Corner2Xoffset]
		  ,t.[Corner2Yoffset]
		  ,t.[Corner3Xoffset]
		  ,t.[Corner3Yoffset]
		  ,t.[Corner4Xoffset]
		  ,t.[Corner4Yoffset]
		  ,t.[Corner5Xoffset]
		  ,t.[Corner5Yoffset]
		  ,t.[Corner6Xoffset]
		  ,t.[Corner6Yoffset]
		  ,t.[Corner7Xoffset]
		  ,t.[Corner7Yoffset]
		  ,t.[Corner8Xoffset]
		  ,t.[Corner8Yoffset]
			  ,t.[ReferenceOptionID]
		  FROM [RigTrack].[tblTarget] t
		  INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
		  INNER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		  ORDER BY t.[ID] ASC
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForCurveGroupID2]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsForCurveGroupID2]
(
	@CurveGroupID int = 0 
	, @TargetID int = 0
)
AS
BEGIN
IF(@CurveGroupID > 0)
	BEGIN
		IF(ISNULL(@TargetID, 0) = 0)
		BEGIN
			  Select t.[ID]
			  ,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			  ,t.[TargetOffsetXoffset]
			  ,t.[TargetOffsetYoffset]
			  ,t.[DiameterOfCircleXoffset]
			  ,t.[DiameterOfCircleYoffset]
			  ,t.[Corner1Xofffset]
			  ,t.[Corner1Yoffset]
			  ,t.[Corner2Xoffset]
			  ,t.[Corner2Yoffset]
			  ,t.[Corner3Xoffset]
			  ,t.[Corner3Yoffset]
			  ,t.[Corner4Xoffset]
			  ,t.[Corner4Yoffset]
			  ,t.[ReferenceOptionID]
			  ,s.[MD]
			  ,s.[INC]
			  ,s.[Azimuth]
			  ,s.[BR]
			  ,s.[WR]
			  ,s.[TFO] AS [HoldLen]
			FROM [RigTrack].[tblTarget] t
			INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = t.[CurveGroupID]
			INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
			INNER JOIN [RigTrack].[tblSurvey] s ON s.[CurveID] = c.[ID]
			LEFT OUTER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
			LEFT OUTER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
			WHERE t.[CurveGroupID] = @CurveGroupID
			ORDER BY t.[ID] DESC
		END
		ELSE
		BEGIN
			  Select t.[ID]
			  ,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			  ,t.[TargetOffsetXoffset]
			  ,t.[TargetOffsetYoffset]
			  ,t.[DiameterOfCircleXoffset]
			  ,t.[DiameterOfCircleYoffset]
			  ,t.[Corner1Xofffset]
			  ,t.[Corner1Yoffset]
			  ,t.[Corner2Xoffset]
			  ,t.[Corner2Yoffset]
			  ,t.[Corner3Xoffset]
			  ,t.[Corner3Yoffset]
			  ,t.[Corner4Xoffset]
			  ,t.[Corner4Yoffset]
			  ,t.[ReferenceOptionID]
			  ,s.[MD]
			  ,s.[INC]
			  ,s.[Azimuth]
			  ,s.[BR]
			  ,s.[WR]
			  ,s.[TFO] AS [HoldLen]
			FROM [RigTrack].[tblTarget] t
			INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = t.[CurveGroupID]
			INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
			INNER JOIN [RigTrack].[tblSurvey] s ON s.[CurveID] = c.[ID]
			LEFT OUTER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
			LEFT OUTER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
			WHERE t.[ID] = @TargetID
			ORDER BY t.[ID] DESC
		END
	END
	ELSE
	BEGIN
			  Select t.[ID]
			  ,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			  ,t.[TargetOffsetXoffset]
			  ,t.[TargetOffsetYoffset]
			  ,t.[DiameterOfCircleXoffset]
			  ,t.[DiameterOfCircleYoffset]
			  ,t.[Corner1Xofffset]
			  ,t.[Corner1Yoffset]
			  ,t.[Corner2Xoffset]
			  ,t.[Corner2Yoffset]
			  ,t.[Corner3Xoffset]
			  ,t.[Corner3Yoffset]
			  ,t.[Corner4Xoffset]
			  ,t.[Corner4Yoffset]
			  ,t.[ReferenceOptionID]
			  ,s.[MD]
			  ,s.[INC]
			  ,s.[Azimuth]
			  ,s.[BR]
			  ,s.[WR]
			  ,s.[TFO] AS [HoldLen]
			FROM [RigTrack].[tblTarget] t
			INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = t.[CurveGroupID]
			INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
			INNER JOIN [RigTrack].[tblSurvey] s ON s.[CurveID] = c.[ID]
			LEFT OUTER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
			LEFT OUTER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		  ORDER BY t.[ID] DESC
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForCurveGroupID3]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsForCurveGroupID3]
	@CurveGroupID int 
AS
BEGIN
IF(@CurveGroupID > 0)
	BEGIN
			  Select t.[ID]
			  ,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			  ,t.[TargetOffsetXoffset]
			  ,t.[TargetOffsetYoffset]
			  ,t.[DiameterOfCircleXoffset]
			  ,t.[DiameterOfCircleYoffset]
			  ,t.[Corner1Xofffset]
			  ,t.[Corner1Yoffset]
			  ,t.[Corner2Xoffset]
			  ,t.[Corner2Yoffset]
			  ,t.[Corner3Xoffset]
			  ,t.[Corner3Yoffset]
			  ,t.[Corner4Xoffset]
			  ,t.[Corner4Yoffset]
			  ,t.[ReferenceOptionID]
			  ,s.[MD]
			  ,s.[INC]
			  ,s.[Azimuth]
			  ,s.[BR]
			  ,s.[WR]
			  ,s.[TFO] AS [HoldLen]
			FROM [RigTrack].[tblTarget] t
			INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = t.[CurveGroupID]
			INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
			INNER JOIN [RigTrack].[tblSurvey] s ON s.[CurveID] = c.[ID]
			LEFT OUTER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
			LEFT OUTER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		WHERE t.CurveGroupID = @CurveGroupID
		ORDER BY t.[ID] DESC
	END
	ELSE
	BEGIN
			  Select t.[ID]
			  ,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			  ,t.[TargetOffsetXoffset]
			  ,t.[TargetOffsetYoffset]
			  ,t.[DiameterOfCircleXoffset]
			  ,t.[DiameterOfCircleYoffset]
			  ,t.[Corner1Xofffset]
			  ,t.[Corner1Yoffset]
			  ,t.[Corner2Xoffset]
			  ,t.[Corner2Yoffset]
			  ,t.[Corner3Xoffset]
			  ,t.[Corner3Yoffset]
			  ,t.[Corner4Xoffset]
			  ,t.[Corner4Yoffset]
			  ,t.[ReferenceOptionID]
			  ,s.[MD]
			  ,s.[INC]
			  ,s.[Azimuth]
			  ,s.[BR]
			  ,s.[WR]
			  ,s.[TFO] AS [HoldLen]
			FROM [RigTrack].[tblTarget] t
			INNER JOIN [RigTrack].[tblCurveGroup] cg ON cg.[ID] = t.[CurveGroupID]
			INNER JOIN [RigTrack].[tblCurve] c ON c.[TargetID] = t.[ID]
			INNER JOIN [RigTrack].[tblSurvey] s ON s.[CurveID] = c.[ID]
			LEFT OUTER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
			LEFT OUTER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		  ORDER BY t.[ID] DESC
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsForCurveID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsForCurveID]
	@CurveID int
AS
BEGIN
	SELECT
		t.[ID]
	   ,t.[TargetShapeID]
	   ,t.[Name]
	   ,t.[EWCoordinate]
	   ,t.[NSCoordinate]
	   ,t.[NumberVertices]
	   ,t.[TVD]
	   ,t.[TargetOffsetXoffset]
		,t.[TargetOffsetYoffset]
		,t.[DiameterOfCircleXoffset]
		,t.[DiameterOfCircleYoffset]
		,t.[Rotation]
		 ,t.[NumberVertices]
		  ,t.[Corner1Xofffset]
		  ,t.[Corner1Yoffset]
		  ,t.[Corner2Xoffset]
		  ,t.[Corner2Yoffset]
		  ,t.[Corner3Xoffset]
		  ,t.[Corner3Yoffset]
		  ,t.[Corner4Xoffset]
		  ,t.[Corner4Yoffset]
		  ,t.[Corner5Xoffset]
		  ,t.[Corner5Yoffset]
		  ,t.[Corner6Xoffset]
		  ,t.[Corner6Yoffset]
		  ,t.[Corner7Xoffset]
		  ,t.[Corner7Yoffset]
		  ,t.[Corner8Xoffset]
		  ,t.[Corner8Yoffset]
	FROM [RigTrack].[tblTarget] t
	INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
	INNER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
	INNER JOIN [RigTrack].[tblCurve] c on c.TargetID = t.ID
	WHERE c.ID = @CurveID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetShapeDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetShapeDetails]
(
	@TargetID int = null
)
AS
BEGIN
	SELECT t.TargetOffsetXoffset
	      ,t.TargetOffsetYoffset
		  ,t.DiameterOfCircleXoffset
		  ,t.DiameterOfCircleYoffset
		  ,t.Corner1Xofffset
		  ,t.Corner1Yoffset
		  ,t.Corner2Xoffset
		  ,t.Corner2Yoffset
		  ,t.Corner3Xoffset
		  ,t.Corner3Yoffset
		  ,t.Corner4Xoffset
		  ,t.Corner4Yoffset
		  ,t.Corner5Xoffset
		  ,t.Corner5Yoffset
		  ,t.Corner6Xoffset
		  ,t.Corner6Yoffset
		  ,t.Corner7Xoffset
		  ,t.Corner7Yoffset
		  ,t.Corner8Xoffset
		  ,t.Corner8Yoffset
		  ,t.NumberVertices
		  ,t.Rotation
		  ,t.ReferenceOptionID
	FROM [RigTrack].[tblTarget] t
	WHERE t.[ID] = @TargetID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsLastSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_GetTargetsLastSurvey]
(
	@TargetID int = null,
	@CurveNumber int = null
)
AS
BEGIN
	SELECT TOP 1 s.Name
		  ,s.MD
		  ,s.INC
		  ,s.Azimuth
		  ,s.TVD
		  ,s.NS
		  ,s.EW
		  ,s.SurveyComment
		  , s.SubseaTVD
		  , s.VerticalSection
		  , s.CL
		  , s.ClosureDistance
		  , s.ClosureDirection
		  , s.DLS
		  , s.DLA
		  , s.BR
		  , s.WR
		  , s.TFO
		  ,s.SurveyComment
	FROM [RigTrack].[tblSurvey] s
	INNER JOIN [RigTrack].[tblCurve] c
	ON c.ID = s.CurveID
	INNER JOIN [RigTrack].[tblTarget] t
	ON t.ID = c.TargetID
	WHERE t.ID = @TargetID AND c.Number = @CurveNumber - 1 AND c.IsActive = 'False'
	ORDER BY s.ID DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTargetsOnTargetID]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [RigTrack].[sp_GetTargetsOnTargetID]
	@TargetID int 
AS
BEGIN
IF(@TargetID  > 0)
	BEGIN
		  Select t.[ID]
		  ,t.[Name] as TargetName
		  ,t.[CurveGroupID]
		  ,t.[TargetShapeID]
		  ,ts.[Name] as 'TargetShapeName'
		  ,t.[TVD]
		  ,t.[NSCoordinate]
		  ,t.[EWCoordinate]
		  ,t.[PolarDirection]
		  ,t.[PolarDistance]
		  ,t.[INCFromLastTarget]
		  ,t.[AZMFromLastTarget]
		  ,t.[InclinationAtTarget]
		  ,t.[AzimuthAtTarget]
		  ,t.[NumberVertices]
		  ,t.[Rotation]
		  ,t.[TargetThickness]
		  ,t.[DrawingPattern]
		  ,d.[Name] as 'DrawingPatternName'
		  ,t.[TargetComment]
		  ,t.[TargetOffsetXoffset]
		  ,t.[TargetOffsetYoffset]
		  ,t.[DiameterOfCircleXoffset]
		  ,t.[DiameterOfCircleYoffset]
		  ,t.[Corner1Xofffset]
		  ,t.[Corner1Yoffset]
		  ,t.[Corner2Xoffset]
		  ,t.[Corner2Yoffset]
		  ,t.[Corner3Xoffset]
		  ,t.[Corner3Yoffset]
		  ,t.[Corner4Xoffset]
		  ,t.[Corner4Yoffset]
		  ,t.[Corner5Xoffset]
		  ,t.[Corner5Yoffset]
		  ,t.[Corner6Xoffset]
		  ,t.[Corner6Yoffset]
		  ,t.[Corner7Xoffset]
		  ,t.[Corner7Yoffset]
		  ,t.[Corner8Xoffset]
		  ,t.[Corner8Yoffset]
		  ,t.[ReferenceOptionID]
		FROM [RigTrack].[tblTarget] t
		INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
		INNER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		WHERE t.ID = @TargetID 
		ORDER BY t.[ID] ASC
	END
	ELSE
	BEGIN
			Select t.[ID]
			,t.[Name] as TargetName
			  ,t.[CurveGroupID]
			  ,t.[TargetShapeID]
			  ,ts.[Name] as 'TargetShapeName'
			  ,t.[TVD]
			  ,t.[NSCoordinate]
			  ,t.[EWCoordinate]
			  ,t.[PolarDirection]
			  ,t.[PolarDistance]
			  ,t.[INCFromLastTarget]
			  ,t.[AZMFromLastTarget]
			  ,t.[InclinationAtTarget]
			  ,t.[AzimuthAtTarget]
			  ,t.[NumberVertices]
			  ,t.[Rotation]
			  ,t.[TargetThickness]
			  ,t.[DrawingPattern]
			  ,d.[Name] as 'DrawingPatternName'
			  ,t.[TargetComment]
			,t.[TargetOffsetXoffset]
			,t.[TargetOffsetYoffset]
		  ,t.[DiameterOfCircleXoffset]
		  ,t.[DiameterOfCircleYoffset]
		  ,t.[Corner1Xofffset]
		  ,t.[Corner1Yoffset]
		  ,t.[Corner2Xoffset]
		  ,t.[Corner2Yoffset]
		  ,t.[Corner3Xoffset]
		  ,t.[Corner3Yoffset]
		  ,t.[Corner4Xoffset]
		  ,t.[Corner4Yoffset]
		  ,t.[Corner5Xoffset]
		  ,t.[Corner5Yoffset]
		  ,t.[Corner6Xoffset]
		  ,t.[Corner6Yoffset]
		  ,t.[Corner7Xoffset]
		  ,t.[Corner7Yoffset]
		  ,t.[Corner8Xoffset]
		  ,t.[Corner8Yoffset]
			  ,t.[ReferenceOptionID]
		  FROM [RigTrack].[tblTarget] t
		  INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.ID = t.TargetShapeID
		  INNER JOIN [RigTrack].[tlkpDrawingPattern] d ON d.ID = t.DrawingPattern
		  ORDER BY t.[ID] ASC
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetTieInOriginalData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [RigTrack].[sp_GetTieInOriginalData]
	@SurveyID int 
AS
BEGIN
	SELECT 
		s.TieInSubseaTVD as 'SubseaTVD'
		,s.TieInNS as 'NS'
		,s.TieInEW as 'EW'
		,s.TieInVerticalSection as 'VerticalSection' 
	FROM [RigTrack].[tblSurvey] s
	WHERE s.ID = @SurveyID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_GetVerticalSectionRefForCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/30/2016
-- Description:	Return Vertical Section Reference
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_GetVerticalSectionRefForCurveGroup] 
(
	@CurveGroupID int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT		v.[Name]
	FROM		[RigTrack].[tblCurveGroup] cg
	INNER JOIN	[RigTrack].[tlkpVerticalSectionRef] v
		ON cg.[VerticalSectionReferenceID] = v.[ID]
	WHERE		cg.[ID] = @CurveGroupID
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertAttachment]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertAttachment]
	@CurveGroupID int = null
   ,@Name nvarchar(250) = null
   ,@Type nvarchar(50) = null
   ,@Attachment varbinary(MAX) = null
AS
BEGIN
		INSERT INTO [RigTrack].[tlkpCurveGroupAttachments]
		([CurveGroupID]
		,[Name]
		,[Type]
		,[Attachment]
		,[CreateDate]
		,[LastModifyDate]
		,[isActive])
		VALUES
		(@CurveGroupID
		,@Name
		,@Type
		,@Attachment
		,GetDate()
		,GetDate()
		,1)
		SELECT @@IDENTITY
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertAttachmentLogo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jesse Dudley 
-- Create date: 9/1/2016
-- Description:	<Insert logo attachment for reports>
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_InsertAttachmentLogo]
	-- Add the parameters for the stored procedure here
	@ReportID int = null
   ,@Name nvarchar(50) = null
   ,@Type nvarchar(50) = null
   ,@Attachment varbinary(MAX) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [RigTrack].[tlkpReportAttachments]
        ([ReportID]
		,[Name]
		,[Type]
		,[Attachment]
		,[CreateDate]
		,[LastModifyDate]
		,[isActive])
		VALUES
		(@ReportID
		,@Name
		,@Type
		,@Attachment
		,GetDate()
		,GetDate()
		,1)
		SELECT @@IDENTITY
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertCompanyAttachmentLogo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jesse Dudley 
-- Create date: 9/1/2016
-- Description:	<Insert logo attachment for reports>
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_InsertCompanyAttachmentLogo]
	-- Add the parameters for the stored procedure here
	@CompanyID int = null
   ,@Name nvarchar(50) = null
   ,@Type nvarchar(50) = null
   ,@Attachment varbinary(MAX) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF(ISNULL(@CompanyID, 0) = 0)
	BEGIN
		SELECT 0
	END
	ELSE
	BEGIN
	UPDATE [RigTrack].[tblCompany]
	SET [isAttachment] = 1

	INSERT INTO [RigTrack].[tlkpCompanyAttachments]
        ([CompanyID]
		,[Name]
		,[Type]
		,[Attachment]
		,[CreateDate]
		,[LastModifyDate]
		,[isActive])
		VALUES
		(@CompanyID
		,@Name
		,@Type
		,@Attachment
		,GetDate()
		,GetDate()
		,1)
		SELECT @@IDENTITY
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateBHABitData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_InsertUpdateBHABitData]
	@ID int = NULL,
	@BHAID int= NULL,
	@BitSno nvarchar(120) = NULL,
	@BitDesc nvarchar(max) = NULL,
	@ODFrac nvarchar(120) = NULL,
	@BitLength decimal(18,2) = NULL,
	@Connection nvarchar(120) = NULL,
	@BitType nvarchar(50) = NULL,
	@BearingType nvarchar(50) = NULL,
	@BitMfg nvarchar(50) = NULL,
	@BitNumber nvarchar(50) = NULL,
	@NUMJETS nvarchar(50) = NULL,
	@InnerRow nvarchar(50) = NULL,
	@OuterRow nvarchar(50) = NULL,
	@DullChar nvarchar(50) = NULL,
	@Location nvarchar(50) = NULL,
	@BearingSeals nvarchar(50) = NULL,
	@Guage nvarchar(50) = NULL,
	@OtherDullChar nvarchar(50) = NULL,
	@ReasonPulled nvarchar(150) = NULL,
	@BittoSensor nvarchar(50) = NULL,
	@BittoGamma nvarchar(50) = NULL,
	@BittoResistivity nvarchar(50) = NULL,
	@BittoPorosity nvarchar(50) = NULL,
	@BittoDNSC nvarchar(50) = NULL,
	@BittoGyro nvarchar(50) = NULL,
	@CreateDate datetime = NULL,
	@LastModifyDate datetime = NULL,
	@isActive bit = NULL,
	@Jet1 decimal(18,2) = NULL,
	@Jet2 decimal(18,2) = NULL,
	@Jet3 decimal(18,2) = NULL,
	@Jet4 decimal(18,2) = NULL,
	@Jet5 decimal(18,2) = NULL,
	@Jet6 decimal(18,2) = NULL,
	@Jet7 decimal(18,2) = NULL,
	@Jet8 decimal(18,2) = NULL,
	@Jet9 decimal(18,2) = NULL,
	@Jet10 decimal(18,2) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblBHABitData]
		   SET
		   BHAID=@BHAID,
	
	BitSno =@BitSno,
	BitDesc =@BitDesc,
	ODFrac =@ODFrac,
	BitLength =@BitLength,
	Connection =@Connection,
	BitType =@BitType,
	BearingType =@BearingType,
	BitMfg =@BitMfg,
	BitNumber =@BitNumber,
	NUMJETS =@NUMJETS,
	InnerRow =@InnerRow ,
	OuterRow =@OuterRow,
	DullChar =@DullChar,
	Location =@Location,
	BearingSeals =@BearingSeals,
	Guage =@Guage,
	OtherDullChar =@OtherDullChar,
	ReasonPulled =@ReasonPulled ,
	
	BittoSensor =@BittoSensor,
	BittoGamma =@BittoGamma,
	BittoResistivity =@BittoResistivity,
	BittoPorosity =@BittoPorosity,
	BittoDNSC =@BittoDNSC,
	BittoGyro =@BittoGyro,
	CreateDate=getDate(),
	LastModifyDate=getDate(),
	isActive=@isActive,
	Jet1=@Jet1,
	Jet2=@Jet2,
	Jet3=@Jet3,
	Jet4=@Jet4,
	Jet5=@Jet5,
	Jet6=@Jet6,
	Jet7=@Jet7,
	Jet8=@Jet8,
	Jet9=@Jet9,
	Jet10=@Jet10
		 WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblBHABitData]
				   (
	BHAID,
	
	BitSno,
	BitDesc,
	ODFrac,
	BitLength,
	Connection ,
	BitType,
	BearingType,
	BitMfg ,
	BitNumber ,
	NUMJETS ,
	InnerRow,
	OuterRow ,
	DullChar,
	Location,
	BearingSeals ,
	Guage ,
	OtherDullChar ,
	ReasonPulled,
	BittoSensor,
	BittoGamma,
	BittoResistivity,
	BittoPorosity,
	BittoDNSC,
	BittoGyro,
	CreateDate,
	LastModifyDate,
	isActive,
	Jet1,
	Jet2,
	Jet3,
	Jet4,
	Jet5,
	Jet6,
	Jet7,
	Jet8,
	Jet9,
	Jet10
	)
			 VALUES
				   (
				   @BHAID,
	
	@BitSno,
	@BitDesc,
	@ODFrac,
	@BitLength,
	@Connection ,
	@BitType,
	@BearingType,
	@BitMfg ,
	@BitNumber ,
	@NUMJETS ,
	@InnerRow,
	@OuterRow ,
	@DullChar,
	@Location,
	@BearingSeals ,
	@Guage ,
	@OtherDullChar ,
	@ReasonPulled,
	
	@BittoSensor,
	@BittoGamma,
	@BittoResistivity,
	@BittoPorosity,
	@BittoDNSC,
	@BittoGyro,
	@CreateDate,
	@LastModifyDate,
	@isActive,
	@Jet1,
	@Jet2,
	@Jet3,
	@Jet4,
	@Jet5,
	@Jet6,
	@Jet7,
	@Jet8,
	@Jet9,
	@Jet10
	)
		SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateBHAInfoDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_InsertUpdateBHAInfoDetails]
	@ID int = NULL,
	@JOBID int= NULL,
	@BHANumber nvarchar(120)= NULL,
	@BHADesc nvarchar(max) = NULL,
	@BHAType int = NULL,
	@BitSno nvarchar(120) = NULL,
	@BitDesc nvarchar(max) = NULL,
	@ODFrac nvarchar(120) = NULL,
	@BitLength decimal(18,2) = NULL,
	@Connection nvarchar(120) = NULL,
	@BitType nvarchar(50) = NULL,
	@BearingType nvarchar(50) = NULL,
	@BitMfg nvarchar(50) = NULL,
	@BitNumber nvarchar(50) = NULL,
	@NUMJETS nvarchar(50) = NULL,
	@InnerRow nvarchar(50) = NULL,
	@OuterRow nvarchar(50) = NULL,
	@DullChar nvarchar(50) = NULL,
	@Location nvarchar(50) = NULL,
	@BearingSeals nvarchar(50) = NULL,
	@Guage nvarchar(50) = NULL,
	@OtherDullChar nvarchar(50) = NULL,
	@ReasonPulled nvarchar(150) = NULL,
	@MotorDesc nvarchar(max) = NULL,
	@MotorMFG nvarchar(50) = NULL,
	@NBStabilizer nvarchar(50) = NULL,
	@Model nvarchar(50) = NULL,
	@Revolutions nvarchar(50) = NULL,
	@Bend nvarchar(50) = NULL,
	@RotorJet nvarchar(50) = NULL,
	@BittoBend nvarchar(50) = NULL,
	@PropBUR nvarchar(50) = NULL,
	@RealBUR nvarchar(50) = NULL,
	@PadOD nvarchar(50) = NULL,
	@AverageDifferential nvarchar(50) = NULL,
	@Lobes nvarchar(50) = NULL,
	@OffBottomDifference nvarchar(50) = NULL,
	@Stages nvarchar(50) = NULL,
	@StallPressure nvarchar(50) = NULL,
	@BittoSensor nvarchar(50) = NULL,
	@BittoGamma nvarchar(50) = NULL,
	@BittoResistivity nvarchar(50) = NULL,
	@BittoPorosity nvarchar(50) = NULL,
	@BittoDNSC nvarchar(50) = NULL,
	@BittoGyro nvarchar(50) = NULL,
	@CreateDate datetime = NULL,
	@LastModifyDate datetime = NULL,
	@isActive bit = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblBHADataInfo]
		   SET
		   --JOBID=@JOBID,
	--BHANumber=@BHANumber,
	BHADesc =@BHADesc,
	--BHAType =@BHAType,
	BitSno =@BitSno,
	BitDesc =@BitDesc,
	ODFrac =@ODFrac,
	BitLength =@BitLength,
	Connection =@Connection,
	BitType =@BitType,
	BearingType =@BearingType,
	BitMfg =@BitMfg,
	BitNumber =@BitNumber,
	NUMJETS =@NUMJETS,
	InnerRow =@InnerRow ,
	OuterRow =@OuterRow,
	DullChar =@DullChar,
	Location =@Location,
	BearingSeals =@BearingSeals,
	Guage =@Guage,
	OtherDullChar =@OtherDullChar,
	ReasonPulled =@ReasonPulled ,
	MotorDesc =@MotorDesc,
	MotorMFG =@MotorMFG ,
	NBStabilizer =@NBStabilizer,
	Model =@Model,
	Revolutions =@Revolutions,
	Bend =@Bend,
	RotorJet =@RotorJet,
	BittoBend =@BittoBend,
	--PropBUR =@PropBUR,
	--RealBUR =@RealBUR,
	--PadOD =@PadOD,
	--AverageDifferential =@AverageDifferential,
	--Lobes =@Lobes,
	--OffBottomDifference =@OffBottomDifference,
	--Stages =@Stages,
	--StallPressure =@StallPressure,
	--BittoSensor =@BittoSensor,
	--BittoGamma =@BittoGamma,
	--BittoResistivity =@BittoResistivity,
	--BittoPorosity =@BittoPorosity,
	--BittoDNSC =@BittoDNSC,
	--BittoGyro =@BittoGyro,
	--CreateDate=getDate(),
	LastModifyDate=getDate(),
	isActive=@isActive
		  WHERE  [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblBHADataInfo]
				   (
	JOBID,
	BHANumber,
	BHADesc,
	BHAType,
	BitSno,
	BitDesc,
	ODFrac,
	BitLength,
	Connection ,
	BitType,
	BearingType,
	BitMfg ,
	BitNumber ,
	NUMJETS ,
	InnerRow,
	OuterRow ,
	DullChar,
	Location,
	BearingSeals ,
	Guage ,
	OtherDullChar ,
	ReasonPulled,
	MotorDesc ,
	MotorMFG,
	NBStabilizer ,
	Model ,
	Revolutions ,
	Bend ,
	RotorJet ,
	BittoBend ,
	PropBUR,
	RealBUR,
	PadOD,
	AverageDifferential,
	Lobes,
	OffBottomDifference,
	Stages,
	StallPressure,
	BittoSensor,
	BittoGamma,
	BittoResistivity,
	BittoPorosity,
	BittoDNSC,
	BittoGyro,
	CreateDate,
	LastModifyDate,
	isActive
	)
			 VALUES
				   (
				   @JOBID,
	@BHANumber,
	@BHADesc,
	@BHAType,
	@BitSno,
	@BitDesc,
	@ODFrac,
	@BitLength,
	@Connection ,
	@BitType,
	@BearingType,
	@BitMfg ,
	@BitNumber ,
	@NUMJETS ,
	@InnerRow,
	@OuterRow ,
	@DullChar,
	@Location,
	@BearingSeals ,
	@Guage ,
	@OtherDullChar ,
	@ReasonPulled,
	@MotorDesc ,
	@MotorMFG,
	@NBStabilizer ,
	@Model ,
	@Revolutions ,
	@Bend ,
	@RotorJet ,
	@BittoBend ,
	@PropBUR,
	@RealBUR,
	@PadOD,
	@AverageDifferential,
	@Lobes,
	@OffBottomDifference,
	@Stages,
	@StallPressure,
	@BittoSensor,
	@BittoGamma,
	@BittoResistivity,
	@BittoPorosity,
	@BittoDNSC,
	@BittoGyro,
	@CreateDate,
	@LastModifyDate,
	@isActive
	)
		SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateBHAMotorData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [RigTrack].[sp_InsertUpdateBHAMotorData]
	@ID int = NULL,
	@BHAID int= NULL,
	@CompanyID int=NULL,
	@JOBID int=NULL,
	@MotorDesc nvarchar(120) = NULL,
	@MotorMFG nvarchar(120) = NULL,
	@NBStabilizer nvarchar(120) = NULL,
	@Model nvarchar(120) = NULL,
	@Revolutions nvarchar(120) = NULL,
	@Bend nvarchar(120) = NULL,
	@RotorJet nvarchar(120) = NULL,
	@BittoBend nvarchar(120) = NULL,
	@PropBUR nvarchar(120) = NULL,
	@RealBUR nvarchar(120) = NULL,
	@PadOD nvarchar(120) = NULL,
	@AverageDifferential nvarchar(120) = NULL,
	@Lobes nvarchar(120) = NULL,
	@OffBottomDifference nvarchar(120) = NULL,
	@Stages nvarchar(120) = NULL,
	@StallPressure nvarchar(120) = NULL,
	@Clearence nvarchar(120) = NULL,
	@AvgOnBottomSPP nvarchar(120) = NULL,
	@AvgOffBottomSPP nvarchar(120) = NULL,
	@NoOfStalls nvarchar(120) = NULL,
	@StallTime nvarchar(120) = NULL,
	@Formation nvarchar(120) = NULL,
	@BentSubDeg nvarchar(120) = NULL,
	@Elastomer nvarchar(120) = NULL,
	@BendType nvarchar(120) = NULL,
	@ClearenceRng nvarchar(120) = NULL,
	@MEDCompany nvarchar(120) = NULL,
	@NoOfMWDRuns nvarchar(120) = NULL,
	@InspectionCmpny nvarchar(120) = NULL,
	@MotorFailure bit = NULL,
	@ExtendedPowerSection bit = NULL,
	@Inspected bit = NULL,
	@ReasonPulled nvarchar(120) = NULL,
	@BHAObjectives nvarchar(120) = NULL,
	@BHAPerformance nvarchar(120) = NULL,
	@AdditionalComments nvarchar(120) = NULL,
	@BotStabilizerType nvarchar(120) = NULL,
	@BotStabBladeType nvarchar(120) = NULL,
	@BotStabLength nvarchar(120) = NULL,
	@LowerStabOD nvarchar(120) = NULL,
	@EvenWall nvarchar(120) = NULL,
	@TopStabilizerType nvarchar(120) = NULL,
	@TopStabBladeType nvarchar(120) = NULL,
	@TopStabLength nvarchar(120) = NULL,
	@UpperStabOD nvarchar(120) = NULL,
	@InterferenceFit nvarchar(120) = NULL,
	@InterferenceTol nvarchar(120) = NULL,
	@WearPad nvarchar(120) = NULL,
	@WearPadType nvarchar(120) = NULL,
	@Wearpadlength nvarchar(120) = NULL,
	@WearpadHeight nvarchar(120) = NULL,
	@WearpadWidth nvarchar(120) = NULL,
	@WearpadGuage nvarchar(120) = NULL,
	@BitToWearpad nvarchar(120) = NULL,
	@MaxSurfRPM nvarchar(120) = NULL,
	@MaxDLRotating nvarchar(120) = NULL,
	@MaxDLSliding nvarchar(120) = NULL,
	@MaxDiffPress nvarchar(120) = NULL,
	@MaxFlowRate nvarchar(120) = NULL,
	@MaxTorque nvarchar(120) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblBHAMotorData]
		   SET
		   BHAID=@BHAID,
		MotorDesc=@MotorDesc,
		MotorMFG=@MotorMFG,
		NBStabilizer=@NBStabilizer,
		Model=@Model,
		Revolutions=@Revolutions,
		Bend=@Bend,
		RotorJet=@RotorJet,
		BittoBend=@BittoBend,
		PropBUR=@PropBUR,
		RealBUR=@RealBUR,
		PadOD=@PadOD,
		AverageDifferential=@AverageDifferential,
		Lobes=@Lobes,
		OffBottomDifference=@OffBottomDifference,
		Stages=@Stages,
		StallPressure=@StallPressure,
		Clearence=@Clearence,
		AvgOnBottomSPP=@AvgOnBottomSPP,
		AvgOffBottomSPP=@AvgOffBottomSPP,
		NoOfStalls=@NoOfStalls,
		StallTime=@StallTime,
		Formation=@Formation,
		BentSubDeg=@BentSubDeg,
		Elastomer=@Elastomer,
		BendType=@BendType,
		ClearenceRng=@ClearenceRng,
		MEDCompany=@MEDCompany,
		NoOfMWDRuns=@NoOfMWDRuns,
		InspectionCmpny=@InspectionCmpny,
		MotorFailure=@MotorFailure,
		ExtendedPowerSection=@ExtendedPowerSection,
		Inspected=@Inspected,
		ReasonPulled=@ReasonPulled,
		BHAObjectives=@BHAObjectives,
		BHAPerformance=@BHAPerformance,
		AdditionalComments=@AdditionalComments,
		BotStabilizerType=@BotStabilizerType,
		BotStabBladeType=@BotStabBladeType,
		BotStabLength=@BotStabLength,
		LowerStabOD=@LowerStabOD,
		EvenWall=@EvenWall,
		TopStabilizerType=@TopStabilizerType,
		TopStabBladeType=@TopStabBladeType,
		TopStabLength=@TopStabLength,
		UpperStabOD=@UpperStabOD,
		InterferenceFit=@InterferenceFit,
		InterferenceTol=@InterferenceTol,
		WearPad=@WearPad,
		WearPadType=@WearPadType,
		Wearpadlength=@Wearpadlength,
		WearpadHeight=@WearpadHeight,
		WearpadWidth=@WearpadWidth,
		WearpadGuage=@WearpadGuage,
		BitToWearpad=@BitToWearpad,
		MaxSurfRPM=@MaxSurfRPM,
		MaxDLRotating=@MaxDLRotating,
		MaxDLSliding=@MaxDLSliding,
		MaxDiffPress=@MaxDiffPress,
		MaxFlowRate=@MaxFlowRate,
		MaxTorque=@MaxTorque
		 WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblBHAMotorData]
				   (
				   CompanyID,
				   JOBID,
	BHAID,
	
	MotorDesc,
MotorMFG,
NBStabilizer,
Model,
Revolutions,
Bend,
RotorJet,
BittoBend,
PropBUR,
RealBUR,
PadOD,
AverageDifferential,
Lobes,
OffBottomDifference,
Stages,
StallPressure,
Clearence,
AvgOnBottomSPP,
AvgOffBottomSPP,
NoOfStalls,
StallTime,
Formation,
BentSubDeg,
Elastomer,
BendType,
ClearenceRng,
MEDCompany,
NoOfMWDRuns,
InspectionCmpny,
MotorFailure,
ExtendedPowerSection,
Inspected,
ReasonPulled,
BHAObjectives,
BHAPerformance,
AdditionalComments,
BotStabilizerType,
BotStabBladeType,
BotStabLength,
LowerStabOD,
EvenWall,
TopStabilizerType,
TopStabBladeType,
TopStabLength,
UpperStabOD,
InterferenceFit,
InterferenceTol,
WearPad,
WearPadType,
Wearpadlength,
WearpadHeight,
WearpadWidth,
WearpadGuage,
BitToWearpad,
MaxSurfRPM,
MaxDLRotating,
MaxDLSliding,
MaxDiffPress,
MaxFlowRate,
MaxTorque
	)
			 VALUES
				   (
				   @CompanyID,
				   @JOBID,
				   @BHAID,
	@MotorDesc,
@MotorMFG,
@NBStabilizer,
@Model,
@Revolutions,
@Bend,
@RotorJet,
@BittoBend,
@PropBUR,
@RealBUR,
@PadOD,
@AverageDifferential,
@Lobes,
@OffBottomDifference,
@Stages,
@StallPressure,
@Clearence,
@AvgOnBottomSPP,
@AvgOffBottomSPP,
@NoOfStalls,
@StallTime,
@Formation,
@BentSubDeg,
@Elastomer,
@BendType,
@ClearenceRng,
@MEDCompany,
@NoOfMWDRuns,
@InspectionCmpny,
@MotorFailure,
@ExtendedPowerSection,
@Inspected,
@ReasonPulled,
@BHAObjectives,
@BHAPerformance,
@AdditionalComments,
@BotStabilizerType,
@BotStabBladeType,
@BotStabLength,
@LowerStabOD,
@EvenWall,
@TopStabilizerType,
@TopStabBladeType,
@TopStabLength,
@UpperStabOD,
@InterferenceFit,
@InterferenceTol,
@WearPad,
@WearPadType,
@Wearpadlength,
@WearpadHeight,
@WearpadWidth,
@WearpadGuage,
@BitToWearpad,
@MaxSurfRPM,
@MaxDLRotating,
@MaxDLSliding,
@MaxDiffPress,
@MaxFlowRate,
@MaxTorque
	)
		SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateCompany]
	@ID int = null
   ,@CompanyName nvarchar(120) = null
   ,@CompanyAddress1 nvarchar(120) = null
   ,@CompanyAddress2 nvarchar(120) = null
   ,@CompanyContactFirstName nvarchar(80) = null
   ,@CompanyContactLastName nvarchar(80) = null
   ,@ContactPhone nvarchar(25) = null
   ,@ContactEmail nvarchar(120) = null
   ,@City nvarchar(80) = null
   ,@StateID int = null
   ,@CountryID int = null
   ,@Zip nvarchar(15) = null
   ,@isAttachment bit = null
   ,@CreateDate datetime = null
   ,@LastModifyDate datetime = null
   ,@isActive bit = null
AS
BEGIN
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCompany]
		SET [CompanyName] = @CompanyName
		   ,[CompanyAddress1] = @CompanyAddress1
		   ,[CompanyAddress2] = @CompanyAddress2
		   ,[CompanyContactFirstName] = @CompanyContactFirstName
		   ,[CompanyContactLastName] = @CompanyContactLastName
		   ,[ContactPhone] = @ContactPhone
		   ,[ContactEmail] = @ContactEmail
		   ,[City] = @City
		   ,[StateID] = @StateID
		   ,[CountryID] = @CountryID
		   ,[Zip] = @Zip
		   ,[isAttachment] = @isAttachment
		   ,[LastModifyDate] = GETDATE()
		   ,[isActive] = @isActive
		WHERE [ID] = @ID
		SELECT @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblCompany]
		([CompanyName]
		,[CompanyAddress1]
		,[CompanyAddress2]
		,[CompanyContactFirstName]
		,[CompanyContactLastName]
		,[ContactPhone]
		,[ContactEmail]
		,[City]
		,[StateID]
		,[CountryID]
		,[Zip]
		,[isAttachment]
		,[CreateDate]
		,[LastModifyDate]
		,[isActive]
		)
		VALUES
		(@CompanyName
		,@CompanyAddress1
		,@companyaddress2
		,@CompanyContactFirstName
		,@CompanyContactLastName
		,@ContactPhone
		,@ContactEmail
		,@City
		,@StateID
		,@CountryID
		,@Zip
		,@isAttachment
		,GETDATE()
		,GETDATE()
		,@isActive
		)
		SET @ID = @@IDENTITY
		SELECT @@IDENTITY
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateCurve]
	@ID int = null
   ,@CurveGroupID int = null
   ,@TargetID int = null
   ,@Number int = null
   ,@Name nvarchar(50) = null
   ,@CurveTypeID int = null
   ,@NorthOffset decimal(18,2) = null
   ,@EastOffset decimal(18,2) = null
   ,@VSDirection decimal(18,2) = null
   ,@RKBElevation decimal(18,2) = null
   ,@Color nvarchar(25) = null
   ,@CreateDate datetime = null
   ,@LastModifyDate datetime = null
   ,@isActive bit = null
AS
BEGIN
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCurve]
		SET [CurveGroupID] = @CurveGroupID
		   ,[TargetID] = @TargetID
		   ,[Number] = @Number
		   ,[Name] = @Name
		   ,[CurveTypeID] = @CurveTypeID
		   ,[NorthOffset] = @NorthOffset
		   ,[EastOffset] = @EastOffset
		   ,[VSDirection] = @VSDirection
		   ,[RKBElevation] = @RKBElevation
		   ,[Color] = @Color
		   ,[LastModifyDate] = GetDate()
		   ,[isActive] = 1
		WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblCurve]
		([CurveGroupID]
		,[TargetID]
		,[Number]
		,[Name]
		,[CurveTypeID]
		,[NorthOffset]
		,[EastOffset]
		,[VSDirection]
		,[RKBElevation]
		,[Color]
		,[CreateDate]
		,[LastModifyDate]
		,[isActive]
		,[LocationID]
		,[BitToSensor]
		,[ListDistanceBool]
		,[ComparisonCurve]
		,[AtHSide]
		,[TVDComp]
		)
		VALUES
		(@CurveGroupID
		,@TargetID
		,@Number
		,@Name
		,@CurveTypeID
		,@NorthOffset
		,@EastOffset
		,@VSDirection
		,@RKBElevation
		,@Color
		,GetDate()
		,GetDate()
		,1
		,1000
		,0.00
		,0
		,0
		,0.00
		,0.00)
		SET @ID = @@IDENTITY
		SELECT @@IDENTITY
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/24/2016
-- Description:	Insert/Update Curve Group (if insert, insert Curve ghost)
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_InsertUpdateCurveGroup] 
(
	  @ID int = 0
	, @CurveGroupName nvarchar(25) = ''
	, @JobNumber nvarchar(25) = ''
	, @Company nvarchar(50) = ''
	, @LeaseWell nvarchar(50) = ''
	, @JobLocation nvarchar(50) = ''
	, @RigName nvarchar(50) = ''
	, @StateID int = 0
	, @CountryID int = 0
	, @Declination decimal(18, 2)
	, @Grid nvarchar(50) = ''
	, @RKB nvarchar(50) = ''
	, @GLorMSLID int = 0
	, @MethodOfCalculationID int = 0
	, @DogLegSeverityID int = 0
	, @MeasurementUnitsID int = 0
	, @UnitsConvert bit = 0
	, @OutputDirectionID int = 0
	, @InputDirectionID int = 0
	, @VerticalSectionReferenceID int = 0
	, @EWOffset decimal(18, 2)
	, @NSOffset decimal(18, 2)
	, @WorkNumber int = 0
    , @PlanNumber int = 0
	, @MD decimal(18,2) = 0
	, @Incl decimal(18,2) = 0
	, @Azimuth decimal(18,2) = 0
	, @TVD decimal(18,2) = 0
	, @NSCoord decimal(18,2) = 0
	, @EWCoord decimal(18,2) = 0
	, @VSection decimal(18,2) = 0
	, @WRate decimal(18,2) = 0
	, @BRate decimal(18,2) = 0
	, @DLS decimal(18,2) = 0
	, @TFO decimal(18,2) = 0
	, @Closure decimal(18,2) = 0
	, @BitToSensor decimal(18,2) = 0
	, @At decimal(18,2) = 0
	, @Location int = 0
	, @LeastDistanceOnOff bit = 0
	, @LeastDistanceText nvarchar(50) = ''
	, @AtHSide nvarchar(50) = ''
	, @TVDComp nvarchar(50) = ''
	, @ComparisonCurveValue int = 0
	, @ComparisonCurveText nvarchar(50) = ''
	, @isActive bit = 1
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CurveGroupID int = 0
	DECLARE @LoopCounter int = 0
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCurveGroup]
		   SET [CurveGroupName] = @CurveGroupName
			  ,[JobNumber] = @JobNumber
			  ,[Company] = @Company
			  ,[LeaseWell] = @LeaseWell
			  ,[JobLocation] = @JobLocation
			  ,[RigName] = @RigName
			  ,[StateID] = @StateID
			  ,[CountryID] = @CountryID
			  ,[Declination] = @Declination
			  ,[Grid] = @Grid
			  ,[RKB] = @RKB
			  ,[GLorMSLID] = @GLorMSLID
			  ,[MethodOfCalculationID] = @MethodOfCalculationID
			  ,[DogLegSeverityID] = @DogLegSeverityID
			  ,[OutputDirectionID] = @OutputDirectionID
			  ,[InputDirectionID] = @InputDirectionID
			  ,[VerticalSectionReferenceID] = @VerticalSectionReferenceID
			  ,[EWOffset] = @EWOffset
			  ,[NSOffset] = @NSOffset
			  ,[UnitsConvert] = @UnitsConvert
			  ,[MeasurementUnitsID] = @MeasurementUnitsID
		      ,[WorkNumber] = @WorkNumber
			  ,[PlanNumber] = @PlanNumber
			  ,[MD] = @MD
			  ,[Incl] = @Incl
			  ,[Azimuth] = @Azimuth
			  ,[TVD] = @TVD
			  ,[NSCoord] = @NSCoord
			  ,[EWCoord] = @EWCoord
			  ,[VSection] = @VSection
			  ,[WRate] = @WRate
			  ,[BRate] = @BRate
			  ,[DLS] = @DLS
			  ,[TFO] = @TFO
			  ,[Closure] = @Closure
			  ,[BitToSensor] = @BitToSensor
			  ,[At] = @At
			  ,[LocationID] = @Location
			  ,[LeastDistanceOnOff] = @LeastDistanceOnOff
			  ,[LeastDistanceText] = @LeastDistanceText
			  ,[AtHSide] = @AtHSide
			  ,[TVDComp] = @TVDComp
			  ,[ComparisonCurveValue] = @ComparisonCurveValue
			  ,[ComparisonCurveText] = @ComparisonCurveText
			  ,[LastModifyDate] = GetDate()
			  ,[isActive] = @isActive
		 WHERE [ID] = @ID
		 SELECT @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblCurveGroup]
				   ([WorkNumber]
				   ,[PlanNumber]
				   ,[MD]
				   ,[Incl]
				   ,[Azimuth]
				   ,[TVD]
				   ,[NSCoord]
				   ,[EWCoord]
				   ,[VSection]
				   ,[WRate]
				   ,[BRate]
				   ,[DLS]
				   ,[TFO]
				   ,[Closure]
				   ,[At]
				   ,[LocationID]
				   ,[LeastDistanceOnOff]
				   ,[LeastDistanceText]
				   ,[AtHSide]
				   ,[TVDComp]
				   ,[ComparisonCurveValue]
				   ,[ComparisonCurveText]
				   ,[CreateDate]
				   ,[LastModifyDate]
				   ,[isActive])
			 VALUES
				   (@WorkNumber
				   ,@PlanNumber
				   ,@MD
				   ,@Incl
				   ,@Azimuth
				   ,@TVD
				   ,@NSCoord
				   ,@EWCoord
				   ,@VSection
				   ,@WRate
				   ,@BRate
				   ,@DLS
				   ,@TFO
				   ,@Closure
				   ,@At
				   ,@Location
				   ,@LeastDistanceOnOff
				   ,@LeastDistanceText
				   ,@AtHSide
				   ,@TVDComp
				   ,@ComparisonCurveValue
				   ,@ComparisonCurveText
				   ,GetDate()
				   ,GetDate()
				   ,@isActive)
		SET @CurveGroupID = @@Identity
		SELECT @@IDENTITY

		WHILE @LoopCounter < 30
		BEGIN
			INSERT INTO [RigTrack].[tblCurve]
					   ([CurveGroupID]
					   ,[Number]
					   ,[Name]
					   ,[CurveTypeID]
					   ,[NorthOffset]
					   ,[EastOffset]
					   ,[VSDirection]
					   ,[RKBElevation]
					   ,[CreateDate]
					   ,[LastModifyDate]
					   ,[isActive])
				 VALUES
					   (@CurveGroupID
					   ,@LoopCounter
					   ,NULL
					   ,1009
					   ,NULL
					   ,NULL
					   ,NULL
					   ,NULL
					   ,GetDate()
					   ,GetDate()
					   ,0)
			SET @LoopCounter = @LoopCounter + 1
		END
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateManageJobsCurveGroups]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateManageJobsCurveGroups]
    @ID int = 0,
	@CurveGroupName nvarchar(25) = '',
	@Company  int=0,
	@LeaseWell  nvarchar(50) = '',
	@JobLocation nvarchar(50) = '',
	@RigName nvarchar(50) = '',
	@NSOffset decimal(18, 2) = 0,
	@JobNumber  nvarchar(25) = '',
    @Grid nvarchar(50) = '', 
	@RKB nvarchar(50) = '',
	@Country int = 0,
	@State int = 0,
	@Method int = 0,
	--@Convert bit = 0,
	@MetersFeet int = 0,
	@DogLeg int = 0,
	@GLmsl int = 0,
	@Declination decimal(18, 2) = 0,
	@OutPutDirection int = 0,
	@InPutDirection int = 0,
	@VSection int = 0,
	@EWOffset decimal(18, 2) = 0,
	@isActive bit = 1,
	@JobStartDate DateTime = '',
	@LatitudeLongitude nvarchar(100) = null
AS
BEGIN

IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCurveGroup]
		   SET [CurveGroupName] = @CurveGroupName
			  
			  ,[CompanyID] = @Company
			  ,[LeaseWell] = @LeaseWell
			  ,[JobLocation] = @JobLocation
			  ,[RigName] = @RigName
			  ,[NSOffset] = @NSOffset
			  ,[JobNumber] = @JobNumber
			  ,[Grid] = @Grid
			  ,[RKB] = @RKB
			  ,[CountryID] = @Country
			  ,[StateID] = @State
			  ,[MethodOfCalculationID] = @Method
			  --,[UnitsConvert] = @Convert
			  ,[MeasurementUnitsID] = @MetersFeet
			  ,[DogLegSeverityID] = @DogLeg
			  ,[GLorMSLID] = @GLmsl
			  ,[Declination] = @Declination
			  ,[OutputDirectionID] = @OutPutDirection
			  ,[InputDirectionID] = @InPutDirection
			  ,[VerticalSectionReferenceID] = @VSection
			  ,[EWOffset] = @EWOffset
			  ,[isActive] = @isActive
			  ,[JobStartDate] = @JobStartDate
			  ,[LastModifyDate] = GetDate()
			  ,[primaryLatLong] = @LatitudeLongitude
			  
		 WHERE [ID] = @ID
		 SELECT @ID
		
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblCurveGroup]
		     ( [CurveGroupName]		  
			  ,[CompanyID] 
			  ,[LeaseWell]
			  ,[JobLocation]
			  ,[RigName] 
			  ,[NSOffset] 
			  ,[JobNumber] 
			  ,[Grid] 
			  ,[RKB] 
			  ,[CountryID]
			  ,[StateID] 
			  ,[MethodOfCalculationID] 
			  --,[UnitsConvert] 
			  ,[MeasurementUnitsID]
			  ,[DogLegSeverityID] 
			  ,[GLorMSLID]
			  ,[Declination]
			  ,[OutputDirectionID] 
			  ,[InputDirectionID] 
			  ,[VerticalSectionReferenceID] 
			  ,[EWOffset] 
			  ,[primaryLatLong]
			  ,[CreateDate]
			  ,[LastModifyDate]
			  ,[isActive]
			  ,[JobStartDate]
			  ,[PlotComments]
			  ,[HasWellPlan] )
			 VALUES

			(@CurveGroupName
			  ,@Company
			  ,@LeaseWell
			  ,@JobLocation
			  ,@RigName
			  ,@NSOffset
			  ,@JobNumber
			  ,@Grid
			  ,@RKB
			  ,@Country
			  ,@State
			  ,@Method
			  --,@Convert
			  ,@MetersFeet
			  ,@DogLeg
			  ,@GLmsl
			  ,@Declination
			  ,@OutPutDirection
			  ,@InPutDirection
			  ,@VSection
			  ,@EWOffset
			  ,@LatitudeLongitude
			  ,getdate()
			  ,getdate()
			  ,@isActive
			  ,@JobStartDate
			  ,''
			  ,0)
			  SELECT @@IDENTITY
		END
	
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateReports]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateReports]
(
	@ID int = 0,
	@Name nvarchar(50) = NULL,
	@HeaderComments nvarchar(500) = NULL,
	@CompanyID int = NULL,
	@CurveGroupID int = NULL,
	@CurveID int = NULL,
	@TargetID int = NULL,
	@MeasuredDepth bit = NULL,
	@Inclination bit = NULL,
	@Azimuth bit = NULL,
	@TrueVerticalDepth bit = NULL,
	@N_SCoordinates bit = NULL,
	@E_WCoordinates bit = NULL,
	@VerticalSection bit = NULL,
	@ClosureDistance bit = NULL,
	@ClosureDirection bit = NULL,
	@DogLegSeverity bit = NULL,
	@CourseLength bit = NULL,
	@WalkRate bit = NULL,
	@BuildRate bit = NULL,
	@ToolFace bit = NULL,
	@Comment bit = NULL,
	@SubseaDepth bit = NULL,
	@Radius bit = NULL,
	@GridX bit = NULL,
	@GridY bit = NULL,
	@Left_Right bit = NULL,
	@Up_Down bit = NULL,
	@FNL_FSL bit = NULL,
	@FEL_FWL bit = NULL,
	@Grouping int = NULL,
	@BoxedComments bit = NULL,
	@IncludeProjectToBit bit = NULL,
	@ShowProjToTVDInc bit = NULL,
	@ExtraHeaderComments nvarchar(500) = NULL,
	@ExtraHeader bit = NULL,
	@InterpolatedReports bit = NULL,
	@EWNSReference int = NULL,
	@Mode int = NULL,
	@CreateDate datetime = NULL,
	@LastModifyDate datetime = NULL,
	@isActive bit = NULL
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblReport]
		   SET [Name] = @Name
			  ,[HeaderComments] = @HeaderComments
			  ,[CompanyID] = @CompanyID
			  ,[CurveGroupID] = @CurveGroupID
			  ,[CurveID] = @CurveID
			  ,[TargetID] = @TargetID
			  ,[MeasuredDepth] = @MeasuredDepth
			  ,[Inclination] = @Inclination
			  ,[Azimuth] = @Azimuth
			  ,[TrueVerticalDepth] = @TrueVerticalDepth
			  ,[N_SCoordinates] = @N_SCoordinates
			  ,[E_WCoordinates] = @E_WCoordinates
			  ,[VerticalSection] = @VerticalSection
			  ,[ClosureDistance] = @ClosureDistance
			  ,[ClosureDirection] = @ClosureDirection
			  ,[DogLegSeverity] = @DogLegSeverity
			  ,[CourseLength] = @CourseLength
			  ,[WalkRate] = @WalkRate
			  ,[BuildRate] = @BuildRate
			  ,[ToolFace] = @ToolFace
			  ,[Comment] = @Comment
			  ,[SubseaDepth] = @SubseaDepth
			  ,[Radius] = @Radius
			  ,[GridX] = @GridX
			  ,[GridY] = @GridY
			  ,[Left_Right] = @Left_Right
			  ,[Up_Down] = @Up_Down
			  ,[FNL_FSL] = @FNL_FSL
			  ,[FEL_FWL] = @FEL_FWL
			  ,[Grouping] = @Grouping
			  ,[BoxedComments] = @BoxedComments
			  ,[IncludeProjectToBit] = @IncludeProjectToBit
			  ,[ShowProjToTVDInc] = @ShowProjToTVDInc
			  ,[ExtraHeaderComments] = @ExtraHeaderComments
			  ,[ExtraHeader] = @ExtraHeader
			  ,[InterpolatedReports] = @InterpolatedReports
			  ,[EWNSReference] = @EWNSReference
			  ,[Mode] = @Mode
			  ,[LastModifyDate] = GetDate()
			  ,[isActive] = @isActive
		 WHERE [ID] = @ID
		 SELECT @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblReport]
				   ([Name]
				   ,[HeaderComments]
				   ,[CompanyID]
				   ,[CurveGroupID]
				   ,[CurveID]
				   ,[TargetID]
				   ,[MeasuredDepth]
				   ,[Inclination]
				   ,[Azimuth]
				   ,[TrueVerticalDepth]
				   ,[N_SCoordinates]
				   ,[E_WCoordinates]
				   ,[VerticalSection]
				   ,[ClosureDistance]
				   ,[ClosureDirection]
				   ,[DogLegSeverity]
				   ,[CourseLength]
				   ,[WalkRate]
				   ,[BuildRate]
				   ,[ToolFace]
				   ,[Comment]
				   ,[SubseaDepth]
				   ,[Radius]
				   ,[GridX]
				   ,[GridY]
				   ,[Left_Right]
				   ,[Up_Down]
				   ,[FNL_FSL]
				   ,[FEL_FWL]
				   ,[Grouping]
				   ,[BoxedComments]
				   ,[IncludeProjectToBit]
				   ,[ShowProjToTVDInc]
				   ,[ExtraHeaderComments]
				   ,[ExtraHeader]
				   ,[InterpolatedReports]
				   ,[EWNSReference]
				   ,[Mode]
				   ,[CreateDate]
				   ,[LastModifyDate]
				   ,[isActive])
			 VALUES
				   (@Name
				   ,@HeaderComments
				   ,@CompanyID
				   ,@CurveGroupID
				   ,@CurveID
				   ,@TargetID
				   ,@MeasuredDepth
				   ,@Inclination
				   ,@Azimuth
				   ,@TrueVerticalDepth
				   ,@N_SCoordinates
				   ,@E_WCoordinates
				   ,@VerticalSection
				   ,@ClosureDistance
				   ,@ClosureDirection
				   ,@DogLegSeverity
				   ,@CourseLength
				   ,@WalkRate
				   ,@BuildRate
				   ,@ToolFace
				   ,@Comment
				   ,@SubseaDepth
				   ,@Radius
				   ,@GridX
				   ,@GridY
				   ,@Left_Right
				   ,@Up_Down
				   ,@FNL_FSL
				   ,@FEL_FWL
				   ,@Grouping
				   ,@BoxedComments
				   ,@IncludeProjectToBit
				   ,@ShowProjToTVDInc
				   ,@ExtraHeaderComments
				   ,@ExtraHeader
				   ,@InterpolatedReports
				   ,@EWNSReference
				   ,@Mode
				   ,GetDate()
				   ,GetDate()
				   ,@isActive)
				   		
				   SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/24/2016
-- Description:	Insert/Update Survey
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_InsertUpdateSurvey] 
(
	  @ID int = 0
	, @CurveID int = 0
	, @CurveGroupID int = 0
	, @Name nvarchar(50) = ''
	, @MD decimal(18,2) = 0
	, @INC decimal(18,2) = 0
	, @Azimuth decimal(18,2) = 0
	, @TVD decimal(18,7) = 0
	
	, @SubseaTVD decimal(18,7) = 0
	
	, @NS decimal(18,7) = 0

	, @EW decimal(18,7) = 0

	, @VerticalSection decimal(18,7) = 0

	, @CL decimal(18,2) = 0
	, @ClosureDistance decimal(18,7) = 0

	, @ClosureDirection decimal(18,7) = 0

	, @DLS decimal(18,7) = 0
	, @DLA decimal(18,7) = 0
	, @BR decimal(18,2) = 0
	, @WR decimal(18,2) = 0
	, @TFO decimal(18,2) = 0
	, @SurveyComment nvarchar(150) = ''
	, @isActive bit = 1
	,@RowNumber int = 0
	,@TieInSubseaTVD decimal(18,2) = 0
	,@TieInNS decimal(18,2) = 0
	,@TieInEW decimal(18,2) = 0 
	,@TieInVerticalSection decimal(18,2) = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblSurvey]
		   SET [CurveID] = @CurveID
			  ,[CurveGroupID] = @CurveGroupID
			  ,[Name] = @Name
			  ,[MD] = @MD
			  ,[INC] = @INC
			  ,[Azimuth] = @Azimuth
			  ,[TVD] = @TVD
			 
			  ,[SubseaTVD] = @SubseaTVD
			
			  ,[NS] = @NS
			 
			  ,[EW] = @EW
			
			  ,[VerticalSection] = @VerticalSection
			
			  ,[CL] = @CL
			  ,[ClosureDistance] = @ClosureDistance
			
			  ,[ClosureDirection] = @ClosureDirection
			
			  ,[DLS] = @DLS
			  ,[DLA] = @DLA
			  ,[BR] = @BR
			  ,[WR] = @WR
			  ,[TFO] = @TFO
			  ,[SurveyComment] = @SurveyComment
			  ,[LastModifyDate] = GetDate()
			  ,[isActive] = @isActive
			  ,[RowNumber] = @RowNumber
			  ,[TieInSubseaTVD] = @TieInSubseaTVD
			  ,[TieInNS] = @TieInNS
			  ,[TieInEW] = @TieInEW
			  ,[TieInVerticalSection] = @TieInVerticalSection
		 WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblSurvey]
				   ([CurveID]
				   ,[CurveGroupID]
				   ,[Name]
				   ,[MD]
				   ,[INC]
				   ,[Azimuth]
				   ,[TVD]
				
				   ,[SubseaTVD]
				   
				   ,[NS]
				
				   ,[EW]
				 
				   ,[VerticalSection]
				 
				   ,[CL]
				   ,[ClosureDistance]
			
				   ,[ClosureDirection]
			
				   ,[DLS]
				   ,[DLA]
				   ,[BR]
				   ,[WR]
				   ,[TFO]
				   ,[SurveyComment]
				   ,[CreateDate]
				   ,[LastModifyDate]
				   ,[isActive]
				   ,[RowNumber]
				   ,[TieInSubseaTVD]
				   ,[TieInNS]
				   ,[TieInEW]
				   ,[TieInVerticalSection])
			 VALUES
				   (@CurveID
				   ,@CurveGroupID
				   ,@Name
				   ,@MD
				   ,@INC
				   ,@Azimuth
				   ,@TVD
				
				   ,@SubseaTVD
			
				   ,@NS
				 
				   ,@EW
		
				   ,@VerticalSection
				
				   ,@CL
				   ,@ClosureDistance
				
				   ,@ClosureDirection
				
				   ,@DLS
				   ,@DLA
				   ,@BR
				   ,@WR
				   ,@TFO
				   ,@SurveyComment
				   ,GetDate()
				   ,GetDate()
				   ,@isActive
				   ,@RowNumber
				   ,@TieInSubseaTVD
				   ,@TieInNS
				   ,@TieInEW
				   ,@TieInVerticalSection)
		SELECT @@Identity
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateTarget]
(
	@ID int = null,
	@CurveGroupID int = null,
	@TargetName varchar(50) = null,
	@TargetShapeID int = null,
	@TVD decimal(18, 2) = null,
	@NSCoordinate decimal(18, 2) = null,
	@EWCoordinate decimal(18, 2) = null,
	@PolarDistance decimal(18, 2) = null,
	@PolarDirection decimal(18, 2) = null,
	@INCFromLastTarget decimal(18, 2) = null,
	@AZMFromLastTarget decimal(18, 2) = null,
	@InclinationAtTarget decimal(18, 4) = null,
	@AzimuthAtTarget decimal(18, 4) = null,
	@NumberVertices decimal(18, 2) = null,
	@Rotation decimal(18, 4) = null,
	@Thickness decimal(18, 2) = null,
	@DrawingPattern int = null,
	@TargetComment nvarchar(150) = null,
	@TargetOffsetXOffset decimal(18, 2) = null,
	@TargetOffsetYOffset decimal(18, 2) = null,
	@DiameterOfCircleXOffset decimal(18, 2) = null,
	@DiameterOfCircleYOffset decimal(18, 2) = null,
	@Corner1XOffset decimal(18, 2) = null,
	@Corner1YOffset decimal(18, 2) = null,
	@Corner2XOffset decimal(18, 2) = null,
	@Corner2YOffset decimal(18, 2) = null,
	@Corner3XOffset decimal(18, 2) = null,
	@Corner3YOffset decimal(18, 2) = null,
	@Corner4XOffset decimal(18, 2) = null,
	@Corner4YOffset decimal(18, 2) = null,
	@Corner5XOffset decimal(18, 2) = null,
	@Corner5YOffset decimal(18, 2) = null,
	@Corner6XOffset decimal(18, 2) = null,
	@Corner6YOffset decimal(18, 2) = null,
	@Corner7XOffset decimal(18, 2) = null,
	@Corner7YOffset decimal(18, 2) = null,
	@Corner8XOffset decimal(18, 2) = null,
	@Corner8YOffset decimal(18, 2) = null,
	@ReferenceOptionID int = null
)
AS
BEGIN
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblTarget]
		SET
			[Name] = @TargetName
	       ,[TargetShapeID] = @TargetShapeID
		   ,[TVD] = @TVD
		   ,[NSCoordinate] = @NSCoordinate
		   ,[EWCoordinate] = @EWCoordinate
		   ,[PolarDistance] = @PolarDistance
		   ,[PolarDirection] = @PolarDirection
		   ,[INCFromLastTarget] = @INCFromLastTarget
		   ,[AZMFromLastTarget] = @AZMFromLastTarget
		   ,[InclinationAtTarget] = @InclinationAtTarget
		   ,[AzimuthAtTarget] = @AzimuthAtTarget
		   ,[NumberVertices] = @NumberVertices
		   ,[Rotation] = @Rotation
		   ,[TargetThickness] = @Thickness
		   ,[DrawingPattern] = @DrawingPattern
		   ,[TargetComment] = @TargetComment
		   ,[TargetOffsetXoffset] = @TargetOffsetXOffset
		   ,[TargetOffsetYoffset] = @TargetOffsetYOffset
		   ,[DiameterOfCircleXoffset] = @DiameterOfCircleXOffset
		   ,[DiameterOfCircleYoffset] = @DiameterOfCircleYOffset
		   ,[Corner1Xofffset] = @Corner1XOffset
		   ,[Corner1Yoffset] = @Corner1YOffset
		   ,[Corner2Xoffset] = @Corner2XOffset
		   ,[Corner2Yoffset] = @Corner2YOffset
		   ,[Corner3Xoffset] = @Corner3XOffset
		   ,[Corner3Yoffset] = @Corner3YOffset
		   ,[Corner4Xoffset] = @Corner4XOffset
		   ,[Corner4Yoffset] = @Corner4YOffset
		   ,[Corner5Xoffset] = @Corner5XOffset
		   ,[Corner5Yoffset] = @Corner5YOffset
		   ,[Corner6Xoffset] = @Corner6XOffset
		   ,[Corner6Yoffset] = @Corner6YOffset
		   ,[Corner7Xoffset] = @Corner7XOffset
		   ,[Corner7Yoffset] = @Corner7YOffset
		   ,[Corner8Xoffset] = @Corner8XOffset
		   ,[Corner8Yoffset] = @Corner8YOffset
		   ,[ReferenceOptionID] = @ReferenceOptionID
		   ,[LastModifyDate] = GETDATE()
		   ,[isActive] = 1
		WHERE [ID] = @ID
		SELECT @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblTarget]
		(
			[CurveGroupID]
		   ,[Name]
		   ,[TargetShapeID]
		   ,[TVD]
		   ,[NSCoordinate]
		   ,[EWCoordinate]
		   ,[PolarDistance]
		   ,[PolarDirection]
		   ,[INCFromLastTarget]
		   ,[AZMFromLastTarget]
		   ,[InclinationAtTarget]
		   ,[AzimuthAtTarget]
		   ,[NumberVertices]
		   ,[Rotation]
		   ,[TargetThickness]
		   ,[DrawingPattern]
		   ,[TargetComment]
		   ,[TargetOffsetXoffset]
		   ,[TargetOffsetYoffset]
		   ,[DiameterOfCircleXoffset]
		   ,[DiameterOfCircleYoffset]
		   ,[Corner1Xofffset]
		   ,[Corner1Yoffset]
		   ,[Corner2Xoffset]
		   ,[Corner2Yoffset]
		   ,[Corner3Xoffset]
		   ,[Corner3Yoffset]
		   ,[Corner4Xoffset]
		   ,[Corner4Yoffset]
		   ,[Corner5Xoffset]
		   ,[Corner5Yoffset]
		   ,[Corner6Xoffset]
		   ,[Corner6Yoffset]
		   ,[Corner7Xoffset]
		   ,[Corner7Yoffset]
		   ,[Corner8Xoffset]
		   ,[Corner8Yoffset]
		   ,[ReferenceOptionID]
		   ,[CreateDate]
		   ,[LastModifyDate]
		   ,[isActive]
		)
		VALUES
		(
			@CurveGroupID
		   ,@TargetName
		   ,@TargetShapeID
		   ,@TVD
		   ,@NSCoordinate
		   ,@EWCoordinate
		   ,@PolarDistance
		   ,@PolarDirection
		   ,@INCFromLastTarget
		   ,@AZMFromLastTarget
		   ,@InclinationAtTarget
		   ,@AzimuthAtTarget
		   ,@NumberVertices
		   ,@Rotation
		   ,@Thickness
		   ,@DrawingPattern
		   ,@TargetComment
		   ,@TargetOffsetXOffset
		   ,@TargetOffsetYOffset
		   ,@DiameterOfCircleXOffset
		   ,@DiameterOfCircleYOffset
		   ,@Corner1XOffset
		   ,@Corner1YOffset
		   ,@Corner2XOffset
		   ,@Corner2YOffset
		   ,@Corner3XOffset
		   ,@Corner3YOffset
		   ,@Corner4XOffset
		   ,@Corner4YOffset
		   ,@Corner5XOffset
		   ,@Corner5YOffset
		   ,@Corner6XOffset
		   ,@Corner6YOffset
		   ,@Corner7XOffset
		   ,@Corner7YOffset
		   ,@Corner8XOffset
		   ,@Corner8YOffset
		   ,@ReferenceOptionID
		   ,GETDATE()
		   ,GETDATE()
		   ,'true'
		)
		SELECT @@IDENTITY
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateTargetDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateTargetDetails]
	@ID int= NULL,
	@TargetName varchar(100)=NULL,
	@CurveGroupID int =NULL,
	@TargetShapeID int =NULL,
	@TVD decimal(18, 4)= NULL,
	@NSCoordinate decimal(18, 4)= NULL,
	@EWCoordinate decimal(18, 4)= NULL,
	@PolarDirection decimal(18, 4)= NULL,
	@PolarDistance decimal(18, 4)= NULL,
	@INCFromLastTarget decimal(18, 4)= NULL,
	@AZMFromLastTarget decimal(18, 4)= NULL,
	@InclinationAtTarget decimal(18, 4) =NULL,
	@AzimuthAtTarget decimal(18, 4) =NULL,
	@NumberVertices decimal(18, 4)= NULL,
	@Rotation decimal(18, 4)= NULL,
	@TargetThickness decimal(18, 2)= NULL,
	@DrawingPattern decimal(18, 2)= NULL,
	@TargetComment nvarchar(150) =NULL,
	@TargetOffsetXoffset decimal(18, 2)= NULL,
	@TargetOffsetYoffset decimal(18, 4),
	@DiameterOfCircleXoffset decimal(18, 4),
	@DiameterOfCircleYoffset decimal(18, 4),
	@Corner1Xofffset decimal(18, 2)= NULL,
	@Corner1Yoffset decimal(18, 2)= NULL,
	@Corner2Xoffset decimal(18, 2)= NULL,
	@Corner2Yoffset decimal(18, 4)= NULL,
	@Corner3Xoffset decimal(18, 4)= NULL,
	@Corner3Yoffset decimal(18, 4)= NULL,
	@Corner4Xoffset decimal(18, 4)= NULL,
	@Corner4Yoffset decimal(18, 2)= NULL,
	@ReferenceOptionID int =1000,
	@isActive bit 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblTarget]
		   SET
		   Name=@TargetName,
	CurveGroupID=@CurveGroupID,
	TargetShapeID=@TargetShapeID,
	TVD=@TVD,
	NSCoordinate=@NSCoordinate,
	EWCoordinate=@EWCoordinate,
	PolarDirection=@PolarDirection,
	PolarDistance=@PolarDistance,
	INCFromLastTarget=@INCFromLastTarget,
	AZMFromLastTarget=@AZMFromLastTarget,
	InclinationAtTarget=@InclinationAtTarget,
	AzimuthAtTarget=@AzimuthAtTarget,
	NumberVertices=@NumberVertices,
	Rotation=@Rotation,
	TargetThickness=@TargetThickness ,
	DrawingPattern=@DrawingPattern,
	TargetComment=@TargetComment,
	TargetOffsetXoffset=@TargetOffsetXoffset,
	TargetOffsetYoffset=@TargetOffsetYoffset,
	DiameterOfCircleXoffset=@DiameterOfCircleXoffset,
	DiameterOfCircleYoffset=@DiameterOfCircleYoffset,
	Corner1Xofffset=@Corner1Xofffset ,
	Corner1Yoffset=@Corner1Yoffset,
	Corner2Xoffset=@Corner2Xoffset ,
	Corner2Yoffset=@Corner2Yoffset,
	Corner3Xoffset=@Corner3Xoffset,
	Corner3Yoffset=@Corner3Yoffset,
	Corner4Xoffset=@Corner4Xoffset,
	Corner4Yoffset=@Corner4Yoffset,
	ReferenceOptionID=@ReferenceOptionID,
	CreateDate=getDate(),
	LastModifyDate=getDate(),
	isActive=@isActive
		 WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblTarget]
				   ([Name],[CurveGroupID] ,
	[TargetShapeID],
	[TVD] ,
	[NSCoordinate] ,
	[EWCoordinate] ,
	[PolarDirection] ,
	[PolarDistance] ,
	[INCFromLastTarget] ,
	[AZMFromLastTarget],
	[InclinationAtTarget] ,
	[AzimuthAtTarget] ,
	[NumberVertices] ,
	[Rotation] ,
	[TargetThickness] ,
	[DrawingPattern] ,
	[TargetComment],
	[TargetOffsetXoffset] ,
	[TargetOffsetYoffset] ,
	[DiameterOfCircleXoffset],
	[DiameterOfCircleYoffset] ,
	[Corner1Xofffset] ,
	[Corner1Yoffset] ,
	[Corner2Xoffset],
	[Corner2Yoffset] ,
	[Corner3Xoffset] ,
	[Corner3Yoffset],
	[Corner4Xoffset],
	[Corner4Yoffset],
	[ReferenceOptionID],
	[CreateDate],
	[LastModifyDate],
	[isActive]
	)
			 VALUES
				   (@TargetName,@CurveGroupID,
	
	@TargetShapeID ,
	@TVD,
	@NSCoordinate ,
	@EWCoordinate ,
	@PolarDirection ,
	@PolarDistance,
	@INCFromLastTarget ,
	@AZMFromLastTarget ,
	@InclinationAtTarget,
	@AzimuthAtTarget,
	@NumberVertices,
	@Rotation ,
	@TargetThickness ,
	@DrawingPattern ,
	@TargetComment,
	@TargetOffsetXoffset ,
	@TargetOffsetYoffset ,
	@DiameterOfCircleXoffset ,
	@DiameterOfCircleYoffset ,
	@Corner1Xofffset ,
	@Corner1Yoffset ,
	@Corner2Xoffset ,
	@Corner2Yoffset ,
	@Corner3Xoffset ,
	@Corner3Yoffset ,
	@Corner4Xoffset ,
	@Corner4Yoffset,
	@ReferenceOptionID,
	getDate(),
	getDate(),
	@isActive
	)
		SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_InsertUpdateTargets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_InsertUpdateTargets]
	@ID int= NULL,
	@CurveGroupID int =NULL,
	@Name nvarchar(50) =NULL,
	@TargetOffsetXoffset decimal(18, 2)= NULL,
	@TargetOffsetYoffset decimal(18, 4),
	@DiameterOfCircleXoffset decimal(18, 4),
	@DiameterOfCircleYoffset decimal(18, 4),
	@Corner1Xofffset decimal(18, 2)= NULL,
	@Corner1Yoffset decimal(18, 2)= NULL,
	@Corner2Xoffset decimal(18, 2)= NULL,
	@Corner2Yoffset decimal(18, 4)= NULL,
	@Corner3Xoffset decimal(18, 4)= NULL,
	@Corner3Yoffset decimal(18, 4)= NULL,
	@Corner4Xoffset decimal(18, 4)= NULL,
	@Corner4Yoffset decimal(18, 2)= NULL,
	@ReferenceOptionID int= NULL,
	@CreateDate datetime =NULL,
	@LastModifyDate datetime =NULL,
	@isActive bit =NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblTarget]
		   SET
	CurveGroupID=@CurveGroupID,
	Name=@Name,
	TargetOffsetXoffset=@TargetOffsetXoffset,
	TargetOffsetYoffset=@TargetOffsetYoffset,
	DiameterOfCircleXoffset=@DiameterOfCircleXoffset,
	DiameterOfCircleYoffset=@DiameterOfCircleYoffset,
	Corner1Xofffset=@Corner1Xofffset ,
	Corner1Yoffset=@Corner1Yoffset,
	Corner2Xoffset=@Corner2Xoffset ,
	Corner2Yoffset=@Corner2Yoffset,
	Corner3Xoffset=@Corner3Xoffset,
	Corner3Yoffset=@Corner3Yoffset,
	Corner4Xoffset=@Corner4Xoffset,
	Corner4Yoffset=@Corner4Yoffset ,
	ReferenceOptionID=@ReferenceOptionID ,
	CreateDate=@CreateDate ,
	LastModifyDate=@LastModifyDate ,
	isActive=@isActive
		 WHERE [ID] = @ID
	END
	ELSE
	BEGIN
		INSERT INTO [RigTrack].[tblTarget]
				   ([CurveGroupID] ,
	[Name],
	[TargetOffsetXoffset] ,
	[TargetOffsetYoffset] ,
	[DiameterOfCircleXoffset],
	[DiameterOfCircleYoffset] ,
	[Corner1Xofffset] ,
	[Corner1Yoffset] ,
	[Corner2Xoffset],
	[Corner2Yoffset] ,
	[Corner3Xoffset] ,
	[Corner3Yoffset],
	[Corner4Xoffset],
	[Corner4Yoffset],
	[ReferenceOptionID],
	[CreateDate],
	[LastModifyDate],
	[isActive])
			 VALUES
				   (@CurveGroupID,
	@Name,
	@TargetOffsetXoffset ,
	@TargetOffsetYoffset ,
	@DiameterOfCircleXoffset ,
	@DiameterOfCircleYoffset ,
	@Corner1Xofffset ,
	@Corner1Yoffset ,
	@Corner2Xoffset ,
	@Corner2Yoffset ,
	@Corner3Xoffset ,
	@Corner3Yoffset ,
	@Corner4Xoffset ,
	@Corner4Yoffset ,
	@ReferenceOptionID ,
	@CreateDate ,
	@LastModifyDate ,
	@isActive)
		SELECT @@Identity
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_PullAllReportValues]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_PullAllReportValues]
(
	@ReportID int = null
)
AS
BEGIN
	IF(ISNULL(@ReportID, 0) = 0)
	BEGIN
		SELECT NULL	
	END
	ELSE
	BEGIN
		SELECT [ID]
			  ,[Name]
			  ,[HeaderComments]
			  ,[CompanyID]
			  ,[CurveGroupID]
			  ,[CurveID]
			  ,[TargetID]
			  ,[MeasuredDepth]
			  ,[Inclination]
			  ,[Azimuth]
			  ,[TrueVerticalDepth]
			  ,[N_SCoordinates]
			  ,[E_WCoordinates]
			  ,[VerticalSection]
			  ,[ClosureDistance]
			  ,[ClosureDirection]
			  ,[DogLegSeverity]
			  ,[CourseLength]
			  ,[WalkRate]
			  ,[BuildRate]
			  ,[ToolFace]
			  ,[Comment]
			  ,[SubseaDepth]
			  ,[Radius]
			  ,[GridX]
			  ,[GridY]
			  ,[Left_Right]
			  ,[Up_Down]
			  ,[FNL_FSL]
			  ,[FEL_FWL]
			  ,[Grouping]
			  ,[BoxedComments]
			  ,[IncludeProjectToBit]
			  ,[ShowProjToTVDInc]
			  ,[ExtraHeaderComments]
			  --,[Logo]
			  ,[InterpolatedReports]
			  ,[EWNSReference]
			  ,[Mode]
			  ,[CreateDate]
			  ,[LastModifyDate]
			  ,[isActive]
		  FROM [RigTrack].[tblReport]
		  WHERE [ID] = @ReportID
  END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SavePlotComments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SavePlotComments]
(
	@CurveGroupID int
	,@Comments nvarchar(500) = ''
)
AS
BEGIN
	UPDATE [RigTrack].[tblCurveGroup]
		SET [PlotComments] = @Comments
		, [LastModifyDate] = GetDate()
	WHERE ID = @CurveGroupID

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchAllCurveGroups]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchAllCurveGroups]
	@CurveGroupID int = null,
	@CurveGroupName nvarchar(25) = null,
	@JobNumber nvarchar(25) = null,
	@JobLocation nvarchar(50) = null,
	@Company nvarchar(50) = null,
	@LeaseWell nvarchar(50) = null,
	@RigName nvarchar(50) = null
AS
BEGIN
	SELECT [ID]
		  ,[CurveGroupName]
		  ,[JobNumber]
		  ,[JobLocation]
		  ,[Company]
		  ,[LeaseWell]
		  ,[RigName]
	FROM [RigTrack].[tblCurveGroup]
	WHERE [ID] = COALESCE(NULLIF(@CurveGroupID, 0), [ID])
	AND [CurveGroupName] = COALESCE(NULLIF(@CurveGroupName,''), [CurveGroupName])
	AND [JobNumber] = COALESCE(NULLIF(@JobNumber,''), [JobNumber])
	AND [JobLocation] = COALESCE(NULLIF(@JobLocation,''), [JobLocation])
	AND [Company] = COALESCE(NULLIF(@Company,''), [Company])
	AND [LeaseWell] = COALESCE(NULLIF(@LeaseWell,''), [LeaseWell])
	AND [RigName] = COALESCE(NULLIF(@RigName,''), [RigName])
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchAllCurveGroups1]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchAllCurveGroups1]
	@CurveGroupID int = null,
	@CurveGroupName nvarchar(25) = null,
	@JobNumber nvarchar(25) = null,
	@JobLocation nvarchar(50) = null,
	@Company nvarchar(50) = null,
	@LeaseWell nvarchar(50) = null,
	@RigName nvarchar(50) = null
AS
BEGIN

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	Declare @CurveGroupNameTable table
	(
		curvegroupname nvarchar(50)
	)
	If(Isnull(@CurveGroupName,'') = '')
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup] where [CurveGroupName] LIKE '%' + @CurveGroupName + '%'
	end

	Declare @JobNumberTable table
	(
		jobnumber nvarchar(50)
	)
	If(Isnull(@JobNumber,'') = '')
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup] where [JobNumber] LIKE '%' + @JobNumber + '%'
	end

	Declare @JobLocationTable table
	(
		joblocation nvarchar(50)
	)
	If(Isnull(@JobLocation,'') = '')
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup] where [JobLocation] LIKE '%' + @JobLocation + '%'
	end

	Declare @CompanyTable table
	(
		company nvarchar(50)
	)
	If(Isnull(@Company,'') = '')
	begin
			insert into @CompanyTable
			(company)
			select [Company] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CompanyTable
			(company)
			select [Company] from [RigTrack].[tblCurveGroup] where [Company] LIKE '%' + @Company + '%'
	end

	Declare @LeaseWellTable table
	(
		leasewell nvarchar(50)
	)
	If(Isnull(@LeaseWell,'') = '')
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup] where [LeaseWell] LIKE '%' + @LeaseWell + '%'
	end

	Declare @RigNameTable table
	(
		rigname nvarchar(50)
	)
	If(Isnull(@RigName,'') = '')
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup] where [RigName] LIKE '%' + @RigName + '%'
	end

	SELECT [ID]
		  ,[CurveGroupName]
		  ,[JobNumber]
		  ,[JobLocation]
		  ,[Company]
		  ,[LeaseWell]
		  ,[RigName]
	FROM [RigTrack].[tblCurveGroup]
	WHERE [ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	AND [CurveGroupName] IN (SELECT curvegroupname FROM @CurveGroupNameTable)
	AND [JobNumber] IN (SELECT jobnumber FROM @JobNumberTable)
	AND [JobLocation] IN (SELECT joblocation FROM @JobLocationTable)
	AND [Company] IN (SELECT company FROM @CompanyTable)
	AND [LeaseWell] IN (SELECT leasewell FROM @LeaseWellTable)
	AND [RigName] IN (SELECT rigname FROM @RigNameTable)
	AND [isActive] = 1
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchAllCurveGroups2]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchAllCurveGroups2]
	@CurveGroupID int = null,
	@CurveGroupName nvarchar(25) = null,
	@JobNumber nvarchar(25) = null,
	@JobLocation nvarchar(50) = null,
	@Company int = 0,
	@LeaseWell nvarchar(50) = null,
	@RigName nvarchar(50) = null,
	@Status bit = null
AS
BEGIN

	Declare @StatusTable table
	(
		status bit
	)
	If(@Status IS NULL)
	begin
			insert into @StatusTable
			(status)
			select [isActive] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @StatusTable
			(status)
			select [isActive] from [RigTrack].[tblCurveGroup] where [isActive] = @Status
	end

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	Declare @CurveGroupNameTable table
	(
		curvegroupname nvarchar(50)
	)
	If(Isnull(@CurveGroupName,'') = '')
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup] where [CurveGroupName] LIKE '%' + @CurveGroupName + '%'
	end

	Declare @JobNumberTable table
	(
		jobnumber nvarchar(50)
	)
	If(Isnull(@JobNumber,'') = '')
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup] where [JobNumber] LIKE '%' + @JobNumber + '%'
	end

	Declare @JobLocationTable table
	(
		joblocation nvarchar(50)
	)
	If(Isnull(@JobLocation,'') = '')
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup] where [JobLocation] LIKE '%' + @JobLocation + '%'
	end

	Declare @CompanyTable table
	(
		company nvarchar(50)
	)
	If(Isnull(@Company,0) = 0)
	begin
			insert into @CompanyTable
			(company)
			select [CompanyID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CompanyTable
			(company)
			select [CompanyID] from [RigTrack].[tblCurveGroup] where [CompanyID] = @Company
	end

	Declare @LeaseWellTable table
	(
		leasewell nvarchar(50)
	)
	If(Isnull(@LeaseWell,'') = '')
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup] where [LeaseWell] LIKE '%' + @LeaseWell + '%'
	end

	Declare @RigNameTable table
	(
		rigname nvarchar(50)
	)
	If(Isnull(@RigName,'') = '')
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup] where [RigName] LIKE '%' + @RigName + '%'
	end

	SELECT cg.[ID]
		  ,cg.[CurveGroupName]
		  ,cg.[JobNumber]
		  ,cg.[JobLocation]
		  ,cg.[CompanyID]
		  ,CompanyTable.CompanyName
		  ,cg.[LeaseWell]
		  ,cg.[RigName]
		  ,cg.[JobStartDate]
		  ,cg.[JobEndDate]
		  , (IIF(cg.[isActive] = 1, 'Open', 'Closed')) AS [Status]
		  , cg.[Comments]
		  , (IIF(cg.[isAttachment] = 1, '<a href="#" onclick="openRadWindow('+CAST(cg.[ID] as nvarchar(10))+')">' + 'File Download' + '</a>', 'NA')) AS [IsAttachment]
	FROM [RigTrack].[tblCurveGroup] cg
	LEFT OUTER JOIN [RigTrack].[tblCompany] CompanyTable
		On CompanyTable.ID = cg.CompanyID
	
	WHERE cg.[ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	AND cg.[CurveGroupName] IN (SELECT curvegroupname FROM @CurveGroupNameTable)
	AND cg.[JobNumber] IN (SELECT jobnumber FROM @JobNumberTable)
	AND cg.[JobLocation] IN (SELECT joblocation FROM @JobLocationTable)
	AND cg.[CompanyID] IN (SELECT company FROM @CompanyTable)
	AND cg.[LeaseWell] IN (SELECT leasewell FROM @LeaseWellTable)
	AND cg.[RigName] IN (SELECT rigname FROM @RigNameTable)
	AND cg.[isActive] IN (SELECT status FROM @StatusTable)

	order by cg.ID desc
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchAllCurveGroups3]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchAllCurveGroups3]
	@CurveGroupID int = null,
	@CurveGroupName nvarchar(25) = null,
	@JobNumber nvarchar(25) = null,
	@JobLocation nvarchar(50) = null,
	@Company int = 0,
	@LeaseWell nvarchar(50) = null,
	@RigName nvarchar(50) = null,
	@Status bit = null
AS
BEGIN

	Declare @StatusTable table
	(
		status bit
	)
	If(@Status IS NULL)
	begin
			insert into @StatusTable
			(status)
			select [isActive] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @StatusTable
			(status)
			select [isActive] from [RigTrack].[tblCurveGroup] where [isActive] = @Status
	end

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	Declare @CurveGroupNameTable table
	(
		curvegroupname nvarchar(50)
	)
	If(Isnull(@CurveGroupName,'') = '')
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup] where [CurveGroupName] LIKE '%' + @CurveGroupName + '%'
	end

	Declare @JobNumberTable table
	(
		jobnumber nvarchar(50)
	)
	If(Isnull(@JobNumber,'') = '')
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobNumberTable
			(jobnumber)
			select [JobNumber] from [RigTrack].[tblCurveGroup] where [JobNumber] LIKE '%' + @JobNumber + '%'
	end

	Declare @JobLocationTable table
	(
		joblocation nvarchar(50)
	)
	If(Isnull(@JobLocation,'') = '')
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @JobLocationTable
			(joblocation)
			select [JobLocation] from [RigTrack].[tblCurveGroup] where [JobLocation] LIKE '%' + @JobLocation + '%'
	end

	Declare @CompanyTable table
	(
		company nvarchar(50)
	)
	If(Isnull(@Company,0) = 0)
	begin
			insert into @CompanyTable
			(company)
			select [CompanyID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CompanyTable
			(company)
			select [CompanyID] from [RigTrack].[tblCurveGroup] where [CompanyID] = @Company
	end

	Declare @LeaseWellTable table
	(
		leasewell nvarchar(50)
	)
	If(Isnull(@LeaseWell,'') = '')
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @LeaseWellTable
			(leasewell)
			select [LeaseWell] from [RigTrack].[tblCurveGroup] where [LeaseWell] LIKE '%' + @LeaseWell + '%'
	end

	Declare @RigNameTable table
	(
		rigname nvarchar(50)
	)
	If(Isnull(@RigName,'') = '')
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @RigNameTable
			(rigname)
			select [RigName] from [RigTrack].[tblCurveGroup] where [RigName] LIKE '%' + @RigName + '%'
	end

	SELECT cg.[ID]
		  ,cg.[CurveGroupName]
		  ,cg.[JobNumber]
		  ,cg.[JobLocation]
		  ,cg.[CompanyID]
		  ,CompanyTable.CompanyName
		  ,cg.[LeaseWell]
		  ,cg.[RigName]
		  ,cg.[JobStartDate]
		  ,cg.[JobEndDate]
		  , (IIF(cg.[isActive] = 1, 'Open', 'Closed')) AS [Status]
		  , cg.[Comments]
		  , (IIF(CompanyTable.[isAttachment] = 1, '<a href="#" onclick="openRadWindow('+CAST(CompanyTable.[ID] as nvarchar(10))+')">' + 'File Download' + '</a>', 'NA')) AS [IsAttachment]
	FROM [RigTrack].[tblCurveGroup] cg
	LEFT OUTER JOIN [RigTrack].[tblCompany] CompanyTable
		On CompanyTable.ID = cg.CompanyID
	
	WHERE cg.[ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	AND cg.[CurveGroupName] IN (SELECT curvegroupname FROM @CurveGroupNameTable)
	AND cg.[JobNumber] IN (SELECT jobnumber FROM @JobNumberTable)
	AND cg.[JobLocation] IN (SELECT joblocation FROM @JobLocationTable)
	AND cg.[CompanyID] IN (SELECT company FROM @CompanyTable)
	AND cg.[LeaseWell] IN (SELECT leasewell FROM @LeaseWellTable)
	AND cg.[RigName] IN (SELECT rigname FROM @RigNameTable)
	AND cg.[isActive] IN (SELECT status FROM @StatusTable)
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchCompany]
	@CompanyName nvarchar(120) = null,
	@City nvarchar(80) = null,
	@StateID int = null
AS
BEGIN
	SELECT 
		c.ID
	   ,c.CompanyName
       ,c.CompanyAddress1
	   ,c.CompanyAddress2
	   ,c.CompanyContactFirstName
	   ,c.CompanyContactLastName
	   ,c.ContactPhone
	   ,c.ContactEmail
	   ,c.City
	   ,c.StateID
	   ,s.Name as StateName
	   ,c.CountryID
	   ,cn.Name as CountryName
	   ,c.Zip
	   ,c.isAttachment
	   ,c.CreateDate
	   ,c.isActive
	   , c.[CompanyName] as 'Name'
	FROM [RigTrack].[tblCompany] c
	LEFT OUTER JOIN [RigTrack].[tlkpState] s
	ON c.StateID = s.ID
	LEFT OUTER JOIN [RigTrack].[tlkpCountry] cn
	ON c.CountryID = cn.ID
	WHERE c.CompanyName like COALESCE('%' + @CompanyName + '%', c.CompanyName)
	AND c.City like COALESCE('%' + @City + '%', c.City)
	AND c.StateID = COALESCE(NULLIF(@StateID,''), c.StateID)
	ORDER BY c.CreateDate DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchManageJobsCurveGroups]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchManageJobsCurveGroups]
	@CurveGroupName nvarchar(25) = null,
	@Company  int = null,
	@LeaseWell  nvarchar(50) = null,
	@JobLocation nvarchar(50) = null,
	@RigName nvarchar(50) = null,
	--@NSOffset decimal(18, 2) = null,
	@JobNumber  nvarchar(25) = null,
 --   @Grid nvarchar(50) = null,
	--@RKB nvarchar(50) = null,
	--@Country int = null,
	--@State int = null,
	--@Method int = null,
	--@Convert bit = null,
	--@MetersFeet int = null,
	--@DogLeg int = null,
	--@GLmsl int = null,
	--@Declination decimal(18, 2) = null,
	--@OutPutDirection int = null,
	--@InPutDirection int = null,
	--@VSection int = null,
	--@EWOffset decimal(18, 2) = null
	@JobStartDate DateTime = null,
	@JobStartDateEnd DateTime = null

AS
BEGIN

	SELECT C.ID
		  ,C.CurveGroupName
		  ,Company.CompanyName
		  ,C.LeaseWell
		  ,C.JobLocation
		  ,C.RigName
		  ,C.NSOffset
		  ,C.JobNumber
		  ,C.Grid
		  ,C.RKB
		  ,CO.Name as 'CountryName'
		  ,S.Name as 'StateName'
		  ,MethodC.Name as 'MethodName'
		  ,MetersFeet.Name 'MetersFeet'
		  ,Dogleg.Name as 'DogLegName'
		  ,Gl.Name as 'GLName'
		  ,C.Declination
		  ,ODirection.Name as 'OutPutName'
		  ,IDirection.Name as 'InputName'
		  ,VSection.Name as 'VSectionName'
		  ,C.EWOffset
		  ,C.JobStartDate
		  ,C.JobEndDate
		  ,C.IsActive
		  ,C.primaryLatLong

		 
	FROM [RigTrack].[tblCurveGroup] C

	LEFT OUTER JOIN [RigTrack].[tlkpCountry] CO
	on CO.ID = C.CountryID

	LEFT OUTER JOIN [RigTrack].[tlkpState] S
	on S.ID = C.StateID

	LEFT OUTER JOIN [RigTrack].[tlkpMethodOfCalculation] MethodC
	on MethodC.ID = C.MethodOfCalculationID

	LEFT OUTER JOIN [RigTrack].[tlkpMeasurementUnits] MetersFeet
	on MetersFeet.ID = C.MeasurementUnitsID

	LEFT OUTER JOIN [RigTrack].[tlkpDogLegSeverity] Dogleg
	on Dogleg.ID = C.DogLegSeverityID

	LEFT OUTER JOIN [RigTrack].[tlkpGLMSL] GL
	on GL.ID = C.GLorMSLID

	LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] ODirection
	on ODirection.ID = C.OutputDirectionID

		LEFT OUTER JOIN [RigTrack].[tlkpInputOutputDirection] IDirection
	on IDirection.ID = C.InputDirectionID

	LEFT OUTER JOIN [RigTrack].[tlkpVerticalSectionRef] VSection
	on VSection.ID = C.VerticalSectionReferenceID

	LEFT OUTER JOIN [RigTrack].[tblCompany] Company
	on Company.ID = C.CompanyID

	where C.CurveGroupName like COALESCE('%' + @CurveGroupName + '%', C.CurveGroupName)

	--and C.Company like COALESCE('%' + @Company + '%', C.Company)

	and C.CompanyID = COALESCE(NULLIF(@Company, ''), C.CompanyID)

	and C.LeaseWell like COALESCE('%' + @LeaseWell + '%', C.LeaseWell)

	and C.JobLocation like COALESCE('%' + @JobLocation + '%', C.JobLocation )

	and C.RigName like COALESCE('%' + @RigName + '%', C.RigName )

	--and C.NSOffset= COALESCE(@NSOffset, C.NSOffset)

	and C.JobNumber like COALESCE('%' + @JobNumber + '%', C.JobNumber)

	--and C.JobStartDate like COALESCE('%' + @JobStartDate + '%', C.JobStartDate)

	--and C.JobEndDate like COALESCE('%' + @JobEndDate + '%', C.JobEndDate )

	and C.JobStartDate  between @JobStartDate and @JobStartDateEnd

	
	
	


	--and C.Grid like COALESCE('%' + @Grid + '%', C.Grid)

	--and C.RKB like COALESCE('%' + @RKB + '%', C.RKB )

	--and C.CountryID= COALESCE(@Country, C.CountryID)

	--and C.StateID= COALESCE(@State, C.StateID)

	--and C.MethodOfCalculationID = COALESCE(@Method, C.MethodOfCalculationID)

	--and C.UnitsConvert = COALESCE(@Convert, C.UnitsConvert)

	--and C.MeasurementUnitsID= COALESCE(@MetersFeet, C.MeasurementUnitsID)

	--and C.DogLegSeverityID = COALESCE(@DogLeg, C.DogLegSeverityID)

	--and C.GLorMSLID = COALESCE(@Glmsl, C.GLorMSLID)

	--and C.Declination = COALESCE(@Declination, C.Declination)

	--and C.OutputDirectionID = COALESCE(@OutPutDirection, C.OutputDirectionID)

	--and C.InputDirectionID = COALESCE(@InPutDirection, C.InputDirectionID)

	--and C.VerticalSectionReferenceID = COALESCE(@VSection, C.VerticalSectionReferenceID)

	--and C.EWOffset = COALESCE(@EWOffset, C.EWOffset)

	--and c.isActive = 1


	ORDER BY C.JobStartDate DESC



	
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchReports]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchReports]
(
	@CurveGroupID int = null,
	@CurveGroupName nvarchar(25) = null,
	@TargetID int = null,
	@TargetName nvarchar(50) = null,
	@CurveID int = null,
	@CurveName nvarchar(50) = null,
	@CompanyID int = null
	--@Status bit = null
)
AS
BEGIN

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	Declare @CurveGroupNameTable table
	(
		curvegroupname nvarchar(50)
	)
	If(Isnull(@CurveGroupName,'') = '')
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupNameTable
			(curvegroupname)
			select [CurveGroupName] from [RigTrack].[tblCurveGroup] where [CurveGroupName] LIKE '%' + @CurveGroupName + '%'
	end

	Declare @TargetIDTable table
	(
		targetid int
	)
	If(Isnull(@TargetID,0) = 0)
	begin
			insert into @TargetIDTable
			(targetid)
			select [ID] from [RigTrack].[tblTarget]
	end
	else
	begin
			insert into @TargetIDTable
			(targetid)
			select [ID] from [RigTrack].[tblTarget] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@TargetID AS nvarchar(25)) + '%'
	end

	Declare @TargetNameTable table
	(
		targetname nvarchar(50)
	)
	If(Isnull(@TargetName,'') = '')
	begin
			insert into @TargetNameTable
			(targetname)
			select [Name] from [RigTrack].[tblTarget]
	end
	else
	begin
			insert into @TargetNameTable
			(targetname)
			select [Name] from [RigTrack].[tblTarget] where [Name] LIKE '%' + @TargetName + '%'
	end

	Declare @CurveIDTable table
	(
		curveid int
	)
	If(Isnull(@CurveID,0) = 0)
	begin
			insert into @CurveIDTable
			(curveid)
			select [ID] from [RigTrack].[tblCurve]
	end
	else
	begin
			insert into @CurveIDTable
			(curveid)
			select [ID] from [RigTrack].[tblCurve] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveID AS nvarchar(25)) + '%'
	end

	Declare @CurveNameTable table
	(
		curvename nvarchar(50)
	)
	If(Isnull(@CurveName,'') = '')
	begin
			insert into @CurveNameTable
			(curvename)
			select [Name] from [RigTrack].[tblCurve]
	end
	else
	begin
			insert into @CurveNameTable
			(curvename)
			select [Name] from [RigTrack].[tblCurve] where [Name] LIKE '%' + @CurveName + '%'
	end


	Declare @CompanyIDTable table
	(
		companyid int
	)
	If(Isnull(@CompanyID,0) = 0)
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany]
	end
	else
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany] where [ID] = @CompanyID
	end

	SELECT r.[ID]
		  ,r.[Name] AS [ReportName]
		  ,cg.[ID] AS [CurveGroupID]
		  ,cg.[CurveGroupName]
		  ,c.[ID] AS [CurveID]
		  ,c.[Name] AS [CurveName]
		  ,t.[ID] AS [TargetID]
		  ,t.[Name] AS [TargetName]
		  ,co.[CompanyName] AS [Company]
		  ,r.[CreateDate] AS [ReportCreateDate]
	FROM [RigTrack].[tblReport] r
	INNER JOIN [RigTrack].[tblCurveGroup] cg ON r.[CurveGroupID] = cg.[ID]
	INNER JOIN [RigTrack].[tblCurve] c ON r.[CurveID] = c.[ID]
	INNER JOIN [RigTrack].[tblTarget] t ON r.[TargetID] = t.[ID]
	INNER JOIN [RigTrack].[tblCompany] co ON co.[ID] = cg.[CompanyID]
	WHERE cg.[ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	AND cg.[CurveGroupName] IN (SELECT curvegroupname FROM @CurveGroupNameTable)
	AND t.[ID] IN (SELECT targetid FROM @TargetIDTable)
	AND t.[Name] IN (SELECT targetname FROM @TargetNameTable)
	AND c.[ID] IN (SELECT curveid FROM @CurveIDTable)
	AND c.[Name] IN (SELECT curvename FROM @CurveNameTable)
	AND cg.[CompanyID] IN (SELECT companyid FROM @CompanyIDTable)
	ORDER BY r.[ID] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_SearchTargets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_SearchTargets]
(
	@CurveGroupID int = null
)
AS
BEGIN

	SELECT t.[ID] AS [TargetID]
		  ,t.[Name] AS [TargetName]
		  ,ts.[Name] AS [TargetShape]
		  ,t.[TVD] AS [TVD]
		  ,t.[NSCoordinate] AS [NSCoordinate]
		  ,t.[EWCoordinate] AS [EWCoordinate]
		  ,t.[PolarDistance] AS [PolarDistance]
		  ,t.[PolarDirection] AS [PolarDirection]
		  ,t.[INCFromLastTarget] AS [INCFromLastTarget]
		  ,t.[AZMFromLastTarget] AS [AZMFromLastTarget]
		  ,t.[InclinationAtTarget] AS [InclinationAtTarget]
		  ,t.[AzimuthAtTarget] AS [AzimuthAtTarget]
		  ,t.[NumberVertices] AS [NumberOfVertices]
		  ,t.[Rotation] AS [Rotation]
		  ,t.[TargetThickness] AS [TargetThickness]
		  ,dp.[Name] AS [DrawingPattern]
	FROM [RigTrack].[tblTarget] t
	INNER JOIN [RigTrack].[tlkpTargetShape] ts ON ts.[ID] = t.[TargetShapeID]
	INNER JOIN [RigTrack].[tlkpDrawingPattern] dp ON dp.[ID] = t.[DrawingPattern]
	WHERE t.[CurveGroupID] = @CurveGroupID
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Warren
-- Create date: 07/24/2016
-- Description:	Update Curve (there should be no need to Insert Curve)
-- =============================================
CREATE PROCEDURE [RigTrack].[sp_UpdateCurve] 
(
	  @ID int = 0
	, @CurveGroupID int = 0
	, @Number int = 0
	, @Name nvarchar(50) = ''
	, @CurveTypeID int = 0
	, @NorthOffset decimal(18,2) = 0
	, @EastOffset decimal(18,2) = 0
	, @VSDirection decimal(18,2) = 0
	, @RKBElevation decimal(18,2) = 0
	, @isActive bit = 1
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCurve]
		   SET [CurveGroupID] = @CurveGroupID
			  ,[Number] = @Number
			  ,[Name] = @Name
			  ,[CurveTypeID] = @CurveTypeID
			  ,[NorthOffset] = @NorthOffset
			  ,[EastOffset] = @EastOffset
			  ,[VSDirection] = @VSDirection
			  ,[RKBElevation] = @RKBElevation
			  ,[LastModifyDate] = GetDate()
			  ,[isActive] = @isActive
		 WHERE [ID] = @ID
		 SELECT 1 --Success
	END
	ELSE
	BEGIN --Code should never be called
		INSERT INTO [RigTrack].[tblCurve]
				   ([CurveGroupID]
				   ,[Number]
				   ,[Name]
				   ,[CurveTypeID]
				   ,[NorthOffset]
				   ,[EastOffset]
				   ,[VSDirection]
				   ,[RKBElevation]
				   ,[CreateDate]
				   ,[LastModifyDate]
				   ,[isActive])
			 VALUES
				   (@CurveGroupID
				   ,@Number
				   ,@Name
				   ,@CurveTypeID
				   ,@NorthOffset
				   ,@EastOffset
				   ,@VSDirection
				   ,@RKBElevation
				   ,GetDate()
				   ,GetDate()
				   ,@isActive)
		SELECT @@Identity
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateCurveColor]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_UpdateCurveColor]
	@ID int = null,
	@Color nvarchar(25) = null
AS
BEGIN
	IF(@ID > 0)
	BEGIN
		UPDATE [RigTrack].[tblCurve]
		SET [Color] = @Color
		WHERE [ID] = @ID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateCurveFromSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_UpdateCurveFromSurvey]
(
	@ID int = null
	,@NorthOffset decimal(18,2) = null
	,@EastOffset decimal(18,2) = null
	,@VSDirection decimal(18,2) = null
	,@RKBElevation decimal(18,2) = null
)
AS
BEGIN
	BEGIN
		UPDATE [RigTrack].[tblCurve]
		SET
		[NorthOffset] = @NorthOffset
		,[EastOffset] = @EastOffset
		,[VSDirection] = @VSDirection
		,[RKBElevation] = @RKBElevation
		,[LastModifyDate] = GetDate()
		WHERE [ID] = @ID
	END

END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateCurveGroupCalculations]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_UpdateCurveGroupCalculations] 
(
	@CurveGroupID int = 0
	,@MethodOfCalculationID int = 0
	,@MeasurementUnitsID int = 0 
	,@UnitsConverted bit = 0
	,@InputDirectionID int = 0 
	,@OutputDirectionID int = 0 
	,@DogLegID int = 0 
	,@VerticalSectionID int = 0 
	,@NSOffset decimal(18,2) = 0
	,@EWOffset decimal(18,2) = 0
)
AS

BEGIN

	SET NOCOUNT ON; 

	BEGIN
		UPDATE [RigTrack].[tblCurveGroup]
		SET
			[MethodOfCalculationID] = @MethodOfCalculationID
			,[MeasurementUnitsID] = @MeasurementUnitsID
			,[UnitsConvert] = @UnitsConverted
			,[InputDirectionID] = @InputDirectionID
			,[OutputDirectionID] = @OutputDirectionID
			,[DogLegSeverityID] = @DogLegID
			,[VerticalSectionReferenceID] = @VerticalSectionID
			,[NSOffset] = @NSOffset
			,[EWOffset] = @EWOffset
			,[LastModifyDate] = GetDate()
	WHERE [ID] = @CurveGroupID
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateCurveLocationDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [RigTrack].[sp_UpdateCurveLocationDetails]
(
	@ID int
	,@LocationID int 
	,@BitToSensor decimal(18,2)
	,@ListDistanceBool bit 
	,@ComparisonCurve int 
	,@AtHSide	decimal(18,2)
	,@TVDComp	decimal(18,2)
)
AS

BEGIN

	SET NOCOUNT ON;
	BEGIN
		UPDATE [RigTrack].[tblCurve]
		SET
		[LocationID] = @LocationID
		,[BitToSensor] = @BitToSensor
		,[ListDistanceBool] = @ListDistanceBool
		,[ComparisonCurve] = @ComparisonCurve
		,[AtHSide] = @AtHSide
		,[TVDComp] = @TVDComp
		,[LastModifyDate] = GetDate()
		WHERE [ID] = @ID 
	END
END

GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateSurveyComments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_UpdateSurveyComments]
(
	@ID int = null
	,@Comments nvarchar(150) = null
)
AS 
BEGIN

	BEGIN 
	UPDATE [RigTrack].[tblSurvey]
	SET 
	[SurveyComment] = @Comments
	WHERE [ID] = @ID

	END

END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_UpdateSurveyName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_UpdateSurveyName]
(
	@ID int = null
	,@Name nvarchar(50) = null

)
AS
BEGIN
	BEGIN 
		UPDATE [RigTrack].[tblSurvey]
		SET 
		[Name] = @Name
		WHERE [ID] = @ID
	END
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_ViewCompanyReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_ViewCompanyReport]
(
	@CompanyID int = null,
	@StateID int = null,
	@StartDate datetime = '1753-01-01 00:00:00.000',
	@EndDate datetime = '9999-12-31 23:59:59.997',
	@Status bit = 1
)
AS
BEGIN

	Declare @CompanyIDTable table
	(
		companyid int
	)
	If(Isnull(@CompanyID,0) = 0)
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany]
	end
	else
	begin
			insert into @CompanyIDTable
			(companyid)
			select [ID] from [RigTrack].[tblCompany] where [ID] = @CompanyID
	end

	Declare @StateIDTable table
	(
		stateid int
	)
	If(Isnull(@StateID,0) = 0)
	begin
			insert into @StateIDTable
			(stateid)
			select [ID] from [RigTrack].[tlkpState]
			insert into @StateIDTable
			(stateid)
			select 0
	end
	else
	begin
			insert into @StateIDTable
			(stateid)
			select [ID] from [RigTrack].[tlkpState] where [ID] = @StateID
	end

		SELECT DISTINCT		  com.[CompanyName] AS [Company]
							, st.[Name] + '/' + co.[Name] AS [State/Country]
							, cg.[JobStartDate] AS [Job Start Date]
							, cg.[JobEndDate] as [Job End Date]
							, cg.[CurveGroupName]
							, cg.[ID]
							, cg.[Comments]
							--, s.[MD], s.[INC], s.[Azimuth], s.[TVD], s.[NS], s.[EW], s.[VerticalSection], s.[ClosureDistance], s.[ClosureDirection], s.[DLS], s.[CL], s.[WR], s.[BR], s.[TFO], s.[SurveyComment], s.[SubseaTVD]
							, cg.[JobNumber]
							, cg.[LeaseWell]
							, cg.[JobLocation]
							, cg.[RigName]
							, cg.[RKB]
							, gl.[Name] AS [GLorMSL]
							, cg.[Declination]
							, cg.[Grid]
		FROM			[RigTrack].[tblSurvey] s
		INNER JOIN		[RigTrack].[tblCurve] c			ON c.[ID] = s.[CurveID]
		INNER JOIN		[RigTrack].[tblCurveGroup] cg	ON cg.[ID] = c.[CurveGroupID]
		INNER JOIN		[RigTrack].[tblTarget] t		ON t.[CurveGroupID] = cg.[ID]
		INNER JOIN		[RigTrack].[tblCompany] com		ON com.[ID] = cg.[CompanyID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] co ON co.[ID] = cg.[CountryID]
		LEFT OUTER JOIN		  [RigTrack].[tlkpState] st ON st.[ID] = cg.[StateID]
		WHERE			cg.[CompanyID] IN (SELECT companyid FROM @CompanyIDTable)
		AND				cg.[StateID] IN (SELECT stateid FROM @StateIDTable)
		AND				cg.[JobStartDate] BETWEEN @StartDate AND @EndDate
		AND				cg.[isActive] = @Status
		ORDER BY cg.[JobStartDate] DESC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_ViewDefaultJobReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_ViewDefaultJobReport]
(
	@CurveGroupID int = null
)
AS
BEGIN

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	SELECT			DISTINCT s.[CurveID], s.[MD], s.[INC], s.[Azimuth], s.[TVD], s.[NS], s.[EW], s.[VerticalSection], s.[ClosureDistance], s.[ClosureDirection], s.[DLS], s.[CL], s.[WR], s.[BR], s.[TFO], s.[SurveyComment], s.[SubseaTVD]
						, cg.[JobNumber]
						, cm.[CompanyName]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, st.[Name] + '/' + co.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, c.[Name] AS [CurveName]
						, mu.[Name] as [MeasurementUnit] 
	FROM			[RigTrack].[tblSurvey] s
	INNER JOIN		[RigTrack].[tblCurve] c			ON c.[ID] = s.[CurveID]
	INNER JOIN		[RigTrack].[tblCurveGroup] cg	ON cg.[ID] = c.[CurveGroupID]
	INNER JOIN		[RigTrack].[tblTarget] t		ON t.[CurveGroupID] = cg.[ID]
	INNER JOIN		[RigTrack].[tblCompany] cm		ON cg.[CompanyID] = cm.[ID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] co ON co.[ID] = cg.[CountryID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpState] st ON st.[ID] = cg.[StateID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpMeasurementUnits] mu on mu.[ID] = cg.[MeasurementUnitsID]
	WHERE			cg.[ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	ORDER BY s.[CurveID], s.[MD] ASC
END
GO
/****** Object:  StoredProcedure [RigTrack].[sp_ViewDefaultReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [RigTrack].[sp_ViewDefaultReport]
(
	@CurveGroupID int = null,
	@TargetID int = null
)
AS
BEGIN

	Declare @CurveGroupIDTable table
	(
		curvegroupid int
	)
	If(Isnull(@CurveGroupID,0) = 0)
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup]
	end
	else
	begin
			insert into @CurveGroupIDTable
			(curvegroupid)
			select [ID] from [RigTrack].[tblCurveGroup] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@CurveGroupID AS nvarchar(25)) + '%'
	end

	Declare @TargetIDTable table
	(
		targetid int
	)
	If(Isnull(@TargetID,0) = 0)
	begin
			insert into @TargetIDTable
			(targetid)
			select [ID] from [RigTrack].[tblTarget]
	end
	else
	begin
			insert into @TargetIDTable
			(targetid)
			select [ID] from [RigTrack].[tblTarget] where CAST([ID] AS nvarchar(25)) LIKE '%' + CAST(@TargetID AS nvarchar(25)) + '%'
	end


	SELECT			s.[MD], s.[INC], s.[Azimuth], s.[TVD], s.[NS], s.[EW], s.[VerticalSection], s.[ClosureDistance], s.[ClosureDirection], s.[DLS], s.[CL], s.[WR], s.[BR], s.[TFO], s.[SurveyComment], s.[SubseaTVD]
						, cg.[JobNumber]
						, cm.[CompanyName]
						, cg.[LeaseWell]
						, cg.[JobLocation]
						, cg.[RigName]
						, cg.[RKB]
						, gl.[Name] AS [GLorMSL]
						, st.[Name] + '/' + co.[Name] AS [StateCountry]
						, cg.[Declination]
						, cg.[Grid]
						, cg.[CurveGroupName]
						, GetDate() AS [CurrentDateTime]
						, '' AS [CurveName]
	FROM			[RigTrack].[tblSurvey] s
	INNER JOIN		[RigTrack].[tblCurve] c			ON c.[ID] = s.[CurveID]
	INNER JOIN		[RigTrack].[tblCurveGroup] cg	ON cg.[ID] = c.[CurveGroupID]
	INNER JOIN		[RigTrack].[tblTarget] t		ON t.[CurveGroupID] = cg.[ID]
	INNER JOIN		[RigTrack].[tblCompany] cm		ON cg.[CompanyID] = cm.[ID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpGLMSL] gl ON gl.[ID] = cg.[GLorMSLID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpCountry] co ON co.[ID] = cg.[CountryID]
	LEFT OUTER JOIN		  [RigTrack].[tlkpState] st ON st.[ID] = cg.[StateID]
	WHERE			cg.[ID] IN (SELECT curvegroupid FROM @CurveGroupIDTable)
	AND				t.[ID] IN (SELECT targetid FROM @TargetIDTable)
END
GO
/****** Object:  Table [dbo].[AccessTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccessTypes](
	[accessTypeID] [int] IDENTITY(1,1) NOT NULL,
	[accessType] [varchar](50) NOT NULL,
 CONSTRAINT [AccessTypes_PK] PRIMARY KEY CLUSTERED 
(
	[accessTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL CONSTRAINT [DF_Accounts_AccountsBitActive]  DEFAULT ((1)),
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[accountTypeID] [int] NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](100) NULL,
	[secondaryAddress2] [nvarchar](100) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[accountTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accountTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_accountTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[actionName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[actionName](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [int] NOT NULL CONSTRAINT [DF_actionName_parentId]  DEFAULT ((0)),
	[type] [varchar](32) NOT NULL,
	[name] [varchar](64) NOT NULL,
	[description] [varchar](255) NOT NULL,
	[saved] [datetime] NOT NULL CONSTRAINT [DF_actionName_saved]  DEFAULT (getdate()),
 CONSTRAINT [PK_actionName] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[adminService]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adminService](
	[adminServiceID] [int] IDENTITY(1,1) NOT NULL,
	[serviceName] [varchar](100) NULL,
	[serviceOverview] [varchar](1000) NULL,
	[serviceBenefits] [varchar](1000) NULL,
	[serviceTechDescription] [varchar](1000) NULL,
	[userID] [int] NULL,
 CONSTRAINT [PK_adminService] PRIMARY KEY CLUSTERED 
(
	[adminServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[assetReadings]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assetReadings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assetAttribID] [int] NOT NULL,
	[datetime] [datetime] NOT NULL,
	[value] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_assetReadings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [uc_assetReadingDate] UNIQUE NONCLUSTERED 
(
	[assetAttribID] ASC,
	[datetime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[assets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[assets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[assetlabelID] [int] NULL,
	[label] [varchar](50) NULL,
	[parentAssetID] [int] NULL,
	[country] [varchar](50) NULL,
	[address1] [varchar](50) NULL,
	[address2] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[postalCode] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[fax] [varchar](50) NULL,
	[status] [varchar](50) NULL,
	[adminUserID] [int] NULL,
	[managerUserID] [int] NULL,
	[subAsset1] [varchar](50) NULL,
	[subAsset2] [varchar](50) NULL,
	[subAsset3] [varchar](50) NULL,
	[subAsset4] [varchar](50) NULL,
	[subAssetSDP] [varchar](50) NULL,
	[subAssetOther] [varchar](50) NULL,
	[primFirstName] [varchar](50) NULL,
	[primLastName] [varchar](50) NULL,
	[primCountry] [varchar](50) NULL,
	[primAddress1] [varchar](50) NULL,
	[primAddress2] [varchar](50) NULL,
	[primCity] [varchar](50) NULL,
	[primState] [varchar](50) NULL,
	[primPostalCode] [varchar](50) NULL,
	[primPhone] [varchar](50) NULL,
	[primFax] [varchar](50) NULL,
	[primEmail] [varchar](50) NULL,
	[secFirstName] [varchar](50) NULL,
	[secLastName] [varchar](50) NULL,
	[secCountry] [varchar](50) NULL,
	[secAddress1] [varchar](50) NULL,
	[secAddress2] [varchar](50) NULL,
	[secCity] [varchar](50) NULL,
	[secState] [varchar](50) NULL,
	[secPostalCode] [varchar](50) NULL,
	[secPhone] [varchar](50) NULL,
	[secFax] [varchar](50) NULL,
	[secEmail] [varchar](50) NULL,
	[serialNumber] [varchar](50) NULL,
	[internalReferenceNumber] [varchar](50) NULL,
	[model] [varchar](50) NULL,
	[make] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[meterParentAssetID] [int] NULL,
	[meterParentSDPID] [int] NULL,
	[meterParentAccountID] [int] NULL,
	[bitActive] [bit] NULL,
	[primLatLong] [nvarchar](100) NULL,
	[primLatLongAccuracy] [nvarchar](50) NULL,
	[secLatLong] [nvarchar](100) NULL,
	[secLatLongAccuracy] [nvarchar](50) NULL,
	[bitSecSamePrimAddress] [bit] NULL,
 CONSTRAINT [PK__assets_3__3214EC274BAC3F29] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[assetsAttributes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[assetsAttributes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[assetID] [int] NOT NULL,
	[attributeID] [int] NOT NULL,
 CONSTRAINT [PK__assetsAt__3214EC27412EB0B6] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[attributes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[attributes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[upperControlLimit] [nvarchar](50) NULL,
	[lowerControlLimit] [nvarchar](50) NULL,
	[interval] [int] NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_attributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[category]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[clientAssets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clientAssets](
	[clientAssetID] [int] IDENTITY(1,1) NOT NULL,
	[subscriptionID] [int] NULL,
	[clientAssetName] [varchar](20) NULL,
	[active] [bit] NULL CONSTRAINT [DF_clientAssets_active]  DEFAULT ((0)),
	[rank] [int] NULL CONSTRAINT [DF_clientAssets_ranking]  DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Collectors]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collectors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL CONSTRAINT [DF_Collectors_CollectorBitActive]  DEFAULT ((1)),
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](100) NULL,
	[secondaryAddress2] [nvarchar](100) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Collectors] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[constantsBoolean]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[constantsBoolean](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[formLabel_id] [int] NOT NULL,
	[constantName] [varchar](255) NULL,
	[constantValue] [varchar](255) NULL,
	[saved] [datetime] NOT NULL CONSTRAINT [DF_constantsBoolean_saved]  DEFAULT (getdate())
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConsumableCategory]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConsumableCategory](
	[ConCatID] [int] IDENTITY(1,1) NOT NULL,
	[ConCatName] [varchar](200) NULL,
	[Description] [varchar](200) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Consumables]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Consumables](
	[ConID] [int] IDENTITY(1,1) NOT NULL,
	[ConName] [varchar](200) NULL,
	[ConCost] [decimal](18, 2) NULL,
	[ConCatID] [int] NULL,
	[ConStatus] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[countryCode]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countryCode](
	[code] [nvarchar](2) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DailyRunReportDocs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyRunReportDocs](
	[runrptdocid] [int] IDENTITY(1,1) NOT NULL,
	[DocumentID] [int] NULL,
	[UserID] [int] NULL,
	[UploadedDate] [datetime] NULL,
	[runid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[daIntervalMeterData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daIntervalMeterData](
	[IntervalMeterDataId] [int] IDENTITY(1,1) NOT NULL,
	[MeterId] [int] NULL,
	[TimeStamp] [datetime] NULL,
	[MeterData] [decimal](18, 4) NULL,
	[Version] [int] NULL,
	[MeterDirectionId] [int] NULL,
	[TimeChanged] [bit] NULL,
	[ClockSetBackward] [bit] NULL,
	[LongInterval] [bit] NULL,
	[ClockSetForward] [bit] NULL,
	[PartialInterval] [bit] NULL,
	[InvalidTime] [bit] NULL,
	[SkippedInterval] [bit] NULL,
	[CompleteOutage] [bit] NULL,
	[PulseOverflow] [bit] NULL,
	[TestMode] [bit] NULL,
	[Tamper] [bit] NULL,
	[PartialOutage] [bit] NULL,
	[SuspectedOutage] [bit] NULL,
	[Restoration] [bit] NULL,
	[DST] [bit] NULL,
	[InvalidValue] [bit] NULL,
	[MeterSerialNumber] [varchar](50) NULL,
	[Source] [varchar](200) NULL CONSTRAINT [DF_daIntervalMeterData_Source]  DEFAULT ('IMPORTED'),
	[ActualMeterRead] [decimal](18, 4) NULL,
	[EstimationInfo] [varchar](200) NULL,
 CONSTRAINT [PK_daInertvalMeterData] PRIMARY KEY CLUSTERED 
(
	[IntervalMeterDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daIntervalMeterDataRevs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daIntervalMeterDataRevs](
	[IntervalMeterDataId] [int] IDENTITY(1,1) NOT NULL,
	[MeterId] [int] NULL,
	[TimeStamp] [datetime] NULL,
	[MeterData] [decimal](18, 4) NULL,
	[Version] [int] NULL,
	[MeterDirectionId] [int] NULL,
	[TimeChanged] [bit] NULL,
	[ClockSetBackward] [bit] NULL,
	[LongInterval] [bit] NULL,
	[ClockSetForward] [bit] NULL,
	[PartialInterval] [bit] NULL,
	[InvalidTime] [bit] NULL,
	[SkippedInterval] [bit] NULL,
	[CompleteOutage] [bit] NULL,
	[PulseOverflow] [bit] NULL,
	[TestMode] [bit] NULL,
	[Tamper] [bit] NULL,
	[PartialOutage] [bit] NULL,
	[SuspectedOutage] [bit] NULL,
	[Restoration] [bit] NULL,
	[DST] [bit] NULL,
	[InvalidValue] [bit] NULL,
	[MeterSerialNumber] [varchar](50) NULL,
	[Source] [varchar](200) NULL CONSTRAINT [DF_daIntervalMeterDataRevs_Source]  DEFAULT ('IMPORTED'),
	[userID] [int] NULL,
	[EditDateTime] [datetime] NULL,
	[ActualMeterRead] [decimal](18, 4) NULL,
	[EstimationInfo] [varchar](200) NULL,
 CONSTRAINT [PK_daInertvalMeterDataRevs] PRIMARY KEY CLUSTERED 
(
	[IntervalMeterDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daPreVEEExtractedFiles]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daPreVEEExtractedFiles](
	[PreVEEExtractedFileId] [int] IDENTITY(1,1) NOT NULL,
	[PreVEEZipFileId] [int] NULL,
	[ExtractedFileName] [varchar](max) NULL,
	[ExtractedFileStatus] [bit] NULL,
 CONSTRAINT [PK_daPreVEEExtractedFiles] PRIMARY KEY CLUSTERED 
(
	[PreVEEExtractedFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daPreVEEZipFiles]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daPreVEEZipFiles](
	[PreVEEZipFileId] [int] IDENTITY(1,1) NOT NULL,
	[serviceId] [int] NOT NULL,
	[ZipFileName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_daPreVEEZipFiles] PRIMARY KEY CLUSTERED 
(
	[PreVEEZipFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daScheduledServices]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daScheduledServices](
	[ScheduledServiceID] [int] IDENTITY(1,1) NOT NULL,
	[serviceId] [int] NULL,
	[templateId] [int] NULL,
	[interval] [varchar](50) NULL,
	[time] [varchar](50) NULL,
	[datenumber] [int] NULL,
	[weekname] [varchar](50) NULL,
	[Status] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daService]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daService](
	[serviceId] [int] IDENTITY(1,1) NOT NULL,
	[serviceName] [varchar](200) NULL,
	[serviceDescription] [varchar](max) NULL,
	[URL] [varchar](max) NULL,
	[userName] [varchar](200) NULL,
	[passWord] [varchar](200) NULL,
	[status] [varchar](50) NULL CONSTRAINT [DF_daService_status]  DEFAULT ('Active'),
 CONSTRAINT [PK_daService] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daServiceAttributes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daServiceAttributes](
	[serviceAttrId] [int] IDENTITY(1,1) NOT NULL,
	[serviceId] [int] NULL,
	[serviceAttr] [varchar](max) NULL,
	[serviceAttrValues] [varchar](max) NULL,
 CONSTRAINT [PK_daServiceAttributes] PRIMARY KEY CLUSTERED 
(
	[serviceAttrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[daServiceTemplates]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[daServiceTemplates](
	[templateId] [int] IDENTITY(1,1) NOT NULL,
	[serviceId] [int] NULL,
	[templateName] [varchar](200) NULL,
	[URL] [varchar](max) NULL,
	[status] [varchar](50) NULL CONSTRAINT [DF_daServiceTemplates_status]  DEFAULT ('Active')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DataApprovalNotes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DataApprovalNotes](
	[notesid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[Date] [datetime] NULL,
	[Notes] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dataCollectionInterval]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dataCollectionInterval](
	[dataCollectionIntervalID] [int] IDENTITY(1,1) NOT NULL,
	[interval] [varchar](50) NULL,
	[intervalType] [varchar](210) NULL,
 CONSTRAINT [PK_dataCollectionInterval] PRIMARY KEY CLUSTERED 
(
	[dataCollectionIntervalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dataMigrationService]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dataMigrationService](
	[dataMigrationID] [int] IDENTITY(1,1) NOT NULL,
	[plantName] [varchar](200) NULL,
	[webServiceID] [int] NULL,
	[date] [varchar](1000) NULL,
	[time] [varchar](1000) NULL,
	[sync] [varchar](1000) NULL,
	[plantAttr] [varchar](1000) NULL,
	[code] [varchar](1000) NULL,
	[plantOutput] [decimal](8, 2) NULL,
	[sellingPrice] [decimal](8, 2) NULL,
 CONSTRAINT [PK_dataMigrationService] PRIMARY KEY CLUSTERED 
(
	[dataMigrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentDisplayName] [varchar](max) NULL,
	[DocumentName] [varchar](max) NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DropDowns]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DropDowns](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[page_name] [nvarchar](100) NULL,
	[status] [bit] NULL,
	[name] [nvarchar](200) NULL,
	[value] [nvarchar](200) NULL,
 CONSTRAINT [PK_DropDowns] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ET_SLAB_RATES]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ET_SLAB_RATES](
	[program_id] [int] NULL,
	[season_id] [int] NULL,
	[et_peaktype_id] [int] NULL,
	[slab_start] [varchar](10) NULL,
	[slab_stop] [varchar](10) NULL,
	[peak_rate] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eventAMI]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eventAMI](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TimeStamp] [datetime] NULL,
	[DiscoveredAt] [varchar](max) NULL,
	[Source] [varchar](max) NULL,
	[EndTime] [datetime] NULL,
	[State] [bit] NULL,
	[Ongoing] [bit] NULL,
	[Phase] [varchar](max) NULL,
	[Counter] [varchar](max) NULL,
	[EventCode] [varchar](50) NULL,
	[AlarmTrigger] [bit] NULL,
	[EventInfo] [varchar](max) NULL,
	[Event_Id] [int] NULL,
	[EventData_Id] [int] NULL,
	[eventAMI_Id] [int] NULL,
	[ElsterMeter_Id] [int] NULL,
	[ElsterMeterSerialNumber] [varchar](max) NULL,
	[userAssignedId] [int] NULL,
	[userAssignedTimestamp] [datetime] NULL,
	[userCompletedTimestamp] [datetime] NULL,
	[userActionId] [int] NULL CONSTRAINT [DF_eventAMI_userActionId]  DEFAULT ((4)),
 CONSTRAINT [PK_eventAMI] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eventAMI_EMAIL]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eventAMI_EMAIL](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eventID] [int] NOT NULL,
	[timestamp] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[eventCategory]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eventCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eventId] [int] NULL,
	[categoryId] [int] NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_eventCategory_active]  DEFAULT ((1)),
 CONSTRAINT [PK_eventCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventDocuments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventDocuments](
	[DocumentID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentDisplayName] [varchar](max) NULL,
	[DocumentName] [varchar](max) NULL,
 CONSTRAINT [PK_EventDocuments_1] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eventFilename]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eventFilename](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [varchar](50) NULL,
	[eventAMIid] [int] NULL,
 CONSTRAINT [PK_eventFilename] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventFiles]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventFiles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eventId] [int] NOT NULL,
	[fileId] [int] NOT NULL,
	[created] [datetime] NOT NULL CONSTRAINT [DF_EventFiles_created]  DEFAULT (getdate()),
 CONSTRAINT [PK_EventFiles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[events]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eventName] [varchar](50) NULL,
	[eventCode] [varchar](10) NULL,
	[eventDescription] [text] NULL,
	[nonAMIevent] [bit] NULL,
	[flagId] [int] NULL,
	[notifyEmail] [bit] NULL,
	[notifyTextMessage] [bit] NULL,
	[notifyPhone] [bit] NULL,
	[notifyAll] [bit] NULL,
	[containsTaskOrder] [bit] NULL,
	[eventnotification] [bit] NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventTaskOrderDocuments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTaskOrderDocuments](
	[EventTaskOrderDocID] [int] IDENTITY(1,1) NOT NULL,
	[eventCodeID] [int] NULL,
	[DocumentID] [int] NULL,
	[UserID] [int] NULL,
	[UploadedDate] [datetime] NULL,
	[mandatoryDocument] [bit] NULL,
 CONSTRAINT [PK_EventTaskOrderDocuments] PRIMARY KEY CLUSTERED 
(
	[EventTaskOrderDocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventUploadedDocuments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EventUploadedDocuments](
	[EventUploadedDocID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NULL,
	[DocumentID] [int] NULL,
	[UserID] [int] NULL,
	[UploadedDate] [datetime] NULL,
	[EventTaskOrderDocID] [int] NULL,
	[DocumentStatus] [varchar](100) NULL CONSTRAINT [DF_EventUploadedDocuments_DocumentStatus]  DEFAULT ('To Be Verified'),
	[Closed] [varchar](50) NULL,
	[AdminID] [int] NULL,
	[AdminModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_EventUploadedDocuments] PRIMARY KEY CLUSTERED 
(
	[EventUploadedDocID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eventUser]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eventUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[eventId] [int] NOT NULL,
	[userId] [int] NOT NULL,
	[active] [bit] NOT NULL CONSTRAINT [DF_eventUser_active]  DEFAULT ((1)),
 CONSTRAINT [PK_categoryUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[files]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[path] [text] NOT NULL,
	[md5] [varchar](64) NULL,
	[user_id] [int] NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[flag]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[flag](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[flagName] [varchar](150) NULL,
	[flagColor] [varchar](255) NULL CONSTRAINT [DF_flag_flagColor]  DEFAULT ('#fff'),
 CONSTRAINT [PK_flag] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[formLabel]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[formLabel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[formName] [varchar](255) NOT NULL,
	[lblName] [varchar](255) NOT NULL,
	[saved] [datetime] NOT NULL CONSTRAINT [DF_formLabel_saved]  DEFAULT (getdate())
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[gridControls]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[gridControls](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[value] [varchar](255) NOT NULL,
	[saved] [datetime] NOT NULL CONSTRAINT [DF_gridControls_saved]  DEFAULT (getdate()),
	[active] [bit] NOT NULL CONSTRAINT [DF_gridControls_active]  DEFAULT ((1)),
 CONSTRAINT [PK_gridControls] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grouping]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grouping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[collectorsID] [int] NULL,
	[sDPID] [int] NULL,
	[accountsID] [int] NULL,
	[meterID] [int] NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK_Grouping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobAssetDocuments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobAssetDocuments](
	[jobdocid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[DocumentID] [int] NULL,
	[UserID] [int] NULL,
	[UploadedDate] [datetime] NULL,
	[type] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobDataApproval]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobDataApproval](
	[jobdataapprovalid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[Date] [datetime] NULL,
	[approvedby] [varchar](200) NULL,
	[submittedby] [varchar](200) NULL,
	[ApprovalDate] [datetime] NULL,
	[ApprovalSignature] [varchar](200) NULL,
	[Notes] [varchar](max) NULL,
	[rigStatusid] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JobDataRates]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobDataRates](
	[jobdataid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[Date] [datetime] NULL,
	[AssetId] [int] NULL,
	[rate] [decimal](18, 2) NULL,
	[ServiceId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JobOrderDocuments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobOrderDocuments](
	[jobdocid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[DocumentID] [int] NULL,
	[UserID] [int] NULL,
	[UploadedDate] [datetime] NULL,
	[type] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jobTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jobTypes](
	[jobtypeid] [int] IDENTITY(1,1) NOT NULL,
	[jobtype] [varchar](200) NULL,
	[jobtypedescription] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[lookupList]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lookupList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[listName] [nvarchar](25) NOT NULL,
	[itemKey] [nvarchar](25) NOT NULL,
	[itemValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_lookupList] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MainteneceNotes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MainteneceNotes](
	[NotesID] [int] IDENTITY(1,1) NOT NULL,
	[AssetMainteneceId] [int] NULL,
	[ComponentMainteneceId] [int] NULL,
	[Notes] [varchar](max) NULL,
	[UserId] [int] NULL,
	[Datetime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[manageJobOrders]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[manageJobOrders](
	[jid] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[jobname] [varchar](200) NULL,
	[jobid] [int] NULL,
	[jobtype] [int] NULL,
	[startdate] [datetime] NULL,
	[enddate] [datetime] NULL,
	[cost] [decimal](18, 2) NULL,
	[Customer] [int] NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](100) NULL,
	[secondaryAddress2] [nvarchar](100) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[status] [varchar](50) NULL,
	[opManagerId] [int] NULL,
	[jobordercreatedid] [varchar](200) NULL,
	[approveddatetime] [datetime] NULL,
	[programManagerId] [int] NULL,
	[JobAssignedDate] [datetime] NULL,
	[salecreateddate] [datetime] NULL,
	[salesnotes] [varchar](max) NULL,
	[opmgrnotes] [varchar](max) NULL,
	[jobsource] [varchar](50) NULL,
	[rigtypeid] [int] NULL,
	[jobfrom] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meter]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[manufacturer] [varchar](50) NULL,
	[guid] [varchar](50) NULL,
	[meterIRN] [bigint] NULL,
	[meterName] [varchar](max) NULL,
	[isActive] [bit] NULL,
	[serialNumber] [varchar](max) NULL,
	[meterType] [varchar](max) NULL,
	[accountIdent] [varchar](max) NULL,
	[description] [varchar](max) NULL,
	[accountName] [varchar](max) NULL,
	[sdpIdent] [varchar](max) NULL,
	[location] [varchar](max) NULL,
	[timeZoneIndex] [bigint] NULL,
	[timeZone] [varchar](max) NULL,
	[timeZoneOffset] [bigint] NULL,
	[observesDaylightSavings] [varchar](max) NULL,
	[mediaType] [varchar](max) NULL,
	[installDate] [datetime] NULL,
	[removalDate] [datetime] NULL,
	[meterID] [int] NULL,
	[metersNotReadID] [int] NULL,
	[metersReadID] [int] NULL,
	[ErrorSectionID] [int] NULL,
	[TamperSectionID] [int] NULL,
	[MeterReadingsID] [int] NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](100) NULL,
	[secondaryAddress2] [nvarchar](100) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
 CONSTRAINT [PK_meter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meter_copy]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[meter_copy](
	[id] [int] NOT NULL,
	[manufacturer] [varchar](50) NULL,
	[guid] [varchar](50) NULL,
	[meterIRN] [bigint] NULL,
	[meterName] [varchar](max) NULL,
	[isActive] [bit] NULL,
	[serialNumber] [varchar](max) NULL,
	[meterType] [varchar](max) NULL,
	[accountIdent] [varchar](max) NULL,
	[description] [varchar](max) NULL,
	[accountName] [varchar](max) NULL,
	[sdpIdent] [varchar](max) NULL,
	[location] [varchar](max) NULL,
	[timeZoneIndex] [bigint] NULL,
	[timeZone] [varchar](max) NULL,
	[timeZoneOffset] [bigint] NULL,
	[observesDaylightSavings] [varchar](max) NULL,
	[mediaType] [varchar](max) NULL,
	[installDate] [datetime] NULL,
	[removalDate] [datetime] NULL,
	[meterID] [int] NULL,
	[metersNotReadID] [int] NULL,
	[metersReadID] [int] NULL,
	[ErrorSectionID] [int] NULL,
	[TamperSectionID] [int] NULL,
	[MeterReadingsID] [int] NULL,
	[primaryLatLong] [nchar](100) NULL,
	[primaryLatLongAccuracy] [nchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](100) NULL,
	[secondaryAddress2] [nvarchar](100) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meterData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meterData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[meterId] [int] NULL,
	[data] [text] NULL,
	[eventId] [int] NULL,
 CONSTRAINT [PK_meterData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeterDirectionType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeterDirectionType](
	[MeterDirectionId] [int] NULL,
	[MeterDirection] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleConstants]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleConstants](
	[constantID] [int] IDENTITY(1,1) NOT NULL,
	[constantModuleID] [int] NULL,
	[constantname] [nvarchar](150) NULL,
	[constantvalue] [nvarchar](150) NULL,
	[constantDescription] [nvarchar](250) NULL,
 CONSTRAINT [PK_ModuleConstants_1] PRIMARY KEY CLUSTERED 
(
	[constantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modules]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Modules](
	[moduleID] [int] IDENTITY(1,1) NOT NULL,
	[moduleName] [varchar](100) NOT NULL,
	[parentId] [int] NOT NULL CONSTRAINT [DF_Modules_parentId]  DEFAULT ((0)),
 CONSTRAINT [Modules_PK] PRIMARY KEY CLUSTERED 
(
	[moduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MyState]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyState](
	[StateID] [int] NOT NULL,
	[StateName] [nvarchar](255) NULL,
	[CountryID] [nvarchar](255) NULL,
 CONSTRAINT [PK_MyState] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prism_Assets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prism_Assets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetId] [bigint] NULL,
	[WarehouseId] [int] NULL,
	[AssetCategoryId] [int] NULL,
	[AssetName] [varchar](200) NULL,
	[SerialNumber] [varchar](200) NULL,
	[Type] [varchar](200) NULL,
	[Make] [varchar](200) NULL,
	[PartNumber] [int] NULL,
	[Description] [text] NULL,
	[Plant] [int] NULL,
	[ResponsibleParty] [int] NULL,
	[DepreciationType] [int] NULL,
	[Size] [int] NULL,
	[Owner] [int] NULL,
	[PuechasedNew] [bit] NULL,
	[Costadjust] [bit] NULL,
	[Depreciate] [bit] NULL,
	[Manufacturer] [int] NULL,
	[ManufactureCountry] [int] NULL,
	[ManufractureDate] [date] NULL,
	[AFE] [varchar](200) NULL,
	[AltPartNumber] [varchar](200) NULL,
	[InserviceDate] [date] NULL,
	[CostAdjustDate] [date] NULL,
	[RetireDate] [date] NULL,
	[Department] [int] NULL,
	[VerifiedLocation] [int] NULL,
	[VerifiedDate] [date] NULL,
	[MonthstoDepreciate] [varchar](200) NULL,
	[Hoursesincelastservice] [varchar](200) NULL,
	[Netvalue] [varchar](200) NULL,
	[Weight_LBS] [varchar](200) NULL,
	[Weight_Kgs] [varchar](200) NULL,
	[ScheduleB] [int] NULL,
	[ABCCode] [varchar](200) NULL,
	[Status] [bit] NULL,
	[Notes] [text] NULL,
	[Cost] [decimal](18, 2) NULL,
	[previoususedhrs] [decimal](18, 2) NULL,
	[repairstatus] [varchar](50) NULL CONSTRAINT [DF_Prism_Assets_repairstatus]  DEFAULT ('Ok'),
	[runhrmaintenance] [int] NULL,
	[maintenancepercentage] [int] NULL,
	[LastMaintanenceDate] [datetime] NULL,
	[ODFrac] [varchar](50) NULL,
	[IDFrac] [varchar](50) NULL,
	[Length] [decimal](18, 2) NULL,
	[TopConnection] [varchar](50) NULL,
	[BottomConnection] [varchar](50) NULL,
	[PinTop] [varchar](50) NULL,
	[PinBottom] [varchar](50) NULL,
	[Comments] [varchar](250) NULL,
	[OnSite] [bit] NULL,
	[FishingNeck] [varchar](50) NULL,
	[StabCenterPoint] [varchar](50) NULL,
	[StabBladeOD] [varchar](50) NULL,
	[Weight] [varchar](50) NULL,
	[EI] [varchar](50) NULL,
	[SizeCategory] [varchar](50) NULL,
 CONSTRAINT [PK_Prism_Assets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism_ComponentCategory]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Prism_ComponentCategory](
	[comp_categoryid] [int] IDENTITY(1,1) NOT NULL,
	[comp_categoryname] [varchar](200) NULL,
	[status] [bit] NULL CONSTRAINT [DF_Prism_ComponentCategory_status]  DEFAULT ('True'),
	[Description] [text] NULL,
 CONSTRAINT [Prism_ComponentCategory_PK] PRIMARY KEY CLUSTERED 
(
	[comp_categoryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism_ComponentNames]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Prism_ComponentNames](
	[componet_id] [int] IDENTITY(1,1) NOT NULL,
	[ComponentName] [varchar](200) NULL,
	[Description] [text] NULL,
	[comp_categoryid] [int] NULL,
 CONSTRAINT [PK_Prism_Components] PRIMARY KEY CLUSTERED 
(
	[componet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism_Components]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Prism_Components](
	[CompID] [int] IDENTITY(1,1) NOT NULL,
	[Warehouseid] [int] NULL,
	[Comp_Categoryid] [int] NULL,
	[Serialno] [varchar](200) NULL,
	[Type] [varchar](200) NULL,
	[Make] [varchar](200) NULL,
	[Cost] [decimal](18, 2) NULL,
	[Componentid] [int] NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[Prism_Components] ADD [repairstatus] [varchar](20) NULL CONSTRAINT [DF_Prism_Components_repairstatus]  DEFAULT ('Ok')
ALTER TABLE [dbo].[Prism_Components] ADD [LastMaintanenceDate] [datetime] NULL
 CONSTRAINT [PK_Prism_Components_1] PRIMARY KEY CLUSTERED 
(
	[CompID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism_NotificationType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prism_NotificationType](
	[notificationid] [int] IDENTITY(1,1) NOT NULL,
	[notificationType] [varchar](50) NULL,
 CONSTRAINT [PK_Prism_NotificationType] PRIMARY KEY CLUSTERED 
(
	[notificationid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism_ProjectPersonnels]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prism_ProjectPersonnels](
	[ProjectPersonnelid] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prism_User_EventNotificationType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prism_User_EventNotificationType](
	[UserNotificationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[EventID] [int] NULL,
	[userRoleID] [int] NULL,
	[notificationid] [int] NULL,
 CONSTRAINT [PK_Prism_User_EventNotificationType] PRIMARY KEY CLUSTERED 
(
	[UserNotificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prism_UserRole_Notification]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prism_UserRole_Notification](
	[eventID] [int] IDENTITY(1,1) NOT NULL,
	[userRoleID] [int] NULL,
	[ID] [int] NULL,
	[status] [varchar](10) NULL,
 CONSTRAINT [PK_Prism_UserRole_Notification] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism24HourActivity]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prism24HourActivity](
	[HourActivityId] [int] IDENTITY(1,1) NOT NULL,
	[24HourActivity] [varchar](200) NULL,
 CONSTRAINT [PK_Prism24HourActivity] PRIMARY KEY CLUSTERED 
(
	[HourActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Prism24Hours]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Prism24Hours](
	[TimeId] [int] NOT NULL,
	[Time] [varchar](10) NULL,
 CONSTRAINT [PK_Prism24Hours] PRIMARY KEY CLUSTERED 
(
	[TimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismAssetComponents]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismAssetComponents](
	[AssetCompId] [int] IDENTITY(1,1) NOT NULL,
	[AssetId] [int] NULL,
	[CompID] [int] NULL,
	[AssignmentStatus] [varchar](20) NULL,
	[Assigneddate] [datetime] NULL,
	[Modifieddate] [datetime] NULL,
	[componentstatus] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismAssetCurrentLocation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismAssetCurrentLocation](
	[AssetCurrentLocationID] [int] IDENTITY(1,1) NOT NULL,
	[AssetID] [int] NULL,
	[FromLocationType] [varchar](50) NULL,
	[FromLocationID] [int] NULL,
	[ToLocationType] [varchar](50) NULL,
	[ToLocationID] [int] NULL,
	[AssetMovedDate] [datetime] NULL,
	[AssetStatus] [varchar](50) NULL,
	[NotInUseSince] [datetime] NULL,
	[CurrentLocationType] [varchar](50) NULL,
	[CurrentLocationID] [int] NULL,
 CONSTRAINT [PK_PrismAssetCurrentLocation] PRIMARY KEY CLUSTERED 
(
	[AssetCurrentLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismAssetKitDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismAssetKitDetails](
	[assetkitid] [int] IDENTITY(1,1) NOT NULL,
	[kitname] [varchar](200) NULL,
	[kitdesc] [varchar](max) NULL,
	[assetids] [int] NULL,
	[createddate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismAssetMaintenanceDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismAssetMaintenanceDetails](
	[Pmaintenanceid] [int] IDENTITY(1,1) NOT NULL,
	[AssetRid] [int] NULL,
	[ConId] [int] NULL,
	[qty] [int] NULL,
	[updateddate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismAssetName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismAssetName](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetName] [varchar](200) NULL,
	[AssetDescription] [text] NULL,
	[AssetCategoryId] [int] NULL,
 CONSTRAINT [PK_PrismAssetName] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismAssetRepairStatus]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismAssetRepairStatus](
	[AssetRid] [int] IDENTITY(1,1) NOT NULL,
	[assetid] [nchar](10) NULL,
	[repairdate] [datetime] NULL,
	[repairfixdate] [datetime] NULL,
	[status] [varchar](50) NULL,
	[Notes] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismComponentCurrentLocation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismComponentCurrentLocation](
	[ComponentCurrentLocationID] [int] IDENTITY(1,1) NOT NULL,
	[ComponentID] [int] NULL,
	[FromLocationType] [varchar](50) NULL,
	[FromLocationID] [int] NULL,
	[ToLocationType] [varchar](50) NULL,
	[ToLocationID] [int] NULL,
	[ComponentMovedDate] [datetime] NULL,
	[AssetStatus] [varchar](50) NULL,
	[NotInUseSince] [datetime] NULL,
	[CurrentLocationType] [varchar](50) NULL,
	[CurrentLocationID] [int] NULL,
 CONSTRAINT [PK_PrismComponentCurrentLocation] PRIMARY KEY CLUSTERED 
(
	[ComponentCurrentLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismComponentRepairStatus]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismComponentRepairStatus](
	[ComponentRid] [int] IDENTITY(1,1) NOT NULL,
	[Componentid] [nchar](10) NULL,
	[repairdate] [datetime] NULL,
	[repairfixdate] [datetime] NULL,
	[status] [varchar](50) NULL,
	[Notes] [varchar](max) NULL,
	[Cost] [decimal](18, 3) NULL,
	[Final] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismDailyRunSummary]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismDailyRunSummary](
	[SummaryId] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[RunNumber] [int] NULL,
	[FaileY/N] [bit] NULL,
	[StartDate] [date] NULL,
	[StratTime] [time](7) NULL,
	[EndDate] [date] NULL,
	[EndTime] [time](7) NULL,
	[CircHours] [varchar](10) NULL,
	[EndDepth] [varchar](10) NULL,
	[MaxTemp] [varchar](10) NULL,
	[FlowRate] [varchar](10) NULL,
	[MudType] [varchar](50) NULL,
	[Sand/Solids] [varchar](50) NULL,
	[BHAOD] [varchar](50) NULL,
	[Poppet] [varchar](50) NULL,
	[Orifice] [varchar](50) NULL,
	[PulseWidth] [varchar](50) NULL,
	[PulseAmp] [varchar](50) NULL,
	[GR Pre-Run Bkgnd] [varchar](50) NULL,
	[Gre Pre-Run High] [varchar](50) NULL,
	[Gre Post-Run Bkgnd] [varchar](50) NULL,
	[Gre Post-Run High] [varchar](50) NULL,
	[CalFactor] [varchar](50) NULL,
	[GammaOffset] [varchar](50) NULL,
	[SurveyOffset] [varchar](50) NULL,
	[StartDepth] [varchar](50) NULL,
	[Batt #1 Voltage No Load] [varchar](50) NULL,
	[Batt #1 Voltage Load] [varchar](50) NULL,
	[Batt #2 Voltage No Load] [varchar](50) NULL,
	[Batt #2 Voltage Load] [varchar](50) NULL,
 CONSTRAINT [PK_PrismDailyRunSummary] PRIMARY KEY CLUSTERED 
(
	[SummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismEvent]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismEvent](
	[eventID] [int] IDENTITY(1,1) NOT NULL,
	[eventCode] [varchar](20) NULL,
	[eventInfo] [varchar](max) NULL,
	[eventTime] [datetime] NULL,
	[userAssignedID] [int] NULL,
	[userAssignedTimeStamp] [datetime] NULL,
	[Source] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobAssetAssignedComponents]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobAssetAssignedComponents](
	[Componet_Assign_ID] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[AssetId] [int] NULL,
	[CompID] [int] NULL,
	[Componentstatus] [varchar](50) NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PrismJobAssetAssignedComponents] PRIMARY KEY CLUSTERED 
(
	[Componet_Assign_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobAssignedAssets]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobAssignedAssets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[AssetId] [int] NULL,
	[AssetStatus] [int] NULL,
	[ModifiedBy] [varchar](200) NULL,
	[ModifiedDate] [datetime] NULL,
	[KitName] [varchar](200) NULL,
	[AssignmentStatus] [varchar](50) NULL,
 CONSTRAINT [PK_PrismJobAssignedAssets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobAssignedKitAssetDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobAssignedKitAssetDetails](
	[jobkitassignedid] [int] IDENTITY(1,1) NOT NULL,
	[jobid] [int] NULL,
	[kitid] [int] NULL,
	[assetnameid] [int] NULL,
	[number] [int] NULL,
	[assigned] [varchar](50) NULL,
	[assetid] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobAssignedPersonals]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobAssignedPersonals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[UserId] [int] NULL,
	[AssignmentStatus] [varchar](50) NULL,
 CONSTRAINT [PK_PrismJobAssignedPersonals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobConsumables]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobConsumables](
	[jobconsumableid] [int] IDENTITY(1,1) NOT NULL,
	[jobid] [int] NULL,
	[consumableid] [int] NULL,
	[qty] [int] NULL,
	[AssignedDate] [datetime] NULL,
	[ConsumableStatus] [int] NULL CONSTRAINT [DF_PrismJobConsumables_ConsumableStatus]  DEFAULT ((1))
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobKits]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobKits](
	[jobkitid] [int] IDENTITY(1,1) NOT NULL,
	[jobid] [int] NULL,
	[kitid] [int] NULL,
	[Finalizedstatus] [varchar](50) NULL,
	[KitDeliveryStatus] [int] NULL CONSTRAINT [DF_PrismJobKits_KitDeliveryStatus]  DEFAULT ((1))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRun24HourActivityLog]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRun24HourActivityLog](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[RunID] [int] NULL,
	[Time] [int] NULL,
	[24HourActivity] [int] NULL,
	[Comments] [text] NULL,
 CONSTRAINT [PK_PrismJobRun24HourActivityLog] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunActivityLog]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunActivityLog](
	[activityid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[hour] [int] NULL,
	[activityassetid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunAssetsNeeded]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunAssetsNeeded](
	[assetneededid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[assetid] [int] NULL,
	[serialnumber] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunAssetsRequired]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobRunAssetsRequired](
	[AssetQntID] [int] IDENTITY(1,1) NOT NULL,
	[AssetID] [int] NULL,
	[JobId] [int] NULL,
	[RunID] [int] NULL,
	[AQntty] [varchar](50) NULL,
 CONSTRAINT [PK_PrismJobRunAssetsRequired] PRIMARY KEY CLUSTERED 
(
	[AssetQntID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRunDailyProgress]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobRunDailyProgress](
	[dailyprogid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[depthstart] [decimal](18, 2) NULL,
	[depthend] [decimal](18, 2) NULL,
	[LastInc] [decimal](18, 2) NULL,
	[lastazm] [decimal](18, 2) NULL,
	[lasttemp] [decimal](18, 2) NULL,
	[comments] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRunDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobRunDetails](
	[runid] [int] IDENTITY(1,1) NOT NULL,
	[jid] [int] NULL,
	[Date] [datetime] NULL,
	[runnumber] [int] NULL,
	[finished] [bit] NULL,
	[dailycharges] [decimal](18, 2) NULL,
	[updateddate] [datetime] NULL,
	[MwdHandDay] [varchar](200) NULL,
	[MwdHandNight] [varchar](200) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRunDetailsFormInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobRunDetailsFormInfo](
	[InfoID] [int] IDENTITY(1,1) NOT NULL,
	[RunId] [int] NULL,
	[CompanyRep] [varchar](200) NULL,
	[POPPET_SIZE] [varchar](200) NULL,
	[ORIFICE_SIZE] [varchar](200) NULL,
	[PULSE_WIDTH] [varchar](200) NULL,
	[PULSE_AMPLITUDE] [varchar](200) NULL,
	[TOTAL_CONNECTED] [varchar](200) NULL,
	[TOTAL_CIRC] [varchar](200) NULL,
	[DEPTH_START] [varchar](200) NULL,
	[DEPTH_END] [varchar](200) NULL,
	[TEMPERATURE_C] [varchar](200) NULL,
	[TEMPERATURE_F] [varchar](200) NULL,
	[AVER_PUMP_PRESSURE] [varchar](200) NULL,
	[AVER_FLOW_RATE] [varchar](200) NULL,
	[MUD_WEIGHT] [varchar](200) NULL,
	[SOLIDS] [varchar](200) NULL,
	[SAND] [varchar](200) NULL,
	[Jobid] [int] NULL,
	[INCStart] [varchar](200) NULL,
	[AZMStart] [varchar](200) NULL,
	[MAGFStart] [varchar](200) NULL,
	[GRAVStart] [varchar](200) NULL,
	[DIPStart] [varchar](200) NULL,
	[DIPEnd] [varchar](200) NULL,
	[GRAVEnd] [varchar](200) NULL,
	[MAGFEnd] [varchar](200) NULL,
	[AZMEnd] [varchar](200) NULL,
	[INCEnd] [varchar](200) NULL,
 CONSTRAINT [PK_PrismJobRunDetailsFormInfo] PRIMARY KEY CLUSTERED 
(
	[InfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRunDrillingParameters]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunDrillingParameters](
	[drillingparamid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[pumppressure] [decimal](18, 2) NULL,
	[flowrate] [decimal](18, 2) NULL,
	[mudweight] [decimal](18, 2) NULL,
	[chlorides] [decimal](18, 2) NULL,
	[pulseamp] [decimal](18, 2) NULL,
	[sand] [int] NULL,
	[solid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunHourdetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunHourdetails](
	[runhrsid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[assetid] [int] NULL,
	[inuse] [bit] NULL,
	[previoustoolshrs] [decimal](18, 2) NULL,
	[cumulativetoolshrs] [decimal](18, 2) NULL,
	[totalrunshrs] [decimal](18, 2) NULL,
	[dailyrunhrs] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunOtherSuppliesNeeded]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismJobRunOtherSuppliesNeeded](
	[othersuppliesneedid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[suppliesneeded] [varchar](200) NULL,
	[serialnumber] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismJobRunPersonals]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunPersonals](
	[personalid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[personid] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobRunServiceDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobRunServiceDetails](
	[runserviceid] [int] IDENTITY(1,1) NOT NULL,
	[runid] [int] NULL,
	[serviceId] [int] NULL,
	[inuse] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismJobServiceAssignment]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismJobServiceAssignment](
	[AssignID] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[ServiceID] [int] NULL,
 CONSTRAINT [PK_PrismJobServiceAssignment] PRIMARY KEY CLUSTERED 
(
	[AssignID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismKitAssetComponentDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismKitAssetComponentDetails](
	[kitassetcompid] [int] IDENTITY(1,1) NOT NULL,
	[component_id] [int] NULL,
	[assetid] [int] NULL,
	[quantity] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismKitAssetFromKitName]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrismKitAssetFromKitName](
	[kitassetid] [int] IDENTITY(1,1) NOT NULL,
	[assetkitid] [int] NULL,
	[assetids] [int] NULL,
	[qty] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrismService]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismService](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](200) NULL,
	[ServiceDescription] [text] NULL,
	[Cost] [decimal](18, 2) NULL,
 CONSTRAINT [PK_PrismService] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismShippingTicket]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismShippingTicket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShippingDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[TicketId] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PrismShippingTicket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrismShippingTicketDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrismShippingTicketDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [varchar](50) NULL,
	[AssetId] [varchar](50) NULL,
	[FromLocation] [varchar](50) NULL,
	[FromLocationID] [varchar](50) NULL,
	[ToLocation] [varchar](50) NULL,
	[ToLocationID] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_PrismShippingTicketDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[progType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[progType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[typeName] [nchar](25) NOT NULL,
 CONSTRAINT [PK_progType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrsimCustomers]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrsimCustomers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](50) NULL,
	[secondaryAddress2] [nvarchar](50) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[Type] [nvarchar](100) NULL,
	[DwellingType] [nvarchar](100) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrsimDepartments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrsimDepartments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](50) NULL,
	[secondaryAddress2] [nvarchar](50) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[Type] [nvarchar](100) NULL,
	[DwellingType] [nvarchar](100) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrsimJobAssetStatus]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrsimJobAssetStatus](
	[Id] [int] NOT NULL,
	[StatusText] [varchar](200) NULL,
 CONSTRAINT [PK_PrsimJobAssetStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PrsimManufracturer]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrsimManufracturer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](50) NULL,
	[secondaryAddress2] [nvarchar](50) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[Type] [nvarchar](100) NULL,
	[DwellingType] [nvarchar](100) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrsimProjectInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrsimProjectInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Senior_MGMT] [int] NULL,
	[Project_MGMT] [int] NULL,
	[Sales_MGMT] [int] NULL,
	[Project_Personal] [int] NULL,
	[Accounting] [int] NULL,
 CONSTRAINT [PK_PrsimProjectInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrsimWarehouses]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrsimWarehouses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](50) NULL,
	[secondaryAddress2] [nvarchar](50) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[Type] [nvarchar](100) NULL,
	[DwellingType] [nvarchar](100) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RigStatusDet]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RigStatusDet](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[rigstatuses] [varchar](200) NULL,
	[Status] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RigTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RigTypes](
	[rigtypeid] [int] IDENTITY(1,1) NOT NULL,
	[rigtypename] [varchar](200) NULL,
	[rigtypedesc] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[routineToIDlookup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routineToIDlookup](
	[routineToIDlookupID] [int] IDENTITY(1,1) NOT NULL,
	[veeRoutinesID] [int] NULL,
	[veeRulesID] [int] NULL,
 CONSTRAINT [PK_routineToIDlookup] PRIMARY KEY CLUSTERED 
(
	[routineToIDlookupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SDP]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[bitActive] [bit] NULL,
	[Number] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[primaryFirst] [nvarchar](100) NULL,
	[primaryLast] [nvarchar](100) NULL,
	[primaryAddress1] [nvarchar](100) NULL,
	[primaryAddress2] [nvarchar](100) NULL,
	[primaryCity] [nvarchar](100) NULL,
	[primaryState] [nvarchar](100) NULL,
	[primaryCountry] [nvarchar](100) NULL,
	[primaryPostalCode] [nvarchar](100) NULL,
	[primaryPhone1] [nvarchar](100) NULL,
	[primaryPhone2] [nvarchar](100) NULL,
	[primaryEmail] [nvarchar](250) NULL,
	[primaryLatLong] [nvarchar](100) NULL,
	[primaryLatLongAccuracy] [nvarchar](50) NULL,
	[secondaryFirst] [nvarchar](100) NULL,
	[secondaryLast] [nvarchar](100) NULL,
	[secondaryAddress1] [nvarchar](50) NULL,
	[secondaryAddress2] [nvarchar](50) NULL,
	[secondaryCity] [nvarchar](100) NULL,
	[secondaryState] [nvarchar](100) NULL,
	[secondaryCountry] [nvarchar](100) NULL,
	[secondaryPostalCode] [nvarchar](100) NULL,
	[secondaryPhone1] [nvarchar](100) NULL,
	[secondaryPhone2] [nvarchar](100) NULL,
	[secondaryEmail] [nvarchar](250) NULL,
	[secondaryLatLong] [nvarchar](100) NULL,
	[secondaryLatLongAccuracy] [nvarchar](50) NULL,
	[Type] [nvarchar](100) NULL,
	[DwellingType] [nvarchar](100) NULL,
	[bitSecondarySamePrimaryAddress] [bit] NULL,
 CONSTRAINT [PK_SDP] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SDPDwellingTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDPDwellingTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_SDPDwellingTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SDPTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDPTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_SDPTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Session]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[sessionID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
 CONSTRAINT [Session_PK] PRIMARY KEY CLUSTERED 
(
	[sessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stateCode]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stateCode](
	[code] [nvarchar](2) NOT NULL,
	[name] [nvarchar](100) NULL,
 CONSTRAINT [PK_stateCode] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblbhaHydinputs]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblbhaHydinputs](
	[Job Id] [varchar](20) NULL,
	[BHA #] [int] NULL,
	[Mud Weight] [decimal](18, 2) NULL,
	[PV] [decimal](18, 2) NULL,
	[YP] [decimal](18, 2) NULL,
	[Depth] [decimal](18, 2) NULL,
	[Flowrate] [decimal](18, 2) NULL,
	[J1] [decimal](18, 2) NULL,
	[J2] [decimal](18, 2) NULL,
	[J3] [decimal](18, 2) NULL,
	[J4] [decimal](18, 2) NULL,
	[J5] [decimal](18, 2) NULL,
	[J6] [decimal](18, 2) NULL,
	[J7] [decimal](18, 2) NULL,
	[J8] [decimal](18, 2) NULL,
	[J9] [decimal](18, 2) NULL,
	[J10] [decimal](18, 2) NULL,
	[TFA] [decimal](18, 2) NULL,
	[MWDDROP] [decimal](18, 2) NULL,
	[MOTORDROP] [decimal](18, 2) NULL,
	[OTHERDROP] [decimal](18, 2) NULL,
	[PRESSURE] [decimal](18, 2) NULL,
	[HOLEDIAM] [decimal](18, 2) NULL,
	[NUMJETS] [decimal](18, 2) NULL,
	[SURFACETYPE] [smallint] NULL,
	[Model] [varchar](15) NULL,
	[CP_MaxSurfRPM] [decimal](18, 2) NULL,
	[CP_MaxDLRotating] [decimal](18, 2) NULL,
	[CP_MaxDLSliding] [decimal](18, 2) NULL,
	[CP_MaxDiffPress] [decimal](18, 2) NULL,
	[CP_MaxFlowRate] [decimal](18, 2) NULL,
	[CP_MaxTorque] [decimal](18, 2) NULL,
	[CP_KickPadYN] [varchar](1) NULL,
	[CP_KickPadType] [varchar](20) NULL,
	[CP_KickPadLength] [decimal](18, 2) NULL,
	[CP_KickPadGuage] [varchar](12) NULL,
	[CP_KickPadWidth] [decimal](18, 2) NULL,
	[CP_BittoKickPad] [decimal](18, 2) NULL,
	[CP_decimal(18, 2)inMotor] [varchar](1) NULL,
	[CP_RotorJetYN] [varchar](1) NULL,
	[CP_RotorCatcher] [varchar](1) NULL,
	[CP_RotorCoating] [varchar](20) NULL,
	[CP_Formation] [varchar](50) NULL,
	[CP_KickPadHeight] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TblBHAINFO]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TblBHAINFO](
	[job no id] [varchar](20) NULL,
	[BHA #] [int] NULL,
	[Wellpath Name] [varchar](50) NULL,
	[General Description] [varchar](150) NULL,
	[BHA TYPE] [varchar](50) NULL,
	[Reason for POOH] [varchar](50) NULL,
	[BHA Failure] [bit] NULL,
	[BHA Failure Description] [varchar](max) NULL,
	[Motor MFG] [varchar](50) NULL,
	[Motor Description] [varchar](50) NULL,
	[Motor Bend] [decimal](18, 2) NULL,
	[Bit to Bend] [decimal](18, 2) NULL,
	[Motor Model] [varchar](20) NULL,
	[Motor Stator] [varchar](50) NULL,
	[Motor Pad OD] [varchar](50) NULL,
	[PERF_FLAG] [bit] NULL,
	[Performance Expanation] [varchar](max) NULL,
	[Bit Serial #] [varchar](20) NULL,
	[Manufacturer] [varchar](20) NULL,
	[Description] [varchar](50) NULL,
	[OD Frac] [varchar](50) NULL,
	[Length] [decimal](18, 2) NULL,
	[TFA] [decimal](18, 2) NULL,
	[NUMJETS] [int] NULL,
	[JET1] [decimal](18, 2) NULL,
	[JET2] [decimal](18, 2) NULL,
	[JET3] [decimal](18, 2) NULL,
	[JET4] [decimal](18, 2) NULL,
	[JET5] [decimal](18, 2) NULL,
	[JET6] [decimal](18, 2) NULL,
	[JET7] [decimal](18, 2) NULL,
	[JET8] [decimal](18, 2) NULL,
	[JET9] [decimal](18, 2) NULL,
	[JET10] [decimal](18, 2) NULL,
	[Top Connection] [varchar](20) NULL,
	[IADC Code] [varchar](50) NULL,
	[Type Bit] [varchar](50) NULL,
	[Cutter Structure] [varchar](15) NULL,
	[Inner] [int] NULL,
	[Outer] [int] NULL,
	[Dull Char] [varchar](50) NULL,
	[Location] [varchar](50) NULL,
	[Bearing Style] [varchar](50) NULL,
	[Bearing/Seals] [varchar](50) NULL,
	[Gage] [varchar](50) NULL,
	[Other Dull  Char] [varchar](50) NULL,
	[Reason Pulled] [varchar](50) NULL,
	[INC IN] [decimal](18, 2) NULL,
	[INC OUT] [decimal](18, 2) NULL,
	[AZM IN] [decimal](18, 2) NULL,
	[AZM OUT] [decimal](18, 2) NULL,
	[Bit to Sensor] [decimal](18, 2) NULL,
	[Bit to Gamma] [decimal](18, 2) NULL,
	[Bit to Resistivity] [decimal](18, 2) NULL,
	[Bit to Porosity] [decimal](18, 2) NULL,
	[Othernum1] [decimal](18, 2) NULL,
	[Othernum2] [decimal](18, 2) NULL,
	[Othernum3] [decimal](18, 2) NULL,
	[Othernum4] [decimal](18, 2) NULL,
	[Othernum5] [decimal](18, 2) NULL,
	[Otherstr1] [varchar](50) NULL,
	[Otherstr2] [varchar](50) NULL,
	[Otherstr3] [varchar](50) NULL,
	[HOLESIZE] [varchar](255) NULL,
	[Bit_to_Gyro] [decimal](18, 2) NULL,
	[Stall_Pressure] [decimal](18, 2) NULL,
	[CasingOD] [varchar](15) NULL,
	[ShoeDepth] [decimal](18, 2) NULL,
	[HangerDepth] [decimal](18, 2) NULL,
	[OffBottom] [decimal](18, 2) NULL,
	[RunNo] [varchar](5) NULL,
	[Motor_Stages] [decimal](18, 2) NULL,
	[BitComment] [varchar](25) NULL,
	[BURActual] [decimal](18, 2) NULL,
	[BURProp] [decimal](18, 2) NULL,
	[Motor_Failure] [smallint] NULL,
	[Stall_Number] [smallint] NULL,
	[Stall_Duration] [varchar](25) NULL,
	[BHA_Performance] [varchar](max) NULL,
	[MoreComment] [varchar](max) NULL,
	[MoreReason] [varchar](max) NULL,
	[Extended] [smallint] NULL,
	[Formation] [varchar](25) NULL,
	[U_Stab_OD] [varchar](10) NULL,
	[L_Stab_OD] [varchar](10) NULL,
	[Stat_Rotor_Clear] [varchar](10) NULL,
	[BentSub] [decimal](18, 2) NULL,
	[Rotary_Failure] [smallint] NULL,
	[Motor_Used] [smallint] NULL,
	[RS_Op_Style] [varchar](10) NULL,
	[RS_FirmWare_Version] [varchar](20) NULL,
	[RS_MaxDLS] [varchar](10) NULL,
	[RS_Stab_OD] [varchar](10) NULL,
	[RS_Bit_to_Ctr_NB] [varchar](10) NULL,
	[RS_Ctr_NB_to_Stab] [varchar](10) NULL,
	[RS_Blade_Type] [varchar](10) NULL,
	[RS_Blade_Style] [varchar](10) NULL,
	[RS_Stab_to_Top] [varchar](10) NULL,
	[RS_Batt_Usage] [varchar](10) NULL,
	[RS_Guage_Length] [varchar](10) NULL,
	[RS_Blade_Collapse] [varchar](10) NULL,
	[RS_Wake_Up_RPM_Drill] [varchar](10) NULL,
	[RS_Blade_count] [varchar](10) NULL,
	[RS_HardFacing] [varchar](10) NULL,
	[RS_Performance] [varchar](max) NULL,
	[RS_Description] [varchar](50) NULL,
	[RS_MFG] [varchar](25) NULL,
	[RS_Serial_Number] [varchar](25) NULL,
	[RS_Pad_OD] [varchar](10) NULL,
	[Hawkeye_TD] [varchar](max) NULL,
	[CP_MotorElastomer] [varchar](25) NULL,
	[CP_MotorBendType] [varchar](25) NULL,
	[CP_Clearance_Range] [varchar](25) NULL,
	[CP_MWDVendor] [varchar](25) NULL,
	[CP_MWDRuns] [smallint] NULL,
	[CP_Inspected] [smallint] NULL,
	[CP_Inspector_Company] [varchar](25) NULL,
	[CP_BHAOBJECTIVE] [varchar](max) NULL,
	[CP_Even_Wall] [varchar](1) NULL,
	[CP_BottomStabType] [varchar](20) NULL,
	[CP_StabBladeLength] [varchar](15) NULL,
	[CP_TopBladeType] [varchar](20) NULL,
	[CP_InterferenceFit] [varchar](15) NULL,
	[CP_InterferenceTolerance] [varchar](15) NULL,
	[CP_AvgOnBtmSPP] [varchar](15) NULL,
	[CP_AvgOffBtmSPP] [varchar](15) NULL,
	[CP_BotBladeType] [varchar](20) NULL,
	[CP_TopStabType] [varchar](20) NULL,
	[CP_TopBladeLength] [varchar](15) NULL,
	[PD_MagtoGravity] [decimal](18, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblbhaitems]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblbhaitems](
	[Job no ID] [varchar](20) NULL,
	[BHA #] [int] NULL,
	[Item No] [int] NULL,
	[Cost Group] [varchar](50) NULL,
	[Serial Number] [varchar](50) NULL,
	[Manufacturer] [varchar](50) NULL,
	[Description] [varchar](80) NULL,
	[ODFrac] [varchar](10) NULL,
	[IDFrac] [varchar](10) NULL,
	[Length] [decimal](18, 2) NULL,
	[Top Connection] [varchar](115) NULL,
	[Pin TOP] [bit] NULL,
	[Bottom Connection] [varchar](15) NULL,
	[Pin Bottom] [bit] NULL,
	[Weight] [decimal](18, 2) NULL,
	[Fishing Neck] [varchar](30) NULL,
	[Stab ODFrace] [varchar](10) NULL,
	[Comment] [varchar](50) NULL,
	[OD] [decimal](18, 2) NULL,
	[ID] [decimal](18, 2) NULL,
	[Stab OD] [decimal](18, 2) NULL,
	[Stab Center Point] [decimal](18, 2) NULL,
	[Stabilzer Type] [varchar](50) NULL,
	[Maximum Torque] [decimal](18, 2) NULL,
	[EI] [decimal](18, 2) NULL,
	[Material] [varchar](50) NULL,
	[ON SITE] [bit] NULL,
	[JOB NO] [varchar](50) NULL,
	[Tool Type] [varchar](50) NULL,
	[Dry Weight] [decimal](18, 2) NULL,
	[WetWt] [decimal](18, 2) NULL,
	[PerFT] [decimal](18, 2) NULL,
	[MFG] [varchar](25) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBHAMWDRuns]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBHAMWDRuns](
	[Job no Id] [varchar](20) NULL,
	[BHA#] [int] NULL,
	[MWDRun#] [smallint] NULL,
	[Battery Serial No1] [varchar](50) NULL,
	[Battery Serial No2] [varchar](50) NULL,
	[Pulser Serial No] [varchar](50) NULL,
	[Gamma Serial No] [varchar](50) NULL,
	[UBHO Serial No] [varchar](50) NULL,
	[Sensor_Serial_No] [varchar](50) NULL,
	[Battery1 Load Volts] [varchar](50) NULL,
	[Battery1 Unload Volts] [varchar](50) NULL,
	[Battery1 AmpHrs] [varchar](50) NULL,
	[Battery2 Load Volts] [varchar](50) NULL,
	[Battery2 Unload Volts] [varchar](50) NULL,
	[Battery2 AmpHrs] [varchar](50) NULL,
	[Remote Terminal] [varchar](50) NULL,
	[Power Supply Serial No] [varchar](50) NULL,
	[Failure] [smallint] NULL,
	[ItemFailed] [varchar](50) NULL,
	[Lost Time (Hours)] [decimal](18, 2) NULL,
	[Location Failed] [varchar](50) NULL,
	[Failure Mode] [varchar](50) NULL,
	[DateIn] [datetime] NULL,
	[TimeIn] [varchar](50) NULL,
	[DateOut] [datetime] NULL,
	[TimeOut] [varchar](50) NULL,
	[DepthIn] [decimal](18, 2) NULL,
	[DepthOut] [int] NULL,
	[Comment] [varchar](max) NULL,
	[Delta Time Hrs] [decimal](18, 2) NULL,
	[Delta Depth] [decimal](18, 2) NULL,
	[PulseWidth] [decimal](18, 2) NULL,
	[PulseSize] [decimal](18, 2) NULL,
	[MainOrifce] [decimal](18, 2) NULL,
	[PoppetTip] [decimal](18, 2) NULL,
	[HowitFailed] [varchar](max) NULL,
	[SolutionsTried] [varchar](max) NULL,
	[OperatorComments] [varchar](max) NULL,
	[ShipEvalutation] [varchar](max) NULL,
	[RepairAction] [varchar](max) NULL,
	[ResistivitySerno] [varchar](50) NULL,
	[PosositySerNumber] [varchar](50) NULL,
	[PressureSerNumber] [varchar](50) NULL,
	[ElectronicsSerNo] [varchar](50) NULL,
	[CircHrs] [decimal](18, 2) NULL,
	[Connect Hrs] [int] NULL,
	[Hrs Below Rotary] [int] NULL,
	[Mud Wt] [decimal](18, 2) NULL,
	[Plastic Viscosity] [decimal](18, 2) NULL,
	[Yield Point] [decimal](18, 2) NULL,
	[Clorides] [decimal](18, 2) NULL,
	[Sand] [decimal](18, 2) NULL,
	[Solid] [decimal](18, 2) NULL,
	[Flow Temperature] [decimal](18, 2) NULL,
	[Bottom Hole Temperature] [decimal](18, 2) NULL,
	[Analine Pt] [decimal](18, 2) NULL,
	[Mud Type] [varchar](50) NULL,
	[PH] [decimal](18, 2) NULL,
	[Mud Additive Comment] [varchar](100) NULL,
	[Fluid Loss] [decimal](18, 2) NULL,
	[OilPercent] [int] NULL,
	[FLOWRATE] [decimal](18, 2) NULL,
	[Motor MFG] [varchar](50) NULL,
	[Motor Description] [varchar](50) NULL,
	[Motor Bend] [decimal](18, 2) NULL,
	[Bit to Bend] [decimal](18, 2) NULL,
	[Motor Model] [varchar](20) NULL,
	[Motor Stator] [varchar](50) NULL,
	[Motor Pad OD] [varchar](50) NULL,
	[Bit Serial #] [varchar](20) NULL,
	[Manufacturer] [varchar](20) NULL,
	[Description] [varchar](50) NULL,
	[OD Frac] [varchar](50) NULL,
	[Length] [decimal](18, 2) NULL,
	[TFA] [decimal](18, 2) NULL,
	[NUMJETS] [int] NULL,
	[JET1] [decimal](18, 2) NULL,
	[JET2] [decimal](18, 2) NULL,
	[JET3] [decimal](18, 2) NULL,
	[JET4] [decimal](18, 2) NULL,
	[JET5] [decimal](18, 2) NULL,
	[JET6] [decimal](18, 2) NULL,
	[JET7] [decimal](18, 2) NULL,
	[JET8] [decimal](18, 2) NULL,
	[JET9] [decimal](18, 2) NULL,
	[JET10] [decimal](18, 2) NULL,
	[Top Connection] [varchar](20) NULL,
	[IADC Code] [varchar](50) NULL,
	[Type Bit] [varchar](50) NULL,
	[TypeEmitter] [varchar](50) NULL,
	[TypeSensor] [varchar](50) NULL,
	[MWDPressureDrop] [decimal](18, 2) NULL,
	[BITPressureDrop] [decimal](18, 2) NULL,
	[Program Mode] [varchar](25) NULL,
	[TotalDrilled] [decimal](18, 2) NULL,
	[BelowRotary] [decimal](18, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBHAType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBHAType](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[BHAType] [varchar](200) NULL,
	[BHADesc] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_RigTrack.tblBHAType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbljob]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbljob](
	[Job ID] [varchar](20) NULL,
	[Company] [varchar](50) NULL,
	[Location] [varchar](50) NULL,
	[API JOB #] [varchar](50) NULL,
	[Rig Name] [varchar](50) NULL,
	[State/Province] [varchar](30) NULL,
	[County/Parish] [varchar](50) NULL,
	[Country] [varchar](30) NULL,
	[RKB] [decimal](18, 2) NULL,
	[GL or MSL] [decimal](18, 2) NULL,
	[Comment] [varchar](255) NULL,
	[Cost Units] [varchar](10) NULL,
	[Job Status] [varchar](50) NULL,
	[Job Type] [varchar](50) NULL,
	[Field] [varchar](50) NULL,
	[Township] [varchar](20) NULL,
	[Range] [varchar](50) NULL,
	[SECTION] [varchar](50) NULL,
	[REPORT TIME] [varchar](5) NULL,
	[CHARGE TIME] [varchar](50) NULL,
	[LINER1] [decimal](18, 2) NULL,
	[LINER2] [decimal](18, 2) NULL,
	[STROKE1] [decimal](18, 2) NULL,
	[STROKE2] [decimal](18, 2) NULL,
	[MODEL1] [varchar](50) NULL,
	[MODEL2] [varchar](50) NULL,
	[TYPE1] [varchar](50) NULL,
	[TYPE2] [varchar](50) NULL,
	[NAME1] [varchar](50) NULL,
	[NAME2] [varchar](50) NULL,
	[EFFIC1] [decimal](18, 2) NULL,
	[EFFIC2] [decimal](18, 2) NULL,
	[UNUSEDSTR1] [varchar](50) NULL,
	[UNUSEDSTR2] [varchar](50) NULL,
	[UNUSEDSTR3] [varchar](50) NULL,
	[UNUSEDSTR4] [varchar](255) NULL,
	[UNUSED STR5] [varchar](50) NULL,
	[UNUSED1] [decimal](18, 2) NULL,
	[UNUSED2] [decimal](18, 2) NULL,
	[UNUSED3] [decimal](18, 2) NULL,
	[UNUSED4] [decimal](18, 2) NULL,
	[UNUSED5] [decimal](18, 2) NULL,
	[KellyLength] [decimal](18, 2) NULL,
	[BHALength] [decimal](18, 2) NULL,
	[StartLength] [decimal](18, 2) NULL,
	[BITtoSensor] [decimal](18, 2) NULL,
	[WorkOrder] [varchar](255) NULL,
	[Contractno] [varchar](255) NULL,
	[Customerno] [varchar](255) NULL,
	[Logoname] [varchar](255) NULL,
	[WinserveFile] [varchar](255) NULL,
	[DDCompany] [varchar](25) NULL,
	[DDAddr] [varchar](max) NULL,
	[OilAddr] [varchar](max) NULL,
	[SURVCURVE] [smallint] NULL,
	[TypeImport] [smallint] NULL,
	[CP_Version] [varchar](25) NULL,
	[CP_TypeRig] [varchar](25) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJobCasing]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJobCasing](
	[Job ID] [varchar](20) NULL,
	[Size] [varchar](50) NULL,
	[Weight] [decimal](18, 2) NULL,
	[ID] [decimal](18, 2) NULL,
	[OD] [decimal](18, 2) NULL,
	[Top Depth] [decimal](18, 2) NULL,
	[Set Depth] [decimal](18, 2) NULL,
	[Othernum] [int] NULL,
	[Othernum1] [int] NULL,
	[Othernum2] [int] NULL,
	[Run_AFTER_BHA_no] [smallint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJOBcost]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJOBcost](
	[JOB ID] [varchar](20) NULL,
	[Date] [datetime] NULL,
	[GROUP] [varchar](50) NULL,
	[ITEM IDENTIFIER] [varchar](50) NULL,
	[IDENTIFIER DESCRIPTION] [varchar](100) NULL,
	[CHARGE METHOD] [varchar](50) NULL,
	[UNITS] [decimal](18, 2) NULL,
	[CHARGE TO APPLY] [varchar](max) NULL,
	[Minimum Charge] [varchar](max) NULL,
	[MAXIMUM CHARGE] [varchar](max) NULL,
	[PRICE] [varchar](max) NULL,
	[Auto Update] [bit] NULL,
	[varchar(max)] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJOBDATE]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJOBDATE](
	[Job ID] [varchar](20) NULL,
	[Date] [datetime] NULL,
	[DD1] [varchar](50) NULL,
	[DD2] [varchar](50) NULL,
	[Company Rep] [varchar](50) NULL,
	[Mud Company] [varchar](50) NULL,
	[Current Mud Data] [varchar](25) NULL,
	[General Comment] [varchar](max) NULL,
	[Mwd1] [varchar](50) NULL,
	[MWD2] [varchar](50) NULL,
	[Tool Pusher] [varchar](50) NULL,
	[Directional Company] [varchar](50) NULL,
	[Inc Inc] [decimal](18, 2) NULL,
	[Azimuth In] [decimal](18, 2) NULL,
	[Inc Out] [decimal](18, 2) NULL,
	[Azimuth Out] [decimal](18, 2) NULL,
	[Daily Cost] [decimal](18, 2) NULL,
	[Quarter] [varchar](12) NULL,
	[DD_Trainee] [varchar](25) NULL,
	[MWD_Trainee] [varchar](25) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJOBDATEitems]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJOBDATEitems](
	[JOb ID] [varchar](20) NULL,
	[Date] [datetime] NULL,
	[Start Time] [varchar](5) NULL,
	[End Time] [varchar](5) NULL,
	[Code] [varchar](20) NULL,
	[Comment] [varchar](max) NULL,
	[Start Depth] [decimal](18, 2) NULL,
	[End Depth] [decimal](18, 2) NULL,
	[WOB] [decimal](18, 2) NULL,
	[BHA#] [int] NULL,
	[TFO] [varchar](50) NULL,
	[ROP] [decimal](18, 2) NULL,
	[RPM] [decimal](18, 2) NULL,
	[Surface Torque] [decimal](18, 2) NULL,
	[Mud Wt] [decimal](18, 2) NULL,
	[Plastic Viscosity] [decimal](18, 2) NULL,
	[Yield Point] [decimal](18, 2) NULL,
	[Clorides] [decimal](18, 2) NULL,
	[Sand] [decimal](18, 2) NULL,
	[Solid] [decimal](18, 2) NULL,
	[Flow Temperature] [decimal](18, 2) NULL,
	[Bottom Hole Temperature] [decimal](18, 2) NULL,
	[Analine Pt] [decimal](18, 2) NULL,
	[Mud Type] [varchar](50) NULL,
	[PH] [decimal](18, 2) NULL,
	[Delta Time] [decimal](18, 2) NULL,
	[Delta Depth] [decimal](18, 2) NULL,
	[Mud Additive Comment] [varchar](100) NULL,
	[Fluid Loss] [decimal](18, 2) NULL,
	[OilPercent] [int] NULL,
	[Inc] [decimal](18, 2) NULL,
	[Azimuth] [decimal](18, 2) NULL,
	[DLS] [decimal](18, 2) NULL,
	[FLOWRATE] [decimal](18, 2) NULL,
	[PICK UP WT] [decimal](18, 2) NULL,
	[SLACK OFF WT] [decimal](18, 2) NULL,
	[RAB WT] [decimal](18, 2) NULL,
	[SPM] [decimal](18, 2) NULL,
	[UNUSED1] [decimal](18, 2) NULL,
	[UNUSED2] [decimal](18, 2) NULL,
	[UNUSED3] [decimal](18, 2) NULL,
	[UNUSEDSTR1] [varchar](50) NULL,
	[UNUSEDSTR2] [varchar](50) NULL,
	[UNUSEDSTR3] [varchar](50) NULL,
	[GAS] [decimal](18, 2) NULL,
	[Viscosity] [int] NULL,
	[MWD#] [smallint] NULL,
	[BIT#] [smallint] NULL,
	[CP_SPPOFF] [decimal](18, 2) NULL,
	[CP_SURVMD] [decimal](18, 2) NULL,
	[CP_DLSMot] [decimal](18, 2) NULL,
	[CP_DLSOut] [decimal](18, 2) NULL,
	[CP_Formation] [varchar](25) NULL,
	[CP_DeltaMax] [decimal](18, 2) NULL,
	[CP_DeltaPress] [decimal](18, 2) NULL,
	[BR] [decimal](18, 2) NULL,
	[WR] [decimal](18, 2) NULL,
	[TFA] [decimal](18, 2) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tbljobinventory]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tbljobinventory](
	[JOBID] [varchar](20) NULL,
	[Serial Number] [varchar](50) NULL,
	[Tool Group] [varchar](50) NULL,
	[Manufacturer] [varchar](50) NULL,
	[Description] [varchar](80) NULL,
	[ODFrac] [varchar](10) NULL,
	[IDFrac] [varchar](10) NULL,
	[Length] [decimal](18, 2) NULL,
	[Top Connection] [varchar](115) NULL,
	[Pin TOP] [bit] NULL,
	[Bottom Connection] [varchar](15) NULL,
	[Pin Bottom] [bit] NULL,
	[Weight] [decimal](18, 2) NULL,
	[Fishing Neck] [varchar](30) NULL,
	[Stab ODFrace] [varchar](10) NULL,
	[Comment] [varchar](50) NULL,
	[OD] [decimal](18, 2) NULL,
	[ID] [decimal](18, 2) NULL,
	[Stab OD] [int] NULL,
	[Stab Center Point] [decimal](18, 2) NULL,
	[Stabilzer Type] [varchar](50) NULL,
	[Maximum Torque] [decimal](18, 2) NULL,
	[EI] [decimal](18, 2) NULL,
	[Material] [varchar](50) NULL,
	[ON SITE] [bit] NULL,
	[JOB NO] [varchar](50) NULL,
	[Tool Type] [varchar](50) NULL,
	[Size Category] [varchar](15) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJobsurveys]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJobsurveys](
	[JobID] [varchar](20) NULL,
	[Curveno] [smallint] NULL,
	[MD] [decimal](18, 2) NULL,
	[INC] [decimal](18, 2) NULL,
	[AZIMUTH] [decimal](18, 2) NULL,
	[TYPE] [smallint] NULL,
	[COMMENT] [varchar](100) NULL,
	[DATE] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblJobSurvinfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblJobSurvinfo](
	[JobID] [varchar](20) NULL,
	[CurveNo] [smallint] NULL,
	[Wellpath_NAME] [varchar](50) NULL,
	[JobNO] [varchar](20) NULL,
	[FEET/METERS] [bit] NULL,
	[TIE_IN_TVD] [decimal](18, 2) NULL,
	[TIE_IN_EW] [decimal](18, 2) NULL,
	[TIE_IN_NS] [decimal](18, 2) NULL,
	[NORTHING] [decimal](18, 2) NULL,
	[EASTING] [decimal](18, 2) NULL,
	[ELEVATION] [decimal](18, 2) NULL,
	[VSP] [decimal](18, 2) NULL,
	[AutoCadColor] [int] NULL,
	[LineType] [smallint] NULL,
	[DisplayFlags] [varchar](50) NULL,
	[Label_Flags] [int] NULL,
	[Magnetic_Declination] [decimal](18, 2) NULL,
	[Method_of_Calculation] [int] NULL,
	[Subsea] [decimal](18, 2) NULL,
	[SurveyType] [varchar](25) NULL,
	[Label_Frequency] [smallint] NULL,
	[Ellipse_Flag] [smallint] NULL,
	[Casing_Flag] [smallint] NULL,
	[Winserve1_Filename] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbljobunits]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbljobunits](
	[JOB ID] [varchar](50) NULL,
	[Measurement] [varchar](50) NULL,
	[Unit] [varchar](50) NULL,
	[Conversion] [varchar](50) NULL,
	[Unit Label] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblReturned]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblReturned](
	[JOB ID] [varchar](20) NULL,
	[RETURN #] [varchar](20) NULL,
	[Date RETURNED] [datetime] NULL,
	[Shipped to Name] [varchar](50) NULL,
	[Shipped to Address1] [varchar](50) NULL,
	[Shipped to City] [varchar](50) NULL,
	[Shipped to State] [varchar](50) NULL,
	[ShippedtoCountry] [varchar](50) NULL,
	[Mode of Transport] [varchar](50) NULL,
	[Sender's name:] [varchar](50) NULL,
	[Comments] [varchar](max) NULL,
	[SEND TICKET] [bit] NULL,
	[ShippedFromName] [varchar](50) NULL,
	[ShippedFromAddress1] [varchar](50) NULL,
	[ShippedFromCity] [varchar](50) NULL,
	[ShippedFromState] [varchar](50) NULL,
	[ShippedFromCountry] [varchar](50) NULL,
	[Contact:] [varchar](25) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblserreturnjob]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblserreturnjob](
	[JOB  ID] [varchar](20) NULL,
	[RETURN #] [varchar](25) NULL,
	[SERIAL #] [varchar](25) NULL,
	[Date Returned] [datetime] NULL,
	[TOOL GROUP] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touDayDefinitions]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touDayDefinitions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nchar](10) NOT NULL,
	[name] [nchar](25) NOT NULL,
	[mon] [bit] NULL,
	[tue] [bit] NULL,
	[wed] [bit] NULL,
	[thurs] [bit] NULL,
	[fri] [bit] NULL,
	[sat] [bit] NULL,
	[sun] [bit] NULL,
	[startDate] [date] NULL,
	[stopDate] [date] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_touDayDefinitions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touFuelAdjust]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touFuelAdjust](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fuelCode] [nchar](10) NOT NULL,
	[fuelCharge] [nchar](10) NOT NULL,
	[effectiveDate] [date] NOT NULL,
	[isActive] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touPeaks]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touPeaks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[seasonID] [int] NOT NULL,
	[dayTypeID] [int] NOT NULL,
	[peakTypeID] [int] NOT NULL,
	[startHR] [int] NOT NULL,
	[stopHR] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_touPeaks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touPeaksEP]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touPeaksEP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[programID] [int] NULL,
	[seasonID] [int] NULL,
	[ET_WEEK_NAME] [varchar](100) NULL,
	[ET_PEAKTYPE_ID] [varchar](100) NULL,
	[ET_START_HR] [varchar](10) NULL,
	[ET_STOP_HR] [varchar](50) NULL,
	[PEAK_STATUS] [varchar](50) NULL,
 CONSTRAINT [PK_touPeaksEP] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touPeakTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touPeakTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[peakType] [nchar](25) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_touPeakTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touPrograms]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touPrograms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[progTypeID] [int] NOT NULL,
	[progName] [varchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_touPrograms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touProgramsInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touProgramsInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[progID] [int] NOT NULL,
	[fuelAdjustID] [int] NOT NULL,
	[taxAdjustID] [int] NOT NULL,
	[billGenerationDate] [date] NOT NULL,
	[paymentDue] [int] NOT NULL,
	[gracePeriod] [int] NOT NULL,
	[lateFee] [int] NOT NULL,
	[serviceCharge] [float] NOT NULL,
	[initAmount] [float] NULL,
	[stdDeposit] [float] NOT NULL,
 CONSTRAINT [PK_touProgramsInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touSeasonPeakTypes]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touSeasonPeakTypes](
	[programID] [int] NOT NULL,
	[seasonID] [int] NOT NULL,
	[peakTypeID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touSeasonProgram]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touSeasonProgram](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[seasonID] [int] NOT NULL,
	[progID] [int] NOT NULL,
 CONSTRAINT [PK_touSeasonProgram] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[touSeasons]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touSeasons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[seasonName] [varchar](50) NOT NULL,
	[startMonth] [nchar](10) NOT NULL,
	[endMonth] [nchar](10) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_touSeasons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_touSeasons] UNIQUE NONCLUSTERED 
(
	[seasonName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touSeasonsEP]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[touSeasonsEP](
	[season_id] [int] IDENTITY(1,1) NOT NULL,
	[PROGRAM_id] [int] NOT NULL,
	[season_no] [int] NOT NULL,
	[season_name] [varchar](100) NULL,
	[jan] [varchar](1) NULL,
	[feb] [varchar](1) NULL,
	[mar] [varchar](1) NULL,
	[apr] [varchar](1) NULL,
	[may] [varchar](1) NULL,
	[jun] [varchar](1) NULL,
	[jul] [varchar](1) NULL,
	[aug] [varchar](1) NULL,
	[sep] [varchar](1) NULL,
	[oct] [varchar](1) NULL,
	[nov] [varchar](1) NULL,
	[dec] [varchar](1) NULL,
 CONSTRAINT [PK_touSeasonsEP] PRIMARY KEY CLUSTERED 
(
	[season_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touSlabRates]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touSlabRates](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[programID] [int] NOT NULL,
	[seasonID] [int] NOT NULL,
	[peakTypeID] [int] NOT NULL,
	[slabStart] [varchar](10) NOT NULL,
	[slabStop] [varchar](10) NOT NULL,
	[peakRate] [varchar](50) NOT NULL,
 CONSTRAINT [PK_touSlabRates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[touTaxAdjust]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[touTaxAdjust](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taxCode] [varchar](200) NOT NULL,
	[taxDescription] [text] NOT NULL,
	[taxCity] [varchar](50) NOT NULL,
	[taxLocal] [varchar](50) NOT NULL,
	[taxCountry] [varchar](50) NOT NULL,
	[taxState] [varchar](50) NOT NULL,
	[taxOther] [varchar](50) NOT NULL,
	[effectiveDate] [date] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Tax-Adjustments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_touTaxAdjust] UNIQUE NONCLUSTERED 
(
	[taxCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[transactionLog]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[transactionLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[pageId] [int] NULL,
	[systemNote] [bit] NOT NULL CONSTRAINT [DF_transactionLog_systemNote]  DEFAULT ((0)),
	[attributeName] [varchar](64) NOT NULL,
	[description] [varchar](max) NOT NULL CONSTRAINT [DF_transactionLog_saved]  DEFAULT (getdate()),
	[superseded] [int] NOT NULL CONSTRAINT [DF_transactionLog_superseded]  DEFAULT ((0)),
	[created] [datetime] NOT NULL CONSTRAINT [DF_transactionLog_created]  DEFAULT (getdate()),
	[columnId] [int] NULL,
 CONSTRAINT [PK_transactionLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userAction]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userAction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[actionName] [varchar](50) NULL,
	[role] [varchar](50) NULL,
 CONSTRAINT [PK_userAction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserRoles](
	[userRoleID] [int] IDENTITY(1,1) NOT NULL,
	[userRole] [varchar](50) NOT NULL,
	[userRoleDescription] [varchar](200) NULL,
 CONSTRAINT [UserRoles_PK] PRIMARY KEY CLUSTERED 
(
	[userRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[userRolesWebservice]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[userRolesWebservice](
	[userRolesWebservi] [int] IDENTITY(1,1) NOT NULL,
	[webServiceURL] [varchar](256) NULL,
	[xmlName] [varchar](256) NULL,
	[userName] [varchar](256) NULL,
	[passWord] [varchar](256) NULL,
	[userID] [int] NULL,
 CONSTRAINT [PK_userRolesWebservice] PRIMARY KEY CLUSTERED 
(
	[userRolesWebservi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userRoleID] [int] NULL,
	[firstName] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[title] [varchar](50) NULL,
	[loginName] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[address2] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[state] [varchar](50) NULL,
	[country] [varchar](50) NULL,
	[zip] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[category] [varchar](50) NULL,
	[lastUpdatedDate] [datetime] NULL,
	[loginStatus] [varchar](5) NULL,
	[cellNo] [varchar](50) NULL,
	[cellMessageStatus] [bit] NULL,
	[pointOfContact] [bit] NULL,
	[activationCode] [varchar](20) NULL,
	[activationExpiry] [int] NULL,
	[attempts] [int] NULL,
	[locked] [bit] NULL,
	[createDate] [datetime] NULL,
	[expDate] [datetime] NULL,
	[passwordExpiry] [int] NULL,
	[status] [varchar](50) NULL,
	[emailMessageStatus] [bit] NULL,
	[eventNotification] [varchar](10) NULL,
 CONSTRAINT [Users_PK] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserTypePermissions]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypePermissions](
	[userTypePermID] [int] IDENTITY(1,1) NOT NULL,
	[moduleID] [int] NULL,
	[userRoleID] [int] NULL,
	[accessTypeID] [int] NULL,
 CONSTRAINT [UserTypePermissions_PK] PRIMARY KEY CLUSTERED 
(
	[userTypePermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[veeConstants]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[veeConstants](
	[meterTimeToleranceDur] [int] NULL,
	[resMeterZeroPulse] [bit] NULL,
	[dailyIntervalBilling] [bit] NULL,
	[CTR] [decimal](18, 4) NULL,
	[VTR] [decimal](18, 4) NULL,
	[spikeCheckThreshold] [decimal](18, 4) NULL,
	[kVARhThreshold] [decimal](18, 4) NULL,
	[highLowConstant] [decimal](18, 4) NULL,
	[webservicetype] [varchar](50) NULL,
	[highLowPercentage] [decimal](18, 4) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[veeRoutines]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[veeRoutines](
	[veeRoutinesID] [int] IDENTITY(1,1) NOT NULL,
	[veeRoutineName] [varchar](50) NULL,
	[veeRoutineDescription] [text] NULL,
 CONSTRAINT [PK_veeRoutines] PRIMARY KEY CLUSTERED 
(
	[veeRoutinesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[veeRuleRoutines]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[veeRuleRoutines](
	[veeRRID] [int] IDENTITY(1,1) NOT NULL,
	[veeRoutineID] [int] NOT NULL,
	[veeRuleID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[veeRules]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[veeRules](
	[veeRuleID] [int] IDENTITY(1,1) NOT NULL,
	[veeRuleName] [varchar](50) NULL,
	[veeRuleDescription] [text] NULL,
 CONSTRAINT [PK_veeRules] PRIMARY KEY CLUSTERED 
(
	[veeRuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[WarehouseformLabel]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WarehouseformLabel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[formName] [varchar](255) NOT NULL,
	[lblName] [varchar](255) NOT NULL,
	[saved] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[webService]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[webService](
	[webServiceID] [int] IDENTITY(1,1) NOT NULL,
	[webServiceURL] [varchar](256) NULL,
	[xmlName] [varchar](256) NULL,
	[userName] [varchar](256) NULL,
	[passWord] [varchar](256) NULL,
	[userID] [int] NULL,
 CONSTRAINT [PK_webService] PRIMARY KEY CLUSTERED 
(
	[webServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[z_temporaryDemoTable_client]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[z_temporaryDemoTable_client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [RigTrack].[tblBHABitData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblBHABitData](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[BHAID] [int] NULL,
	[BitSno] [nvarchar](120) NULL,
	[BitDesc] [nvarchar](max) NULL,
	[ODFrac] [nvarchar](120) NULL,
	[BitLength] [decimal](18, 2) NULL,
	[Connection] [nvarchar](120) NULL,
	[BitType] [nvarchar](50) NULL,
	[BearingType] [nvarchar](50) NULL,
	[BitMfg] [nvarchar](50) NULL,
	[BitNumber] [nvarchar](50) NULL,
	[NUMJETS] [nvarchar](50) NULL,
	[InnerRow] [nvarchar](50) NULL,
	[OuterRow] [nvarchar](50) NULL,
	[DullChar] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[BearingSeals] [nvarchar](50) NULL,
	[Guage] [nvarchar](50) NULL,
	[OtherDullChar] [nvarchar](50) NULL,
	[ReasonPulled] [nvarchar](150) NULL,
	[BittoSensor] [nvarchar](50) NULL,
	[BittoGamma] [nvarchar](50) NULL,
	[BittoResistivity] [nvarchar](50) NULL,
	[BittoPorosity] [nvarchar](50) NULL,
	[BittoDNSC] [nvarchar](50) NULL,
	[BittoGyro] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[Jet1] [decimal](18, 2) NULL,
	[Jet2] [decimal](18, 2) NULL,
	[Jet3] [decimal](18, 2) NULL,
	[Jet4] [decimal](18, 2) NULL,
	[Jet5] [decimal](18, 2) NULL,
	[Jet6] [decimal](18, 2) NULL,
	[Jet7] [decimal](18, 2) NULL,
	[Jet8] [decimal](18, 2) NULL,
	[Jet9] [decimal](18, 2) NULL,
	[Jet10] [decimal](18, 2) NULL,
 CONSTRAINT [tblBHABitData_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblBHADataInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblBHADataInfo](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[JOBID] [int] NULL,
	[BHANumber] [nvarchar](120) NULL,
	[BHADesc] [nvarchar](max) NULL,
	[BHAType] [int] NULL,
	[BitSno] [nvarchar](120) NULL,
	[BitDesc] [nvarchar](max) NULL,
	[ODFrac] [nvarchar](120) NULL,
	[BitLength] [decimal](18, 2) NULL,
	[Connection] [nvarchar](120) NULL,
	[BitType] [nvarchar](50) NULL,
	[BearingType] [nvarchar](50) NULL,
	[BitMfg] [nvarchar](50) NULL,
	[BitNumber] [nvarchar](50) NULL,
	[NUMJETS] [nvarchar](50) NULL,
	[InnerRow] [nvarchar](50) NULL,
	[OuterRow] [nvarchar](50) NULL,
	[DullChar] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[BearingSeals] [nvarchar](50) NULL,
	[Guage] [nvarchar](50) NULL,
	[OtherDullChar] [nvarchar](50) NULL,
	[ReasonPulled] [nvarchar](150) NULL,
	[MotorDesc] [nvarchar](max) NULL,
	[MotorMFG] [nvarchar](50) NULL,
	[NBStabilizer] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[Revolutions] [nvarchar](50) NULL,
	[Bend] [nvarchar](50) NULL,
	[RotorJet] [nvarchar](50) NULL,
	[BittoBend] [nvarchar](50) NULL,
	[PropBUR] [nvarchar](50) NULL,
	[RealBUR] [nvarchar](50) NULL,
	[PadOD] [nvarchar](50) NULL,
	[AverageDifferential] [nvarchar](50) NULL,
	[Lobes] [nvarchar](50) NULL,
	[OffBottomDifference] [nvarchar](50) NULL,
	[Stages] [nvarchar](50) NULL,
	[StallPressure] [nvarchar](50) NULL,
	[BittoSensor] [nvarchar](50) NULL,
	[BittoGamma] [nvarchar](50) NULL,
	[BittoResistivity] [nvarchar](50) NULL,
	[BittoPorosity] [nvarchar](50) NULL,
	[BittoDNSC] [nvarchar](50) NULL,
	[BittoGyro] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblBHADataItemsInfo]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblBHADataItemsInfo](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[BHAID] [int] NULL,
	[ToolID] [int] NULL,
 CONSTRAINT [PK_tblBHADataItemsInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblBHAMotorData]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblBHAMotorData](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CompanyID] [int] NULL,
	[JOBID] [int] NULL,
	[BHAID] [int] NULL,
	[BitSno] [nvarchar](120) NULL,
	[MotorDesc] [nvarchar](120) NULL,
	[MotorMFG] [nvarchar](120) NULL,
	[NBStabilizer] [nvarchar](120) NULL,
	[Model] [nvarchar](120) NULL,
	[Revolutions] [nvarchar](120) NULL,
	[Bend] [nvarchar](120) NULL,
	[RotorJet] [nvarchar](120) NULL,
	[BittoBend] [nvarchar](120) NULL,
	[PropBUR] [nvarchar](120) NULL,
	[RealBUR] [nvarchar](120) NULL,
	[PadOD] [nvarchar](120) NULL,
	[AverageDifferential] [nvarchar](120) NULL,
	[Lobes] [nvarchar](120) NULL,
	[OffBottomDifference] [nvarchar](120) NULL,
	[Stages] [nvarchar](120) NULL,
	[StallPressure] [nvarchar](120) NULL,
	[Clearence] [nvarchar](120) NULL,
	[AvgOnBottomSPP] [nvarchar](120) NULL,
	[AvgOffBottomSPP] [nvarchar](120) NULL,
	[NoOfStalls] [nvarchar](120) NULL,
	[StallTime] [nvarchar](120) NULL,
	[Formation] [nvarchar](120) NULL,
	[BentSubDeg] [nvarchar](120) NULL,
	[Elastomer] [nvarchar](120) NULL,
	[BendType] [nvarchar](120) NULL,
	[ClearenceRng] [nvarchar](120) NULL,
	[MEDCompany] [nvarchar](120) NULL,
	[NoOfMWDRuns] [nvarchar](120) NULL,
	[InspectionCmpny] [nvarchar](120) NULL,
	[MotorFailure] [bit] NULL,
	[ExtendedPowerSection] [bit] NULL,
	[Inspected] [bit] NULL,
	[ReasonPulled] [nvarchar](max) NULL,
	[BHAObjectives] [nvarchar](max) NULL,
	[BHAPerformance] [nvarchar](max) NULL,
	[AdditionalComments] [nvarchar](max) NULL,
	[BotStabilizerType] [nvarchar](120) NULL,
	[BotStabBladeType] [nvarchar](120) NULL,
	[BotStabLength] [nvarchar](120) NULL,
	[LowerStabOD] [nvarchar](120) NULL,
	[EvenWall] [nvarchar](120) NULL,
	[TopStabilizerType] [nvarchar](120) NULL,
	[TopStabBladeType] [nvarchar](120) NULL,
	[TopStabLength] [nvarchar](120) NULL,
	[UpperStabOD] [nvarchar](120) NULL,
	[InterferenceFit] [nvarchar](120) NULL,
	[InterferenceTol] [nvarchar](120) NULL,
	[WearPad] [nvarchar](120) NULL,
	[WearPadType] [nvarchar](120) NULL,
	[Wearpadlength] [nvarchar](120) NULL,
	[WearpadHeight] [nvarchar](120) NULL,
	[WearpadWidth] [nvarchar](120) NULL,
	[WearpadGuage] [nvarchar](120) NULL,
	[BitToWearpad] [nvarchar](120) NULL,
	[MaxSurfRPM] [nvarchar](120) NULL,
	[MaxDLRotating] [nvarchar](120) NULL,
	[MaxDLSliding] [nvarchar](120) NULL,
	[MaxDiffPress] [nvarchar](120) NULL,
	[MaxFlowRate] [nvarchar](120) NULL,
	[MaxTorque] [nvarchar](120) NULL,
 CONSTRAINT [tblBHAMotorData_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblCompany]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblCompany](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CompanyName] [nvarchar](120) NULL,
	[CompanyAddress1] [nvarchar](120) NULL,
	[CompanyAddress2] [nvarchar](80) NULL,
	[CompanyContactFirstName] [nvarchar](80) NULL,
	[CompanyContactLastName] [nvarchar](80) NULL,
	[ContactPhone] [nvarchar](25) NULL,
	[ContactEmail] [nvarchar](120) NULL,
	[City] [nvarchar](80) NULL,
	[StateID] [int] NULL,
	[CountryID] [int] NULL,
	[Zip] [nvarchar](15) NULL,
	[isAttachment] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tblCompany] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblCreateToolManufacturer]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblCreateToolManufacturer](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Manufacturer] [nvarchar](120) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [tblCreateToolManufacturer_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblCurve]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblCurve](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveGroupID] [int] NOT NULL,
	[TargetID] [int] NOT NULL,
	[Number] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[CurveTypeID] [int] NULL,
	[NorthOffset] [decimal](18, 2) NULL,
	[EastOffset] [decimal](18, 2) NULL,
	[VSDirection] [decimal](18, 2) NULL,
	[RKBElevation] [decimal](18, 2) NULL,
	[LocationID] [int] NULL,
	[BitToSensor] [decimal](18, 2) NULL,
	[ListDistanceBool] [bit] NULL,
	[ComparisonCurve] [int] NULL,
	[AtHSide] [decimal](18, 2) NULL,
	[TVDComp] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[Color] [nvarchar](25) NULL,
 CONSTRAINT [PK_tblCurve] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblCurveGroup]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblCurveGroup](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveGroupName] [nvarchar](25) NULL,
	[JobNumber] [nvarchar](25) NULL,
	[CompanyID] [int] NULL,
	[LeaseWell] [nvarchar](50) NULL,
	[JobLocation] [nvarchar](50) NULL,
	[RigName] [nvarchar](50) NULL,
	[StateID] [int] NULL,
	[CountryID] [int] NULL,
	[Declination] [decimal](18, 2) NULL,
	[Grid] [nvarchar](50) NULL,
	[RKB] [nvarchar](50) NULL,
	[GLorMSLID] [int] NULL,
	[MethodOfCalculationID] [int] NULL,
	[MeasurementUnitsID] [int] NULL,
	[UnitsConvert] [bit] NULL,
	[OutputDirectionID] [int] NULL,
	[InputDirectionID] [int] NULL,
	[DogLegSeverityID] [int] NULL,
	[VerticalSectionReferenceID] [int] NULL,
	[EWOffset] [decimal](18, 2) NULL,
	[NSOffset] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[JobStartDate] [date] NULL,
	[JobEndDate] [date] NULL,
	[Comments] [nvarchar](500) NULL,
	[isAttachment] [bit] NULL,
	[PlotComments] [nvarchar](500) NULL,
	[HasWellPlan] [bit] NULL,
	[primaryLatLong] [nvarchar](100) NULL,
 CONSTRAINT [PK_RigTrack_tblCurveGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblGraph]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblGraph](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveID] [int] NULL,
	[CurveNumber] [int] NULL,
	[TargetID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[ModeID] [int] NULL,
	[MD] [decimal](18, 2) NULL,
	[INC] [decimal](18, 2) NULL,
	[Azimuth] [decimal](18, 2) NULL,
	[TVD] [decimal](18, 2) NULL,
	[EW] [decimal](18, 2) NULL,
	[NS] [decimal](18, 2) NULL,
	[Buildrate] [decimal](18, 2) NULL,
	[Walkrate] [decimal](18, 2) NULL,
	[DLS] [decimal](18, 2) NULL,
	[HoldLen] [decimal](18, 2) NULL,
	[SpacingID] [int] NULL,
	[CurrentTrend] [bit] NULL,
	[StraightLineTVD] [bit] NULL,
	[BuildAndWalk] [bit] NULL,
	[ProposalCurve] [bit] NULL,
	[MinDLSCurve] [bit] NULL,
	[DLSCurve] [bit] NULL,
	[ShowVertical] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tblGraph] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblReport](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[HeaderComments] [nvarchar](500) NULL,
	[CompanyID] [int] NULL,
	[CurveGroupID] [int] NULL,
	[CurveID] [int] NULL,
	[TargetID] [int] NULL,
	[MeasuredDepth] [bit] NULL,
	[Inclination] [bit] NULL,
	[Azimuth] [bit] NULL,
	[TrueVerticalDepth] [bit] NULL,
	[N_SCoordinates] [bit] NULL,
	[E_WCoordinates] [bit] NULL,
	[VerticalSection] [bit] NULL,
	[ClosureDistance] [bit] NULL,
	[ClosureDirection] [bit] NULL,
	[DogLegSeverity] [bit] NULL,
	[CourseLength] [bit] NULL,
	[WalkRate] [bit] NULL,
	[BuildRate] [bit] NULL,
	[ToolFace] [bit] NULL,
	[Comment] [bit] NULL,
	[SubseaDepth] [bit] NULL,
	[Radius] [bit] NULL,
	[GridX] [bit] NULL,
	[GridY] [bit] NULL,
	[Left_Right] [bit] NULL,
	[Up_Down] [bit] NULL,
	[FNL_FSL] [bit] NULL,
	[FEL_FWL] [bit] NULL,
	[Grouping] [int] NULL,
	[BoxedComments] [bit] NULL,
	[IncludeProjectToBit] [bit] NULL,
	[ShowProjToTVDInc] [bit] NULL,
	[ExtraHeaderComments] [nvarchar](500) NULL,
	[InterpolatedReports] [bit] NULL,
	[EWNSReference] [int] NULL,
	[Mode] [int] NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[ExtraHeader] [bit] NULL,
 CONSTRAINT [PK_tblReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblSurvey]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblSurvey](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveGroupID] [int] NULL,
	[CurveID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[MD] [decimal](18, 2) NULL,
	[INC] [decimal](18, 2) NULL,
	[Azimuth] [decimal](18, 2) NULL,
	[TVD] [decimal](18, 7) NULL,
	[SubseaTVD] [decimal](18, 7) NULL,
	[NS] [decimal](18, 7) NULL,
	[EW] [decimal](18, 7) NULL,
	[VerticalSection] [decimal](18, 7) NULL,
	[CL] [decimal](18, 2) NULL,
	[ClosureDistance] [decimal](18, 7) NULL,
	[ClosureDirection] [decimal](18, 7) NULL,
	[DLS] [decimal](18, 7) NULL,
	[DLA] [decimal](18, 7) NULL,
	[BR] [decimal](18, 2) NULL,
	[WR] [decimal](18, 2) NULL,
	[TFO] [decimal](18, 2) NULL,
	[SurveyComment] [nvarchar](150) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[RowNumber] [int] NULL,
	[TieInSubseaTVD] [decimal](18, 2) NULL,
	[TieInNS] [decimal](18, 2) NULL,
	[TieInEW] [decimal](18, 2) NULL,
	[TieInVerticalSection] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblSurvey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblTarget]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblTarget](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveGroupID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[TargetShapeID] [int] NULL,
	[TVD] [decimal](18, 2) NULL,
	[NSCoordinate] [decimal](18, 2) NULL,
	[EWCoordinate] [decimal](18, 2) NULL,
	[PolarDirection] [decimal](18, 2) NULL,
	[PolarDistance] [decimal](18, 2) NULL,
	[INCFromLastTarget] [decimal](18, 2) NULL,
	[AZMFromLastTarget] [decimal](18, 2) NULL,
	[InclinationAtTarget] [decimal](18, 4) NULL,
	[AzimuthAtTarget] [decimal](18, 4) NULL,
	[NumberVertices] [decimal](18, 2) NULL,
	[Rotation] [decimal](18, 2) NULL,
	[TargetThickness] [decimal](18, 2) NULL,
	[DrawingPattern] [int] NULL,
	[TargetComment] [nvarchar](150) NULL,
	[TargetOffsetXoffset] [decimal](18, 2) NULL,
	[TargetOffsetYoffset] [decimal](18, 2) NULL,
	[DiameterOfCircleXoffset] [decimal](18, 2) NULL,
	[DiameterOfCircleYoffset] [decimal](18, 2) NULL,
	[Corner1Xofffset] [decimal](18, 2) NULL,
	[Corner1Yoffset] [decimal](18, 2) NULL,
	[Corner2Xoffset] [decimal](18, 2) NULL,
	[Corner2Yoffset] [decimal](18, 2) NULL,
	[Corner3Xoffset] [decimal](18, 2) NULL,
	[Corner3Yoffset] [decimal](18, 2) NULL,
	[Corner4Xoffset] [decimal](18, 2) NULL,
	[Corner4Yoffset] [decimal](18, 2) NULL,
	[ReferenceOptionID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[Corner5Xoffset] [decimal](18, 2) NULL,
	[Corner5Yoffset] [decimal](18, 2) NULL,
	[Corner6Xoffset] [decimal](18, 2) NULL,
	[Corner6Yoffset] [decimal](18, 2) NULL,
	[Corner7Xoffset] [decimal](18, 2) NULL,
	[Corner7Yoffset] [decimal](18, 2) NULL,
	[Corner8Xoffset] [decimal](18, 2) NULL,
	[Corner8Yoffset] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblTarget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tblTargetDetails]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tblTargetDetails](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[CurveGroupID] [int] NULL,
	[TargetShapeID] [int] NULL,
	[TVD] [decimal](18, 2) NULL,
	[NSCoordinate] [decimal](18, 2) NULL,
	[EWCoordinate] [decimal](18, 2) NULL,
	[PolarDirection] [decimal](18, 2) NULL,
	[PolarDistance] [decimal](18, 2) NULL,
	[INCFromLastTarget] [decimal](18, 2) NULL,
	[AZMFromLastTarget] [decimal](18, 2) NULL,
	[InclinationAtTarget] [decimal](18, 2) NULL,
	[AzimuthAtTarget] [decimal](18, 2) NULL,
	[NumberVertices] [decimal](18, 2) NULL,
	[Rotation] [decimal](18, 2) NULL,
	[TargetThickness] [decimal](18, 2) NULL,
	[DrawingPattern] [int] NULL,
	[TargetComment] [nvarchar](150) NULL,
	[TargetOffsetXoffset] [decimal](18, 2) NULL,
	[TargetOffsetYoffset] [decimal](18, 2) NULL,
	[DiameterOfCircleXoffset] [decimal](18, 2) NULL,
	[DiameterOfCircleYoffset] [decimal](18, 2) NULL,
	[Corner1Xofffset] [decimal](18, 2) NULL,
	[Corner1Yoffset] [decimal](18, 2) NULL,
	[Corner2Xoffset] [decimal](18, 2) NULL,
	[Corner2Yoffset] [decimal](18, 2) NULL,
	[Corner3Xoffset] [decimal](18, 2) NULL,
	[Corner3Yoffset] [decimal](18, 2) NULL,
	[Corner4Xoffset] [decimal](18, 2) NULL,
	[Corner4Yoffset] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblTargetDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpCompanyAttachments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [RigTrack].[tlkpCompanyAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Attachment] [varbinary](max) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpCompanyAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [RigTrack].[tlkpCountry]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpCountry](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CountryCode] [nvarchar](2) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Country_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpCurveGroupAttachments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [RigTrack].[tlkpCurveGroupAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CurveGroupID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Type] [nvarchar](25) NULL,
	[Attachment] [varbinary](max) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpCurveGroupAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [RigTrack].[tlkpCurveType]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpCurveType](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpCurveType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpDogLegSeverity]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpDogLegSeverity](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpDrawingPattern]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpDrawingPattern](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpDrawingPattern] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpEWNSReferences]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpEWNSReferences](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpEWNSReferences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpGLMSL]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpGLMSL](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpGLMSL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpInputOutputDirection]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpInputOutputDirection](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpInputDirection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpLocation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpLocation](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpMeasurementUnits]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpMeasurementUnits](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpMeasurementUnits] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpMethodOfCalculation]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpMethodOfCalculation](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpMethodOfCalculation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpMode]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpMode](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpMode] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpModeReport]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpModeReport](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpModeReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpReferenceOption]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpReferenceOption](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpReferenceOption] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpReportAttachments]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [RigTrack].[tlkpReportAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReportID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[Attachment] [varbinary](max) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpReportAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [RigTrack].[tlkpSpacing]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpSpacing](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpSpacing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpState]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpState](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_RigTrack]].[tlkpState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpTargetShape]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpTargetShape](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpTargetShape] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RigTrack].[tlkpVerticalSectionRef]    Script Date: 11/13/2016 1:19:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [RigTrack].[tlkpVerticalSectionRef](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](25) NULL,
	[CreateDate] [datetime] NULL,
	[LastModifyDate] [datetime] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_tlkpVerticalSectionRef] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EventTaskOrderDocuments] ADD  CONSTRAINT [DF_EventTaskOrderDocuments_mandatoryDocument]  DEFAULT ('False') FOR [mandatoryDocument]
GO
ALTER TABLE [dbo].[adminService]  WITH CHECK ADD  CONSTRAINT [FK_adminService_adminService] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([userID])
GO
ALTER TABLE [dbo].[adminService] CHECK CONSTRAINT [FK_adminService_adminService]
GO
ALTER TABLE [dbo].[assetReadings]  WITH CHECK ADD  CONSTRAINT [FK_assetReadings_assetsAttributes] FOREIGN KEY([assetAttribID])
REFERENCES [dbo].[assetsAttributes] ([ID])
GO
ALTER TABLE [dbo].[assetReadings] CHECK CONSTRAINT [FK_assetReadings_assetsAttributes]
GO
ALTER TABLE [dbo].[assetsAttributes]  WITH CHECK ADD  CONSTRAINT [FK_assetsAttributes_assets] FOREIGN KEY([assetID])
REFERENCES [dbo].[assets] ([ID])
GO
ALTER TABLE [dbo].[assetsAttributes] CHECK CONSTRAINT [FK_assetsAttributes_assets]
GO
ALTER TABLE [dbo].[assetsAttributes]  WITH CHECK ADD  CONSTRAINT [FK_assetsAttributes_attributes] FOREIGN KEY([attributeID])
REFERENCES [dbo].[attributes] ([id])
GO
ALTER TABLE [dbo].[assetsAttributes] CHECK CONSTRAINT [FK_assetsAttributes_attributes]
GO
ALTER TABLE [dbo].[dataCollectionInterval]  WITH CHECK ADD  CONSTRAINT [FK_dataCollectionInterval_dataCollectionInterval] FOREIGN KEY([dataCollectionIntervalID])
REFERENCES [dbo].[dataCollectionInterval] ([dataCollectionIntervalID])
GO
ALTER TABLE [dbo].[dataCollectionInterval] CHECK CONSTRAINT [FK_dataCollectionInterval_dataCollectionInterval]
GO
ALTER TABLE [dbo].[dataMigrationService]  WITH CHECK ADD  CONSTRAINT [FK_dataMigrationService_dataMigrationService] FOREIGN KEY([webServiceID])
REFERENCES [dbo].[webService] ([webServiceID])
GO
ALTER TABLE [dbo].[dataMigrationService] CHECK CONSTRAINT [FK_dataMigrationService_dataMigrationService]
GO
ALTER TABLE [dbo].[eventAMI_EMAIL]  WITH CHECK ADD  CONSTRAINT [FK_eventAMI_EMAIL_eventAMI] FOREIGN KEY([eventID])
REFERENCES [dbo].[eventAMI] ([id])
GO
ALTER TABLE [dbo].[eventAMI_EMAIL] CHECK CONSTRAINT [FK_eventAMI_EMAIL_eventAMI]
GO
ALTER TABLE [dbo].[eventCategory]  WITH CHECK ADD  CONSTRAINT [FK_eventCategory_events] FOREIGN KEY([eventId])
REFERENCES [dbo].[events] ([id])
GO
ALTER TABLE [dbo].[eventCategory] CHECK CONSTRAINT [FK_eventCategory_events]
GO
ALTER TABLE [dbo].[EventTaskOrderDocuments]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskOrderDocuments_Documents] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Documents] ([DocumentID])
GO
ALTER TABLE [dbo].[EventTaskOrderDocuments] CHECK CONSTRAINT [FK_EventTaskOrderDocuments_Documents]
GO
ALTER TABLE [dbo].[EventTaskOrderDocuments]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskOrderDocuments_events] FOREIGN KEY([eventCodeID])
REFERENCES [dbo].[events] ([id])
GO
ALTER TABLE [dbo].[EventTaskOrderDocuments] CHECK CONSTRAINT [FK_EventTaskOrderDocuments_events]
GO
ALTER TABLE [dbo].[EventUploadedDocuments]  WITH CHECK ADD  CONSTRAINT [FK_EventUploadedDocuments_Documents] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Documents] ([DocumentID])
GO
ALTER TABLE [dbo].[EventUploadedDocuments] CHECK CONSTRAINT [FK_EventUploadedDocuments_Documents]
GO
ALTER TABLE [dbo].[EventUploadedDocuments]  WITH CHECK ADD  CONSTRAINT [FK_EventUploadedDocuments_eventAMI] FOREIGN KEY([EventID])
REFERENCES [dbo].[eventAMI] ([id])
GO
ALTER TABLE [dbo].[EventUploadedDocuments] CHECK CONSTRAINT [FK_EventUploadedDocuments_eventAMI]
GO
ALTER TABLE [dbo].[meterData]  WITH CHECK ADD  CONSTRAINT [FK_meterData_events] FOREIGN KEY([eventId])
REFERENCES [dbo].[events] ([id])
GO
ALTER TABLE [dbo].[meterData] CHECK CONSTRAINT [FK_meterData_events]
GO
ALTER TABLE [dbo].[meterData]  WITH CHECK ADD  CONSTRAINT [FK_meterData_meter] FOREIGN KEY([meterId])
REFERENCES [dbo].[meter] ([id])
GO
ALTER TABLE [dbo].[meterData] CHECK CONSTRAINT [FK_meterData_meter]
GO
ALTER TABLE [dbo].[routineToIDlookup]  WITH CHECK ADD  CONSTRAINT [FK_routineToIDlookup_veeRoutines] FOREIGN KEY([veeRoutinesID])
REFERENCES [dbo].[veeRoutines] ([veeRoutinesID])
GO
ALTER TABLE [dbo].[routineToIDlookup] CHECK CONSTRAINT [FK_routineToIDlookup_veeRoutines]
GO
ALTER TABLE [dbo].[routineToIDlookup]  WITH CHECK ADD  CONSTRAINT [FK_routineToIDlookup_veeRules] FOREIGN KEY([veeRulesID])
REFERENCES [dbo].[veeRules] ([veeRuleID])
GO
ALTER TABLE [dbo].[routineToIDlookup] CHECK CONSTRAINT [FK_routineToIDlookup_veeRules]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [Users_Session_FK1] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([userID])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [Users_Session_FK1]
GO
ALTER TABLE [dbo].[touPeaks]  WITH CHECK ADD  CONSTRAINT [FK_touPeaks_touDayDefinitions] FOREIGN KEY([dayTypeID])
REFERENCES [dbo].[touDayDefinitions] ([id])
GO
ALTER TABLE [dbo].[touPeaks] CHECK CONSTRAINT [FK_touPeaks_touDayDefinitions]
GO
ALTER TABLE [dbo].[touPeaks]  WITH CHECK ADD  CONSTRAINT [FK_touPeaks_touPeakTypes] FOREIGN KEY([peakTypeID])
REFERENCES [dbo].[touPeakTypes] ([id])
GO
ALTER TABLE [dbo].[touPeaks] CHECK CONSTRAINT [FK_touPeaks_touPeakTypes]
GO
ALTER TABLE [dbo].[touPeaks]  WITH CHECK ADD  CONSTRAINT [FK_touPeaks_touSeasons] FOREIGN KEY([seasonID])
REFERENCES [dbo].[touSeasons] ([ID])
GO
ALTER TABLE [dbo].[touPeaks] CHECK CONSTRAINT [FK_touPeaks_touSeasons]
GO
ALTER TABLE [dbo].[touPrograms]  WITH CHECK ADD  CONSTRAINT [FK_touPrograms_progType] FOREIGN KEY([progTypeID])
REFERENCES [dbo].[progType] ([ID])
GO
ALTER TABLE [dbo].[touPrograms] CHECK CONSTRAINT [FK_touPrograms_progType]
GO
ALTER TABLE [dbo].[touSeasonPeakTypes]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonPeakTypes_touPeakTypes] FOREIGN KEY([peakTypeID])
REFERENCES [dbo].[touPeakTypes] ([id])
GO
ALTER TABLE [dbo].[touSeasonPeakTypes] CHECK CONSTRAINT [FK_touSeasonPeakTypes_touPeakTypes]
GO
ALTER TABLE [dbo].[touSeasonPeakTypes]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonPeakTypes_touPrograms] FOREIGN KEY([programID])
REFERENCES [dbo].[touPrograms] ([ID])
GO
ALTER TABLE [dbo].[touSeasonPeakTypes] CHECK CONSTRAINT [FK_touSeasonPeakTypes_touPrograms]
GO
ALTER TABLE [dbo].[touSeasonPeakTypes]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonPeakTypes_touSeasons] FOREIGN KEY([seasonID])
REFERENCES [dbo].[touSeasons] ([ID])
GO
ALTER TABLE [dbo].[touSeasonPeakTypes] CHECK CONSTRAINT [FK_touSeasonPeakTypes_touSeasons]
GO
ALTER TABLE [dbo].[touSeasonProgram]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonProgram_touSeasonProgram] FOREIGN KEY([progID])
REFERENCES [dbo].[touPrograms] ([ID])
GO
ALTER TABLE [dbo].[touSeasonProgram] CHECK CONSTRAINT [FK_touSeasonProgram_touSeasonProgram]
GO
ALTER TABLE [dbo].[touSeasonProgram]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonProgram_touSeasonProgram1] FOREIGN KEY([seasonID])
REFERENCES [dbo].[touSeasons] ([ID])
GO
ALTER TABLE [dbo].[touSeasonProgram] CHECK CONSTRAINT [FK_touSeasonProgram_touSeasonProgram1]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [FK_touSeasonsEP_touSeasonsEP0] FOREIGN KEY([PROGRAM_id])
REFERENCES [dbo].[touPrograms] ([ID])
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [FK_touSeasonsEP_touSeasonsEP0]
GO
ALTER TABLE [dbo].[touSlabRates]  WITH CHECK ADD  CONSTRAINT [FK_touSlabRates_touPeakTypes] FOREIGN KEY([peakTypeID])
REFERENCES [dbo].[touPeakTypes] ([id])
GO
ALTER TABLE [dbo].[touSlabRates] CHECK CONSTRAINT [FK_touSlabRates_touPeakTypes]
GO
ALTER TABLE [dbo].[touSlabRates]  WITH CHECK ADD  CONSTRAINT [FK_touSlabRates_touPrograms] FOREIGN KEY([programID])
REFERENCES [dbo].[touPrograms] ([ID])
GO
ALTER TABLE [dbo].[touSlabRates] CHECK CONSTRAINT [FK_touSlabRates_touPrograms]
GO
ALTER TABLE [dbo].[touSlabRates]  WITH CHECK ADD  CONSTRAINT [FK_touSlabRates_touSeasons] FOREIGN KEY([seasonID])
REFERENCES [dbo].[touSeasons] ([ID])
GO
ALTER TABLE [dbo].[touSlabRates] CHECK CONSTRAINT [FK_touSlabRates_touSeasons]
GO
ALTER TABLE [dbo].[userRolesWebservice]  WITH CHECK ADD  CONSTRAINT [FK_userRolesWebservice_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([userID])
GO
ALTER TABLE [dbo].[userRolesWebservice] CHECK CONSTRAINT [FK_userRolesWebservice_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoles] FOREIGN KEY([userRoleID])
REFERENCES [dbo].[UserRoles] ([userRoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRoles]
GO
ALTER TABLE [dbo].[UserTypePermissions]  WITH CHECK ADD  CONSTRAINT [AccessTypes_UserTypePermissions_FK1] FOREIGN KEY([accessTypeID])
REFERENCES [dbo].[AccessTypes] ([accessTypeID])
GO
ALTER TABLE [dbo].[UserTypePermissions] CHECK CONSTRAINT [AccessTypes_UserTypePermissions_FK1]
GO
ALTER TABLE [dbo].[UserTypePermissions]  WITH CHECK ADD  CONSTRAINT [FK_UserTypePermissions_Modules] FOREIGN KEY([moduleID])
REFERENCES [dbo].[Modules] ([moduleID])
GO
ALTER TABLE [dbo].[UserTypePermissions] CHECK CONSTRAINT [FK_UserTypePermissions_Modules]
GO
ALTER TABLE [dbo].[UserTypePermissions]  WITH CHECK ADD  CONSTRAINT [UserRoles_UserTypePermissions_FK1] FOREIGN KEY([userRoleID])
REFERENCES [dbo].[UserRoles] ([userRoleID])
GO
ALTER TABLE [dbo].[UserTypePermissions] CHECK CONSTRAINT [UserRoles_UserTypePermissions_FK1]
GO
ALTER TABLE [dbo].[webService]  WITH CHECK ADD  CONSTRAINT [FK_webService_Users] FOREIGN KEY([userID])
REFERENCES [dbo].[Users] ([userID])
GO
ALTER TABLE [dbo].[webService] CHECK CONSTRAINT [FK_webService_Users]
GO
ALTER TABLE [RigTrack].[tblCurve]  WITH CHECK ADD  CONSTRAINT [FK_tblCurve_tblCurveGroup] FOREIGN KEY([CurveGroupID])
REFERENCES [RigTrack].[tblCurveGroup] ([ID])
GO
ALTER TABLE [RigTrack].[tblCurve] CHECK CONSTRAINT [FK_tblCurve_tblCurveGroup]
GO
ALTER TABLE [RigTrack].[tblCurve]  WITH CHECK ADD  CONSTRAINT [FK_tblCurve_tlkpCurveType] FOREIGN KEY([CurveTypeID])
REFERENCES [RigTrack].[tlkpCurveType] ([ID])
GO
ALTER TABLE [RigTrack].[tblCurve] CHECK CONSTRAINT [FK_tblCurve_tlkpCurveType]
GO
ALTER TABLE [RigTrack].[tblCurve]  WITH CHECK ADD  CONSTRAINT [FK_tblCurve_tlkpLocation] FOREIGN KEY([LocationID])
REFERENCES [RigTrack].[tlkpLocation] ([ID])
GO
ALTER TABLE [RigTrack].[tblCurve] CHECK CONSTRAINT [FK_tblCurve_tlkpLocation]
GO
ALTER TABLE [RigTrack].[tblCurveGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblCurveGroup_tblCompany] FOREIGN KEY([CompanyID])
REFERENCES [RigTrack].[tblCompany] ([ID])
GO
ALTER TABLE [RigTrack].[tblCurveGroup] CHECK CONSTRAINT [FK_tblCurveGroup_tblCompany]
GO
ALTER TABLE [RigTrack].[tblCurveGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblCurveGroup_tblCurveGroup] FOREIGN KEY([ID])
REFERENCES [RigTrack].[tblCurveGroup] ([ID])
GO
ALTER TABLE [RigTrack].[tblCurveGroup] CHECK CONSTRAINT [FK_tblCurveGroup_tblCurveGroup]
GO
ALTER TABLE [RigTrack].[tblGraph]  WITH CHECK ADD  CONSTRAINT [FK_tblGraph_tblCurve] FOREIGN KEY([CurveID])
REFERENCES [RigTrack].[tblCurve] ([ID])
GO
ALTER TABLE [RigTrack].[tblGraph] CHECK CONSTRAINT [FK_tblGraph_tblCurve]
GO
ALTER TABLE [RigTrack].[tblGraph]  WITH CHECK ADD  CONSTRAINT [FK_tblGraph_tlkpMode] FOREIGN KEY([ModeID])
REFERENCES [RigTrack].[tlkpMode] ([ID])
GO
ALTER TABLE [RigTrack].[tblGraph] CHECK CONSTRAINT [FK_tblGraph_tlkpMode]
GO
ALTER TABLE [RigTrack].[tblGraph]  WITH CHECK ADD  CONSTRAINT [FK_tblGraph_tlkpSpacing] FOREIGN KEY([SpacingID])
REFERENCES [RigTrack].[tlkpSpacing] ([ID])
GO
ALTER TABLE [RigTrack].[tblGraph] CHECK CONSTRAINT [FK_tblGraph_tlkpSpacing]
GO
ALTER TABLE [RigTrack].[tblReport]  WITH CHECK ADD  CONSTRAINT [FK_tblReport_tblCompany] FOREIGN KEY([CompanyID])
REFERENCES [RigTrack].[tblCompany] ([ID])
GO
ALTER TABLE [RigTrack].[tblReport] CHECK CONSTRAINT [FK_tblReport_tblCompany]
GO
ALTER TABLE [RigTrack].[tblReport]  WITH CHECK ADD  CONSTRAINT [FK_tblReport_tblCurve] FOREIGN KEY([CurveID])
REFERENCES [RigTrack].[tblCurve] ([ID])
GO
ALTER TABLE [RigTrack].[tblReport] CHECK CONSTRAINT [FK_tblReport_tblCurve]
GO
ALTER TABLE [RigTrack].[tblReport]  WITH CHECK ADD  CONSTRAINT [FK_tblReport_tblCurveGroup] FOREIGN KEY([CurveGroupID])
REFERENCES [RigTrack].[tblCurveGroup] ([ID])
GO
ALTER TABLE [RigTrack].[tblReport] CHECK CONSTRAINT [FK_tblReport_tblCurveGroup]
GO
ALTER TABLE [RigTrack].[tblSurvey]  WITH CHECK ADD  CONSTRAINT [FK_tblSurvey_tblCurve] FOREIGN KEY([CurveID])
REFERENCES [RigTrack].[tblCurve] ([ID])
GO
ALTER TABLE [RigTrack].[tblSurvey] CHECK CONSTRAINT [FK_tblSurvey_tblCurve]
GO
ALTER TABLE [RigTrack].[tlkpCompanyAttachments]  WITH CHECK ADD  CONSTRAINT [FK_tlkpCompanyAttachments_tblCompany] FOREIGN KEY([CompanyID])
REFERENCES [RigTrack].[tblCompany] ([ID])
GO
ALTER TABLE [RigTrack].[tlkpCompanyAttachments] CHECK CONSTRAINT [FK_tlkpCompanyAttachments_tblCompany]
GO
ALTER TABLE [RigTrack].[tlkpCurveGroupAttachments]  WITH CHECK ADD  CONSTRAINT [FK_tlkpCurveGroupAttachments_tblCurveGroup] FOREIGN KEY([CurveGroupID])
REFERENCES [RigTrack].[tblCurveGroup] ([ID])
GO
ALTER TABLE [RigTrack].[tlkpCurveGroupAttachments] CHECK CONSTRAINT [FK_tlkpCurveGroupAttachments_tblCurveGroup]
GO
ALTER TABLE [RigTrack].[tlkpReportAttachments]  WITH CHECK ADD  CONSTRAINT [FK_tlkpReportAttachments_tblReport] FOREIGN KEY([ReportID])
REFERENCES [RigTrack].[tblReport] ([ID])
GO
ALTER TABLE [RigTrack].[tlkpReportAttachments] CHECK CONSTRAINT [FK_tlkpReportAttachments_tblReport]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_Table_2] CHECK  (([feb]='N' OR [feb]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_Table_2]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_Table_3] CHECK  (([mar]='N' OR [mar]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_Table_3]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP] CHECK  (([jan]='N' OR [jan]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_1] CHECK  (([apr]='N' OR [apr]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_1]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_2] CHECK  (([may]='N' OR [may]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_2]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_3] CHECK  (([jun]='N' OR [jun]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_3]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_4] CHECK  (([jul]='N' OR [jul]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_4]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_5] CHECK  (([aug]='N' OR [aug]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_5]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_6] CHECK  (([sep]='N' OR [sep]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_6]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_7] CHECK  (([oct]='N' OR [oct]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_7]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_8] CHECK  (([nov]='N' OR [nov]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_8]
GO
ALTER TABLE [dbo].[touSeasonsEP]  WITH CHECK ADD  CONSTRAINT [CK_touSeasonsEP_9] CHECK  (([dec]='N' OR [dec]='Y'))
GO
ALTER TABLE [dbo].[touSeasonsEP] CHECK CONSTRAINT [CK_touSeasonsEP_9]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key, Integer ie. 12345' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True or False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'bitActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Account Number As String' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key, Integer ie. 12345' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collectors', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True or False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collectors', @level2type=N'COLUMN',@level2name=N'bitActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Account Number As String' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Collectors', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'current_timestamp' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'files', @level2type=N'COLUMN',@level2name=N'created'
GO
