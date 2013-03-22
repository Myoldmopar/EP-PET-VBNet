''' <summary>
''' This interface defines the required methods when implementing a new component model;
''' COMPONENT_EXTENSION: 09: Create an instance of this interface, along with a component data structure
''' </summary>
''' <remarks></remarks>
Public Interface IComponentEngine

	Property GenerateObjectOrParameterList() As PublicData.ObjectOrParList

	ReadOnly Property EnergyPlusObjectFormat() As String

	Event ProgressEvent_Initialize(ByVal maxvalue As Integer)

	Event ProgressEvent_Increment()

	Event ProgressEvent_Complete(ByVal objectentry As String)

	Event PlotEvent(ByVal PlotData As PublicData.PlotData)

	Function GenerateComponentParameters() As PublicData.ComponentCalculationReturn

	Sub GeneratePlot_Absolute()

	Sub GeneratePlot_PercentError()

End Interface

''' <summary>
''' This class operates on a single thread, handling all the individual calls to component routines as needed
''' </summary>
''' <remarks></remarks>
Public Class ThreadableInterfaceComponentManager

	Public Sub New(ByVal CompType As PublicData.SenderType, ByVal DataStructure As Object, ByVal ThreadInitDataSet As PublicData.ThreadInitStructure)
		Me._ComponentType = CompType
		Me._ComponentDataStructure = DataStructure
		Me._ThreadInitData = ThreadInitDataSet
	End Sub

	Private WithEvents _Component As IComponentEngine
	Public ReadOnly Property Component() As IComponentEngine
		Get
			Return _Component
		End Get
	End Property

	Private _ComponentDataStructure As Object
	Public ReadOnly Property ComponentDataStructure() As Object
		Get
			Return _ComponentDataStructure
		End Get
	End Property

	Private _ComponentType As PublicData.SenderType
	Public ReadOnly Property ComponentType() As PublicData.SenderType
		Get
			Return _ComponentType
		End Get
	End Property

	Private _ThreadInitData As ThreadInitStructure
	Public ReadOnly Property ThreadInitData() As ThreadInitStructure
		Get
			Return _ThreadInitData
		End Get
	End Property

	Public Sub ProcessComponent()

		Select Case ComponentType
			Case SenderType.HPWaterAirHeatingCurveFit
				Dim WAHPHtg_CurveFit As New HeatPump_WaterAir_Heating_CurveFit
				WAHPHtg_CurveFit.WAHPHeating = DirectCast(Me.ComponentDataStructure, HeatPump_WaterAir_Heating_CurveFit.WAHPHeatingData)
				Me._Component = WAHPHtg_CurveFit
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.HPWaterAirHeatingWaterSideCurveFit
				Dim WAHPHtg_WatersideCurveFit As New HeatPump_WaterAir_Heating_WaterSideCurveFit
				WAHPHtg_WatersideCurveFit.WAHPHeating = DirectCast(Me.ComponentDataStructure, HeatPump_WaterAir_Heating_WaterSideCurveFit.WAHPHeatingData)
				Me._Component = WAHPHtg_WatersideCurveFit
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.HPWaterAirHeatingParamEst
				Dim WAHPHtg_ParamEst As New HeatPump_WaterAir_Heating_ParameterEstimation
				WAHPHtg_ParamEst.WAHPHeating = DirectCast(Me.ComponentDataStructure, HeatPump_WaterAir_Heating_ParameterEstimation.WAHPHeatingData)
				Me._Component = WAHPHtg_ParamEst
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.HPWaterAirCoolingCurveFit
				Dim WAHPClg_CurveFit As New HeatPump_WaterAir_Cooling_CurveFit
				WAHPClg_CurveFit.WAHPCooling = DirectCast(Me.ComponentDataStructure, HeatPump_WaterAir_Cooling_CurveFit.WAHPCoolingData)
				Me._Component = WAHPClg_CurveFit
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.HPWaterWaterHeatingCurveFit
				Dim WWHPHtg_CurveFit As New HeatPump_WaterWater_Heating_CurveFit
				WWHPHtg_CurveFit.WWHPHeating = DirectCast(Me.ComponentDataStructure, HeatPump_WaterWater_Heating_CurveFit.WWHPHeatingData)
				Me._Component = WWHPHtg_CurveFit
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.HPWaterWaterCoolingCurveFit
				Dim WWHPCl_CurveFit As New HeatPump_WaterWater_Cooling_CurveFit
				WWHPCl_CurveFit.WWHPCooling = DirectCast(Me.ComponentDataStructure, HeatPump_WaterWater_Cooling_CurveFit.WWHPCoolingData)
				Me._Component = WWHPCl_CurveFit
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case SenderType.Pump_ConstantSpeed_NonDimensional
				Dim PumpClass As New Pumps_ConstantSpeed_NonDimensional
				PumpClass.PumpData = DirectCast(Me.ComponentDataStructure, Pumps_ConstantSpeed_NonDimensional.PumpDataStructure)
				Me._Component = PumpClass
				Me._Component.GenerateObjectOrParameterList = Me.ThreadInitData.DoObjectOrParameters
			Case Else
				'COMPONENT_EXTENSION: 15. Add more components here, passing in their data structures and setting the Me.Component object
		End Select

		Dim ReturnData As PublicData.ComponentCalculationReturn = Me.Component.GenerateComponentParameters

		If Not ReturnData.Success Then
			'warning
			PublicData.ShowSevereError(ReturnData.ErrorMessage)
			Return
		End If

		If Me.ThreadInitData.DoAbsolutePlot Then Me.Component.GeneratePlot_Absolute()

		If Me.ThreadInitData.DoPercErrPlot Then Me.Component.GeneratePlot_PercentError()

	End Sub

#Region " Relay events from child components up to the main thread "
	Public Event ProgressEvent_Initialize(ByVal maxvalue As Integer)
	Public Event ProgressEvent_Increment()
	Public Event ProgressEvent_Complete(ByVal ObjectEntry As String)
	Public Event PlotEvent(ByVal PlotData As PublicData.PlotData)

	Private Sub _Component_PlotEvent(ByVal PlotData As PublicData.PlotData) Handles _Component.PlotEvent
		RaiseEvent PlotEvent(PlotData)
	End Sub

	Private Sub _Component_ProgressEvent_Initialize(ByVal maxvalue As Integer) Handles _Component.ProgressEvent_Initialize
		RaiseEvent ProgressEvent_Initialize(maxvalue)
	End Sub

	Private Sub _Component_ProgressEvent_Increment() Handles _Component.ProgressEvent_Increment
		RaiseEvent ProgressEvent_Increment()
	End Sub

	Private Sub _Component_ProgressEvent_Complete(ByVal objectentry As String) Handles _Component.ProgressEvent_Complete
		RaiseEvent ProgressEvent_Complete(objectentry)
	End Sub
#End Region

End Class

