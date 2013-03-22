Public Class CorrectionFactorDataForm

	Private Sub CorrectionFactorDataForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		'set up captions
		Dim Caption As String

		Caption = "The dry bulb value is used when a dry-bulb/wet-bulb correction is being entered." & vbCrLf & _
		"A single correction is entered for each dry bulb value.  For each dry bulb value" & vbCrLf & _
		"there will be a set of wet bulbs, which each have their own correction factors for heat transfer, etc."
		TT.SetToolTip(Me.Label3, Caption)
		TT.SetToolTip(Me.txtDB, Caption)
		TT.SetToolTip(Me.cboDB, Caption)

		Caption = "The units must be entered for the correction data if the type is 'replacement'." & vbCrLf & _
		  "This is because the units must be conformed properly before the tabular catalog data is entered."
		TT.SetToolTip(Me.cboConstValue, Caption)

		Caption = "This is where the correction values are entered.  The first column will modify the base data." & vbCrLf & _
		 "These will be either replacement values or multipliers.  The other columns will be multipliers for the affected data."
		TT.SetToolTip(Me.DataGridView1, Caption)

		'set up error handler
		ErrorSignal.BlinkRate = 500
		ErrorSignal.BlinkStyle = ErrorBlinkStyle.AlwaysBlink

	End Sub

	Private Sub chkAddRows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAddRows.CheckedChanged
		If chkAddRows.Checked Then
			Me.DataGridView1.AllowUserToAddRows = True
			Me.DataGridView1.AllowUserToDeleteRows = True
		Else
			Me.DataGridView1.AllowUserToAddRows = False
			Me.DataGridView1.AllowUserToDeleteRows = False
		End If
	End Sub

#Region "Combobox Change Handling"

	Private Sub cboConstValue_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboConstValue.SelectedIndexChanged
		Dim UnitType As Integer
		Try
			UnitType = DirectCast(cboConstValue.Tag, Integer)
		Catch xc As Exception
			UnitType = Nothing
		End Try

		Dim CurIndex As Integer = cboConstValue.SelectedIndex
		Dim myInfo As PublicData.CboInformation
		myInfo.UnitType = UnitType
		myInfo.CurIndex = CurIndex
		If DoIConformUnits Then HandleCorrDataFormCboChange(myInfo)
	End Sub

	Private Sub cboDB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDB.SelectedIndexChanged
		Dim UnitType As Integer
		Try
			UnitType = DirectCast(cboDB.Tag, Integer)
		Catch xc As Exception
			UnitType = Nothing
		End Try

		Dim CurIndex As Integer = cboDB.SelectedIndex
		Dim myInfo As PublicData.CboInformation
		myInfo.UnitType = UnitType
		myInfo.CurIndex = CurIndex
		If DoIConformUnits Then HandleCorrDataFormCboChange(myInfo)
	End Sub

	Private Sub HandleCorrDataFormCboChange(ByVal myInfo As PublicData.CboInformation)
		Dim NeedToConform As Boolean = False
		If myInfo.CurIndex <> 9999 Then
			Dim UnitType As Integer = myInfo.UnitType
			Dim CurIndex As Integer = myInfo.CurIndex
			NeedToConform = Not PublicData.ValidateUnitIndex(UnitType, CurIndex)
		End If
		With Me.Button1
			If NeedToConform Then
				.Text = "Conform Units"
				.DialogResult = Windows.Forms.DialogResult.None
				AddHandler Me.Button1.Click, AddressOf ConformUnits
			Else
				.Text = "Done"
				.DialogResult = Windows.Forms.DialogResult.OK
				RemoveHandler Me.Button1.Click, AddressOf ConformUnits
			End If
		End With
	End Sub

	Dim DoIConformUnits As Boolean = True
	Private Sub ConformUnits(ByVal sender As System.Object, ByVal e As EventArgs)

		'first do the replacement data 
		Dim ErrorsEncountered As Boolean = False
		DoIConformUnits = False
		Dim TempConversion As Double
		Dim UnitType As PublicData.UnitType
		Dim CurUnitIndex As Integer = cboConstValue.SelectedIndex
		Try
			UnitType = DirectCast(cboConstValue.Tag, Integer)
		Catch ex As Exception
			'handle error, not sure what this error would be though
			DoIConformUnits = True
		End Try
		Dim rowCount As Integer = DataGridView1.RowCount - 1
		Dim NumSigDigits As Integer = 5

		Dim UnitConverter As Func(Of Integer, Double, Double) = PublicData.GetUnitConverter(UnitType)
		Dim CalculationUnit As Double = PublicData.GetCalculationUnit(UnitType)

		If CurUnitIndex <> CalculationUnit Then
			For I As Integer = 0 To rowCount
				TempConversion = UnitConverter(CurUnitIndex, DataGridView1.Item(0, I).Value)
				If TempConversion = Nothing Then
					TempConversion = 0.0
					ErrorsEncountered = True
				End If
				DataGridView1.Item(0, I).Value = Math.Round(TempConversion, NumSigDigits)
			Next
			cboConstValue.SelectedIndex = CalculationUnit
		End If

		'then do the dry bulb if needed
		If Me.txtDB.Enabled Then
			Dim CurDBUnitIndex As Integer = cboDB.SelectedIndex
			If CurDBUnitIndex <> PublicData.TempUnits.CalculationUnit Then
				Dim BaseVal As Double
				Try
					BaseVal = CDbl(txtDB.Text)
				Catch ex As Exception
					Call ShowSevereError("A severe error was encountered when attempting to conform dry bulb units." & vbCrLf & _
					   "Ensure that dry bulb value is numeric (not a letter), and try again.")
					DoIConformUnits = True
					Exit Sub
				End Try
				TempConversion = UnitConversion.TempDegC(CurDBUnitIndex, BaseVal)
				txtDB.Text = Math.Round(TempConversion, NumSigDigits).ToString
				cboDB.SelectedIndex = PublicData.TempUnits.CalculationUnit
			End If
		End If

		DoIConformUnits = True
		Dim TempCboInfo As PublicData.CboInformation
		TempCboInfo.UnitType = Nothing
		TempCboInfo.CurIndex = 9999
		HandleCorrDataFormCboChange(TempCboInfo)

	End Sub

