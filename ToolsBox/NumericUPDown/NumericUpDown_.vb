Imports System.Drawing.Drawing2D, System.ComponentModel, System.Windows.Forms
Imports ToolsBox.Controller
Imports ToolsBox.Utils

<ToolboxBitmap(GetType(NumericUpDown), "NumericUpDown")> _
Public Class NumericUpDown_
    Inherits Control

    Private W, H As Integer
    Private State As MouseState = MouseState.None
    Private x, y As Integer
    Private _Value, _Min, _Max As Long
    Private Bool As Boolean
    Friend G As Graphics, B As Bitmap
    Friend _FlatColor As Color = Color.FromArgb(35, 168, 109)
    Friend NearSF As New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near}
    Friend CenterSF As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _ButtonColor As Color = _FlatColor
    Private _NumericColor As Color = Color.White
    Private _PlusColor As Color = Color.White
    Private _NegativeColor As Color = Color.White
    Private _RoundedBorder As Integer = 0

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
        ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
        ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 10)
        BackColor = Color.FromArgb(60, 70, 73)
        ForeColor = Color.White
        _Min = 0
        _Max = 100
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property Value As Long
        Get
            Return _Value
        End Get
        Set(value As Long)
            If value <= _Max And value >= _Min Then _Value = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Integer
        Get
            Return _RoundedBorder
        End Get
        Set(value As Integer)

            If value > 50 Then _RoundedBorder = 50 Else _RoundedBorder = value

            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Maximum As Long
        Get
            Return _Max
        End Get
        Set(value As Long)
            If value > _Min Then _Max = value
            If _Value > _Max Then _Value = _Max
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Minimum As Long
        Get
            Return _Min
        End Get
        Set(value As Long)
            If value < _Max Then _Min = value
            If _Value < _Min Then _Value = Minimum
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor As Color
        Get
            Return _ButtonColor
        End Get
        Set(value As Color)
            _ButtonColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property NumericColor As Color
        Get
            Return _NumericColor
        End Get
        Set(value As Color)
            _NumericColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property PlusColor As Color
        Get
            Return _PlusColor
        End Get
        Set(value As Color)
            _PlusColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property NegativeColor As Color
        Get
            Return _NegativeColor
        End Get
        Set(value As Color)
            _NegativeColor = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)
            .FillRectangle(New SolidBrush(_ButtonColor), New Rectangle(Width - 24, 0, 24, H))

            '-- Add
            .DrawString("+", New Font("Segoe UI", 12), New SolidBrush(_PlusColor), New Point(Width - 12, 8), CenterSF)
            '-- Subtract
            .DrawString("-", New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(_NegativeColor), New Point(Width - 12, 22), CenterSF)

            '-- Text
            .DrawString(Value, Font, New SolidBrush(_NumericColor), New Rectangle(5, 1, W, H), New StringFormat() With {.LineAlignment = StringAlignment.Center})
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
        CarregaBordas(Me, _RoundedBorder, _RoundedBorder)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.Location.X
        y = e.Location.Y
        Invalidate()
        If e.X < Width - 23 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If x > Width - 21 AndAlso x < Width - 3 Then
            If y < 15 Then
                If (Value + 1) <= _Max Then _Value += 1
            Else
                If (Value - 1) >= _Min Then _Value -= 1
            End If
        Else
            Bool = Not Bool
            Focus()
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        Try
            If Bool Then _Value = CStr(CStr(_Value) & e.KeyChar.ToString())
            If _Value > _Max Then _Value = _Max
            Invalidate()
        Catch : End Try
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Back Then
            Value = 0
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 30
    End Sub
End Class
