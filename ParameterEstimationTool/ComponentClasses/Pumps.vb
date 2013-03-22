
Public Class Pumps_ConstantSpeed_NonDimensional
	Implements IComponentEngine

	Public Structure PumpDataStructure
		
		Dim RotationalSpeed As Double
		Dim ImpellerDiameter As Double
		Dim FluidTemperature As Double 'for now it is just water
		Dim FluidDensity As Double

		Dim CatalogDataArray(,) As Single

		Dim PressureHead As List(Of Double)
		Dim PredictedPsiValue As List(Of Double)
		Dim PredictedPressureHead As List(Of Double)
		Dim VolumeFlowRate As List(Of Double)
		Dim PhiValues As List(Of Double)
		Dim PsiValues As List(Of Double)

		Sub ReInitDataLists()
			Me.PressureHead = New List(Of Double)
			Me.PredictedPsiValue = New List(Of Double)
			Me.PredictedPressureHead = New List(Of Double)
			Me.VolumeFlowRate = New List(Of Double)
			Me.PhiValues = New List(Of Double)
			Me.PsiValues = New List(Of Double)
		End Sub

	End Structure
	Public PumpData As New PumpDataStructure

	Public ReadOnly Property EnergyPlusObjectFormat() As String Implements IComponentEngine.EnergyPlusObjectFormat
		Get
			Dim Format As New System.Text.StringBuilder
			Format.AppendLine("  Pump:ConstantSpeed,")
			Format.AppendLine("    Constant Speed Pump, !- Pump Name")
			Format.AppendLine("    Pump Inlet Node, !- Inlet Node Name")
			Format.AppendLine("    Pump Outlet Node, !- Outlet Node Name")
			Format.AppendLine("    autosize,        !- Rated Flow Rate")
			Format.AppendLine("    55400.0,         !- Rated Pump Head")
			Format.AppendLine("    autosize,        !- Rated Power Consumption")
			Format.AppendLine("    0.9,             !- Motor Efficiency")
			Format.AppendLine("    1.0,             !- Fraction of Motor Inefficiencies to Fluid Stream")
			Format.AppendLine("    Intermittent,    !- Pump Control Type")
			Format.AppendLine("    ,                !- Pump Flow Rate Schedule Name")
			Format.AppendLine("    PumpHeadCurve,   !- Pump Curve Name")
			Format.AppendLine("{0},{1}!- Impeller Diameter ")
			Format.AppendLine("{2};{3}!- Impeller Speed")
			Format.AppendLine()
			Format.AppendLine("  Curve:Quartic,")
			Format.AppendLine("    PumpHeadCurve,   !- Pump Curve Name")
			Format.AppendLine("{4},{5}!- Constant")
			Format.AppendLine("{6},{7}!- 1st coefficient")
			Format.AppendLine("{8},{9}!- 2nd coefficient")
			Format.AppendLine("{10},{11}!- 3rd coefficient")
			Format.AppendLine("{12},{13}!- 4th coefficient")
			Format.AppendLine("{14},{15}!- Min Phi Value ")
			Format.AppendLine("{16};{17}!- Max Phi Value ")
			Return Format.ToString
		End Get
	End Property

	Private _GenerateObjectOrParameterList As PublicData.ObjectOrParList
	Public Property GenerateObjectOrParameterList() As PublicData.ObjectOrParList Implements IComponentEngine.GenerateObjectOrParameterList
		Get
			Return _GenerateObjectOrParameterList
		End Get
		Set(ByVal value As PublicData.ObjectOrParList)
			_GenerateObjectOrParameterList = value
		End Set
	End Property

	Public Event PlotEvent(ByVal PlotData As PublicData.PlotData) Implements IComponentEngine.PlotEvent
	Public Event ProgressEvent_Complete(ByVal objectentry As String) Implements IComponentEngine.ProgressEvent_Complete
	Public Event ProgressEvent_Increment() Implements IComponentEngine.ProgressEvent_Increment
	Public Event ProgressEvent_Initialize(ByVal maxvalue As Integer) Implements IComponentEngine.ProgressEvent_Initialize

	Public Function GenerateComponentParameters() As PublicData.ComponentCalculationReturn Implements IComponentEngine.GenerateComponentParameters

		Dim NumDataPoints As Integer = PumpData.CatalogDataArray.GetUpperBound(1) + 1	'Number of Data points
		Dim NumDataPoints_BZ As Integer = NumDataPoints - 1	'number of data points in base zero indexing

		Dim NumCoefficients As Integer = 5		'Number of Coefficients in Curve fit
		Dim NumCoefficients_BZ As Integer = NumCoefficients - 1	'number of coefficients in base zero indexing

		PumpData.FluidDensity = FluidProperties.WRHO(PumpData.FluidTemperature)

		RaiseEvent ProgressEvent_Initialize(7)

		PumpData.ReInitDataLists()

		'=============READING INPUT DATA========================
		For I As Integer = 0 To NumDataPoints_BZ
			PumpData.VolumeFlowRate.Add(PumpData.CatalogDataArray(0, I))
			PumpData.PhiValues.Add(PumpData.VolumeFlowRate(I) / (PumpData.RotationalSpeed * (PumpData.ImpellerDiameter ^ 3)))
			PumpData.PressureHead.Add(PumpData.CatalogDataArray(1, I))
			PumpData.PsiValues.Add(PumpData.PressureHead(I) / (PumpData.FluidDensity * (PumpData.RotationalSpeed ^ 2) * (PumpData.ImpellerDiameter ^ 2)))
		Next I
		RaiseEvent ProgressEvent_Increment()

		'create instances of relevant matrices
		Dim F As New MatrixLibrary.Matrix(NumDataPoints, NumCoefficients)
		Dim Y As New MatrixLibrary.Matrix(NumDataPoints, 1)
		RaiseEvent ProgressEvent_Increment()

		'Fill in the F matrix
		For I As Integer = 0 To NumDataPoints_BZ
			F(I, 0) = PumpData.PhiValues(I) ^ 4
			F(I, 1) = PumpData.PhiValues(I) ^ 3
			F(I, 2) = PumpData.PhiValues(I) ^ 2
			F(I, 3) = PumpData.PhiValues(I)
			F(I, 4) = 1
		Next
		RaiseEvent ProgressEvent_Increment()

		'Fill Y array based on which instance we are on
		For I As Integer = 0 To NumDataPoints_BZ
			Y(I, 0) = PumpData.PsiValues(I)
		Next
		RaiseEvent ProgressEvent_Increment()

		'Do the required matrix operations to get Coefficient matrix.
		Dim C As MatrixLibrary.Matrix
		Try
			C = MatrixLibrary.Matrix.SolveEqSet(F, Y)
		Catch ex As Exception
			Return New PublicData.ComponentCalculationReturn(False, "Matrix inverse routine error, could be bad input data")
		End Try
		RaiseEvent ProgressEvent_Increment()

		'Generate Outputs
		For I As Integer = 0 To NumDataPoints_BZ
			PumpData.PredictedPsiValue.Add( _
			  C(0, 0) * F(I, 0) + _
			  C(1, 0) * F(I, 1) + _
			  C(2, 0) * F(I, 2) + _
			  C(3, 0) * F(I, 3) + _
			  C(4, 0) * F(I, 4))
			PumpData.PredictedPressureHead.Add(PumpData.PredictedPsiValue(I) * PumpData.FluidDensity * (PumpData.ImpellerDiameter ^ 2) * (PumpData.RotationalSpeed ^ 2))
		Next
		RaiseEvent ProgressEvent_Increment()

		'generate text output
		Dim TextOutput As String = ""
		Select Case Me.GenerateObjectOrParameterList
			Case ObjectOrParList.EnergyPlusObject
				Dim FieldValues As New List(Of String)
				FieldValues.Add(Math.Round(PumpData.ImpellerDiameter, DecPts).ToString)
				FieldValues.Add(Math.Round(PumpData.RotationalSpeed * 60, DecPts).ToString)	'multiplied by 60 for RPM
				For I As Integer = NumCoefficients_BZ To 0 Step -1
					FieldValues.Add(Math.Round(C(I, 0), DecPts).ToString)
				Next
				FieldValues.Add(Math.Round(PumpData.PhiValues.Min, DecPts).ToString)
				FieldValues.Add(Math.Round(PumpData.PhiValues.Max, DecPts).ToString)
				TextOutput = EPlusObject(FieldValues.ToArray, Me.EnergyPlusObjectFormat())
			Case ObjectOrParList.ParameterList
				Dim TextBuilder As New System.Text.StringBuilder
				TextBuilder.AppendLine("**Begin Nomenclature**")
				TextBuilder.AppendLine("  ψ (PSI): Dimensionless Pressure Head")
				TextBuilder.AppendLine("  φ (PHI): Dimensionless Flow Rate")
				TextBuilder.AppendLine("  ΔP: Pump Pressure Head")
				TextBuilder.AppendLine("  ρ: Fluid Density")
				TextBuilder.AppendLine("  N: Pump Impeller Rotation Speed")
				TextBuilder.AppendLine("  D: Pump Impeller Diameter")
				TextBuilder.AppendLine("  a_i: Curve fit Coefficients")
				TextBuilder.AppendLine("**End Nomenclature**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Governing Equations**")
				TextBuilder.AppendLine("  ψ = ΔP / (ρ N^2 D^2)")
				TextBuilder.AppendLine("  φ = m_dot / (ρ N D^3)")
				TextBuilder.AppendLine("  ψ = (a1 * φ^4) + (a2 * φ^3) + (a3 * φ^2) + (a4 * φ) + a5")
				TextBuilder.AppendLine("**End Governing Equations**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Reporting Parameters**")
				For J As Integer = 0 To NumCoefficients_BZ
					TextBuilder.AppendLine("  Pump Curve Coefficients a" & (J + 1).ToString & ": " & Math.Round(C(J, 0), DecPts).ToString)
				Next
				TextBuilder.AppendLine("  Fluid Density: " & Math.Round(PumpData.FluidDensity, DecPts).ToString & " {kg/m3}")
				TextBuilder.AppendLine("  Pump Impeller Rotation Speed: " & Math.Round(PumpData.RotationalSpeed, DecPts).ToString & " {revs/sec}")
				TextBuilder.AppendLine("  Pump Impeller Diameter: " & Math.Round(PumpData.ImpellerDiameter, DecPts).ToString & " {m}")
				TextBuilder.AppendLine("**End Reporting Parameters**")
				TextOutput = TextBuilder.ToString
		End Select
		RaiseEvent ProgressEvent_Increment()
		RaiseEvent ProgressEvent_Complete(TextOutput)

		'now exit the function, returning completion information to the threaded caller
		GenerateComponentParameters.Success = True
		GenerateComponentParameters.ErrorMessage = ""

	End Function

	Public Sub GeneratePlot_Absolute() Implements IComponentEngine.GeneratePlot_Absolute

		Dim PlotSeries As New Collections.Generic.List(Of PublicData.PlotSeriesData)

		PlotSeries.Add(New PublicData.PlotSeriesData("Model Pressure Head", PlotTypeEnum.LinePlot, Color.Red, PumpData.PredictedPressureHead.ToArray))
		PlotSeries.Add(New PublicData.PlotSeriesData("Catalog Pressure Head", PlotTypeEnum.PointPlot, Color.Red, PumpData.PressureHead.ToArray))

		Dim MyPlotData As New PublicData.PlotData("Model vs. Catalog Data Points", "Pressure Head [Pa]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

	Public Sub GeneratePlot_PercentError() Implements IComponentEngine.GeneratePlot_PercentError

		Dim UpperBound As Integer = PumpData.PredictedPressureHead.Count - 1
		Dim PercDiff(UpperBound) As Double
		
		For I As Integer = 0 To UpperBound
			PercDiff(I) = 100 * (PumpData.PredictedPressureHead(I) - PumpData.PressureHead(I)) / PumpData.PressureHead(I)
		Next

		Dim PlotSeries As New Generic.List(Of PublicData.PlotSeriesData)
		PlotSeries.Add(New PublicData.PlotSeriesData("Pump Head % Error", PlotTypeEnum.LinePlot, Color.Red, PercDiff))
		
		Dim MyPlotData As New PublicData.PlotData("% Error", "Pump Head [%]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

End Class