#End Region

#Region "Cell/Textbox Data Validation"
	Private RangeChecks As Integer = 0
	Private Const MaxRangeChecks As Integer = 1
	Private Const MinMult As Double = 0.01
	Private Const MaxMult As Double = 1.99

	Private Sub dataGridView1_CellValidating(ByVal sender As Object, ByVal e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
		'if nothing then who cares, skip over the validation call
		If e.FormattedValue = Nothing Then Exit Sub

		Dim ValidDataSignal As PublicData.DataValidationReturnType = PublicData.DataValidationTest(e.FormattedValue, MinMult, MaxMult)

		Select Case ValidDataSignal
			Case DataValidationReturnType.NotNumeric
				Call ShowSevereError("Value (" & e.FormattedValue & ") entered is not valid, enter a valid number and try again." & vbCrLf & _
				   " - or press ESC to cancel data entry once this message box is closed")
				e.Cancel = True
			Case DataValidationReturnType.RangeMin, DataValidationReturnType.RangeMax
				If RangeChecks < MaxRangeChecks Then
					If e.ColumnIndex > 0 Then
						Call ShowWarningError("Value (" & e.FormattedValue & ") entered.  While it may actually be valid, it is out of the normal range for multipliers: (" & MinMult.ToString & " < x < " & MaxMult.ToString & ")" & vbCrLf & _
						  "Note that this warning is only issued " & MaxRangeChecks.ToString & " time(s) before all values are automatically accepted.")
						RangeChecks += 1
					End If
				End If
			Case DataValidationReturnType.DataIsNothing
				'stub
			Case DataValidationReturnType.Valid
				'stub
		End Select
	End Sub

	Private Sub txtDB_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDB.Validating
		If txtDB.Text = Nothing Then Exit Sub
		Dim ValidDataSignal As PublicData.DataValidationReturnType = PublicData.DataValidationTest(txtDB.Text)
		If ValidDataSignal <> DataValidationReturnType.Valid Then
			e.Cancel = True
			txtDB.SelectAll()
			ErrorSignal.SetError(txtDB, "Entered value is not valid, please make a valid numeric entry or leave blank for now.")
		End If
	End Sub
	Private Sub txtDB_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDB.Validated
		ErrorSignal.SetError(txtDB, "")
	End Sub

	Private SkipFinalValidation As Boolean = False
	Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
		SkipFinalValidation = True
	End Sub

	Private Sub CorrectionFactorDataForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		'if user cancels out, flag will be true:
		If SkipFinalValidation Then Exit Sub

		'this sub will perform a single sweep through to validate all data before the window closes and we move on
		Dim okToLeave As Boolean = True
		Dim ReasonNotOK As String = ""

		'first the critical values that will halt the program
		If txtDB.Enabled Then
			If okToLeave Then
				If PublicData.DataValidationTest(txtDB.Text) <> DataValidationReturnType.Valid Then
					okToLeave = False
					ReasonNotOK = "The dry bulb value textbox contained an invalid or blank value."
					txtDB.Focus()
					txtDB.SelectAll()
				End If
			End If
		End If
		If cboConstValue.Enabled Then
			If okToLeave Then
				If cboConstValue.SelectedIndex < 0 Then
					okToLeave = False
					ReasonNotOK = "The drop down box for the constant value units contained an invalid value."
				End If
			End If
		End If
		If cboDB.Enabled Then
			If okToLeave Then
				If cboDB.SelectedIndex < 0 Then
					okToLeave = False
					ReasonNotOK = "The drop down box for the dry bulb temperature units contained an invalid value."
				End If
			End If
		End If
		For ColCtr As Integer = 0 To DataGridView1.ColumnCount - 1
			If okToLeave Then
				For RowCtr As Integer = 0 To DataGridView1.RowCount - 1
					If PublicData.DataValidationTest(DataGridView1.Item(ColCtr, RowCtr).Value) <> DataValidationReturnType.Valid Then
						okToLeave = False
						ReasonNotOK = "At least one value in the data grid contained an invalid value." & vbCrLf & _
						 "First instance of error at (row " & (RowCtr + 1).ToString & ", column " & (ColCtr + 1).ToString & ")." & vbCrLf & _
						 "Further cells in the data grid were not validated."
						Exit For
					End If
				Next
			End If
		Next
		If Not okToLeave Then
			e.Cancel = True
			Call ShowSevereError("There was an error validating the data on this form." & vbCrLf & _
			   ReasonNotOK & vbCrLf & _
			   "Please verify inputs and retry")
			Exit Sub
		End If

		'if ok till now then go back and give the data grid columns a smoothness check
		Dim UnsmoothData As Boolean = False
		Dim UnSmoothColumn As Integer
		UnsmoothData = False
		If DataGridView1.RowCount > 2 Then
			For ColCtr = 0 To DataGridView1.ColumnCount - 1
				If UnsmoothData Then Exit For
				For RowCtr As Integer = 1 To DataGridView1.RowCount - 2
					Dim Value As Double = CDbl(DataGridView1.Item(ColCtr, RowCtr).Value)
					Dim ValueAbove As Double = CDbl(DataGridView1.Item(ColCtr, RowCtr - 1).Value)
					Dim ValueBelow As Double = CDbl(DataGridView1.Item(ColCtr, RowCtr + 1).Value)
					If ((ValueAbove <= Value) AndAlso (Value <= ValueBelow)) Or ((ValueAbove >= Value) AndAlso (Value >= ValueBelow)) Then
						Continue For
					End If
					UnsmoothData = True
					UnSmoothColumn = ColCtr + 1
					Exit For
				Next
			Next
		End If
		If UnsmoothData Then
			Dim DR As MsgBoxResult = MsgBox("The data in column " & (UnSmoothColumn).ToString & " (" & DataGridView1.Columns(UnSmoothColumn - 1).HeaderText & ") does not appear to be smooth." & vbCrLf & _
	"This may be perfectly fine, but may require a second look for any entered typos." & vbCrLf & _
	"Would you like to go back and check the data?", MsgBoxStyle.YesNo, "Unsmooth data warning")
			If DR = MsgBoxResult.Yes Then
				e.Cancel = True
				Exit Sub
			End If
		End If

	End Sub
#End Region

	Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
		Dim dgv As DataGridView = TryCast(sender, DataGridView)
		Dim txt As TextBox = TryCast(e.Control, TextBox)
		If dgv IsNot Nothing AndAlso txt IsNot Nothing Then
			AddHandler txt.KeyDown, AddressOf EditModeKeyDownHandler
		End If
	End Sub

	Private Sub EditModeKeyDownHandler(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
		If e.KeyCode = Keys.V AndAlso e.Modifiers = Keys.Control Then
			Me.DataGridView1.EndEdit()
			DataGridViewCopyPaste.PasteDataGridViewCells(Me.DataGridView1)
		End If
	End Sub

	Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
		Select Case e.KeyCode
			Case Keys.V
				If Me.DataGridView1.IsCurrentCellInEditMode Then
					Try
						Me.DataGridView1.EndEdit()
					Catch
						'just let it go I think
					End Try
				End If
				If e.Modifiers = Keys.Control Then Call DataGridViewCopyPaste.PasteDataGridViewCells(Me.DataGridView1)
			Case Keys.C
				If e.Modifiers = Keys.Control Then Call DataGridViewCopyPaste.CopyDataGridViewCells(Me.DataGridView1, True)
		End Select
	End Sub

End Class