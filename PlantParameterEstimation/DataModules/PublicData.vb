Public Module PublicData

#Region "Variables"

	Friend FullCorrectionFactorData() As ParametricDataSet

	Friend CorrectionFormReturnType As CorrectionFactorReturnType

#End Region

#Region "Form Instances"
	Friend WithEvents MainFormInstance As MainForm
#End Region

#Region "Functions"
	Friend Function DataValidationTest(ByVal Source As Object, Optional ByVal MinVal As Double = Nothing, Optional ByVal MaxVal As Double = Nothing) As PublicData.DataValidationReturnType
		'first check to see if it is NOTHING
		If Source Is Nothing Then Return DataValidationReturnType.DataIsNothing

		Dim TempDbl As Double

		'second check to see if value is even a number
		Try
			TempDbl = CDbl(Source)
		Catch ex As Exception
			Return DataValidationReturnType.NotNumeric
		End Try

		'third check min val if it is sent
		If MinVal <> Nothing Then
			If TempDbl < MinVal Then Return DataValidationReturnType.RangeMin
		End If

		'fourth check max val if it is sent
		If MaxVal <> Nothing Then
			If TempDbl > MaxVal Then Return DataValidationReturnType.RangeMax
		End If

		'fifth check for infinity
		If Double.IsInfinity(TempDbl) Then Return DataValidationReturnType.Infinity

		'if we made it this far, then simply return valid, could add more checks here with additional enums
		Return DataValidationReturnType.Valid

	End Function

	Friend Function EPlusObject(ByVal sFieldValues As String(), ByVal EPlusFormatString As String) As String

		Const MaxSpace As Integer = 16			'Number of spaces in E+ object parameters before ! mark
		Const ObjectIndentChars As Integer = 2
		Const HangingIndentChars As Integer = 4
		'Prepare values based on input
		Dim ZeroBasedVals As Integer = sFieldValues.GetUpperBound(0)
		Dim PaddingValues(ZeroBasedVals) As String
		For I As Integer = 0 To ZeroBasedVals
			'number of spaces to add
			Dim Padding As Integer = MaxSpace - sFieldValues(I).Trim.Length
			'Minimum of 2 spaces
			Padding = Math.Max(Padding, 2)
			PaddingValues(I) = New String(" "c, Padding)
		Next
		Dim NumValsToInsert As Integer = 2 * ZeroBasedVals + 1
		Dim InsertValues(NumValsToInsert) As String
		Dim TempCounter As Integer
		Dim ObjIndentString As New String(" "c, ObjectIndentChars)
		Dim HangingIndentString As New String(" "c, HangingIndentChars)
		For I As Integer = 0 To NumValsToInsert
			If Math.IEEERemainder(I, 2) = 0 Then 'we have an even number, insert the field string
				TempCounter = I / 2
				InsertValues(I) = HangingIndentString & sFieldValues(TempCounter)
			Else 'we have an odd number, insert padding
				TempCounter = (I + 1) / 2
				InsertValues(I) = PaddingValues(TempCounter - 1)
			End If
		Next

		Return String.Format(EPlusFormatString, InsertValues)

	End Function
#End Region

#Region "Constants"
	Friend Const DecPts As Integer = 4				'Number of decimal places used when reporting coefficients
#End Region

#Region "Function Return Types"
	Friend Enum DataValidationReturnType
		Valid
		DataIsNothing
		NotNumeric
		RangeMax
		RangeMin
		Infinity
	End Enum

	Friend Enum CorrectionFactorReturnType
		_Done = 0
		_Skip = 1
		_Error = 2
		_Cancel = 3
	End Enum

	Public Structure ComponentCalculationReturn
		Dim Success As Boolean
		Dim ErrorMessage As String
		Sub New(ByVal _success As Boolean, ByVal _ErrorMessage As String)
			Me.Success = _success
			Me.ErrorMessage = _ErrorMessage
		End Sub
	End Structure
#End Region

