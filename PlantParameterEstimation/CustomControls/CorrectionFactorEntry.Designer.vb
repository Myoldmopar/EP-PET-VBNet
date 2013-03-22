<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CorrectionFactorEntry
	Inherits System.Windows.Forms.UserControl

	'UserControl1 overrides dispose to clean up the component list.
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
		Dim CheckBoxProperties1 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties
		Me.grpOverall = New System.Windows.Forms.GroupBox
		Me.Label3 = New System.Windows.Forms.Label
		Me.chkCbo = New PresentationControls.CheckBoxComboBox
		Me.chkWBDB = New System.Windows.Forms.CheckBox
		Me.cboBase = New System.Windows.Forms.ComboBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.grpCorrType = New System.Windows.Forms.GroupBox
		Me.rdoReplacement = New System.Windows.Forms.RadioButton
		Me.rdoMultiplier = New System.Windows.Forms.RadioButton
		Me.txtCorrValues = New System.Windows.Forms.TextBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.btnRemove = New System.Windows.Forms.Button
		Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.grpOverall.SuspendLayout()
		Me.grpCorrType.SuspendLayout()
		CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'grpOverall
		'
		Me.grpOverall.Controls.Add(Me.Label3)
		Me.grpOverall.Controls.Add(Me.chkCbo)
		Me.grpOverall.Controls.Add(Me.chkWBDB)
		Me.grpOverall.Controls.Add(Me.cboBase)
		Me.grpOverall.Controls.Add(Me.Label2)
		Me.grpOverall.Controls.Add(Me.grpCorrType)
		Me.grpOverall.Controls.Add(Me.txtCorrValues)
		Me.grpOverall.Controls.Add(Me.Label1)
		Me.grpOverall.Controls.Add(Me.btnRemove)
		Me.grpOverall.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grpOverall.Location = New System.Drawing.Point(0, 0)
		Me.grpOverall.Name = "grpOverall"
		Me.grpOverall.Size = New System.Drawing.Size(639, 108)
		Me.grpOverall.TabIndex = 0
		Me.grpOverall.TabStop = False
		Me.grpOverall.Text = "CorrFactorName"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(385, 57)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(158, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "Data affected by this correction:"
		'
		'chkCbo
		'
		CheckBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkCbo.CheckBoxProperties = CheckBoxProperties1
		Me.chkCbo.DisplayMemberSingleItem = ""
		Me.chkCbo.FormattingEnabled = True
		Me.chkCbo.Location = New System.Drawing.Point(388, 73)
		Me.chkCbo.Name = "chkCbo"
		Me.chkCbo.Size = New System.Drawing.Size(245, 21)
		Me.chkCbo.TabIndex = 7
		'
		'chkWBDB
		'
		Me.chkWBDB.Location = New System.Drawing.Point(116, 52)
		Me.chkWBDB.Name = "chkWBDB"
		Me.chkWBDB.Size = New System.Drawing.Size(114, 34)
		Me.chkWBDB.TabIndex = 6
		Me.chkWBDB.Text = "This is a Wet-Bulb + Dry-Bulb factor"
		Me.chkWBDB.UseVisualStyleBackColor = True
		'
		'cboBase
		'
		Me.cboBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboBase.FormattingEnabled = True
		Me.cboBase.Location = New System.Drawing.Point(388, 30)
		Me.cboBase.Name = "cboBase"
		Me.cboBase.Size = New System.Drawing.Size(245, 21)
		Me.cboBase.TabIndex = 5
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(385, 14)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(122, 13)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "Base data for this factor:"
		'
		'grpCorrType
		'
		Me.grpCorrType.Controls.Add(Me.rdoReplacement)
		Me.grpCorrType.Controls.Add(Me.rdoMultiplier)
		Me.grpCorrType.Location = New System.Drawing.Point(250, 23)
		Me.grpCorrType.Name = "grpCorrType"
		Me.grpCorrType.Size = New System.Drawing.Size(129, 66)
		Me.grpCorrType.TabIndex = 3
		Me.grpCorrType.TabStop = False
		Me.grpCorrType.Text = "Correction Factor Type"
		'
		'rdoReplacement
		'
		Me.rdoReplacement.AutoSize = True
		Me.rdoReplacement.Location = New System.Drawing.Point(19, 42)
		Me.rdoReplacement.Name = "rdoReplacement"
		Me.rdoReplacement.Size = New System.Drawing.Size(88, 17)
		Me.rdoReplacement.TabIndex = 1
		Me.rdoReplacement.Text = "Replacement"
		Me.rdoReplacement.UseVisualStyleBackColor = True
		'
		'rdoMultiplier
		'
		Me.rdoMultiplier.AutoSize = True
		Me.rdoMultiplier.Checked = True
		Me.rdoMultiplier.Location = New System.Drawing.Point(19, 19)
		Me.rdoMultiplier.Name = "rdoMultiplier"
		Me.rdoMultiplier.Size = New System.Drawing.Size(66, 17)
		Me.rdoMultiplier.TabIndex = 0
		Me.rdoMultiplier.TabStop = True
		Me.rdoMultiplier.Text = "Multiplier"
		Me.rdoMultiplier.UseVisualStyleBackColor = True
		'
		'txtCorrValues
		'
		Me.txtCorrValues.Location = New System.Drawing.Point(215, 28)
		Me.txtCorrValues.Name = "txtCorrValues"
		Me.txtCorrValues.Size = New System.Drawing.Size(24, 20)
		Me.txtCorrValues.TabIndex = 2
		Me.txtCorrValues.Text = "5"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(106, 30)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(103, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "# Correction Values:"
		'
		'btnRemove
		'
		Me.btnRemove.Location = New System.Drawing.Point(6, 38)
		Me.btnRemove.Name = "btnRemove"
		Me.btnRemove.Size = New System.Drawing.Size(79, 24)
		Me.btnRemove.TabIndex = 0
		Me.btnRemove.Text = "Remove"
		Me.btnRemove.UseVisualStyleBackColor = True
		'
		'ErrorProvider1
		'
		Me.ErrorProvider1.ContainerControl = Me
		'
		'CorrectionFactorEntry
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.grpOverall)
		Me.MaximumSize = New System.Drawing.Size(1500, 108)
		Me.MinimumSize = New System.Drawing.Size(639, 108)
		Me.Name = "CorrectionFactorEntry"
		Me.Size = New System.Drawing.Size(639, 108)
		Me.grpOverall.ResumeLayout(False)
		Me.grpOverall.PerformLayout()
		Me.grpCorrType.ResumeLayout(False)
		Me.grpCorrType.PerformLayout()
		CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents grpOverall As System.Windows.Forms.GroupBox
	Friend WithEvents txtCorrValues As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents btnRemove As System.Windows.Forms.Button
	Friend WithEvents chkWBDB As System.Windows.Forms.CheckBox
	Friend WithEvents cboBase As System.Windows.Forms.ComboBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents grpCorrType As System.Windows.Forms.GroupBox
	Friend WithEvents rdoReplacement As System.Windows.Forms.RadioButton
	Friend WithEvents rdoMultiplier As System.Windows.Forms.RadioButton
	Friend WithEvents chkCbo As PresentationControls.CheckBoxComboBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider

End Class
