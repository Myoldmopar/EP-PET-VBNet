'Imports System.Runtime.InteropServices.Marshal

Public Class CatalogDataWizard

#Region "Variables"
	Friend ColumnType() As Integer
	Friend ColumnUnit() As Integer
	Friend AlreadySetUp As Boolean = False
	Friend ConstantColumns() As Boolean
	Friend DisabledColumnUnits() As Integer
#End Region

#Region "Initialize and Paste Data"
	Private Sub CatalogDataWizard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

		'if we cancelled out then just go ahead and leave
		If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then Exit Sub

		'then we will check if there is data available in the data grid
		'first turn off the addition of new rows to cancel any blank entries (save this value)
		Dim bTemp As Boolean = Me.DataGridView1.AllowUserToAddRows
		Me.DataGridView1.AllowUserToAddRows = False
		If DataGridView1.RowCount > 0 Then Exit Sub
		'if we made it through then the data grid must be blank, for kicks, reset the allowance of new rows to previous value
		Me.DataGridView1.AllowUserToAddRows = bTemp

		'since there weren't any rows of data, go ahead and ask the user's decision
		Dim MR As MsgBoxResult = ShowOKCancelBox("The OK button was pressed, yet the data grid appears to be empty." & vbCrLf & _
		 "Would you like to go back and edit the form?  Cancelling will abort the entire data entry operation.")

		'take action
		If MR = MsgBoxResult.Ok Then 'user wants to edit, cancel the closing procedure
			e.Cancel = True
			Exit Sub
		ElseIf MR = MsgBoxResult.Cancel Then 'user wants to cancel, set dialogresult to inform owner
			Me.DialogResult = Windows.Forms.DialogResult.Cancel
		End If

	End Sub

	Private Sub CatalogData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Me.lblInstruct.Text = "This wizard will help input catalog data.  To be brought in properly, the data should be divided in columns with 'tabs' and in rows with line delimiters." & vbCrLf & _
   "This is the way most spreadsheets will copy and paste data, so first paste the data in a spreadsheet, and then copy here and press the button below to begin."
		Me.lblInstruct2.Text = "Once the data is pasted, use the comboboxes below to define the units for each column.  Finally, click 'conform units' below to convert the units for the calculations."
		Dim Success As Boolean = True
		Success = InitializeDataGridsSuccess()
		If Not Success Then
			'Me.Dispose() 'need to do something else here I think
			Exit Sub
		End If
		Call InitializeGridViewSychronizers()
		If Me.ConstantColumns IsNot Nothing Then
			Call PublicData.ShowInfoBox("Correction Factors have been detected for this simulation." & vbCrLf & _
			 "Note that these correction factors will be implemented to data pasted in this window." & vbCrLf & _
			 "This form will only handle the base data: tabular and constant values." & vbCrLf & _
			 "Columns which are a darker shade of gray indicate columns which will have corrections applied.") ' & vbCrLf & _
		End If
	End Sub

	Private Function InitializeDataGridsSuccess() As Boolean

		If Not PublicData.CatalogDataInPlace Then

			'clear grid views
			For Each DGV As DataGridView In New DataGridView() {DataGridView1, DataGridUnits}
				For J As Integer = DGV.Rows.Count To 1 Step -1
					Dim TempRow As DataGridViewRow = DGV.Rows.Item(J - 1)
					DGV.Rows.Remove(TempRow)
				Next
				For J As Integer = DGV.Columns.Count To 1 Step -1
					Dim TempCol As DataGridViewColumn = DGV.Columns.Item(J - 1)
					DGV.Columns.Remove(TempCol)
				Next
			Next

			'get data for headers
			Dim MyArray As String() = PublicData.GetHeaderStrings
			Dim MyUnits As Integer() = PublicData.GetHeaderUnits
			If (MyArray Is Nothing) Or (MyUnits Is Nothing) Then
				MsgBox("A call was made to set up catalog data headers from an unknown source.", MsgBoxStyle.Exclamation)
				Return False
			End If

			'declare the number of columns expected by the grid view for this component
			Dim ExpectedCols As Integer = MyArray.Count - 1

			'set up headers in the main grid
			For I As Integer = 0 To ExpectedCols
				DataGridView1.Columns.Add("Column" & I.ToString, MyArray(I))
				DataGridView1.AutoResizeColumns()
				If Me.ConstantColumns IsNot Nothing Then
					If Me.ConstantColumns(I) Then DataGridView1.Columns(I).DefaultCellStyle.BackColor = Color.LightGray
				End If
			Next

			'Copy columns to units data grid
			For I As Integer = 0 To ExpectedCols
				DataGridUnits.Columns.Add("Column" & I.ToString, MyArray(I))
				DataGridUnits.Columns(I).Width = DataGridView1.Columns(I).Width
				DataGridUnits.Columns(I).DefaultCellStyle.BackColor = DataGridView1.Columns(I).DefaultCellStyle.BackColor
			Next

			'add a single unit row in the units grid
			DataGridUnits.Rows.Add()
			DataGridUnits.Rows(0).HeaderCell.Value = ""
			For I As Integer = 0 To ExpectedCols
				DataGridUnits.Item(I, 0).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
			Next

			'Set the unit type of each column
			ReDim ColumnType(ExpectedCols)
			For I As Integer = 0 To ExpectedCols
				ColumnType(I) = MyUnits(I)
			Next

			'Set the units in each column to be defaulted to calculation unit for each type
			ReDim ColumnUnit(ExpectedCols)
			For I As Integer = 0 To ExpectedCols
				ColumnUnit(I) = PublicData.GetCalculationUnit(ColumnType(I))
				Call UpdateUnitsGrid(I)
			Next

			Return True

		End If

		Call ConformOrOK()

	End Function

	Private Sub btnPasteData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPasteData.Click

		DataGridView1.CausesValidation = False

		'Read data from clipboard
		Dim strOriginal As String = GetClipboardText()

		'Clear the main datagrid
		Me.DataGridUnits.Focus()
		Dim RowCount As Integer = Me.DataGridView1.Rows.Count - 1
		For j As Integer = 0 To RowCount
			If Me.DataGridView1.Rows(j).IsNewRow Then
				Call PublicData.ShowSevereError("It appears a custom row was entered before data was pasted." & vbCrLf & _
				  "This row should be deleted before any pasting can be performed." & vbCrLf & _
				  "Either delete this row, or complete it by clicking outside of the datagrid, and try again.")
				DataGridView1.CausesValidation = True
				Exit Sub
			End If
		Next
		Me.DataGridView1.Rows.Clear()

		'Split the source data according to excel row/column separation characters
		Dim strOnceSplit() As String
		Dim strTwiceSplit(,) As String
		Dim Rows As Integer
		Dim Cols As Integer
		Dim ColsOld As Integer
		Dim MaxCols As Integer	'zero-based count of data columns
		strOnceSplit = strOriginal.Trim.Split(vbCrLf)
		Rows = strOnceSplit.GetUpperBound(0)
		ReDim strTwiceSplit(Rows, 0)
		Dim TempString() As String
		For I As Integer = 0 To Rows
			TempString = strOnceSplit(I).Split(vbTab)
			Cols = TempString.GetUpperBound(0)
			If I > 0 And Cols <> MaxCols Then Exit For
			For J As Integer = 0 To Cols
				ColsOld = strTwiceSplit.GetUpperBound(1)
				MaxCols = Math.Max(ColsOld, Cols)
				ReDim Preserve strTwiceSplit(Rows, MaxCols)
				strTwiceSplit(I, J) = TempString(J).Trim
			Next
		Next

		'first let's see how many columns we really expect
		Dim TotalNumDataColumns As Integer = Me.DataGridView1.ColumnCount - 1
		Dim ExpectedCols As Integer = TotalNumDataColumns
		If MaxCols < ExpectedCols Then
			Call PublicData.ShowSevereError("Incorrect number of columns encountered." & vbCrLf & _
			 "Expected: " & (ExpectedCols + 1).ToString & "; encountered: " & (MaxCols + 1).ToString & vbCrLf & _
			 "Pasting the amount encountered...")
		ElseIf MaxCols > ExpectedCols AndAlso MaxCols <= TotalNumDataColumns Then
			Call PublicData.ShowSevereError("Unexpected number of columns encountered." & vbCrLf & _
			  "Expected: " & ExpectedCols + 1 & "; encountered:" & MaxCols + 1 & vbCrLf & _
			  "Data will be pasted with constant values overriding where applicable." & vbCrLf & _
			  "Manual manipulation will probably be required after pasting.")
		ElseIf MaxCols > TotalNumDataColumns Then
			Call PublicData.ShowSevereError("Encountered more columns than expected.  Check data source." & vbCrLf & _
			  "Truncating data to expected amount of columns.")
			MaxCols = ExpectedCols
		End If

		'Set up rows
		DataGridView1.Rows.Add(Rows + 1)

		'Place data in datagrid
		Dim DataColumnCounter As Integer
		For RowCounter As Integer = 0 To Rows
			DataColumnCounter = -1
			For ColumnCounter As Integer = 0 To TotalNumDataColumns

				'  if we have a regular column, increment the data counter and see if we are out of bounds, if so fill with zeroes
				DataColumnCounter += 1
				If DataColumnCounter > strTwiceSplit.GetUpperBound(1) Then
					DataGridView1.Item(ColumnCounter, RowCounter).Value = (0.0).ToString
					Continue For
				End If

				'if we made it this far, we should be good to go with a regular pasted data value
				DataGridView1.Item(ColumnCounter, RowCounter).Value = strTwiceSplit(RowCounter, DataColumnCounter)

			Next

		Next

		'update units data grid
		For J As Integer = 0 To MaxCols
			Call UpdateUnitsGrid(J)
		Next J

		Call ConformOrOK()

		'manually check data for out of range
		Dim BadDataFound As Boolean = False
		For RowCounter As Integer = 0 To DataGridView1.Rows.Count - 1
			For ColumnCounter As Integer = 0 To DataGridView1.Columns.Count - 1
				If PublicData.DataValidationTest(DataGridView1.Item(ColumnCounter, RowCounter).Value) <> PublicData.DataValidationReturnType.Valid Then
					BadDataFound = True
					DataGridView1.Item(ColumnCounter, RowCounter).Value = 0
				End If
			Next
		Next
		If BadDataFound Then
			Call ShowWarningError("At least one bad data point was found after the data was pasted." & vbCrLf & _
			 "This can be a non-numeric data point, or an empty data point." & vbCrLf & _
			 "Any values have been replaced with a zero value to avoid problems after pasting." & vbCrLf & _
			 "Correct these before continuing to avoid any problems.")
		End If

		DataGridView1.CausesValidation = True

	End Sub

	Private Sub cmdClearGrids_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearGrids.Click
		Me.AlreadySetUp = False
		Dim Success As Boolean = InitializeDataGridsSuccess()
	End Sub
