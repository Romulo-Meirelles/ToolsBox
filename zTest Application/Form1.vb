Imports ToolsBox
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.DoEvents()
        Dim PanelGradient As New Panel_Gradient
        PanelGradient.Dock = DockStyle.Fill
        PanelGradient.GradientBottomLeft = Color.Black
        PanelGradient.GradientBottomRight = Color.Coral
        PanelGradient.GradientTopLeft = Color.DarkRed
        PanelGradient.GradientTopRight = Color.DarkRed
        Me.Controls.Add(PanelGradient)
        Application.DoEvents()
    End Sub
End Class