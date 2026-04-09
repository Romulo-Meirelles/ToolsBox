Imports System.ComponentModel

<ToolboxBitmap(GetType(Button_Custom), "Red.ico")>
<DesignTimeVisible(True)>
Public Class Button_Custom
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
