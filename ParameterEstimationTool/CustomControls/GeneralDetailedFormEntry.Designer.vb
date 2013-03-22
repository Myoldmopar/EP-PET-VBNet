<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneralDetailedFormEntry
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
		Me.cboDataUnits = New System.Windows.Forms.ComboBox
		Me.lblDataName = New System.Windows.Forms.Label
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.txtDataValue = New System.Windows.Forms.TextBox
		Me.lblInfoHover = New System.Windows.Forms.Label
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.TableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'cboDataUnits
		'
		Me.cboDataUnits.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cboDataUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboDataUnits.FormattingEnabled = True
		Me.cboDataUnits.Location = New System.Drawing.Point(252, 33)
		Me.cboDataUnits.Name = "cboDataUnits"
		Me.cboDataUnits.Size = New System.Drawing.Size(99, 21)
		Me.cboDataUnits.TabIndex = 3
		'
		'lblDataName
		'
		Me.lblDataName.AutoSize = True
		Me.lblDataName.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblDataName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDataName.Location = New System.Drawing.Point(4, 1)
		Me.lblDataName.Name = "lblDataName"
		Me.lblDataName.Size = New System.Drawing.Size(241, 28)
		Me.lblDataName.TabIndex = 0
		Me.lblDataName.Text = "Rated Air Vol Flow Rate:"
		Me.lblDataName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
		Me.TableLayoutPanel1.ColumnCount = 2
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.18073!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.81928!))
		Me.TableLayoutPanel1.Controls.Add(Me.txtDataValue, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.cboDataUnits, 1, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.lblDataName, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.lblInfoHover, 0, 1)
		Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 2
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(355, 59)
		Me.TableLayoutPanel1.TabIndex = 14
		'
		'txtDataValue
		'
		Me.txtDataValue.Dock = System.Windows.Forms.DockStyle.Fill
		Me.txtDataValue.Location = New System.Drawing.Point(252, 4)
		Me.txtDataValue.Name = "txtDataValue"
		Me.txtDataValue.Size = New System.Drawing.Size(99, 20)
		Me.txtDataValue.TabIndex = 2
		Me.txtDataValue.Text = "0.1887"
		Me.txtDataValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'lblInfoHover
		'
		Me.lblInfoHover.AutoSize = True
		Me.lblInfoHover.BackColor = System.Drawing.Color.LightGoldenrodYellow
		Me.lblInfoHover.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblInfoHover.Location = New System.Drawing.Point(4, 30)
		Me.lblInfoHover.Name = "lblInfoHover"
		Me.lblInfoHover.Size = New System.Drawing.Size(241, 28)
		Me.lblInfoHover.TabIndex = 1
		Me.lblInfoHover.Text = "Hover here for information about this value..."
		Me.lblInfoHover.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'GeneralDetailedFormEntry
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.MinimumSize = New System.Drawing.Size(355, 55)
		Me.Name = "GeneralDetailedFormEntry"
		Me.Size = New System.Drawing.Size(355, 59)
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents lblDataName As System.Windows.Forms.Label
	Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents lblInfoHover As System.Windows.Forms.Label
	Public WithEvents cboDataUnits As System.Windows.Forms.ComboBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtDataValue As System.Windows.Forms.TextBox

End Class
