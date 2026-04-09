Imports System.ComponentModel
Imports System.Drawing.Drawing2D


<ToolboxBitmap(GetType(ChartPie), "Pink.ico")>
<DesignTimeVisible(True)>
Public Class ChartPie
    Inherits Control

    ' ================== DADOS ==================
    Private _values As Decimal() = {30, 25, 20, 15, 10}
    Private _labels As String() = Nothing
    Private _sliceSpacing As Single = 1.0F
    Private _hoverIndex As Integer = -1
    Private _explodeOffset As Single = 5.0F
    Private _enableAnimation As Boolean = True

    ' ================== APARÊNCIA ==================
    Private _sliceColors As Color() =
    {
        Color.DodgerBlue,
        Color.MediumSeaGreen,
        Color.Orange,
        Color.MediumPurple,
        Color.HotPink
    }

    Private _textColor As Color = Color.FromArgb(64, 64, 64)
    Private _showLabels As Boolean = True

    ' ================== ANIMAÇÃO ==================
    Private _animatedProgress As Single = 1.0F
    Private _animationSpeed As Single = 0.15F
    Private _timer As Timer

    ' ================== PROPRIEDADES ==================

    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Values As Decimal()
        Get
            Return _values
        End Get
        Set(ByVal value As Decimal())
            _values = value

            If _enableAnimation AndAlso Not Me.DesignMode Then
                _animatedProgress = 0.0F
                _timer.Start()
            Else
                _animatedProgress = 1.0F
                _timer.Stop()
            End If

            Invalidate()

        End Set

    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Labels As String()
        Get
            Return _labels
        End Get
        Set(value As String())
            _labels = value
            Invalidate()
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property SliceColors As Color()
        Get
            Return _sliceColors
        End Get
        Set(value As Color())
            If value IsNot Nothing AndAlso value.Length > 0 Then
                _sliceColors = value
                Invalidate()
            End If
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(value As Color)
            _textColor = value
            Invalidate()
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowLabels As Boolean
        Get
            Return _showLabels
        End Get
        Set(value As Boolean)
            _showLabels = value
            Invalidate()
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AnimationSpeed As Single
        Get
            Return _animationSpeed
        End Get
        Set(value As Single)
            _animationSpeed = Math.Max(0.01F, Math.Min(1.0F, value))
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property SliceSpacing As Single
        Get
            Return _sliceSpacing
        End Get
        Set(value As Single)
            _sliceSpacing = Math.Max(0, value)
            Invalidate()
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ExplodeOffset As Single
        Get
            Return _explodeOffset
        End Get
        Set(value As Single)
            _explodeOffset = Math.Max(0, value)
            Invalidate()
        End Set
    End Property
    <Category("ChartPie"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableAnimation As Boolean
        Get
            Return _enableAnimation
        End Get
        Set(value As Boolean)
            _enableAnimation = value

            If Not value Then
                _animatedProgress = 1.0F
                _timer.Stop()
            End If

            Invalidate()
        End Set
    End Property



    ' ================== CONSTRUTOR ==================
    Public Sub New()
        Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Me.MinimumSize = New Size(50, 50)
        Size = New Size(200, 200)
        _animatedProgress = 1.0F   ' <<< ESSENCIAL
        _timer = New Timer()
        _timer.Interval = 16
        AddHandler _timer.Tick, AddressOf AnimateStep

        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
    End Sub

    ' ================== DESENHO ==================
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using

        DrawPie(g)
    End Sub

    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
            Dim g = pevent.Graphics
            Dim state = g.Save()

            g.TranslateTransform(-Left, -Top)
            InvokePaintBackground(Parent, pevent)
            InvokePaint(Parent, pevent)

            g.Restore(state)
        Else
            MyBase.OnPaintBackground(pevent)
        End If
    End Sub

    ' ================== DESENHO DO GRÁFICO ==================

    Private Sub DrawPie(g As Graphics)
        Dim progress As Single = If(Me.DesignMode OrElse Not _enableAnimation, 1.0F, _animatedProgress)

        Dim total As Single = _values.Sum()
        If total <= 0 Then Return

        Dim margin As Single = _sliceSpacing + _explodeOffset + 4

        Dim rect As New RectangleF(
    margin,
    margin,
    Width - margin * 2,
    Height - margin * 2
)

        Dim cx As Single = rect.X + rect.Width / 2
        Dim cy As Single = rect.Y + rect.Height / 2

        Dim startAngle As Single = -90.0F

        Using textBrush As New SolidBrush(_textColor)
            For i = 0 To _values.Length - 1

                Dim fullSweep As Single = (_values(i) / total) * 360.0F
                Dim sweepAngle As Single = fullSweep * _animatedProgress


                If sweepAngle <= 0 Then Continue For


                Dim midAngle As Single = startAngle + sweepAngle / 2
                Dim rad As Single = midAngle * CSng(Math.PI / 180)

                ' GAP REAL (igual para todas)
                Dim gapX As Single = CSng(Math.Cos(rad) * _sliceSpacing)
                Dim gapY As Single = CSng(Math.Sin(rad) * _sliceSpacing)

                ' Hover explode soma ao gap
                If i = _hoverIndex Then
                    gapX += CSng(Math.Cos(rad) * _explodeOffset)
                    gapY += CSng(Math.Sin(rad) * _explodeOffset)
                End If

                Using br As New SolidBrush(_sliceColors(i Mod _sliceColors.Length))
                    g.FillPie(
                    br,
                    rect.X + gapX,
                    rect.Y + gapY,
                    rect.Width,
                    rect.Height,
                    startAngle,
                    sweepAngle
                )
                End Using

                ' Texto
                If _showLabels Then
                    Dim radius As Single = rect.Width * 0.33F
                    Dim tx As Single = cx + CSng(Math.Cos(rad) * radius)
                    Dim ty As Single = cy + CSng(Math.Sin(rad) * radius)

                    Dim txt As String =
                    If(_labels IsNot Nothing AndAlso i < _labels.Length,
                       _labels(i),
                       _values(i).ToString())

                    Dim size = g.MeasureString(txt, Font)
                    g.DrawString(txt, Font, textBrush,
                             tx - size.Width / 2,
                             ty - size.Height / 2)
                End If

                startAngle += sweepAngle
            Next
        End Using
    End Sub



    ' ================== ANIMAÇÃO ==================
    Private Sub AnimateStep(sender As Object, e As EventArgs)

        If Not _enableAnimation Then
            _timer.Stop()
            Return
        End If

        _animatedProgress += _animationSpeed

        If _animatedProgress >= 1 Then
            _animatedProgress = 1
            _timer.Stop()
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If Not _enableAnimation Then
            _timer.Stop()
            Return
        End If

        MyBase.OnMouseMove(e)

        Dim rect As New RectangleF(10, 10, Width - 20, Height - 20)
        Dim cx = rect.X + rect.Width / 2
        Dim cy = rect.Y + rect.Height / 2

        Dim dx = e.X - cx
        Dim dy = e.Y - cy

        Dim dist = Math.Sqrt(dx * dx + dy * dy)
        If dist > rect.Width / 2 Then
            _hoverIndex = -1
            Invalidate()
            Return
        End If

        Dim angle = Math.Atan2(dy, dx) * 180 / Math.PI
        angle += 90
        If angle < 0 Then angle += 360

        Dim total = _values.Sum()
        Dim acc As Single = 0

        _hoverIndex = -1

        For i = 0 To _values.Length - 1
            Dim sweep = (_values(i) / total) * 360.0F
            If angle >= acc AndAlso angle < acc + sweep Then
                _hoverIndex = i
                Exit For
            End If
            acc += sweep
        Next

        Invalidate()
    End Sub


    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hoverIndex = -1
        Invalidate()
    End Sub

    Private Function HitTestSlice(p As Point) As Integer
        If _values Is Nothing OrElse _values.Length = 0 Then Return -1

        Dim cx = Width \ 2
        Dim cy = Height \ 2

        Dim dx = p.X - cx
        Dim dy = p.Y - cy

        Dim distance = Math.Sqrt(dx * dx + dy * dy)
        Dim radius = Math.Min(Width, Height) \ 2 - 10

        If distance > radius Then Return -1

        Dim angle = Math.Atan2(dy, dx) * 180 / Math.PI
        If angle < 0 Then angle += 360

        Dim total = _values.Sum()
        Dim startAngle As Single = 0

        For i = 0 To _values.Length - 1
            Dim sweep = CSng((_values(i) / total) * 360)

            If angle >= startAngle AndAlso angle < startAngle + sweep Then
                Return i
            End If

            startAngle += sweep
        Next

        Return -1
    End Function

End Class

