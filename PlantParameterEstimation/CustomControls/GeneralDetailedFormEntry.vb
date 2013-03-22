Public Class GeneralDetailedFormEntry

	Private _UnitType As PublicData.UnitType
	''' <summary>
	''' Gets or Sets the unit type for this entry, setting this also resets the entries in the combobox
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Property UnitType() As PublicData.UnitType
		Get
			Return Me._UnitType
		End Get
		Set(ByVal value As PublicData.UnitType)

			Me._UnitType = value
			Me.cboDataUnits.Items.Clear()

			Dim UnitStrings As String() = PublicData.GetUnitStrings(value)

			If value = PublicData.UnitType.NoUnits OrElse UnitStrings Is Nothing Then
				Me.cboDataUnits.Enabled = False
				Return
			End If

			Me.txtDataValue.Enabled = True
			Me.cboDataUnits.Items.AddRange(UnitStrings)

			Dim CalcIndex As Integer = PublicData.GetCalculationUnit(value)
			If CalcIndex <> -1 Then Me.cboDataUnits.SelectedIndex = CalcIndex

		End Set
	End Property

	''' <summary>
	''' The initial data value to show in the textbox
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	Public WriteOnly Property InitialDataValue() As String
		Set(ByVal value As String)
			Me.txtDataValue.Text = value
		End Set
	End Property

	''' <summary>
	''' Tooltip text to show when the user hovers for assistance
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	Public WriteOnly Property HoverTipText() As String
		Set(ByVal value As String)
			Me.ToolTip1.SetToolTip(Me.lblInfoHover, value)
		End Set
	End Property

	''' <summary>
	''' The name of this data entry, such as "Rated Heat Capacity"
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	Public WriteOnly Property DataNameText() As String
		Set(ByVal value As String)
			Me.lblDataName.Text = value
		End Set
	End Property

	Private _NameTag As String
	''' <summary>
	''' A short useful string used to retrieve the data that the user entered
	''' </summary>
	''' <value></value>
	''' <returns></returns>
	''' <remarks></remarks>
	Public ReadOnly Property NameTag() As String
		Get
			Return _NameTag
		End Get
	End Property

	''' <summary>
	''' All-in-one initialization of the control
	''' </summary>
	''' <param name="DataText">The name of this data entry, such as "Rated Heat Capacity"</param>
	''' <param name="NameTag">A short useful string used to retrieve the data that the user entered</param>
	''' <param name="HoverText">Tooltip text to show when the user hovers for assistance</param>
	''' <param name="InitialValue">The initial data value to show in the textbox</param>
	''' <param name="UnitType">The unit type of this entry, such as temperature or flow rate</param>
	''' <remarks></remarks>
	Public Sub New(ByVal DataText As String, ByVal NameTag As String, ByVal HoverText As String, ByVal InitialValue As String, ByVal UnitType As UnitType) ', Optional ByVal ThisDetailedDataType As PublicData.DetailedDataType = PublicData.DetailedDataType.None)

		MyBase.New()
		InitializeComponent()

		Me.DataNameText = DataText
		Me._NameTag = NameTag
		Me.HoverTipText = HoverText
		Me.InitialDataValue = InitialValue

		'If ThisDetailedDataType = PublicData.DetailedDataType.None Then
		'	Me.TableLayoutPanel2.c()
		'End If
		Me.UnitType = UnitType
		'Me.DetailedDataType = ThisDetailedDataType

	End Sub

	'Private _DetailedDataType As PublicData.DetailedDataType
	'Public Property DetailedDataType() As PublicData.DetailedDataType
	'	Get
	'		Return _DetailedDataType
	'	End Get
	'	Set(ByVal value As PublicData.DetailedDataType)

	'		_DetailedDataType = value
	'		If value = PublicData.DetailedDataType.None Then Return

	'		Me.cboDataUnits.Items.Clear()
	'		Dim UnitStrings As String() = PublicData.GetDetailedDataStrings(value)
	'		If UnitStrings IsNot Nothing Then Me.cboDataUnits.Items.AddRange(UnitStrings)

	'		Me.cboDataUnits.SelectedIndex = 0

	'		Me.txtDataValue.Enabled = False
	'		Me.cboDataUnits.Enabled = True

	'	End Set
	'End Property

End Class
