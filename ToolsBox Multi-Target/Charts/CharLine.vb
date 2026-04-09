Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing
<DesignTimeVisible(True)>
Public Class ChartLine
    Inherits Control

    ' ===== DADOS =====
    Private _values As New List(Of Decimal()) From {
        New Decimal() {230D, 280D, 200D, 240D, 150D, 220D, 170D},
        New Decimal() {180D, 200D, 170D, 195D, 140D, 150D, 160D},
        New Decimal() {60D, 90D, 10D, 20D, 40D, 35D, 30D}
    }

    Private _labels As String() =
        {"January", "March", "April", "June", "July", "October", "December"}

    ' ===== APARÊNCIA =====
    Private _lineColors As New List(Of Color) From {
        Color.FromArgb(40, 30, 90),
        Color.Olive,
        Color.Red
    }

    Private _fillColors As New List(Of Color) From {
        Color.FromArgb(120, 40, 30, 90),
        Color.FromArgb(120, 130, 140, 0),
        Color.FromArgb(120, 255, 0, 0)
    }

    Private _lineWidths As New List(Of Single) From {2.5F, 2.0F, 2.0F}

    Private _maxValue As Decimal = 300D
    Private _gridMaxValue As Decimal = 300D
    Private _gridLines As Integer = 6
    Private _showGrid As Boolean = True
    Private _showGridNumbers As Boolean = True
    Private _showPercent As Boolean = True
    Private _showPoints As Boolean = True

    ' ===== SOMBRA =====
    Private _showShadow As Boolean = True
    Private _shadowColor As Color = Color.FromArgb(60, 0, 0, 0)

    ' ===== HOVER / ANIMAÇÃO =====
    Private _hoverLine As Integer = -1
    Private _hoverGrow As Single = 0F
    Private Const HoverMax As Single = 3.0F
    Private WithEvents _timer As New Timer With {.Interval = 16}

    ' ===== MARGENS =====
    Private Const ML As Integer = 45
    Private Const MR As Integer = 10
    Private Const MT As Integer = 10
    Private Const MB As Integer = 35

    ' ===== PROPRIEDADES =====

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Values As List(Of Decimal())
        Get
            Return _values
        End Get
        Set(value As List(Of Decimal()))
            _values = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Labels As String()
        Get
            Return _labels
        End Get
        Set(value As String())
            _labels = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property LineColors As List(Of Color)
        Get
            Return _lineColors
        End Get
        Set(value As List(Of Color))
            _lineColors = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property FillColors As List(Of Color)
        Get
            Return _fillColors
        End Get
        Set(value As List(Of Color))
            _fillColors = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property LineWidths As List(Of Single)
        Get
            Return _lineWidths
        End Get
        Set(value As List(Of Single))
            _lineWidths = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property MaxValue As Decimal
        Get
            Return _maxValue
        End Get
        Set(value As Decimal)
            _maxValue = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridMaxValue As Decimal
        Get
            Return _gridMaxValue
        End Get
        Set(value As Decimal)
            _gridMaxValue = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowGrid As Boolean
        Get
            Return _showGrid
        End Get
        Set(value As Boolean)
            _showGrid = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowGridNumbers As Boolean
        Get
            Return _showGridNumbers
        End Get
        Set(value As Boolean)
            _showGridNumbers = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowPercent As Boolean
        Get
            Return _showPercent
        End Get
        Set(value As Boolean)
            _showPercent = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowPoints As Boolean
        Get
            Return _showPoints
        End Get
        Set(value As Boolean)
            _showPoints = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowShadow As Boolean
        Get
            Return _showShadow
        End Get
        Set(value As Boolean)
            _showShadow = value
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShadowColor As Color
        Get
            Return _shadowColor
        End Get
        Set(value As Color)
            _shadowColor = value
            Invalidate()
        End Set
    End Property

    ' ===== CONSTRUTOR =====
    Public Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                 ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.ResizeRedraw Or
                 ControlStyles.UserPaint, True)

        BackColor = Color.White
        Font = New Font("Segoe UI", 8)
        Me.MinimumSize = New Size(130, 90)
        Size = New Size(500, 220)

        _timer.Start()

        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
    End Sub

    ' ===== DESENHO =====
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using

        If _showGrid Then DrawGrid(g)

        For i As Integer = 0 To _values.Count - 1
            DrawLineSeries(g, i)
        Next

        DrawBottomLabels(g)
    End Sub

    ' ===== GRID =====
    Private Sub DrawGrid(g As Graphics)
        Using p As New Pen(Color.FromArgb(60, Color.Gray))
            For i As Integer = 0 To _gridLines
                Dim y As Single = CSng(MT + (Height - MT - MB) * i / _gridLines)
                g.DrawLine(p, ML, y, Width - MR, y)

                If _showGridNumbers Then
                    Dim v As Integer = CInt(_gridMaxValue - (_gridMaxValue / _gridLines) * i)
                    g.DrawString(v.ToString(), Font, Brushes.Gray, 2, y - 6)
                End If
            Next
        End Using
    End Sub

    ' ===== LINHA + ÁREA + SOMBRA =====
    Private Sub DrawLineSeries(g As Graphics, index As Integer)

        Dim data = _values(index)
        Dim w = Width - ML - MR
        Dim h = Height - MT - MB
        Dim stepX = w / (data.Length - 1)

        Dim pts(data.Length - 1) As PointF

        For i As Integer = 0 To data.Length - 1
            pts(i) = New PointF(
                CSng(ML + i * stepX),
                CSng(MT + (1 - data(i) / _maxValue) * h)
            )
        Next

        Using gp As New GraphicsPath()
            gp.AddLines(pts)
            gp.AddLine(pts.Last.X, pts.Last.Y, pts.Last.X, Height - MB)
            gp.AddLine(pts.Last.X, Height - MB, pts(0).X, Height - MB)
            gp.CloseFigure()

            Using b As New SolidBrush(_fillColors(index))
                g.FillPath(b, gp)
            End Using

            If _showShadow Then
                Using sb As New SolidBrush(_shadowColor)
                    Using m As New Matrix()
                        m.Translate(3, 3)
                        gp.Transform(m)
                        g.FillPath(sb, gp)
                    End Using
                End Using
            End If
        End Using

        Dim grow = If(index = _hoverLine, _hoverGrow, 0)

        Using p As New Pen(_lineColors(index), _lineWidths(index) + grow)
            g.DrawLines(p, pts)
        End Using

        If _showPoints Then
            For i As Integer = 0 To pts.Length - 1
                Dim r As Single = 4 + grow
                g.FillEllipse(New SolidBrush(_lineColors(index)),
                              pts(i).X - r, pts(i).Y - r, r * 2, r * 2)

                If _showPercent Then
                    Dim pct = CInt((data(i) / _maxValue) * 100)
                    g.DrawString(pct & "%", Font, Brushes.Black,
                                 pts(i).X - 10, pts(i).Y - 18)
                End If
            Next
        End If
    End Sub

    ' ===== LABELS =====
    Private Sub DrawBottomLabels(g As Graphics)
        Dim stepX As Single = (Width - ML - MR) / (_labels.Length - 1)
        For i As Integer = 0 To _labels.Length - 1
            g.DrawString(_labels(i), Font, Brushes.Gray,
                         CSng(ML + i * stepX - 15),
                         Height - MB + 5)
        Next
    End Sub

    ' ===== HOVER =====
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        _hoverLine = HitTest(e.Location)
    End Sub

    Private Function HitTest(pt As Point) As Integer
        For i As Integer = 0 To _values.Count - 1
            Dim data = _values(i)
            Dim w = Width - ML - MR
            Dim h = Height - MT - MB
            Dim stepX = w / (data.Length - 1)

            For j As Integer = 0 To data.Length - 2
                Dim p1 As New PointF(
                    CSng(ML + j * stepX),
                    CSng(MT + (1 - data(j) / _maxValue) * h)
                )
                Dim p2 As New PointF(
                    CSng(ML + (j + 1) * stepX),
                    CSng(MT + (1 - data(j + 1) / _maxValue) * h)
                )

                Using gp As New GraphicsPath()
                    gp.AddLine(p1, p2)
                    Using pen As New Pen(Color.Black, 8)
                        If gp.IsOutlineVisible(pt, pen) Then Return i
                    End Using
                End Using
            Next
        Next
        Return -1
    End Function

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
        If _hoverLine >= 0 Then
            _hoverGrow = Math.Min(_hoverGrow + 0.4F, HoverMax)
        Else
            _hoverGrow = Math.Max(_hoverGrow - 0.4F, 0)
        End If
        Invalidate()
    End Sub

