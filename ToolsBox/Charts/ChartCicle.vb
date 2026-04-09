Imports System.ComponentModel
Imports System.Drawing.Drawing2D

<ToolboxBitmap(GetType(ChartCircle), "Pink.ico")>
<DesignTimeVisible(True)>
Public Class ChartCircle
    Inherits Control

    ' ================= DADOS =================
    Private _values As Decimal() = {20, 20, 20, 20, 20}
    Private _labels As String() = {"Pencil", "Pen", "Eraser", "Paper", "Sharpener"}
    Private _colors As Color() = {
        Color.FromArgb(40, 45, 60),
        Color.Green,
        Color.Red,
        Color.Orange,
        Color.DeepSkyBlue
    }

    ' ================= APARÊNCIA =================
    Private _ringThickness As Integer = 30
    Private _textFont As Font = New Font("Segoe UI", 7, FontStyle.Bold)
    Private _textColor As Color = Color.Gray
    Private _showPercent As Boolean = True
    Private _explodeOffset As Integer = 3

    ' ================= ANIMAÇÃO =================
    Private _animatedProgress As Single = 1.0F
    Private _enableAnimation As Boolean = True
    Private _animationSpeed As Single = 0.08F
    Private _timer As Timer

    ' ================= HOVER =================
    Private _hoverIndex As Integer = -1
    Private _hoverProgress As Single = 0.0F
    Private _hoverSpeed As Single = 0.15F

    ' ================= PROPRIEDADES =================
    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Values As Decimal()
        Get
            Return _values
        End Get
        Set(value As Decimal())
            _values = value
            ResetAnimation()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Labels As String()
        Get
            Return _labels
        End Get
        Set(value As String())
            _labels = value
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property SliceColors As Color()
        Get
            Return _colors
        End Get
        Set(value As Color())
            _colors = value
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property RingThickness As Integer
        Get
            Return _ringThickness
        End Get
        Set(value As Integer)
            _ringThickness = Math.Max(5, value)
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ExplodeOffset As Integer
        Get
            Return _explodeOffset
        End Get
        Set(value As Integer)
            _explodeOffset = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableAnimation As Boolean
        Get
            Return _enableAnimation
        End Get
        Set(value As Boolean)
            _enableAnimation = value
            If value Then
                _animatedProgress = 0
                _timer.Start()
            Else
                _animatedProgress = 1.0F
                _timer.Stop()
            End If
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AnimationSpeed As Single
        Get
            Return _animationSpeed
        End Get
        Set(value As Single)
            _animationSpeed = Math.Max(0.02F, Math.Min(0.3F, value))
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextFont As Font
        Get
            Return _textFont
        End Get
        Set(value As Font)
            _textFont = value
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(value As Color)
            _textColor = value
            Invalidate()
        End Set
    End Property

    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowPercent As Boolean
        Get
            Return _showPercent
        End Get
        Set(value As Boolean)
            _showPercent = value
            Invalidate()
        End Set
    End Property

    ' ================= CONSTRUTOR =================
    Public Sub New()
        Me.MinimumSize = New Size(40, 40)
        Size = New Size(260, 180)

        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
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

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using

        Dim total As Integer = _values.Sum()
        If total = 0 Then Return

        Dim cx = Width \ 2
        Dim cy = Height \ 2
        Dim radius = Math.Min(Width, Height) \ 3

        Dim baseRect As New RectangleF(
            cx - radius,
            cy - radius,
            radius * 2,
            radius * 2)

        Dim startAngle As Single = -90
        Dim progressTotal As Single = total * _animatedProgress

        For i = 0 To _values.Length - 1
            Dim v = Math.Min(_values(i), progressTotal)
            ' If v <= 0 Then Exit For

            Dim sweep = (v / total) * 360
            Dim midAngle = startAngle + sweep / 2
            Dim rad = midAngle * Math.PI / 180

            Dim dx As Single = 0
            Dim dy As Single = 0

            If i = _hoverIndex Then
                dx = CSng(Math.Cos(rad) * _explodeOffset * _hoverProgress)
                dy = CSng(Math.Sin(rad) * _explodeOffset * _hoverProgress)
            End If

            Dim rect = baseRect
            rect.Offset(dx, dy)

            Using pen As New Pen(_colors(i Mod _colors.Length), _ringThickness)
                g.DrawArc(pen, rect, startAngle, sweep)
            End Using

            Dim p1x = cx + Math.Cos(rad) * radius + dx
            Dim p1y = cy + Math.Sin(rad) * radius + dy
            Dim p2x = cx + Math.Cos(rad) * (radius + 18) + dx
            Dim p2y = cy + Math.Sin(rad) * (radius + 18) + dy

            Using penLine As New Pen(_textColor, 1)
                g.DrawLine(penLine, CSng(p1x), CSng(p1y), CSng(p2x), CSng(p2y))
            End Using


            If Not v <= 0 Then
                Dim percent = CInt((_values(i) / total) * 100)
                Dim txt As String =
                    If(_showPercent, percent & "% ", "") &
                    If(i < _labels.Length, _labels(i), "")
                Dim size = g.MeasureString(txt, _textFont)
                Dim tx = If(p2x < cx, p2x - size.Width - 4, p2x + 4)
                tx = Math.Max(2, Math.Min(tx, Width - size.Width - 2))

                Dim ty = p2y - size.Height / 2
                ty = Math.Max(2, Math.Min(ty, Height - size.Height - 2))

                g.DrawString(txt, _textFont, New SolidBrush(_textColor), CSng(tx), CSng(ty))

                startAngle += sweep
                progressTotal -= v
            End If



        Next
    End Sub

    ' ================= ANIMAÇÃO =================
    Private Sub AnimateStep(sender As Object, e As EventArgs)
        If _enableAnimation AndAlso _animatedProgress < 1 Then
            _animatedProgress += _animationSpeed
            If _animatedProgress > 1 Then _animatedProgress = 1
        End If

        If _hoverIndex >= 0 Then
            _hoverProgress += _hoverSpeed
            If _hoverProgress > 1 Then _hoverProgress = 1
        Else
            _hoverProgress -= _hoverSpeed
            If _hoverProgress < 0 Then _hoverProgress = 0
        End If

        Invalidate()
    End Sub

    Private Sub ResetAnimation()
        If _enableAnimation Then
            _animatedProgress = 0
            _timer.Start()
        Else
            _animatedProgress = 1
        End If
        Invalidate()
    End Sub

    ' ================= MOUSE =================
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        Dim dx = e.X - Width \ 2
        Dim dy = e.Y - Height \ 2
        Dim angle = Math.Atan2(dy, dx) * 180 / Math.PI + 90
        If angle < 0 Then angle += 360

        Dim total = _values.Sum()
        Dim acc As Single = 0
        _hoverIndex = -1

        If Not total <= 0 Then
            For i = 0 To _values.Length - 1
                Dim sweep = (_values(i) / total) * 360
                If angle >= acc AndAlso angle < acc + sweep Then
                    _hoverIndex = i
                    Exit For
                End If
                acc += sweep
            Next
        End If

    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _hoverIndex = -1
    End Sub

    ' ================= TRANSPARÊNCIA =================
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
            Dim g = pevent.Graphics
            Dim s = g.Save()
            g.TranslateTransform(-Left, -Top)
            InvokePaintBackground(Parent, pevent)
            InvokePaint(Parent, pevent)
            g.Restore(s)
        Else
            MyBase.OnPaintBackground(pevent)
        End If
    End Sub

