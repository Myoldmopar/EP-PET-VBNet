<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.lblProgress = New System.Windows.Forms.Label
		Me.prgProgress = New System.Windows.Forms.ProgressBar
		Me.grpOutputType = New System.Windows.Forms.GroupBox
		Me.rdoSummary = New System.Windows.Forms.RadioButton
		Me.chkPercErrPlot = New System.Windows.Forms.CheckBox
		Me.rdoObject = New System.Windows.Forms.RadioButton
		Me.chkComparePlot = New System.Windows.Forms.CheckBox
		Me.treeEquipType = New System.Windows.Forms.TreeView
		Me.btnCatalog = New System.Windows.Forms.Button
		Me.btnRun = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.txtOutput = New System.Windows.Forms.RichTextBox
		Me.btnData = New System.Windows.Forms.Button
		Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
		Me.GeneralOperationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ReinitializeFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.StartWizardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.CreateParametersFromEnteredDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ProgramWalkthroughAndInstructionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.OpenPDFHelpItem = New System.Windows.Forms.ToolStripMenuItem
		Me.WhatDataWillINeedForSelectedEquipmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
		Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
		Me.CatalogDataNote = New System.Windows.Forms.ToolStripStatusLabel
		Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel
		Me.CorrectionDataNote = New System.Windows.Forms.ToolStripStatusLabel
		Me.SenderDataNote = New System.Windows.Forms.ToolStripStatusLabel
		Me.grpEquipType = New System.Windows.Forms.GroupBox
		Me.btnEngage = New System.Windows.Forms.Button
		Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.CatalogErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.OutputErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.grpOutputType.SuspendLayout()
		Me.MenuStrip1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.StatusStrip1.SuspendLayout()
		Me.grpEquipType.SuspendLayout()
		CType(Me.CatalogErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.OutputErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'lblProgress
		'
		Me.lblProgress.Location = New System.Drawing.Point(6, 137)
		Me.lblProgress.Name = "lblProgress"
		Me.lblProgress.Size = New System.Drawing.Size(121, 19)
		Me.lblProgress.TabIndex = 16
		Me.lblProgress.Text = "Run Progress"
		Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'prgProgress
		'
		Me.prgProgress.Location = New System.Drawing.Point(6, 159)
		Me.prgProgress.Maximum = 4
		Me.prgProgress.Name = "prgProgress"
		Me.prgProgress.Size = New System.Drawing.Size(121, 19)
		Me.prgProgress.Step = 1
		Me.prgProgress.TabIndex = 15
		'
		'grpOutputType
		'
		Me.grpOutputType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.grpOutputType.Controls.Add(Me.rdoSummary)
		Me.grpOutputType.Controls.Add(Me.chkPercErrPlot)
		Me.grpOutputType.Controls.Add(Me.rdoObject)
		Me.grpOutputType.Controls.Add(Me.chkComparePlot)
		Me.grpOutputType.Location = New System.Drawing.Point(185, 208)
		Me.grpOutputType.Name = "grpOutputType"
		Me.grpOutputType.Size = New System.Drawing.Size(305, 68)
		Me.grpOutputType.TabIndex = 10
		Me.grpOutputType.TabStop = False
		Me.grpOutputType.Text = "Output Type"
		'
		'rdoSummary
		'
		Me.rdoSummary.AutoSize = True
		Me.rdoSummary.Location = New System.Drawing.Point(5, 41)
		Me.rdoSummary.Name = "rdoSummary"
		Me.rdoSummary.Size = New System.Drawing.Size(119, 17)
		Me.rdoSummary.TabIndex = 1
		Me.rdoSummary.Text = "Parameter Summary"
		Me.rdoSummary.UseVisualStyleBackColor = True
		'
		'chkPercErrPlot
		'
		Me.chkPercErrPlot.AutoSize = True
		Me.chkPercErrPlot.Checked = True
		Me.chkPercErrPlot.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkPercErrPlot.Location = New System.Drawing.Point(153, 42)
		Me.chkPercErrPlot.Name = "chkPercErrPlot"
		Me.chkPercErrPlot.Size = New System.Drawing.Size(106, 17)
		Me.chkPercErrPlot.TabIndex = 20
		Me.chkPercErrPlot.Text = "Data % Error Plot"
		Me.chkPercErrPlot.UseVisualStyleBackColor = True
		'
		'rdoObject
		'
		Me.rdoObject.AutoSize = True
		Me.rdoObject.Checked = True
		Me.rdoObject.Location = New System.Drawing.Point(5, 18)
		Me.rdoObject.Name = "rdoObject"
		Me.rdoObject.Size = New System.Drawing.Size(112, 17)
		Me.rdoObject.TabIndex = 0
		Me.rdoObject.TabStop = True
		Me.rdoObject.Text = "EnergyPlus Object"
		Me.rdoObject.UseVisualStyleBackColor = True
		'
		'chkComparePlot
		'
		Me.chkComparePlot.AutoSize = True
		Me.chkComparePlot.Checked = True
		Me.chkComparePlot.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkComparePlot.Location = New System.Drawing.Point(153, 19)
		Me.chkComparePlot.Name = "chkComparePlot"
		Me.chkComparePlot.Size = New System.Drawing.Size(128, 17)
		Me.chkComparePlot.TabIndex = 19
		Me.chkComparePlot.Text = "Data Comparison Plot"
		Me.chkComparePlot.UseVisualStyleBackColor = True
		'
		'treeEquipType
		'
		Me.treeEquipType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.treeEquipType.FullRowSelect = True
		Me.treeEquipType.Location = New System.Drawing.Point(6, 16)
		Me.treeEquipType.Name = "treeEquipType"
		Me.treeEquipType.Size = New System.Drawing.Size(220, 144)
		Me.treeEquipType.TabIndex = 25
		'
		'btnCatalog
		'
		Me.btnCatalog.BackColor = System.Drawing.Color.WhiteSmoke
		Me.btnCatalog.Location = New System.Drawing.Point(6, 19)
		Me.btnCatalog.Name = "btnCatalog"
		Me.btnCatalog.Size = New System.Drawing.Size(121, 37)
		Me.btnCatalog.TabIndex = 13
		Me.btnCatalog.Text = "Catalog Data Wizard"
		Me.btnCatalog.UseVisualStyleBackColor = False
		'
		'btnRun
		'
		Me.btnRun.BackColor = System.Drawing.Color.WhiteSmoke
		Me.btnRun.Location = New System.Drawing.Point(6, 105)
		Me.btnRun.Name = "btnRun"
		Me.btnRun.Size = New System.Drawing.Size(121, 24)
		Me.btnRun.TabIndex = 12
		Me.btnRun.Text = "Create Parameters"
		Me.btnRun.UseVisualStyleBackColor = False
		'
		'btnCancel
		'
		Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
		Me.btnCancel.Location = New System.Drawing.Point(7, 206)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(121, 24)
		Me.btnCancel.TabIndex = 11
		Me.btnCancel.Text = "Close"
		Me.btnCancel.UseVisualStyleBackColor = False
		'
		'txtOutput
		'
		Me.txtOutput.BackColor = System.Drawing.Color.WhiteSmoke
		Me.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txtOutput.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtOutput.Location = New System.Drawing.Point(3, 16)
		Me.txtOutput.Name = "txtOutput"
		Me.txtOutput.Size = New System.Drawing.Size(282, 228)
		Me.txtOutput.TabIndex = 17
		Me.txtOutput.Text = ""
		Me.txtOutput.WordWrap = False
		'
		'btnData
		'
		Me.btnData.BackColor = System.Drawing.Color.WhiteSmoke
		Me.btnData.Enabled = False
		Me.btnData.Location = New System.Drawing.Point(6, 67)
		Me.btnData.Name = "btnData"
		Me.btnData.Size = New System.Drawing.Size(146, 27)
		Me.btnData.TabIndex = 20
		Me.btnData.Text = "Save Full Input Data to File"
		Me.btnData.UseVisualStyleBackColor = False
		'
		'MenuStrip1
		'
		Me.MenuStrip1.BackColor = System.Drawing.Color.WhiteSmoke
		Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GeneralOperationsToolStripMenuItem, Me.HelpToolStripMenuItem})
		Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.MenuStrip1.Name = "MenuStrip1"
		Me.MenuStrip1.Size = New System.Drawing.Size(805, 24)
		Me.MenuStrip1.TabIndex = 21
		Me.MenuStrip1.Text = "MenuStrip1"
		'
		'GeneralOperationsToolStripMenuItem
		'
		Me.GeneralOperationsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReinitializeFormToolStripMenuItem, Me.StartWizardToolStripMenuItem, Me.CreateParametersFromEnteredDataToolStripMenuItem})
		Me.GeneralOperationsToolStripMenuItem.Name = "GeneralOperationsToolStripMenuItem"
		Me.GeneralOperationsToolStripMenuItem.Size = New System.Drawing.Size(112, 20)
		Me.GeneralOperationsToolStripMenuItem.Text = "General Operations"
		'
		'ReinitializeFormToolStripMenuItem
		'
		Me.ReinitializeFormToolStripMenuItem.Name = "ReinitializeFormToolStripMenuItem"
		Me.ReinitializeFormToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
		Me.ReinitializeFormToolStripMenuItem.Text = "Re-initialize form"
		'
		'StartWizardToolStripMenuItem
		'
		Me.StartWizardToolStripMenuItem.Name = "StartWizardToolStripMenuItem"
		Me.StartWizardToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
		Me.StartWizardToolStripMenuItem.Text = "Start catalog data entry wizard"
		'
		'CreateParametersFromEnteredDataToolStripMenuItem
		'
		Me.CreateParametersFromEnteredDataToolStripMenuItem.Name = "CreateParametersFromEnteredDataToolStripMenuItem"
		Me.CreateParametersFromEnteredDataToolStripMenuItem.Size = New System.Drawing.Size(267, 22)
		Me.CreateParametersFromEnteredDataToolStripMenuItem.Text = "Create parameters from entered data"
		'
		'HelpToolStripMenuItem
		'
		Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgramWalkthroughAndInstructionsToolStripMenuItem, Me.OpenPDFHelpItem, Me.WhatDataWillINeedForSelectedEquipmentToolStripMenuItem, Me.AboutToolStripMenuItem})
		Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
		Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
		Me.HelpToolStripMenuItem.Text = "Help"
		'
		'ProgramWalkthroughAndInstructionsToolStripMenuItem
		'
		Me.ProgramWalkthroughAndInstructionsToolStripMenuItem.Name = "ProgramWalkthroughAndInstructionsToolStripMenuItem"
		Me.ProgramWalkthroughAndInstructionsToolStripMenuItem.Size = New System.Drawing.Size(305, 22)
		Me.ProgramWalkthroughAndInstructionsToolStripMenuItem.Text = "Program walkthrough and instructions"
		'
		'OpenPDFHelpItem
		'
		Me.OpenPDFHelpItem.Name = "OpenPDFHelpItem"
		Me.OpenPDFHelpItem.Size = New System.Drawing.Size(305, 22)
		Me.OpenPDFHelpItem.Text = "Open documentation .pdf file"
		'
		'WhatDataWillINeedForSelectedEquipmentToolStripMenuItem
		'
		Me.WhatDataWillINeedForSelectedEquipmentToolStripMenuItem.Name = "WhatDataWillINeedForSelectedEquipmentToolStripMenuItem"
		Me.WhatDataWillINeedForSelectedEquipmentToolStripMenuItem.Size = New System.Drawing.Size(305, 22)
		Me.WhatDataWillINeedForSelectedEquipmentToolStripMenuItem.Text = "What data will I need for selected equipment?"
		'
		'AboutToolStripMenuItem
		'
		Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
		Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(305, 22)
		Me.AboutToolStripMenuItem.Text = "About..."
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.btnCatalog)
		Me.GroupBox1.Controls.Add(Me.btnRun)
		Me.GroupBox1.Controls.Add(Me.prgProgress)
		Me.GroupBox1.Controls.Add(Me.lblProgress)
		Me.GroupBox1.Controls.Add(Me.btnData)
		Me.GroupBox1.Controls.Add(Me.btnCancel)
		Me.GroupBox1.Location = New System.Drawing.Point(11, 36)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(158, 239)
		Me.GroupBox1.TabIndex = 22
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Controls Arena"
		'
		'GroupBox2
		'
		Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox2.Controls.Add(Me.txtOutput)
		Me.GroupBox2.Location = New System.Drawing.Point(505, 30)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(288, 247)
		Me.GroupBox2.TabIndex = 23
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Parameter Output"
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.CatalogDataNote, Me.ToolStripStatusLabel3, Me.CorrectionDataNote, Me.SenderDataNote})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 280)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(805, 22)
		Me.StatusStrip1.TabIndex = 24
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'ToolStripStatusLabel1
		'
		Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
		Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(111, 17)
		Me.ToolStripStatusLabel1.Text = "Catalog Data Status: "
		'
		'CatalogDataNote
		'
		Me.CatalogDataNote.Name = "CatalogDataNote"
		Me.CatalogDataNote.Size = New System.Drawing.Size(146, 17)
		Me.CatalogDataNote.Text = " Data Is Empty or Erroneous "
		'
		'ToolStripStatusLabel3
		'
		Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
		Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(129, 17)
		Me.ToolStripStatusLabel3.Text = "Correction Factor Status:"
		'
		'CorrectionDataNote
		'
		Me.CorrectionDataNote.Name = "CorrectionDataNote"
		Me.CorrectionDataNote.Size = New System.Drawing.Size(146, 17)
		Me.CorrectionDataNote.Text = " Data Is Empty or Erroneous "
		'
		'SenderDataNote
		'
		Me.SenderDataNote.Name = "SenderDataNote"
		Me.SenderDataNote.Size = New System.Drawing.Size(87, 17)
		Me.SenderDataNote.Text = "SenderDataNote"
		'
		'grpEquipType
		'
		Me.grpEquipType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.grpEquipType.Controls.Add(Me.btnEngage)
		Me.grpEquipType.Controls.Add(Me.treeEquipType)
		Me.grpEquipType.Location = New System.Drawing.Point(185, 36)
		Me.grpEquipType.Name = "grpEquipType"
		Me.grpEquipType.Size = New System.Drawing.Size(305, 166)
		Me.grpEquipType.TabIndex = 25
		Me.grpEquipType.TabStop = False
		Me.grpEquipType.Text = "Equipment Type"
		'
		'btnEngage
		'
		Me.btnEngage.Location = New System.Drawing.Point(232, 60)
		Me.btnEngage.Name = "btnEngage"
		Me.btnEngage.Size = New System.Drawing.Size(60, 39)
		Me.btnEngage.TabIndex = 26
		Me.btnEngage.Text = "Engage Selection"
		Me.btnEngage.UseVisualStyleBackColor = True
		'
		'CatalogErrorProvider
		'
		Me.CatalogErrorProvider.ContainerControl = Me
		'
		'OutputErrorProvider
		'
		Me.OutputErrorProvider.ContainerControl = Me
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(805, 302)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Controls.Add(Me.grpEquipType)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.MenuStrip1)
		Me.Controls.Add(Me.grpOutputType)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MainMenuStrip = Me.MenuStrip1
		Me.MinimumSize = New System.Drawing.Size(813, 336)
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Plant Equipment Parameter Estimator"
		Me.grpOutputType.ResumeLayout(False)
		Me.grpOutputType.PerformLayout()
		Me.MenuStrip1.ResumeLayout(False)
		Me.MenuStrip1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox2.ResumeLayout(False)
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.grpEquipType.ResumeLayout(False)
		CType(Me.CatalogErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.OutputErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents lblProgress As System.Windows.Forms.Label
	Friend WithEvents prgProgress As System.Windows.Forms.ProgressBar
	Friend WithEvents grpOutputType As System.Windows.Forms.GroupBox
	Friend WithEvents rdoSummary As System.Windows.Forms.RadioButton
	Friend WithEvents rdoObject As System.Windows.Forms.RadioButton
	Friend WithEvents btnCatalog As System.Windows.Forms.Button
	Friend WithEvents btnRun As System.Windows.Forms.Button
	Friend WithEvents btnCancel As System.Windows.Forms.Button
	Friend WithEvents txtOutput As System.Windows.Forms.RichTextBox
	Friend WithEvents chkComparePlot As System.Windows.Forms.CheckBox
	Friend WithEvents chkPercErrPlot As System.Windows.Forms.CheckBox
	Friend WithEvents btnData As System.Windows.Forms.Button
	Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
	Friend WithEvents GeneralOperationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents WhatDataWillINeedForSelectedEquipmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ReinitializeFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents StartWizardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ProgramWalkthroughAndInstructionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents CreateParametersFromEnteredDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents CatalogDataNote As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents CorrectionDataNote As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents OpenPDFHelpItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents treeEquipType As System.Windows.Forms.TreeView
	Friend WithEvents grpEquipType As System.Windows.Forms.GroupBox
	Friend WithEvents SenderDataNote As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents btnEngage As System.Windows.Forms.Button
	Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
	Friend WithEvents CatalogErrorProvider As System.Windows.Forms.ErrorProvider
	Friend WithEvents OutputErrorProvider As System.Windows.Forms.ErrorProvider

End Class
