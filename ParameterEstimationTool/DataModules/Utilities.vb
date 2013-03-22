Module UnitConversion

#Region " Temperature Conversion Tools and Data "

	Structure TempParameters
		Dim Dummy As Integer
		Const FCRatio As Double = 1.8
		Const FCShift As Double = 32
		Const KCShift As Double = 273.15
	End Structure

	Friend Function TempDegC(ByVal TempUnitType As PublicData.TempUnits, ByVal InputValue As Double) As Double
		' First make sure we have at least one input
		If (TempUnitType < PublicData.TempUnits.MinUnitValue) OrElse (TempUnitType > PublicData.TempUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case TempUnitType
			Case PublicData.TempUnits.TempF
				TempDegC = (InputValue - TempParameters.FCShift) / TempParameters.FCRatio
			Case PublicData.TempUnits.TempK
				TempDegC = InputValue - TempParameters.KCShift
			Case PublicData.TempUnits.TempC
				TempDegC = InputValue
		End Select
	End Function

#End Region

#Region " Heat Transfer Rate (Power) Conversion Tools and Data "

	Structure PowerParameters
		Dim Dummy As Integer
		Const kWBtuHRatio As Double = 1 / (3.412 * 1000)
		Const kWMBtuHRatio As Double = kWBtuHRatio * 10 ^ 3
		Const kWWRatio As Double = 0.001
	End Structure

	Friend Function PowerkW(ByVal PowerUnitType As PublicData.PowerUnits, ByVal InputValue As Double) As Double
		' First make sure we have at least one input
		If (PowerUnitType < PublicData.PowerUnits.MinUnitValue) OrElse (PowerUnitType > PublicData.PowerUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case PowerUnitType
			Case PublicData.PowerUnits.PowerW
				PowerkW = InputValue * PowerParameters.kWWRatio
			Case PublicData.PowerUnits.PowerBtuH
				PowerkW = InputValue * PowerParameters.kWBtuHRatio
			Case PublicData.PowerUnits.PowerMBtuH
				PowerkW = InputValue * PowerParameters.kWMBtuHRatio
			Case PowerUnits.PowerkW
				PowerkW = InputValue
		End Select
	End Function

#End Region

#Region " Flow Rate Conversion Tools and Data "

	Structure FlowParameters
		Dim Dummy As Integer
		Const m3sCFMRatio As Double = 0.0004719474432
		Const m3sGPMRatio As Double = 0.00006309
	End Structure

	Friend Function Flowm3s(ByVal FlowUnitType As PublicData.FlowUnits, ByVal InputValue As Double) As Double
		' First make sure we have at least one input
		If (FlowUnitType < PublicData.FlowUnits.MinUnitValue) OrElse (FlowUnitType > PublicData.FlowUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case FlowUnitType
			Case PublicData.FlowUnits.FlowCFM
				Flowm3s = InputValue * FlowParameters.m3sCFMRatio
			Case PublicData.FlowUnits.FlowGPM
				Flowm3s = InputValue * FlowParameters.m3sGPMRatio
			Case PublicData.FlowUnits.FlowM3S
				Flowm3s = InputValue
		End Select
	End Function

#End Region

#Region " Pressure Conversion Tools and Data "

	Structure PressureParameters
		Dim Dummy As Integer
		Const kPa_to_Pa As Double = 1000.0
		Const atm_to_Pa As Double = 101325.0
		Const psi_to_Pa As Double = 6894.757
	End Structure

	Friend Function PressurePa(ByVal PressureUnitType As PublicData.PressureUnits, ByVal InputValue As Double) As Double
		' First make sure we have at least one input
		If (PressureUnitType < PublicData.PressureUnits.MinUnitValue) OrElse (PressureUnitType > PublicData.PressureUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case PressureUnitType
			Case PressureUnits.kPa
				PressurePa = InputValue * PressureParameters.kPa_to_Pa
			Case PressureUnits.Pa
				PressurePa = InputValue
			Case PressureUnits.PSI
				PressurePa = InputValue * PressureParameters.psi_to_Pa
			Case PressureUnits.atm
				PressurePa = InputValue * PressureParameters.atm_to_Pa
		End Select
	End Function

#End Region

#Region " Length Conversion Tools and Data "

	Structure LengthParameters
		Dim Dummy As Integer
		Const ft_to_m As Double = 0.3048
		Const in_to_m As Double = 0.0254
		Const cm_to_m As Double = 0.01
		Const mm_to_m As Double = 0.001
	End Structure

	Friend Function LengthMeters(ByVal LengthUnitType As PublicData.LengthUnits, ByVal InputValue As Double) As Double
		' First make sure we have at least one input
		If (LengthUnitType < LengthUnits.MinUnitValue) OrElse (LengthUnitType > LengthUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case LengthUnitType
			Case LengthUnits.Feet
				Return InputValue * LengthParameters.ft_to_m
			Case LengthUnits.Inches
				Return InputValue * LengthParameters.in_to_m
			Case LengthUnits.Centimeters
				Return InputValue * LengthParameters.cm_to_m
			Case LengthUnits.Millimeters
				Return InputValue * LengthParameters.mm_to_m
			Case LengthUnits.Meters
				Return InputValue
		End Select
	End Function

#End Region

#Region " Rotational Speed Tools and Data "

	Structure RotSpeedParameters
		Dim Dummy As Integer
		Const RPM_to_RPS As Double = 1 / 60
		Const RadPerSec_to_RPS As Double = 1 / 6.2831853
	End Structure

	Friend Function RotSpeedRevPerSec(ByVal RotSpeedUnitType As PublicData.RotationSpeedUnits, ByVal InputValue As Double) As Double
		If (RotSpeedUnitTYpe < RotationSpeedUnits.MinUnitValue) OrElse (RotSpeedUnitTYpe > RotationSpeedUnits.MaxUnitValue) OrElse (InputValue = Nothing) Then Return Nothing
		Select Case RotSpeedUnitType
			Case RotationSpeedUnits.RadiansPerSecond
				Return InputValue * RotSpeedParameters.RadPerSec_to_RPS
			Case RotationSpeedUnits.RevsPerMinute
				Return InputValue * RotSpeedParameters.RPM_to_RPS
			Case RotationSpeedUnits.RevsPerSecond
				Return InputValue
		End Select
	End Function

#End Region

End Module

Module OtherUtilities

    Public Function GetClipboardText() As String
        Dim objClipboard As IDataObject = Clipboard.GetDataObject()
        With objClipboard
            If .GetDataPresent(DataFormats.Text) Then
                Return .GetData(DataFormats.Text)
            Else
                Return ""
            End If
        End With
    End Function

End Module

Public Module DataGridViewCopyPaste
	'******************************************************************************
	'a Shared class with methods that allow copying and pasting of DataGridView
	'contents. The contents are copied to memory as a
	'DataGridViewSelectedCellCollection by default. Optionally, the contents may
	'also be copied to the Microsoft® Windows Clipboard as tab-delimited text.
	'The pasting method first checks for an optionally provided
	'DataGridViewSelectedCellCollection. If the object was not provided, the last
	'copied Clipboard text is pasted, taking into account tabs and end of line
	'characters.
	'******************************************************************************

	''' <summary>
	''' enumerated variable to indicate the direction in which cells were selected
	''' </summary>
	''' <remarks></remarks>
	Private Enum selectDirection
		LeftToRightTopToBottom
		LeftToRightBottomToTop
		RightToLeftTopToBottom
		RightToLeftBottomToTop
	End Enum

#Region "Public methods"

	'**************************************************************************
	'copy the contents of the selected cells
	'**************************************************************************

	Public Function CopyDataGridViewCells(ByVal dgv As DataGridView, _
	Optional ByVal alsoCopyToClipBoard As Boolean = False) _
	As DataGridViewSelectedCellCollection
		'get the selected cells
		Dim selectedCells As DataGridViewSelectedCellCollection _
		 = dgv.SelectedCells

		'if the selected cell contents are to be copied to the clipboard
		If alsoCopyToClipBoard Then
			Dim cells(,) As DataGridViewCell = SortCells(selectedCells)
			CopyToClipboard(cells)
		End If

		Return selectedCells
	End Function

	'**************************************************************************
	'paste data into the selected cells
	'**************************************************************************

	Public Sub PasteDataGridViewCells(ByVal dgv As DataGridView, _
	 Optional ByVal copiedCells As DataGridViewSelectedCellCollection = Nothing, _
	 Optional ByVal maintainColumns As Boolean = False)

		Dim cells(,) As DataGridViewCell

		If copiedCells Is Nothing Then
			'if a DataGridViewSelectedCellCollection was not passed to the
			'method, then get the contents of the clipboard
			If Not My.Computer.Clipboard.ContainsText Then
				'the clipboard does not contain any text
				Exit Sub
			End If

			'get the text from the clipboard
			Dim table As String = My.Computer.Clipboard.GetText()

			'parse the text into a 2-dimensional array of columns and rows
			cells = AsciiTableToDataGridViewCellsArray(table)
		Else
			'make sure data is sorted from Left to Right and Top to Bottom
			cells = SortCells(copiedCells)
		End If

		Select Case dgv.SelectedCells.Count
			Case Is = 1
				'a single cell is selected. Fill the cells to the left and
				'down from the selected cell, as needed

				'determine the number of rows and columns available
				Dim availableRows As Integer = dgv.RowCount - (dgv.CurrentCell.RowIndex + 1)
				Dim availableCols As Integer = dgv.ColumnCount - (dgv.CurrentCell.ColumnIndex + 1)

				If copiedCells Is Nothing OrElse maintainColumns = False Then
					'don't worry about maintaining the columns
					For r As Integer = 0 To cells.GetUpperBound(1)
						If r > availableRows Then
							Exit For
						End If
						For c As Integer = 0 To cells.GetUpperBound(0)
							If c > availableCols Then
								Exit For
							End If
							If Not cells(c, r) Is Nothing Then
								dgv.Item(dgv.CurrentCell.ColumnIndex + c, dgv.CurrentCell.RowIndex + r).Value = cells(c, r).Value
							End If
						Next
					Next
				Else
					'keep track of column from which the cell was copied
					For r As Integer = 0 To cells.GetUpperBound(1)
						If r > availableRows Then
							Exit For
						End If
						For c As Integer = 0 To cells.GetUpperBound(0)
							If Not cells(c, r) Is Nothing AndAlso cells(c, r).ColumnIndex >= dgv.CurrentCell.ColumnIndex Then
								dgv.Item(cells(c, r).ColumnIndex, dgv.CurrentCell.RowIndex + r).Value = cells(c, r).Value
							End If
						Next
					Next
				End If
			Case Is > 1
				'a range of cells is selected. Fill the selected range,
				'truncating or repeating the copied cells, as needed

				'make sure the cells are sorted Left to Right and Top to Bottom
				Dim pasteCells(,) As DataGridViewCell = SortCells( _
				 dgv.SelectedCells)

				'determine the number of rows and columns available
				Dim availableRows As Integer = pasteCells.GetUpperBound(1) + 1
				Dim availableCols As Integer = pasteCells.GetUpperBound(0) + 1

				'copiedCells is Nothing when data is copied from somewhere else
				'other than a DataGridView control
				If copiedCells Is Nothing OrElse maintainColumns = False Then
					'don't worry about maintaining the columns
					Dim pasteRow As Integer = 0
					Do While pasteRow <= pasteCells.GetUpperBound(1)
						For r As Integer = 0 To cells.GetUpperBound(1)
							If r > availableRows Then
								Exit For
							End If

							For c As Integer = 0 To cells.GetUpperBound(0)
								If c > availableCols Then
									Exit For
								End If
								If Not cells(c, r) Is Nothing Then
									pasteCells(c, pasteRow).Value = cells(c, _
									 r).Value
								End If
							Next

							'repeat the copied data until all of the rows
							'selected for pasting are filled
							pasteRow += 1
							If pasteRow > pasteCells.GetUpperBound(1) Then
								Exit For
							End If
						Next
					Loop
				Else
					'keep track of column from which the cell was copied
					Dim pasteRow As Integer = 0
					Do While pasteRow <= pasteCells.GetUpperBound(1)
						For r As Integer = 0 To cells.GetUpperBound(1)
							If r > availableRows Then
								Exit For
							End If

							For c As Integer = 0 To cells.GetUpperBound(0)
								If Not cells(c, r) Is Nothing _
								AndAlso cells(c, r).ColumnIndex _
								>= pasteCells(0, pasteRow).ColumnIndex Then
									pasteCells(c, pasteRow).Value = cells(c, _
									 r).Value
								End If
							Next

							'repeat the copied data until all of the rows
							'selected for pasting are filled
							pasteRow += 1
							If pasteRow > pasteCells.GetUpperBound(1) Then
								Exit For
							End If
						Next
					Loop
				End If
			Case Else
				'no cells are selected for pasting
				Exit Sub
		End Select
	End Sub

#End Region

#Region "Private methods"

	'**************************************************************************
	'determine the direction in which the cells were selected
	'**************************************************************************

	Private Function GetSelectDirection( _
	ByVal selectedCells As DataGridViewSelectedCellCollection) _
	As selectDirection
		'initialize direction to Left-Right/Top-Bottom
		Dim direction As selectDirection _
		 = selectDirection.LeftToRightTopToBottom

		'get the first and last cells in the collection
		Dim initCell As DataGridViewCell = selectedCells(0)
		Dim finalCell As DataGridViewCell = selectedCells( _
		 selectedCells.Count - 1)

		'check for column direction
		Dim leftToRight As Boolean = True
		If finalCell.ColumnIndex < initCell.ColumnIndex Then
			leftToRight = False
		End If

		'check for row direction
		Dim topToBottom As Boolean = True
		If finalCell.RowIndex < initCell.RowIndex Then
			topToBottom = False
		End If

		'set the final direction combination
		If leftToRight Then
			If topToBottom Then
				direction = selectDirection.LeftToRightTopToBottom
			Else
				direction = selectDirection.LeftToRightBottomToTop
			End If
		Else
			If topToBottom Then
				direction = selectDirection.RightToLeftTopToBottom
			Else
				direction = selectDirection.RightToLeftBottomToTop
			End If
		End If

		Return direction
	End Function

	'**************************************************************************
	'organize the contents so that they are from Left to Right & Top to
	'Bottom, as needed
	'**************************************************************************

	Private Function SortCells( _
	ByVal selectedCells As DataGridViewSelectedCellCollection) _
	As DataGridViewCell(,)
		'Dim direction As selectDirection = GetSelectDirection(selectedCells)

		Dim rowCount As Integer = 0
		Dim minRow As Integer = 0
		Dim maxRow As Integer = 0
		Dim colCount As Integer = 0
		Dim minCol As Integer = 0
		Dim maxCol As Integer = 0

		'determine the column and row limits based on the selection direction
		'Select Case direction
		'    Case selectDirection.LeftToRightTopToBottom
		'        rowCount = selectedCells(selectedCells.Count - 1).RowIndex _
		'            - selectedCells(0).RowIndex + 1
		'        colCount = selectedCells(selectedCells.Count - 1).ColumnIndex _
		'            - selectedCells(0).ColumnIndex + 1
		'        minRow = selectedCells(0).RowIndex
		'        minCol = selectedCells(0).ColumnIndex
		'    Case selectDirection.LeftToRightBottomToTop
		'        rowCount = selectedCells(0).RowIndex - selectedCells( _
		'            selectedCells.Count - 1).RowIndex + 1
		'        colCount = selectedCells(selectedCells.Count - 1).ColumnIndex _
		'            - selectedCells(0).ColumnIndex + 1
		'        minRow = selectedCells(selectedCells.Count - 1).RowIndex
		'        minCol = selectedCells(0).ColumnIndex
		'    Case selectDirection.RightToLeftTopToBottom
		'        rowCount = selectedCells(selectedCells.Count - 1).RowIndex _
		'            - selectedCells(0).RowIndex + 1
		'        colCount = CType(selectedCells.Count / rowCount, Integer)
		'        minRow = selectedCells(0).RowIndex
		'        minCol = selectedCells(0).ColumnIndex - colCount + 1
		'    Case selectDirection.RightToLeftBottomToTop
		'        rowCount = selectedCells(0).RowIndex - selectedCells( _
		'            selectedCells.Count - 1).RowIndex + 1
		'        colCount = CType(selectedCells.Count / rowCount, Integer)
		'        minRow = selectedCells(0).RowIndex - rowCount + 1
		'        minCol = selectedCells(0).ColumnIndex - colCount + 1
		'End Select

		'first take the rows/columns from the first value
		minRow = selectedCells(0).RowIndex
		maxRow = selectedCells(0).RowIndex
		minCol = selectedCells(0).ColumnIndex
		maxCol = selectedCells(0).ColumnIndex

		'then loop through the rest updating min/max as necessary
		For I As Integer = 1 To (selectedCells.Count - 1)
			minRow = Math.Min(minRow, selectedCells(I).RowIndex)
			maxRow = Math.Max(maxRow, selectedCells(I).RowIndex)
			minCol = Math.Min(minCol, selectedCells(I).ColumnIndex)
			maxCol = Math.Max(maxCol, selectedCells(I).ColumnIndex)
		Next

		'then update the sizes
		colCount = (maxCol - minCol) + 1
		rowCount = (maxRow - minRow) + 1

		'size the array
		Dim cells(colCount - 1, rowCount - 1) As DataGridViewCell
		'fill the array
		For Each cell As DataGridViewCell In selectedCells
			cells(cell.ColumnIndex - minCol, cell.RowIndex - minRow) = cell
		Next

		Return cells
	End Function

	'**************************************************************************
	'copy the cell information to the clipboard
	'**************************************************************************

	Private Sub CopyToClipboard(ByVal cells As DataGridViewCell(,))
		'create the tab and CrLf delimited string
		Dim cellsTable As New System.Text.StringBuilder
		For r As Integer = 0 To cells.GetUpperBound(1)
			For c As Integer = 0 To cells.GetUpperBound(0)
				If Not cells(c, r).Value Is Nothing Then
					If cells(c, r).Value.GetType.ToString = "System.DBNull" Then
						cellsTable.Append(CType(cells(c, r).Value, String) _
						 & Microsoft.VisualBasic.ControlChars.Tab)
					Else
						cellsTable.Append(Microsoft.VisualBasic.ControlChars.Tab)
					End If
				Else
					cellsTable.Append(Microsoft.VisualBasic.ControlChars.Tab)
				End If
			Next
			cellsTable.Append(Microsoft.VisualBasic.ControlChars.CrLf)
		Next

		'send the string to the clipboard
		My.Computer.Clipboard.SetText(cellsTable.ToString)
	End Sub

	'**************************************************************************
	'convert clipboard ASCII table to an array of DataGridViewCells
	'**************************************************************************

	Private Function AsciiTableToDataGridViewCellsArray(ByVal table As String) As DataGridViewCell(,)

		Dim cells(,) As DataGridViewCell

		'parse the text into a 2-dimensional array of columns and rows
		If table.Contains(Microsoft.VisualBasic.ControlChars.CrLf) OrElse table.Contains(Microsoft.VisualBasic.ControlChars.Tab) Then
			'split the rows by the CrLf end of line string
			Dim separator() As String = {Microsoft.VisualBasic.ControlChars.CrLf}
			Dim rows() As String = table.Split(separator, StringSplitOptions.None)

			'determine how many columns are in each row
			Dim colCount As Integer = 0
			Dim foundIndex As Integer = 0
			Do
				foundIndex = rows(0).IndexOf(Microsoft.VisualBasic.ControlChars.Tab, foundIndex + 1)
				If foundIndex > 0 Then
					colCount += 1
				End If
			Loop Until foundIndex <= 0

			'size the array to fit
			ReDim cells(colCount, rows.GetUpperBound(0) + 1)

			'get the cells in each row
			separator(0) = Microsoft.VisualBasic.ControlChars.Tab.ToString
			For r As Integer = 0 To rows.GetUpperBound(0)
				Dim cellText() As String = rows(r).Split(separator, StringSplitOptions.None)

				'fill the 2-dimensional array with the cell values
				For c As Integer = 0 To cellText.GetUpperBound(0)
					cells(c, r) = New DataGridViewTextBoxCell()
					Dim rawcelltext As String = cellText(c)
					If rawcelltext.Contains("%") Then
						'convert percent to a decimal
						rawcelltext = rawcelltext.Remove(rawcelltext.IndexOf("%"), 1)
						Dim val As Double
						If Double.TryParse(rawcelltext, val) Then
							Dim decval As Double = val / 100
							cellText(c) = decval
						Else
							'leave it alone I suppose, invalid numerics should be caught in validation
						End If
					End If
					cells(c, r).Value = cellText(c)
				Next
			Next
		Else
			'there is only a single cell
			ReDim cells(0, 0)
			cells(0, 0) = New DataGridViewTextBoxCell()
			cells(0, 0).Value = table
		End If

		Return cells
	End Function

#End Region

End Module

Public Module Plotting_Operations

	Private Structure NPlotData
		Dim nPlot As List(Of NPlot.LinePlot)
		Dim nPPlot As List(Of NPlot.PointPlot)
		Structure FontInfo
			Dim XAxisFontFamily As String
			Dim YAxisFontFamily As String
			Dim XTickFontFamily As String
			Dim YTickFontFamily As String
			Dim TitleFontFamily As String
			Dim TitleSize As Integer
			Dim XAxisSize As Integer
			Dim XTickSize As Integer
			Dim YAxisSize As Integer
			Dim YTickSize As Integer
			Dim TitleFont As System.Drawing.Font
			Dim XAxisFont As System.Drawing.Font
			Dim YAxisFont As System.Drawing.Font
			Dim XTickFont As System.Drawing.Font
			Dim YTickFont As System.Drawing.Font
		End Structure
		Dim Fonts As FontInfo
		Dim PlotLegend As NPlot.Legend
		Dim X As Double()
		Dim Y As List(Of Double())
		Dim Title As String
		Dim SeriesTitles As List(Of String)
		Dim BackgndColor As Color
		Dim XAxisLabel As String
		Dim XNumberFormat As String
		Dim XTicksLabelAngle As Integer
		Dim XTicksTextNextToAxis As Boolean
		Dim XFlipTicksLabel As Boolean
		Dim XLabelOffSet As Integer
		Dim XLabelOffSetAbs As Boolean
		Dim YAxisLabel As String
		Dim YAxisLabelFont As Font
		Dim YAxisTickFont As Font
		Dim YAxisNumberFormat As String
		Sub SetupDefaults()
			With Me
				.nPlot = New List(Of NPlot.LinePlot)
				.nPPlot = New List(Of NPlot.PointPlot)
				.Fonts.XAxisFontFamily = "Arial"
				.Fonts.YAxisFontFamily = "Arial"
				.Fonts.XTickFontFamily = "Arial"
				.Fonts.YTickFontFamily = "Arial"
				.Fonts.TitleFontFamily = "Arial"
				.Fonts.TitleSize = 14
				.Fonts.XAxisSize = 12
				.Fonts.XTickSize = 10
				.Fonts.YAxisSize = 12
				.Fonts.YTickSize = 10
				.Y = New List(Of Double())
				.SeriesTitles = New List(Of String)
				.Title = ""
				.BackgndColor = Color.WhiteSmoke
				.XAxisLabel = "Catalog Data Points (no order)"
				.XNumberFormat = "{0:####0}" '"yyyy-MM-dd HH:mm"
				.XTicksLabelAngle = 90
				.XTicksTextNextToAxis = True
				.XFlipTicksLabel = True
				.XLabelOffSet = 50 '110
				.XLabelOffSetAbs = True
				.YAxisLabel = ""
				.YAxisNumberFormat = "{0:####0.0}"
			End With
		End Sub
	End Structure

	Friend Sub MakeAPlot(ByVal ChartTitle As String, ByVal YLabel As String, ByVal SeriesCollection As Collections.Generic.List(Of PublicData.PlotSeriesData), ByVal PlotSurface As NPlot.Windows.PlotSurface2D)

		'See how many data points there are.  For now, assume all series have the same number of points
		Dim NumSeries As Integer = SeriesCollection.Count - 1
		Dim NumDataPoints As Integer = SeriesCollection(0).YValues.GetUpperBound(0)	'zero based

		'Set up Data Structure to defaults
		Dim MyPlot As New NPlotData
		With MyPlot
			.SetupDefaults()
			.Title = ChartTitle

			'set up x-axis.  for now this is just an index value, from 0 to number of data points
			ReDim .X(NumDataPoints)
			For I As Integer = 0 To NumDataPoints
				MyPlot.X(I) = I
			Next

			'setup y-axis (series data)
			For Each series As PublicData.PlotSeriesData In SeriesCollection
				.Y.Add(series.YValues)
				.SeriesTitles.Add(series.SeriesName)
			Next
			.YAxisLabel = YLabel
		End With

		'Then Make plot
		With MyPlot
			.Fonts.TitleFont = New System.Drawing.Font(.Fonts.TitleFontFamily, .Fonts.TitleSize)
			.Fonts.XAxisFont = New System.Drawing.Font(.Fonts.XAxisFontFamily, .Fonts.XAxisSize)
			.Fonts.XTickFont = New System.Drawing.Font(.Fonts.XTickFontFamily, .Fonts.XTickSize)
			.Fonts.YAxisFont = New System.Drawing.Font(.Fonts.YAxisFontFamily, .Fonts.YAxisSize)
			.Fonts.YTickFont = New System.Drawing.Font(.Fonts.YTickFontFamily, .Fonts.YTickSize)
			.PlotLegend = New NPlot.Legend
			.YAxisLabelFont = .Fonts.YAxisFont
			.YAxisTickFont = .Fonts.YTickFont
			With PlotSurface
				.Clear()
				.Title = MyPlot.Title
				.AutoScaleTitle = True
				.BackColor = MyPlot.BackgndColor
				.TitleFont = MyPlot.Fonts.TitleFont
				.Add(New NPlot.Grid, NPlot.PlotSurface2D.XAxisPosition.Bottom, NPlot.PlotSurface2D.YAxisPosition.Left)
				'Plot Data 
				With MyPlot
					Dim NumLinePlots As Integer = -1
					Dim NumPointPlots As Integer = -1
					For SeriesNum As Integer = 0 To NumSeries
						If SeriesCollection(SeriesNum).PlotType = PublicData.PlotTypeEnum.LinePlot Then
							NumLinePlots += 1
							.nPlot.Add(New NPlot.LinePlot)
							With .nPlot(NumLinePlots)
								.AbscissaData = MyPlot.X
								.DataSource = SeriesCollection(SeriesNum).YValues
								.Color = SeriesCollection(SeriesNum).PlotColor
								.Label = SeriesCollection(SeriesNum).SeriesName
							End With
							PlotSurface.Add(MyPlot.nPlot(NumLinePlots), NPlot.PlotSurface2D.XAxisPosition.Bottom, NPlot.PlotSurface2D.YAxisPosition.Left)
						ElseIf SeriesCollection(SeriesNum).PlotType = PublicData.PlotTypeEnum.PointPlot Then
							NumPointPlots += 1
							.nPPlot.Add(New NPlot.PointPlot)
							With .nPPlot(NumPointPlots)
								.AbscissaData = MyPlot.X
								.DataSource = SeriesCollection(SeriesNum).YValues
								.Marker = New NPlot.Marker(NPlot.Marker.MarkerType.Diamond, 5, SeriesCollection(SeriesNum).PlotColor)
								.Label = SeriesCollection(SeriesNum).SeriesName
							End With
							PlotSurface.Add(MyPlot.nPPlot(NumPointPlots), NPlot.PlotSurface2D.XAxisPosition.Bottom, NPlot.PlotSurface2D.YAxisPosition.Left)
						End If
					Next
				End With
				'Prepare bottom X axis (a Plot MUST first be assigned to the bottom X axis):
				.XAxis1.Label = MyPlot.XAxisLabel
				.XAxis1.LabelFont = MyPlot.Fonts.XAxisFont
				.XAxis1.TickTextFont = MyPlot.Fonts.XTickFont
				.XAxis1.NumberFormat = MyPlot.XNumberFormat
				.XAxis1.TicksLabelAngle = MyPlot.XTicksLabelAngle
				.XAxis1.TickTextNextToAxis = MyPlot.XTicksTextNextToAxis
				.XAxis1.FlipTicksLabel = MyPlot.XFlipTicksLabel
				.XAxis1.LabelOffset = MyPlot.XLabelOffSet
				.XAxis1.LabelOffsetAbsolute = MyPlot.XLabelOffSetAbs
				'Prepare left Y axis (a Plot MUST first be assigned to the left Y axis):
				.YAxis1.Label = MyPlot.YAxisLabel
				.YAxis1.LabelFont = MyPlot.YAxisLabelFont
				.YAxis1.TickTextFont = MyPlot.YAxisTickFont
				.YAxis1.NumberFormat = MyPlot.YAxisNumberFormat
			End With
			With .PlotLegend
				.Font = MyPlot.YAxisLabelFont
				.AttachTo(NPlot.PlotSurface2D.XAxisPosition.Top, NPlot.PlotSurface2D.YAxisPosition.Right)
				.VerticalEdgePlacement = NPlot.Legend.Placement.Outside	'inside
				.HorizontalEdgePlacement = NPlot.Legend.Placement.Inside 'Outside
				.BorderStyle = NPlot.LegendBase.BorderType.Line
			End With
			PlotSurface.Legend = .PlotLegend

			Dim OutOfRange As Boolean = False
			Dim BadCollectionItem() As Boolean = Nothing
			Call CheckForInfiniteData(SeriesCollection, OutOfRange, BadCollectionItem)
			If OutOfRange Then
				Call ShowSevereError("Out of range (likely Infinite) data points detected.  All infinite points are set to 0.0 to allow the operations to continue." & vbCrLf & _
				"This could be an algorithmic problem, or an input data problem.  Verify input data first.")
				PlotSurface = Nothing
				Return
			End If

			'wrapping this in a Try block did not help anything
			'I think there is a threading issue, where the thread that actually 
			'encounters the exception is not contained within this Try block
			'so I just have to make sure that the above out of range checks correct all data
			PlotSurface.Refresh()

		End With

	End Sub

	Private Sub CheckForInfiniteData(ByRef SeriesCollection As List(Of PublicData.PlotSeriesData), ByRef OutOfRange As Boolean, ByRef BadColumns() As Boolean)
		Dim NumSeries As Integer = SeriesCollection.Count - 1
		ReDim BadColumns(NumSeries)
		For I As Integer = 0 To NumSeries
			BadColumns(I) = False
		Next
		For I As Integer = 0 To NumSeries
			OutOfRange = False
			Dim MyArray As Double() = SeriesCollection(I).YValues
			For J As Integer = MyArray.GetLowerBound(0) To MyArray.GetUpperBound(0)
				Dim Valid As PublicData.DataValidationReturnType = DataValidationTest(MyArray(J))
				If Valid = DataValidationReturnType.Infinity Then
					OutOfRange = True
					BadColumns(I) = True
					MyArray(J) = 0.0
					'Exit For
				End If
			Next
			If OutOfRange Then
				'do time consuming task of replacing list items
				Dim MyTempPlotData As PublicData.PlotSeriesData = SeriesCollection(I)
				MyTempPlotData.YValues = MyArray
				SeriesCollection(I) = MyTempPlotData
			End If
		Next I
	End Sub

End Module



