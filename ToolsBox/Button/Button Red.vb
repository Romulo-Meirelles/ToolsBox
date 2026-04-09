Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(Button_Red), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Red
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
                DrawGradient(Color.FromArgb(160, 0, 0), Color.FromArgb(109, 16, 16), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(109, 16, 16), Color.FromArgb(212, 20, 20), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(212, 20, 20), Color.FromArgb(109, 16, 16), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(Color.FromArgb(160, 0, 0), Color.FromArgb(109, 16, 16), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(109, 16, 16), Color.FromArgb(212, 20, 20), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(212, 20, 20), Color.FromArgb(109, 16, 16), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(Color.FromArgb(160, 0, 0), Color.FromArgb(249, 50, 50), 0, 0, 15, 15, 90S)
                DrawGradient(Color.FromArgb(249, 50, 50), Color.FromArgb(212, 20, 20), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(212, 20, 20), Color.FromArgb(249, 50, 50), 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class