#Region "Units"
	Public Enum UnitType
		PowerUnits = 1
		FlowUnits = 2
		TempUnits = 3
		NoUnits = 4
		PressureUnits = 5
		Length = 6
		RotationalSpeed = 7
	End Enum

	Public Enum PowerUnits
		PowerW = 0
		PowerkW = 1
		PowerBtuH = 2
		PowerMBtuH = 3
		CalculationUnit = 1
		BaseIPUnit = 3
		BaseSIUnit = 1
		MinUnitValue = 0
		MaxUnitValue = 3 'adjust as you add more units to this category
	End Enum
	Public PowerUnitStrings As String() = {"Watts", "Kilowatts", "Btu/hr", "MBtu/hr"}

	Public Enum FlowUnits
		FlowGPM = 0
		FlowCFM = 1
		FlowM3S = 2
		CalculationUnit = 2
		BaseIPUnit = 0
		BaseSIUnit = 2
		MinUnitValue = 0
		MaxUnitValue = 2 'adjust as you add more units to this category
	End Enum
	Public FlowUnitStrings As String() = {"GPM", "CFM", "m^3/s"}

	Public Enum TempUnits
		TempF = 0
		TempC = 1
		TempK = 2
		CalculationUnit = 1
		BaseIPUnit = 0
		BaseSIUnit = 1
		MinUnitValue = 0
		MaxUnitValue = 2 'adjust as you add more units to this category
	End Enum
	Public TempUnitStrings As String() = {"deg F", "deg C", "deg K"}

	Public Enum PressureUnits
		Pa = 0
		kPa = 1
		atm = 2
		PSI = 3
		CalculationUnit = 0
		BaseIPUnit = 3
		BaseSIUnit = 0
		MinUnitValue = 0
		MaxUnitValue = 3 'adjust as you add more units to this category
	End Enum
	Public PressureUnitStrings As String() = {"Pa", "kPa", "atm", "psi"}

	Public Enum LengthUnits
		Meters = 0
		Feet = 1
		Inches = 2
		Centimeters = 3
		Millimeters = 4
		CalculationUnit = 0
		BaseIPUnit = 2
		BaseSIUnit = 0
		MinUnitValue = 0
		MaxUnitValue = 4
	End Enum
	Public LengthUnitStrings As String() = {"Meters", "Feet", "Inches", "Centimeters", "Millimeters"}

	Public Enum RotationSpeedUnits
		RevsPerSecond = 0
		RevsPerMinute = 1
		RadiansPerSecond = 2
		CalculationUnit = 0
		BaseIPUnit = 1
		BaseSIUnit = 0
		MinUnitValue = 0
		MaxUnitValue = 2
	End Enum
	Public RotationSpeedUnitStrings As String() = {"Revs/Sec", "Revs/Min", "Rads/Sec"}

	Friend Function ValidateUnitIndex(ByVal UnitType As UnitType, ByVal CurIndex As Integer) As Boolean
		Select Case UnitType
			Case PublicData.UnitType.TempUnits
				If CurIndex <> PublicData.TempUnits.CalculationUnit Then Return False
			Case PublicData.UnitType.PowerUnits
				If CurIndex <> PublicData.PowerUnits.CalculationUnit Then Return False
			Case PublicData.UnitType.FlowUnits
				If CurIndex <> PublicData.FlowUnits.CalculationUnit Then Return False
			Case PublicData.UnitType.PressureUnits
				If CurIndex <> PublicData.PressureUnits.CalculationUnit Then Return False
			Case PublicData.UnitType.Length
				If CurIndex <> PublicData.LengthUnits.CalculationUnit Then Return False
			Case PublicData.UnitType.RotationalSpeed
				If CurIndex <> PublicData.RotationSpeedUnits.CalculationUnit Then Return False
			Case Else
				'UNIT_EXTENSION: Need to add catch in here to report if a value is the calculation unit or not
		End Select
		'if we made it this far, we are ok.
		Return True
	End Function

	Friend Function GetUnitStrings(ByVal UnitType As UnitType) As String()
		Select Case UnitType
			Case PublicData.UnitType.FlowUnits : Return PublicData.FlowUnitStrings
			Case PublicData.UnitType.PowerUnits : Return PublicData.PowerUnitStrings
			Case PublicData.UnitType.TempUnits : Return PublicData.TempUnitStrings
			Case PublicData.UnitType.PressureUnits : Return PublicData.PressureUnitStrings
			Case PublicData.UnitType.Length : Return PublicData.LengthUnitStrings
			Case PublicData.UnitType.RotationalSpeed : Return PublicData.RotationSpeedUnitStrings
			Case PublicData.UnitType.NoUnits : Return Nothing
			Case Else : Return Nothing 'UNIT_EXTENSION: Set array holder here
		End Select
	End Function

	Friend Function GetCalculationUnit(ByVal UnitType As UnitType) As Integer
		Select Case UnitType
			Case PublicData.UnitType.FlowUnits : Return PublicData.FlowUnits.CalculationUnit
			Case PublicData.UnitType.PowerUnits : Return PublicData.PowerUnits.CalculationUnit
			Case PublicData.UnitType.TempUnits : Return PublicData.TempUnits.CalculationUnit
			Case PublicData.UnitType.PressureUnits : Return PublicData.PressureUnits.CalculationUnit
			Case PublicData.UnitType.Length : Return PublicData.LengthUnits.CalculationUnit
			Case PublicData.UnitType.RotationalSpeed : Return PublicData.RotationSpeedUnits.CalculationUnit
			Case PublicData.UnitType.NoUnits : Return Nothing
			Case Else : Return -1 'UNIT_EXTENSION: Return proper calculation unit
		End Select
	End Function

	Friend Function GetUnitConverter(ByVal UnitType As UnitType) As Func(Of Integer, Double, Double)
		Select Case UnitType
			Case UnitType.FlowUnits
				Return New Func(Of Integer, Double, Double)(AddressOf Flowm3s)
			Case UnitType.PowerUnits
				Return New Func(Of Integer, Double, Double)(AddressOf PowerkW)
			Case UnitType.TempUnits
				Return New Func(Of Integer, Double, Double)(AddressOf TempDegC)
			Case UnitType.PressureUnits
				Return New Func(Of Integer, Double, Double)(AddressOf PressurePa)
			Case UnitType.Length
				Return New Func(Of Integer, Double, Double)(AddressOf LengthMeters)
			Case UnitType.RotationalSpeed
				Return New Func(Of Integer, Double, Double)(AddressOf RotSpeedRevPerSec)
			Case UnitType.NoUnits
				Return Nothing
			Case Else
				'UNIT_EXTENSION: Add in unit conversion capabilities
				Return Nothing
		End Select
	End Function

	Friend Function GetBaseIPUnit(ByVal UnitType As UnitType) As Integer
		Select Case UnitType
			Case PublicData.UnitType.FlowUnits : Return PublicData.FlowUnits.BaseIPUnit
			Case PublicData.UnitType.PowerUnits : Return PublicData.PowerUnits.BaseIPUnit
			Case PublicData.UnitType.TempUnits : Return PublicData.TempUnits.BaseIPUnit
			Case PublicData.UnitType.PressureUnits : Return PublicData.PressureUnits.BaseIPUnit
			Case PublicData.UnitType.Length : Return PublicData.LengthUnits.BaseIPUnit
			Case PublicData.UnitType.RotationalSpeed : Return PublicData.RotationSpeedUnits.BaseIPUnit
			Case PublicData.UnitType.NoUnits : Return Nothing
			Case Else : Return -1 'UNIT_EXTENSION: Return Base IP unit
		End Select
	End Function

	Friend Function GetBaseSIUnit(ByVal UnitType As UnitType) As Integer
		Select Case UnitType
			Case PublicData.UnitType.FlowUnits : Return PublicData.FlowUnits.BaseSIUnit
			Case PublicData.UnitType.PowerUnits : Return PublicData.PowerUnits.BaseSIUnit
			Case PublicData.UnitType.TempUnits : Return PublicData.TempUnits.BaseSIUnit
			Case PublicData.UnitType.PressureUnits : Return PublicData.PressureUnits.BaseSIUnit
			Case PublicData.UnitType.Length : Return PublicData.LengthUnits.BaseSIUnit
			Case PublicData.UnitType.RotationalSpeed : Return PublicData.RotationSpeedUnits.BaseSIUnit
			Case PublicData.UnitType.NoUnits : Return Nothing
			Case Else : Return -1 'UNIT_EXTENSION: Return Base SI unit
		End Select
	End Function

