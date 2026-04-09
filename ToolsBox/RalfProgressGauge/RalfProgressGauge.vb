Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<ToolboxBitmap(GetType(RalfProgressGauge), "Orange.ico")>
<DesignTimeVisible(True)>
Public Class RalfProgressGauge
    Inherits Control

    ' ========= BACKING FIELDS =========

    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 100
    Private _Value As Integer = 40
    Private _Thickness As Integer = 30

    Private _ProgressBackColor As Color = Color.LightBlue
    Private _ProgressColorLow As Color = Color.DodgerBlue
    Private _ProgressColorHigh As Color = Color.Crimson

    Private _AutoGenerateProgressColorWhenLow As Boolean = False
    Private _AutoGenerateProgressColorWhenHigh As Boolean = True

    Private _LighteningFactor As Integer = 70
    Private _WarningMark As Integer = 70

    Private _ShowValueLabel As Boolean = True
    Private _ShowRangeLabels As Boolean = True
    Private _RangeLabelsInside As Boolean = False

    Private _RangeLabelsColor As Color = Color.Black
    Private _ValueLabelColor As Color = Color.Black

    Private _Prefix As String = ""
    Private _Suffix As String = "%"

    Private _ProgressCap As LineCap = LineCap.Flat

    Private _GaugeFont As Font = New Font("Century Gothic", 14, FontStyle.Regular)

    ' ========= PROPERTIES =========

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Minimum As Integer
        Get
            Return _Minimum
        End Get
        Set(value As Integer)
            _Minimum = value
            If _Value < _Minimum Then _Value = _Minimum
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Maximum As Integer
        Get
            Return _Maximum
        End Get
        Set(value As Integer)
            _Maximum = Math.Max(_Minimum + 1, value)
            If _Value > _Maximum Then _Value = _Maximum
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(value As Integer)
            _Value = Math.Max(_Minimum, Math.Min(value, _Maximum))
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Thickness As Integer
        Get
            Return _Thickness
        End Get
        Set(value As Integer)
            _Thickness = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ProgressBackColor As Color
        Get
            Return _ProgressBackColor
        End Get
        Set(value As Color)
            _ProgressBackColor = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ProgressColorLow As Color
        Get
            Return _ProgressColorLow
        End Get
        Set(value As Color)
            _ProgressColorLow = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ProgressColorHigh As Color
        Get
            Return _ProgressColorHigh
        End Get
        Set(value As Color)
            _ProgressColorHigh = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AutoGenerateProgressColorWhenLow As Boolean
        Get
            Return _AutoGenerateProgressColorWhenLow
        End Get
        Set(value As Boolean)
            _AutoGenerateProgressColorWhenLow = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AutoGenerateProgressColorWhenHigh As Boolean
        Get
            Return _AutoGenerateProgressColorWhenHigh
        End Get
        Set(value As Boolean)
            _AutoGenerateProgressColorWhenHigh = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property LighteningFactor As Integer
        Get
            Return _LighteningFactor
        End Get
        Set(value As Integer)
            _LighteningFactor = Math.Max(0, Math.Min(100, value))
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property WarningMark As Integer
        Get
            Return _WarningMark
        End Get
        Set(value As Integer)
            _WarningMark = Math.Max(_Minimum, Math.Min(value, _Maximum))
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowValueLabel As Boolean
        Get
            Return _ShowValueLabel
        End Get
        Set(value As Boolean)
            _ShowValueLabel = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowRangeLabels As Boolean
        Get
            Return _ShowRangeLabels
        End Get
        Set(value As Boolean)
            _ShowRangeLabels = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RangeLabelsInside As Boolean
        Get
            Return _RangeLabelsInside
        End Get
        Set(value As Boolean)
            _RangeLabelsInside = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RangeLabelsColor As Color
        Get
            Return _RangeLabelsColor
        End Get
        Set(value As Color)
            _RangeLabelsColor = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ValueLabelColor As Color
        Get
            Return _ValueLabelColor
        End Get
        Set(value As Color)
            _ValueLabelColor = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Prefix As String
        Get
            Return _Prefix
        End Get
        Set(value As String)
            _Prefix = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Suffix As String
        Get
            Return _Suffix
        End Get
        Set(value As String)
            _Suffix = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ProgressCap As LineCap
        Get
            Return _ProgressCap
        End Get
        Set(value As LineCap)
            _ProgressCap = value
            Invalidate()
        End Set
    End Property

    <Category("Progress Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GaugeFont As Font
        Get
            Return _GaugeFont
        End Get
        Set(value As Font)
            _GaugeFont = value
            Invalidate()
        End Set
    End Property

    ' ========= CONSTRUCTOR =========

    Public Sub New()
        DoubleBuffered = True
        Me.MinimumSize = New Size(50, 20)
        Size = New Size(250, 150)

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

        Dim rect As New Rectangle(
            Thickness,
            Thickness,
            Width - Thickness * 2 - 25,
            Height * 2 - Thickness * 2 - 25)

        Using backPen As New Pen(_ProgressBackColor, _Thickness)
            backPen.StartCap = _ProgressCap
            backPen.EndCap = _ProgressCap
            e.Graphics.DrawArc(backPen, rect, 180, 180)
        End Using

        Dim percent As Double = (_Value - _Minimum) / (_Maximum - _Minimum)
        Dim sweep As Single = CSng(180 * percent)

        Dim progressColor As Color = ResolveProgressColor()

        Using progPen As New Pen(progressColor, _Thickness)
            progPen.StartCap = _ProgressCap
            progPen.EndCap = _ProgressCap
            e.Graphics.DrawArc(progPen, rect, 180, sweep)
        End Using

        DrawLabels(e.Graphics)
    End Sub

    Private Function ResolveProgressColor() As Color
        If _AutoGenerateProgressColorWhenHigh AndAlso _Value >= _WarningMark Then
            Return _ProgressColorHigh
        End If

        If _AutoGenerateProgressColorWhenLow AndAlso _Value < _WarningMark Then
            Return ControlPaint.Light(_ProgressColorLow, _LighteningFactor / 100.0F)
        End If

        Return _ProgressColorLow
    End Function

    Private Sub DrawLabels(g As Graphics)
        If _ShowValueLabel Then
            Dim txt As String = _Prefix & _Value.ToString() & _Suffix
            Using br As New SolidBrush(_ValueLabelColor)
                Dim sz = g.MeasureString(txt, _GaugeFont)
                g.DrawString(txt, _GaugeFont, br, (Width - sz.Width) / 2, (Height - sz.Height) / 1.35F)
            End Using
        End If

        If _ShowRangeLabels Then
            Using br As New SolidBrush(_RangeLabelsColor)

                Dim minTxt As String = _Minimum.ToString()
                Dim maxTxt As String = _Maximum.ToString()

                Dim minSz = g.MeasureString(minTxt, Font)
                Dim maxSz = g.MeasureString(maxTxt, Font)

                ' Retângulo do arco (mesmo do OnPaint)
                Dim arcRect As New RectangleF(
                    Thickness,
                    Thickness,
                    Width - Thickness * 2 - 25,
                    Height * 2 - Thickness * 2 - 25
                )

                ' Extremos X do arco
                Dim xMin As Single = arcRect.Left - minSz.Width / 2
                Dim xMax As Single = arcRect.Right - maxSz.Width / 2

                ' Clamp horizontal
                xMin = Math.Max(2, xMin)
                xMax = Math.Min(Width - maxSz.Width - 2, xMax)

                ' PONTO MAIS BAIXO VISÍVEL DO ARCO (meio da altura do controle)
                Dim arcVisibleBottom As Single = Height - Thickness - 2

                If _RangeLabelsInside Then
                    ' Dentro (logo acima da barra)
                    Dim yInside As Single = arcVisibleBottom + 3
                    g.DrawString(minTxt, Font, br, xMin, yInside)
                    g.DrawString(maxTxt, Font, br, xMax, yInside)
                Else
                    ' Fora (logo abaixo da barra)
                    Dim yOutside As Single = arcVisibleBottom + 21
                    g.DrawString(minTxt, Font, br, xMin, yOutside)
                    g.DrawString(maxTxt, Font, br, xMax, yOutside)
                End If

            End Using
        End If


        'If _ShowRangeLabels Then
        '    Using br As New SolidBrush(_RangeLabelsColor)
        '        Dim minTxt As String = _Minimum.ToString()
        '        Dim maxTxt As String = _Maximum.ToString()

        '        Dim minSz = g.MeasureString(minTxt, Font)
        '        Dim maxSz = g.MeasureString(maxTxt, Font)

        '        If _RangeLabelsInside Then
        '            ' dentro do arco (início e fim)
        '            g.DrawString(minTxt, Font, br, Thickness, Height - Thickness * 1.8F)
        '            g.DrawString(maxTxt, Font, br, Width - Thickness - maxSz.Width, Height - Thickness * 1.8F)
        '        Else
        '            ' fora do arco (ABAIXO da barra, fiel à imagem)
        '            ' posição abaixo do arco (logo abaixo da barra, não no rodapé)
        '            Dim arcBottom As Single = Thickness + (Height * 0.5F)
        '            Dim yBelow As Single = arcBottom + (Thickness / 2)
        '            g.DrawString(minTxt, Font, br, Thickness - minSz.Width / 2, yBelow + 25)
        '            g.DrawString(maxTxt, Font, br, Width - Thickness - maxSz.Width / 2, yBelow + 25)
        '        End If
        '    End Using
        'End If
    End Sub

End Class

