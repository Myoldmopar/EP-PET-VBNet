Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Runtime.ConstrainedExecution
Imports Microsoft.VisualBasic.VBMath

Public Class PlotForm

	Private LocalCollection As Collections.Generic.List(Of Double())
	Private LocalSeriesNames As Collections.Generic.List(Of String)

	Private Sub CopyImageToClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImageToClipboard.Click
		'TODO: FIX THIS
		'Clipboard.SetImage(PictureBox1.Image)
	End Sub

	Private Sub CopyDataToClipboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyDataToClipboard.Click

		Dim sOutput As New System.Text.StringBuilder
		
		'write the first line, just headers
		sOutput.Append("Data Point")
		For Each SeriesName As String In LocalSeriesNames
			sOutput.Append("," & SeriesName)
		Next
		sOutput.AppendLine()

		'then find the max # of data rows
		Dim MaxUpperBound As Integer = 0
		For Each DblArray As Double() In LocalCollection
			MaxUpperBound = Math.Max(MaxUpperBound, DblArray.GetUpperBound(0))
		Next

		For I As Integer = 0 To MaxUpperBound
			sOutput.Append(I)
			For Each DblArray As Double() In LocalCollection
				If I <= DblArray.GetUpperBound(0) Then
					sOutput.Append("," & DblArray(I))
				Else
					sOutput.Append(",")
				End If
			Next
			sOutput.AppendLine()
		Next

		Clipboard.SetText(sOutput.ToString)

	End Sub

End Class