End Class

'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
'Public Class CircleGraph
'    Inherits Control

'    ' ================= DADOS =================
'    Private _values As Integer() = {38, 45, 13, 3, 1}
'    Private _labels As String() = {"16–24", "25–34", "35–44", "45–54", "55+"}
'    Private _colors As Color() = {
'        Color.FromArgb(40, 45, 60),
'        Color.Gainsboro,
'        Color.IndianRed,
'        Color.Orange,
'        Color.DeepSkyBlue
'    }

'    ' ================= APARÊNCIA =================
'    Private _ringThickness As Integer = 22
'    Private _textFont As Font = New Font("Segoe UI", 8, FontStyle.Bold)
'    Private _textColor As Color = Color.Gray
'    Private _showPercent As Boolean = True

'    ' ================= ANIMAÇÃO =================
'    Private _animatedProgress As Single = 0
'    Private _enableAnimation As Boolean = True
'    Private _animationSpeed As Single = 0.08F
'    Private _timer As Timer

'    ' ===== HOVER (NOVO, SEM QUEBRAR NADA) =====
'    Private _hoverProgress As Single = 0
'    Private _hoverTarget As Single = 0
'    Private Const HoverExpand As Single = 10

'    ' ================= CONSTRUTOR =================
'    Public Sub New()
'        SetStyle(ControlStyles.UserPaint Or
'                 ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.SupportsTransparentBackColor, True)

