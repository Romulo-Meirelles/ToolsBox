Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Gray), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Gray
    Inherits ThemeControl
    Sub New()
        Size = New Size(90, 15)
        MinimumSize = New Size(14, 14)
        MaximumSize = New Size(15, 15)
        Cursor = Cursors.Hand
    End Sub
    Overrides Sub PaintHook()


        Select Case MouseState
            Case State.MouseNone
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(80, 80, 80), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(80, 80, 80), Color.FromArgb(140, 140, 140), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(140, 140, 140), Color.FromArgb(80, 80, 80), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(80, 80, 80), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(80, 80, 80), Color.FromArgb(140, 140, 140), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(140, 140, 140), Color.FromArgb(80, 80, 80), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(160, 160, 160), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(160, 160, 160), Color.FromArgb(130, 130, 130), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(130, 130, 130), Color.FromArgb(160, 160, 160), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class
