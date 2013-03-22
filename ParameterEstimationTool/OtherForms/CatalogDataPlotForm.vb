Public Class CatalogDataPlotForm

	Public Sub MakePlots(ByVal FullDataSet As Single(,))

		For ColCounter As Integer = 0 To FullDataSet.GetUpperBound(0)

			Dim PlotSeries As New Collections.Generic.List(Of PublicData.PlotSeriesData)
			Dim Data As New List(Of Double)
			For DataVal As Integer = 0 To FullDataSet.GetUpperBound(1)
				Data.Add(FullDataSet(ColCounter, DataVal))
			Next
			PlotSeries.Add(New PublicData.PlotSeriesData(PublicData.GetHeaderStrings(ColCounter), PlotTypeEnum.PointPlot, Color.Red, Data.ToArray))

			Dim MyPlotData As New PublicData.PlotData("Catalog Data Display", "-", PlotSeries)
			Dim ThisPlot As New NPlot.Windows.PlotSurface2D
			AddTabPageAndPlotSurface(PublicData.GetHeaderStrings(ColCounter), ThisPlot)
			Plotting_Operations.MakeAPlot(MyPlotData.ChartTitle, MyPlotData.YLabel, MyPlotData.SeriesData, ThisPlot)
			ThisPlot.Refresh()

		Next

	End Sub

	Private Sub AddTabPageAndPlotSurface(ByVal TabText As String, ByRef PlotSurfaceAdded As NPlot.Windows.PlotSurface2D)

		Dim NewTabPage As New TabPage
		With NewTabPage
			.Text = TabText
		End With

		PlotSurfaceAdded.Dock = DockStyle.Fill
		NewTabPage.Controls.Add(PlotSurfaceAdded)

		Me.TabControl1.TabPages.Add(NewTabPage)

	End Sub

End Class