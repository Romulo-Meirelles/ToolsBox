'Imports System.ComponentModel
'Imports System.Drawing.Drawing2D

'Public Class FearGreedGauge
'    Inherits Control

'    Private _value As Integer = 30
'    Private _currentValue As Integer = 30
'    Private _animate As Boolean = True
'    Private WithEvents _timer As New Timer With {.Interval = 15}

'    Public Class GaugeRange
'        Public Property StartPercent As Integer
'        Public Property EndPercent As Integer
'        Public Property Color As Color
'        Public Property Text As String
'    End Class

'    Private _ranges As New List(Of GaugeRange)

'    Public Sub New()
'        SetStyle(ControlStyles.AllPaintingInWmPaint Or
'                 ControlStyles.UserPaint Or
'                 ControlStyles.OptimizedDoubleBuffer Or
'                 ControlStyles.ResizeRedraw, True)

'        Size = New Size(320, 200)
'        BackColor = Color.White
'        Font = New Font("Segoe UI", 20, FontStyle.Bold)

'        _ranges.Add(New GaugeRange With {.StartPercent = 0, .EndPercent = 25, .Color = Color.FromArgb(255, 200, 180), .Text = "EXTREME FEAR"})
'        _ranges.Add(New GaugeRange With {.StartPercent = 25, .EndPercent = 50, .Color = Color.FromArgb(255, 225, 180), .Text = "FEAR"})
'        _ranges.Add(New GaugeRange With {.StartPercent = 50, .EndPercent = 75, .Color = Color.FromArgb(210, 240, 210), .Text = "GREED"})
'        _ranges.Add(New GaugeRange With {.StartPercent = 75, .EndPercent = 100, .Color = Color.FromArgb(190, 220, 255), .Text = "EXTREME GREED"})
'    End Sub

'    Public Property Value As Integer
'        Get
'            Return _value
'        End Get
'        Set(v As Integer)
'            v = Math.Max(0, Math.Min(100, v))
'            _value = v
'            If _animate Then
'                _timer.Start()
'            Else
'                _currentValue = _value
'                Invalidate()
'            End If
'        End Set
'    End Property

'    Public Property Animate As Boolean
'        Get
'            Return _animate
'        End Get
'        Set(value As Boolean)
'            _animate = value
'        End Set
'    End Property

'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)

'        Dim g = e.Graphics
'        g.SmoothingMode = SmoothingMode.AntiAlias
'        g.Clear(BackColor)

'        Dim cx = Width \ 2
'        Dim cy = Height - 20
'        Dim radius = Math.Min(Width \ 2, Height - 20)
'        Dim thickness = 22

'        Dim rect As New Rectangle(
'            cx - radius,
'            cy - radius,
'            radius * 2,
'            radius * 2
'        )

'        Dim startBaseAngle As Single = 210
'        Dim totalSweep As Single = 240

'        For Each r In _ranges
'            Dim a1 = startBaseAngle + (r.StartPercent / 100.0F) * totalSweep
'            Dim a2 = startBaseAngle + (r.EndPercent / 100.0F) * totalSweep
'            Dim sweep = a2 - a1

'            Using p As New Pen(r.Color, thickness)
'                p.StartCap = LineCap.Flat
'                p.EndCap = LineCap.Flat
'                g.DrawArc(p, rect, a1, sweep)
'            End Using
'        Next

'        Dim angle = startBaseAngle + (_currentValue / 100.0F) * totalSweep
'        Dim rad = angle * Math.PI / 180

'        Dim needleLen = radius - thickness - 10

'        Dim x2 = cx + Math.Cos(rad) * needleLen
'        Dim y2 = cy + Math.Sin(rad) * needleLen

'        Using p As New Pen(Color.Black, 3)
'            g.DrawLine(p, cx, cy, CSng(x2), CSng(y2))
'        End Using

'        Using b As New SolidBrush(Color.Black)
'            g.FillEllipse(b, cx - 4, cy - 4, 8, 8)
'        End Using

'        Dim txt = _currentValue.ToString()
'        Dim sz = g.MeasureString(txt, Font)

'        g.DrawString(
'            txt,
'            Font,
'            Brushes.Black,
'            cx - sz.Width / 2,
'            cy - radius / 2 - sz.Height / 2
'        )
'    End Sub

'    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles _timer.Tick
'        If _currentValue = _value Then
'            _timer.Stop()
'            Exit Sub
'        End If

'        If _currentValue < _value Then
'            _currentValue += 1
'        Else
'            _currentValue -= 1
'        End If

'        Invalidate()
'    End Sub

'End Class

