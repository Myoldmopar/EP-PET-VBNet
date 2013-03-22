<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CatalogDataRepairForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CatalogDataRepairForm))
		Me.Label1 = New System.Windows.Forms.Label
		Me.cbo0 = New System.Windows.Forms.ComboBox
		Me.lblInstructions = New System.Windows.Forms.Label
		Me.btnOK = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.lblActually = New System.Windows.Forms.Label
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 49)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(39, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Label1"
		'
		'cbo0
		'
		Me.cbo0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbo0.FormattingEnabled = True
		Me.cbo0.Location = New System.Drawing.Point(161, 71)
		Me.cbo0.MaxDropDownItems = 10
		Me.cbo0.Name = "cbo0"
		Me.cbo0.Size = New System.Drawing.Size(170, 21)
		Me.cbo0.TabIndex = 1
		'
		'lblInstructions
		'
		Me.lblInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblInstructions.Location = New System.Drawing.Point(12, 7)
		Me.lblInstructions.Name = "lblInstructions"
		Me.lblInstructions.Size = New System.Drawing.Size(378, 34)
		Me.lblInstructions.TabIndex = 2
		Me.lblInstructions.Text = "This form will help correct data that is misaligned with the column headers.  Sim" & _
			"ply choose which columns the data are supposed to be in, and press ok!"
		Me.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnOK
		'
		Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOK.Location = New System.Drawing.Point(63, 264)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(88, 30)
		Me.btnOK.TabIndex = 3
		Me.btnOK.Text = "OK"
		Me.btnOK.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(242, 264)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(89, 30)
		Me.btnCancel.TabIndex = 4
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'lblActually
		'
		Me.lblActually.AutoSize = True
		Me.lblActually.Location = New System.Drawing.Point(17, 74)
		Me.lblActually.Name = "lblActually"
		Me.lblActually.Size = New System.Drawing.Size(138, 13)
		Me.lblActually.TabIndex = 5
		Me.lblActually.Text = "Actually belongs in column: "
		'
		'CatalogDataRepairForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.ClientSize = New System.Drawing.Size(600, 306)
		Me.Controls.Add(Me.lblActually)
		Me.Controls.Add(Me.cbo0)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOK)
		Me.Controls.Add(Me.lblInstructions)
		Me.Controls.Add(Me.Label1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "CatalogDataRepairForm"
		Me.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Catalog Data/Header Alignment Repair"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbo0 As System.Windows.Forms.ComboBox
    Friend WithEvents lblInstructions As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblActually As System.Windows.Forms.Label
End Class
