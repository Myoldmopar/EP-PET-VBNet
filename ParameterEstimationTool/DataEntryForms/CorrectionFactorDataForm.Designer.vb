<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CorrectionFactorDataForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CorrectionFactorDataForm))
		Me.DataGridView1 = New System.Windows.Forms.DataGridView
		Me.Label1 = New System.Windows.Forms.Label
		Me.Button1 = New System.Windows.Forms.Button
		Me.Button2 = New System.Windows.Forms.Button
		Me.chkAddRows = New System.Windows.Forms.CheckBox
		Me.txtDB = New System.Windows.Forms.TextBox
		Me.Label3 = New System.Windows.Forms.Label
		Me.cboConstValue = New System.Windows.Forms.ComboBox
		Me.cboDB = New System.Windows.Forms.ComboBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.TT = New System.Windows.Forms.ToolTip(Me.components)
		Me.ErrorSignal = New System.Windows.Forms.ErrorProvider(Me.components)
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.ErrorSignal, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.AllowUserToDeleteRows = False
		Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Location = New System.Drawing.Point(12, 160)
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.Size = New System.Drawing.Size(499, 221)
		Me.DataGridView1.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(39, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Label1"
		'
		'Button1
		'
		Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.Button1.Location = New System.Drawing.Point(152, 387)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 4
		Me.Button1.Text = "OK"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.Button2.Location = New System.Drawing.Point(291, 387)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(75, 23)
		Me.Button2.TabIndex = 5
		Me.Button2.Text = "Cancel"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'chkAddRows
		'
		Me.chkAddRows.Anchor = System.Windows.Forms.AnchorStyles.Left
		Me.chkAddRows.AutoSize = True
		Me.chkAddRows.Location = New System.Drawing.Point(12, 391)
		Me.chkAddRows.Name = "chkAddRows"
		Me.chkAddRows.Size = New System.Drawing.Size(113, 17)
		Me.chkAddRows.TabIndex = 6
		Me.chkAddRows.Text = "Manually edit rows"
		Me.chkAddRows.UseVisualStyleBackColor = True
		'
		'txtDB
		'
		Me.txtDB.Enabled = False
		Me.txtDB.Location = New System.Drawing.Point(210, 107)
		Me.txtDB.Name = "txtDB"
		Me.txtDB.Size = New System.Drawing.Size(87, 20)
		Me.txtDB.TabIndex = 8
		Me.txtDB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(12, 110)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(171, 13)
		Me.Label3.TabIndex = 9
		Me.Label3.Text = "Dry Bulb Value/Units (if applicable)"
		'
		'cboConstValue
		'
		Me.cboConstValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboConstValue.Enabled = False
		Me.cboConstValue.FormattingEnabled = True
		Me.cboConstValue.Location = New System.Drawing.Point(210, 133)
		Me.cboConstValue.Name = "cboConstValue"
		Me.cboConstValue.Size = New System.Drawing.Size(89, 21)
		Me.cboConstValue.TabIndex = 10
		'
		'cboDB
		'
		Me.cboDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboDB.Enabled = False
		Me.cboDB.FormattingEnabled = True
		Me.cboDB.Location = New System.Drawing.Point(320, 107)
		Me.cboDB.Name = "cboDB"
		Me.cboDB.Size = New System.Drawing.Size(89, 21)
		Me.cboDB.TabIndex = 11
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(12, 136)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(191, 13)
		Me.Label4.TabIndex = 12
		Me.Label4.Text = "Replacement Data Units (if applicable):"
		'
		'ErrorSignal
		'
		Me.ErrorSignal.ContainerControl = Me
		'
		'CorrectionFactorDataForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.ClientSize = New System.Drawing.Size(523, 417)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.cboDB)
		Me.Controls.Add(Me.cboConstValue)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.txtDB)
		Me.Controls.Add(Me.chkAddRows)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.DataGridView1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "CorrectionFactorDataForm"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Enter Data for Correction Factor"
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.ErrorSignal, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Button1 As System.Windows.Forms.Button
	Friend WithEvents Button2 As System.Windows.Forms.Button
	Friend WithEvents chkAddRows As System.Windows.Forms.CheckBox
	Friend WithEvents txtDB As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cboConstValue As System.Windows.Forms.ComboBox
	Friend WithEvents cboDB As System.Windows.Forms.ComboBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents TT As System.Windows.Forms.ToolTip
	Friend WithEvents ErrorSignal As System.Windows.Forms.ErrorProvider
End Class