End Class


'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<DesignTimeVisible(True)>
'Public Class CharLine
'    Inherits Control

'    ' ===== DADOS =====
'    Private _values As New List(Of Decimal()) From {
'        New Decimal() {230D, 280D, 200D, 240D, 150D, 220D, 170D},
'        New Decimal() {180D, 200D, 170D, 195D, 140D, 150D, 160D},
'        New Decimal() {60D, 90D, 10D, 20D, 40D, 35D, 30D}
'    }

'    Private _labels As String() =
'        {"January", "March", "April", "June", "July", "October", "December"}

'    ' ===== APARÊNCIA =====
'    Private _lineColors As New List(Of Color) From {
'        Color.FromArgb(40, 30, 90),
'        Color.Olive,
'        Color.Red
'    }

'    Private _fillColors As New List(Of Color) From {
'        Color.FromArgb(120, 40, 30, 90),
'        Color.FromArgb(120, 130, 140, 0),
'        Color.FromArgb(120, 255, 0, 0)
'    }

'    Private _lineWidths As New List(Of Single) From {2.5F, 2.0F, 2.0F}

'    Private _maxValue As Decimal = 300D
'    Private _gridLines As Integer = 6
'    Private _showGrid As Boolean = True
'    Private _showGridNumbers As Boolean = True
'    Private _showPercent As Boolean = True
'    Private _showPoints As Boolean = True

