Imports Microsoft.Win32
Imports System.Threading
Public Class MainForm

#Region " Variables "

	'data entry forms; instantiated here so they won't get disposed
	Friend myform As CatalogDataWizard
	Friend corrForm As CorrectionFactorForm
	Private WithEvents myCorrForm As CorrectionFactorDataForm
	Friend GenDetailsForm As GeneralDetailedForm = Nothing

	'cross thread classes; instantiated here to allow handling of events
	Friend WithEvents ProgClass As OutputProgress
	Friend WithEvents ThreadedComponentManager As ThreadableInterfaceComponentManager

	'local instantiations of component data structures
	Private DataStruc_WAHPHtg_CurveFit As New HeatPump_WaterAir_Heating_CurveFit.WAHPHeatingData
	Private DataStruc_WAHPHtg_WaterSideCurveFit As New HeatPump_WaterAir_Heating_WaterSideCurveFit.WAHPHeatingData
	Private DataStruc_WAHPHtg_ParamEst As New HeatPump_WaterAir_Heating_ParameterEstimation.WAHPHeatingData
	Private DataStruc_WAHPClg_CurveFit As New HeatPump_WaterAir_Cooling_CurveFit.WAHPCoolingData
	Private DataStruc_WWHPHtg_CurveFit As New HeatPump_WaterWater_Heating_CurveFit.WWHPHeatingData
	Private DataStruc_WWHPClg_CurveFit As New HeatPump_WaterWater_Cooling_CurveFit.WWHPCoolingData
	Private DataStruc_ConstPump_NonDimensional As New Pumps_ConstantSpeed_NonDimensional.PumpDataStructure
	'COMPONENT_EXTENSION: 10. Add data structure instances here

	Private Const spacing As Integer = 20

	'this is a full general 2D array of catalog data
	Dim FullDataSet As Single(,) = Nothing			'Full data set after correction factors (if any) are applied

	'this is the "default" tree node, used to re-init the form
	Dim tnInit As TreeNode

#End Region

#Region " Cross Thread Progress Triggers and Handlers "

	Private Sub InitializeProgressBarTrigger(ByVal maximum As Integer) Handles ThreadedComponentManager.ProgressEvent_Initialize, ProgClass.InitProgress
		Me.BeginInvoke(New Action(Of Integer)(AddressOf InitializeProgressBarHandler), maximum)
	End Sub
	Private Sub InitializeProgressBarHandler(ByVal maximum As Integer)
		With Me.prgProgress
			.Value = 0
			.Maximum = maximum
		End With
	End Sub

	Private Sub IncrementProgressBarTrigger() Handles ThreadedComponentManager.ProgressEvent_Increment, ProgClass.UpdateProgress
		Me.BeginInvoke(New action(AddressOf IncrementProgressBarHandler))
	End Sub
	Private Sub IncrementProgressBarHandler()
		Me.prgProgress.PerformStep()
	End Sub

	Private Sub CompleteComponentObjectStringTrigger(ByVal s As String) Handles ThreadedComponentManager.ProgressEvent_Complete
		Me.BeginInvoke(New Action(Of String)(AddressOf CompleteComponentObjectStringHandler), s)
	End Sub
	Private Sub CompleteComponentObjectStringHandler(ByVal s As String)
		Me.txtOutput.Text = s
	End Sub

	Private Sub MakeAPlot(ByVal PlotData As PublicData.PlotData) Handles ThreadedComponentManager.PlotEvent
		Me.BeginInvoke(New Action(Of PublicData.PlotData)(AddressOf MakeAPlotHandler), New Object() {PlotData})
	End Sub
	Private Sub MakeAPlotHandler(ByVal PlotData As PublicData.PlotData)
		Dim plotform As New PlotForm
		Plotting_Operations.MakeAPlot(PlotData.ChartTitle, _
		 PlotData.YLabel, _
		 PlotData.SeriesData, _
		 plotform.PlotSurface2D1)
		plotform.Show()
	End Sub

#End Region

