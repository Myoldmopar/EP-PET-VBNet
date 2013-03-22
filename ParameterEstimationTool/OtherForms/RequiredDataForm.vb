Public Class RequiredDataForm

	Private MyArray() As String

	Friend Sub Initialize()
		Me.Label3.Text = "The following data is required: " & vbCrLf
		MyArray = PublicData.GetHeaderStrings
		Select Case PublicData.Sender
			Case SenderType.HPWaterAirHeatingCurveFit
				Me.Label1.Text = "Water-Air Heat Pump Heating Selected"
			Case SenderType.HPWaterAirCoolingCurveFit
				Me.Label1.Text = "Water-Air Heat Pump Cooling Selected"
			Case SenderType.HPWaterWaterHeatingCurveFit
				Me.Label1.Text = "Water-Water Heat Pump Heating Selected"
			Case SenderType.HPWaterWaterCoolingCurveFit
				Me.Label1.Text = "Water-Water Heat Pump Cooling Selected"
			Case Else
				'COMONENT_EXTENSION: Add information here
				Me.Label1.Text = "Unknown equipment selected"
		End Select
		If MyArray Is Nothing Then
			ReDim MyArray(0)
			MyArray(0) = "Data Unavailable"
		End If
		For Each Str As String In MyArray
			Me.Label3.Text &= "  " & Str & vbCrLf
		Next

		Me.Label4.Top = Me.Label3.Bottom + 20
		Me.Label4.Text = "If any data uses correction factors, " & vbCrLf & "  the constant values are entered in tabular form " & vbCrLf & "  and correction factors are separate from tabulated data."

		Me.Button1.Top = Me.Label4.Bottom + 20
		Me.Button2.Top = Me.Button1.Top

		Me.Width = 20 'initialize
		For Each ctl As Control In Me.Controls
			Dim lbl As Label = TryCast(ctl, Label)
			If lbl Is Nothing Then Continue For
			Me.Width = Math.Max(Me.Width, lbl.Width + 30)
		Next
		Me.Width = Math.Max(Me.Width, Button2.Right + 20)

		Me.Height = Me.Button1.Bottom + 45
		Me.MaximumSize = Me.Size
		Me.MinimumSize = Me.Size

	End Sub

	Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
		Dim TempStr As String = MyArray(0)
		For I As Integer = 1 To MyArray.GetUpperBound(0)
			TempStr &= "," & MyArray(I)
		Next
		Clipboard.SetText(TempStr)
	End Sub

End Class