'    ' ===== HOVER / ANIMAÇÃO =====
'    Private _hoverLine As Integer = -1
'    Private _hoverGrow As Single = 0F
'    Private Const HoverMax As Single = 3.0F
'    Private WithEvents _timer As New Timer With {.Interval = 16}

'    ' ===== MARGENS =====
'    Private Const ML As Integer = 45
'    Private Const MR As Integer = 10
'    Private Const MT As Integer = 10
'    Private Const MB As Integer = 35

'    ' ===== PROPRIEDADES =====

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As List(Of Decimal())
'        Get
'            Return _values
'        End Get
'        Set(value As List(Of Decimal()))
'            _values = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Labels As String()
'        Get
'            Return _labels
'        End Get
'        Set(value As String())
'            _labels = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineColors As List(Of Color)
'        Get
'            Return _lineColors
'        End Get
'        Set(value As List(Of Color))
'            _lineColors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineWidths As List(Of Single)
'        Get
'            Return _lineWidths
'        End Get
'        Set(value As List(Of Single))
'            _lineWidths = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGrid As Boolean
'        Get
'            Return _showGrid
'        End Get
'        Set(value As Boolean)
'            _showGrid = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridNumbers As Boolean
'        Get
'            Return _showGridNumbers
'        End Get
'        Set(value As Boolean)
'            _showGridNumbers = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowPercent As Boolean
'        Get
'            Return _showPercent
'        End Get
'        Set(value As Boolean)
'            _showPercent = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowPoints As Boolean
'        Get
'            Return _showPoints
'        End Get
'        Set(value As Boolean)
'            _showPoints = value
'            Invalidate()
'        End Set
'    End Property

'    ' ===== CONSTRUTOR =====
'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw Or
'                 ControlStyles.UserPaint, True)

'        BackColor = Color.White
'        Font = New Font("Segoe UI", 8)
'        Size = New Size(500, 220)

'        _timer.Start()
'    End Sub

'    ' ===== DESENHO =====
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias
'        g.Clear(BackColor)

'        If _showGrid Then DrawGrid(g)

'        For i As Integer = 0 To _values.Count - 1
'            DrawLineSeries(g, i)
'        Next

'        DrawBottomLabels(g)
'    End Sub

'    ' ===== GRID =====
'    Private Sub DrawGrid(g As Graphics)
'        Using p As New Pen(Color.FromArgb(60, Color.Gray))
'            For i As Integer = 0 To _gridLines
'                Dim y As Single = CSng(MT + (Height - MT - MB) * i / _gridLines)
'                g.DrawLine(p, ML, y, Width - MR, y)

'                If _showGridNumbers Then
'                    Dim v As Integer = CInt(_maxValue - (_maxValue / _gridLines) * i)
'                    g.DrawString(v.ToString(), Font, Brushes.Gray, 2, y - 6)
'                End If
'            Next
'        End Using
'    End Sub

'    ' ===== LINHA + PONTOS =====
'    Private Sub DrawLineSeries(g As Graphics, index As Integer)

'        Dim data = _values(index)
'        Dim w = Width - ML - MR
'        Dim h = Height - MT - MB
'        Dim stepX = w / (data.Length - 1)

'        Dim pts(data.Length - 1) As PointF

