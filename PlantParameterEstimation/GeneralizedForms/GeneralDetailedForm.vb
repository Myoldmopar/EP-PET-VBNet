Public Class GeneralDetailedForm

#Region " Variables "
	Private Const spacing As Integer = 10
	Private ErrorSignal As New ErrorProvider
	Private SkipFinalValidation As Boolean = False
	Dim DoIConformUnits As Boolean = True
#End Region

	'Public Structure DetailedFormReturnData
	'	Dim RealValue As Double
	'	Dim StringValue As String
	'	Dim IntegerValue As Integer
	'	Sub New(ByVal Dummy As Object, Optional ByVal real As Double = 0, Optional ByVal str As String = "", Optional ByVal int As Integer = 3)
	'		RealValue = real
	'		StringValue = str
	'		IntegerValue = int
	'	End Sub
	'End Structure

	Private _EntryList As New System.Collections.Generic.List(Of GeneralDetailedFormEntry)
	''' <summary>
	''' This provides the means to add entries into the detailed form
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property EntryList() As System.Collections.Generic.List(Of GeneralDetailedFormEntry)
		Get
			Return _EntryList
		End Get
		Set(ByVal value As System.Collections.Generic.List(Of GeneralDetailedFormEntry))
			_EntryList = value
			UpdateEntryLocations()
		End Set
	End Property

	Private _NameTagValueLookup As New System.Collections.Generic.Dictionary(Of String, Double)
	''' <summary>
	''' This provides public access to a paired list of nametags and double-valued entries parsed from the textboxes
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property NameTagValueLookup() As System.Collections.Generic.Dictionary(Of String, Double)
		Get
			Return _NameTagValueLookup
		End Get
		Set(ByVal value As System.Collections.Generic.Dictionary(Of String, Double))
			_NameTagValueLookup = value
		End Set
	End Property

	Private Sub UpdateEntryLocations()
		Dim ctr As Integer = -1
		For Each entry As GeneralDetailedFormEntry In Me.EntryList
			ctr += 1
			If Not Panel1.Controls.Contains(entry) Then
				Panel1.Controls.Add(entry)
			End If
			entry.Top = ctr * (entry.Height + spacing) + spacing
			entry.Left = spacing
			If ctr = 0 Then entry.txtDataValue.Select()
		Next
	End Sub

	Private Sub GeneralDetailedForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		UpdateEntryLocations()
		For Each entry As GeneralDetailedFormEntry In Me.Panel1.Controls
			AddHandler entry.txtDataValue.Validating, AddressOf DataValidation
			AddHandler entry.txtDataValue.Validated, AddressOf DataValidated
			AddHandler entry.cboDataUnits.SelectedIndexChanged, AddressOf HandleCorrDataFormCboChange
		Next

	End Sub

	Private Sub DataValidation(ByVal Sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
		Dim txt As TextBox = TryCast(Sender, TextBox)
		If txt Is Nothing Then Exit Sub
		If txt.Text = Nothing Then Exit Sub
		Dim ValidDataSignal As PublicData.DataValidationReturnType = PublicData.DataValidationTest(txt.Text)
		If ValidDataSignal <> DataValidationReturnType.Valid Then
			e.Cancel = True
			txt.SelectAll()
			ErrorSignal.SetError(txt, "Entered value is not valid, please make a valid numeric entry or leave blank for now.")
		End If
	End Sub

	Private Sub DataValidated(ByVal Sender As Object, ByVal e As System.EventArgs)
		Dim txt As TextBox = TryCast(Sender, TextBox)
		If txt Is Nothing Then Exit Sub
		ErrorSignal.SetError(txt, "")
	End Sub

	Private Sub HandleCorrDataFormCboChange(ByVal sender As Object, ByVal e As System.EventArgs)
		If Not DoIConformUnits Then Exit Sub

		Dim NeedToConform As Boolean = False

		'create arrays of combo/text boxes to conform
		Dim cbolist As New System.Collections.Generic.List(Of ComboBox)
		Dim unitlist As New System.Collections.Generic.List(Of PublicData.UnitType)
		Dim txtlist As New System.Collections.Generic.List(Of TextBox)
		For Each entry As GeneralDetailedFormEntry In Me.Panel1.Controls
			cbolist.Add(entry.cboDataUnits)
			unitlist.Add(entry.UnitType)
			txtlist.Add(entry.txtDataValue)
		Next
		
		For ItemCounter As Integer = 0 To cbolist.Count - 1
			Dim curCbo As ComboBox = cbolist(ItemCounter)
			Dim curTxt As TextBox = txtlist(ItemCounter)
			Dim CurUnitIndex As Integer = curCbo.SelectedIndex
			NeedToConform = Not PublicData.ValidateUnitIndex(unitlist(ItemCounter), CurUnitIndex)
			If NeedToConform Then Exit For
		Next

		With Me.btnOK
			If NeedToConform Then
				.Text = "Conform Units"
				.DialogResult = Windows.Forms.DialogResult.None
				AddHandler Me.btnOK.Click, AddressOf ConformUnits
			Else
				.Text = "Done"
				.DialogResult = Windows.Forms.DialogResult.OK
				RemoveHandler Me.btnOK.Click, AddressOf ConformUnits
			End If
		End With

	End Sub

	Private Sub ConformUnits(ByVal sender As System.Object, ByVal e As EventArgs)
		'first do the replacement data 
		Dim ErrorsEncountered As Boolean = False
		DoIConformUnits = False

		Call ConformUnitSub(ErrorsEncountered)

		DoIConformUnits = True
		Call HandleCorrDataFormCboChange(sender, e)
	End Sub

	Private Sub ConformUnitSub(ByVal ErrorsEncountered As Boolean)
		Dim TempConversion As Double
		Dim UnitType As PublicData.UnitType

		'create arrays of combo/text boxes to conform
		Dim cbolist As New System.Collections.Generic.List(Of ComboBox)
		Dim unitlist As New System.Collections.Generic.List(Of PublicData.UnitType)
		Dim txtlist As New System.Collections.Generic.List(Of TextBox)
		'Dim datatypelist As New System.Collections.Generic.List(Of PublicData.DetailedDataType)
		For Each entry As GeneralDetailedFormEntry In Me.Panel1.Controls
			cbolist.Add(entry.cboDataUnits)
			unitlist.Add(entry.UnitType)
			txtlist.Add(entry.txtDataValue)
			'	datatypelist.Add(entry.DetailedDataType)
		Next
		
		For ItemCounter As Integer = 0 To cbolist.Count - 1

			'skip out if this is not a normal units case
			'If datatypelist(ItemCounter) = DetailedDataType.None Then Continue For

			Dim curCbo As ComboBox = cbolist(ItemCounter)
			Dim curTxt As TextBox = txtlist(ItemCounter)

			Dim CurUnitIndex As Integer = curCbo.SelectedIndex
			UnitType = unitlist(ItemCounter)
			Dim NumSigDigits As Integer = 5

			Dim UnitConverter As Func(Of Integer, Double, Double) = PublicData.GetUnitConverter(UnitType)
			Dim CalculationUnit As Integer = PublicData.GetCalculationUnit(UnitType)

			If CurUnitIndex <> CalculationUnit Then
				Dim Validity As PublicData.DataValidationReturnType = DataValidationTest(curTxt.Text)
				If Validity <> DataValidationReturnType.Valid Then
					'don't do anything, so that data will still need to be conformed
					Continue For
				End If
				TempConversion = UnitConverter(CurUnitIndex, curTxt.Text)
				If TempConversion = Nothing Then
					TempConversion = 0.0
					ErrorsEncountered = True
				End If
				curTxt.Text = Math.Round(TempConversion, NumSigDigits)
				curCbo.SelectedIndex = CalculationUnit
			End If

		Next
	End Sub

	Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
		SkipFinalValidation = True
	End Sub

	Private Sub GeneralDetailedForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

		'if user cancels out, flag will be true:
		If SkipFinalValidation Then Exit Sub

		'otherwise, confirm that all values are good to go
		Call ConformUnitSub(e.Cancel)
		If e.Cancel Then
			Call ShowSevereError("A value could not be converted to a number.  Please check entries and retry")
			Exit Sub
		End If

		'if we made it this far, all values are legitimate numerics, so we won't do error handling here
		Me.NameTagValueLookup.Clear()
		For Each entry As GeneralDetailedFormEntry In Me.Panel1.Controls
			Dim val As Double
			'If entry.DetailedDataType = DetailedDataType.None Then
			Double.TryParse(entry.txtDataValue.Text, val)
			'Else
			'val = CDbl(entry.cboDataUnits.SelectedIndex)
			'End If
			Me.NameTagValueLookup.Add(entry.NameTag, val)
		Next

	End Sub

End Class