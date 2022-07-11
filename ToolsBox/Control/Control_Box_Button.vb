Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Red
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Green
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Yellow
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Blue
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Dark
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Gray
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Light
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
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Button_Custom
    Inherits ThemeControl

    Private _BackColorOne As Color
    Public Property BackColorOne() As Color
        Get
            Return _BackColorOne
        End Get
        Set(ByVal v As Color)
            _BackColorOne = v
            Invalidate()
        End Set
    End Property

    Private _BackColorTwo As Color
    Public Property BackColorTwo() As Color
        Get
            Return _BackColorTwo
        End Get
        Set(ByVal v As Color)
            _BackColorTwo = v
            Invalidate()
        End Set
    End Property

    Sub New()
        Size = New Size(90, 15)
        MinimumSize = New Size(14, 14)
        MaximumSize = New Size(15, 15)
        Cursor = Cursors.Hand
        _BackColorOne = Color.White
        _BackColorTwo = Color.Gray
    End Sub
    Overrides Sub PaintHook()


        Select Case MouseState
            Case State.MouseNone
                DrawGradient(_BackColorOne, _BackColorTwo, 0, 0, 15, 15, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 3, 3, 9, 9, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseDown
                DrawGradient(_BackColorOne, _BackColorTwo, 0, 0, 15, 15, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 3, 3, 9, 9, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
            Case State.MouseOver
                DrawGradient(_BackColorOne, _BackColorTwo, 0, 0, 15, 15, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 3, 3, 9, 9, 90S)
                DrawGradient(_BackColorOne, _BackColorTwo, 4, 4, 7, 7, 90S)
                DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 15, 15))
        End Select
        Me.Cursor = Cursors.Hand

    End Sub
End Class