'        For i As Integer = 0 To data.Length - 1
'            pts(i) = New PointF(
'                CSng(ML + i * stepX),
'                CSng(MT + (1 - data(i) / _maxValue) * h)
'            )
'        Next

'        Dim grow = If(index = _hoverLine, _hoverGrow, 0)

'        Using p As New Pen(_lineColors(index), _lineWidths(index) + grow)
'            g.DrawLines(p, pts)
'        End Using

'        If _showPoints Then
'            For i As Integer = 0 To pts.Length - 1
'                Dim r As Single = 4 + grow
'                g.FillEllipse(New SolidBrush(_lineColors(index)),
'                              pts(i).X - r, pts(i).Y - r, r * 2, r * 2)

'                If _showPercent Then
'                    Dim pct = CInt((data(i) / _maxValue) * 100)
'                    g.DrawString(pct & "%", Font, Brushes.Black,
'                                 pts(i).X - 10, pts(i).Y - 18)
'                End If
'            Next
'        End If
'    End Sub

'    ' ===== LABELS =====
'    Private Sub DrawBottomLabels(g As Graphics)
'        Dim stepX As Single = (Width - ML - MR) / (_labels.Length - 1)
'        For i As Integer = 0 To _labels.Length - 1
'            g.DrawString(_labels(i), Font, Brushes.Gray,
'                         CSng(ML + i * stepX - 15),
'                         Height - MB + 5)
'        Next
'    End Sub

'    ' ===== HOVER =====
'    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
'        _hoverLine = HitTest(e.Location)
'    End Sub

'    Private Function HitTest(pt As Point) As Integer
'        For i As Integer = 0 To _values.Count - 1
'            Dim data = _values(i)
'            Dim w = Width - ML - MR
'            Dim h = Height - MT - MB
'            Dim stepX = w / (data.Length - 1)

'            For j As Integer = 0 To data.Length - 2
'                Dim p1 As New PointF(
'                    CSng(ML + j * stepX),
'                    CSng(MT + (1 - data(j) / _maxValue) * h)
'                )
'                Dim p2 As New PointF(
'                    CSng(ML + (j + 1) * stepX),
'                    CSng(MT + (1 - data(j + 1) / _maxValue) * h)
'                )

'                Using gp As New GraphicsPath()
'                    gp.AddLine(p1, p2)
'                    Using pen As New Pen(Color.Black, 8)
'                        If gp.IsOutlineVisible(pt, pen) Then Return i
'                    End Using
'                End Using
'            Next
'        Next
'        Return -1
'    End Function

'    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
'        If _hoverLine >= 0 Then
'            _hoverGrow = Math.Min(_hoverGrow + 0.4F, HoverMax)
'        Else
'            _hoverGrow = Math.Max(_hoverGrow - 0.4F, 0)
'        End If
'        Invalidate()
'    End Sub

'End Class

'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<DesignTimeVisible(True)>
'Public Class CharLine
'    Inherits Control

'    ' ===== DADOS =====
'    Private _values As New List(Of Decimal()) From {
'        New Decimal() {230D, 280D, 200D, 240D, 150D, 220D, 170D},
'        New Decimal() {180D, 200D, 170D, 195D, 140D, 150D, 160D},
'        New Decimal() {60D, 90D, 10D, 20D, 40D, 35D, 30D}
'    }

'    Private _labels As String() =
'        {"January", "March", "April", "June", "July", "October", "December"}

'    ' ===== APARÊNCIA =====
'    Private _lineColors As New List(Of Color) From {
'        Color.FromArgb(40, 30, 90),
'        Color.Olive,
'        Color.Red
'    }

'    Private _fillColors As New List(Of Color) From {
'        Color.FromArgb(120, 40, 30, 90),
'        Color.FromArgb(120, 130, 140, 0),
'        Color.FromArgb(120, 255, 0, 0)
'    }

'    Private _lineWidths As New List(Of Single) From {2.5F, 2.0F, 2.0F}

'    Private _maxValue As Decimal = 300D
'    Private _gridLines As Integer = 6
'    Private _showGrid As Boolean = True
'    Private _showGridNumbers As Boolean = True

'    ' ===== PORCENTAGEM =====
'    Private _showPercent As Boolean = True
'    Private _percentFormat As String = "{0}%"

'    ' ===== HOVER / ANIMAÇÃO =====
'    Private _hoverLine As Integer = -1
'    Private _hoverGrow As Single = 0F
'    Private _hoverTarget As Single = 3.0F
'    Private WithEvents _timer As New Timer With {.Interval = 16}

