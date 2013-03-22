<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CatalogDataWizard
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CatalogDataWizard))
		Me.DataGridView1 = New System.Windows.Forms.DataGridView
		Me.btnPasteData = New System.Windows.Forms.Button
		Me.lblInstruct = New System.Windows.Forms.Label
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnOK = New System.Windows.Forms.Button
		Me.lblInstruct2 = New System.Windows.Forms.Label
		Me.DataGridUnits = New System.Windows.Forms.DataGridView
		Me.cxtUnits = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.cxtUnitsColumn = New System.Windows.Forms.ToolStripMenuItem
		Me.cxtUnitsUnits = New System.Windows.Forms.ToolStripMenuItem
		Me.EdwinHereToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.cxtUnitsConstant = New System.Windows.Forms.ToolStripMenuItem
		Me.chkManuallyEditData = New System.Windows.Forms.CheckBox
		Me.btnConformUnits = New System.Windows.Forms.Button
		Me.btnRepairHeaders = New System.Windows.Forms.Button
		Me.grpUnits = New System.Windows.Forms.GroupBox
		Me.cmdSI = New System.Windows.Forms.Button
		Me.cmdIP = New System.Windows.Forms.Button
		Me.cmdClearGrids = New System.Windows.Forms.Button
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridUnits, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.cxtUnits.SuspendLayout()
		Me.grpUnits.SuspendLayout()
		Me.SuspendLayout()
		'
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.AllowUserToDeleteRows = False
		Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Location = New System.Drawing.Point(12, 179)
		Me.DataGridView1.MultiSelect = False
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.Size = New System.Drawing.Size(747, 286)
		Me.DataGridView1.TabIndex = 0
		'
		'btnPasteData
		'
		Me.btnPasteData.Location = New System.Drawing.Point(216, 38)
		Me.btnPasteData.Name = "btnPasteData"
		Me.btnPasteData.Size = New System.Drawing.Size(173, 26)
		Me.btnPasteData.TabIndex = 2
		Me.btnPasteData.Text = "Paste data from Clipboard"
		Me.btnPasteData.UseVisualStyleBackColor = True
		'
		'lblInstruct
		'
		Me.lblInstruct.Location = New System.Drawing.Point(12, 9)
		Me.lblInstruct.Name = "lblInstruct"
		Me.lblInstruct.Size = New System.Drawing.Size(747, 25)
		Me.lblInstruct.TabIndex = 3
		Me.lblInstruct.Text = "To operate, retrieve catalog data and paste into Excel, then copy from Excel and " & _
			"click button below to paste here..."
		Me.lblInstruct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(683, 482)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(79, 26)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnOK
		'
		Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOK.Enabled = False
		Me.btnOK.Location = New System.Drawing.Point(598, 482)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(79, 26)
		Me.btnOK.TabIndex = 5
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = True
		'
		'lblInstruct2
		'
		Me.lblInstruct2.Location = New System.Drawing.Point(12, 64)
		Me.lblInstruct2.Name = "lblInstruct2"
		Me.lblInstruct2.Size = New System.Drawing.Size(744, 25)
		Me.lblInstruct2.TabIndex = 8
		Me.lblInstruct2.Text = "Then choose the units for the data by right-clicking and selecting the appropriat" & _
			"e units..."
		Me.lblInstruct2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'DataGridUnits
		'
		Me.DataGridUnits.AllowUserToAddRows = False
		Me.DataGridUnits.AllowUserToDeleteRows = False
		Me.DataGridUnits.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.DataGridUnits.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.DataGridUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridUnits.ContextMenuStrip = Me.cxtUnits
		Me.DataGridUnits.Location = New System.Drawing.Point(12, 89)
		Me.DataGridUnits.MultiSelect = False
		Me.DataGridUnits.Name = "DataGridUnits"
		Me.DataGridUnits.ReadOnly = True
		Me.DataGridUnits.Size = New System.Drawing.Size(747, 85)
		Me.DataGridUnits.TabIndex = 9
		'
		'cxtUnits
		'
		Me.cxtUnits.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cxtUnitsColumn, Me.cxtUnitsUnits, Me.cxtUnitsConstant})
		Me.cxtUnits.Name = "cxtUnits"
		Me.cxtUnits.ShowImageMargin = False
		Me.cxtUnits.Size = New System.Drawing.Size(279, 92)
		'
		'cxtUnitsColumn
		'
		Me.cxtUnitsColumn.Name = "cxtUnitsColumn"
		Me.cxtUnitsColumn.Size = New System.Drawing.Size(278, 22)
		Me.cxtUnitsColumn.Text = "This numeric data actually belongs in column: "
		'
		'cxtUnitsUnits
		'
		Me.cxtUnitsUnits.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EdwinHereToolStripMenuItem})
		Me.cxtUnitsUnits.Name = "cxtUnitsUnits"
		Me.cxtUnitsUnits.Size = New System.Drawing.Size(278, 22)
		Me.cxtUnitsUnits.Text = "The units of this column are actually:"
		'
		'EdwinHereToolStripMenuItem
		'
		Me.EdwinHereToolStripMenuItem.Name = "EdwinHereToolStripMenuItem"
		Me.EdwinHereToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
		Me.EdwinHereToolStripMenuItem.Text = "Edwin Here"
		'
		'cxtUnitsConstant
		'
		Me.cxtUnitsConstant.Name = "cxtUnitsConstant"
		Me.cxtUnitsConstant.Size = New System.Drawing.Size(278, 22)
		Me.cxtUnitsConstant.Text = "Fill this column with a constant value..."
		'
		'chkManuallyEditData
		'
		Me.chkManuallyEditData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chkManuallyEditData.AutoSize = True
		Me.chkManuallyEditData.Location = New System.Drawing.Point(434, 488)
		Me.chkManuallyEditData.Name = "chkManuallyEditData"
		Me.chkManuallyEditData.Size = New System.Drawing.Size(165, 17)
		Me.chkManuallyEditData.TabIndex = 10
		Me.chkManuallyEditData.Text = "Manually Add/Remove Rows"
		Me.chkManuallyEditData.UseVisualStyleBackColor = True
		'
		'btnConformUnits
		'
		Me.btnConformUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnConformUnits.Location = New System.Drawing.Point(220, 482)
		Me.btnConformUnits.Name = "btnConformUnits"
		Me.btnConformUnits.Size = New System.Drawing.Size(86, 26)
		Me.btnConformUnits.TabIndex = 11
		Me.btnConformUnits.Text = "Conform Units"
		Me.btnConformUnits.UseVisualStyleBackColor = True
		'
		'btnRepairHeaders
		'
		Me.btnRepairHeaders.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnRepairHeaders.Location = New System.Drawing.Point(12, 482)
		Me.btnRepairHeaders.Name = "btnRepairHeaders"
		Me.btnRepairHeaders.Size = New System.Drawing.Size(203, 26)
		Me.btnRepairHeaders.TabIndex = 12
		Me.btnRepairHeaders.Text = "Repair column data/misaligned headers"
		Me.btnRepairHeaders.UseVisualStyleBackColor = True
		'
		'grpUnits
		'
		Me.grpUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.grpUnits.Controls.Add(Me.cmdSI)
		Me.grpUnits.Controls.Add(Me.cmdIP)
		Me.grpUnits.Location = New System.Drawing.Point(312, 471)
		Me.grpUnits.Name = "grpUnits"
		Me.grpUnits.Size = New System.Drawing.Size(116, 46)
		Me.grpUnits.TabIndex = 13
		Me.grpUnits.TabStop = False
		Me.grpUnits.Text = "Units Quick Convert"
		'
		'cmdSI
		'
		Me.cmdSI.Location = New System.Drawing.Point(64, 15)
		Me.cmdSI.Name = "cmdSI"
		Me.cmdSI.Size = New System.Drawing.Size(40, 25)
		Me.cmdSI.TabIndex = 1
		Me.cmdSI.Text = "SI"
		Me.cmdSI.UseVisualStyleBackColor = True
		'
		'cmdIP
		'
		Me.cmdIP.Location = New System.Drawing.Point(15, 15)
		Me.cmdIP.Name = "cmdIP"
		Me.cmdIP.Size = New System.Drawing.Size(40, 25)
		Me.cmdIP.TabIndex = 0
		Me.cmdIP.Text = "IP"
		Me.cmdIP.UseVisualStyleBackColor = True
		'
		'cmdClearGrids
		'
		Me.cmdClearGrids.Location = New System.Drawing.Point(395, 38)
		Me.cmdClearGrids.Name = "cmdClearGrids"
		Me.cmdClearGrids.Size = New System.Drawing.Size(154, 26)
		Me.cmdClearGrids.TabIndex = 14
		Me.cmdClearGrids.Text = "Clear Data Grids"
		Me.cmdClearGrids.UseVisualStyleBackColor = True
		'
		'CatalogDataWizard
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(771, 525)
		Me.Controls.Add(Me.cmdClearGrids)
		Me.Controls.Add(Me.btnConformUnits)
		Me.Controls.Add(Me.btnRepairHeaders)
		Me.Controls.Add(Me.grpUnits)
		Me.Controls.Add(Me.DataGridUnits)
		Me.Controls.Add(Me.chkManuallyEditData)
		Me.Controls.Add(Me.lblInstruct2)
		Me.Controls.Add(Me.lblInstruct)
		Me.Controls.Add(Me.btnPasteData)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.DataGridView1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "CatalogDataWizard"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Catalog Data Wizard"
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataGridUnits, System.ComponentModel.ISupportInitialize).EndInit()
		Me.cxtUnits.ResumeLayout(False)
		Me.grpUnits.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnPasteData As System.Windows.Forms.Button
    Friend WithEvents lblInstruct As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblInstruct2 As System.Windows.Forms.Label
    Friend WithEvents DataGridUnits As System.Windows.Forms.DataGridView
    Friend WithEvents chkManuallyEditData As System.Windows.Forms.CheckBox
    Friend WithEvents btnConformUnits As System.Windows.Forms.Button
    Friend WithEvents cxtUnits As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cxtUnitsColumn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cxtUnitsUnits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EdwinHereToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRepairHeaders As System.Windows.Forms.Button
    Friend WithEvents grpUnits As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSI As System.Windows.Forms.Button
    Friend WithEvents cmdIP As System.Windows.Forms.Button
	Friend WithEvents cmdClearGrids As System.Windows.Forms.Button
	Friend WithEvents cxtUnitsConstant As System.Windows.Forms.ToolStripMenuItem
End Class
