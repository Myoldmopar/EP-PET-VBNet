<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CorrectionFactorForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CorrectionFactorForm))
		Me.btnAdd2 = New System.Windows.Forms.Button
		Me.btnCancel2 = New System.Windows.Forms.Button
		Me.btnDone2 = New System.Windows.Forms.Button
		Me.Label1 = New System.Windows.Forms.Label
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.SuspendLayout()
		'
		'btnAdd2
		'
		Me.btnAdd2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnAdd2.Location = New System.Drawing.Point(132, 358)
		Me.btnAdd2.Name = "btnAdd2"
		Me.btnAdd2.Size = New System.Drawing.Size(75, 23)
		Me.btnAdd2.TabIndex = 0
		Me.btnAdd2.Text = "Add"
		Me.btnAdd2.UseVisualStyleBackColor = True
		'
		'btnCancel2
		'
		Me.btnCancel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel2.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel2.Location = New System.Drawing.Point(515, 358)
		Me.btnCancel2.Name = "btnCancel2"
		Me.btnCancel2.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel2.TabIndex = 1
		Me.btnCancel2.Text = "Cancel"
		Me.btnCancel2.UseVisualStyleBackColor = True
		'
		'btnDone2
		'
		Me.btnDone2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnDone2.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnDone2.Location = New System.Drawing.Point(411, 358)
		Me.btnDone2.Name = "btnDone2"
		Me.btnDone2.Size = New System.Drawing.Size(75, 23)
		Me.btnDone2.TabIndex = 2
		Me.btnDone2.Text = "Skip"
		Me.btnDone2.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(111, 6)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(486, 91)
		Me.Label1.TabIndex = 3
		Me.Label1.Text = resources.GetString("Label1.Text")
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Panel1
		'
		Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Panel1.AutoScroll = True
		Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Panel1.Location = New System.Drawing.Point(12, 103)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(684, 246)
		Me.Panel1.TabIndex = 4
		'
		'CorrectionFactorForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(708, 392)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.btnDone2)
		Me.Controls.Add(Me.btnCancel2)
		Me.Controls.Add(Me.btnAdd2)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(716, 400)
		Me.Name = "CorrectionFactorForm"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Correction Factor Wizard"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents btnAdd2 As System.Windows.Forms.Button
	Friend WithEvents btnCancel2 As System.Windows.Forms.Button
	Friend WithEvents btnDone2 As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
