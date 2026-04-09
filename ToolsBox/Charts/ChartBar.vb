Imports System.ComponentModel
Imports System.Drawing.Drawing2D

<ToolboxBitmap(GetType(ChartBar), "Pink.ico")>
<DesignTimeVisible(True)>
Public Class ChartBar
    Inherits Control

    ' ================== DADOS ==================
    Private _values As Decimal() = {60, 45, 80, 40, 55, 70}
    Private _labels As String() = Nothing
    Private _topTexts As String() = Nothing
    Private _showTopText As Boolean = True
    Private _showInnerPercent As Boolean = True
    Private _barColors As Color() = Nothing

    ' ================== APARÊNCIA ==================
    Private _barColor As Color = Color.RoyalBlue
    Private _gridColor As Color = Color.FromArgb(70, Color.Silver)
    Private _textColor As Color = Color.DimGray

    Private _barSpacing As Integer = 10
    Private _maxValue As Integer = 100

    ' ================== GRID ==================
    Private _showGridLines As Boolean = True
    Private _showGridValues As Boolean = True
    Private _gridLineCount As Integer = 5
    Private _gridTextColor As Color = Color.Gray
    Private _gridFont As Font = New Font("Segoe UI", 6, FontStyle.Regular)
    Private _gridPaddingLeft As Integer = 35

    ' ================== ANIMAÇÃO ==================
    Private _animatedValues() As Single
    Private _animationTimer As Timer
    Private _animationSpeed As Single = 0.2F

    ' ================== HOVER ==================
    Private _hoverIndex As Integer = -1
    Private _enableHoverAnimation As Boolean = True
    Private _explodeOffset As Integer = 2

    ' ================== MARGENS FIXAS ==================
    Private Const TopPadding As Integer = 15
    Private Const BottomPadding As Integer = 20

    ' ================== PROPRIEDADES ==================
    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Values As Decimal()
        Get
            Return _values
        End Get
        Set(value As Decimal())
            If value Is Nothing OrElse value.Length = 0 Then Return

            _values = value

            If _animatedValues Is Nothing OrElse _animatedValues.Length <> value.Length Then
                ReDim _animatedValues(value.Length - 1)
                For i = 0 To value.Length - 1
                    _animatedValues(i) = value(i)
                Next
            End If

            _animationTimer.Start()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Labels As String()
        Get
            Return _labels
        End Get
        Set(value As String())
            _labels = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BarColor As Color
        Get
            Return _barColor
        End Get
        Set(value As Color)
            _barColor = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridLineColor As Color
        Get
            Return _gridColor
        End Get
        Set(value As Color)
            _gridColor = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(value As Color)
            _textColor = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridTextColor As Color
        Get
            Return _gridTextColor
        End Get
        Set(value As Color)
            _gridTextColor = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridFont As Font
        Get
            Return _gridFont
        End Get
        Set(value As Font)
            _gridFont = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridLineCount As Integer
        Get
            Return _gridLineCount
        End Get
        Set(value As Integer)
            _gridLineCount = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowGridLines As Boolean
        Get
            Return _showGridLines
        End Get
        Set(value As Boolean)
            _showGridLines = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowGridValues As Boolean
        Get
            Return _showGridValues
        End Get
        Set(value As Boolean)
            _showGridValues = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GridPaddingLeft As Integer
        Get
            Return _gridPaddingLeft
        End Get
        Set(value As Integer)
            _gridPaddingLeft = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BarSpacing As Integer
        Get
            Return _barSpacing
        End Get
        Set(value As Integer)
            _barSpacing = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property MaxValue As Integer
        Get
            Return _maxValue
        End Get
        Set(value As Integer)
            _maxValue = Math.Max(1, value)
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property AnimationSpeed As Single
        Get
            Return _animationSpeed
        End Get
        Set(value As Single)
            _animationSpeed = Math.Max(0.01F, Math.Min(1.0F, value))
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TopTexts As String()
        Get
            Return _topTexts
        End Get
        Set(value As String())
            _topTexts = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowTopText As Boolean
        Get
            Return _showTopText
        End Get
        Set(value As Boolean)
            _showTopText = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowInnerPercentage As Boolean
        Get
            Return _showInnerPercent
        End Get
        Set(value As Boolean)
            _showInnerPercent = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BarColors As Color()
        Get
            Return _barColors
        End Get
        Set(value As Color())
            _barColors = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableHoverAnimation As Boolean
        Get
            Return _enableHoverAnimation
        End Get
        Set(value As Boolean)
            _enableHoverAnimation = value
            Invalidate()
        End Set
    End Property

    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ExplodeOffset As Integer
        Get
            Return _explodeOffset
        End Get
        Set(value As Integer)
            _explodeOffset = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    ' ================== CONSTRUTOR ==================
    Public Sub New()
        SetStyle(ControlStyles.UserPaint Or
                 ControlStyles.AllPaintingInWmPaint Or
                         ControlStyles.OptimizedDoubleBuffer Or
                         ControlStyles.ResizeRedraw, True)

        DoubleBuffered = True
        ResizeRedraw = True

        Font = New Font("Segoe UI", 6, FontStyle.Bold)
        Me.MinimumSize = New Size(80, 70)
        Size = New Size(260, 150)
        TopTexts = {"U$100", "U$300", "U$1000", "U$900", "U$600", "U$800"}
        BarColors = {Color.Red, Color.Green, Color.Blue}

        ReDim _animatedValues(_values.Length - 1)
        For i = 0 To _values.Length - 1
            _animatedValues(i) = _values(i)
        Next

        _animationTimer = New Timer With {.Interval = 16}
        AddHandler _animationTimer.Tick, AddressOf AnimateStep

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

        DrawGrid(g)
        DrawBars(g)
    End Sub

    Private Sub DrawGrid(g As Graphics)
        If Not _showGridLines Then Return

        Dim usableHeight = Height - TopPadding - BottomPadding

        Using pen As New Pen(_gridColor, 1),
              textBrush As New SolidBrush(_gridTextColor)

            For i = 0 To _gridLineCount
                Dim percent = i / _gridLineCount
                Dim y As Integer = CInt(TopPadding + usableHeight - (percent * usableHeight))

                g.DrawLine(pen, _gridPaddingLeft, y, Width, y)

                If _showGridValues Then
                    Dim value = CInt(_maxValue * percent)
                    Dim txt = value.ToString()
                    Dim size = g.MeasureString(txt, _gridFont)

                    Dim textY As Integer = CInt(y - size.Height / 2)

                    If textY < TopPadding Then textY = TopPadding
                    If textY + size.Height > Height - BottomPadding Then
                        textY = Height - BottomPadding - CInt(size.Height)
                    End If

                    g.DrawString(txt, _gridFont, textBrush,
                                 _gridPaddingLeft - size.Width - 5,
                                 textY)
                End If
            Next

        End Using
    End Sub


    Private Sub DrawBars(g As Graphics)

        Dim count = Math.Min(_values.Length, _animatedValues.Length)
        Dim totalSpacing = _barSpacing * (count + 1)
        Dim usableWidth = Width - _gridPaddingLeft
        Dim usableHeight = Height - TopPadding - BottomPadding

        Dim barWidth = Math.Max(5, (usableWidth - totalSpacing) \ count)

        Using barBrush As New SolidBrush(_barColor),
          textBrush As New SolidBrush(_textColor)

            For i = 0 To count - 1

                Dim value = Math.Min(_animatedValues(i), _maxValue)
                Dim barHeight = CInt((value / _maxValue) * usableHeight)

                Dim x = _gridPaddingLeft + _barSpacing + i * (barWidth + _barSpacing)
                Dim y = TopPadding + usableHeight - barHeight

                ' ===== COPIAS LOCAIS (NÃO AFETA OUTRAS BARRAS) =====
                Dim drawX = x
                Dim drawY = y
                Dim drawWidth = barWidth
                Dim drawHeight = barHeight

                ' ===== ANIMAÇÃO NO HOVER (EXPANSÃO, NÃO SALTO) =====
                If _enableHoverAnimation AndAlso i = _hoverIndex Then
                    Dim expand = _explodeOffset
                    drawX -= expand \ 2
                    drawY -= expand
                    drawWidth += expand
                    drawHeight += expand
                End If

                ' ===== COR INDIVIDUAL =====
                Dim barColorToUse As Color = _barColor
                If _barColors IsNot Nothing AndAlso i < _barColors.Length Then
                    barColorToUse = _barColors(i)
                End If

                Using barBrush2 As New SolidBrush(barColorToUse)
                    g.FillRectangle(barBrush2, drawX, y, drawWidth, barHeight)
                End Using


                ' ===== TEXTO EM CIMA (STRING) =====
                If _showTopText AndAlso _topTexts IsNot Nothing AndAlso i < _topTexts.Length Then
                    Dim topText = _topTexts(i)
                    Dim topSize = g.MeasureString(topText, Font)

                    g.DrawString(topText, Font, textBrush,
                    drawX + (drawWidth - topSize.Width) / 2,
                    drawY - topSize.Height - 2)
                End If

                ' ===== PORCENTAGEM DENTRO DA BARRA =====
                If _showInnerPercent Then
                    Dim percentValue As Integer = CInt((value / _maxValue) * 100)
                    Dim percentText As String = percentValue.ToString() & "%"

                    Dim percentSize = g.MeasureString(percentText, Font)

                    Dim px = drawX + (drawWidth - percentSize.Width) / 2
                    Dim py = drawY + (drawHeight - percentSize.Height) / 2

                    If py >= TopPadding Then
                        g.DrawString(percentText, Font, Brushes.White, px, py)
                    End If
                End If

                ' ===== LABEL (EMBAIXO DA BARRA) =====
                If _labels IsNot Nothing AndAlso i < _labels.Length Then
                    Dim labelText = _labels(i)
                    Dim labelSize = g.MeasureString(labelText, Font)

                    g.DrawString(labelText, Font, textBrush,
                    drawX + (drawWidth - labelSize.Width) / 2,
                    TopPadding + usableHeight + 2)
                End If

            Next
        End Using
    End Sub


    Private Sub AnimateStep(sender As Object, e As EventArgs)
        Dim finished As Boolean = True

        For i = 0 To _values.Length - 1
            Dim delta = (_values(i) - _animatedValues(i)) * _animationSpeed

            If Math.Abs(delta) > 0.5F Then
                _animatedValues(i) += delta
                finished = False
            Else
                _animatedValues(i) = _values(i)
            End If
        Next

        Invalidate()

        If finished Then _animationTimer.Stop()
    End Sub

    ' ================== MOUSE ==================
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Dim index = HitTestBar(e.Location)
        If index <> _hoverIndex Then
            _hoverIndex = index
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _hoverIndex = -1
        Invalidate()
    End Sub

    Private Function HitTestBar(p As Point) As Integer
        Dim usableWidth = Width - _gridPaddingLeft
        Dim count = _values.Length
        Dim barWidth = (usableWidth - _barSpacing * (count + 1)) \ count

        For i = 0 To count - 1
            Dim x = _gridPaddingLeft + _barSpacing + i * (barWidth + _barSpacing)
            If p.X >= x AndAlso p.X <= x + barWidth Then Return i
        Next
        Return -1
    End Function