'        BackColor = Color.Transparent
'        Size = New Size(260, 180)

'        _timer = New Timer()
'        _timer.Interval = 16
'        AddHandler _timer.Tick, AddressOf AnimateStep
'        _timer.Start()
'    End Sub

'    ' ================= DESENHO =================
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias

'        If BackColor <> Color.Transparent Then
'            g.Clear(BackColor)
'        End If

'        Dim total As Integer = _values.Sum()
'        If total = 0 Then Return

'        Dim centerX = Width \ 2
'        Dim centerY = Height \ 2

'        Dim baseRadius = Math.Min(Width, Height) \ 3
'        Dim radius = baseRadius + (_hoverProgress * HoverExpand)

'        Dim rect As New RectangleF(
'            centerX - radius,
'            centerY - radius,
'            radius * 2,
'            radius * 2)

'        Dim startAngle As Single = -90
'        Dim progressTotal As Single = total * _animatedProgress

'        For i = 0 To _values.Length - 1
'            Dim v = Math.Min(_values(i), progressTotal)
'            If v <= 0 Then Exit For

'            Dim sweep = (v / total) * 360

'            Using pen As New Pen(_colors(i Mod _colors.Length), _ringThickness)
'                g.DrawArc(pen, rect, startAngle, sweep)
'            End Using

'            ' ===== TEXTO EXTERNO =====
'            Dim midAngle = startAngle + sweep / 2
'            Dim rad = midAngle * Math.PI / 180

'            Dim p1x = centerX + Math.Cos(rad) * radius
'            Dim p1y = centerY + Math.Sin(rad) * radius

'            Dim p2x = centerX + Math.Cos(rad) * (radius + 18)
'            Dim p2y = centerY + Math.Sin(rad) * (radius + 18)

'            Using penLine As New Pen(_textColor, 1)
'                g.DrawLine(penLine, CSng(p1x), CSng(p1y), CSng(p2x), CSng(p2y))
'            End Using

'            Dim percent = CInt((_values(i) / total) * 100)
'            Dim txt As String =
'                If(_showPercent, percent & "%", "") &
'                " " & If(i < _labels.Length, _labels(i), "")

'            Dim size = g.MeasureString(txt, _textFont)
'            Dim tx = If(p2x < centerX, p2x - size.Width - 4, p2x + 4)

'            g.DrawString(txt, _textFont, New SolidBrush(_textColor),
'                         CSng(tx),
'                         CSng(p2y - size.Height / 2))

'            startAngle += sweep
'            progressTotal -= v
'        Next
'    End Sub

'    ' ================= ANIMAÇÃO =================
'    Private Sub AnimateStep(sender As Object, e As EventArgs)
'        If _enableAnimation AndAlso _animatedProgress < 1 Then
'            _animatedProgress += _animationSpeed
'            If _animatedProgress > 1 Then _animatedProgress = 1
'        End If

'        _hoverProgress += (_hoverTarget - _hoverProgress) * 0.15F

'        Invalidate()
'    End Sub

'    ' ================= HOVER =================
'    Protected Overrides Sub OnMouseEnter(e As EventArgs)
'        _hoverTarget = 1
'        _timer.Start()
'        MyBase.OnMouseEnter(e)
'    End Sub

'    Protected Overrides Sub OnMouseLeave(e As EventArgs)
'        _hoverTarget = 0
'        _timer.Start()
'        MyBase.OnMouseLeave(e)
'    End Sub

'    ' ================= TRANSPARÊNCIA =================
'    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
'        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
'            Dim g = pevent.Graphics
'            Dim s = g.Save()
'            g.TranslateTransform(-Left, -Top)
'            InvokePaintBackground(Parent, pevent)
'            InvokePaint(Parent, pevent)
'            g.Restore(s)
'        Else
'            MyBase.OnPaintBackground(pevent)
'        End If
'    End Sub

'End Class

'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
'Public Class CircleGraph
'    Inherits Control

'    ' ================= DADOS =================
'    Private _values As Integer() = {38, 45, 13, 3, 1}
'    Private _labels As String() = {"16–24", "25–34", "35–44", "45–54", "55+"}
'    Private _colors As Color() = {
'        Color.FromArgb(40, 45, 60),
'        Color.Gainsboro,
'        Color.IndianRed,
'        Color.Orange,
'        Color.DeepSkyBlue
'    }

