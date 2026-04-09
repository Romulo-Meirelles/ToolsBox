Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<ToolboxBitmap(GetType(RatingControl), "WhitePurple.ico")>
<DesignTimeVisible(True)>
Public Class RatingControl
    Inherits Control

    Public Enum RatingShape
        Star
        Square
        Circle
        Triangle
    End Enum

    ' ========= BACKING FIELDS =========

    Private _Maximum As Integer = 5
    Private _Value As Integer = 3
    Private _Spacing As Integer = 4
    Private _BorderThickness As Integer = 1
    Private _InnerRadius As Integer = 2
    Private _OuterRadius As Integer = 15
    Private _Shape As RatingShape = RatingShape.Star
    Private _ReadOnly_ As Boolean = False
    Private _RightClickToClear As Boolean = True

    Private _EmptyFillColor As Color = Color.FromArgb(212, 212, 212)
    Private _EmptyBorderColor As Color = Color.FromArgb(212, 212, 212)
    Private _HoverFillColor As Color = Color.FromArgb(248, 217, 20)
    Private _HoverBorderColor As Color = Color.FromArgb(248, 217, 20)
    Private _RatedFillColor As Color = Color.FromArgb(248, 217, 20)
    Private _RatedBorderColor As Color = Color.FromArgb(248, 217, 20)
    Private _DisabledEmptyFillColor As Color = Color.FromArgb(212, 212, 212)
    Private _DisabledRatedFillColor As Color = Color.DarkGray

    Private HoverIndex As Integer = -1

    ' ========= PROPERTIES =========

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Maximum As Integer
        Get
            Return _Maximum
        End Get
        Set(value As Integer)
            _Maximum = Math.Max(1, value)
            If _Value > _Maximum Then _Value = _Maximum
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(value As Integer)
            _Value = Math.Max(0, Math.Min(value, _Maximum))
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Spacing As Integer
        Get
            Return _Spacing
        End Get
        Set(value As Integer)
            _Spacing = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BorderThickness As Integer
        Get
            Return _BorderThickness
        End Get
        Set(value As Integer)
            _BorderThickness = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property InnerRadius As Integer
        Get
            Return _InnerRadius
        End Get
        Set(value As Integer)
            _InnerRadius = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property OuterRadius As Integer
        Get
            Return _OuterRadius
        End Get
        Set(value As Integer)
            _OuterRadius = Math.Max(2, value)
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Shape As RatingShape
        Get
            Return _Shape
        End Get
        Set(value As RatingShape)
            _Shape = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ReadOnly_ As Boolean
        Get
            Return _ReadOnly_
        End Get
        Set(value As Boolean)
            _ReadOnly_ = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RightClickToClear As Boolean
        Get
            Return _RightClickToClear
        End Get
        Set(value As Boolean)
            _RightClickToClear = value
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EmptyFillColor As Color
        Get
            Return _EmptyFillColor
        End Get
        Set(value As Color)
            _EmptyFillColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EmptyBorderColor As Color
        Get
            Return _EmptyBorderColor
        End Get
        Set(value As Color)
            _EmptyBorderColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HoverFillColor As Color
        Get
            Return _HoverFillColor
        End Get
        Set(value As Color)
            _HoverFillColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property HoverBorderColor As Color
        Get
            Return _HoverBorderColor
        End Get
        Set(value As Color)
            _HoverBorderColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RatedFillColor As Color
        Get
            Return _RatedFillColor
        End Get
        Set(value As Color)
            _RatedFillColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RatedBorderColor As Color
        Get
            Return _RatedBorderColor
        End Get
        Set(value As Color)
            _RatedBorderColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property DisabledEmptyFillColor As Color
        Get
            Return _DisabledEmptyFillColor
        End Get
        Set(value As Color)
            _DisabledEmptyFillColor = value
            Invalidate()
        End Set
    End Property

    <Category("Rating"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property DisabledRatedFillColor As Color
        Get
            Return _DisabledRatedFillColor
        End Get
        Set(value As Color)
            _DisabledRatedFillColor = value
            Invalidate()
        End Set
    End Property

    ' ========= CONSTRUCTOR =========

    Public Sub New()
        DoubleBuffered = True
        Me.MinimumSize = New Size(40, 10)
        Size = New Size(170, 30)

        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
    End Sub

    ' ========= PAINT =========

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        For i As Integer = 0 To _Maximum - 1
            Dim x As Integer = i * (_OuterRadius * 2 + _Spacing)
            Dim rect As New Rectangle(x, 0, _OuterRadius * 2, _OuterRadius * 2)

            Dim fill As Color = _EmptyFillColor
            Dim border As Color = _EmptyBorderColor

            If Not Enabled Then
                fill = If(i < _Value, _DisabledRatedFillColor, _DisabledEmptyFillColor)
            ElseIf HoverIndex >= 0 AndAlso i <= HoverIndex Then
                fill = _HoverFillColor
                border = _HoverBorderColor
            ElseIf i < _Value Then
                fill = _RatedFillColor
                border = _RatedBorderColor
            End If

            Using path As GraphicsPath = CreateShape(rect)
                Using b As New SolidBrush(fill)
                    e.Graphics.FillPath(b, path)
                End Using
                Using p As New Pen(border, _BorderThickness)
                    e.Graphics.DrawPath(p, path)
                End Using
            End Using
        Next
    End Sub

    Private Function CreateShape(rect As Rectangle) As GraphicsPath
        Dim gp As New GraphicsPath

        Select Case _Shape
            Case RatingShape.Circle
                gp.AddEllipse(rect)

            Case RatingShape.Square
                gp.AddRectangle(rect)

            Case RatingShape.Triangle
                gp.AddPolygon({
                    New Point(rect.Left + rect.Width \ 2, rect.Top),
                    New Point(rect.Right, rect.Bottom),
                    New Point(rect.Left, rect.Bottom)
                })

            Case RatingShape.Star
                gp.AddPolygon(CreateExactStar(rect))
        End Select

        Return gp
    End Function

    Private Function CreateExactStar(rect As Rectangle) As PointF()
        Dim cx As Single = rect.Left + rect.Width / 2
        Dim cy As Single = rect.Top + rect.Height / 2

        Dim outerR As Single = _OuterRadius
        Dim innerR As Single = outerR * 0.45F

        Dim pts(9) As PointF
        Dim angle As Double = -Math.PI / 2

        For i As Integer = 0 To 9
            Dim r As Single = If(i Mod 2 = 0, outerR, innerR)
            pts(i) = New PointF(
                cx + CSng(Math.Cos(angle) * r),
                cy + CSng(Math.Sin(angle) * r)
            )
            angle += Math.PI / 5
        Next

        Return pts
    End Function

    ' ========= MOUSE =========

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If _ReadOnly_ OrElse Not Enabled Then Exit Sub

        Dim idx As Integer = e.X \ (_OuterRadius * 2 + _Spacing)
        HoverIndex = If(idx >= 0 AndAlso idx < _Maximum, idx, -1)
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        HoverIndex = -1
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If _ReadOnly_ OrElse Not Enabled Then Exit Sub

        If e.Button = MouseButtons.Right AndAlso _RightClickToClear Then
            Value = 0
        ElseIf e.Button = MouseButtons.Left AndAlso HoverIndex >= 0 Then
            Value = HoverIndex + 1
        End If
        Invalidate()
    End Sub

End Class
