Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Dark), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Dark
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
                DrawGradient(Color.FromArgb(30, 30, 30), Color.FromArgb(50, 50, 50), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(130, 130, 130), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(130, 130, 130), Color.FromArgb(50, 50, 50), 4, 4, 7, 7, 90S)
                DrawBorders(New Pen(Color.FromArgb(105, 105, 105)), Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(30, 30, 30), Color.FromArgb(50, 50, 50), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(50, 50, 50), Color.FromArgb(130, 130, 130), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(130, 130, 130), Color.FromArgb(50, 50, 50), 4, 4, 7, 7, 90S)
                DrawBorders(New Pen(Color.FromArgb(105, 105, 105)), Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(30, 30, 30), Color.FromArgb(160, 160, 160), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(160, 160, 160), Color.FromArgb(130, 130, 130), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(130, 130, 130), Color.FromArgb(160, 160, 160), 4, 4, 7, 7, 90S)
                DrawBorders(New Pen(Color.FromArgb(105, 105, 105)), Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class