#End Region

#Region "Equipment/Fluid Data"
	'Public Enum DetailedDataType
	'	CompressorType
	'	RefrigerantType
	'	FluidType
	'	None
	'End Enum

	Public Enum CompressorType
		Scroll = 0
		Rotary = 1
		Reciprocating = 2
	End Enum
	Public CompressorStrings As String() = {"Scroll", "Rotary", "Reciprocating"}

	Public Enum RefrigerantType
		R11 = 0
		R12 = 1
		R13 = 2
		R14 = 3
		R21 = 4
		R22 = 5
		R23 = 6
		R113 = 7
		R114 = 8
		R500 = 9
		R502 = 10
		R318 = 11
	End Enum
	Public RefrigerantStrings As String() = {"R11", "R12", "R13", "R14", "R21", "R22", "R23", "R113", "R114", "R500", "R502", "R318"}

	Public Enum FluidType
		Water = 0
		Antifreeze = 1
	End Enum
	Public FluidStrings As String() = {"Water", "Antifreeze"}

	'Public Function GetDetailedDataStrings(ByVal Type As DetailedDataType) As String()
	'	Select Case Type
	'		Case DetailedDataType.CompressorType
	'			Return CompressorStrings
	'		Case DetailedDataType.RefrigerantType
	'			Return RefrigerantStrings
	'		Case DetailedDataType.FluidType
	'			Return FluidStrings
	'		Case Else
	'			Return Nothing
	'	End Select
	'End Function

