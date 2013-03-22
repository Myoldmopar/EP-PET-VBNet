Public Class CorrectionFactorForm

#Region "Variables"
	Private Const spacing As Integer = 20
	Private ShowWBDBInfoMsg As Boolean = True
	Private CorrFactorControlList As New System.Collections.Generic.List(Of CorrectionFactorEntry)
#End Region

	Private Sub CorrectionFactorForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Skip
	End Sub

	Private Sub btnAdd2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd2.Click
		Call AddANewFactor()
	End Sub

	Private Sub btnDone2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone2.Click
		Call GetCorrectionDataSummary()
	End Sub

	Private Sub AddANewFactor()

		Dim MyArray As String() = PublicData.GetHeaderStrings
		Dim MyUnits As Integer() = PublicData.GetHeaderUnits
		If (MyArray Is Nothing) Or (MyUnits Is Nothing) Then
			Call ShowSevereError("An erroneous call was made to set up catalog data headers.  Ensure a proper equipment selection was made and re-try.")
			PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
			Exit Sub
		End If

		'get correction factor name
		Dim myName As String
		myName = InputBox("A name for this correction factor data set", "Correction Set Name", "New Correction Factor Set")
		If myName = "" Or myName = Nothing Then Exit Sub 'nothing else needs to happen

		Dim newCorrFactor As New CorrectionFactorEntry
		With newCorrFactor
			.CorrectionFactorName = myName
			.Left = spacing
			AddHandler .RemoveButtonClick, AddressOf RemoveAFactor
			AddHandler .cboBaseSelectedIndexChanged, AddressOf NoteThatDataHasChanged
			AddHandler .chkWBDBCheckedChanged, AddressOf HandleCheckBox
			AddHandler .chkWBDBCheckedChanged, AddressOf NoteThatDataHasChanged
			AddHandler .rdoChanged, AddressOf NoteThatDataHasChanged
			AddHandler .chkCboCheckedChanged, AddressOf NoteThatDataHasChanged
			.chkWBDB.Enabled = (PublicData.GetWBValue <> -1) 'if wb column value = -1 this is false
			.FillCbosWithStringArray(MyArray)
			If Me.CorrFactorControlList.Count > 0 Then 'there is at least one above me
				'let's find the group box right above us
				Dim OneGroupUp As CorrectionFactorEntry = CorrFactorControlList(Me.CorrFactorControlList.Count - 1)
				Dim Booleans As New System.Collections.Generic.List(Of Boolean)
				Dim MyChkCbo As PresentationControls.CheckBoxComboBox = OneGroupUp.ChkCboExposed
				For Each item As PresentationControls.CheckBoxComboBoxItem In MyChkCbo.CheckBoxItems
					Booleans.Add(item.Checked)
				Next
				.FillChkCboWithBooleans(Booleans.ToArray)
			End If
		End With
		Me.CorrFactorControlList.Add(newCorrFactor)
		Me.Panel1.Controls.Add(newCorrFactor)
		Call UpdateCorrFactorIndecesAndLocations()
		btnDone2.Text = "Done"

	End Sub

	Private Sub UpdateCorrFactorIndecesAndLocations()

		Me.Panel1.AutoScrollPosition = New Point(0, 0)

		For I As Integer = 0 To Me.CorrFactorControlList.Count - 1
			Me.CorrFactorControlList(I).CorrFactorIndex = I
			Me.CorrFactorControlList(I).Top = I * (Me.CorrFactorControlList(I).Height + spacing) + spacing
		Next

	End Sub

	Private Sub RemoveAFactor(ByVal MyIndex As Integer)

		Me.Panel1.Controls.Remove(Me.CorrFactorControlList(MyIndex))
		Me.CorrFactorControlList.RemoveAt(MyIndex)
		Me.UpdateCorrFactorIndecesAndLocations()
		If Me.CorrFactorControlList.Count = 0 Then btnDone2.Text = "Skip"

	End Sub

	Private Sub GetCorrectionDataSummary()

		Dim MyArray As String() = PublicData.GetHeaderStrings
		Dim MyUnits As Integer() = PublicData.GetHeaderUnits
		If (MyArray Is Nothing) Or (MyUnits Is Nothing) Then
			Call ShowSevereError("An erroneous call was made to set up catalog data headers.  Ensure a proper equipment selection was made and re-try.")
			PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
			Exit Sub
		End If

		'assume we will have success, then modify later if necessary
		PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Done

		'alert caller if there were no correction factors to be handled
		If Me.CorrFactorControlList.Count = 0 Then
			PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Skip
			Exit Sub
		End If

		'reinitialize the correction factor array if needed
		If Not PublicData.CorrectionDataInPlace Then
			ReDim PublicData.FullCorrectionFactorData(Me.CorrFactorControlList.Count - 1)
			For I As Integer = PublicData.FullCorrectionFactorData.GetLowerBound(0) To PublicData.FullCorrectionFactorData.GetUpperBound(0)
				PublicData.FullCorrectionFactorData(I) = New ParametricDataSet
			Next
		End If

		'loop through all factors and set up the data
		For FactorCounter As Integer = 0 To Me.CorrFactorControlList.Count - 1
			Dim TempFactor As CorrectionFactorEntry = CorrFactorControlList(FactorCounter)

			'correction factor name
			Dim Name As String = TempFactor.CorrectionFactorName
			PublicData.FullCorrectionFactorData(FactorCounter).Name = Name

			'correction factor base index
			Dim baseIndex As Integer = TempFactor.BaseCboIndex
			If baseIndex = -1 Then
				Call ShowSevereError("Base column not found for correction factor: " & Name)
				PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
				Exit Sub
			Else
				PublicData.FullCorrectionFactorData(FactorCounter).BaseColumnIndex = baseIndex
			End If

			'correction factor modification type
			If TempFactor.rdoMultiplierChecked Then
				PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Multiplier
			ElseIf TempFactor.rdoReplacementChecked Then
				PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Replacer
			Else
				Call ShowSevereError("No data type selected (multiplier/replacement) for correction factor: " & Name)
				PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
				Exit Sub
			End If

			'affected data
			Dim chkCbo As PresentationControls.CheckBoxComboBox = TempFactor.ChkCboExposed
			Dim TFarray(chkCbo.CheckBoxItems.Count - 1) As Boolean
			Dim FoundAnyTrue As Boolean = False
			For I As Integer = 0 To chkCbo.CheckBoxItems.Count - 1
				If chkCbo.CheckBoxItems(I).Checked Then
					TFarray(I) = True
					FoundAnyTrue = True
				Else
					TFarray(I) = False
				End If
			Next
			If Not FoundAnyTrue Then
				Call ShowSevereError("It seems no affected data columns have been selected." & vbCrLf & _
				   "Please select at least one affected data column to which corrections will be applied." & vbCrLf & _
				   "Occurred in Correction Factor #" & (FactorCounter + 1).ToString & ": " & Name)
				PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
				Exit Sub
			End If
			PublicData.FullCorrectionFactorData(FactorCounter).ColumnsToModify = TFarray

			'number of correction values
			Dim MyVal As Integer
			If Integer.TryParse(TempFactor.NumDataText, MyVal) Then
				If MyVal = Nothing Or MyVal = 0 Then
					Call ShowSevereError("The number of correction values appeared to be zero or null." & vbCrLf & _
					  "Ensure that the number of values is greater than zero." & vbCrLf & _
					"Occurred in Correction Factor #" & (FactorCounter + 1).ToString & ": " & PublicData.FullCorrectionFactorData(FactorCounter).Name)
					PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
					Exit Sub
				Else
					PublicData.FullCorrectionFactorData(FactorCounter).NumCorrections = MyVal
				End If
			Else
				Call ShowSevereError("Could not convert the # of values given to an integer." & vbCrLf & _
				   "This could indicate a letter or symbol was entered." & vbCrLf & _
				  "Ensure that the number of values is greater than zero." & vbCrLf & _
				  "Occurred in Correction Factor #" & (FactorCounter + 1).ToString & ": " & PublicData.FullCorrectionFactorData(FactorCounter).Name)
				PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
				Exit Sub
			End If

			'WB/DB check state
			PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorIsaWBDBSet = TempFactor.ChkWBDBChecked

			'any last minute error handling here once all data is in place
			If PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorIsaWBDBSet AndAlso _
			   PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Multiplier Then
				Call ShowSevereError("There is an expected mismatch in the data format for correction: " & PublicData.FullCorrectionFactorData(FactorCounter).Name & vbCrLf & _
				   "The correction is a wet-bulb + dry-bulb set, but yet the modification type is -multiplier-." & vbCrLf & _
				   "This is irregular, and not supported in this program.  Please change to -replacement- or uncheck wet bulb correction box." & vbCrLf & _
				  "Occurred in Correction Factor #" & (FactorCounter + 1).ToString & ": " & PublicData.FullCorrectionFactorData(FactorCounter).Name)
				PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
				Exit Sub
			End If

		Next

	End Sub

	Private Sub NoteThatDataHasChanged(ByVal CorrFactorIndex As Integer)
		PublicData.CorrectionDataInPlace = False
	End Sub

	Private Sub HandleCheckBox(ByVal CorrFactorIndex As Integer)

		'if we are checked, then let's automatically set replacement type and wet bulb as the base column
		Dim CorrFactor As CorrectionFactorEntry = Me.CorrFactorControlList(CorrFactorIndex)
		If Not CorrFactor.ChkWBDBChecked Then Exit Sub
		CorrFactor.rdoReplacementChecked = True

		'set combobox to our wet bulb value
		If PublicData.GetWBValue <> -1 Then
			CorrFactor.BaseCboIndex = PublicData.GetWBValue
		End If

		'now alert user
		If Me.ShowWBDBInfoMsg = False Then Exit Sub
		Call PublicData.ShowInfoBox( _
		 "Correction factor sets which correct for variations in Dry Bulb and Wet Bulb at the same time require special treatment." & vbCrLf & _
		 "In most cases, the data is offered as a table with the first column being wet bulb, general correction factor columns, " & vbCrLf & _
		 " and a series of dry bulb columns.  To enter these here, one should enter each column of dry bulb as a separate correction factor." & vbCrLf & _
		 "Currently this is the smoothest method for translating the correction factors programmatically." & vbCrLf & _
		 "Thus, if your factor has 5 wet bulb rows, 3 dry bulb columns, and 2 other factors, three factors would need to be entered." & vbCrLf & _
		 "Each would have the number of valid wet bulb rows (up to 5 for this particular data), " & vbCrLf & _
		 "the 2 other factors, and a single value of dry bulb entered separately." & vbCrLf & _
		 "The value of dry bulb will be entered in a textbox above the tabulated correction factor data.")
		Me.ShowWBDBInfoMsg = False

	End Sub

	Private Sub CorrectionFactorForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		If PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error Then e.Cancel = True
	End Sub

End Class