'    ' ===== MARGENS =====
'    Private Const ML As Integer = 45
'    Private Const MR As Integer = 10
'    Private Const MT As Integer = 10
'    Private Const MB As Integer = 35

'    ' ===== PROPRIEDADES =====

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As List(Of Decimal())
'        Get
'            Return _values
'        End Get
'        Set(value As List(Of Decimal()))
'            _values = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowPercent As Boolean
'        Get
'            Return _showPercent
'        End Get
'        Set(value As Boolean)
'            _showPercent = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property PercentFormat As String
'        Get
'            Return _percentFormat
'        End Get
'        Set(value As String)
'            _percentFormat = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineWidths As List(Of Single)
'        Get
'            Return _lineWidths
'        End Get
'        Set(value As List(Of Single))
'            _lineWidths = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGrid As Boolean
'        Get
'            Return _showGrid
'        End Get
'        Set(value As Boolean)
'            _showGrid = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridNumbers As Boolean
'        Get
'            Return _showGridNumbers
'        End Get
'        Set(value As Boolean)
'            _showGridNumbers = value
'            Invalidate()
'        End Set
'    End Property

'    ' ===== CONSTRUTOR =====
'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw Or
'                 ControlStyles.UserPaint, True)

'        BackColor = Color.White
'        Font = New Font("Segoe UI", 8)
'        Size = New Size(500, 220)

'        _timer.Start()
'    End Sub

'    ' ===== DESENHO =====
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias
'        g.Clear(BackColor)

'        If _showGrid Then DrawGrid(g)

'        For i As Integer = 0 To _values.Count - 1
'            DrawAreaLine(g, i)
'        Next

'        DrawBottomLabels(g)
'    End Sub

'    ' ===== GRID =====
'    Private Sub DrawGrid(g As Graphics)
'        Using p As New Pen(Color.FromArgb(60, Color.Gray))
'            For i = 0 To _gridLines
'                Dim y As Single = MT + (Height - MT - MB) * i / _gridLines
'                g.DrawLine(p, ML, y, Width - MR, y)

'                If _showGridNumbers Then
'                    Dim val = CInt(_maxValue - (_maxValue / _gridLines) * i)
'                    g.DrawString(val.ToString(), Font, Brushes.Gray, 2, y - 6)
'                End If
'            Next
'        End Using
'    End Sub

'    ' ===== ÁREA + LINHA =====
'    Private Sub DrawAreaLine(g As Graphics, index As Integer)
'        Dim data = _values(index)

'        Dim w As Single = Width - ML - MR
'        Dim h As Single = Height - MT - MB
'        Dim stepX As Single = w / (data.Length - 1)

'        Dim pts(data.Length - 1) As PointF

'        For i = 0 To data.Length - 1
'            pts(i) = New PointF(
'                ML + i * stepX,
'                MT + (1 - data(i) / _maxValue) * h
'            )
'        Next

'        Using gp As New GraphicsPath()
'            gp.AddLines(pts)
'            gp.AddLine(pts.Last.X, pts.Last.Y, pts.Last.X, Height - MB)
'            gp.AddLine(pts.Last.X, Height - MB, pts(0).X, Height - MB)
'            gp.CloseFigure()

'            Using b As New SolidBrush(_fillColors(index))
'                g.FillPath(b, gp)
'            End Using
'        End Using

'        Dim grow As Single = If(index = _hoverLine, _hoverGrow, 0)

'        Using p As New Pen(_lineColors(index), _lineWidths(index) + grow)
'            g.DrawLines(p, pts)
'        End Using

'        ' ===== PORCENTAGEM =====
'        If _showPercent Then
'            For i = 0 To pts.Length - 1
'                Dim percent = CInt((data(i) / _maxValue) * 100)
'                Dim txt = String.Format(_percentFormat, percent)
'                g.DrawString(txt, Font, Brushes.Black, pts(i).X - 10, pts(i).Y - 18)
'            Next
'        End If
'    End Sub

'    ' ===== LABELS =====
'    Private Sub DrawBottomLabels(g As Graphics)
'        Dim stepX As Single = (Width - ML - MR) / (_labels.Length - 1)
'        For i = 0 To _labels.Length - 1
'            g.DrawString(_labels(i), Font, Brushes.Gray,
'                         ML + i * stepX - 15, Height - MB + 5)
'        Next
'    End Sub

'    ' ===== HOVER =====
'    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
'        _hoverLine = HitTestLine(e.Location)
'    End Sub