#End Region

#Region "Sender Information"

	'COMPONENT_EXTENSION: 01. Add component string here, the index should match with the SenderType enumeration below
	Friend EquipNames1 As String() = _
	 {"Water-Air Htg Curve-Fit", _
	  "Water-Air Htg Waterside Curve-Fit", _
	  "Water-Air Htg Param-Est", _
	  "Water-Air Clg Curve-Fit", _
	  "Water-Water Htg Curve-Fit", _
	  "Water-Water Clg Curve-Fit", _
	  "NonDimensional Constant Speed Pump"}

	'COMPONENT_EXTENSION: 02. Add component index here, should match with the names defined in EquipNames1 above
	Public Enum SenderType
		None = -1
		HPWaterAirHeatingCurveFit = None + 1
		HPWaterAirHeatingWaterSideCurveFit = HPWaterAirHeatingCurveFit + 1
		HPWaterAirHeatingParamEst = HPWaterAirHeatingWaterSideCurveFit + 1
		HPWaterAirCoolingCurveFit = HPWaterAirHeatingParamEst + 1
		HPWaterWaterHeatingCurveFit = HPWaterAirCoolingCurveFit + 1
		HPWaterWaterCoolingCurveFit = HPWaterWaterHeatingCurveFit + 1
		Pump_ConstantSpeed_NonDimensional = HPWaterWaterCoolingCurveFit + 1
	End Enum

#End Region

#Region "Enumerations"

	Public Enum CorrectionFactorType
		Multiplier = 1
		Replacer = 2
	End Enum

	Public Enum TreeNodeType
		Root
		Type
		EquipSelection
	End Enum

	Public Enum ObjectOrParList
		EnergyPlusObject
		ParameterList
	End Enum

#End Region

