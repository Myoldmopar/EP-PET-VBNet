Friend Class ParametricDataSet

#Region "Properties"
	Private _Name As String
	Property Name() As String
		Get
			Return _Name
		End Get
		Set(ByVal value As String)
			_Name = value
		End Set
	End Property

	Private _BaseColumnIndex As Integer
	Property BaseColumnIndex() As Integer
		Get
			Return _BaseColumnIndex
		End Get
		Set(ByVal value As Integer)
			_BaseColumnIndex = value
		End Set
	End Property

	Private _ColumnsToModify As Boolean()
	Property ColumnsToModify() As Boolean()
		Get
			Return _ColumnsToModify
		End Get
		Set(ByVal value As Boolean())
			_ColumnsToModify = value
		End Set
	End Property

	Private _NumCorrections As Integer
	Property NumCorrections() As Integer
		Get
			Return _NumCorrections
		End Get
		Set(ByVal value As Integer)
			_NumCorrections = value
		End Set
	End Property

	Private _ModificationType As CorrectionFactorType
	Property ModificationType() As CorrectionFactorType
		Get
			Return _ModificationType
		End Get
		Set(ByVal value As CorrectionFactorType)
			_ModificationType = value
		End Set
	End Property

	Private _BaseData As Double()
	Property BaseData() As Double()
		Get
			Return _BaseData
		End Get
		Set(ByVal value As Double())
			_BaseData = value
		End Set
	End Property

	Private _AffectedData As Double(,)
	Property AffectedData() As Double(,)
		Get
			Return _AffectedData
		End Get
		Set(ByVal value As Double(,))
			_AffectedData = value
		End Set
	End Property

	Private _CorrFactorIsaWBDBSet As Boolean
	Property CorrFactorIsaWBDBSet() As Boolean
		Get
			Return _CorrFactorIsaWBDBSet
		End Get
		Set(ByVal value As Boolean)
			_CorrFactorIsaWBDBSet = value
		End Set
	End Property

	Private _CorrFactorDBValue As Double
	Property CorrFactorDBValue() As Double
		Get
			Return _CorrFactorDBValue
		End Get
		Set(ByVal value As Double)
			_CorrFactorDBValue = value
		End Set
	End Property

	Private _isNewOrBlank As Boolean
	Property isNewOrBlank() As Boolean
		Get
			Return _isNewOrBlank
		End Get
		Set(ByVal value As Boolean)
			_isNewOrBlank = value
		End Set
	End Property
#End Region

#Region "Constructor/Methods"

	Friend Sub New()
		isNewOrBlank = True
		Name = Nothing
		BaseColumnIndex = Nothing
		ColumnsToModify = Nothing
		NumCorrections = Nothing
		ModificationType = Nothing
		BaseData = Nothing
		AffectedData = Nothing
		CorrFactorIsaWBDBSet = Nothing
		CorrFactorDBValue = Nothing
		isNewOrBlank = Nothing
	End Sub

	Private Sub NotNewAnymore() Handles Me.LocalDataChanged
		isNewOrBlank = False
	End Sub

#End Region

#Region "Events"

	Private Event LocalDataChanged()

	Friend Event FactorModified()

#End Region

End Class