'    Private Function HitTestLine(pt As Point) As Integer
'        For i = 0 To _values.Count - 1
'            Dim data = _values(i)
'            Dim w = Width - ML - MR
'            Dim h = Height - MT - MB
'            Dim stepX = w / (data.Length - 1)

'            For j = 0 To data.Length - 2
'                Dim p1 = New PointF(ML + j * stepX, MT + (1 - data(j) / _maxValue) * h)
'                Dim p2 = New PointF(ML + (j + 1) * stepX, MT + (1 - data(j + 1) / _maxValue) * h)
'                Using pen As New Pen(Color.Black, 6)
'                    If New GraphicsPath({p1, p2}, {Byte.MinValue, Byte.MinValue}).IsOutlineVisible(pt, pen) Then
'                        Return i
'                    End If
'                End Using
'            Next
'        Next
'        Return -1
'    End Function

'    ' ===== ANIMAÇÃO =====
'    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
'        If _hoverLine >= 0 Then
'            _hoverGrow = Math.Min(_hoverGrow + 0.4F, _hoverTarget)
'        Else
'            _hoverGrow = Math.Max(_hoverGrow - 0.4F, 0)
'        End If
'        Invalidate()
'    End Sub

'End Class



'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<DesignTimeVisible(True)>
'Public Class CharLine
'    Inherits Control

'    ' ===== DADOS =====
'    Private _values As New List(Of Decimal()) From {
'        New Decimal() {230D, 280D, 200D, 240D, 150D, 220D, 170D},
'        New Decimal() {180D, 200D, 170D, 195D, 140D, 150D, 160D},
'        New Decimal() {60D, 90D, 10D, 20D, 40D, 35D, 30D}
'    }

'    Private _labels As String() =
'        {"January", "March", "April", "June", "July", "October", "December"}

'    ' ===== APARÊNCIA =====
'    Private _lineColors As New List(Of Color) From {
'        Color.FromArgb(40, 30, 90),
'        Color.Olive,
'        Color.Red
'    }

'    Private _fillColors As New List(Of Color) From {
'        Color.FromArgb(120, 40, 30, 90),
'        Color.FromArgb(120, 130, 140, 0),
'        Color.FromArgb(120, 255, 0, 0)
'    }

'    Private _lineWidths As New List(Of Single) From {2.5F, 2.0F, 2.0F}

'    Private _maxValue As Decimal = 300D
'    Private _gridLines As Integer = 6
'    Private _showGrid As Boolean = True
'    Private _showGridNumbers As Boolean = True

'    ' ===== MARGENS =====
'    Private Const ML As Integer = 45
'    Private Const MR As Integer = 10
'    Private Const MT As Integer = 10
'    Private Const MB As Integer = 35

'    ' ===== PROPRIEDADES =====

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As List(Of Decimal())
'        Get
'            Return _values
'        End Get
'        Set(value As List(Of Decimal()))
'            _values = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineColors As List(Of Color)
'        Get
'            Return _lineColors
'        End Get
'        Set(value As List(Of Color))
'            _lineColors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property FillColors As List(Of Color)
'        Get
'            Return _fillColors
'        End Get
'        Set(value As List(Of Color))
'            _fillColors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineWidths As List(Of Single)
'        Get
'            Return _lineWidths
'        End Get
'        Set(value As List(Of Single))
'            _lineWidths = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGrid As Boolean
'        Get
'            Return _showGrid
'        End Get
'        Set(value As Boolean)
'            _showGrid = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridNumbers As Boolean
'        Get
'            Return _showGridNumbers
'        End Get
'        Set(value As Boolean)
'            _showGridNumbers = value
'            Invalidate()
'        End Set
'    End Property

'    ' ===== CONSTRUTOR =====
'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw Or
'                 ControlStyles.UserPaint, True)

'        BackColor = Color.White
'        Font = New Font("Segoe UI", 8)
'        Size = New Size(500, 220)
'    End Sub

'    ' ===== DESENHO =====
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g As Graphics = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias
'        g.Clear(BackColor)

'        If _showGrid Then
'            DrawGrid(g)
'        End If

'        For i As Integer = 0 To _values.Count - 1
'            DrawAreaLine(
'                g,
'                _values(i),
'                GetSafeColor(_lineColors, i, Color.Blue),
'                GetSafeColor(_fillColors, i, Color.FromArgb(100, Color.Blue)),
'                GetSafeWidth(_lineWidths, i, 2.0F)
'            )
'        Next

'        DrawBottomLabels(g)
'    End Sub

