Public Class WalkthroughForm

	Private Sub WalkthroughForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Me.TabControl1.SelectedIndex = 2
		Me.TabControl1.SelectedIndex = 0
		Me.MaximumSize = Me.Size
		Me.MinimumSize = Me.Size
	End Sub

	Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
		If Me.TabControl1.SelectedIndex = 0 Then
			Me.btnBack.Enabled = False
			Me.btnNext.Enabled = True
		ElseIf Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then
			Me.btnBack.Enabled = True
			Me.btnNext.Enabled = False
		Else
			Me.btnBack.Enabled = True
			Me.btnNext.Enabled = True
		End If
	End Sub

	Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
		Me.TabControl1.SelectedIndex -= 1
		Call TabControl1_Click(sender, e)
	End Sub

	Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
		Me.TabControl1.SelectedIndex += 1
		Call TabControl1_Click(sender, e)
	End Sub

	Private Sub btnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
		Me.Dispose()
	End Sub

End Class