#Region " Initialization Routines "
	''' <summary>
	''' Main Form Initialization Routine
	''' </summary>
	''' <param name="sender">Not sure who sends this...</param>
	''' <param name="e">hmm...</param>
	''' <remarks></remarks>
	Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		'Initialize Properties
		Call InitUTILSECC()
		Call InitREFRIGPR()

		'Initialize main page
		Call BuildTreeView()
		Call Initialize()
		Call ReInitForm()
		treeEquipType.Select()
		Me.SenderDataNote.Text = "  Equipment initialized: Water-Air-HP\HTG\Curve Fit"

		'Set public variable to my instance so that my routines can be accessed as needed
		PublicData.MainFormInstance = Me

	End Sub

	''' <summary>
	''' This routine builds the component tree based on available components
	''' </summary>
	''' <remarks></remarks>
	Private Sub BuildTreeView()
		Dim tn As TreeNode
		Dim tn2 As TreeNode
		Dim tn3 As TreeNode

		tn = New TreeNode("Heat Pumps")
		tn.Tag = New TreeNodeTag(TreeNodeType.Root, SenderType.None)
		Me.treeEquipType.Nodes.Add(tn)

		tn2 = New TreeNode("Water to Air Heating")
		tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		tn.Nodes.Add(tn2)

		tn3 = New TreeNode("Curve Fit")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterAirHeatingCurveFit)
		tn2.Nodes.Add(tn3)
		tnInit = tn3

		tn3 = New TreeNode("Waterside-only Curve Fit")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterAirHeatingWaterSideCurveFit)
		tn2.Nodes.Add(tn3)

		tn3 = New TreeNode("Parameter Estimation")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterAirHeatingParamEst)
		tn2.Nodes.Add(tn3)

		tn2 = New TreeNode("Water to Air Cooling")
		tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		tn.Nodes.Add(tn2)

		tn3 = New TreeNode("Curve Fit")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterAirCoolingCurveFit)
		tn2.Nodes.Add(tn3)

		'tn3 = New TreeNode("Parameter Estimation")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterAirCoolingParameterEstimation)
		'tn2.Nodes.Add(tn3)

		tn2 = New TreeNode("Water to Water Heating")
		tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		tn.Nodes.Add(tn2)

		tn3 = New TreeNode("Curve Fit")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterWaterHeatingCurveFit)
		tn2.Nodes.Add(tn3)

		'tn3 = New TreeNode("Parameter Estimation")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterWaterHeatingParameterEstimation)
		'tn2.Nodes.Add(tn3)

		tn2 = New TreeNode("Water to Water Cooling")
		tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		tn.Nodes.Add(tn2)

		tn3 = New TreeNode("Curve Fit")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterWaterCoolingCurveFit)
		tn2.Nodes.Add(tn3)

		'tn3 = New TreeNode("Parameter Estimation")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPWaterWaterCoolingParameterEstimation)
		'tn2.Nodes.Add(tn3)

		'tn2 = New TreeNode("Air to Air Heating")
		'tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		'tn.Nodes.Add(tn2)

		'tn3 = New TreeNode("Curve Fit")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPAirAirHeatingCurveFit)
		'tn2.Nodes.Add(tn3)

		'tn3 = New TreeNode("Parameter Estimation")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPAirAirHeatingParameterEstimation)
		'tn2.Nodes.Add(tn3)

		'tn2 = New TreeNode("Air to Air Cooling")
		'tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		'tn.Nodes.Add(tn2)

		'tn3 = New TreeNode("Curve Fit")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPAirAirCoolingCurveFit)
		'tn2.Nodes.Add(tn3)

		'tn3 = New TreeNode("Parameter Estimation")
		'tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.HPAirAirCoolingParameterEstimation)
		'tn2.Nodes.Add(tn3)

		tn = New TreeNode("Pumps")
		tn.Tag = New TreeNodeTag(TreeNodeType.Root, SenderType.None)
		Me.treeEquipType.Nodes.Add(tn)

		tn2 = New TreeNode("Constant Speed Pump")
		tn2.Tag = New TreeNodeTag(TreeNodeType.Type, SenderType.None)
		tn.Nodes.Add(tn2)
		tn.Expand()

		tn3 = New TreeNode("Non-Dimensional")
		tn3.Tag = New TreeNodeTag(TreeNodeType.EquipSelection, SenderType.Pump_ConstantSpeed_NonDimensional)
		tn2.Nodes.Add(tn3)
		
		'COMPONENT_EXTENSION: 08. Add more nodes to the treeview based on the type added

		Me.treeEquipType.SelectedNode = tnInit
		'tnInit.Parent.expand

	End Sub

	''' <summary>
	''' Custom initialization routine
	''' </summary>
	''' <remarks>Handles error provider inits and radio button click handler inits</remarks>
	Private Sub Initialize()

		'Set up event handlers
		For Each ctl As Control In Me.grpOutputType.Controls
			Dim rdo As RadioButton = TryCast(ctl, RadioButton)
			If rdo IsNot Nothing Then AddHandler rdo.CheckedChanged, AddressOf ClearRadioErrorProviders
		Next

		'Set up error providers
		CatalogErrorProvider = New System.Windows.Forms.ErrorProvider
		With CatalogErrorProvider
			.SetIconAlignment(Me.btnCatalog, ErrorIconAlignment.MiddleRight)
			.SetIconPadding(Me.btnCatalog, 2)
			.BlinkRate = 1000
			.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink
		End With
		OutputErrorProvider = New System.Windows.Forms.ErrorProvider
		With OutputErrorProvider
			.SetIconAlignment(Me.grpOutputType, ErrorIconAlignment.MiddleRight)
			.SetIconPadding(Me.grpOutputType, 2)
			.BlinkRate = 1000
			.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink
		End With

		'tooltips? could be expanded for more tips
		Me.ToolTip.SetToolTip(Me.txtOutput, "This is where output will be placed.  Use Ctrl-A to select all, then Ctrl-C to copy to clipboard.")

	End Sub

	''' <summary>
	''' This subroutine will reset the main form
	''' </summary>
	''' <remarks>This routine resets which equipment is selected, 
	''' Resets the 'data-in-place' flags,
	''' and clears any error providers</remarks>
	Private Sub ReInitForm()

		'changing this radio button should re-initialize 'data-in-place' flags
		treeEquipType.CollapseAll()
		treeEquipType.SelectedNode = tnInit
		tnInit.EnsureVisible()
		Call btnEngage_Click(Me, New EventArgs)

		'but we'll do this anyway for good measure
		PublicData.CatalogDataInPlace = False
		PublicData.CorrectionDataInPlace = False

		'and we'll also clear any errors
		Call ClearRadioErrorProviders(Me, New EventArgs)

	End Sub

	''' <summary>
	''' This subroutine resets any radio providers
	''' </summary>
	''' <param name="sender">Form 1</param>
	''' <param name="e">Event arguments</param>
	''' <remarks></remarks>
	Private Sub ClearRadioErrorProviders(ByVal sender As Object, ByVal e As System.EventArgs)
		'Clear the errors by setting their error message to ""
		OutputErrorProvider.SetError(Me.grpOutputType, "")
		'Also reset the output text and progress bar while here
		Me.txtOutput.Clear()
		Me.prgProgress.Value = 0
	End Sub
#End Region

#Region " Tree View Handling "
	Private Sub treeEquipType_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeEquipType.AfterSelect
		Dim tn As TreeNode = treeEquipType.SelectedNode
		Dim tag As TreeNodeTag = tn.Tag
		Select Case tag.Type
			Case TreeNodeType.EquipSelection
				btnEngage.Enabled = True
				If tag.Sender <> PublicData.Sender Then
					btnEngage.BackColor = Color.Orange
					Me.ToolTip.SetToolTip(btnEngage, "Click me to engage the selection of this component")
				Else
					btnEngage.BackColor = Color.FromKnownColor(KnownColor.Control)
					Me.ToolTip.SetToolTip(btnEngage, "This component is already engaged, no further action required")
				End If
			Case Else
				btnEngage.Enabled = False
				btnEngage.BackColor = Color.FromKnownColor(KnownColor.Control)
		End Select
	End Sub

	Private Sub btnEngage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEngage.Click

        'we should probably check first before we overwrite anything already entered
        If PublicData.CatalogDataInPlace Then
            If ShowOKCancelBox("This will reset the form and begin operating on a new data entry task, clearing out any data you have entered.") = MsgBoxResult.Cancel Then
                Return
            End If
        End If

		PublicData.CatalogDataInPlace = False

		'the following is needed to make sure we use the right details form
		'this could be enhanced to be more like the correction factor form/catalog form
		'in which case, a public variable would represent whether data is present or not
		Me.GenDetailsForm = Nothing

		Me.btnData.Enabled = False

		Dim tn As TreeNode = treeEquipType.SelectedNode
		Dim tag As TreeNodeTag = tn.Tag
		Select Case tag.Type
			Case TreeNodeType.Root, TreeNodeType.Type
				'reset the sender
				PublicData.Sender = SenderType.None
			Case TreeNodeType.EquipSelection
				'set which one
				PublicData.Sender = tag.Sender
		End Select

		btnEngage.BackColor = Color.FromKnownColor(KnownColor.Control)

	End Sub
#End Region