End Class



'<ToolboxBitmap(GetType(GroupBox), "PerformanceCounter")>
'Public Class ChartBar
'    Inherits Control

'    ' ================== DADOS ==================
'    Private _values As Integer() = {60, 45, 80, 40, 55, 70}
'    Private _labels As String() = Nothing
'    Private _topTexts As String() = Nothing
'    Private _showTopText As Boolean = True
'    Private _showInnerPercent As Boolean = True
'    Private _barColors As Color() = Nothing

'    ' ================== APARÊNCIA ==================
'    Private _barColor As Color = Color.RoyalBlue
'    Private _gridColor As Color = Color.FromArgb(70, Color.Silver)
'    Private _textColor As Color = Color.DimGray

'    Private _barSpacing As Integer = 10
'    Private _maxValue As Integer = 100

'    ' ================== GRID ==================
'    Private _showGridLines As Boolean = True
'    Private _showGridValues As Boolean = True
'    Private _gridLineCount As Integer = 5
'    Private _gridTextColor As Color = Color.Gray
'    Private _gridFont As Font = New Font("Segoe UI", 8, FontStyle.Regular)
'    Private _gridPaddingLeft As Integer = 35

'    ' ================== ANIMAÇÃO ==================
'    Private _animatedValues() As Single
'    Private _animationTimer As Timer
'    Private _animationSpeed As Single = 0.2F

