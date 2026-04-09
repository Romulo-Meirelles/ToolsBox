Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DesignTimeVisible(True)>
Public Class ChartPyramid
    Inherits Control

    ' ===== DADOS =====
    Private ReadOnly _segments As New List(Of PyramidSegment)

    ' ===== HIT TEST (FORMATO DE CADA SEGMENTO) =====
    Private _segmentPaths As New List(Of GraphicsPath)

    ' ===== ANIMAÇÃO =====
    Private WithEvents _animTimer As New Timer() With {
    .Interval = 16 ' ~60 FPS
}

    ' ===== VISUAL =====
    Private _segmentSpacing As Integer = 2
    Private _explodeOffset As Integer = 0
    Private _enableHoverAnimation As Boolean = True


    Public Event Click_ChartPyramid(sender As Object, segment As PyramidSegment, index As Integer)


    ' ===== PROPRIEDADES =====
    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public ReadOnly Property Segments As List(Of PyramidSegment)
        Get
            Return _segments
        End Get
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property SegmentSpacing As Integer
        Get
            Return _segmentSpacing
        End Get
        Set(value As Integer)
            _segmentSpacing = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ExplodeOffset As Integer
        Get
            Return _explodeOffset
        End Get
        Set(value As Integer)
            _explodeOffset = Math.Max(0, value)
            Invalidate()
        End Set
    End Property

    <Category("Chart"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property EnableHoverAnimation As Boolean
        Get
            Return _enableHoverAnimation
        End Get
        Set(value As Boolean)
            _enableHoverAnimation = value
            Invalidate()
        End Set
    End Property

    ' ===== CONSTRUTOR =====
    Public Sub New()
        Size = New Size(350, 240)
        BackColor = Color.White

        ' 🔒 Dados iniciais (apenas uma vez)
        If _segments.Count = 0 Then
            CreateDefaultSegments()
        End If
        DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        BackColor = Color.Transparent
    End Sub

    ' ===== DADOS PADRÃO =====
    Private Sub CreateDefaultSegments()
        _segments.Clear()

        _segments.Add(New PyramidSegment With {
            .Value = 30D,
            .InsideText = "Car",
            .OutsideText = "Car",
            .FillColor = Color.OrangeRed
        })

        _segments.Add(New PyramidSegment With {
            .Value = 25D,
            .InsideText = "Scooter",
            .OutsideText = "Scooter",
            .FillColor = Color.Orange
        })

        _segments.Add(New PyramidSegment With {
            .Value = 20D,
            .InsideText = "Bike",
            .OutsideText = "Bike",
            .FillColor = Color.Gold
        })

        _segments.Add(New PyramidSegment With {
            .Value = 15D,
            .InsideText = "Skater",
            .OutsideText = "Skater",
            .FillColor = Color.YellowGreen
        })

        _segments.Add(New PyramidSegment With {
            .Value = 10D,
            .InsideText = "Motocycle",
            .OutsideText = "Motocycle",
            .FillColor = Color.Teal
        })
    End Sub

    ' ===== DESENHO =====
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        _segmentPaths.Clear()

        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        Using br As New SolidBrush(Me.BackColor)
            g.FillRectangle(br, ClientRectangle)
        End Using

        If _segments Is Nothing OrElse _segments.Count = 0 Then Exit Sub

        ' ───── ESPAÇO LATERAL DINÂMICO ─────
        Dim needRightSpace As Boolean =
        _segments.Any(Function(s) s.ShowPointer OrElse s.ShowOutsideText)

        Dim rightPadding As Integer = If(needRightSpace, 100, 0)

        ' ───── ÁREA ÚTIL ─────
        Dim rect As New RectangleF(
        _explodeOffset,
        _explodeOffset,
        Width - _explodeOffset * 2 - rightPadding,
        Height - _explodeOffset * 2
    )

        Dim centerX As Single = rect.Left + rect.Width / 2
        Dim stepH As Single = rect.Height / _segments.Count

        For i As Integer = 0 To _segments.Count - 1

            Dim seg = _segments(i)

            ' ===== ANIMAÇÃO HOVER (SÓ AQUI) =====
            Dim hoverOffset As Single = 0
            If seg.HoverProgress > 0 Then
                hoverOffset = -seg.HoverProgress * 8.0F
            End If

            ' ===== LARGURA PROGRESSIVA (PIRÂMIDE) =====
            Dim ratioTop As Single = i / _segments.Count
            Dim ratioBottom As Single = (i + 1) / _segments.Count

            Dim topW As Single = rect.Width * ratioTop
            Dim botW As Single = rect.Width * ratioBottom

            Dim y1 As Single = rect.Top + i * stepH + _segmentSpacing / 2 + hoverOffset
            Dim y2 As Single = y1 + stepH - _segmentSpacing

            Dim pts As PointF() = {
            New PointF(centerX - topW / 2, y1),
            New PointF(centerX + topW / 2, y1),
            New PointF(centerX + botW / 2, y2),
            New PointF(centerX - botW / 2, y2)
        }

            Using path As New GraphicsPath()

                path.AddPolygon(pts)

                _segmentPaths.Add(CType(path.Clone(), GraphicsPath))

                ' ===== SEGMENTO =====
                Using br As New SolidBrush(seg.FillColor)
                    g.FillPath(br, path)
                End Using

                Dim bounds As RectangleF = path.GetBounds()

                ' ===== TEXTO INTERNO + PORCENTAGEM =====
                Dim showText As Boolean =
                seg.ShowInsideText AndAlso Not String.IsNullOrEmpty(seg.InsideText)

                Dim showPct As Boolean = seg.ShowPercentage

                If showText OrElse showPct Then

                    Dim blockHeight As Single = 0
                    If showText Then blockHeight += seg.InsideTextFont.Height
                    If showPct Then blockHeight += seg.InsideTextFont.Height

                    Dim y As Single = bounds.Top + (bounds.Height - blockHeight) / 2

                    If showText Then
                        Using brTxt As New SolidBrush(seg.InsideTextColor)
                            g.DrawString(
                            seg.InsideText,
                            seg.InsideTextFont,
                            brTxt,
                            New RectangleF(bounds.X, y, bounds.Width, seg.InsideTextFont.Height),
                            New StringFormat With {
                                .Alignment = StringAlignment.Center,
                                .LineAlignment = StringAlignment.Center
                            }
                        )
                        End Using
                        y += seg.InsideTextFont.Height
                    End If

                    If showPct Then
                        Using brTxt As New SolidBrush(seg.InsideTextColor)
                            g.DrawString(
                            $"{seg.Value}%",
                            seg.InsideTextFont,
                            brTxt,
                            New RectangleF(bounds.X, y, bounds.Width, seg.InsideTextFont.Height),
                            New StringFormat With {
                                .Alignment = StringAlignment.Center,
                                .LineAlignment = StringAlignment.Center
                            }
                        )
                        End Using
                    End If
                End If

                ' ===== POINTER + TEXTO EXTERNO =====
                If needRightSpace AndAlso (seg.ShowPointer OrElse seg.ShowOutsideText) Then

                    Dim midY As Single = bounds.Top + bounds.Height / 2
                    Dim xStart As Single = bounds.Right
                    Dim xEnd As Single = Width - 10

                    If seg.ShowPointer Then
                        Using p As New Pen(seg.PointerColor, seg.PointerWidth)
                            g.DrawLine(p, xStart, midY, xEnd, midY)
                        End Using
                    End If

                    If seg.ShowOutsideText AndAlso Not String.IsNullOrEmpty(seg.OutsideText) Then
                        Using brTxt As New SolidBrush(seg.OutsideTextColor)
                            g.DrawString(
                            seg.OutsideText,
                            seg.OutsideTextFont,
                            brTxt,
                            xStart,
                            midY - seg.OutsideTextFont.Height - 2
                        )
                        End Using
                    End If
                End If
            End Using
        Next
    End Sub


    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If Not _enableHoverAnimation Then Return

        For i As Integer = 0 To _segmentPaths.Count - 1
            If _segmentPaths(i).IsVisible(e.Location) Then
                _segments(i).HoverTarget = 1.0F
            Else
                _segments(i).HoverTarget = 0.0F
            End If
        Next

        _animTimer.Start()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        If e.Button <> MouseButtons.Left Then Return

        For i As Integer = 0 To _segmentPaths.Count - 1
            If _segmentPaths(i).IsVisible(e.Location) Then
                RaiseEvent Click_ChartPyramid(Me, _segments(i), i)
                Exit For
            End If
        Next
    End Sub


    Private Sub _animTimer_Tick(sender As Object, e As EventArgs) Handles _animTimer.Tick
        Dim stillAnimating As Boolean = False

        For Each seg In _segments
            If seg.HoverProgress < seg.HoverTarget Then
                seg.HoverProgress += 0.15F
                If seg.HoverProgress > seg.HoverTarget Then seg.HoverProgress = seg.HoverTarget
                stillAnimating = True

            ElseIf seg.HoverProgress > seg.HoverTarget Then
                seg.HoverProgress -= 0.15F
                If seg.HoverProgress < seg.HoverTarget Then seg.HoverProgress = seg.HoverTarget
                stillAnimating = True
            End If
        Next

        Invalidate()

        If Not stillAnimating Then _animTimer.Stop()
    End Sub



    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        For Each seg In _segments
            seg.HoverTarget = 0.0F
        Next

        _animTimer.Start()
    End Sub


    Private Function HitTest(pt As Point) As Integer
        Dim rect As New Rectangle(
            _explodeOffset,
            _explodeOffset,
            Width - _explodeOffset * 2,
            Height - _explodeOffset * 2
        )

        If Not rect.Contains(pt) Then Return -1

        Dim stepH As Single = rect.Height / _segments.Count
        Dim idx As Integer = CInt((pt.Y - rect.Top) \ stepH)

        If idx < 0 OrElse idx >= _segments.Count Then Return -1
        Return idx
    End Function

End Class

Public Class PyramidSegment

    Public Property Value As Decimal

    ' ===== VISUAL =====
    Public Property FillColor As Color = Color.Gray

    ' ===== TEXTO INTERNO =====
    Public Property ShowInsideText As Boolean = True
    Public Property InsideText As String = ""
    Public Property InsideTextColor As Color = Color.White
    Public Property InsideTextFont As Font = New Font("Segoe UI", 8, FontStyle.Bold)

    ' ===== TEXTO EXTERNO =====
    Public Property ShowOutsideText As Boolean = True
    Public Property OutsideText As String = ""
    Public Property OutsideTextColor As Color = Color.Black
    Public Property OutsideTextFont As Font = New Font("Segoe UI", 8, FontStyle.Regular)

    ' ===== PERCENTUAL =====
    Public Property ShowPercentage As Boolean = True

    ' ===== LINHA =====
    Public Property ShowPointer As Boolean = True
    Public Property PointerColor As Color = Color.Gray
    Public Property PointerWidth As Single = 1.5F

    ' ===== ANIMAÇÃO (OBRIGATÓRIO) =====
    Friend HoverProgress As Single = 0.0F   ' 0 → 1
    Friend HoverTarget As Single = 0.0F     ' 0 ou 1

End Class