#End Region

#Region "Set up Context Menus"
	Private Sub cxtUnits_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles cxtUnits.Opening

		Dim MyArray As String() = PublicData.GetHeaderStrings
		If (MyArray Is Nothing) Then
			MsgBox("A call was made to set up catalog data headers from an unknown source.", MsgBoxStyle.Exclamation)
			Exit Sub
		End If

		Dim ColNum As Integer = DataGridUnits.SelectedCells(0).OwningColumn.Index

		'Try filling the columns column
		Try
			Me.cxtUnitsColumn.DropDownItems.Clear()
			For Each HeaderName As String In MyArray
				Dim sItem As New ToolStripMenuItem
				sItem.Name = HeaderName
				sItem.Text = HeaderName
				Me.cxtUnitsColumn.DropDownItems.Add(sItem)
				AddHandler sItem.Click, AddressOf ColumnItemClick
			Next

		Catch xc As Exception
			'meh

		End Try

		'Try filling the units column
		Try
			Dim ArrayHolder As String() = PublicData.GetUnitStrings(ColumnType(ColNum))

			Me.cxtUnitsUnits.DropDownItems.Clear()
			For Each sUnitName As String In ArrayHolder
				Dim sItem As New ToolStripMenuItem
				sItem.Name = sUnitName
				sItem.Text = sUnitName
				Me.cxtUnitsUnits.DropDownItems.Add(sItem)
				AddHandler sItem.Click, AddressOf UnitItemClick
			Next

		Catch xc As Exception
			'meh

		End Try

	End Sub

	Private Sub ColumnItemClick(ByVal sender As Object, ByVal e As EventArgs)
		Try

			Dim MyArray As String() = PublicData.GetHeaderStrings
			If (MyArray Is Nothing) Then
				MsgBox("A call was made to set up catalog data headers from an unknown source.", MsgBoxStyle.Exclamation)
				Exit Sub
			End If

			'Get original and other column indeces
			Dim ColNum As Integer = DataGridUnits.SelectedCells(0).OwningColumn.Index
			Dim I As Integer = 0
			Dim SwapCol As Integer = -1
			For Each sHeader As String In MyArray
				I += 1
				If CType(sender, ToolStripMenuItem).Text = sHeader Then SwapCol = I - 1
			Next

			'Set up temporary array
			Dim TempArray As String()
			Dim NumDataPoints As Integer = DataGridView1.RowCount
			ReDim TempArray(NumDataPoints)

			'Store data from that other column
			For J As Integer = 0 To NumDataPoints - 1
				TempArray(J) = DataGridView1.Item(SwapCol, J).Value
			Next
			'Move data from current column into other column
			For J As Integer = 0 To NumDataPoints - 1
				DataGridView1.Item(SwapCol, J).Value = DataGridView1.Item(ColNum, J).Value
			Next
			'Move temporary data into original column
			For J As Integer = 0 To NumDataPoints - 1
				DataGridView1.Item(ColNum, J).Value = TempArray(J)
			Next

		Catch ex As Exception
			'meh
		End Try
	End Sub

	Private Sub UnitItemClick(ByVal sender As Object, ByVal e As EventArgs)
		Dim ColNum As Integer = DataGridUnits.SelectedCells(0).OwningColumn.Index
		Dim ArrayHolder As String() = PublicData.GetUnitStrings(ColumnType(ColNum))
		Dim I As Integer = 0
		For Each sUnit As String In ArrayHolder
			I += 1
			If CType(sender, ToolStripMenuItem).Text = sUnit Then ColumnUnit(ColNum) = I - 1
		Next
		Call UpdateUnitsGrid(ColNum)
		Call ConformOrOK()
	End Sub

	Private Sub cxtUnitsConstant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cxtUnitsConstant.Click
		Dim ColNum As Integer = DataGridUnits.SelectedCells(0).OwningColumn.Index
		Dim MysVal As String = InputBox("Input the value to fill this column: " & vbCrLf & Me.DataGridView1.Columns(ColNum).HeaderText, _
		 "Enter constant value", 25.56)
		Dim MyVal As Double
		If Not Double.TryParse(MysVal, myval) Then
			Call ShowWarningError("Invalid entry in input box.  Value entered=" & MysVal & vbCrLf & _
				   "Data not entered in grid.")
			Return
		End If
		Dim DataValidation As PublicData.DataValidationReturnType = PublicData.DataValidationTest(MyVal)
		If DataValidation <> DataValidationReturnType.Valid Then
			Call ShowWarningError("Data entered could not be converted to a numeric value, please try again")
			Exit Sub
		End If
		Try
			For I As Integer = 0 To DataGridView1.Rows.Count - 1
				DataGridView1.Item(ColNum, I).Value = MyVal
			Next
		Catch xc As Exception
			Call ShowWarningError("Data could not be entered into data grid view.  Make sure cells are unlocked and try again.")
		End Try
	End Sub