'    ' ================== HOVER ==================
'    Private _hoverIndex As Integer = -1
'    Private _enableHoverAnimation As Boolean = True
'    Private _explodeOffset As Integer = 8

'    ' ================== MARGENS FIXAS ==================
'    Private Const TopPadding As Integer = 15
'    Private Const BottomPadding As Integer = 20

'    ' ================== PROPRIEDADES ==================
'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Values As Integer()
'        Get
'            Return _values
'        End Get
'        Set(value As Integer())
'            If value Is Nothing OrElse value.Length = 0 Then Return

'            _values = value

'            If _animatedValues Is Nothing OrElse _animatedValues.Length <> value.Length Then
'                ReDim _animatedValues(value.Length - 1)
'            End If

'            For i = 0 To value.Length - 1
'                _animatedValues(i) = value(i)
'            Next

'            _animationTimer.Start()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property Labels As String()
'        Get
'            Return _labels
'        End Get
'        Set(value As String())
'            _labels = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property BarColor As Color
'        Get
'            Return _barColor
'        End Get
'        Set(value As Color)
'            _barColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property BarColors As Color()
'        Get
'            Return _barColors
'        End Get
'        Set(value As Color())
'            _barColors = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridLineColor As Color
'        Get
'            Return _gridColor
'        End Get
'        Set(value As Color)
'            _gridColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TextColor As Color
'        Get
'            Return _textColor
'        End Get
'        Set(value As Color)
'            _textColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridTextColor As Color
'        Get
'            Return _gridTextColor
'        End Get
'        Set(value As Color)
'            _gridTextColor = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridFont As Font
'        Get
'            Return _gridFont
'        End Get
'        Set(value As Font)
'            _gridFont = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridLineCount As Integer
'        Get
'            Return _gridLineCount
'        End Get
'        Set(value As Integer)
'            _gridLineCount = Math.Max(1, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridLines As Boolean
'        Get
'            Return _showGridLines
'        End Get
'        Set(value As Boolean)
'            _showGridLines = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowGridValues As Boolean
'        Get
'            Return _showGridValues
'        End Get
'        Set(value As Boolean)
'            _showGridValues = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property GridPaddingLeft As Integer
'        Get
'            Return _gridPaddingLeft
'        End Get
'        Set(value As Integer)
'            _gridPaddingLeft = Math.Max(0, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property BarSpacing As Integer
'        Get
'            Return _barSpacing
'        End Get
'        Set(value As Integer)
'            _barSpacing = Math.Max(0, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property MaxValue As Integer
'        Get
'            Return _maxValue
'        End Get
'        Set(value As Integer)
'            _maxValue = Math.Max(1, value)
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property AnimationSpeed As Single
'        Get
'            Return _animationSpeed
'        End Get
'        Set(value As Single)
'            _animationSpeed = Math.Max(0.01F, Math.Min(1.0F, value))
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property TopTexts As String()
'        Get
'            Return _topTexts
'        End Get
'        Set(value As String())
'            _topTexts = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowTopText As Boolean
'        Get
'            Return _showTopText
'        End Get
'        Set(value As Boolean)
'            _showTopText = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ShowInnerPercentage As Boolean
'        Get
'            Return _showInnerPercent
'        End Get
'        Set(value As Boolean)
'            _showInnerPercent = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property EnableHoverAnimation As Boolean
'        Get
'            Return _enableHoverAnimation
'        End Get
'        Set(value As Boolean)
'            _enableHoverAnimation = value
'            Invalidate()
'        End Set
'    End Property