#Region "Data Structures"

	Friend Structure CorrectionFactorInformation
		Dim Name As String
		Dim BaseColumnIndex As Integer
		Dim ColumnsToModify As Boolean()
		Dim NumCorrections As Integer
		Dim ModificationType As CorrectionFactorType
		Dim BaseData As Double()
		Dim AffectedData As Double(,)
		Dim CorrFactorIsAWBDBset As Boolean	 'used for factors which correct for both DB and WB in the same set
		Dim CorrFactorDBValue As Double	 'used as DB value for WB/DB correction set
		Sub Initialize()
			Name = Nothing
			BaseColumnIndex = Nothing
			ColumnsToModify = Nothing
			NumCorrections = Nothing
			ModificationType = Nothing
			BaseData = Nothing
			AffectedData = Nothing
			CorrFactorIsAWBDBset = Nothing
			CorrFactorDBValue = Nothing
		End Sub
	End Structure

	Friend Structure CboInformation
		Dim UnitType As Integer
		Dim CurIndex As Integer
	End Structure

	Friend Structure TreeNodeTag
		Dim Type As TreeNodeType
		Dim Sender As PublicData.SenderType
		Sub New(ByVal _type As TreeNodeType, ByVal _sender As PublicData.SenderType)
			Type = _type
			Sender = _sender
		End Sub
	End Structure

	Public Enum PlotTypeEnum
		LinePlot = 1
		PointPlot = 2
	End Enum

	Public Structure PlotData
		Dim ChartTitle As String
		Dim YLabel As String
		Dim SeriesData As Collections.Generic.List(Of PlotSeriesData)
		Sub New(ByVal _ChartTitle As String, ByVal _YLabel As String, ByVal _SeriesData As Collections.Generic.List(Of PlotSeriesData))
			Me.ChartTitle = _ChartTitle
			Me.YLabel = _YLabel
			Me.SeriesData = _SeriesData
		End Sub
	End Structure

	Public Structure PlotSeriesData
		Dim YValues As Double()
		Dim SeriesName As String
		Dim PlotType As PlotTypeEnum
		Dim PlotColor As Color
		Sub New(ByVal _SeriesName As String, ByVal _PlotType As PlotTypeEnum, ByVal _PlotColor As Color, ByVal _Values As Double())
			Me.YValues = _Values
			Me.SeriesName = _SeriesName
			Me.PlotType = _PlotType
			Me.PlotColor = _PlotColor
		End Sub
	End Structure

	Public Structure ThreadInitStructure
		Dim DoAbsolutePlot As Boolean
		Dim DoPercErrPlot As Boolean
		Dim DoObjectOrParameters As PublicData.ObjectOrParList
		Sub New(ByVal AbsPlot As Boolean, ByVal PercErrPlot As Boolean, ByVal OutputType As PublicData.ObjectOrParList)
			Me.DoAbsolutePlot = AbsPlot
			Me.DoPercErrPlot = PercErrPlot
			Me.DoObjectOrParameters = OutputType
		End Sub
	End Structure

#End Region

