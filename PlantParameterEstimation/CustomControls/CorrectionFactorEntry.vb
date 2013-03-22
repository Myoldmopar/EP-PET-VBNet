Public Class CorrectionFactorEntry

	''' <summary>
	''' Used to provide a user-meaningful name to this correction factor set
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property CorrectionFactorName() As String
		Get
			Return Me.grpOverall.Text
		End Get
		Set(ByVal value As String)
			Me.grpOverall.Text = value
		End Set
	End Property

	Private _CorrFactorIndex As Integer
	''' <summary>
	''' Used to provide an index of the correction factors entered
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property CorrFactorIndex() As Integer
		Get
			Return _CorrFactorIndex
		End Get
		Set(ByVal value As Integer)
			_CorrFactorIndex = value
		End Set
	End Property

	Public Property ChkWBDBChecked() As Boolean
		Get
			Return Me.chkWBDB.Checked
		End Get
		Set(ByVal value As Boolean)
			Me.chkWBDB.Checked = value
		End Set
	End Property

	Public Property ChkWBDBEnabled() As Boolean
		Get
			Return Me.chkWBDB.Enabled
		End Get
		Set(ByVal value As Boolean)
			Me.chkWBDB.Enabled = value
		End Set
	End Property

	Public Property rdoMultiplierChecked() As Boolean
		Get
			Return rdoMultiplier.Checked
		End Get
		Set(ByVal value As Boolean)
			rdoMultiplier.Checked = value
		End Set
	End Property

	Public Property rdoReplacementChecked() As Boolean
		Get
			Return rdoReplacement.Checked
		End Get
		Set(ByVal value As Boolean)
			rdoReplacement.Checked = value
		End Set
	End Property

	Public Property ChkCboExposed() As PresentationControls.CheckBoxComboBox
		Get
			Return Me.chkCbo
		End Get
		Set(ByVal value As PresentationControls.CheckBoxComboBox)
			Me.chkCbo = value
		End Set
	End Property

	Public ReadOnly Property NumDataText() As String
		Get
			Return Me.txtCorrValues.Text
		End Get
	End Property

	Public Event RemoveButtonClick(ByVal CorrFactorIndex As Integer)
	Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
		RaiseEvent RemoveButtonClick(Me._CorrFactorIndex)
	End Sub

	Private Sub txtCorrValues_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtCorrValues.Validating
		Dim Text As String = txtCorrValues.Text
		Dim IntegerText As Integer
		If Not Integer.TryParse(Text, IntegerText) Then
			e.Cancel = True
			txtCorrValues.SelectAll()
			Me.ErrorProvider1.SetError(txtCorrValues, "Entered value is not valid, please enter a positive number or leave blank for now.")
		End If
	End Sub

	Private Sub txtCorrValues_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCorrValues.Validated
		Dim txt As TextBox = TryCast(sender, TextBox)
		If txt Is Nothing Then Exit Sub
		Me.ErrorProvider1.SetError(txt, "")
	End Sub

	Public Event DataPointsTextBoxTextChanged(ByVal CorrFactorIndex As Integer)
	Private Sub txtCorrValues_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCorrValues.TextChanged
		RaiseEvent DataPointsTextBoxTextChanged(Me._CorrFactorIndex)
	End Sub

	Public Event cboBaseSelectedIndexChanged(ByVal CorrFactorIndex As Integer)
	Private Sub cboBase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBase.SelectedIndexChanged
		RaiseEvent cboBaseSelectedIndexChanged(Me._CorrFactorIndex)
	End Sub

	Public Event chkWBDBCheckedChanged(ByVal CorrFactorIndex As Integer)
	Private Sub chkWBDB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkWBDB.CheckedChanged
		RaiseEvent chkWBDBCheckedChanged(Me._CorrFactorIndex)
	End Sub

	Public Event rdoChanged(ByVal CorrFactorIndex As Integer)
	Private Sub rdo_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoMultiplier.CheckedChanged, rdoReplacement.CheckedChanged
		RaiseEvent rdoChanged(Me._CorrFactorIndex)
	End Sub

	Private Sub chkCbo_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCbo.MouseEnter

		'make sure all items are visible
		For Each cbi As PresentationControls.CheckBoxComboBoxItem In Me.chkCbo.CheckBoxItems
			cbi.Enabled = True
		Next

		'now, see if base combobox has a current selection
		Dim BaseIndex As Integer = Me.cboBase.SelectedIndex

		'if we don't have anything selected
		If BaseIndex = -1 Then Exit Sub

		'otherwise, remove the selected value, and uncheck it
		Me.chkCbo.CheckBoxItems(BaseIndex).Checked = False
		Me.chkCbo.CheckBoxItems(BaseIndex).Enabled = False

	End Sub

	Public Event chkCboCheckedChanged(ByVal CorrFactorIndex As Integer)
	Private Sub chkCbo_CheckBoxCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCbo.CheckBoxCheckedChanged
		RaiseEvent chkCboCheckedChanged(Me._CorrFactorIndex)
	End Sub

	Public Sub FillCbosWithStringArray(ByVal sArray As String())
		Me.cboBase.Items.Clear()
		For Each s As String In sArray
			Me.cboBase.Items.Add(s)
		Next
		Me.chkCbo.Items.Clear()
		For Each s As String In sArray
			Me.chkCbo.Items.Add(s)
		Next
		If Me.chkCbo.Items(0) = "" Then Me.chkCbo.Items.RemoveAt(0)
	End Sub

	Public Property BaseCboIndex() As Integer
		Get
			Return Me.cboBase.SelectedIndex
		End Get
		Set(ByVal value As Integer)
			Me.cboBase.SelectedIndex = value
		End Set
	End Property

	Public Sub FillChkCboWithBooleans(ByVal bArray As Boolean())
		For I As Integer = 0 To Me.chkCbo.CheckBoxItems.Count - 1
			Me.chkCbo.CheckBoxItems(I).Checked = bArray(I)
		Next
	End Sub

End Class
