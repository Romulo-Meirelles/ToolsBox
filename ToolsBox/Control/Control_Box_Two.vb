Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Two
    Inherits ThemeControl154
    Private X As Integer
    Dim BG, Edge As Color
    Dim BEdge As Pen
    Private _BackColorReal As Color
    Private _ButtonColor As Color
    Private _ButtonSize As Double()
    Private _MouseOver As Color
    Private _MouseDown As Color

    Protected Overrides Sub ColorHook()
        BG = GetColor("Background")
        Edge = GetColor("Edge color")
        BEdge = New Pen(GetColor("Button edge color"))
    End Sub
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorReal As Color
        Get
            Return _BackColorReal
        End Get
        Set(value As Color)
            _BackColorReal = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ButtonSize As Double()
        Get
            Return _ButtonSize
        End Get
        Set(value As Double())
            _ButtonSize = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor As Color
        Get
            Return _ButtonColor
        End Get
        Set(value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MouseOver As Color
        Get
            Return _MouseOver
        End Get
        Set(value As Color)
            _MouseOver = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MouseDownn As Color
        Get
            Return _MouseDown
        End Get
        Set(value As Color)
            _MouseDown = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetColor("Background", Color.FromArgb(239, 239, 242))
        SetColor("Edge color", Color.Transparent)
        SetColor("Button edge color", Color.Transparent)
        Me.Size = New Size(71, 19)
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _ButtonColor = Color.Black
        _BackColorReal = Color.Transparent
        _ButtonSize = {8.25, 8.25, 10}
        _MouseOver = Color.White
        _MouseDown = Color.Black
    End Sub

    Protected Overrides Sub OnMouseMove(e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.X
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As System.EventArgs)
        MyBase.OnClick(e)
        If X <= 22 Then
            FindForm.WindowState = FormWindowState.Minimized
        ElseIf X > 22 And X <= 44 Then
            If FindForm.WindowState <> FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Maximized Else FindForm.WindowState = FormWindowState.Normal
        ElseIf X > 44 Then
            FindForm.Close()
        End If
    End Sub

    Protected Overrides Sub PaintHook()
        'Draw outer edge
        G.Clear(Color.FromArgb(239, 239, 242))

        'Fill buttons
        G.FillRectangle(New SolidBrush(_BackColorReal), New Rectangle(New Point(1, 1), New Size(Width - 2, Height - 2)))

        'Draw icons
        G.DrawString("0", New Font("Marlett", _ButtonSize(0)), New SolidBrush(_ButtonColor), New Point(5, 5))
        If FindForm.WindowState <> FormWindowState.Maximized Then G.DrawString("1", New Font("Marlett", _ButtonSize(1)), New SolidBrush(_ButtonColor), New Point(27, 4)) Else G.DrawString("2", New Font("Marlett", _ButtonSize(1)), New SolidBrush(_ButtonColor), New Point(27, 4))
        G.DrawString("r", New Font("Marlett", _ButtonSize(2)), New SolidBrush(_ButtonColor), New Point(49, 3))


        'Draw button outlines
        G.DrawRectangle(BEdge, New Rectangle(New Point(1, 1), New Size(20, 16)))
        G.DrawRectangle(BEdge, New Rectangle(New Point(23, 1), New Size(20, 16)))
        G.DrawRectangle(BEdge, New Rectangle(New Point(45, 1), New Size(24, 16)))

        'Mouse states
        Select Case State
            Case MouseState.Over
                If X <= 22 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(40, _MouseOver)), New Rectangle(New Point(1, 1), New Size(21, Height - 2)))
                ElseIf X > 22 And X <= 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(40, _MouseOver)), New Rectangle(New Point(23, 1), New Size(21, Height - 2)))
                ElseIf X > 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(40, _MouseOver)), New Rectangle(New Point(45, 1), New Size(25, Height - 2)))
                End If
            Case MouseState.Down
                If X <= 22 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, _MouseDown)), New Rectangle(New Point(1, 1), New Size(21, Height - 2)))
                ElseIf X > 22 And X <= 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, _MouseDown)), New Rectangle(New Point(23, 1), New Size(21, Height - 2)))
                ElseIf X > 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, _MouseDown)), New Rectangle(New Point(45, 1), New Size(25, Height - 2)))
                End If
        End Select
    End Sub
End Class





