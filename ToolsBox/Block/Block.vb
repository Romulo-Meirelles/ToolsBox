Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<ToolboxBitmap(GetType(BlockGauge), "Orange.ico")>
<DesignTimeVisible(True)>
Public Class BlockGauge
    Inherits Control

    ' ===== VALOR =====
    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 100
    Private _Value As Decimal = 40

    ' ===== VISIBILIDADE =====
    Private _ShowTopText As Boolean = True
    Private _ShowCenterText As Boolean = True
    Private _ShowBottomText As Boolean = True
    Private _ShowBottomIcon As Boolean = True
    Private _Icon As Image

    ' ===== CORES =====
    Private _ShowShadow As Boolean = True
    Private _ShadowOffset As Integer = 6
    Private _ShadowColor As Color = Color.FromArgb(120, 0, 0, 0)
    Private _ShowCube As Boolean = True
    ' Private _CubeDepth As Integer = 10
    Private _CubeBackColor As Color = Color.FromArgb(64, 64, 64)
    Private _CubeDirection As CubeDirection = CubeDirection.LeftTop
    Private _TopTextColor As Color = Color.FromArgb(64, 64, 64)
    Private _CenterTextColor As Color = Color.White
    Private _BottomTextColor As Color = Color.LightGray
    Private _CubeBackDepth As Integer = 5
    Private _CubeSpacing As Integer = 15

    ' ===== MOUSE / ANIMAÇÃO =====
    Private _EnableHoverAnimation As Boolean = True
    Private WithEvents _AnimTimer As New Timer() With {.Interval = 15}
    Private _HoverProgress As Single = 0.0F
    Private _HoverTarget As Single = 0.0F

    Public Event CubeClick(sender As Object, cube As GaugeCubeItem, index As Integer)

    ' ===== TEXTOS =====
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TopText As String = "932252"
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CenterText As String = "0,006 BTC"
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BottomText As String = "773 transações"

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BlockBackColor As Color = Color.FromArgb(40, 40, 40)
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GradientStart As Color = Color.FromArgb(0, 120, 255)
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property GradientEnd As Color = Color.FromArgb(120, 0, 255)

    ' ===== PROPRIEDADES =====

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableHoverAnimation As Boolean
        Get
            Return _EnableHoverAnimation
        End Get
        Set(value As Boolean)
            _EnableHoverAnimation = value

            If Not value Then
                _HoverTarget = 0.0F
                _HoverProgress = 0.0F
                _AnimTimer.Stop()
            End If

            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CubeBackDepth As Integer
        Get
            Return _CubeBackDepth
        End Get
        Set(value As Integer)
            _CubeBackDepth = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TopTextColor As Color
        Get
            Return _TopTextColor
        End Get
        Set(value As Color)
            _TopTextColor = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CenterTextColor As Color
        Get
            Return _CenterTextColor
        End Get
        Set(value As Color)
            _CenterTextColor = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BottomTextColor As Color
        Get
            Return _BottomTextColor
        End Get
        Set(value As Color)
            _BottomTextColor = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Minimum As Integer
        Get
            Return _Minimum
        End Get
        Set(value As Integer)
            _Minimum = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowCube As Boolean
        Get
            Return _ShowCube
        End Get
        Set(value As Boolean)
            _ShowCube = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CubeBackColor As Color
        Get
            Return _CubeBackColor
        End Get
        Set(value As Color)
            _CubeBackColor = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CubeDirection As CubeDirection
        Get
            Return _CubeDirection
        End Get
        Set(value As CubeDirection)
            _CubeDirection = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Maximum As Integer
        Get
            Return _Maximum
        End Get
        Set(value As Integer)
            _Maximum = Math.Max(_Minimum + 1, value)
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowShadow As Boolean
        Get
            Return _ShowShadow
        End Get
        Set(value As Boolean)
            _ShowShadow = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Value As Decimal
        Get
            Return _Value
        End Get
        Set(value As Decimal)
            _Value = Math.Max(_Minimum, Math.Min(value, _Maximum))
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowTopText As Boolean
        Get
            Return _ShowTopText
        End Get
        Set(value As Boolean)
            _ShowTopText = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowCenterText As Boolean
        Get
            Return _ShowCenterText
        End Get
        Set(value As Boolean)
            _ShowCenterText = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowBottomText As Boolean
        Get
            Return _ShowBottomText
        End Get
        Set(value As Boolean)
            _ShowBottomText = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ShowBottomIcon As Boolean
        Get
            Return _ShowBottomIcon
        End Get
        Set(value As Boolean)
            _ShowBottomIcon = value
            Invalidate()
        End Set
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property BottomIcon As Image
        Get
            Return _Icon
        End Get
        Set(value As Image)
            _Icon = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        DoubleBuffered = True
        Size = New Size(120, 200)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
        Me._Icon = My.Resources.Resources.bitcoin_16px
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using

        ' ======================================================
        ' MARGENS
        ' ======================================================
        Dim topMargin As Integer = If(_ShowTopText, 40, 0)

        Dim bottomMargin As Integer = If(_ShowBottomText, 25, 0)

        If _EnableHoverAnimation Then
            topMargin += 6
        End If

        If _ShowBottomIcon Then
            bottomMargin += 20
        End If

        ' ======================================================
        ' CONFIGURAÇÃO DO CUBO ÚNICO
        ' ======================================================
        Dim usableWidth As Integer = Width
        Dim cubeWidth As Integer = usableWidth
        Dim cubeHeight As Integer = Height - topMargin - bottomMargin

        ' ======================================================
        ' PROFUNDIDADE DO CUBO
        ' ======================================================
        Dim depth As Integer = If(_ShowCube, _CubeBackDepth, 0)
        Dim dx As Integer = 0
        Dim dy As Integer = 0

        If _ShowCube Then
            Select Case _CubeDirection
                Case CubeDirection.RightBottom : dx = depth : dy = depth
                Case CubeDirection.RightTop : dx = depth : dy = -depth
                Case CubeDirection.LeftBottom : dx = -depth : dy = depth
                Case CubeDirection.LeftTop : dx = -depth : dy = -depth
                Case CubeDirection.Top : dy = -depth
                Case CubeDirection.Bottom : dy = depth
            End Select
        End If

        ' ======================================================
        ' BASE RECT
        ' ======================================================
        Dim x As Integer = 0
        Dim baseRect As Rectangle

        If Not _ShowCube Then
            If _ShowShadow Then
                Select Case _CubeDirection
                    Case CubeDirection.Top
                        baseRect = New Rectangle(x, topMargin + 5, cubeWidth, cubeHeight - 5)
                    Case CubeDirection.RightTop
                        baseRect = New Rectangle(x, topMargin + 5, cubeWidth - 5, cubeHeight - 5)
                    Case CubeDirection.LeftTop
                        baseRect = New Rectangle(x + 5, topMargin + 5, cubeWidth - 5, cubeHeight - 5)
                    Case CubeDirection.Bottom
                        baseRect = New Rectangle(x, topMargin, cubeWidth, cubeHeight - 5)
                    Case CubeDirection.RightBottom
                        baseRect = New Rectangle(x, topMargin - 5, cubeWidth - 5, cubeHeight)
                    Case CubeDirection.LeftBottom
                        baseRect = New Rectangle(x + 5, topMargin, cubeWidth - 5, cubeHeight - 5)
                        'baseRect = New Rectangle(x + 5, topMargin, cubeWidth - 5, cubeHeight - 5)
                End Select

            Else
                'baseRect = New Rectangle(x, topMargin, cubeWidth, cubeHeight)
                baseRect = New Rectangle(x, topMargin, cubeWidth, cubeHeight)
            End If

        Else
            Select Case _CubeDirection
                Case CubeDirection.RightBottom
                    If _ShowShadow Then
                        baseRect = New Rectangle(x, topMargin, cubeWidth - CubeBackDepth - 5, cubeHeight - CubeBackDepth - 5)
                    Else
                        baseRect = New Rectangle(x, topMargin, cubeWidth - CubeBackDepth, cubeHeight - CubeBackDepth)
                    End If

                Case CubeDirection.RightTop
                    If _ShowShadow Then
                        baseRect = New Rectangle(x, topMargin + CubeBackDepth + 5, cubeWidth - CubeBackDepth - 5, cubeHeight - CubeBackDepth - 5)
                    Else
                        baseRect = New Rectangle(x, topMargin + CubeBackDepth, cubeWidth - CubeBackDepth, cubeHeight - CubeBackDepth)
                    End If

                Case CubeDirection.LeftBottom
                    If _ShowShadow Then
                        baseRect = New Rectangle(x + CubeBackDepth + 5, topMargin, cubeWidth - CubeBackDepth - 5, cubeHeight - CubeBackDepth - 5)
                    Else
                        baseRect = New Rectangle(x + CubeBackDepth, topMargin, cubeWidth - CubeBackDepth, cubeHeight - CubeBackDepth)
                    End If

                Case CubeDirection.LeftTop
                    If _ShowShadow Then
                        baseRect = New Rectangle(x + CubeBackDepth + 5, topMargin + CubeBackDepth + 5, cubeWidth - CubeBackDepth - 5, cubeHeight - CubeBackDepth-5)
                    Else
                        baseRect = New Rectangle(x + CubeBackDepth, topMargin + CubeBackDepth, cubeWidth - CubeBackDepth, cubeHeight - CubeBackDepth)
                    End If

                Case CubeDirection.Top
                    If _ShowShadow Then
                        baseRect = New Rectangle(x, topMargin + CubeBackDepth + 5, cubeWidth, cubeHeight - CubeBackDepth - 5)
                    Else
                        baseRect = New Rectangle(x, topMargin + CubeBackDepth, cubeWidth, cubeHeight - CubeBackDepth)
                    End If

                Case CubeDirection.Bottom
                    If _ShowShadow Then
                        baseRect = New Rectangle(x, topMargin, cubeWidth, cubeHeight - CubeBackDepth - 5)
                    Else
                        baseRect = New Rectangle(x, topMargin, cubeWidth, cubeHeight - CubeBackDepth)
                    End If

            End Select
        End If

        ' ======================================================
        ' ANIMAÇÃO
        ' ======================================================
        Dim lift As Integer = If(_EnableHoverAnimation, CInt(6 * _HoverProgress), 0)

        Dim blockRect As New Rectangle(
        baseRect.X,
        baseRect.Y - lift,
        baseRect.Width,
        baseRect.Height
    )

        ' ================= SOMBRA =================
        If _ShowShadow Then
            Using sb As New SolidBrush(_ShadowColor)

                Select Case _CubeDirection
                    Case CubeDirection.RightBottom
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset + 20 + CubeBackDepth,
             blockRect.Y + _ShadowOffset + CubeBackDepth,
             blockRect.Width,
             blockRect.Height)

                    Case CubeDirection.RightTop
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset + CubeBackDepth,
             blockRect.Y - _ShadowOffset - CubeBackDepth,
             blockRect.Width,
             blockRect.Height)

                    Case CubeDirection.LeftBottom
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset - 20 - CubeBackDepth,
             blockRect.Y + _ShadowOffset + CubeBackDepth,
             blockRect.Width,
             blockRect.Height)

                    Case CubeDirection.LeftTop
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset - 10 - CubeBackDepth,
             blockRect.Y + _ShadowOffset - 10 - CubeBackDepth,
             blockRect.Width,
             blockRect.Height)

                    Case CubeDirection.Top
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset - 6,
             blockRect.Y - _ShadowOffset - CubeBackDepth,
             blockRect.Width,
             blockRect.Height)

                    Case CubeDirection.Bottom
                        g.FillRectangle(sb,
             blockRect.X + _ShadowOffset - 6,
             blockRect.Y + _ShadowOffset + CubeBackDepth,
             blockRect.Width,
             blockRect.Height)
                End Select
            End Using
        End If

        ' ================= VOLUME LATERAL =================
        If _ShowCube AndAlso dx <> 0 Then
            Dim sideFace() As Point =
        If(dx > 0,
        {
            New Point(blockRect.Right, blockRect.Top),
            New Point(blockRect.Right + dx, blockRect.Top + dy),
            New Point(blockRect.Right + dx, blockRect.Bottom + dy),
            New Point(blockRect.Right, blockRect.Bottom)
        },
        {
            New Point(blockRect.Left, blockRect.Top),
            New Point(blockRect.Left + dx, blockRect.Top + dy),
            New Point(blockRect.Left + dx, blockRect.Bottom + dy),
            New Point(blockRect.Left, blockRect.Bottom)
        })

            Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.25F))
                g.FillPolygon(br, sideFace)
            End Using
        End If

        ' ================= VOLUME TOP/BOTTOM =================
        If _ShowCube AndAlso dy <> 0 Then
            Dim faceTB() As Point =
        If(dy > 0,
        {
            New Point(blockRect.Left, blockRect.Bottom),
            New Point(blockRect.Right, blockRect.Bottom),
            New Point(blockRect.Right + dx, blockRect.Bottom + dy),
            New Point(blockRect.Left + dx, blockRect.Bottom + dy)
        },
        {
            New Point(blockRect.Left, blockRect.Top),
            New Point(blockRect.Right, blockRect.Top),
            New Point(blockRect.Right + dx, blockRect.Top + dy),
            New Point(blockRect.Left + dx, blockRect.Top + dy)
        })

            Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.4F))
                g.FillPolygon(br, faceTB)
            End Using
        End If

        ' ================= FRENTE =================
        Using bg As New SolidBrush(_BlockBackColor)
            g.FillRectangle(bg, blockRect)
        End Using


        ' ================= GRADIENTE (COM ANIMAÇÃO) =================
        Dim percent As Single = (_Value - _Minimum) / (_Maximum - _Minimum)
        percent = Math.Max(0, Math.Min(1, percent))

        Dim fillHeight As Integer = CInt(blockRect.Height * percent)

        If fillHeight <= 0 Then
            fillHeight = 0.6
        End If

        If blockRect.Width <= 0 Then
            blockRect.Width = 0.6
        End If

        ' deslocamento visual do gradiente (NÃO move o bloco)
        Dim gradOffset As Integer = If(_EnableHoverAnimation, CInt(8 * _HoverProgress), 0)

        Dim gradRect As New Rectangle(
    blockRect.X,
    blockRect.Bottom - fillHeight - gradOffset,
    blockRect.Width,
    fillHeight + gradOffset
)

        Using lg As New LinearGradientBrush(
    gradRect,
    _GradientStart,
    _GradientEnd,
    LinearGradientMode.Vertical)

            g.FillRectangle(lg, gradRect)
        End Using


        ' ================= TEXTO SUPERIOR =================
        ' ================= TEXTO INFERIOR =================
        Select Case _CubeDirection
            Case CubeDirection.Bottom
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X, blockRect.Y - 50, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8,
                        yBottom + _CubeBackDepth + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X, yBottom + _CubeBackDepth, blockRect.Width, 20))
                    End Using
                End If

            Case CubeDirection.RightBottom
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X, blockRect.Y - 50, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8 + _CubeBackDepth,
                        yBottom + _CubeBackDepth + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X + _CubeBackDepth, yBottom + _CubeBackDepth, blockRect.Width, 20))
                    End Using
                End If

            Case CubeDirection.LeftBottom
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X, blockRect.Y - 50, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8 - _CubeBackDepth,
                        yBottom + _CubeBackDepth + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X - _CubeBackDepth, yBottom + _CubeBackDepth, blockRect.Width, 20))
                    End Using
                End If

            Case CubeDirection.Top
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X, blockRect.Y - 50 - _CubeBackDepth, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8,
                        yBottom + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X, yBottom, blockRect.Width, 20))
                    End Using
                End If

            Case CubeDirection.LeftTop
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X - _CubeBackDepth, blockRect.Y - 50 - _CubeBackDepth, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8,
                        yBottom + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X, yBottom, blockRect.Width, 20))
                    End Using
                End If

            Case CubeDirection.RightTop
                ' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(_TopText) Then
                    Using br As New SolidBrush(_TopTextColor)
                        DrawCenteredText(g, _TopText, Font, br,
                          New Rectangle(blockRect.X + _CubeBackDepth, blockRect.Y - 50 - _CubeBackDepth, blockRect.Width, 40))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(_BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If _ShowBottomIcon Then
                        If _Icon IsNot Nothing Then
                            g.DrawImage(_Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8,
                        yBottom + 20, 16, 16)
                        End If
                    End If

                    Using br As New SolidBrush(_BottomTextColor)
                        DrawCenteredText(g, _BottomText, Font, br,
                    New Rectangle(blockRect.X, yBottom, blockRect.Width, 20))
                    End Using
                End If
        End Select


        ' ================= TEXTO CENTRAL =================
        If _ShowCenterText AndAlso Not String.IsNullOrEmpty(_CenterText) Then
            Using br As New SolidBrush(_CenterTextColor)
                DrawCenteredText(g, _CenterText,
                New Font(Font.FontFamily, 11, FontStyle.Bold),
                br, blockRect)
            End Using
        End If


    End Sub

    Private Sub DrawCenteredText(g As Graphics, text As String, font As Font, brush As Brush, rect As Rectangle)
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        g.DrawString(text, font, brush, rect, sf)
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        If _EnableHoverAnimation Then
            _HoverTarget = 1.0F
            _AnimTimer.Start()
        End If
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        If _EnableHoverAnimation Then
            _HoverTarget = 0.0F
            _AnimTimer.Start()
        End If
    End Sub


    Private Sub _AnimTimer_Tick(sender As Object, e As EventArgs) Handles _AnimTimer.Tick
        Dim speed As Single = 0.15F

        _HoverProgress += (_HoverTarget - _HoverProgress) * speed

        If Math.Abs(_HoverTarget - _HoverProgress) < 0.01F Then
            _HoverProgress = _HoverTarget
            _AnimTimer.Stop()
        End If

        Invalidate()
    End Sub

End Class

