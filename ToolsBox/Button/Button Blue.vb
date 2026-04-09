Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Blue), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Blue
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
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(34, 64, 160), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(34, 64, 160), Color.FromArgb(27, 81, 255), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(27, 81, 255), Color.FromArgb(34, 64, 160), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(34, 64, 160), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(34, 64, 160), Color.FromArgb(27, 81, 255), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(27, 81, 255), Color.FromArgb(34, 64, 160), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(110, 170, 255), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(110, 170, 255), Color.FromArgb(94, 128, 235), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(94, 128, 235), Color.FromArgb(110, 170, 255), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class
