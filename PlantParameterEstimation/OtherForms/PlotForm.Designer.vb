﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlotForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlotForm))
		Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.CopyImageToClipboard = New System.Windows.Forms.ToolStripMenuItem
		Me.CopyDataToClipboard = New System.Windows.Forms.ToolStripMenuItem
		Me.PlotSurface2D1 = New NPlot.Windows.PlotSurface2D
		Me.ContextMenuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ContextMenuStrip1
		'
		Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyImageToClipboard, Me.CopyDataToClipboard})
		Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
		Me.ContextMenuStrip1.Size = New System.Drawing.Size(205, 48)
		'
		'CopyImageToClipboard
		'
		Me.CopyImageToClipboard.Name = "CopyImageToClipboard"
		Me.CopyImageToClipboard.Size = New System.Drawing.Size(204, 22)
		Me.CopyImageToClipboard.Text = "Copy Image to Clipboard"
		'
		'CopyDataToClipboard
		'
		Me.CopyDataToClipboard.Name = "CopyDataToClipboard"
		Me.CopyDataToClipboard.Size = New System.Drawing.Size(204, 22)
		Me.CopyDataToClipboard.Text = "Copy Data to Clipboard"
		'
		'PlotSurface2D1
		'
		Me.PlotSurface2D1.AutoScaleAutoGeneratedAxes = False
		Me.PlotSurface2D1.AutoScaleTitle = False
		Me.PlotSurface2D1.BackColor = System.Drawing.SystemColors.ControlLightLight
		Me.PlotSurface2D1.DateTimeToolTip = False
		Me.PlotSurface2D1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.PlotSurface2D1.Legend = Nothing
		Me.PlotSurface2D1.LegendZOrder = -1
		Me.PlotSurface2D1.Location = New System.Drawing.Point(0, 0)
		Me.PlotSurface2D1.Name = "PlotSurface2D1"
		Me.PlotSurface2D1.RightMenu = Nothing
		Me.PlotSurface2D1.ShowCoordinates = True
		Me.PlotSurface2D1.Size = New System.Drawing.Size(664, 379)
		Me.PlotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None
		Me.PlotSurface2D1.TabIndex = 1
		Me.PlotSurface2D1.Text = "PlotSurface2D1"
		Me.PlotSurface2D1.Title = ""
		Me.PlotSurface2D1.TitleFont = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
		Me.PlotSurface2D1.XAxis1 = Nothing
		Me.PlotSurface2D1.XAxis2 = Nothing
		Me.PlotSurface2D1.YAxis1 = Nothing
		Me.PlotSurface2D1.YAxis2 = Nothing
		'
		'PlotForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(664, 379)
		Me.ContextMenuStrip = Me.ContextMenuStrip1
		Me.Controls.Add(Me.PlotSurface2D1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(415, 238)
		Me.Name = "PlotForm"
		Me.Text = "Results Comparison"
		Me.ContextMenuStrip1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Friend WithEvents CopyImageToClipboard As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents CopyDataToClipboard As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents PlotSurface2D1 As NPlot.Windows.PlotSurface2D
End Class
