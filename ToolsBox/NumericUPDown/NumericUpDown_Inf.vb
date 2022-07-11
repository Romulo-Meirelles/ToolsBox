Imports System, System.IO, System.Collections.Generic, System.Runtime.InteropServices, System.ComponentModel
Imports System.Drawing, System.Drawing.Drawing2D, System.Drawing.Imaging, System.Windows.Forms
Imports ToolsBox.Controller
Imports ToolsBox.Utils

<ToolboxBitmap(GetType(NumericUpDown), "NumericUpDown")> _
Public Class NumericUpDown_Inf
    Inherits Control

    Private State As New MouseState
    Private X As Integer
    Private Y As Integer
    Private _Value As Long
    Private _Max As Long
    Private _Min As Long
    Private Typing As Boolean
    Private _Border As Boolean
    Private _RoundedBorder As Integer
    Private _TriangleDown As Color
    Private _TriangleUp As Color
    Private _BackColor As Color
    Private _ShadowColor As Color

    <Category("ToolsBox Herramienta")> _
    Public Property Value As Long
        Get
            Return _Value
        End Get
        Set(ByVal V As Long)
            If V <= _Max And V >= _Min Then _Value = V
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Maximum As Long
        Get
            Return _Max
        End Get
        Set(ByVal V As Long)
            If V > _Min Then _Max = V
            If _Value > _Max Then _Value = _Max
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Minimum As Long
        Get
            Return _Min
        End Get
        Set(ByVal V As Long)
            If V < _Max Then _Min = V
            If _Value < _Min Then _Value = _Min
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Border As Boolean
        Get
            Return _Border
        End Get
        Set(ByVal Value As Boolean)
            _Border = Value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Integer
        Get
            Return _RoundedBorder
        End Get
        Set(ByVal Value As Integer)
            If Value > 50 Then _RoundedBorder = 50 Else _RoundedBorder = Value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property TriangleUp As Color
        Get
            Return _TriangleUp
        End Get
        Set(ByVal Value As Color)
            _TriangleUp = Value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property TriangleDown As Color
        Get
            Return _TriangleDown
        End Get
        Set(ByVal Value As Color)
            _TriangleDown = Value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property [BackColor] As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal Value As Color)
            _BackColor = Value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ShadowColor As Color
        Get
            Return _ShadowColor
        End Get
        Set(ByVal Value As Color)
            _ShadowColor = Value
            Invalidate()
        End Set
    End Property

    Sub New()
        _Max = 100
        _Min = 0
        _Border = True
        _RoundedBorder = 0
        _TriangleDown = Color.White
        _TriangleUp = Color.White
        _BackColor = Color.FromArgb(125, 78, 75, 73)
        _ShadowColor = Color.FromArgb(125, 61, 59, 55)
        Cursor = Cursors.IBeam
        SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        G.Clear(BackColor)


        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Color.FromArgb(20, 20, 20))

        Dim g1 As New LinearGradientBrush(ClientRectangle, _BackColor, _ShadowColor, 90S)
        G.FillPath(g1, RoundRect(New Rectangle(0, 0, Width - 1, Height - 2), 2))

        Dim h1 As New HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(100, 31, 31, 31), Color.FromArgb(100, 36, 36, 36))
        G.FillPath(h1, RoundRect(New Rectangle(0, 0, Width - 1, Height - 2), 2))

        Dim s1 As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height / 2), Color.FromArgb(35, Color.White), Color.FromArgb(0, Color.White), 90S)

        G.FillPath(s1, RoundRect(New Rectangle(0, 0, Width - 1, Height / 2 - 1), 2))

        'Border
        If _Border = True Then
            G.DrawPath(New Pen(Color.FromArgb(150, 97, 94, 90)), RoundRect(New Rectangle(0, 1, Width - 1, Height - 3), 2))
            G.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(New Rectangle(0, 0, Width - 1, Height - 1), 2))
        Else
        End If


        ''Separator Lines
        G.DrawLine(New Pen(Color.FromArgb(10, 10, 10)), New Point(Width - 21, 0), New Point(Width - 21, Height))
        G.DrawLine(New Pen(Color.FromArgb(70, 70, 70)), New Point(Width - 20, 1), New Point(Width - 20, Height - 3))
        G.DrawLine(New Pen(Color.FromArgb(10, 10, 10)), New Point(Width - 20, 10), New Point(Width, 10))
        G.DrawLine(New Pen(Color.FromArgb(70, 70, 70)), New Point(Width - 19, 11), New Point(Width - 3, 11))

        'Top Triangle
        DrawTriangle(_TriangleDown, New Point(Width - 14, 14), New Point(Width - 7, 14), New Point(Width - 11, 17.5), G)

        'Bottom Triangle
        DrawTriangle(_TriangleUp, New Point(Width - 14, 7), New Point(Width - 7, 7), New Point(Width - 11, 3), G)

        G.DrawString(Value, Font, Brushes.White, New Point(5, 4))

        'Border
        If _Border = True Then
            G.DrawRectangle(New Pen(Color.FromArgb(70, 70, 70)), New Rectangle(1, 1, Width - 3, Height - 3))
            G.DrawRectangle(New Pen(Color.FromArgb(30, 30, 30)), New Rectangle(0, 0, Width - 1, Height - 1))
        Else
        End If

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
        CarregaBordas(Me, _RoundedBorder, _RoundedBorder)

    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.Location.X
        Y = e.Location.Y
        Invalidate()
        If e.X < Width - 23 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Default
    End Sub
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Me.Height = 22
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        If X > Me.Width - 21 Then
            If Y < 10 Then
                If (Value + 1) <= _Max Then _Value += 1
            Else
                If (Value - 1) >= _Min Then _Value -= 1
            End If
        Else
            Typing = Not Typing
            Focus()
        End If
        Invalidate()
    End Sub
    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        Try
            If Typing Then _Value = CStr(CStr(_Value) & e.KeyChar.ToString)
        Catch ex As Exception : End Try
    End Sub
    Protected Overrides Sub OnKeyup(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyUp(e)
        If e.KeyCode = Keys.Up Then
            If (Value + 1) <= _Max Then _Value += 1
            Invalidate()
        ElseIf e.KeyCode = Keys.Down Then
            If (Value - 1) >= _Min Then _Value -= 1
        End If
        Invalidate()
    End Sub
    Protected Sub DrawTriangle(ByVal Clr As Color, ByVal FirstPoint As Point, ByVal SecondPoint As Point, ByVal ThirdPoint As Point, ByVal G As Graphics)
        Dim points As New List(Of Point)()
        points.Add(FirstPoint)
        points.Add(SecondPoint)
        points.Add(ThirdPoint)
        G.FillPolygon(New SolidBrush(Clr), points.ToArray)
    End Sub
End Class