'    ' ===== GRID =====
'    Private Sub DrawGrid(g As Graphics)
'        Using p As New Pen(Color.FromArgb(60, Color.Gray))
'            For i As Integer = 0 To _gridLines
'                Dim y As Single = MT + (Height - MT - MB) * i / _gridLines
'                g.DrawLine(p, ML, y, Width - MR, y)

'                If _showGridNumbers Then
'                    Dim val As Integer = CInt(_maxValue - (_maxValue / _gridLines) * i)
'                    g.DrawString(val.ToString(), Font, Brushes.Gray, 2, y - 6)
'                End If
'            Next
'        End Using
'    End Sub

'    ' ===== ÁREA + LINHA =====
'    Private Sub DrawAreaLine(g As Graphics,
'                             data As Decimal(),
'                             lineColor As Color,
'                             fillColor As Color,
'                             lineWidth As Single)

'        Dim w As Single = Width - ML - MR
'        Dim h As Single = Height - MT - MB
'        Dim stepX As Single = w / (data.Length - 1)

'        Dim pts(data.Length - 1) As PointF

'        For i As Integer = 0 To data.Length - 1
'            Dim x As Single = ML + i * stepX
'            Dim y As Single = MT + (1 - data(i) / _maxValue) * h
'            pts(i) = New PointF(x, y)
'        Next

'        Using gp As New GraphicsPath()
'            gp.AddLines(pts)
'            gp.AddLine(pts(pts.Length - 1).X, pts(pts.Length - 1).Y,
'                       pts(pts.Length - 1).X, Height - MB)
'            gp.AddLine(pts(pts.Length - 1).X, Height - MB,
'                       pts(0).X, Height - MB)
'            gp.CloseFigure()

'            Using b As New SolidBrush(fillColor)
'                g.FillPath(b, gp)
'            End Using
'        End Using

'        Using p As New Pen(lineColor, lineWidth)
'            g.DrawLines(p, pts)
'        End Using
'    End Sub

'    ' ===== LABELS =====
'    Private Sub DrawBottomLabels(g As Graphics)
'        Dim stepX As Single = (Width - ML - MR) / (_labels.Length - 1)
'        For i As Integer = 0 To _labels.Length - 1
'            Dim x As Single = ML + i * stepX
'            g.DrawString(_labels(i), Font, Brushes.Gray, x - 15, Height - MB + 5)
'        Next
'    End Sub

'    ' ===== SAFE HELPERS =====
'    Private Function GetSafeColor(list As List(Of Color), index As Integer, fallback As Color) As Color
'        If index < list.Count Then
'            Return list(index)
'        End If
'        Return fallback
'    End Function

'    Private Function GetSafeWidth(list As List(Of Single), index As Integer, fallback As Single) As Single
'        If index < list.Count Then
'            Return list(index)
'        End If
'        Return fallback
'    End Function

'End Class


'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'<DesignTimeVisible(True)>
'<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
'Public Class CharLine
'    Inherits Control

'    ' ===== DADOS =====
'    Private _values As Decimal() = {20D, 60D, 45D, 80D, 55D, 90D, 30D}
'    Private _gridLineCount As Integer = 5

'    ' ===== APARÊNCIA =====
'    Private _lineColor As Color = Color.Blue
'    Private _gridColor As Color = Color.FromArgb(80, Color.Gray)
'    Private _textColor As Color = Color.DimGray
'    Private _fillColor As Color = Color.FromArgb(80, Color.RoyalBlue)
'    Private _fillAlpha As Integer = 120 ' 0–255

'    Private _maxValue As Decimal = 100D
'    Private _showGrid As Boolean = True

'    ' Margens internas
'    Private Const MarginLeft As Integer = 40
'    Private Const MarginRight As Integer = 10
'    Private Const MarginTop As Integer = 10
'    Private Const MarginBottom As Integer = 30

