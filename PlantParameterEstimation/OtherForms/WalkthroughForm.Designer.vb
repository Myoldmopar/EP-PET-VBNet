<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WalkthroughForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WalkthroughForm))
		Me.TabControl1 = New System.Windows.Forms.TabControl
		Me.TabPage1 = New System.Windows.Forms.TabPage
		Me.Label1 = New System.Windows.Forms.Label
		Me.TabPage2 = New System.Windows.Forms.TabPage
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.PictureBox1 = New System.Windows.Forms.PictureBox
		Me.TabPage3 = New System.Windows.Forms.TabPage
		Me.Label4 = New System.Windows.Forms.Label
		Me.PictureBox2 = New System.Windows.Forms.PictureBox
		Me.TabPage6 = New System.Windows.Forms.TabPage
		Me.Label8 = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.TabPage4 = New System.Windows.Forms.TabPage
		Me.Label5 = New System.Windows.Forms.Label
		Me.TabPage5 = New System.Windows.Forms.TabPage
		Me.Label6 = New System.Windows.Forms.Label
		Me.btnBack = New System.Windows.Forms.Button
		Me.btnNext = New System.Windows.Forms.Button
		Me.btnDone = New System.Windows.Forms.Button
		Me.Label9 = New System.Windows.Forms.Label
		Me.PictureBox4 = New System.Windows.Forms.PictureBox
		Me.TabControl1.SuspendLayout()
		Me.TabPage1.SuspendLayout()
		Me.TabPage2.SuspendLayout()
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TabPage3.SuspendLayout()
		CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TabPage6.SuspendLayout()
		Me.TabPage4.SuspendLayout()
		Me.TabPage5.SuspendLayout()
		CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TabControl1
		'
		Me.TabControl1.Controls.Add(Me.TabPage1)
		Me.TabControl1.Controls.Add(Me.TabPage2)
		Me.TabControl1.Controls.Add(Me.TabPage3)
		Me.TabControl1.Controls.Add(Me.TabPage6)
		Me.TabControl1.Controls.Add(Me.TabPage4)
		Me.TabControl1.Controls.Add(Me.TabPage5)
		Me.TabControl1.Location = New System.Drawing.Point(21, 16)
		Me.TabControl1.Name = "TabControl1"
		Me.TabControl1.SelectedIndex = 0
		Me.TabControl1.Size = New System.Drawing.Size(643, 279)
		Me.TabControl1.TabIndex = 0
		'
		'TabPage1
		'
		Me.TabPage1.Controls.Add(Me.Label1)
		Me.TabPage1.Location = New System.Drawing.Point(4, 22)
		Me.TabPage1.Name = "TabPage1"
		Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage1.Size = New System.Drawing.Size(635, 253)
		Me.TabPage1.TabIndex = 0
		Me.TabPage1.Text = "Introduction"
		Me.TabPage1.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(182, 71)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(282, 106)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "This walkthrough will help you get comfortable with taking manufacturer's data an" & _
			"d using this program to develop parameters and input for EnergyPlus.  Click Next" & _
			" to get started."
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'TabPage2
		'
		Me.TabPage2.Controls.Add(Me.Label3)
		Me.TabPage2.Controls.Add(Me.Label2)
		Me.TabPage2.Controls.Add(Me.PictureBox1)
		Me.TabPage2.Location = New System.Drawing.Point(4, 22)
		Me.TabPage2.Name = "TabPage2"
		Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.TabPage2.Size = New System.Drawing.Size(635, 253)
		Me.TabPage2.TabIndex = 1
		Me.TabPage2.Text = "What Data is Required?"
		Me.TabPage2.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(492, 34)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(111, 173)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "Typical data will include tabular fluid properties or heat transfer characteristi" & _
			"cs, along with any correction factor data."
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label2
		'
		Me.Label2.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(34, 34)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(111, 173)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "The data required will vary based on the equipment type, however, this program is" & _
			" intended to use readily available data from manufacturers."
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'PictureBox1
		'
		Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
		Me.PictureBox1.Location = New System.Drawing.Point(178, 18)
		Me.PictureBox1.Name = "PictureBox1"
		Me.PictureBox1.Size = New System.Drawing.Size(281, 216)
		Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.PictureBox1.TabIndex = 0
		Me.PictureBox1.TabStop = False
		'
		'TabPage3
		'
		Me.TabPage3.Controls.Add(Me.Label4)
		Me.TabPage3.Controls.Add(Me.PictureBox2)
		Me.TabPage3.Location = New System.Drawing.Point(4, 22)
		Me.TabPage3.Name = "TabPage3"
		Me.TabPage3.Size = New System.Drawing.Size(635, 253)
		Me.TabPage3.TabIndex = 2
		Me.TabPage3.Text = "Getting Data Into Program"
		Me.TabPage3.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(471, 19)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(161, 210)
		Me.Label4.TabIndex = 3
		Me.Label4.Text = resources.GetString("Label4.Text")
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'PictureBox2
		'
		Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
		Me.PictureBox2.Location = New System.Drawing.Point(3, 19)
		Me.PictureBox2.Name = "PictureBox2"
		Me.PictureBox2.Size = New System.Drawing.Size(462, 210)
		Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.PictureBox2.TabIndex = 0
		Me.PictureBox2.TabStop = False
		'
		'TabPage6
		'
		Me.TabPage6.Controls.Add(Me.Label8)
		Me.TabPage6.Controls.Add(Me.Label7)
		Me.TabPage6.Location = New System.Drawing.Point(4, 22)
		Me.TabPage6.Name = "TabPage6"
		Me.TabPage6.Size = New System.Drawing.Size(635, 253)
		Me.TabPage6.TabIndex = 5
		Me.TabPage6.Text = "Processing Input"
		Me.TabPage6.UseVisualStyleBackColor = True
		'
		'Label8
		'
		Me.Label8.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label8.Location = New System.Drawing.Point(332, 16)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(282, 222)
		Me.Label8.TabIndex = 2
		Me.Label8.Text = resources.GetString("Label8.Text")
		Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label7
		'
		Me.Label7.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Location = New System.Drawing.Point(16, 16)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(282, 222)
		Me.Label7.TabIndex = 1
		Me.Label7.Text = resources.GetString("Label7.Text")
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'TabPage4
		'
		Me.TabPage4.Controls.Add(Me.Label5)
		Me.TabPage4.Location = New System.Drawing.Point(4, 22)
		Me.TabPage4.Name = "TabPage4"
		Me.TabPage4.Size = New System.Drawing.Size(635, 253)
		Me.TabPage4.TabIndex = 3
		Me.TabPage4.Text = "Running A Simulation"
		Me.TabPage4.UseVisualStyleBackColor = True
		'
		'Label5
		'
		Me.Label5.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(247, 40)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(140, 173)
		Me.Label5.TabIndex = 3
		Me.Label5.Text = "After some specifics about your particular component have been entered, the simul" & _
			"ation will be ready to run.  The simulation time and results will depend on your" & _
			" component type."
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'TabPage5
		'
		Me.TabPage5.Controls.Add(Me.PictureBox4)
		Me.TabPage5.Controls.Add(Me.Label9)
		Me.TabPage5.Controls.Add(Me.Label6)
		Me.TabPage5.Location = New System.Drawing.Point(4, 22)
		Me.TabPage5.Name = "TabPage5"
		Me.TabPage5.Size = New System.Drawing.Size(635, 253)
		Me.TabPage5.TabIndex = 4
		Me.TabPage5.Text = "Handling Results"
		Me.TabPage5.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Location = New System.Drawing.Point(13, 16)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(238, 173)
		Me.Label6.TabIndex = 3
		Me.Label6.Text = resources.GetString("Label6.Text")
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnBack
		'
		Me.btnBack.Location = New System.Drawing.Point(156, 303)
		Me.btnBack.Name = "btnBack"
		Me.btnBack.Size = New System.Drawing.Size(98, 23)
		Me.btnBack.TabIndex = 1
		Me.btnBack.Text = "<< Back"
		Me.btnBack.UseVisualStyleBackColor = True
		'
		'btnNext
		'
		Me.btnNext.Location = New System.Drawing.Point(296, 303)
		Me.btnNext.Name = "btnNext"
		Me.btnNext.Size = New System.Drawing.Size(98, 23)
		Me.btnNext.TabIndex = 2
		Me.btnNext.Text = "Next >>"
		Me.btnNext.UseVisualStyleBackColor = True
		'
		'btnDone
		'
		Me.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnDone.Location = New System.Drawing.Point(436, 303)
		Me.btnDone.Name = "btnDone"
		Me.btnDone.Size = New System.Drawing.Size(98, 23)
		Me.btnDone.TabIndex = 3
		Me.btnDone.Text = "Done"
		Me.btnDone.UseVisualStyleBackColor = True
		'
		'Label9
		'
		Me.Label9.BackColor = System.Drawing.Color.WhiteSmoke
		Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Location = New System.Drawing.Point(3, 210)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(629, 41)
		Me.Label9.TabIndex = 5
		Me.Label9.Text = "In addition, 1 or 2 plots will be produced, showing the quality of the match betw" & _
			"een manufacturer's data points and the predicted points based on generated param" & _
			"eters or curve fits."
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'PictureBox4
		'
		Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
		Me.PictureBox4.Location = New System.Drawing.Point(262, 0)
		Me.PictureBox4.Name = "PictureBox4"
		Me.PictureBox4.Size = New System.Drawing.Size(370, 207)
		Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
		Me.PictureBox4.TabIndex = 6
		Me.PictureBox4.TabStop = False
		'
		'WalkthroughForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(690, 337)
		Me.Controls.Add(Me.btnDone)
		Me.Controls.Add(Me.btnNext)
		Me.Controls.Add(Me.btnBack)
		Me.Controls.Add(Me.TabControl1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "WalkthroughForm"
		Me.Text = "Program Walkthrough"
		Me.TabControl1.ResumeLayout(False)
		Me.TabPage1.ResumeLayout(False)
		Me.TabPage2.ResumeLayout(False)
		CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TabPage3.ResumeLayout(False)
		CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TabPage6.ResumeLayout(False)
		Me.TabPage4.ResumeLayout(False)
		Me.TabPage5.ResumeLayout(False)
		CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
	Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
	Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
	Friend WithEvents btnBack As System.Windows.Forms.Button
	Friend WithEvents btnNext As System.Windows.Forms.Button
	Friend WithEvents btnDone As System.Windows.Forms.Button
	Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
	Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
End Class