#End Region

#Region "Units"
	Private Sub cmdIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIP.Click
		For I As Integer = 0 To DataGridView1.ColumnCount - 1
			If PublicData.GetBaseIPUnit(ColumnType(I)) <> -1 Then ColumnUnit(I) = PublicData.GetBaseIPUnit(ColumnType(I))
			Call UpdateUnitsGrid(I)
		Next
		Call ConformOrOK()
	End Sub

	Private Sub cmdSI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSI.Click
		For I As Integer = 0 To DataGridView1.ColumnCount - 1
			If PublicData.GetBaseSIUnit(ColumnType(I)) <> -1 Then ColumnUnit(I) = PublicData.GetBaseSIUnit(ColumnType(I))
			Call UpdateUnitsGrid(I)
		Next
		Call ConformOrOK()
	End Sub

	Private Sub UpdateUnitsGrid(ByVal J As Integer)
		Dim UnitString As String
		Dim UnitStrings As String() = GetUnitStrings(ColumnType(J))
		If UnitStrings IsNot Nothing AndAlso ColumnUnit(J) <= UnitStrings.GetUpperBound(0) Then
			UnitString = UnitStrings(ColumnUnit(J))
		Else
			UnitString = "-" 'for example, dimensionless
		End If
		DataGridUnits.Item(J, 0).Value = "[" & UnitString & "]"
	End Sub

	Private Sub btnConformUnits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConformUnits.Click

		Dim colCount As Integer = DataGridView1.ColumnCount - 1
		Dim rowCount As Integer = DataGridView1.RowCount - 1
		Dim TempConversion As Double
		Dim ErrCount As Integer = 0
		Dim MaxErrs As Integer = 0
		'turn off manual data entry to disallow values
		Me.chkManuallyEditData.Checked = False

		For I As Integer = 0 To colCount

			Dim UnitConverter As Func(Of Integer, Double, Double) = PublicData.GetUnitConverter(ColumnType(I))
			Dim CalculationUnit As Integer = PublicData.GetCalculationUnit(ColumnType(I))

			For J As Integer = 0 To rowCount
				TempConversion = UnitConverter(ColumnUnit(I), DataGridView1.Item(I, J).Value)
				If TempConversion = Nothing Then
					If ErrCount <= MaxErrs Then Call PublicData.ShowSevereError("Blank or erroneous data detected in catalog data.  Zeroes will fill as necessary.")
					ErrCount += 1
					TempConversion = 0.0
				End If
				DataGridView1.Item(I, J).Value = TempConversion
			Next
			ColumnUnit(I) = CalculationUnit

			Call UpdateUnitsGrid(I)
		Next

		Call ConformOrOK()	'btnOK.Enabled = True
	End Sub

	Private Sub ConformOrOK()
		Dim Conform As Boolean = False
		'Check for the first instance of a unit that needs to be updated
		For I As Integer = 0 To DataGridView1.ColumnCount - 1
			If ColumnUnit(I) <> PublicData.GetCalculationUnit(ColumnType(I)) Then
				Conform = True
				Exit For
			End If
		Next
		'If any were found, then force a conform; if not then allow OK
		If Conform = True Then
			Me.btnConformUnits.Enabled = True
			Me.btnConformUnits.BackColor = Color.GreenYellow
			Me.btnOK.Enabled = False
			Me.btnOK.BackColor = Color.FromKnownColor(KnownColor.Control)
		ElseIf Conform = False Then
			Me.btnConformUnits.Enabled = False
			Me.btnConformUnits.BackColor = Color.FromKnownColor(KnownColor.Control)
			Me.btnOK.Enabled = True
			Me.btnOK.BackColor = Color.GreenYellow
		End If
	End Sub