'    ' ===== PROPRIEDADES =====

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As Decimal()
'        Get
'            Return _values
'        End Get
'        Set(value As Decimal())
'            _values = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property LineColor As Color
'        Get
'            Return _lineColor
'        End Get
'        Set(value As Color)
'            _lineColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property FillColor As Color
'        Get
'            Return _fillColor
'        End Get
'        Set(value As Color)
'            _fillColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridLineColor As Color
'        Get
'            Return _gridColor
'        End Get
'        Set(value As Color)
'            _gridColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextColor As Color
'        Get
'            Return _textColor
'        End Get
'        Set(value As Color)
'            _textColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property MaxValue As Decimal
'        Get
'            Return _maxValue
'        End Get
'        Set(value As Decimal)
'            _maxValue = Math.Max(1D, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridLines As Boolean
'        Get
'            Return _showGrid
'        End Get
'        Set(value As Boolean)
'            _showGrid = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridLineCount As Integer
'        Get
'            Return _gridLineCount
'        End Get
'        Set(value As Integer)
'            _gridLineCount = Math.Max(1, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartLine"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property FillTransparency As Integer
'        Get
'            Return _fillAlpha
'        End Get
'        Set(value As Integer)
'            _fillAlpha = Math.Max(0, Math.Min(255, value))
'            Invalidate()
'        End Set
'    End Property

'    ' ===== CONSTRUTOR =====
'    Public Sub New()
'        SetStyle(ControlStyles.UserPaint Or
'                 ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw Or
'                 ControlStyles.SupportsTransparentBackColor, True)

'        BackColor = Color.Transparent
'        Font = New Font("Segoe UI", 8)
'        Size = New Size(300, 180)
'    End Sub

'    ' ===== DESENHO =====
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        If Width <= 0 OrElse Height <= 0 Then Return

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias

'        ' Fundo transparente real
'        If BackColor <> Color.Transparent Then
'            g.Clear(BackColor)
'        End If

'        Dim data = If(Me.DesignMode OrElse _values Is Nothing OrElse _values.Length = 0,
'                      New Decimal() {10, 40, 30, 70, 50, 90, 20},
'                      _values)

'        DrawGrid(g)
'        DrawLineAndFill(g, data)
'    End Sub

'    ' ===== GRID =====
'    Private Sub DrawGrid(g As Graphics)
'        If Not _showGrid Then Return

'        Using pen As New Pen(_gridColor, 1),
'              txtBrush As New SolidBrush(_textColor)

'            For i = 0 To _gridLineCount
'                Dim ratio As Single = CSng(i / CSng(_gridLineCount))
'                Dim y As Integer = CInt(MarginTop + (1 - ratio) * (Height - MarginTop - MarginBottom))

'                g.DrawLine(pen,
'                           MarginLeft, y,
'                           Width - MarginRight, y)

'                Dim valueLabel As String = CInt(_maxValue * ratio).ToString()
'                Dim size = g.MeasureString(valueLabel, Font)

'                g.DrawString(valueLabel, Font, txtBrush,
'                             MarginLeft - size.Width - 4,
'                             y - size.Height / 2)
'            Next
'        End Using
'    End Sub

'    ' ===== LINHA + ÁREA =====
'    Private Sub DrawLineAndFill(g As Graphics, data As Decimal())
'        Dim count = data.Length
'        If count < 2 Then Return

'        Dim chartWidth = Width - MarginLeft - MarginRight
'        Dim chartHeight = Height - MarginTop - MarginBottom
'        Dim stepX As Single = chartWidth / (count - 1)

'        Dim points(count - 1) As PointF

'        For i = 0 To count - 1
'            Dim x As Single = MarginLeft + i * stepX
'            Dim y As Single = MarginTop + (1 - CSng(data(i) / _maxValue)) * chartHeight
'            points(i) = New PointF(x, y)
'        Next

'        ' Área preenchida
'        Using fillPath As New GraphicsPath()
'            fillPath.AddLines(points)
'            fillPath.AddLine(points.Last.X, points.Last.Y,
'                             points.Last.X, Height - MarginBottom)
'            fillPath.AddLine(points.Last.X, Height - MarginBottom,
'                             points.First.X, Height - MarginBottom)
'            fillPath.CloseFigure()

'            Using fillBrush As New SolidBrush(Color.FromArgb(_fillAlpha, _fillColor))
'                g.FillPath(fillBrush, fillPath)
'            End Using
'        End Using

'        ' Linha
'        Using pen As New Pen(_lineColor, 2)
'            g.DrawLines(pen, points)
'        End Using
'    End Sub

'    ' ===== TRANSPARÊNCIA REAL =====
'    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
'        If BackColor = Color.Transparent AndAlso Parent IsNot Nothing Then
'            Dim g = pevent.Graphics
'            Dim state = g.Save()

'            g.TranslateTransform(-Left, -Top)
'            InvokePaintBackground(Parent, pevent)
'            InvokePaint(Parent, pevent)

'            g.Restore(state)
'        Else
'            MyBase.OnPaintBackground(pevent)
'        End If
'    End Sub

'End Class