'    ' ================= APARÊNCIA =================
'    Private _ringThickness As Integer = 22
'    Private _textFont As Font = New Font("Segoe UI", 8, FontStyle.Bold)
'    Private _textColor As Color = Color.Gray
'    Private _showPercent As Boolean = True

'    ' ================= ANIMAÇÃO =================
'    Private _animatedProgress As Single = 0
'    Private _enableAnimation As Boolean = True
'    Private _animationSpeed As Single = 0.08F
'    Private _timer As Timer

'    ' ================= HOVER / EXPANSÃO =================
'    Private _hoverProgress As Single = 0
'    Private _hoverTarget As Single = 0
'    Private _explodeOffset As Integer = 10

'    ' ================= PROPRIEDADES =================
'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As Integer()
'        Get
'            Return _values
'        End Get
'        Set(value As Integer())
'            _values = value
'            ResetAnimation()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Labels As String()
'        Get
'            Return _labels
'        End Get
'        Set(value As String())
'            _labels = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property SliceColors As Color()
'        Get
'            Return _colors
'        End Get
'        Set(value As Color())
'            _colors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property RingThickness As Integer
'        Get
'            Return _ringThickness
'        End Get
'        Set(value As Integer)
'            _ringThickness = Math.Max(5, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextFont As Font
'        Get
'            Return _textFont
'        End Get
'        Set(value As Font)
'            _textFont = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextColor As Color
'        Get
'            Return _textColor
'        End Get
'        Set(value As Color)
'            _textColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowPercent As Boolean
'        Get
'            Return _showPercent
'        End Get
'        Set(value As Boolean)
'            _showPercent = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property EnableAnimation As Boolean
'        Get
'            Return _enableAnimation
'        End Get
'        Set(value As Boolean)
'            _enableAnimation = value
'            If Not value Then
'                _animatedProgress = 1
'                _timer.Stop()
'                Invalidate()
'            Else
'                ResetAnimation()
'            End If
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property AnimationSpeed As Single
'        Get
'            Return _animationSpeed
'        End Get
'        Set(value As Single)
'            _animationSpeed = Math.Max(0.02F, Math.Min(0.3F, value))
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ExplodeOffset As Integer
'        Get
'            Return _explodeOffset
'        End Get
'        Set(value As Integer)
'            _explodeOffset = Math.Max(0, value)
'            Invalidate()
'        End Set
'    End Property

'    ' ================= CONSTRUTOR =================
'    Public Sub New()
'        SetStyle(ControlStyles.UserPaint Or
'                 ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.SupportsTransparentBackColor, True)

'        BackColor = Color.Transparent
'        Size = New Size(260, 180)

'        _timer = New Timer()
'        _timer.Interval = 16
'        AddHandler _timer.Tick, AddressOf AnimateStep
'        _timer.Start()
'    End Sub

'    ' ================= DESENHO =================
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias

'        If BackColor <> Color.Transparent Then
'            g.Clear(BackColor)
'        End If

'        Dim total As Integer = _values.Sum()
'        If total = 0 Then Return

'        Dim cx = Width \ 2
'        Dim cy = Height \ 2

'        Dim baseRadius As Single = Math.Min(Width, Height) \ 3
'        Dim maxRadius As Single = (Math.Min(Width, Height) \ 2) - _ringThickness - 2
'        Dim radius As Single = Math.Min(baseRadius + _hoverProgress * _explodeOffset, maxRadius)

'        Dim rect As New RectangleF(cx - radius, cy - radius, radius * 2, radius * 2)

'        Dim startAngle As Single = -90
'        Dim progressTotal As Single = total * _animatedProgress

'        For i = 0 To _values.Length - 1
'            Dim v = Math.Min(_values(i), progressTotal)
'            If v <= 0 Then Exit For

'            Dim sweep As Single = (v / total) * 360

'            Using pen As New Pen(_colors(i Mod _colors.Length), _ringThickness)
'                g.DrawArc(pen, rect, startAngle, sweep)
'            End Using

'            ' ===== BARRINHA INDICADORA (leader line) =====
'            Dim midAngle = startAngle + sweep / 2
'            Dim rad = midAngle * Math.PI / 180

'            Dim p1x = cx + Math.Cos(rad) * radius
'            Dim p1y = cy + Math.Sin(rad) * radius

'            Dim p2x = cx + Math.Cos(rad) * (radius + 18)
'            Dim p2y = cy + Math.Sin(rad) * (radius + 18)