'    <Category("ChartBar"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
'    Public Property ExplodeOffset As Integer
'        Get
'            Return _explodeOffset
'        End Get
'        Set(value As Integer)
'            _explodeOffset = Math.Max(0, value)
'            Invalidate()
'        End Set
'    End Property

'    ' ================== CONSTRUTOR ==================
'    Public Sub New()
'        SetStyle(ControlStyles.UserPaint Or
'                 ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw, True)

'        DoubleBuffered = True
'        ResizeRedraw = True

'        Font = New Font("Segoe UI", 6, FontStyle.Bold)
'        Size = New Size(260, 150)
'        TopTexts = {"U$100", "U$300", "U$1000", "U$900", "U$600", "U$800"}


'        ReDim _animatedValues(_values.Length - 1)
'        For i = 0 To _values.Length - 1
'            _animatedValues(i) = _values(i)
'        Next

'        _animationTimer = New Timer With {.Interval = 16}
'        AddHandler _animationTimer.Tick, AddressOf AnimateStep
'    End Sub

'    ' ================== DESENHO ==================
'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)
'        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
'        DrawGrid(e.Graphics)
'        DrawBars(e.Graphics)
'    End Sub

'    ' ===== GRID =====
'    Private Sub DrawGrid(g As Graphics)
'        If Not _showGridLines Then Return

