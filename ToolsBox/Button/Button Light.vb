Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Light), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Light
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
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(150, 150, 150), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(150, 150, 150), Color.FromArgb(230, 230, 230), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(230, 230, 230), Color.FromArgb(150, 150, 150), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(150, 150, 150), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(150, 150, 150), Color.FromArgb(230, 230, 230), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(230, 230, 230), Color.FromArgb(150, 150, 150), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(235, 235, 235), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(235, 235, 235), Color.FromArgb(215, 215, 215), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(215, 215, 215), Color.FromArgb(235, 235, 235), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class
