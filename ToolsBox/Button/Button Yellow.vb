Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Yellow), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Yellow
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
                DrawGradient(Color.FromArgb(160, 160, 0), Color.FromArgb(162, 154, 18), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(162, 154, 18), Color.FromArgb(237, 225, 25), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(237, 225, 25), Color.FromArgb(162, 154, 18), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(160, 160, 0), Color.FromArgb(162, 154, 18), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(162, 154, 18), Color.FromArgb(237, 225, 25), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(237, 225, 25), Color.FromArgb(162, 154, 18), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(160, 160, 0), Color.FromArgb(244, 234, 68), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(244, 234, 68), Color.FromArgb(237, 225, 25), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(237, 225, 25), Color.FromArgb(244, 234, 68), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class

