Imports System.Drawing, System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Seven
    Inherits ThemeControl154
    Private X As Integer
    Protected Overrides Sub ColorHook()
    End Sub

    Private _BorderOut As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BorderOut() As Color
        Get
            Return _BorderOut
        End Get
        Set(ByVal value As Color)
            _BorderOut = value
            Invalidate()
        End Set
    End Property

    Private _BorderInside As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BorderInside() As Color
        Get
            Return _BorderInside
        End Get
        Set(ByVal value As Color)
            _BorderInside = value
            Invalidate()
        End Set
    End Property

    Private _ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property

    Private _BackColorOne As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorOne() As Color
        Get
            Return _BackColorOne
        End Get
        Set(ByVal value As Color)
            _BackColorOne = value
            Invalidate()
        End Set
    End Property

    Private _BackColorTwo As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorTwo() As Color
        Get
            Return _BackColorTwo
        End Get
        Set(ByVal value As Color)
            _BackColorTwo = value
            Invalidate()
        End Set
    End Property

    Private _GlassyColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property GlassyColor() As Color
        Get
            Return _GlassyColor
        End Get
        Set(ByVal value As Color)
            _GlassyColor = value
            Invalidate()
        End Set
    End Property

    Private _GlassyEffect As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property GlassyEffect() As Boolean
        Get
            Return _GlassyEffect
        End Get
        Set(ByVal value As Boolean)
            _GlassyEffect = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetColor("Background", Color.FromArgb(64, 64, 64))
        SetColor("Edge color", Color.Black)
        SetColor("Button edge color", Color.FromArgb(90, 90, 90))
        Me.Size = New Size(71, 19)
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        _BackColorOne = GetColor("Background")
        _BackColorTwo = Color.FromArgb(30, 30, 30)
        _ButtonColor = Color.White
        _BorderOut = GetColor("Edge color")
        _BorderInside = GetColor("Button edge color")
        _GlassyColor = Color.Black
        _GlassyEffect = True
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
        G.Clear(_BorderOut)

        'Fill buttons
        Dim SB As New LinearGradientBrush(New Rectangle(New Point(1, 1), New Size(Width - 2, Height - 2)), _BackColorOne, _BackColorTwo, 90.0F)
        G.FillRectangle(SB, New Rectangle(New Point(1, 1), New Size(Width - 2, Height - 2)))

        'Draw icons
        G.DrawString("0", New Font("Marlett", 8.25), New SolidBrush(_ButtonColor), New Point(5, 5))
        If FindForm.WindowState <> FormWindowState.Maximized Then G.DrawString("1", New Font("Marlett", 8.25), New SolidBrush(_ButtonColor), New Point(27, 4)) Else G.DrawString("2", New Font("Marlett", 8.25), New SolidBrush(_ButtonColor), New Point(27, 4))
        G.DrawString("r", New Font("Marlett", 10), New SolidBrush(_ButtonColor), New Point(49, 3))


        If _GlassyEffect Then
            'Glassy effect
            Dim CBlend As New ColorBlend(2)
            CBlend.Colors = {Color.FromArgb(100, _GlassyColor), Color.Transparent}
            CBlend.Positions = {0, 1}
            DrawGradient(CBlend, New Rectangle(New Point(1, 8), New Size(68, 8)), 90.0F)
        End If

        'Draw button outlines
        G.DrawRectangle(New Pen(_BorderInside), New Rectangle(New Point(1, 1), New Size(20, 16)))
        G.DrawRectangle(New Pen(_BorderInside), New Rectangle(New Point(23, 1), New Size(20, 16)))
        G.DrawRectangle(New Pen(_BorderInside), New Rectangle(New Point(45, 1), New Size(24, 16)))

        'Mouse states
        Select Case State
            Case MouseState.Over
                If X <= 22 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.White)), New Rectangle(New Point(1, 1), New Size(21, Height - 2)))
                ElseIf X > 22 And X <= 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.White)), New Rectangle(New Point(23, 1), New Size(21, Height - 2)))
                ElseIf X > 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.White)), New Rectangle(New Point(45, 1), New Size(25, Height - 2)))
                End If
            Case MouseState.Down
                If X <= 22 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(New Point(1, 1), New Size(21, Height - 2)))
                ElseIf X > 22 And X <= 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(New Point(23, 1), New Size(21, Height - 2)))
                ElseIf X > 44 Then
                    G.FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), New Rectangle(New Point(45, 1), New Size(25, Height - 2)))
                End If
        End Select
    End Sub
End Class






