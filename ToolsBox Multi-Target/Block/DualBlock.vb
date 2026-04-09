Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Windows.Forms

Public Enum CubeDirection
    RightBottom
    RightTop
    Top
    LeftBottom
    LeftTop
    Bottom
End Enum

Public Class DualBlockGauge
    Inherits Control

    ' ===== VALOR =====
    Private _Minimum As Integer = 0
    Private _Maximum As Integer = 100
    Private _Value As Integer = 40
    Private _Cubes As List(Of GaugeCubeItem)
    Private _CubeCount As Integer = 3

    ' ===== VISIBILIDADE =====
    Private _ShowTopText As Boolean = True
    Private _ShowCenterText As Boolean = True
    Private _ShowBottomText As Boolean = True
    Private _ShowBottomIcon As Boolean = True

    ' ===== CORES =====
    Private _ShowShadow As Boolean = True
    Private _ShadowOffset As Integer = 6
    Private _ShadowColor As Color = Color.FromArgb(120, 0, 0, 0)
    Private _ShowCube As Boolean = True
    Private _CubeDepth As Integer = 10
    Private _CubeBackColor As Color = Color.FromArgb(64, 64, 64)
    Private _CubeDirection As CubeDirection = CubeDirection.LeftTop
    Private _TopTextColor As Color = Color.FromArgb(64, 64, 64)
    Private _CenterTextColor As Color = Color.White
    Private _BottomTextColor As Color = Color.LightGray
    Private _CubeBackDepth As Integer = 5
    Private _CubeSpacing As Integer = 15

    '===== Mouse =====
    Private _EnableHoverAnimation As Boolean = True
    Private WithEvents _AnimTimer As New Timer() With {.Interval = 15}
    Private _CubeRects As New List(Of Rectangle)
    Private _HoverProgress As Single = 0.0F

    Public Event CubeClick(sender As Object, cube As GaugeCubeItem, index As Integer)


    ' ===== TEXTOS =====
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property TopText As String = "932252"
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property CenterText As String = "0,006 BTC"
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property BottomText As String = "773 transações"

    ' ===== ÍCONE =====
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property BottomIcon As Image = Nothing
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property BlockBackColor As Color = Color.FromArgb(40, 40, 40)
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property GradientStart As Color = Color.FromArgb(0, 120, 255)
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property GradientEnd As Color = Color.FromArgb(120, 0, 255)


    ' ===== PROPRIEDADES =====
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property CubeSpacing As Integer
        Get
            Return _CubeSpacing
        End Get
        Set(value As Integer)
            _CubeSpacing = Math.Max(0, value)
            Invalidate()
        End Set
    End Property
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public ReadOnly Property Cubes As List(Of GaugeCubeItem)
        Get
            Return _Cubes
        End Get
    End Property

    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableHoverAnimation As Boolean
        Get
            Return _EnableHoverAnimation
        End Get
        Set(value As Boolean)
            _EnableHoverAnimation = value

            If Not value Then
                ' Zera animação de TODOS os cubos
                For Each cube In _Cubes
                    cube.HoverTarget = 0.0F
                    cube.HoverProgress = 0.0F
                Next

                ' Para o timer
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
    Private Property TopTextColor As Color
        Get
            Return _TopTextColor
        End Get
        Set(value As Color)
            _TopTextColor = value
            Invalidate()
        End Set
    End Property
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property CenterTextColor As Color
        Get
            Return _CenterTextColor
        End Get
        Set(value As Color)
            _CenterTextColor = value
            Invalidate()
        End Set
    End Property
    <Category("Block Gauge"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Private Property BottomTextColor As Color
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
    Private Property CubeBackColor As Color
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
    Private Property CubeDepth As Integer
        Get
            Return _CubeDepth
        End Get
        Set(value As Integer)
            _CubeDepth = Math.Max(1, value)
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
    Private Property Value As Integer
        Get
            Return _Value
        End Get
        Set(value As Integer)
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

    Public Sub New()
        DoubleBuffered = True
        Size = New Size(365, 180)
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        _TopTextColor = Color.FromArgb(64, 64, 64)
        _Cubes = New List(Of GaugeCubeItem)

        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        If DesignMode AndAlso _Cubes.Count = 0 Then
            '    ' CUBO DE TESTE (OBRIGATÓRIO)
            _Cubes.Add(New GaugeCubeItem With {
        .Value = 60,
        .TopText = "988745441",
        .TopTextColor = Color.FromArgb(64, 64, 64),
        .CenterText = "60%",
        .CenterTextColor = Color.White,
        .BottomText = "874 Transações",
        .BottomTextColor = Color.LightGray,
        .CubeBackColor = Color.FromArgb(64, 64, 64),
        .BlockBackColor = Color.FromArgb(40, 40, 40),
        .GradientStart = Color.FromArgb(0, 120, 255),
        .GradientEnd = Color.FromArgb(120, 0, 255),
        .Icon = Nothing
    })

            Cubes.Add(New GaugeCubeItem With {
                .Value = 80,
                .TopText = "988741265",
                .TopTextColor = Color.FromArgb(64, 64, 64),
                .CenterText = "80%",
                .CenterTextColor = Color.White,
                .BottomText = "684 Transações",
                .BottomTextColor = Color.LightGray,
                .CubeBackColor = Color.FromArgb(64, 64, 64),
                .BlockBackColor = Color.FromArgb(40, 40, 40),
                .GradientStart = Color.FromArgb(0, 120, 255),
                .GradientEnd = Color.FromArgb(120, 0, 255),
                .Icon = Nothing
            })

            Cubes.Add(New GaugeCubeItem With {
                .Value = 100,
                .TopText = "998455123",
                .TopTextColor = Color.FromArgb(64, 64, 64),
                .CenterText = "100%",
                .CenterTextColor = Color.White,
                .BottomText = "998 Transações",
                .BottomTextColor = Color.LightGray,
                .CubeBackColor = Color.FromArgb(64, 64, 64),
                .BlockBackColor = Color.FromArgb(40, 40, 40),
                .GradientStart = Color.FromArgb(0, 120, 255),
                .GradientEnd = Color.FromArgb(120, 0, 255),
                .Icon = Nothing
            })

            _AnimTimer.Start()
        End If
    End Sub

    Private Function IsInDesignMode() As Boolean
        Return LicenseManager.UsageMode = LicenseUsageMode.Designtime _
           OrElse DesignMode _
           OrElse (Site IsNot Nothing AndAlso Site.DesignMode)
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        ' g.Clear(BackColor)

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using
        ' ======================================================
        ' MARGENS
        ' ======================================================
        Dim topMargin As Integer = If(_ShowTopText, 40, 20)
        Dim bottomMargin As Integer = If(_ShowBottomText, 55, 1)

        ' ======================================================
        ' CONFIGURAÇÃO DOS CUBOS
        ' ======================================================
        Dim cubeCount As Integer = Math.Max(1, _Cubes.Count)
        Dim spacing As Integer = _CubeSpacing

        Dim totalSpacing As Integer = spacing * (cubeCount - 1)
        Dim usableWidth As Integer = Width - 30 - totalSpacing
        Dim cubeWidth As Integer = usableWidth \ cubeCount
        Dim cubeHeight As Integer = Height - topMargin - bottomMargin
        Dim animOffset As Integer = CInt(6 * _HoverProgress)

        ' ======================================================
        ' PROFUNDIDADE DO CUBO
        ' ======================================================
        Dim depth As Integer = If(_ShowCube, _CubeDepth + _CubeBackDepth, 0)
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
        ' BASE RECTS (SEM ANIMAÇÃO)
        ' ======================================================
        Dim baseRects As New List(Of Rectangle)
        Dim x As Integer = 15



        If _ShowCube = False Then

            For Each cube As GaugeCubeItem In _Cubes
                baseRects.Add(New Rectangle(
                x,
                topMargin - 10,
                cubeWidth,
                cubeHeight
            ))
                x += cubeWidth + spacing
            Next


        Else
            For Each cube As GaugeCubeItem In _Cubes

                Select Case _CubeDirection
                    Case CubeDirection.RightBottom
                        baseRects.Add(New Rectangle(
             x,
             topMargin - 15,
             cubeWidth - CubeBackDepth,
             cubeHeight - CubeBackDepth
         ))
                        x += cubeWidth + spacing




                    Case CubeDirection.RightTop
                        baseRects.Add(New Rectangle(
            x,
            topMargin + CubeBackDepth,
            cubeWidth - CubeBackDepth,
            cubeHeight - CubeBackDepth
        ))
                        x += cubeWidth + spacing




                    Case CubeDirection.LeftBottom
                        baseRects.Add(New Rectangle(
           x + CubeBackDepth,
           topMargin - 15,
           cubeWidth - CubeBackDepth,
           cubeHeight - CubeBackDepth
       ))
                        x += cubeWidth + spacing


                    Case CubeDirection.LeftTop

                        baseRects.Add(New Rectangle(
                x + CubeBackDepth,
                topMargin + CubeBackDepth,
                cubeWidth - CubeBackDepth,
                cubeHeight - CubeBackDepth
            ))
                        x += cubeWidth + spacing

                    Case CubeDirection.Top
                        baseRects.Add(New Rectangle(
           x,
           topMargin + CubeBackDepth,
           cubeWidth - CubeBackDepth,
           cubeHeight - CubeBackDepth
       ))
                        x += cubeWidth + spacing


                    Case CubeDirection.Bottom

                        baseRects.Add(New Rectangle(
           x,
           topMargin,
           cubeWidth - CubeBackDepth,
           cubeHeight - CubeBackDepth
       ))
                        x += cubeWidth + spacing


                End Select

            Next

        End If


        ' ======================================================
        ' LIMPA RECTS REAIS (IMPORTANTE)
        ' ======================================================
        _CubeRects.Clear()
        For i As Integer = 0 To _Cubes.Count - 1
            _CubeRects.Add(Rectangle.Empty)
        Next



        ' ======================================================
        ' DESENHO (DIREITA → ESQUERDA)
        ' ======================================================
        ' For i As Integer = _Cubes.Count - 1 To 0 Step -1
        Dim start As Integer
        Dim [end] As Integer
        Dim stepDir As Integer

        If dx > 0 Then
            ' Volume indo para a DIREITA → desenha ESQUERDA → DIREITA
            start = 0
            [end] = _Cubes.Count - 1
            stepDir = 1
        Else
            ' Volume indo para a ESQUERDA ou neutro → desenha DIREITA → ESQUERDA
            start = _Cubes.Count - 1
            [end] = 0
            stepDir = -1
        End If

        For i As Integer = start To [end] Step stepDir

            Dim cube As GaugeCubeItem = _Cubes(i)
            Dim baseRect As Rectangle = baseRects(i)

            ' ===== ANIMAÇÃO INDIVIDUAL =====
            Dim lift As Integer = 0
            If _EnableHoverAnimation Then
                lift = CInt(6 * cube.HoverProgress)
            End If

            Dim blockRect As New Rectangle(
            baseRect.X,
            baseRect.Y - lift,
            baseRect.Width,
            baseRect.Height
        )

            ' >>> ESTE É O PONTO-CHAVE <<<
            _CubeRects(i) = blockRect


            ' ================= SOMBRA =================
            If _ShowShadow Then
                Using sb As New SolidBrush(_ShadowColor)
                    g.FillRectangle(sb,
                    blockRect.X + _ShadowOffset,
                    blockRect.Y + _ShadowOffset,
                    blockRect.Width,
                    blockRect.Height)
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

                Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.25F))
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

                Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.4F))
                    g.FillPolygon(br, faceTB)
                End Using
            End If

            ' ================= FRENTE =================
            Using bg As New SolidBrush(cube.BlockBackColor)
                g.FillRectangle(bg, blockRect)
            End Using

            ' ================= GRADIENTE =================
            Dim percent As Single = (cube.Value - _Minimum) / (_Maximum - _Minimum)
            percent = Math.Max(0, Math.Min(1, percent))

            Dim fillHeight As Integer = CInt(blockRect.Height * percent)

            Using lg As New LinearGradientBrush(
            New Rectangle(blockRect.X, blockRect.Bottom - fillHeight, blockRect.Width, fillHeight),
            cube.GradientStart,
            cube.GradientEnd,
            LinearGradientMode.Vertical)
                g.FillRectangle(lg, lg.Rectangle)
            End Using


            If _ShowCube = False Then
                '' ================= TEXTO SUPERIOR =================
                If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                    Using br As New SolidBrush(cube.TopTextColor)
                        DrawCenteredText(g, cube.TopText, Font, br,
                        New Rectangle(blockRect.X, blockRect.Y - 15, blockRect.Width, 10))
                    End Using
                End If

                ' ================= TEXTO INFERIOR =================
                If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                    Dim yBottom As Integer = blockRect.Bottom + 6

                    If cube.Icon IsNot Nothing Then
                        g.DrawImage(cube.Icon,
                        blockRect.X + (blockRect.Width \ 2) - 8,
                        yBottom, 16, 16)
                    End If

                    Using br As New SolidBrush(cube.BottomTextColor)
                        DrawCenteredText(g, cube.BottomText, Font, br,
                        New Rectangle(blockRect.X, yBottom, blockRect.Width, 50))
                    End Using
                End If
            Else

                Select Case _CubeDirection
                    Case CubeDirection.RightBottom
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X, blockRect.Y - 15, blockRect.Width, 10))
                            End Using
                        End If

                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X + 10 + CubeBackDepth, yBottom + CubeBackDepth, blockRect.Width, 50))
                            End Using
                        End If

                    Case CubeDirection.RightTop
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X + 15 + CubeBackDepth, blockRect.Y - 30 - CubeBackDepth, blockRect.Width, 10))
                            End Using
                        End If

                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X + 5, yBottom - 10, blockRect.Width, 50))
                            End Using
                        End If

                    Case CubeDirection.LeftBottom
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X, blockRect.Y - 20, blockRect.Width, 10))
                            End Using
                        End If

                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X - 10 - CubeBackDepth, yBottom + CubeBackDepth, blockRect.Width, 50))
                            End Using
                        End If

                    Case CubeDirection.LeftTop
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X - 15 - CubeBackDepth, blockRect.Y - 30 - CubeBackDepth, blockRect.Width, 10))
                            End Using
                        End If

                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X - 5, yBottom - 10, blockRect.Width, 50))
                            End Using
                        End If




                    Case CubeDirection.Top
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X, blockRect.Y - 30 - CubeBackDepth, blockRect.Width, 10))
                            End Using
                        End If
                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X, yBottom - 10, blockRect.Width, 50))
                            End Using
                        End If

                    Case CubeDirection.Bottom
                        '' ================= TEXTO SUPERIOR =================
                        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
                            Using br As New SolidBrush(cube.TopTextColor)
                                DrawCenteredText(g, cube.TopText, Font, br,
                                New Rectangle(blockRect.X, blockRect.Y - 15, blockRect.Width, 10))
                            End Using
                        End If

                        ' ================= TEXTO INFERIOR =================
                        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
                            Dim yBottom As Integer = blockRect.Bottom + 6

                            If cube.Icon IsNot Nothing Then
                                g.DrawImage(cube.Icon,
                                blockRect.X + (blockRect.Width \ 2) - 8,
                                yBottom, 16, 16)
                            End If

                            Using br As New SolidBrush(cube.BottomTextColor)
                                DrawCenteredText(g, cube.BottomText, Font, br,
                                New Rectangle(blockRect.X, yBottom + CubeBackDepth, blockRect.Width, 50))
                            End Using
                        End If
                End Select

            End If


            ' ================= TEXTO CENTRAL =================
            If _ShowCenterText AndAlso Not String.IsNullOrEmpty(cube.CenterText) Then
                Using br As New SolidBrush(cube.CenterTextColor)
                    DrawCenteredText(g, cube.CenterText,
                    New Font(Font.FontFamily, 11, FontStyle.Bold),
                    br, blockRect)
                End Using
            End If

        Next
    End Sub


    'Protected Overrides Sub OnPaint(e As PaintEventArgs)
    '    MyBase.OnPaint(e)

    '    Dim g = e.Graphics
    '    g.SmoothingMode = SmoothingMode.AntiAlias
    '    g.Clear(BackColor)

    '    ' ======================================================
    '    ' MARGENS
    '    ' ======================================================
    '    Dim topMargin As Integer = If(_ShowTopText, 40, 20)
    '    Dim bottomMargin As Integer = If(_ShowBottomText, 55, 1)

    '    ' ======================================================
    '    ' CONFIGURAÇÃO DOS CUBOS
    '    ' ======================================================
    '    Dim cubeCount As Integer = Math.Max(1, _Cubes.Count)
    '    Dim spacing As Integer = _CubeSpacing

    '    Dim totalSpacing As Integer = spacing * (cubeCount - 1)
    '    Dim usableWidth As Integer = Width - 30 - totalSpacing
    '    Dim cubeWidth As Integer = usableWidth \ cubeCount
    '    Dim cubeHeight As Integer = Height - topMargin - bottomMargin
    '    Dim animOffset As Integer = CInt(6 * _HoverProgress)

    '    ' ======================================================
    '    ' PROFUNDIDADE DO CUBO
    '    ' ======================================================
    '    Dim depth As Integer = If(_ShowCube, _CubeDepth + _CubeBackDepth, 0)
    '    Dim dx As Integer = 0
    '    Dim dy As Integer = 0

    '    If _ShowCube Then
    '        Select Case _CubeDirection
    '            Case CubeDirection.RightBottom : dx = depth : dy = depth
    '            Case CubeDirection.RightTop : dx = depth : dy = -depth
    '            Case CubeDirection.LeftBottom : dx = -depth : dy = depth
    '            Case CubeDirection.LeftTop : dx = -depth : dy = -depth
    '            Case CubeDirection.Top : dy = -depth
    '            Case CubeDirection.Bottom : dy = depth
    '        End Select
    '    End If

    '    ' ======================================================
    '    ' POSIÇÕES (ESQUERDA → DIREITA)
    '    ' ======================================================
    '    Dim cubeRects As New List(Of Rectangle)
    '    Dim x As Integer = 15

    '    For Each cube As GaugeCubeItem In _Cubes
    '        cubeRects.Add(New Rectangle(
    '        x,
    '        topMargin - animOffset,
    '        cubeWidth,
    '        cubeHeight
    '    ))
    '        x += cubeWidth + spacing
    '    Next

    '    ' ======================================================
    '    ' DESENHO (DIREITA → ESQUERDA)
    '    ' ======================================================
    '    For i As Integer = _Cubes.Count - 1 To 0 Step -1

    '        Dim cube As GaugeCubeItem = _Cubes(i)
    '        Dim blockRect As Rectangle = cubeRects(i)


    '        ' ======================================================
    '        ' ANIMAÇÃO (GLOBAL – A QUE JÁ EXISTE)
    '        ' ======================================================
    '        animOffset = If(_EnableHoverAnimation, CInt(6 * cube.HoverProgress), 0)
    '        'Dim animOffset As Integer = CInt(6 * cube.HoverProgress)



    '        If _EnableHoverAnimation Then
    '            animOffset = CInt(6 * cube.HoverProgress)
    '            blockRect.Y -= animOffset
    '        End If


    '        ' ================= SOMBRA =================
    '        If _ShowShadow Then
    '            Using sb As New SolidBrush(_ShadowColor)
    '                g.FillRectangle(sb,
    '                blockRect.X + _ShadowOffset,
    '                blockRect.Y + _ShadowOffset,
    '                blockRect.Width,
    '                blockRect.Height)
    '            End Using
    '        End If

    '        ' ================= VOLUME LATERAL =================
    '        If _ShowCube AndAlso dx <> 0 Then
    '            Dim sideFace() As Point =
    '            If(dx > 0,
    '            {
    '                New Point(blockRect.Right, blockRect.Top),
    '                New Point(blockRect.Right + dx, blockRect.Top + dy),
    '                New Point(blockRect.Right + dx, blockRect.Bottom + dy),
    '                New Point(blockRect.Right, blockRect.Bottom)
    '            },
    '            {
    '                New Point(blockRect.Left, blockRect.Top),
    '                New Point(blockRect.Left + dx, blockRect.Top + dy),
    '                New Point(blockRect.Left + dx, blockRect.Bottom + dy),
    '                New Point(blockRect.Left, blockRect.Bottom)
    '            })

    '            Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.25F))
    '                g.FillPolygon(br, sideFace)
    '            End Using
    '        End If

    '        ' ================= VOLUME SUPERIOR / INFERIOR =================
    '        If _ShowCube AndAlso dy <> 0 Then
    '            Dim faceTB() As Point =
    '            If(dy > 0,
    '            {
    '                New Point(blockRect.Left, blockRect.Bottom),
    '                New Point(blockRect.Right, blockRect.Bottom),
    '                New Point(blockRect.Right + dx, blockRect.Bottom + dy),
    '                New Point(blockRect.Left + dx, blockRect.Bottom + dy)
    '            },
    '            {
    '                New Point(blockRect.Left, blockRect.Top),
    '                New Point(blockRect.Right, blockRect.Top),
    '                New Point(blockRect.Right + dx, blockRect.Top + dy),
    '                New Point(blockRect.Left + dx, blockRect.Top + dy)
    '            })

    '            Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.4F))
    '                g.FillPolygon(br, faceTB)
    '            End Using
    '        End If

    '        ' ================= FRENTE =================
    '        Using bg As New SolidBrush(cube.BlockBackColor)
    '            g.FillRectangle(bg, blockRect)
    '        End Using

    '        ' ================= GRADIENTE =================
    '        Dim percent As Single = (cube.Value - _Minimum) / (_Maximum - _Minimum)
    '        percent = Math.Max(0, Math.Min(1, percent))

    '        Dim fillHeight As Integer = CInt(blockRect.Height * percent)

    '        Using lg As New LinearGradientBrush(
    '        New Rectangle(blockRect.X, blockRect.Bottom - fillHeight, blockRect.Width, fillHeight),
    '        cube.GradientStart,
    '        cube.GradientEnd,
    '        LinearGradientMode.Vertical)
    '            g.FillRectangle(lg, lg.Rectangle)
    '        End Using

    '        ' ================= TEXTO CENTRAL =================
    '        If _ShowCenterText AndAlso Not String.IsNullOrEmpty(cube.CenterText) Then
    '            Using br As New SolidBrush(cube.CenterTextColor)
    '                DrawCenteredText(g, cube.CenterText,
    '                New Font(Font.FontFamily, 11, FontStyle.Bold),
    '                br, blockRect)
    '            End Using
    '        End If

    '        ' ================= TEXTO SUPERIOR =================
    '        If _ShowTopText AndAlso Not String.IsNullOrEmpty(cube.TopText) Then
    '            Using br As New SolidBrush(cube.TopTextColor)
    '                DrawCenteredText(g, cube.TopText, Font, br,
    '                New Rectangle(blockRect.X, blockRect.Y - 40, blockRect.Width, 20))
    '            End Using
    '        End If

    '        ' ================= TEXTO INFERIOR =================
    '        If _ShowBottomText AndAlso Not String.IsNullOrEmpty(cube.BottomText) Then
    '            Dim yBottom As Integer = blockRect.Bottom + 6

    '            If cube.Icon IsNot Nothing Then
    '                g.DrawImage(cube.Icon,
    '                blockRect.X + (blockRect.Width \ 2) - 8,
    '                yBottom, 16, 16)
    '            End If

    '            Using br As New SolidBrush(cube.BottomTextColor)
    '                DrawCenteredText(g, cube.BottomText, Font, br,
    '                New Rectangle(blockRect.X, yBottom + 16, blockRect.Width, 20))
    '            End Using
    '        End If

    '    Next
    'End Sub







    'Protected Overrides Sub OnPaint(e As PaintEventArgs)
    '    MyBase.OnPaint(e)

    '    Dim g = e.Graphics
    '    g.SmoothingMode = SmoothingMode.AntiAlias
    '    g.Clear(BackColor)

    '    ' ======================================================
    '    ' MARGENS
    '    ' ======================================================
    '    Dim topMargin As Integer = If(_ShowTopText, 40, 20)
    '    Dim bottomMargin As Integer = If(_ShowBottomText, 55, 1)

    '    ' ======================================================
    '    ' CONFIGURAÇÃO DOS CUBOS
    '    ' ======================================================
    '    Dim cubeCount As Integer = Math.Max(1, _Cubes.Count)
    '    Dim spacing As Integer = _CubeSpacing

    '    Dim totalSpacing As Integer = spacing * (cubeCount - 1)
    '    Dim usableWidth As Integer = Width - 30 - totalSpacing
    '    Dim cubeWidth As Integer = usableWidth \ cubeCount

    '    ' ======================================================
    '    ' ANIMAÇÃO
    '    ' ======================================================
    '    Dim animOffset As Integer = If(_EnableHoverAnimation, CInt(6 * _HoverProgress), 0)

    '    ' ======================================================
    '    ' PROFUNDIDADE
    '    ' ======================================================
    '    Dim depth As Integer = If(_ShowCube, _CubeDepth + _CubeBackDepth, 0)
    '    Dim dx As Integer = 0
    '    Dim dy As Integer = 0

    '    If _ShowCube Then
    '        Select Case _CubeDirection
    '            Case CubeDirection.RightBottom : dx = depth : dy = depth
    '            Case CubeDirection.RightTop : dx = depth : dy = -depth
    '            Case CubeDirection.LeftBottom : dx = -depth : dy = depth
    '            Case CubeDirection.LeftTop : dx = -depth : dy = -depth
    '            Case CubeDirection.Top : dy = -depth
    '            Case CubeDirection.Bottom : dy = depth
    '        End Select
    '    End If

    '    ' ======================================================
    '    ' POSIÇÕES (ESQUERDA → DIREITA)
    '    ' ======================================================
    '    Dim cubeRects As New List(Of Rectangle)
    '    Dim x As Integer = 15

    '    For Each cube As GaugeCubeItem In _Cubes
    '        cubeRects.Add(New Rectangle(
    '        x,
    '        topMargin - animOffset,
    '        cubeWidth,
    '        Height - topMargin - bottomMargin
    '    ))
    '        x += cubeWidth + spacing
    '    Next

    '    ' ======================================================
    '    ' DESENHO (DIREITA → ESQUERDA)
    '    ' ======================================================
    '    For i As Integer = _Cubes.Count - 1 To 0 Step -1

    '        Dim cube As GaugeCubeItem = _Cubes(i)
    '        Dim blockRect As Rectangle = cubeRects(i)
    '        'animOffset = CInt(6 * cube.HoverProgress)


    '        If _EnableHoverAnimation Then
    '            animOffset = CInt(6 * cube.HoverProgress)
    '            blockRect.Y -= animOffset
    '        End If


    '        ' ================= VOLUME =================
    '        If _ShowCube Then

    '            If dx <> 0 Then
    '                Dim sideFace() As Point =
    '                If(dx > 0,
    '                {
    '                    New Point(blockRect.Right, blockRect.Top),
    '                    New Point(blockRect.Right + dx, blockRect.Top + dy),
    '                    New Point(blockRect.Right + dx, blockRect.Bottom + dy),
    '                    New Point(blockRect.Right, blockRect.Bottom)
    '                },
    '                {
    '                    New Point(blockRect.Left, blockRect.Top),
    '                    New Point(blockRect.Left + dx, blockRect.Top + dy),
    '                    New Point(blockRect.Left + dx, blockRect.Bottom + dy),
    '                    New Point(blockRect.Left, blockRect.Bottom)
    '                })

    '                Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.25F))
    '                    g.FillPolygon(br, sideFace)
    '                End Using
    '            End If

    '            If dy <> 0 Then
    '                Dim faceTB() As Point =
    '                If(dy > 0,
    '                {
    '                    New Point(blockRect.Left, blockRect.Bottom),
    '                    New Point(blockRect.Right, blockRect.Bottom),
    '                    New Point(blockRect.Right + dx, blockRect.Bottom + dy),
    '                    New Point(blockRect.Left + dx, blockRect.Bottom + dy)
    '                },
    '                {
    '                    New Point(blockRect.Left, blockRect.Top),
    '                    New Point(blockRect.Right, blockRect.Top),
    '                    New Point(blockRect.Right + dx, blockRect.Top + dy),
    '                    New Point(blockRect.Left + dx, blockRect.Top + dy)
    '                })

    '                Using br As New SolidBrush(ControlPaint.Dark(cube.CubeBackColor, 0.4F))
    '                    g.FillPolygon(br, faceTB)
    '                End Using
    '            End If
    '        End If

    '        ' ================= SOMBRA =================
    '        If _ShowShadow Then
    '            Using sb As New SolidBrush(_ShadowColor)
    '                g.FillRectangle(sb,
    '                blockRect.X + _ShadowOffset,
    '                blockRect.Y + _ShadowOffset,
    '                blockRect.Width,
    '                blockRect.Height)
    '            End Using
    '        End If

    '        ' ================= FRENTE (AQUI ESTÁ A CORREÇÃO) =================
    '        Using bg As New SolidBrush(cube.BlockBackColor)
    '            g.FillRectangle(bg, blockRect)
    '        End Using




    '        ' ================= GRADIENTE =================
    '        Dim percent As Single = (cube.Value - _Minimum) / (_Maximum - _Minimum)
    '        percent = Math.Max(0, Math.Min(1, percent))

    '        Dim fillHeight As Integer = CInt(blockRect.Height * percent)

    '        Using lg As New LinearGradientBrush(
    '        New Rectangle(blockRect.X, blockRect.Bottom - fillHeight, blockRect.Width, fillHeight),
    '        cube.GradientStart,
    '        cube.GradientEnd,
    '        LinearGradientMode.Vertical)
    '            g.FillRectangle(lg, lg.Rectangle)
    '        End Using

    '        ' ================= TEXTO CENTRAL =================
    '        If _ShowCenterText AndAlso Not String.IsNullOrEmpty(cube.CenterText) Then
    '            Using br As New SolidBrush(cube.CenterTextColor)
    '                DrawCenteredText(g, cube.CenterText,
    '                New Font(Font.FontFamily, 11, FontStyle.Bold),
    '                br, blockRect)
    '            End Using
    '        End If

    '        ' ================= TEXTO SUPERIOR =================
    '        If Not String.IsNullOrEmpty(cube.TopText) Then
    '            Using br As New SolidBrush(cube.TopTextColor)
    '                DrawCenteredText(g, cube.TopText, Font, br,
    '                New Rectangle(blockRect.X - 15, blockRect.Y - 40, blockRect.Width, 20))
    '            End Using
    '        End If

    '        ' ================= TEXTO INFERIOR =================
    '        If Not String.IsNullOrEmpty(cube.BottomText) Then
    '            Dim yBottom As Integer = blockRect.Bottom + 6

    '            If cube.Icon IsNot Nothing Then
    '                g.DrawImage(cube.Icon,
    '                blockRect.X + (blockRect.Width \ 2) - 8,
    '                yBottom, 16, 16)
    '            End If

    '            Using br As New SolidBrush(cube.BottomTextColor)
    '                DrawCenteredText(g, cube.BottomText, Font, br,
    '                New Rectangle(blockRect.X, yBottom + 16, blockRect.Width, 20))
    '            End Using
    '        End If

    '    Next
    'End Sub

    Private Sub DrawRightFace(g As Graphics, r As Rectangle, dx As Integer, dy As Integer)
        Dim pts() As Point = {
        New Point(r.Right, r.Top),
        New Point(r.Right + dx, r.Top + dy),
        New Point(r.Right + dx, r.Bottom + dy),
        New Point(r.Right, r.Bottom)
    }

        Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.25F))
            g.FillPolygon(br, pts)
        End Using
    End Sub
    Private Sub DrawLeftFace(g As Graphics, r As Rectangle, dx As Integer, dy As Integer)
        Dim pts() As Point = {
        New Point(r.Left, r.Top),
        New Point(r.Left + dx, r.Top + dy),
        New Point(r.Left + dx, r.Bottom + dy),
        New Point(r.Left, r.Bottom)
    }

        Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.25F))
            g.FillPolygon(br, pts)
        End Using
    End Sub
    Private Sub DrawBottomFace(g As Graphics, r As Rectangle, dx As Integer, dy As Integer)
        Dim pts() As Point = {
        New Point(r.Left, r.Bottom),
        New Point(r.Right, r.Bottom),
        New Point(r.Right + dx, r.Bottom + dy),
        New Point(r.Left + dx, r.Bottom + dy)
    }

        Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.45F))
            g.FillPolygon(br, pts)
        End Using
    End Sub
    Private Sub DrawTopFace(g As Graphics, r As Rectangle, dx As Integer, dy As Integer)
        Dim pts() As Point = {
        New Point(r.Left, r.Top),
        New Point(r.Right, r.Top),
        New Point(r.Right + dx, r.Top + dy),
        New Point(r.Left + dx, r.Top + dy)
    }

        Using br As New SolidBrush(ControlPaint.Dark(_CubeBackColor, 0.45F))
            g.FillPolygon(br, pts)
        End Using
    End Sub
    Private Sub DrawCenteredText(g As Graphics, txt As String, f As Font, br As Brush, r As Rectangle)
        Dim sz = g.MeasureString(txt, f)
        g.DrawString(txt, f, br,
                     r.X + (r.Width - sz.Width) / 2,
                     r.Y + (r.Height - sz.Height) / 2)
    End Sub


    Private Sub AnimTimer_Tick(sender As Object, e As EventArgs) Handles _AnimTimer.Tick
        Dim stillAnimating As Boolean = False

        For Each cube In _Cubes
            If cube.HoverProgress < cube.HoverTarget Then
                cube.HoverProgress += 0.15F
                If cube.HoverProgress > cube.HoverTarget Then cube.HoverProgress = cube.HoverTarget
                stillAnimating = True

            ElseIf cube.HoverProgress > cube.HoverTarget Then
                cube.HoverProgress -= 0.15F
                If cube.HoverProgress < cube.HoverTarget Then cube.HoverProgress = cube.HoverTarget
                stillAnimating = True
            End If
        Next

        Invalidate()

        If Not stillAnimating Then
            _AnimTimer.Stop()
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If Not _EnableHoverAnimation Then Return

        For i As Integer = 0 To _CubeRects.Count - 1
            Dim cube = _Cubes(i)

            If _CubeRects(i).Contains(e.Location) Then
                cube.HoverTarget = 1.0F
            Else
                cube.HoverTarget = 0.0F
            End If
        Next

        _AnimTimer.Start()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        For i As Integer = 0 To _CubeRects.Count - 1
            If _CubeRects(i).Contains(e.Location) Then
                RaiseEvent CubeClick(Me, _Cubes(i), i)
                Exit For
            End If
        Next
    End Sub



End Class
Public Class GaugeCubeItem
    Public Property Value As Integer = 1
    Public Property TopText As String = "9987451245"
    Public Property TopTextColor As Color = Color.FromArgb(64, 64, 64)
    Public Property CenterText As String = "80%"
    Public Property CenterTextColor As Color = Color.White
    Public Property BottomText As String = "897 Transations."
    Public Property BottomTextColor As Color = Color.LightGray
    Public Property BlockBackColor As Color = Color.Salmon
    Public Property CubeBackColor As Color = Color.Salmon
    Public Property GradientStart As Color = Color.SeaGreen
    Public Property GradientEnd As Color = Color.Purple
    Public Property Icon As Image

    ' ===== ANIMAÇÃO INDIVIDUAL =====
    Friend HoverProgress As Single = 0.0F
    Friend HoverTarget As Single = 0.0F
End Class