#Region "Properties (and corresponding variables)"
	Private _Sender As SenderType
	Friend Property Sender() As SenderType
		Get
			Return _Sender
		End Get
		Set(ByVal value As SenderType)
			_Sender = value
			If MainFormInstance IsNot Nothing Then Call MainFormInstance.UpdateSenderTypeNote()
		End Set
	End Property

	Private _CatalogDataInPlace As Boolean
	Public Event CatalogDataInPlaceChanged()
	''' <summary>
	''' This variable/property tells whether the catalog (tabulated) data is in place
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks>Any change in this value will reset the correction factor and catalogDataChanged property</remarks>
	Friend Property CatalogDataInPlace() As Boolean
		Get
			Return _CatalogDataInPlace
		End Get
		Set(ByVal value As Boolean)
			_CatalogDataInPlace = value
			If MainFormInstance IsNot Nothing Then Call MainFormInstance.UpdateCatalogDataNote()
			RaiseEvent CatalogDataInPlaceChanged()
			If Not _CatalogDataInPlace Then
				CorrectionDataInPlace = False
			End If
		End Set
	End Property

	Private _CorrectionDataInPlace As Boolean
	Friend Event CorrectionDataInPlaceChanged()
	''' <summary>
	''' This variable/property tells whether the correction data is in place
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks>This is used to tell whether the correction factor forms need to be re-drawn, or re-initialized</remarks>
	Friend Property CorrectionDataInPlace() As Boolean
		Get
			Return _CorrectionDataInPlace
		End Get
		Set(ByVal value As Boolean)
			_CorrectionDataInPlace = value
			If MainFormInstance IsNot Nothing Then Call MainFormInstance.UpdateCorrectionDataNote()
			RaiseEvent CorrectionDataInPlaceChanged()
		End Set
	End Property

	Friend ReadOnly Property GetHeaderStrings() As String()
		Get
			Select Case PublicData.Sender
				Case SenderType.HPWaterAirHeatingCurveFit, SenderType.HPWaterAirHeatingParamEst
					Return HeaderNameArray(PublicData.WAHPHtgHeaders)
				Case SenderType.HPWaterAirHeatingWaterSideCurveFit
					Return HeaderNameArray(PublicData.WAHPHtgWaterSideHeaders)
				Case SenderType.HPWaterAirCoolingCurveFit
					Return HeaderNameArray(PublicData.WAHPClgHeaders)
				Case SenderType.HPWaterWaterHeatingCurveFit
					Return HeaderNameArray(PublicData.WWHPHtgHeaders)
				Case SenderType.HPWaterWaterCoolingCurveFit
					Return HeaderNameArray(PublicData.WWHPClgHeaders)
				Case SenderType.Pump_ConstantSpeed_NonDimensional
					Return HeaderNameArray(PublicData.NonDimensionalConstSpeedPumpHeaders)
				Case Else
					'COMPONENT_EXTENSION: 04. Return proper column header strings, they are defined below
					Return Nothing
			End Select
		End Get
	End Property

	Friend ReadOnly Property GetHeaderUnits() As Integer()
		Get
			Select Case PublicData.Sender
				Case SenderType.HPWaterAirHeatingCurveFit, SenderType.HPWaterAirHeatingParamEst
					Return HeaderUnitArray(PublicData.WAHPHtgHeaders)
				Case SenderType.HPWaterAirHeatingWaterSideCurveFit
					Return HeaderUnitArray(PublicData.WAHPHtgWaterSideHeaders)
				Case SenderType.HPWaterAirCoolingCurveFit
					Return HeaderUnitArray(PublicData.WAHPClgHeaders)
				Case SenderType.HPWaterWaterHeatingCurveFit
					Return HeaderUnitArray(PublicData.WWHPHtgHeaders)
				Case SenderType.HPWaterWaterCoolingCurveFit
					Return HeaderUnitArray(PublicData.WWHPClgHeaders)
				Case SenderType.Pump_ConstantSpeed_NonDimensional
					Return HeaderUnitArray(PublicData.NonDimensionalConstSpeedPumpHeaders)
				Case Else
					'COMPONENT_EXTENSION: 05. Return proper column unit array, they are defined below
					Return Nothing
			End Select
		End Get
	End Property

	Friend ReadOnly Property GetDBValue() As Integer
		'returning -1 if there isn't a DB column to avoid wasting time searching
		Get
			Select Case PublicData.Sender
				Case SenderType.HPWaterAirHeatingCurveFit, SenderType.HPWaterAirHeatingParamEst
					Return GetDBColumn(WAHPHtgHeaders)
				Case SenderType.HPWaterAirHeatingWaterSideCurveFit
					Return -1
				Case SenderType.HPWaterAirCoolingCurveFit
					Return GetDBColumn(WAHPClgHeaders)
				Case SenderType.HPWaterWaterHeatingCurveFit
					Return -1
				Case SenderType.HPWaterWaterCoolingCurveFit
					Return -1
				Case SenderType.Pump_ConstantSpeed_NonDimensional
					Return -1
				Case Else
					'COMPONENT_EXTENSION: 06. Return proper dry bulb column index, they are defined below
					Return Nothing
			End Select
		End Get
	End Property

	Friend ReadOnly Property GetWBValue() As Integer
		'returning -1 if there isn't a WB column to avoid wasting time searching
		Get
			Select Case PublicData.Sender
				Case SenderType.HPWaterAirHeatingCurveFit, SenderType.HPWaterAirHeatingParamEst
					Return -1
				Case SenderType.HPWaterAirHeatingWaterSideCurveFit
					Return -1
				Case SenderType.HPWaterAirCoolingCurveFit
					Return GetWBColumn(WAHPClgHeaders)
				Case SenderType.HPWaterWaterHeatingCurveFit
					Return -1
				Case SenderType.HPWaterWaterCoolingCurveFit
					Return -1
				Case SenderType.Pump_ConstantSpeed_NonDimensional
					Return -1
				Case Else
					'COMPONENT_EXTENSION: 07. Return proper wet bulb column index, they are defined below
					Return Nothing
			End Select
		End Get
	End Property
#End Region