'            Using linePen As New Pen(_textColor, 1)
'                g.DrawLine(linePen, CSng(p1x), CSng(p1y), CSng(p2x), CSng(p2y))
'            End Using

'            ' ===== TEXTO =====
'            Dim percent = CInt((_values(i) / total) * 100)
'            Dim txt As String =
'                If(_showPercent, percent & "% ", "") &
'                If(i < _labels.Length, _labels(i), "")

'            Dim size = g.MeasureString(txt, _textFont)
'            Dim tx = If(p2x < cx, p2x - size.Width - 4, p2x + 4)

'            g.DrawString(txt, _textFont, New SolidBrush(_textColor),
'                         CSng(tx),
'                         CSng(p2y - size.Height / 2))

'            startAngle += sweep
'            progressTotal -= v
'        Next
'    End Sub

'    ' ================= ANIMAÇÃO =================
'    Private Sub AnimateStep(sender As Object, e As EventArgs)
'        If _enableAnimation AndAlso _animatedProgress < 1 Then
'            _animatedProgress += _animationSpeed
'            If _animatedProgress > 1 Then _animatedProgress = 1
'        End If

'        _hoverProgress += (_hoverTarget - _hoverProgress) * 0.15F
'        Invalidate()
'    End Sub

'    Private Sub ResetAnimation()
'        _animatedProgress = If(_enableAnimation, 0, 1)
'        _timer.Start()
'        Invalidate()
'    End Sub

'    ' ================= HOVER =================
'    Protected Overrides Sub OnMouseEnter(e As EventArgs)
'        _hoverTarget = 1
'        _timer.Start()
'        MyBase.OnMouseEnter(e)
'    End Sub

'    Protected Overrides Sub OnMouseLeave(e As EventArgs)
'        _hoverTarget = 0
'        _timer.Start()
'        MyBase.OnMouseLeave(e)
'    End Sub

'    ' ================= TRANSPARÊNCIA =================
'    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
'        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
'            Dim g = pevent.Graphics
'            Dim s = g.Save()
'            g.TranslateTransform(-Left, -Top)
'            InvokePaintBackground(Parent, pevent)
'            InvokePaint(Parent, pevent)
'            g.Restore(s)
'        Else
'            MyBase.OnPaintBackground(pevent)
'        End If
'    End Sub

'End Class

'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
'Public Class CircleGraph
'    Inherits Control

'    ' ================= DADOS =================
'    Private _values As Integer() = {38, 45, 13, 3, 1}
'    Private _labels As String() = {"16–24", "25–34", "35–44", "45–54", "55+"}
'    Private _colors As Color() = {
'        Color.FromArgb(40, 45, 60),
'        Color.Gainsboro,
'        Color.IndianRed,
'        Color.Orange,
'        Color.DeepSkyBlue
'    }

'    ' ================= APARÊNCIA =================
'    Private _ringThickness As Integer = 22
'    Private _textFont As Font = New Font("Segoe UI", 8, FontStyle.Bold)
'    Private _textColor As Color = Color.Gray
'    Private _showPercent As Boolean = True

'    ' ================= ANIMAÇÃO =================
'    Private _animatedProgress As Single = 0
'    Private _enableAnimation As Boolean = True
'    Private _animationSpeed As Single = 0.08F
'    Private _timer As Timer

'    ' ================= PROPRIEDADES =================
'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As Integer()
'        Get
'            Return _values
'        End Get
'        Set(value As Integer())
'            _values = value
'            ResetAnimation()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Labels As String()
'        Get
'            Return _labels
'        End Get
'        Set(value As String())
'            _labels = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property SliceColors As Color()
'        Get
'            Return _colors
'        End Get
'        Set(value As Color())
'            _colors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property RingThickness As Integer
'        Get
'            Return _ringThickness
'        End Get
'        Set(value As Integer)
'            _ringThickness = Math.Max(5, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property EnableAnimation As Boolean
'        Get
'            Return _enableAnimation
'        End Get
'        Set(value As Boolean)
'            _enableAnimation = value
'            If Not value Then
'                _animatedProgress = 1
'                _timer.Stop()
'                Invalidate()
'            End If
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property AnimationSpeed As Single
'        Get
'            Return _animationSpeed
'        End Get
'        Set(value As Single)
'            _animationSpeed = Math.Max(0.02F, Math.Min(0.3F, value))
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextFont As Font
'        Get
'            Return _textFont
'        End Get
'        Set(value As Font)
'            _textFont = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextColor As Color
'        Get
'            Return _textColor
'        End Get
'        Set(value As Color)
'            _textColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("CircleGraph"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowPercent As Boolean
'        Get
'            Return _showPercent
'        End Get
'        Set(value As Boolean)
'            _showPercent = value
'            Invalidate()
'        End Set
'    End Property