'        Dim usableHeight = Height - TopPadding - BottomPadding

'        Using pen As New Pen(_gridColor, 1),
'              textBrush As New SolidBrush(_gridTextColor)

'            For i = 0 To _gridLineCount
'                Dim percent = i / _gridLineCount
'                Dim y = CInt(TopPadding + usableHeight - (percent * usableHeight))

'                g.DrawLine(pen, _gridPaddingLeft, y, Width, y)

'                If _showGridValues Then
'                    Dim value = CInt(_maxValue * percent)
'                    Dim txt = value.ToString()
'                    Dim size = g.MeasureString(txt, _gridFont)

'                    g.DrawString(txt, _gridFont, textBrush,
'                                 _gridPaddingLeft - size.Width - 5,
'                                 y - size.Height / 2)
'                End If
'            Next
'        End Using
'    End Sub

'    ' ===== BARRAS =====
'    Private Sub DrawBars(g As Graphics)
'        Dim count = _values.Length
'        Dim usableHeight = Height - TopPadding - BottomPadding
'        Dim usableWidth = Width - _gridPaddingLeft
'        Dim barWidth = (usableWidth - _barSpacing * (count + 1)) \ count

'        For i = 0 To count - 1
'            Dim value = Math.Min(_animatedValues(i), _maxValue)
'            Dim baseHeight = CSng((value / _maxValue) * usableHeight)

'            Dim extra As Single = 0
'            If _enableHoverAnimation AndAlso i = _hoverIndex Then
'                extra = Math.Min(_explodeOffset, usableHeight - baseHeight)
'            End If

'            Dim finalHeight = baseHeight + extra
'            Dim x = _gridPaddingLeft + _barSpacing + i * (barWidth + _barSpacing)
'            Dim y = TopPadding + usableHeight - finalHeight

'            Dim c As Color = If(_barColors IsNot Nothing AndAlso i < _barColors.Length,
'                                _barColors(i), _barColor)

'            Using br As New SolidBrush(c)
'                g.FillRectangle(br, x, y, barWidth, finalHeight)
'            End Using
'        Next
'    End Sub
'    ' ================== ANIMAÇÃO ==================
'    Private Sub AnimateStep(sender As Object, e As EventArgs)
'        For i = 0 To _values.Length - 1
'            _animatedValues(i) += (_values(i) - _animatedValues(i)) * _animationSpeed
'        Next
'        Invalidate()
'    End Sub



'    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
'        MyBase.OnMouseMove(e)

'        If Not _enableHoverAnimation Then Return

'        Dim count = _values.Length
'        Dim totalSpacing = _barSpacing * (count + 1)
'        Dim usableWidth = Width - _gridPaddingLeft
'        Dim usableHeight = Height - TopPadding - BottomPadding
'        Dim barWidth = Math.Max(5, (usableWidth - totalSpacing) \ count)

'        Dim newHover As Integer = -1

'        For i = 0 To count - 1
'            Dim value = Math.Min(_animatedValues(i), _maxValue)
'            Dim barHeight = CInt((value / _maxValue) * usableHeight)

'            Dim x = _gridPaddingLeft + _barSpacing + i * (barWidth + _barSpacing)
'            Dim y = TopPadding + usableHeight - barHeight

'            Dim rect As New Rectangle(x, y, barWidth, barHeight)

'            If rect.Contains(e.Location) Then
'                newHover = i
'                Exit For
'            End If
'        Next

'        If newHover <> _hoverIndex Then
'            _hoverIndex = newHover
'            Invalidate()
'        End If
'    End Sub
'    Protected Overrides Sub OnMouseLeave(e As EventArgs)
'        MyBase.OnMouseLeave(e)
'        _hoverIndex = -1
'        Invalidate()
'    End Sub

'End Class