#End Region

#Region "Manually editing data"
	Private Sub chkManuallyEditData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkManuallyEditData.CheckedChanged
		If chkManuallyEditData.Checked Then
			Me.DataGridView1.AllowUserToAddRows = True
			Me.DataGridView1.AllowUserToDeleteRows = True
		Else
			Me.DataGridView1.AllowUserToAddRows = False
			Me.DataGridView1.AllowUserToDeleteRows = False
		End If
		Call ConformOrOK()
	End Sub

	Private Sub DataGridView1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
		Call RenameRowHeaders()
	End Sub

	Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
		Call RenameRowHeaders()
	End Sub

	Private Sub RenameRowHeaders()
		Dim rowCount As Integer = DataGridView1.RowCount - 1
		For I As Integer = 0 To rowCount
			DataGridView1.Rows(I).HeaderCell.Value = (I + 1).ToString
		Next
	End Sub
#End Region

#Region "Header/Data Repair"
	Private Sub btnRepairHeaders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepairHeaders.Click
		'allow scroll on repair form
		Dim RepairForm As New CatalogDataRepairForm
		Dim MaxSize As New Size(0, 0)

		Dim MyArray As String() = PublicData.GetHeaderStrings
		If MyArray Is Nothing Then
			Call PublicData.ShowSevereError("This equipment type has not yet been set up, cancelling...")
			Exit Sub
		End If

		Dim NumHeadersTotal = MyArray.Count
		Dim MaxLabelWidth As Integer = 0
		For I As Integer = 0 To NumHeadersTotal - 1
			If I = 0 Then

				RepairForm.Label1.Text = "Data In Column: " & MyArray(I) & " = {"
				For J As Integer = 0 To DataGridView1.RowCount - 1
					Dim CommaOrNot As String
					If J > 0 Then : CommaOrNot = "," : Else : CommaOrNot = "" : End If
					If DataGridView1.Item(I, J).Value = Nothing Then
						RepairForm.Label1.Text &= CommaOrNot & "__"
					Else
						RepairForm.Label1.Text &= CommaOrNot & DataGridView1.Item(I, J).Value.ToString
					End If
					If J > 10 Then
						RepairForm.Label1.Text &= " ... "
						Exit For
					End If
				Next J
				RepairForm.Label1.Text &= "}"

				For Each str As String In MyArray
					RepairForm.cbo0.Items.Add(str)
				Next

				MaxSize.Width = Math.Max(3 * RepairForm.lblInstructions.Left + RepairForm.lblInstructions.Width, RepairForm.Label1.Left + RepairForm.Label1.Width)

			ElseIf I > 0 Then

				Dim myLabel As New Label
				With myLabel
					.Name = ""
					.Top = RepairForm.Label1.Top + I * 50
					.Left = RepairForm.Label1.Left
					.AutoSize = True
					.Text = "Data Currently In Column: " & MyArray(I) & " = {"
					For J As Integer = 0 To DataGridView1.RowCount - 1
						Dim CommaOrNot As String
						If J > 0 Then : CommaOrNot = "," : Else : CommaOrNot = "" : End If
						If DataGridView1.Item(I, J).Value = Nothing Then
							.Text &= CommaOrNot & "__"
						Else
							.Text &= CommaOrNot & DataGridView1.Item(I, J).Value.ToString
						End If
						If J > 10 Then
							.Text &= " ... "
							Exit For
						End If
					Next J
					.Text &= "}"
				End With
				RepairForm.Controls.Add(myLabel)

				Dim mylabel2 As New Label
				With mylabel2
					.Name = ""
					.Top = RepairForm.lblActually.Top + I * 50
					.Left = 17
					.AutoSize = True
					.Text = "Actually belongs in column:"
				End With
				RepairForm.Controls.Add(mylabel2)

				Dim myCbo As New ComboBox
				With myCbo
					.Name = "Cbo" & I.ToString
					.Top = RepairForm.cbo0.Top + I * 50
					.Left = RepairForm.cbo0.Left
					.Size = RepairForm.cbo0.Size
					.DropDownStyle = ComboBoxStyle.DropDownList
					For Each str As String In MyArray
						.Items.Add(str)
					Next
				End With

				MaxSize.Width = Math.Max(MaxSize.Width, 3 * myLabel.Left + myLabel.Width)
				RepairForm.btnOK.Top = myCbo.Top + 30
				RepairForm.btnCancel.Top = RepairForm.btnOK.Top
				RepairForm.Controls.Add(myCbo)
				MaxSize.Height = RepairForm.btnOK.Bottom + 40

			End If
		Next

		RepairForm.MaximumSize = MaxSize
		RepairForm.Size = MaxSize

		For Each ctl As Control In RepairForm.Controls
			Dim cbo As ComboBox = TryCast(ctl, ComboBox)
			If cbo IsNot Nothing Then
				For I As Integer = 0 To NumHeadersTotal - 1
					If cbo.Name.ToUpper = "CBO" & I.ToString Then
						cbo.SelectedIndex = I
						cbo.Text = cbo.Items(I)
					End If
				Next
			End If
		Next

		Dim Approved As Boolean = False
		Dim NewArrayVals(NumHeadersTotal - 1) As Integer
		Dim DR As DialogResult = RepairForm.ShowDialog()
		If DR = Windows.Forms.DialogResult.OK Then
			For Each ctl As Control In RepairForm.Controls
				Dim cbo As ComboBox = TryCast(ctl, ComboBox)
				If cbo IsNot Nothing Then
					For I As Integer = 0 To NumHeadersTotal - 1
						If cbo.Name.ToUpper = "CBO" & I.ToString Then
							NewArrayVals(I) = cbo.SelectedIndex
							Exit For
						End If
					Next
				End If
			Next
			Call CheckRepairValues(NewArrayVals, NumHeadersTotal, Approved)
			If Approved = True Then Call RepairValues(NewArrayVals)
		End If


	End Sub

	Private Sub CheckRepairValues(ByVal NewArrayVals() As Integer, ByVal HeaderCount As Integer, ByRef Approved As Boolean)
		'First assume it's ok
		Approved = True

		'Sweep through all tentative header values
		For I As Integer = 0 To HeaderCount - 1

			'At each header value, sweep through all the rest of the values and make sure none match
			For J As Integer = 0 To HeaderCount - 1
				If I <> J Then
					If NewArrayVals(I) = NewArrayVals(J) Then Approved = False
				End If
			Next

		Next

	End Sub

	Private Sub RepairValues(ByVal NewArrayVals() As Integer)

		'Get max row and column indeces
		Dim TotalCols As Integer = DataGridView1.ColumnCount
		Dim TotalRows As Integer = DataGridView1.RowCount

		'Set up the temporary array to store values
		Dim TempArray(TotalCols - 1, TotalRows - 1) As String
		For I As Integer = 0 To TotalCols - 1
			For J As Integer = 0 To TotalRows - 1
				TempArray(I, J) = DataGridView1.Item(I, J).Value
			Next
		Next

		'Find column values
		Dim NewColVals(TotalCols - 1)
		Dim TempCounter As Integer = -1
		For Each I As Integer In NewArrayVals
			TempCounter += 1
			NewColVals(I) = TempCounter
		Next


		'Replace data in the datagridview as deemed by the new array values
		For I As Integer = 0 To TotalCols - 1
			For J As Integer = 0 To TotalRows - 1
				DataGridView1.Item(I, J).Value = TempArray(NewColVals(I), J)
			Next
		Next
	End Sub