'    ' ================= CONSTRUTOR =================
'    Public Sub New()
'        SetStyle(ControlStyles.UserPaint Or
'                 ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.SupportsTransparentBackColor, True)

'        BackColor = Color.Transparent
'        Size = New Size(260, 180)

'        _timer = New Timer()
'        _timer.Interval = 16
'        AddHandler _timer.Tick, AddressOf AnimateStep
'        _timer.Start()
'    End Sub

'    ' ================= DESENHO =================
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias

'        If BackColor <> Color.Transparent Then
'            g.Clear(BackColor)
'        End If

'        Dim total As Integer = _values.Sum()
'        If total = 0 Then Return

'        Dim centerX = Width \ 2
'        Dim centerY = Height \ 2
'        Dim radius = Math.Min(Width, Height) \ 3

'        Dim rect As New RectangleF(
'            centerX - radius,
'            centerY - radius,
'            radius * 2,
'            radius * 2)

'        Dim startAngle As Single = -90
'        Dim progressTotal As Single = total * _animatedProgress

'        For i = 0 To _values.Length - 1
'            Dim v = Math.Min(_values(i), progressTotal)
'            If v <= 0 Then Exit For

'            Dim sweep = (v / total) * 360

'            Using pen As New Pen(_colors(i Mod _colors.Length), _ringThickness)
'                g.DrawArc(pen, rect, startAngle, sweep)
'            End Using

'            ' ===== TEXTO EXTERNO =====
'            Dim midAngle = startAngle + sweep / 2
'            Dim rad = midAngle * Math.PI / 180

'            Dim p1x = centerX + Math.Cos(rad) * radius
'            Dim p1y = centerY + Math.Sin(rad) * radius

'            Dim p2x = centerX + Math.Cos(rad) * (radius + 18)
'            Dim p2y = centerY + Math.Sin(rad) * (radius + 18)

'            Using penLine As New Pen(_textColor, 1)
'                g.DrawLine(penLine, CSng(p1x), CSng(p1y), CSng(p2x), CSng(p2y))
'            End Using

'            Dim percent = CInt((_values(i) / total) * 100)
'            Dim txt As String =
'                If(_showPercent, percent & "%", "") &
'                " " & If(i < _labels.Length, _labels(i), "")

'            Dim size = g.MeasureString(txt, _textFont)

'            ' ===== CORREÇÃO AQUI (CLAMP) =====
'            Dim tx As Single =
'                If(p2x < centerX, p2x - size.Width - 4, p2x + 4)
'            Dim ty As Single = p2y - size.Height / 2

'            tx = Math.Max(2, Math.Min(tx, Width - size.Width - 2))
'            ty = Math.Max(2, Math.Min(ty, Height - size.Height - 2))
'            ' =================================

'            Using br As New SolidBrush(_textColor)
'                g.DrawString(txt, _textFont, br, tx, ty)
'            End Using

'            startAngle += sweep
'            progressTotal -= v
'        Next
'    End Sub

'    ' ================= ANIMAÇÃO =================
'    Private Sub AnimateStep(sender As Object, e As EventArgs)
'        If Not _enableAnimation Then Return

'        If _animatedProgress < 1 Then
'            _animatedProgress += _animationSpeed
'            If _animatedProgress > 1 Then _animatedProgress = 1
'            Invalidate()
'        End If
'    End Sub

'    Private Sub ResetAnimation()
'        If _enableAnimation Then
'            _animatedProgress = 0
'            _timer.Start()
'        Else
'            _animatedProgress = 1
'        End If
'        Invalidate()
'    End Sub

'    ' ================= TRANSPARÊNCIA =================
'    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
'        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
'            Dim g = pevent.Graphics
'            Dim s = g.Save()
'            g.TranslateTransform(-Left, -Top)
'            InvokePaintBackground(Parent, pevent)
'            InvokePaint(Parent, pevent)
'            g.Restore(s)
'        Else
'            MyBase.OnPaintBackground(pevent)
'        End If
'    End Sub

'End Class