#Region "Column Header Declarations"
	Private Structure ColumnHeader
		Dim UnitType As UnitType
		Dim Name As String
		Dim DryBulbColumnFlag As Boolean
		Dim WetBulbColumnFlag As Boolean
		''' <summary>
		''' 
		''' </summary>
		''' <param name="n"></param>
		''' <param name="u"></param>
		''' <param name="DB"></param>
		''' <param name="WB"></param>
		''' <remarks></remarks>
		Sub New(ByVal n As String, ByVal u As UnitType, Optional ByVal DB As Boolean = False, Optional ByVal WB As Boolean = False)
			Me.UnitType = u
			Me.Name = n
			Me.DryBulbColumnFlag = DB
			Me.WetBulbColumnFlag = WB
		End Sub
	End Structure

	Function CreateHeaderArray(Of ColumnHeader)(ByVal ParamArray elements As ColumnHeader()) As ColumnHeader()
		Return elements
	End Function

	Private Function HeaderUnitArray(ByVal ComponentHeaders As List(Of ColumnHeader)) As UnitType()
		Dim TempList As New List(Of UnitType)
		For Each header As ColumnHeader In ComponentHeaders
			TempList.Add(header.UnitType)
		Next
		Return TempList.ToArray
	End Function

	Private Function HeaderNameArray(ByVal ComponentHeaders As List(Of ColumnHeader)) As String()
		Dim TempList As New List(Of String)
		For Each header As ColumnHeader In ComponentHeaders
			TempList.Add(header.Name)
		Next
		Return TempList.ToArray
	End Function

	Private Function GetDBColumn(ByVal ComponentHeaders As List(Of ColumnHeader)) As Integer
		For Each header As ColumnHeader In ComponentHeaders
			If header.DryBulbColumnFlag Then Return ComponentHeaders.IndexOf(header)
		Next
		Return -1
	End Function

	Private Function GetWBColumn(ByVal ComponentHeaders As List(Of ColumnHeader)) As Integer
		For Each header As ColumnHeader In ComponentHeaders
			If header.WetBulbColumnFlag Then Return ComponentHeaders.IndexOf(header)
		Next
		Return -1
	End Function

	Private WAHPHtgHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Load Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Load Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Heat Capacity", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Heat Absorption", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Source Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Compressor Power Input", UnitType.PowerUnits)))

	Private WAHPHtgWaterSideHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Load Side Heat Capacity", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Heat Absorption", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Source Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Compressor Power Input", UnitType.PowerUnits)))

	Private WAHPClgHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Load Side Entering Dry Bulb", UnitType.TempUnits, True), _
	 New ColumnHeader("Load Side Entering Wet Bulb", UnitType.TempUnits, False, True), _
	 New ColumnHeader("Load Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Total Heat Rate", UnitType.PowerUnits), _
	 New ColumnHeader("Load Side Sensible Heat Rate", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Source Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Source Side Heat Rejection", UnitType.PowerUnits), _
	 New ColumnHeader("Compressor Power Input", UnitType.PowerUnits)))

	Private WWHPHtgHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Source Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Source Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Load Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Heating Capacity", UnitType.PowerUnits), _
	 New ColumnHeader("Compressor Power Input", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Heat Absorption", UnitType.PowerUnits)))

	Private WWHPClgHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Source Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Source Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Entering Temp", UnitType.TempUnits), _
	 New ColumnHeader("Load Side Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Load Side Cooling Capacity", UnitType.PowerUnits), _
	 New ColumnHeader("Compressor Power Input", UnitType.PowerUnits), _
	 New ColumnHeader("Source Side Heat Rejection", UnitType.PowerUnits)))

	Private NonDimensionalConstSpeedPumpHeaders As New List(Of ColumnHeader)(CreateHeaderArray( _
	 New ColumnHeader("Volume Flow Rate", UnitType.FlowUnits), _
	 New ColumnHeader("Pressure Head", UnitType.PressureUnits)))

	'COMPONENT_EXTENSION: 03. Add unit type array and header string array here

#End Region

#Region "Error and Information Handling"
	Friend Sub ShowWarningError(ByVal Text As String)
		MessageBox.Show(Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
	End Sub

	Friend Sub ShowSevereError(ByVal Text As String)
		MessageBox.Show(Text, "Severe Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
	End Sub

	Friend Sub ShowFatalError(ByVal Text As String)
		MessageBox.Show(Text, "Fatal Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End
	End Sub

	Friend Sub ShowInfoBox(ByVal Text As String)
		MessageBox.Show(Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Friend Function ShowOKCancelBox(ByVal Text As String) As MsgBoxResult
		Return MessageBox.Show(Text, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
	End Function
#End Region

End Module
