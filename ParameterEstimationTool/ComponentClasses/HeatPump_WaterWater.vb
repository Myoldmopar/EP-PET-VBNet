Public Class HeatPump_WaterWater_Heating_CurveFit
	Implements IComponentEngine

	Public Structure WWHPHeatingData

		Dim RatedLoadVolFlowRate As Single
		Dim RatedSourceVolFlowRate As Single
		Dim RatedTotalCapacity As Single
		Dim RatedCompressorPower As Single

		Dim CatalogDataArray(,) As Single

		Dim SourceSideInletTemp As List(Of Double)
		Dim SourceSideVolFlowRate As List(Of Double)
		Dim LoadSideInletTemp As List(Of Double)
		Dim LoadSideVolFlowRate As List(Of Double)
		Dim TotalHeatingCapacity As List(Of Double)
		Dim CompressorPower As List(Of Double)
		Dim HeatAbsorption As List(Of Double)
		Dim PredictedTotHtgCapacity As List(Of Double)
		Dim PredictedSourceHtgRate As List(Of Double)
		Dim PredictedPowerInput As List(Of Double)

		Sub ReInitDataLists()
			LoadSideInletTemp = New List(Of Double)
			LoadSideVolFlowRate = New List(Of Double)
			SourceSideInletTemp = New List(Of Double)
			SourceSideVolFlowRate = New List(Of Double)
			TotalHeatingCapacity = New List(Of Double)
			HeatAbsorption = New List(Of Double)
			CompressorPower = New List(Of Double)
			PredictedTotHtgCapacity = New List(Of Double)
			PredictedSourceHtgRate = New List(Of Double)
			PredictedPowerInput = New List(Of Double)
		End Sub

	End Structure
	Public WWHPHeating As New WWHPHeatingData

	Private _GenerateObjectOrParameterList As PublicData.ObjectOrParList
	Public Property GenerateObjectOrParameterList() As PublicData.ObjectOrParList Implements IComponentEngine.GenerateObjectOrParameterList
		Get
			Return _GenerateObjectOrParameterList
		End Get
		Set(ByVal value As PublicData.ObjectOrParList)
			_GenerateObjectOrParameterList = value
		End Set
	End Property

	Public ReadOnly Property EnergyPlusObjectFormat() As String Implements IComponentEngine.EnergyPlusObjectFormat
		Get
			Dim Format As New System.Text.StringBuilder
			Format.AppendLine("HeatPump:WaterToWater:EquationFit:Heating,")
			Format.AppendLine("{0},{1}!-Name")
			Format.AppendLine("{2},{3}!-Source Side Inlet Node Name")
			Format.AppendLine("{4},{5}!-Source Side Outlet Node Name")
			Format.AppendLine("{6},{7}!-Load Side Inlet Node Name")
			Format.AppendLine("{8},{9}!-Load Side Outlet Node Name")
			Format.AppendLine("{10},{11}!-Rated Load Side Flow Rate")
			Format.AppendLine("{12},{13}!-Rated Source Side Flow Rate")
			Format.AppendLine("{14},{15}!-Rated Heating Capacity")
			Format.AppendLine("{16},{17}!-Rated Heating COP")
			Format.AppendLine("{18},{19}!-Heating Capacity Coefficient 1")
			Format.AppendLine("{20},{21}!-Heating Capacity Coefficient 2")
			Format.AppendLine("{22},{23}!-Heating Capacity Coefficient 3")
			Format.AppendLine("{24},{25}!-Heating Capacity Coefficient 4")
			Format.AppendLine("{26},{27}!-Heating Capacity Coefficient 5")
			Format.AppendLine("{28},{29}!-Heating Compressor Power Coefficient 1")
			Format.AppendLine("{30},{31}!-Heating Compressor Power Coefficient 2")
			Format.AppendLine("{32},{33}!-Heating Compressor Power Coefficient 3")
			Format.AppendLine("{34},{35}!-Heating Compressor Power Coefficient 4")
			Format.AppendLine("{36},{37}!-Heating Compressor Power Coefficient 5")
			Format.AppendLine("{38};{39}!-Cycle Time")
			Return Format.ToString
		End Get
	End Property

	Public Event PlotEvent(ByVal PlotData As PublicData.PlotData) Implements IComponentEngine.PlotEvent
	Public Event ProgressEvent_Complete(ByVal objectentry As String) Implements IComponentEngine.ProgressEvent_Complete
	Public Event ProgressEvent_Increment() Implements IComponentEngine.ProgressEvent_Increment
	Public Event ProgressEvent_Initialize(ByVal maxvalue As Integer) Implements IComponentEngine.ProgressEvent_Initialize

	Public Function GenerateComponentParameters() As PublicData.ComponentCalculationReturn Implements IComponentEngine.GenerateComponentParameters

		Dim NumDataPoints As Integer = WWHPHeating.CatalogDataArray.GetUpperBound(1) + 1
		Dim NumDataPoints_BZ As Integer = NumDataPoints - 1

		Dim NumCoefficients As Integer = 5
		Dim NumCoefficients_BZ As Integer = NumCoefficients - 1

		RaiseEvent ProgressEvent_Initialize(13)

		Const CelciusToKelvin As Double = 273.15
		Const Tref As Double = 283.15					 'reference temperature = 10C
		Const InstanceCalcCapacity As Integer = 1
		Const InstanceCalcPower As Integer = 2

		WWHPHeating.ReInitDataLists()

		'=============READING INPUT DATA========================
		'Read in the catalog data input
		'*1000=Change kW to W
		For I As Integer = 0 To NumDataPoints_BZ
			WWHPHeating.SourceSideInletTemp.Add(WWHPHeating.CatalogDataArray(0, I))
			WWHPHeating.SourceSideVolFlowRate.Add(WWHPHeating.CatalogDataArray(1, I))
			WWHPHeating.LoadSideInletTemp.Add(WWHPHeating.CatalogDataArray(2, I))
			WWHPHeating.LoadSideVolFlowRate.Add(WWHPHeating.CatalogDataArray(3, I))
			WWHPHeating.TotalHeatingCapacity.Add(WWHPHeating.CatalogDataArray(4, I) * 1000)
			WWHPHeating.CompressorPower.Add(WWHPHeating.CatalogDataArray(5, I) * 1000)
			WWHPHeating.HeatAbsorption.Add(WWHPHeating.CatalogDataArray(6, I) * 1000)
		Next I
		RaiseEvent ProgressEvent_Increment()

		Dim C1 As MatrixLibrary.Matrix = Nothing
		Dim C2 As MatrixLibrary.Matrix = Nothing

		For InstanceNum As Integer = InstanceCalcCapacity To InstanceCalcPower

			'Redimension all arrays to the required size determined by number of coefficients and number of data points.
			Dim F As New MatrixLibrary.Matrix(NumDataPoints, NumCoefficients)
			Dim Y As New MatrixLibrary.Matrix(NumDataPoints, 1)
			RaiseEvent ProgressEvent_Increment()

			'Fill in the F matrix
			For I As Integer = 0 To NumDataPoints_BZ
				F(I, 0) = 1
				F(I, 1) = ((WWHPHeating.LoadSideInletTemp(I) + CelciusToKelvin) / Tref)
				F(I, 2) = ((WWHPHeating.SourceSideInletTemp(I) + CelciusToKelvin) / Tref)
				F(I, 3) = (WWHPHeating.LoadSideVolFlowRate(I) / WWHPHeating.RatedLoadVolFlowRate)
				F(I, 4) = (WWHPHeating.SourceSideVolFlowRate(I) / WWHPHeating.RatedSourceVolFlowRate)
			Next
			RaiseEvent ProgressEvent_Increment()

			'Fill Y array based on which instance we are on
			For I As Integer = 0 To NumDataPoints_BZ
				Select Case InstanceNum
					Case InstanceCalcCapacity
						Y(I, 0) = WWHPHeating.TotalHeatingCapacity(I) / WWHPHeating.RatedTotalCapacity
					Case InstanceCalcPower
						Y(I, 0) = WWHPHeating.CompressorPower(I) / WWHPHeating.RatedCompressorPower
				End Select
			Next I
			RaiseEvent ProgressEvent_Increment()

			'Do the required matrix operations to get Coefficient matrix.
			Try
				Select Case InstanceNum
					Case InstanceCalcCapacity
						C1 = MatrixLibrary.Matrix.SolveEqSet(F, Y)
					Case InstanceCalcPower
						C2 = MatrixLibrary.Matrix.SolveEqSet(F, Y)
				End Select
			Catch ex As Exception
				Return New PublicData.ComponentCalculationReturn(False, "Matrix inverse routine error, could be bad input data")
			End Try
			RaiseEvent ProgressEvent_Increment()

			'Generate Outputs
			For I As Integer = 0 To NumDataPoints_BZ
				Select Case InstanceNum
					Case InstanceCalcCapacity
						WWHPHeating.PredictedTotHtgCapacity.Add(WWHPHeating.RatedTotalCapacity * _
						(C1(0, 0) * F(I, 0) + _
						 C1(1, 0) * F(I, 1) + _
						 C1(2, 0) * F(I, 2) + _
						 C1(3, 0) * F(I, 3) + _
						 C1(4, 0) * F(I, 4)))

					Case InstanceCalcPower
						WWHPHeating.PredictedPowerInput.Add(WWHPHeating.RatedCompressorPower * _
						(C2(0, 0) * F(I, 0) + _
						 C2(1, 0) * F(I, 1) + _
						 C2(2, 0) * F(I, 2) + _
						 C2(3, 0) * F(I, 3) + _
						 C2(4, 0) * F(I, 4)))

						WWHPHeating.PredictedSourceHtgRate.Add(WWHPHeating.PredictedTotHtgCapacity(I) - WWHPHeating.PredictedPowerInput(I))

				End Select
			Next
			RaiseEvent ProgressEvent_Increment()

		Next
		RaiseEvent ProgressEvent_Increment()

		'output the data
		Dim TextOutput As String = ""
		Select Case Me.GenerateObjectOrParameterList
			Case ObjectOrParList.EnergyPlusObject
				Dim FieldValues As New List(Of String)
				FieldValues.Add("Your Heating Coil Name")
				FieldValues.Add("Your Coil Source Side Inlet Node")
				FieldValues.Add("Your Coil Source Side Outlet Node")
				FieldValues.Add("Your Coil Load Side Inlet Node")
				FieldValues.Add("Your Coil Load Side Outlet Node")
				FieldValues.Add(WWHPHeating.RatedLoadVolFlowRate)
				FieldValues.Add(WWHPHeating.RatedSourceVolFlowRate)
				FieldValues.Add(WWHPHeating.RatedTotalCapacity)
				FieldValues.Add(Math.Round((WWHPHeating.RatedTotalCapacity / WWHPHeating.RatedCompressorPower), DecPts).ToString)
				For J As Integer = 0 To NumCoefficients_BZ
					FieldValues.Add(Math.Round(C1(J, 0), DecPts).ToString)
				Next
				For J As Integer = 0 To NumCoefficients_BZ
					FieldValues.Add(Math.Round(C2(J, 0), DecPts).ToString)
				Next
				FieldValues.Add("Your HP Cycle Time")
				TextOutput = EPlusObject(FieldValues.ToArray, Me.EnergyPlusObjectFormat())
			Case ObjectOrParList.ParameterList
				Dim TextBuilder As New System.Text.StringBuilder
				TextBuilder.AppendLine("**Begin Nomenclature**")
				TextBuilder.AppendLine("HC: Heating Capacity")
				TextBuilder.AppendLine("HP: Heating Power Consumption")
				TextBuilder.AppendLine("TLI: Entering Load-side Temperature")
				TextBuilder.AppendLine("TSI: Entering Source-side Temperature")
				TextBuilder.AppendLine("VLI: Entering Load-side Flow Rate")
				TextBuilder.AppendLine("VSI: Entering Source-side Flow Rate")
				TextBuilder.AppendLine("Subscript _R: Rated Value")
				TextBuilder.AppendLine("Subscript _#: Coefficient #")
				TextBuilder.AppendLine("**End Nomenclature**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Governing Equations**")
				TextBuilder.AppendLine("(HC/HC_R) = HC_1 + HC_2*(TLI/TLI_R) + HC_3*(TSI/TSI_R) + HC_4*(VLI/VLI_R) + HC_5*(VSI/VSI_R)")
				TextBuilder.AppendLine("(HP/HP_R) = HP_1 + HP_2*(TLI/TLI_R) + HP_3*(TSI/TSI_R) + HP_4*(VLI/VLI_R) + HP_5*(VSI/VSI_R)")
				TextBuilder.AppendLine("**End Governing Equations**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Reporting Parameters**")
				For J As Integer = 0 To NumCoefficients_BZ
					TextBuilder.AppendLine("Heating Capacity Coefficient HC_" & (J + 1).ToString & ": " & Math.Round(C1(J, 0), DecPts).ToString)
				Next
				For J As Integer = 0 To NumCoefficients_BZ
					TextBuilder.AppendLine("Heating Power Consumption Coefficient HP_" & (J + 1).ToString & ": " & Math.Round(C2(J, 0), DecPts).ToString)
				Next
				TextBuilder.AppendLine("Rated Load-side Heating Capacity: " & WWHPHeating.RatedTotalCapacity.ToString & "{kW}")
				TextBuilder.AppendLine("Rated Heating Power Consumption: " & WWHPHeating.RatedCompressorPower.ToString & "{kW}")
				TextBuilder.AppendLine("Rated Load-side Volumetric Flow Rate: " & WWHPHeating.RatedLoadVolFlowRate.ToString & "{m3/s}")
				TextBuilder.AppendLine("Rated Source-side Volumetric Flow Rate: " & WWHPHeating.RatedSourceVolFlowRate.ToString & "{m3/s}")
				TextBuilder.AppendLine("**End Reporting Parameters**")
				TextOutput = TextBuilder.ToString
		End Select
		RaiseEvent ProgressEvent_Increment()
		RaiseEvent ProgressEvent_Complete(TextOutput)

		GenerateComponentParameters.Success = True
		GenerateComponentParameters.ErrorMessage = ""
	End Function

	Public Sub GeneratePlot_Absolute() Implements IComponentEngine.GeneratePlot_Absolute

		Dim PlotSeries As New List(Of PublicData.PlotSeriesData)
		PlotSeries.Add(New PlotSeriesData("Load HT Model Output", PlotTypeEnum.LinePlot, Color.Red, WWHPHeating.PredictedTotHtgCapacity.ToArray))
		PlotSeries.Add(New PlotSeriesData("Load HT Catalog Data", PlotTypeEnum.PointPlot, Color.Red, WWHPHeating.TotalHeatingCapacity.ToArray))
		PlotSeries.Add(New PlotSeriesData("Source HT Model Output", PlotTypeEnum.LinePlot, Color.BlueViolet, WWHPHeating.PredictedSourceHtgRate.ToArray))
		PlotSeries.Add(New PlotSeriesData("Source HT Catalog Data", PlotTypeEnum.PointPlot, Color.BlueViolet, WWHPHeating.HeatAbsorption.ToArray))
		PlotSeries.Add(New PlotSeriesData("Compressor Power Model Output", PlotTypeEnum.LinePlot, Color.Green, WWHPHeating.PredictedPowerInput.ToArray))
		PlotSeries.Add(New PlotSeriesData("Compressor Power Catalog Data", PlotTypeEnum.PointPlot, Color.Green, WWHPHeating.CompressorPower.ToArray))

		Dim MyPlotData As New PublicData.PlotData("Model vs. Catalog Data Points", "Heat Transfer Rate/Power [W]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

	Public Sub GeneratePlot_PercentError() Implements IComponentEngine.GeneratePlot_PercentError
		Dim UpperBound As Integer = WWHPHeating.PredictedTotHtgCapacity.Count - 1
		Dim PercDiffTotal(UpperBound) As Double
		Dim PercDiffSource(UpperBound) As Double
		Dim PercDiffPower(UpperBound) As Double

		For I As Integer = 1 To UpperBound
			PercDiffTotal(I) = 100 * (WWHPHeating.PredictedTotHtgCapacity(I) - WWHPHeating.TotalHeatingCapacity(I)) / WWHPHeating.TotalHeatingCapacity(I)
			PercDiffSource(I) = 100 * (WWHPHeating.PredictedSourceHtgRate(I) - WWHPHeating.HeatAbsorption(I)) / WWHPHeating.HeatAbsorption(I)
			PercDiffPower(I) = 100 * (WWHPHeating.PredictedPowerInput(I) - WWHPHeating.CompressorPower(I)) / WWHPHeating.CompressorPower(I)
		Next

		Dim PlotSeries As New Generic.List(Of PublicData.PlotSeriesData)
		PlotSeries.Add(New PublicData.PlotSeriesData("Load HT % Error", PlotTypeEnum.LinePlot, Color.Red, PercDiffTotal))
		PlotSeries.Add(New PublicData.PlotSeriesData("Source HT % Error", PlotTypeEnum.LinePlot, Color.BlueViolet, PercDiffSource))
		PlotSeries.Add(New PublicData.PlotSeriesData("Comp. Power % Error", PlotTypeEnum.LinePlot, Color.Green, PercDiffPower))

		Dim MyPlotData As New PublicData.PlotData("% Error", "Heat Transfer Rate/Power [%]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

End Class

Public Class HeatPump_WaterWater_Cooling_CurveFit
	Implements IComponentEngine

	Public Structure WWHPCoolingData

		Dim RatedLoadVolFlowRate As Single
		Dim RatedSourceVolFlowRate As Single
		Dim RatedTotalCapacity As Single
		Dim RatedCompressorPower As Single

		Dim CatalogDataArray(,) As Single

		Dim SourceSideInletTemp As List(Of Double)
		Dim SourceSideVolFlowRate As List(Of Double)
		Dim LoadSideInletTemp As List(Of Double)
		Dim LoadSideVolFlowRate As List(Of Double)
		Dim TotalCoolingCapacity As List(Of Double)
		Dim CompressorPower As List(Of Double)
		Dim HeatRejection As List(Of Double)
		Dim PredictedTotClgCapacity As List(Of Double)
		Dim PredictedSourceHeatRate As List(Of Double)
		Dim PredictedPowerInput As List(Of Double)

		Sub ReInitDataLists()
			LoadSideInletTemp = New List(Of Double)
			LoadSideVolFlowRate = New List(Of Double)
			SourceSideInletTemp = New List(Of Double)
			SourceSideVolFlowRate = New List(Of Double)
			TotalCoolingCapacity = New List(Of Double)
			HeatRejection = New List(Of Double)
			CompressorPower = New List(Of Double)
			PredictedTotClgCapacity = New List(Of Double)
			PredictedSourceHeatRate = New List(Of Double)
			PredictedPowerInput = New List(Of Double)
		End Sub

	End Structure
	Public WWHPCooling As New WWHPCoolingData

	Private _GenerateObjectOrParameterList As PublicData.ObjectOrParList
	Public Property GenerateObjectOrParameterList() As PublicData.ObjectOrParList Implements IComponentEngine.GenerateObjectOrParameterList
		Get
			Return _GenerateObjectOrParameterList
		End Get
		Set(ByVal value As PublicData.ObjectOrParList)
			_GenerateObjectOrParameterList = value
		End Set
	End Property

	Public ReadOnly Property EnergyPlusObjectFormat() As String Implements IComponentEngine.EnergyPlusObjectFormat
		Get
			Dim Format As New System.Text.StringBuilder
			Format.AppendLine("HeatPump:WaterToWater:EquationFit:Cooling,")
			Format.AppendLine("{0},{1}!-Name")
			Format.AppendLine("{2},{3}!-Source Side Inlet Node Name")
			Format.AppendLine("{4},{5}!-Source Side Outlet Node Name")
			Format.AppendLine("{6},{7}!-Load Side Inlet Node Name")
			Format.AppendLine("{8},{9}!-Load Side Outlet Node Name")
			Format.AppendLine("{10},{11}!-Rated Load Side Flow Rate")
			Format.AppendLine("{12},{13}!-Rated Source Side Flow Rate")
			Format.AppendLine("{14},{15}!-Rated Cooling Capacity")
			Format.AppendLine("{16},{17}!-Rated Cooling COP")
			Format.AppendLine("{18},{19}!-Cooling Capacity Coefficient 1")
			Format.AppendLine("{20},{21}!-Cooling Capacity Coefficient 2")
			Format.AppendLine("{22},{23}!-Cooling Capacity Coefficient 3")
			Format.AppendLine("{24},{25}!-Cooling Capacity Coefficient 4")
			Format.AppendLine("{26},{27}!-Cooling Capacity Coefficient 5")
			Format.AppendLine("{28},{29}!-Cooling Compressor Power Coefficient 1")
			Format.AppendLine("{30},{31}!-Cooling Compressor Power Coefficient 2")
			Format.AppendLine("{32},{33}!-Cooling Compressor Power Coefficient 3")
			Format.AppendLine("{34},{35}!-Cooling Compressor Power Coefficient 4")
			Format.AppendLine("{36},{37}!-Cooling Compressor Power Coefficient 5")
			Format.AppendLine("{38};{39}!-Cycle Time")
			Return Format.ToString
		End Get
	End Property

	Public Event PlotEvent(ByVal PlotData As PublicData.PlotData) Implements IComponentEngine.PlotEvent
	Public Event ProgressEvent_Complete(ByVal objectentry As String) Implements IComponentEngine.ProgressEvent_Complete
	Public Event ProgressEvent_Increment() Implements IComponentEngine.ProgressEvent_Increment
	Public Event ProgressEvent_Initialize(ByVal maxvalue As Integer) Implements IComponentEngine.ProgressEvent_Initialize

	Public Function GenerateComponentParameters() As PublicData.ComponentCalculationReturn Implements IComponentEngine.GenerateComponentParameters

		Dim NumDataPoints As Integer = WWHPCooling.CatalogDataArray.GetUpperBound(1) + 1
		Dim NumDataPoints_BZ As Integer = NumDataPoints - 1

		Dim NumCoefficients As Integer = 5
		Dim NumCoefficients_BZ As Integer = NumCoefficients - 1

		RaiseEvent ProgressEvent_Initialize(13)

		Const CelciusToKelvin As Double = 273.15
		Const Tref As Double = 283.15					'reference temperature = 10C
		Const InstanceCalcCapacity As Integer = 1
		Const InstanceCalcPower As Integer = 2

		WWHPCooling.ReInitDataLists()

		'=============READING INPUT DATA========================
		'Read in the catalog data input
		'*1000=Change kW to W
		For I As Integer = 0 To NumDataPoints_BZ
			WWHPCooling.SourceSideInletTemp.Add(WWHPCooling.CatalogDataArray(0, I))
			WWHPCooling.SourceSideVolFlowRate.Add(WWHPCooling.CatalogDataArray(1, I))
			WWHPCooling.LoadSideInletTemp.Add(WWHPCooling.CatalogDataArray(2, I))
			WWHPCooling.LoadSideVolFlowRate.Add(WWHPCooling.CatalogDataArray(3, I))
			WWHPCooling.TotalCoolingCapacity.Add(WWHPCooling.CatalogDataArray(4, I) * 1000)
			WWHPCooling.CompressorPower.Add(WWHPCooling.CatalogDataArray(5, I) * 1000)
			WWHPCooling.HeatRejection.Add(WWHPCooling.CatalogDataArray(6, I) * 1000)
		Next I
		RaiseEvent ProgressEvent_Increment()

		Dim C1 As MatrixLibrary.Matrix = Nothing
		Dim C2 As MatrixLibrary.Matrix = Nothing

		For InstanceNum As Integer = InstanceCalcCapacity To InstanceCalcPower

			'Redimension all arrays to the required size determined by number of coefficients and number of data points.
			Dim F As New MatrixLibrary.Matrix(NumDataPoints, NumCoefficients)
			Dim Y As New MatrixLibrary.Matrix(NumDataPoints, 1)
			RaiseEvent ProgressEvent_Increment()

			'Fill in the F matrix. This calls the function Func().
			For I As Integer = 0 To NumDataPoints_BZ
				F(I, 0) = 1
				F(I, 1) = ((WWHPCooling.LoadSideInletTemp(I) + CelciusToKelvin) / Tref)
				F(I, 2) = ((WWHPCooling.SourceSideInletTemp(I) + CelciusToKelvin) / Tref)
				F(I, 3) = (WWHPCooling.LoadSideVolFlowRate(I) / WWHPCooling.RatedLoadVolFlowRate)
				F(I, 4) = (WWHPCooling.SourceSideVolFlowRate(I) / WWHPCooling.RatedSourceVolFlowRate)
			Next I
			RaiseEvent ProgressEvent_Increment()

			'Fill Y array based on instance
			For I As Integer = 0 To NumDataPoints_BZ
				Select Case InstanceNum
					Case InstanceCalcCapacity
						Y(I, 0) = WWHPCooling.TotalCoolingCapacity(I) / WWHPCooling.RatedTotalCapacity
					Case InstanceCalcPower
						Y(I, 0) = WWHPCooling.CompressorPower(I) / WWHPCooling.RatedCompressorPower
				End Select
			Next I
			RaiseEvent ProgressEvent_Increment()

			'Do the required matrix operations to get Coefficient matrix.
			Try
				Select Case InstanceNum
					Case InstanceCalcCapacity
						C1 = MatrixLibrary.Matrix.SolveEqSet(F, Y)
					Case InstanceCalcPower
						C2 = MatrixLibrary.Matrix.SolveEqSet(F, Y)
				End Select
			Catch ex As Exception
				Return New PublicData.ComponentCalculationReturn(False, "Matrix inverse routine error, could be bad input data")
			End Try
			RaiseEvent ProgressEvent_Increment()

			'Generate Outputs
			For I As Integer = 0 To NumDataPoints_BZ
				Select Case InstanceNum
					Case InstanceCalcCapacity
						WWHPCooling.PredictedTotClgCapacity.Add(WWHPCooling.RatedTotalCapacity * _
						(C1(0, 0) * F(I, 0) + _
						 C1(1, 0) * F(I, 1) + _
						 C1(2, 0) * F(I, 2) + _
						 C1(3, 0) * F(I, 3) + _
						 C1(4, 0) * F(I, 4)))

					Case InstanceCalcPower
						WWHPCooling.PredictedPowerInput.Add(WWHPCooling.RatedCompressorPower * _
						(C2(0, 0) * F(I, 0) + _
						 C2(1, 0) * F(I, 1) + _
						 C2(2, 0) * F(I, 2) + _
						 C2(3, 0) * F(I, 3) + _
						 C2(4, 0) * F(I, 4)))

						WWHPCooling.PredictedSourceHeatRate.Add(WWHPCooling.PredictedTotClgCapacity(I) - WWHPCooling.PredictedPowerInput(I))

				End Select
			Next
			RaiseEvent ProgressEvent_Increment()

		Next InstanceNum
		RaiseEvent ProgressEvent_Increment()

		'output the data
		Dim TextOutput As String = ""
		Select Case Me.GenerateObjectOrParameterList
			Case ObjectOrParList.EnergyPlusObject
				Dim FieldValues As New List(Of String)
				FieldValues.Add("Your Cooling Coil Name")
				FieldValues.Add("Your Coil Source Side Inlet Node")
				FieldValues.Add("Your Coil Source Side Outlet Node")
				FieldValues.Add("Your Coil Load Side Inlet Node")
				FieldValues.Add("Your Coil Load Side Outlet Node")
				FieldValues.Add(WWHPCooling.RatedLoadVolFlowRate)
				FieldValues.Add( WWHPCooling.RatedSourceVolFlowRate)
				FieldValues.Add(WWHPCooling.RatedTotalCapacity * 1000) 'convert to watts
				FieldValues.Add(WWHPCooling.RatedCompressorPower * 1000) 'convert to watts
				For J As Integer = 0 To NumCoefficients_BZ
					FieldValues.Add(Math.Round(C1(J, 0), DecPts).ToString)
				Next
				For J As Integer = 0 To NumCoefficients_BZ
					FieldValues.Add(Math.Round(C2(J, 0), DecPts).ToString)
				Next
				FieldValues(19) = "Your HP Cycle Time"
				TextOutput = EPlusObject(FieldValues.ToArray, Me.EnergyPlusObjectFormat())
			Case ObjectOrParList.ParameterList
				Dim TextBuilder As New System.Text.StringBuilder
				TextBuilder.AppendLine("**Begin Nomenclature**")
				TextBuilder.AppendLine("CC: Cooling Capacity")
				TextBuilder.AppendLine("CP: Cooling Power Consumption")
				TextBuilder.AppendLine("TLI: Entering Load Temperature")
				TextBuilder.AppendLine("TSI: Entering Source Temperature")
				TextBuilder.AppendLine("VLI: Entering Load Flow Rate")
				TextBuilder.AppendLine("VSI: Entering Source Flow Rate")
				TextBuilder.AppendLine("Subscript _R: Rated Value")
				TextBuilder.AppendLine("Subscript _#: Coefficient #")
				TextBuilder.AppendLine("**End Nomenclature**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Governing Equations**")
				TextBuilder.AppendLine("(CC/CC_R) = CC_1 + CC_2*(TLI/TLI_R) + CC_3*(TSI/TSI_R) + CC_4*(VLI/VLI_R) + CC_5*(VSI/VSI_R)")
				TextBuilder.AppendLine("(CP/CP_R) = CP_1 + CP_2*(TLI/TLI_R) + CP_3*(TSI/TSI_R) + CP_4*(VLI/VLI_R) + CP_5*(VSI/VSI_R)")
				TextBuilder.AppendLine("**End Governing Equations**")
				TextBuilder.AppendLine()
				TextBuilder.AppendLine("**Begin Reporting Parameters**")
				For J As Integer = 0 To NumCoefficients_BZ
					TextBuilder.AppendLine("Cooling Capacity Coefficient CC_" & (J + 1).ToString & ": " & Math.Round(C1(J, 0), DecPts).ToString)
				Next
				For J As Integer = 0 To NumCoefficients_BZ
					TextBuilder.AppendLine("Cooling Power Consumption Coefficient CP_" & (J + 1).ToString & ": " & Math.Round(C2(J, 0), DecPts).ToString)
				Next
				TextBuilder.AppendLine("Rated Cooling Load-side Capacity: " & WWHPCooling.RatedTotalCapacity.ToString & "{kW}")
				TextBuilder.AppendLine("Rated Cooling Power Consumption: " & WWHPCooling.RatedCompressorPower.ToString & "{kW}")
				TextBuilder.AppendLine("Rated Load side Volumetric Flow Rate: " & WWHPCooling.RatedLoadVolFlowRate.ToString & "{m3/s}")
				TextBuilder.AppendLine("Rated Source side Volumetric Flow Rate: " & WWHPCooling.RatedSourceVolFlowRate.ToString & "{m3/s}")
				TextBuilder.AppendLine("**End Reporting Parameters**")
				TextOutput = TextBuilder.ToString
		End Select
		RaiseEvent ProgressEvent_Increment()
		RaiseEvent ProgressEvent_Complete(TextOutput)

		GenerateComponentParameters.Success = True
		GenerateComponentParameters.ErrorMessage = ""
	End Function

	Public Sub GeneratePlot_Absolute() Implements IComponentEngine.GeneratePlot_Absolute

		Dim PlotSeries As New List(Of PublicData.PlotSeriesData)
		PlotSeries.Add(New PlotSeriesData("Load HT Model Output", PlotTypeEnum.LinePlot, Color.Red, WWHPCooling.PredictedTotClgCapacity.ToArray))
		PlotSeries.Add(New PlotSeriesData("Load HT Catalog Data", PlotTypeEnum.PointPlot, Color.Red, WWHPCooling.TotalCoolingCapacity.ToArray))
		PlotSeries.Add(New PlotSeriesData("Source HT Model Output", PlotTypeEnum.LinePlot, Color.BlueViolet, WWHPCooling.PredictedSourceHeatRate.ToArray))
		PlotSeries.Add(New PlotSeriesData("Source HT Catalog Data", PlotTypeEnum.PointPlot, Color.BlueViolet, WWHPCooling.HeatRejection.ToArray))
		PlotSeries.Add(New PlotSeriesData("Compressor Power Model Output", PlotTypeEnum.LinePlot, Color.Green, WWHPCooling.PredictedPowerInput.ToArray))
		PlotSeries.Add(New PlotSeriesData("Compressor Power Catalog Data", PlotTypeEnum.PointPlot, Color.Green, WWHPCooling.CompressorPower.ToArray))

		Dim MyPlotData As New PublicData.PlotData("Model vs. Catalog Data Points", "Heat Transfer Rate/Power [W]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

	Public Sub GeneratePlot_PercentError() Implements IComponentEngine.GeneratePlot_PercentError

		Dim UpperBound As Integer = WWHPCooling.PredictedTotClgCapacity.Count - 1
		Dim PercDiffTotal(UpperBound) As Double
		Dim PercDiffSource(UpperBound) As Double
		Dim PercDiffPower(UpperBound) As Double

		For I As Integer = 1 To UpperBound
			PercDiffTotal(I) = 100 * (WWHPCooling.PredictedTotClgCapacity(I) - WWHPCooling.TotalCoolingCapacity(I)) / WWHPCooling.TotalCoolingCapacity(I)
			PercDiffSource(I) = 100 * (WWHPCooling.PredictedSourceHeatRate(I) - WWHPCooling.HeatRejection(I)) / WWHPCooling.HeatRejection(I)
			PercDiffPower(I) = 100 * (WWHPCooling.PredictedPowerInput(I) - WWHPCooling.CompressorPower(I)) / WWHPCooling.CompressorPower(I)
		Next

		Dim PlotSeries As New Generic.List(Of PublicData.PlotSeriesData)
		PlotSeries.Add(New PublicData.PlotSeriesData("Load HT % Error", PlotTypeEnum.LinePlot, Color.Red, PercDiffTotal))
		PlotSeries.Add(New PublicData.PlotSeriesData("Source HT % Error", PlotTypeEnum.LinePlot, Color.BlueViolet, PercDiffSource))
		PlotSeries.Add(New PublicData.PlotSeriesData("Comp. Power % Error", PlotTypeEnum.LinePlot, Color.Green, PercDiffPower))

		Dim MyPlotData As New PublicData.PlotData("% Error", "Heat Transfer Rate/Power [%]", PlotSeries)
		RaiseEvent PlotEvent(MyPlotData)

	End Sub

End Class
