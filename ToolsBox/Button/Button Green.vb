Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(Button_Green), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Green
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
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(4, 128, 7), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(4, 128, 7), Color.FromArgb(17, 196, 21), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(17, 196, 21), Color.FromArgb(4, 128, 7), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(4, 128, 7), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(4, 128, 7), Color.FromArgb(17, 196, 21), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(17, 196, 21), Color.FromArgb(4, 128, 7), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(22, 234, 27), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(22, 234, 27), Color.FromArgb(17, 196, 21), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(17, 196, 21), Color.FromArgb(22, 234, 27), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class