#Region " Data Wizard Routines "
	''' <summary>
	''' The catalog data wizard subroutine
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	''' <remarks>Manages the form operation of the catalog data wizard:
	''' Clears Errors, Instantiate data wizard form, Set public sender
	''' Calls Correction factor management,
	''' Inputs Tabulated Data sets,
	''' Calls Detailed equipment management,
	''' Calls Correction factor simulation,
	''' Simulates Correction factors,
	''' Creates full data set array</remarks>
	Private Sub btnCatalog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatalog.Click

		If PublicData.Sender = SenderType.HPWaterAirHeatingParamEst Then
			ShowWarningError("The parameter estimation model is still under development and currently not operational.")
			Return
		End If

		'reset error providers and clear them, assume the catalog will be successful for now
		Call ClearRadioErrorProviders(sender, e)
		CatalogErrorProvider.SetError(Me.btnCatalog, "")

		're-instantiate if needed
		If Not PublicData.CatalogDataInPlace Then myform = New CatalogDataWizard

		'manage the correction factors, first call ManageCorrectionFactors to get data, if any
		Dim Cancel As Boolean = False
		Dim CorrectionsExist As Boolean = False
		Call ManageCorrectionFactors(Cancel, CorrectionsExist)
		If Cancel Then Exit Sub

		'if there are correction factors, then analyze the data
		Dim ConstantColumns() As Boolean = Nothing
		If CorrectionsExist Then
			PublicData.CorrectionDataInPlace = True
			Call AnalyzeColumnList(ConstantColumns)
			myform.ConstantColumns = ConstantColumns
		Else
			myform.ConstantColumns = Nothing
		End If

		'show catalog data wizard
		If myform.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

		'now call for the detailed component data for each individual equipment type
		Call ManageDetailedComponentData(Cancel)
		If Cancel Then Exit Sub

		'if we made it this far then the catalog data is successfully in place
		PublicData.CatalogDataInPlace = True

		'now that we have catalog data, detailed component data, and possibly correction factor data, 
		'we just need to build the large data array
		'start by converting the tabulated catalog data into a temporary array
		Dim ArrayParameters As Integer
		Dim ArrayEntries As Integer
		Dim TempArray(,) As Single = Nothing
		Try
			'convert the table to values
			ArrayParameters = myform.DataGridView1.ColumnCount - 1
			ArrayEntries = myform.DataGridView1.RowCount - 1
			ReDim TempArray(ArrayParameters, ArrayEntries)
			For I As Integer = 0 To ArrayParameters
				For J As Integer = 0 To ArrayEntries
					Dim TempVar As String = myform.DataGridView1.Item(I, J).Value
					TempArray(I, J) = CType(TempVar, Double)
				Next
			Next
		Catch xc As Exception
			Call PublicData.ShowSevereError("There was a problem getting the simulation parameters." & vbCrLf & _
			  "This can occur if an alpha entry is entered where a number is expected." & vbCrLf & _
			  "Ensure that the catalog data contains only numeric values." & vbCrLf & _
			  "Source of problem is in the tabulated data window.")
			Exit Sub
		End Try

		'If successful, then simulate the correction factors (if any)
		Try
			If PublicData.CorrectionDataInPlace Then 'PublicData.FullCorrectionFactorData IsNot Nothing Then
				Dim ErrorsFound As Boolean = False
				Call SimulateCorrectionFactors(TempArray, FullDataSet, ErrorsFound)
				'If ErrorsFound then Throw New Exception()  MAYBE? 
			Else
				FullDataSet = TempArray
			End If
		Catch xc As Exception
			Call PublicData.ShowSevereError("There was a problem applying the correction factors." & vbCrLf & _
			  "This can occur for many reasons, including non-numeric entries." & vbCrLf & _
			  "Ensure that numeric entries are entered and try again.")
			Exit Sub
		End Try

		'now we will check to make sure that every column has at least two different values
		Dim FoundUniformColumn As Boolean = False
		Dim UniformColumns(FullDataSet.GetUpperBound(0)) As Boolean
		For ColumnCounter As Integer = 0 To FullDataSet.GetUpperBound(0)
			'for each column, sum all the values and find the average
			Dim Sum As Double = 0
			For EntryCounter As Integer = 0 To FullDataSet.GetUpperBound(1)
				Sum += FullDataSet(ColumnCounter, EntryCounter)
			Next
			Dim Avg As Double = Sum / (FullDataSet.GetUpperBound(1) + 1)
			'now go back and see if at least one value differs from the average
			For EntryCounter As Integer = 0 To FullDataSet.GetUpperBound(1)
				If FullDataSet(ColumnCounter, EntryCounter) <> Avg Then Exit For
				If EntryCounter = FullDataSet.GetUpperBound(1) Then	'we've made it through all values without a diff
					UniformColumns(ColumnCounter) = True
					If Not FoundUniformColumn Then FoundUniformColumn = True
				End If
			Next
		Next
		If FoundUniformColumn Then
			Dim myStr As String = ""
			For ColumnCounter As Integer = 0 To FullDataSet.GetUpperBound(0)
				If UniformColumns(ColumnCounter) Then
					myStr &= (ColumnCounter + 1).ToString & ", "
				End If
			Next
			myStr = myStr.Substring(0, myStr.Length - 2)
			Call ShowWarningError("It appears that even after any existing correction factors were applied, at least one column of data" & vbCrLf & _
			 "still contains only a single value.  This can lead to odd results for curve fits when a" & vbCrLf & _
			 "variable doesn't actually vary.  This was encountered at column(s) #(" & myStr & ")")
		End If

		'If successful again, then assign the array to the proper component
		Select Case PublicData.Sender
			Case SenderType.HPWaterAirHeatingCurveFit
				DataStruc_WAHPHtg_CurveFit.CatalogDataArray = FullDataSet
			Case SenderType.HPWaterAirHeatingWaterSideCurveFit
				DataStruc_WAHPHtg_WaterSideCurveFit.CatalogDataArray = FullDataSet
			Case SenderType.HPWaterAirHeatingParamEst
				DataStruc_WAHPHtg_ParamEst.CatalogDataArray = FullDataSet
			Case SenderType.HPWaterAirCoolingCurveFit
				DataStruc_WAHPClg_CurveFit.CatalogDataArray = FullDataSet
			Case SenderType.HPWaterWaterHeatingCurveFit
				DataStruc_WWHPHtg_CurveFit.CatalogDataArray = FullDataSet
			Case SenderType.HPWaterWaterCoolingCurveFit
				DataStruc_WWHPClg_CurveFit.CatalogDataArray = FullDataSet
			Case SenderType.Pump_ConstantSpeed_NonDimensional
				DataStruc_ConstPump_NonDimensional.CatalogDataArray = FullDataSet
			Case Else
				'COMPONENT_EXTENSION: 11. Pass array of data to each component class structure here
		End Select

		Using MyCatalogDataPlotForm As New CatalogDataPlotForm
			Dim ReportWriter As New System.Text.StringBuilder
			ReportWriter.AppendLine("The data has been processed successfully.")
			If Not FoundUniformColumn Then ReportWriter.AppendLine("Diversity has been detected in all of the independent variables.")
			If CorrectionDataInPlace Then ReportWriter.AppendLine("The resulting data set has had correction factors applied to it.")
			ReportWriter.AppendLine("The array is now filled with " & FullDataSet.GetLength(1) & " data points.")
			With MyCatalogDataPlotForm
				.MakePlots(FullDataSet)
				.lblInfo.Text = ReportWriter.ToString
				.ShowDialog()
			End With
		End Using

		'enable the 'output data' button to allow the user to copy the full corrected data set
		Me.btnData.Enabled = True

	End Sub

	''' <summary>
	''' Governs the input of Correction Factor data
	''' </summary>
	''' <param name="CancelWizard">Signal to Catalog wizard to cancel if error/cancel induced</param>
	''' <param name="CorrectionsExist">Signal to Catalog wizard to simulate available factors</param>
	''' <remarks>Instantiates Correction Form,
	''' If corrections exist, Calls detailed correction input routine</remarks>
	Private Sub ManageCorrectionFactors(ByRef CancelWizard As Boolean, ByRef CorrectionsExist As Boolean)

		're-instantiate form if needed
		If Not PublicData.CorrectionDataInPlace Then corrForm = New CorrectionFactorForm

		Dim DR As DialogResult = corrForm.ShowDialog
		If DR = Windows.Forms.DialogResult.Cancel Then
			CancelWizard = True
			CorrectionsExist = False
			PublicData.CorrectionDataInPlace = False
		Else
			'make decisions based on signal 
			Select Case PublicData.CorrectionFormReturnType
				Case CorrectionFactorReturnType._Cancel
					CancelWizard = True
					CorrectionsExist = False
					PublicData.CorrectionDataInPlace = False
				Case CorrectionFactorReturnType._Done
					CancelWizard = False
					CorrectionsExist = True
					'PublicData.CorrectionDataInPlace = True 'can't say this yet, if the corrections are already in place
					'and the user simply accepts them, the datainplace will stay true, so the detailed data form will be filled
				Case CorrectionFactorReturnType._Skip
					CancelWizard = False
					CorrectionsExist = False
					PublicData.CorrectionDataInPlace = False 'True
				Case CorrectionFactorReturnType._Error
					CancelWizard = True
					CorrectionsExist = False
					PublicData.CorrectionDataInPlace = False
			End Select
		End If

		If CorrectionsExist Then
			Call GetDetailedCorrectionData(CancelWizard)
		End If

	End Sub

	''' <summary>
	''' Gets the detailed data for each correction factor
	''' </summary>
	''' <param name="Cancel">Signal to caller that an error or cancel has been induced</param>
	''' <remarks>Instantiates form for each factor, filling data when available</remarks>
	Private Sub GetDetailedCorrectionData(ByRef Cancel As Boolean)

		Dim MyArray As String() = PublicData.GetHeaderStrings
		Dim MyUnits As Integer() = PublicData.GetHeaderUnits
		If (MyArray Is Nothing) Or (MyUnits Is Nothing) Then
			Call ShowSevereError("An erroneous call was made to set up catalog data headers.  Ensure a proper equipment selection was made and re-try.")
			PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Error
			Exit Sub
		End If

		'now that we have the correction factor summary, we'll need to get individual data for each
		For FactorCounter As Integer = 0 To PublicData.FullCorrectionFactorData.Count - 1

			myCorrForm = New CorrectionFactorDataForm
			myCorrForm.Text &= " " & (FactorCounter + 1).ToString & "/" & PublicData.FullCorrectionFactorData.Count

			Dim MultRepl As String = "multiplier"
			If PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Replacer Then MultRepl = "replacement"

			Dim NumAffCols As Integer = 0
			Dim ColNames As String() = Nothing
			Dim ColIndex As Integer = -1
			Dim ColString As String = ""
			For ColCounter As Integer = 0 To PublicData.FullCorrectionFactorData(FactorCounter).ColumnsToModify.GetUpperBound(0)
				If PublicData.FullCorrectionFactorData(FactorCounter).ColumnsToModify(ColCounter) Then
					NumAffCols += 1
					ColIndex += 1
					ReDim Preserve ColNames(ColIndex)
					ColNames(ColIndex) = MyArray(ColCounter)
					ColString &= "   -- " & ColNames(ColIndex) & vbCrLf
				End If
			Next

			Dim WBDBString As String = ""
			If PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorIsaWBDBSet Then
				WBDBString &= "In addition, this correction factor is a wet-bulb/dry-bulb correction set," & vbCrLf & _
				   "so the current value of DB must be entered as well.  The program assumes that" & vbCrLf & _
				   "this is in the same units as the wet bulb temperature, which is entered in the table."
				myCorrForm.txtDB.Enabled = True
				myCorrForm.cboDB.Enabled = True
				If (PublicData.CorrectionDataInPlace) AndAlso (PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorDBValue <> Nothing) Then
					myCorrForm.txtDB.Text = PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorDBValue
				End If
			End If

			Dim ConstString As String = ""
			If PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Multiplier Then
				ConstString &= vbCrLf & "For multiplier values, a constant value is given later with the tabular data," & vbCrLf & _
				 "and multipliers are entered in the table" & vbCrLf
			ElseIf PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Replacer Then
				ConstString &= vbCrLf & "For replacer values, a constant value is given later with the tabular data, " & vbCrLf & _
				 "and replacement data and corresponding units are entered in the table and drop down box." & vbCrLf & _
				 "The units of the replacement values must be conformed to program standards before exiting this window."
				myCorrForm.cboConstValue.Enabled = True
			End If

			Dim MyUnitStrings() As String = Nothing
			Dim MyCalcUnit As Integer
			With myCorrForm
				'set up main label
				.Label1.Text = "Entering correction factor data for:  -- " & PublicData.FullCorrectionFactorData(FactorCounter).Name & " --" & vbCrLf & _
				"This correction factor requires inputting -- " & PublicData.FullCorrectionFactorData(FactorCounter).NumCorrections & " -- " & MultRepl & " -- values for -- " & MyArray(PublicData.FullCorrectionFactorData(FactorCounter).BaseColumnIndex) & " --" & vbCrLf & _
				ConstString & vbCrLf & _
				"In addition, multipliers must be given for the following " & NumAffCols & " column(s):" & vbCrLf & _
				ColString & vbCrLf & _
				WBDBString

				'set up constant value combobox for replacer data
				If PublicData.FullCorrectionFactorData(FactorCounter).ModificationType = CorrectionFactorType.Replacer Then
					Dim UnitType As Integer = MyUnits(PublicData.FullCorrectionFactorData(FactorCounter).BaseColumnIndex)
					MyUnitStrings = PublicData.GetUnitStrings(UnitType)
					MyCalcUnit = PublicData.GetCalculationUnit(UnitType)
					If MyUnitStrings Is Nothing Then
						.cboConstValue.Enabled = False
						MyCalcUnit = 0
					End If
					If .cboConstValue.Enabled Then
						For UnitCounter As Integer = 0 To MyUnitStrings.GetUpperBound(0)
							.cboConstValue.Items.Add(MyUnitStrings(UnitCounter))
						Next
						.cboConstValue.SelectedIndex = MyCalcUnit
						.cboConstValue.Tag = UnitType
					End If
				End If

				'set up dry bulb units box if needed
				If PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorIsaWBDBSet Then
					For UnitCounter As Integer = 0 To PublicData.TempUnitStrings.GetUpperBound(0)
						.cboDB.Items.Add(PublicData.TempUnitStrings(UnitCounter))
					Next
					.cboDB.SelectedIndex = PublicData.TempUnits.CalculationUnit
					.cboDB.Tag = PublicData.UnitType.TempUnits
				End If

				'set up data grid view
				If ColNames IsNot Nothing Then
					'add base column first
					.DataGridView1.Columns.Add("BaseColumn", MyArray(PublicData.FullCorrectionFactorData(FactorCounter).BaseColumnIndex))
					.DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.LightGray

					'then all the other columns (affected data)
					For colCounter As Integer = 0 To ColNames.GetUpperBound(0)
						.DataGridView1.Columns.Add("Column" & colCounter, ColNames(colCounter))
					Next

					'then add number of rows
					.DataGridView1.Rows.Add(PublicData.FullCorrectionFactorData(FactorCounter).NumCorrections)
					For rowCounter As Integer = 0 To .DataGridView1.Rows.Count - 1
						.DataGridView1.Rows(rowCounter).HeaderCell.Value = rowCounter + 1
					Next
					.DataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders)
				End If

				'make sure data grid view and other controls and down far enough
				If .Label1.Bottom > .Label3.Top Then
					Dim DropDistance As Integer = .Label1.Bottom - .Label3.Top + spacing
					.cboConstValue.Top += DropDistance
					.Label3.Top += DropDistance
					.cboDB.Top += DropDistance
					.Label4.Top += DropDistance
					.DataGridView1.Top += DropDistance
					.Button1.Top += DropDistance
					.Button2.Top += DropDistance
					.chkAddRows.Top += DropDistance
					.txtDB.Top += DropDistance
				End If

				'also need to update tab indeces to make it easy to enter data
				Call UpdateTabIndeces()

				'also need to possibly refill the data
				If (PublicData.CorrectionDataInPlace) AndAlso _
				(PublicData.FullCorrectionFactorData(FactorCounter).BaseData IsNot Nothing) AndAlso _
				(PublicData.FullCorrectionFactorData(FactorCounter).AffectedData IsNot Nothing) Then
					For colCounter As Integer = 0 To .DataGridView1.ColumnCount - 1
						For rowCounter As Integer = 0 To .DataGridView1.Rows.Count - 1
							If colCounter = 0 Then
								.DataGridView1.Item(colCounter, rowCounter).Value = PublicData.FullCorrectionFactorData(FactorCounter).BaseData(rowCounter)
							Else
								.DataGridView1.Item(colCounter, rowCounter).Value = PublicData.FullCorrectionFactorData(FactorCounter).AffectedData(colCounter - 1, rowCounter)
							End If
						Next
					Next
				End If

				'show the dialog
				Dim DR As DialogResult = .ShowDialog
				If DR = Windows.Forms.DialogResult.Cancel Then
					PublicData.CorrectionFormReturnType = PublicData.CorrectionFactorReturnType._Cancel
					Cancel = True
					Exit Sub
				End If

				'data is validated in the data form itself
				'try-block is redundant, but doesn't hurt anything
				Try
					ReDim PublicData.FullCorrectionFactorData(FactorCounter).BaseData(.DataGridView1.Rows.Count - 1)
					ReDim PublicData.FullCorrectionFactorData(FactorCounter).AffectedData(.DataGridView1.ColumnCount - 2, .DataGridView1.Rows.Count - 1)
					For colCounter As Integer = 0 To .DataGridView1.ColumnCount - 1
						For rowCounter As Integer = 0 To .DataGridView1.Rows.Count - 1
							If colCounter = 0 Then
								PublicData.FullCorrectionFactorData(FactorCounter).BaseData(rowCounter) = CSng(.DataGridView1.Item(colCounter, rowCounter).Value)
							Else
								PublicData.FullCorrectionFactorData(FactorCounter).AffectedData(colCounter - 1, rowCounter) = CSng(.DataGridView1.Item(colCounter, rowCounter).Value)
							End If
						Next
					Next
					If .txtDB.Enabled Then PublicData.FullCorrectionFactorData(FactorCounter).CorrFactorDBValue = .txtDB.Text 'apply dry bulb to data structure
				Catch

				End Try
			End With

		Next
	End Sub

	''' <summary>
	''' Sets the tab index appropriately for each control that is visible on the detailed corr data form
	''' </summary>
	''' <remarks></remarks>
	Private Sub UpdateTabIndeces()
		Dim CorrFormControls As New Collection
		CorrFormControls.Add(myCorrForm.txtDB)
		CorrFormControls.Add(myCorrForm.cboDB)
		CorrFormControls.Add(myCorrForm.cboConstValue)
		CorrFormControls.Add(myCorrForm.DataGridView1)
		CorrFormControls.Add(myCorrForm.Button1)
		CorrFormControls.Add(myCorrForm.Button2)
		CorrFormControls.Add(myCorrForm.chkAddRows)
		Dim TabIndexCounter As Integer = -1
		For Each ctl As Control In CorrFormControls
			If ctl.Enabled Then
				TabIndexCounter += 1
				ctl.TabIndex = TabIndexCounter
			End If
		Next
	End Sub

	''' <summary>
	''' Analyzes the correction factor to find columns to correct
	''' </summary>
	''' <param name="ColumnsOfConstantValues">Resulting list of booleans, true if the column is to be corrected</param>
	''' <remarks>If the column is to be corrected, it should be marked differently in the catalog wizard</remarks>
	Private Sub AnalyzeColumnList(ByRef ColumnsOfConstantValues As Boolean())

		'get appropriate array so we know how many columns we'll have
		Dim MyArray As String() = PublicData.GetHeaderStrings
		Dim MyDBValue As Integer = PublicData.GetDBValue
		If (MyArray Is Nothing) Or (MyDBValue = -2) Then
			Call PublicData.ShowSevereError("This equipment type has not yet been set up, cancelling...")
			Exit Sub
		End If

		'assume they are all enabled
		ReDim ColumnsOfConstantValues(MyArray.GetUpperBound(0))
		For I As Integer = 0 To MyArray.GetUpperBound(0) : ColumnsOfConstantValues(I) = False : Next

		'then disable them one by one
		Dim NumCorrectors As Integer = PublicData.FullCorrectionFactorData.GetUpperBound(0)	'zero-based
		For I As Integer = 0 To NumCorrectors
			ColumnsOfConstantValues(PublicData.FullCorrectionFactorData(I).BaseColumnIndex) = True
			If PublicData.FullCorrectionFactorData(I).CorrFactorIsaWBDBSet Then ColumnsOfConstantValues(MyDBValue) = True 'shouldn't need a handler
		Next

	End Sub

	''' <summary>
	''' Inputs data for each particular component such as rated capacities
	''' </summary>
	''' <param name="CancelMe">Signal to caller that process should be cancelled</param>
	''' <remarks>Inputs curve fit vs. Parameter Estimation, Then inputs data based on current sender</remarks>
	Private Sub ManageDetailedComponentData(ByRef CancelMe As Boolean)

		'now we need to request the details
		If GenDetailsForm Is Nothing Then GenDetailsForm = New GeneralDetailedForm
		With GenDetailsForm
			If .EntryList.Count = 0 Then 'we should fill it
				Select Case PublicData.Sender
					Case SenderType.HPWaterAirHeatingCurveFit
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Air Volume Flow Rate", "RatedAirVolFlow", "This is a nominal flow rate value from manufacturer's data", "0.1887", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Water Volume Flow Rate", "RatedWaterVolFlow", "This is a nominal flow rate value from manufacturer's data", "0.0001892", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Total Heating Capacity", "RatedHeatCapacity", "This is a nominal value of the load-side heating capacity for this heat pump", "3.513", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Compressor Power Use", "RatedCompPower", "This is a nominal value of the compressor power for this heat pump", "0.900", UnitType.PowerUnits))
					Case SenderType.HPWaterAirHeatingWaterSideCurveFit
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Water Volume Flow Rate", "RatedWaterVolFlow", "This is a nominal flow rate value from manufacturer's data", "0.0001892", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Total Heating Capacity", "RatedHeatCapacity", "This is a nominal value of the load-side heating capacity for this heat pump", "3.513", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Compressor Power Use", "RatedCompPower", "This is a nominal value of the compressor power for this heat pump", "0.900", UnitType.PowerUnits))
						'Case SenderType.HPWaterAirHeatingParamEst
						'	.EntryList.Add(New GeneralDetailedFormEntry("Compressor Type", "CompType", "Select the type of compressor in this equipment from the drop down list", "", UnitType.NoUnits, DetailedDataType.CompressorType))
						'	.EntryList.Add(New GeneralDetailedFormEntry("Source Fluid Type", "FluidType", "Choose the working fluid for the source side of this equipment", "", UnitType.NoUnits, DetailedDataType.FluidType))
						'	.EntryList.Add(New GeneralDetailedFormEntry("Refrigerant Type", "RefrigType", "Choose the refrigerant type for this equipment", "", UnitType.NoUnits, DetailedDataType.RefrigerantType))
						'	.EntryList.Add(New GeneralDetailedFormEntry("Optimization Accuracy", "Accuracy", "Specify the accuracy for the optimization routine, the lower value, the longer the simulation", "0.0001", UnitType.NoUnits))
					Case SenderType.HPWaterAirCoolingCurveFit
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Air Volume Flow Rate", "RatedAirVolFlow", "This is a nominal flow rate value from manufacturer's data", "0.2171", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Water Volume Flow Rate", "RatedWaterVolFlow", "This is a nominal flow rate value from manufacturer's data", "0.00023", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Total Cooling Capacity", "RatedCoolCapacity", "This is a nominal value of the total load-side cooling capacity for this heat pump", "4.689", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Sensible Cooling Capacity", "RatedSensibleCap", "This is a nominal value of the sensible load-side cooling capacity for this heat pump", "3.517", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Compressor Power Use", "RatedCompPower", "This is a nominal value of the compressor power for this heat pump", "1.25", UnitType.PowerUnits))
					Case SenderType.HPWaterWaterHeatingCurveFit
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Load Side Flow Rate", "RatedLoadFlow", "This is a nominal flow rate value for the load-side of the heat pump", "0.0006887", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Source Side Flow Rate", "RatedSourceFlow", "This is a nominal flow rate value for the source-side of the heat pump", "0.0001892", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Total Heating Capacity", "RatedCapacity", "This is a nominal value of the load-side heating capacity of the heat pump", "3.513", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Compressor Power Use", "RatedCompPower", "This is a nominal value of the compressor power for this heat pump", "0.900", UnitType.PowerUnits))
					Case SenderType.HPWaterWaterCoolingCurveFit
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Load Side Flow Rate", "RatedLoadFlow", "This is a nominal flow rate value for the load-side of the heat pump", "0.0006887", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Source Side Flow Rate", "RatedSourceFlow", "This is a nominal flow rate value for the source-side of the heat pump", "0.0001892", UnitType.FlowUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Total Cooling Capacity", "RatedCapacity", "This is a nominal value of the load-side cooling capacity of the heat pump", "3.513", UnitType.PowerUnits))
						.EntryList.Add(New GeneralDetailedFormEntry("Rated Compressor Power Use", "RatedCompPower", "This is a nominal value of the compressor power for this heat pump", "0.900", UnitType.PowerUnits))
					Case SenderType.Pump_ConstantSpeed_NonDimensional
						.EntryList.Add(New GeneralDetailedFormEntry("Impeller Diameter", "ImpellerDiameter", "This is the impeller diameter used to non-dimensionalize the curve", "0.10795", UnitType.Length))
						.EntryList.Add(New GeneralDetailedFormEntry("Impeller Rotational Speed", "RotationalSpeed", "This is the impeller rotational speed used to non-dimensionalize the curve", "29.167", UnitType.RotationalSpeed))
						.EntryList.Add(New GeneralDetailedFormEntry("Fluid (H2O) Temperature", "FluidTemp", "This is an operating fluid temperature, used to calculate density", "25.556", UnitType.TempUnits))
					Case Else
						'COMPONENT_EXTENSION: 12. Generate a new detailed data form here
				End Select
			Else
				'we shouldn't fill it, because it already had items in it, user probably re-opened form
			End If

			'display the form and wait for the user to be odne with data entry	
			If .ShowDialog = Windows.Forms.DialogResult.OK Then
				PublicData.CatalogDataInPlace = True
			Else
				CancelMe = True
				Exit Sub
			End If

			'at this point the data should be validated by the form itself, and a cancel operation would already have been accounted for
			'so we will just retrieve the data from the dictionary using the same nametags we used earlier, CaSe matters here!
			Select Case PublicData.Sender
				Case SenderType.HPWaterAirHeatingCurveFit
					DataStruc_WAHPHtg_CurveFit.RatedAirVolFlowRate = .NameTagValueLookup("RatedAirVolFlow")
					DataStruc_WAHPHtg_CurveFit.RatedWaterVolFlowRate = .NameTagValueLookup("RatedWaterVolFlow")
					DataStruc_WAHPHtg_CurveFit.RatedTotalCap = .NameTagValueLookup("RatedHeatCapacity") * 1000
					DataStruc_WAHPHtg_CurveFit.RatedPower = .NameTagValueLookup("RatedCompPower") * 1000
				Case SenderType.HPWaterAirHeatingWaterSideCurveFit
					DataStruc_WAHPHtg_WaterSideCurveFit.RatedWaterVolFlowRate = .NameTagValueLookup("RatedWaterVolFlow")
					DataStruc_WAHPHtg_WaterSideCurveFit.RatedTotalCap = .NameTagValueLookup("RatedHeatCapacity") * 1000
					DataStruc_WAHPHtg_WaterSideCurveFit.RatedPower = .NameTagValueLookup("RatedCompPower") * 1000
				Case SenderType.HPWaterAirHeatingParamEst
					DataStruc_WAHPHtg_ParamEst.CompressorType = .NameTagValueLookup("CompType")
					DataStruc_WAHPHtg_ParamEst.FluidType = .NameTagValueLookup("FluidType")
					DataStruc_WAHPHtg_ParamEst.Refrigerant = .NameTagValueLookup("RefrigType")
					DataStruc_WAHPHtg_ParamEst.Accuracy = .NameTagValueLookup("Accuracy")
				Case SenderType.HPWaterAirCoolingCurveFit
					DataStruc_WAHPClg_CurveFit.RatedAirVolFlowRate = .NameTagValueLookup("RatedAirVolFlow")	'
					DataStruc_WAHPClg_CurveFit.RatedWaterVolFlowRate = .NameTagValueLookup("RatedWaterVolFlow")
					DataStruc_WAHPClg_CurveFit.RatedTotalCap = .NameTagValueLookup("RatedCoolCapacity") * 1000
					DataStruc_WAHPClg_CurveFit.RatedSensibleCap = .NameTagValueLookup("RatedSensibleCap") * 1000
					DataStruc_WAHPClg_CurveFit.RatedPower = .NameTagValueLookup("RatedCompPower") * 1000
				Case SenderType.HPWaterWaterHeatingCurveFit
					DataStruc_WWHPHtg_CurveFit.RatedLoadVolFlowRate = .NameTagValueLookup("RatedLoadFlow")
					DataStruc_WWHPHtg_CurveFit.RatedSourceVolFlowRate = .NameTagValueLookup("RatedSourceFlow")
					DataStruc_WWHPHtg_CurveFit.RatedTotalCapacity = .NameTagValueLookup("RatedCapacity") * 1000
					DataStruc_WWHPHtg_CurveFit.RatedCompressorPower = .NameTagValueLookup("RatedCompPower") * 1000
				Case SenderType.HPWaterWaterCoolingCurveFit
					DataStruc_WWHPClg_CurveFit.RatedLoadVolFlowRate = .NameTagValueLookup("RatedLoadFlow")
					DataStruc_WWHPClg_CurveFit.RatedSourceVolFlowRate = .NameTagValueLookup("RatedSourceFlow")
					DataStruc_WWHPClg_CurveFit.RatedTotalCapacity = .NameTagValueLookup("RatedCapacity") * 1000
					DataStruc_WWHPClg_CurveFit.RatedCompressorPower = .NameTagValueLookup("RatedCompPower") * 1000
				Case SenderType.Pump_ConstantSpeed_NonDimensional
					DataStruc_ConstPump_NonDimensional.ImpellerDiameter = .NameTagValueLookup("ImpellerDiameter")
					DataStruc_ConstPump_NonDimensional.RotationalSpeed = .NameTagValueLookup("RotationalSpeed")
					DataStruc_ConstPump_NonDimensional.FluidTemperature = .NameTagValueLookup("FluidTemp")
				Case Else
					'COMPONENT_EXTENSION: 13. Retrieve detailed data values here
			End Select

		End With

	End Sub

	''' <summary>
	''' Applies the correction factor to the tabulated data
	''' </summary>
	''' <param name="TempArray">Input array: only the tabulated data</param>
	''' <param name="MyFullArray">Resulting corrected array (typically very large)</param>
	''' <param name="ErrorsFound">Signal to caller that an error was encountered</param>
	''' <remarks>Recursively steps through each correction factor, applying it to the
	''' full set of data developed up to that point (even data that has already been corrected
	''' by other factors).</remarks>
	Private Sub SimulateCorrectionFactors(ByVal TempArray As Single(,), _
	  ByRef MyFullArray As Single(,), _
	  ByRef ErrorsFound As Boolean)

		'let's create an array showing how many corrections each factor will make
		Dim NumCorrectors As Integer = PublicData.FullCorrectionFactorData.GetUpperBound(0)	'zero-based
		Dim NumCorrs(NumCorrectors) As Integer
		For I As Integer = 0 To NumCorrectors
			NumCorrs(I) = PublicData.FullCorrectionFactorData(I).NumCorrections	 'one-based for multiplication purposes
		Next

		'let's see how many values we will end up with
		Dim TotalValues As Integer
		TotalValues = TempArray.GetUpperBound(1) + 1  'second dimension is rows, add one to be one-based for multiplying
		For I As Integer = 0 To NumCorrectors
			TotalValues += NumCorrs(I) * TotalValues
		Next

		'set up an index to carry through
		Dim Index As Integer = -1 'zero-based

		'set up the output array also
		ReDim MyFullArray(TempArray.GetUpperBound(0), TotalValues - 1) 'zero-based

		'first put raw data into the output array
		For Entry As Integer = 0 To TempArray.GetUpperBound(1)
			Index += 1 'increment the index for every row/entry
			For Column As Integer = 0 To TempArray.GetUpperBound(0)
				MyFullArray(Column, Entry) = TempArray(Column, Entry)
			Next
		Next

		'apply dry bulb column if it exists
		Dim MyDBColumn As Integer = PublicData.GetDBValue

		Try
			'now use correction factors
			For CorrectionCounter As Integer = 0 To NumCorrs.GetUpperBound(0) 'for each correction factor type

				'set up a variable to tell how many sweeps each subsequent correction will have to make
				Dim TotalToSweep As Integer = Index	'next factor will have to sweep over all values up til now

				For CorrValueCounter As Integer = 0 To PublicData.FullCorrectionFactorData(CorrectionCounter).NumCorrections - 1 'I think it's minus one 'for each value in that factor

					'Need to also remember where we are in the base data to apply the correction factors 
					Dim BaseDataCounter As Integer = -1	'reset each new correction factor value

					For EntryCounter As Integer = 0 To TotalToSweep	'for every value up til now

						Index += 1 'increment every time we change a value
						BaseDataCounter += 1 'increment every time we change a value

						'need to also set up a column counter, as the affected data array doesn't have same # columns as the fullarray
						Dim AffectedColumnCounter As Integer = -1

						For ColumnCounter As Integer = 0 To MyFullArray.GetUpperBound(0)

							'check if a dry bulb column should be adjusted
							If PublicData.FullCorrectionFactorData(CorrectionCounter).CorrFactorIsaWBDBSet Then
								If ColumnCounter = MyDBColumn Then
									MyFullArray(ColumnCounter, Index) = PublicData.FullCorrectionFactorData(CorrectionCounter).CorrFactorDBValue
									Continue For
								End If
							End If

							'adjust base data if applicable
							If PublicData.FullCorrectionFactorData(CorrectionCounter).BaseColumnIndex = ColumnCounter Then
								Select Case PublicData.FullCorrectionFactorData(CorrectionCounter).ModificationType
									Case PublicData.CorrectionFactorType.Multiplier
										MyFullArray(ColumnCounter, Index) = TempArray(ColumnCounter, BaseDataCounter) * PublicData.FullCorrectionFactorData(CorrectionCounter).BaseData(CorrValueCounter)
									Case PublicData.CorrectionFactorType.Replacer
										MyFullArray(ColumnCounter, Index) = PublicData.FullCorrectionFactorData(CorrectionCounter).BaseData(CorrValueCounter)
								End Select
								Continue For
							End If

							'then adjust the affected data
							If PublicData.FullCorrectionFactorData(CorrectionCounter).ColumnsToModify(ColumnCounter) Then
								AffectedColumnCounter += 1
								MyFullArray(ColumnCounter, Index) = TempArray(ColumnCounter, BaseDataCounter) * PublicData.FullCorrectionFactorData(CorrectionCounter).AffectedData(AffectedColumnCounter, CorrValueCounter)
								Continue For
							End If

							'if none of these then just copy the data down
							MyFullArray(ColumnCounter, Index) = TempArray(ColumnCounter, BaseDataCounter)

						Next 'column counter

					Next 'entry in the base array

				Next 'correction value

				'if we need to, we'll repackage the temparray to match all values up to now
				If CorrectionCounter < NumCorrs.GetUpperBound(0) Then
					ReDim TempArray(MyFullArray.GetUpperBound(0), Index)
					For I As Integer = 0 To Index
						For ColumnCounter As Integer = 0 To MyFullArray.GetUpperBound(0)
							TempArray(ColumnCounter, I) = MyFullArray(ColumnCounter, I)
						Next
					Next
				End If

			Next 'correction factor
		Catch xc As Exception
			Call ShowWarningError("Correction Factors could not be applied." & vbCrLf & _
			 "Error message: " & xc.ToString & vbCrLf & _
			 "Further messages may follow.")
		End Try
	End Sub
#End Region

#Region " Main Form Button Click Handlers "
	Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click

		'Check to make sure prerequisites are in place
		If PublicData.CatalogDataInPlace = False Then
			CatalogErrorProvider.SetError(Me.btnCatalog, "Must input catalog data prior to generating output")
			Exit Sub
		ElseIf rdoObject.Checked = False AndAlso rdoSummary.Checked = False Then
			OutputErrorProvider.SetError(Me.grpOutputType, "Must select an output type to generate data")
			Exit Sub
		End If

		'set up the threaded data so it knows what all it needs to do
		Dim ThreadedDataStructure As New PublicData.ThreadInitStructure
		With ThreadedDataStructure
			.DoAbsolutePlot = Me.chkComparePlot.Checked
			.DoPercErrPlot = Me.chkPercErrPlot.Checked
			If rdoObject.Checked Then
				.DoObjectOrParameters = ObjectOrParList.EnergyPlusObject
			Else
				.DoObjectOrParameters = ObjectOrParList.ParameterList
			End If
		End With

		'instantiate the threaded manager with the proper data
		Select Case PublicData.Sender
			Case SenderType.HPWaterAirHeatingCurveFit
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WAHPHtg_CurveFit, ThreadedDataStructure)
			Case SenderType.HPWaterAirHeatingWaterSideCurveFit
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WAHPHtg_WaterSideCurveFit, ThreadedDataStructure)
			Case SenderType.HPWaterAirHeatingParamEst
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WAHPHtg_ParamEst, ThreadedDataStructure)
			Case SenderType.HPWaterAirCoolingCurveFit
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WAHPClg_CurveFit, ThreadedDataStructure)
			Case SenderType.HPWaterWaterHeatingCurveFit
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WWHPHtg_CurveFit, ThreadedDataStructure)
			Case SenderType.HPWaterWaterCoolingCurveFit
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_WWHPClg_CurveFit, ThreadedDataStructure)
			Case SenderType.Pump_ConstantSpeed_NonDimensional
				Me.ThreadedComponentManager = New ThreadableInterfaceComponentManager(PublicData.Sender, Me.DataStruc_ConstPump_NonDimensional, ThreadedDataStructure)
			Case Else
				'COMPONENT_EXTENSION: 14. Instantiate proper threaded class and information
		End Select

		'start up a new thread with the threaded manager
		Dim ComponentManagerThread As New Threading.Thread(AddressOf Me.ThreadedComponentManager.ProcessComponent)
		With ComponentManagerThread
			.Name = "Parameter Estimation Tool Component Processing Thread"
			.Start()
		End With

	End Sub

	Private Sub btnData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnData.Click
		If Me.FullDataSet Is Nothing Then Exit Sub
		ProgClass = New OutputProgress
		ProgClass.MyFullArray = Me.FullDataSet
		Dim ProgThread As New Thread(AddressOf ProgClass.OutStringSub)
		ProgThread.SetApartmentState(ApartmentState.STA)
		ProgThread.Start()
	End Sub

	Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
		If MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Exit Program?") = MsgBoxResult.Yes Then
			Me.Close()
			Application.Exit()
			End
		End If
	End Sub
#End Region

#Region " Data In Place Tag Routines "
	Friend Sub UpdateSenderTypeNote()
		Select Case PublicData.Sender
			Case SenderType.None
				Me.SenderDataNote.Text = "  No Equip Selected"
			Case Else
				Me.SenderDataNote.Text = "  Equip: " & PublicData.EquipNames1(PublicData.Sender)
		End Select
	End Sub

	Friend Sub UpdateCatalogDataNote()
		SetADataNote(CatalogDataNote, PublicData.CatalogDataInPlace)
	End Sub

	Friend Sub UpdateCorrectionDataNote()
		SetADataNote(CorrectionDataNote, PublicData.CorrectionDataInPlace)
	End Sub

	Private Sub SetADataNote(ByVal t As ToolStripStatusLabel, ByVal b As Boolean)
		If b Then
			t.Text = " Data Entry Successful "
		Else
			t.Text = " Data is Empty or Erroneous "
		End If
	End Sub
#End Region

#Region " MenuStrip Items "
	Private Sub ProgramWalkthroughAndInstructionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgramWalkthroughAndInstructionsToolStripMenuItem.Click
		Dim HelpForm As New WalkthroughForm
		HelpForm.Show()
	End Sub

	Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
		Dim InfoText As New System.Text.StringBuilder
		InfoText.AppendLine("This program is intended as a parameter and coefficient generator for thermal systems equipment.")
		InfoText.AppendLine("Program created by Edwin Lee, August 2008 - " & Today.ToString("MMM yyyy"))
		InfoText.AppendLine()
		InfoText.AppendLine("Additional features to mention:")
		InfoText.AppendLine(vbTab & "● Graphical plotting engine is 'NPlot', available free from www.netcontrols.org")
		InfoText.AppendLine(vbTab & "● Dropdown checkbox class is 'CheckBoxComboBox', available free from www.codeproject.com")
		InfoText.AppendLine(vbTab & "● Matrix calculation library is 'Advanced Matrix Library', available free from www.codeproject.com")
		'why did the text in the last two lines do this when I pasted in the bullet?
		'the bullet character is: U+25CF Times New Roman "Black Circle"
		Call ShowInfoBox(InfoText.ToString)
	End Sub

	Private Sub WhatDataWillINeedForSelectedEquipmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WhatDataWillINeedForSelectedEquipmentToolStripMenuItem.Click
		Using dataform As New RequiredDataForm
			Call dataform.Initialize()
			dataform.ShowDialog()
		End Using
	End Sub

	Private Sub ReinitializeFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReinitializeFormToolStripMenuItem.Click
		Call ReInitForm()
	End Sub

	Private Sub StartWizardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartWizardToolStripMenuItem.Click
		Call btnCatalog_Click(Me, e)
	End Sub

	Private Sub OpenPDFHelpItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenPDFHelpItem.Click
		Dim CurRunPath As String = Application.StartupPath
		Dim PDFPath As String = CurRunPath & IO.Path.DirectorySeparatorChar & "Documentation.pdf"
		Try
			Dim myProcess As New Process
			With myProcess.StartInfo
				.FileName = PDFPath
				.WorkingDirectory = CurRunPath
			End With
			myProcess.Start()
		Catch ex As Exception
			Call ShowSevereError("There was an error opening the file or starting the process." & vbCrLf & _
			   "Expected to find documentation at path:" & vbCrLf & _
			   "  " & PDFPath & " " & vbCrLf & _
			   "Process terminated")
		End Try
	End Sub

	Private Sub CreateParametersFromEnteredDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateParametersFromEnteredDataToolStripMenuItem.Click
		Call btnRun_Click(Me, e)
	End Sub
#End Region

#Region " Program Settings Management "

	Private Sub chkComparePlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkComparePlot.CheckedChanged
		SaveProgramSettings()
	End Sub

	Private Sub chkPercErrPlot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPercErrPlot.CheckedChanged
		SaveProgramSettings()
	End Sub

	Private Sub SaveProgramSettings()
		My.Settings.chkAbsPlot = Me.chkComparePlot.Checked
		My.Settings.chkPercPlot = Me.chkPercErrPlot.Checked
		My.Settings.Save()
	End Sub

	Private Sub LoadAndSetProgramSettings()
		Me.chkComparePlot.Checked = My.Settings.chkAbsPlot
		Me.chkPercErrPlot.Checked = My.Settings.chkPercPlot
	End Sub

#End Region

End Class

''' <summary>
''' This class handles the output of data sets
''' </summary>
''' <remarks>A separate class was used to fully remove this from the main program.  Multithreading
''' is also used to complete the separation.  Events are triggered to update the progress bar.</remarks>i
Friend Class OutputProgress

	Friend Event InitProgress(ByVal i As Integer)
	Friend Event UpdateProgress()
	Friend MyFullArray(,) As Single
	
	<STAThread()> _
 Friend Sub OutStringSub()

		'reporting frequency
		Const OutputIncrement As Integer = 20

		'get a file path to save the data
		Dim FileDialog As New SaveFileDialog
		FileDialog.Filter = "Comma-delimited files (*.csv)|*.csv|All files (*.*)|*.*"
		FileDialog.Title = "Specify a path to save the data set"
		If FileDialog.ShowDialog = DialogResult.Cancel Then Return

		Dim ColumnSeparator As String = vbTab
		If My.Computer.FileSystem.GetFileInfo(FileDialog.FileName).Extension.ToUpper = ".CSV" Then ColumnSeparator = ","

		'get header strings
		Dim MyArray() As String = PublicData.GetHeaderStrings

		'alert caller to the number of steps
		RaiseEvent InitProgress(Int(MyFullArray.GetUpperBound(1) / OutputIncrement))

		'instantiate the output writer
		Using OutWriter As New IO.StreamWriter(FileDialog.FileName)

			'write headers
			If MyArray IsNot Nothing Then
				For I As Integer = 0 To MyArray.GetUpperBound(0)
					OutWriter.Write(MyArray(I))
					If I < MyArray.GetUpperBound(0) Then OutWriter.Write(ColumnSeparator)
				Next
				OutWriter.WriteLine()
			End If

			'write data
			For I As Integer = 0 To MyFullArray.GetUpperBound(1)
				If I > 0 Then OutWriter.WriteLine()
				For J As Integer = 0 To MyFullArray.GetUpperBound(0)
					OutWriter.Write(MyFullArray(J, I).ToString)
					If J < MyFullArray.GetUpperBound(0) Then OutWriter.Write(ColumnSeparator)
				Next
				If (I / OutputIncrement) = (Int(I / OutputIncrement)) Then RaiseEvent UpdateProgress()
			Next

		End Using

	End Sub

End Class

