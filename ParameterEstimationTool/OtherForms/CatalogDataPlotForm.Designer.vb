<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CatalogDataPlotForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CatalogDataPlotForm))
		Me.TabControl1 = New System.Windows.Forms.TabControl
		Me.btnDone = New System.Windows.Forms.Button
		Me.lblInfo = New System.Windows.Forms.Label
		Me.SuspendLayout()
		'
		'TabControl1
		'
		Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TabControl1.Location = New System.Drawing.Point(12, 68)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(611, 287)
		Me.TabControl1.TabIndex = 0
		'
		'btnDone
		'
		Me.btnDone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnDone.Location = New System.Drawing.Point(542, 361)
		Me.btnDone.Name = "btnDone"
		Me.btnDone.Size = New System.Drawing.Size(81, 25)
		Me.btnDone.TabIndex = 2
		Me.btnDone.Text = "OK, Done"
		Me.btnDone.UseVisualStyleBackColor = True
		'
		'lblInfo
		'
		Me.lblInfo.AutoSize = True
		Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lblInfo.Location = New System.Drawing.Point(12, 9)
		Me.lblInfo.Name = "lblInfo"
		Me.lblInfo.Size = New System.Drawing.Size(90, 15)
		Me.lblInfo.TabIndex = 3
		Me.lblInfo.Text = "Information Label"
		'
		'CatalogDataPlotForm
		'
		Me.AcceptButton = Me.btnDone
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(635, 398)
		Me.Controls.Add(Me.lblInfo)
		Me.Controls.Add(Me.btnDone)
		Me.Controls.Add(Me.TabControl1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(400, 300)
		Me.Name = "CatalogDataPlotForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Catalog Data Array Display"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
	Friend WithEvents btnDone As System.Windows.Forms.Button
	Friend WithEvents lblInfo As System.Windows.Forms.Label
End Class
