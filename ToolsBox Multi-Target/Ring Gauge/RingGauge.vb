Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing

<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
Public Class RingGauge
    Inherits Control

    ' ================= DADOS =================
    Private _value As Integer = 30
    Private _maxValue As Integer = 100
    Private _displayText As String = "Info"
    Private _showPercentage As Boolean = True

    ' ================= APARÊNCIA =================
    Private _ringColor As Color = Color.RoyalBlue
    Private _baseRingColor As Color = Color.Gainsboro
    Private _textColor As Color = Color.Silver
    Private _ringThickness As Integer = 12

    Private _valueFont As Font = New Font("Segoe UI", 10, FontStyle.Bold)
    Private _textFont As Font = New Font("Segoe UI", 8, FontStyle.Regular)

    ' ================= ANIMAÇÃO =================
    Private _animatedValue As Single = 0
    Private _enableAnimation As Boolean = True
    Private _animationSpeed As Single = 0.15F
    Private _timer As Timer

    Private _explodeOffset As Integer = 1
    Private _currentExplode As Integer = 0
    Private _targetExplode As Integer = 0

    ' ================= PROPRIEDADES =================
    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(value As Integer)
            _value = Math.Max(0, Math.Min(value, _maxValue))
            If Not _enableAnimation Then
                _animatedValue = _value
            End If
            _timer.Start()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property MaxValue As Integer
        Get
            Return _maxValue
        End Get
        Set(value As Integer)
            _maxValue = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property DisplayText As String
        Get
            Return _displayText
        End Get
        Set(value As String)
            _displayText = value
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RingColor As Color
        Get
            Return _ringColor
        End Get
        Set(value As Color)
            _ringColor = value
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BaseRingColor As Color
        Get
            Return _baseRingColor
        End Get
        Set(value As Color)
            _baseRingColor = value
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(value As Color)
            _textColor = value
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RingThickness As Integer
        Get
            Return _ringThickness
        End Get
        Set(value As Integer)
            _ringThickness = Math.Max(4, value)
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableAnimation As Boolean
        Get
            Return _enableAnimation
        End Get
        Set(value As Boolean)
            _enableAnimation = value
            If Not value Then
                _animatedValue = _value
                _timer.Stop()
                Invalidate()
            End If
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AnimationSpeed As Single
        Get
            Return _animationSpeed
        End Get
        Set(value As Single)
            _animationSpeed = Math.Max(0.05F, Math.Min(0.5F, value))
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowPercentage As Boolean
        Get
            Return _showPercentage
        End Get
        Set(value As Boolean)
            _showPercentage = value
            Invalidate()
        End Set
    End Property

    <Category("Ring Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ExplodeOffset As Integer
        Get
            Return _explodeOffset
        End Get
        Set(value As Integer)
            _explodeOffset = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    ' ================= CONSTRUTOR =================
    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or
                 ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.SupportsTransparentBackColor, True)

        DoubleBuffered = True
        Me.MinimumSize = New Size(30, 30)
        Size = New Size(120, 120)
        BackColor = Color.Transparent

        _timer = New Timer()
        _timer.Interval = 16
        AddHandler _timer.Tick, AddressOf AnimateStep
        _timer.Start()
    End Sub

    ' ================= DESENHO =================
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        If BackColor <> Color.Transparent Then
            g.Clear(BackColor)
        End If

        Dim rect As New Rectangle(
            _ringThickness + _currentExplode,
            _ringThickness + _currentExplode,
            Width - (_ringThickness * 2) - (_currentExplode * 2),
            Height - (_ringThickness * 2) - (_currentExplode * 2))

        Using basePen As New Pen(_baseRingColor, _ringThickness),
              valuePen As New Pen(_ringColor, _ringThickness)

            g.DrawArc(basePen, rect, -90, 360)

            Dim sweep As Single = (_animatedValue / _maxValue) * 360
            g.DrawArc(valuePen, rect, -90, sweep)
        End Using

        If _showPercentage Then
            Dim valueText As String = _animatedValue.ToString("0") & "%"
            Dim size = g.MeasureString(valueText, _valueFont)
            g.DrawString(valueText, _valueFont, New SolidBrush(_textColor),
                         CSng((Width - size.Width) / 2),
                         CSng(Height / 2 - size.Height))
        End If

        Dim textSize = g.MeasureString(_displayText, _textFont)
        g.DrawString(_displayText, _textFont, New SolidBrush(_textColor),
                     CSng((Width - textSize.Width) / 2),
                     CSng(Height / 2 + 2))
    End Sub

    ' ================= ANIMAÇÃO =================
    Private Sub AnimateStep(sender As Object, e As EventArgs)
        If _enableAnimation Then
            Dim delta = (_value - _animatedValue) * _animationSpeed
            If Math.Abs(delta) < 0.3F Then
                _animatedValue = _value
            Else
                _animatedValue += delta
            End If
        End If

        If _currentExplode < _targetExplode Then
            _currentExplode += 1
        ElseIf _currentExplode > _targetExplode Then
            _currentExplode -= 1
        End If

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        If Not _enableAnimation Then
            _animatedValue = _value
            Exit Sub
        End If
        _targetExplode = _explodeOffset
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _targetExplode = 0
        MyBase.OnMouseLeave(e)
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

End Class

