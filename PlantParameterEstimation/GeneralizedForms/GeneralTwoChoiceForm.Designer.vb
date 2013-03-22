<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneralTwoChoiceForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralTwoChoiceForm))
		Me.btnChoiceYes = New System.Windows.Forms.Button
		Me.btnChoiceNo = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'btnChoiceYes
		'
		Me.btnChoiceYes.DialogResult = System.Windows.Forms.DialogResult.Yes
		Me.btnChoiceYes.Location = New System.Drawing.Point(58, 13)
		Me.btnChoiceYes.Name = "btnChoiceYes"
		Me.btnChoiceYes.Size = New System.Drawing.Size(127, 28)
		Me.btnChoiceYes.TabIndex = 0
		Me.btnChoiceYes.Text = "Choice 1"
		Me.btnChoiceYes.UseVisualStyleBackColor = True
		'
		'btnChoiceNo
		'
		Me.btnChoiceNo.DialogResult = System.Windows.Forms.DialogResult.No
		Me.btnChoiceNo.Location = New System.Drawing.Point(58, 47)
		Me.btnChoiceNo.Name = "btnChoiceNo"
		Me.btnChoiceNo.Size = New System.Drawing.Size(127, 28)
		Me.btnChoiceNo.TabIndex = 1
		Me.btnChoiceNo.Text = "Choice 2"
		Me.btnChoiceNo.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(58, 83)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(127, 28)
		Me.btnCancel.TabIndex = 2
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'BasicTwoChoiceForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(244, 123)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnChoiceNo)
		Me.Controls.Add(Me.btnChoiceYes)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "BasicTwoChoiceForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "BasicTwoChoiceForm"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btnChoiceYes As System.Windows.Forms.Button
	Friend WithEvents btnChoiceNo As System.Windows.Forms.Button
	Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