#End Region

#Region "Synchronizing and controlling data grid views"

	Private Enum DataGridViewDirection
		UnitsToValues
		ValuesToUnits
	End Enum

	Private Sub InitializeGridViewSychronizers()
		AddHandler Me.DataGridUnits.ColumnWidthChanged, AddressOf DataGridUnits_ColumnWidthChanged
		AddHandler Me.DataGridView1.ColumnWidthChanged, AddressOf DataGridView1_ColumnWidthChanged
		AddHandler Me.DataGridUnits.RowHeadersWidthChanged, AddressOf DataGridUnits_RowHeaderWidthChanged
		AddHandler Me.DataGridView1.RowHeadersWidthChanged, AddressOf DataGridView1_RowHeaderWidthChanged
	End Sub

	Private Sub DataGridUnits_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridUnits.CellMouseDown
		If (e.Button = MouseButtons.Right) Then
			Dim ColInt As Integer = DirectCast(e, System.Windows.Forms.DataGridViewCellMouseEventArgs).ColumnIndex
			DataGridUnits.Item(ColInt, 0).Selected = True
		End If
	End Sub

	Private Sub DataGridUnits_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs)
		'turn off handlers
		RemoveHandler Me.DataGridUnits.ColumnWidthChanged, AddressOf DataGridUnits_ColumnWidthChanged
		RemoveHandler Me.DataGridView1.ColumnWidthChanged, AddressOf DataGridView1_ColumnWidthChanged
		'update widths
		Call UpdateColumnWidths(DataGridViewDirection.UnitsToValues)
		'turn back on handlers
		AddHandler Me.DataGridUnits.ColumnWidthChanged, AddressOf DataGridUnits_ColumnWidthChanged
		AddHandler Me.DataGridView1.ColumnWidthChanged, AddressOf DataGridView1_ColumnWidthChanged
	End Sub

	Private Sub DataGridView1_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs)
		'turn off handlers
		RemoveHandler Me.DataGridUnits.ColumnWidthChanged, AddressOf DataGridUnits_ColumnWidthChanged
		RemoveHandler Me.DataGridView1.ColumnWidthChanged, AddressOf DataGridView1_ColumnWidthChanged
		'update widths
		Call UpdateColumnWidths(DataGridViewDirection.ValuesToUnits)
		'turn back on handlers
		AddHandler Me.DataGridUnits.ColumnWidthChanged, AddressOf DataGridUnits_ColumnWidthChanged
		AddHandler Me.DataGridView1.ColumnWidthChanged, AddressOf DataGridView1_ColumnWidthChanged
	End Sub

	Private Sub DataGridUnits_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridUnits.Scroll
		Me.DataGridView1.HorizontalScrollingOffset = Me.DataGridUnits.HorizontalScrollingOffset
	End Sub

	Private Sub DataGridView1_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll
		Me.DataGridUnits.HorizontalScrollingOffset = Me.DataGridView1.HorizontalScrollingOffset
	End Sub

	Private Sub UpdateColumnWidths(ByVal dir As DataGridViewDirection)
		Dim srcDGV As DataGridView = Nothing
		Dim trgDGV As DataGridView = Nothing
		Select Case dir
			Case DataGridViewDirection.UnitsToValues
				srcDGV = DataGridUnits
				trgDGV = DataGridView1
			Case DataGridViewDirection.ValuesToUnits
				srcDGV = DataGridView1
				trgDGV = DataGridUnits
		End Select
		If srcDGV.ColumnCount = 0 Or trgDGV.ColumnCount = 0 Then Exit Sub
		For I As Integer = 0 To srcDGV.ColumnCount - 1
			trgDGV.Columns.Item(I).Width = srcDGV.Columns.Item(I).Width
		Next
	End Sub

	Private Sub DataGridUnits_RowHeaderWidthChanged(ByVal sender As Object, ByVal e As System.EventArgs)
		'turn off handlers
		RemoveHandler Me.DataGridUnits.RowHeadersWidthChanged, AddressOf DataGridUnits_RowHeaderWidthChanged
		RemoveHandler Me.DataGridView1.RowHeadersWidthChanged, AddressOf DataGridView1_RowHeaderWidthChanged
		'update widths
		Call UpdateRowHeaderWidths(DataGridViewDirection.UnitsToValues)
		'turn back on handlers
		AddHandler Me.DataGridUnits.RowHeadersWidthChanged, AddressOf DataGridUnits_RowHeaderWidthChanged
		AddHandler Me.DataGridView1.RowHeadersWidthChanged, AddressOf DataGridView1_RowHeaderWidthChanged
	End Sub

	Private Sub DataGridView1_RowHeaderWidthChanged(ByVal sender As Object, ByVal e As System.EventArgs)
		'turn off handlers
		RemoveHandler Me.DataGridUnits.RowHeadersWidthChanged, AddressOf DataGridUnits_RowHeaderWidthChanged
		RemoveHandler Me.DataGridView1.RowHeadersWidthChanged, AddressOf DataGridView1_RowHeaderWidthChanged
		'update widths
		Call UpdateRowHeaderWidths(DataGridViewDirection.ValuesToUnits)
		'turn back on handlers
		AddHandler Me.DataGridUnits.RowHeadersWidthChanged, AddressOf DataGridUnits_RowHeaderWidthChanged
		AddHandler Me.DataGridView1.RowHeadersWidthChanged, AddressOf DataGridView1_RowHeaderWidthChanged
	End Sub

	Private Sub UpdateRowHeaderWidths(ByVal dir As DataGridViewDirection)
		Dim srcDGV As DataGridView = Nothing
		Dim trgDGV As DataGridView = Nothing
		Select Case dir
			Case DataGridViewDirection.UnitsToValues
				srcDGV = DataGridUnits
				trgDGV = DataGridView1
			Case DataGridViewDirection.ValuesToUnits
				srcDGV = DataGridView1
				trgDGV = DataGridUnits
		End Select
		trgDGV.RowHeadersWidth = srcDGV.RowHeadersWidth
	End Sub

#End Region

#Region "Cell Validation"
	Private Sub dataGridView1_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
		'if nothing then who cares, skip over the validation call
		If e.FormattedValue = Nothing Then Exit Sub

		Dim ValidDataSignal As PublicData.DataValidationReturnType = PublicData.DataValidationTest(e.FormattedValue)

		Select Case ValidDataSignal
			Case DataValidationReturnType.NotNumeric
				Call ShowSevereError("Value (" & e.FormattedValue & ") entered is not valid, enter a valid number and try again." & vbCrLf & _
				   " - or press ESC to cancel data entry once this message box is closed")
				e.Cancel = True
			Case DataValidationReturnType.RangeMin, DataValidationReturnType.RangeMax
				'stub
			Case DataValidationReturnType.DataIsNothing
				'stub
			Case DataValidationReturnType.Valid
				'stub
		End Select

	End Sub
#End Region

End Class