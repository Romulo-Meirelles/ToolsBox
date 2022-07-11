Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<ToolboxBitmap(GetType(PictureBox), "PictureBox")> _
Public Class PictureBox_Circular
    Inherits PictureBox

    Private _borderSize As Integer = 2
    Private _borderColor As Color = Color.RoyalBlue
    Private _borderColor2 As Color = Color.HotPink
    Private _borderLineStyle As DashStyle = DashStyle.Solid
    Private _borderCapStyle As DashCap = DashCap.Flat
    Private _gradientAngle As Single = 50.0F

    Public Sub New()
        Me.Size = New Size(100, 100)
        Me.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(ByVal value As Integer)
            _borderSize = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColor2 As Color
        Get
            Return _borderColor2
        End Get
        Set(ByVal value As Color)
            _borderColor2 = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderLineStyle As DashStyle
        Get
            Return _borderLineStyle
        End Get
        Set(ByVal value As DashStyle)
            _borderLineStyle = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderCapStyle As DashCap
        Get
            Return _borderCapStyle
        End Get
        Set(ByVal value As DashCap)
            _borderCapStyle = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property GradientAngle As Single
        Get
            Return _gradientAngle
        End Get
        Set(ByVal value As Single)
            _gradientAngle = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.Size = New Size(Me.Width, Me.Width)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
        MyBase.OnPaint(pe)
        Dim graph = pe.Graphics
        Dim rectContourSmooth = Rectangle.Inflate(Me.ClientRectangle, -1, -1)
        Dim rectBorder = Rectangle.Inflate(rectContourSmooth, -_borderSize, -_borderSize)
        Dim smoothSize = If(_borderSize > 0, _borderSize * 3, 1)

        Using borderGColor = New LinearGradientBrush(rectBorder, _borderColor, _borderColor2, _gradientAngle)

            Using pathRegion = New GraphicsPath()

                Using penSmooth = New Pen(Me.Parent.BackColor, smoothSize)

                    Using penBorder = New Pen(borderGColor, _borderSize)
                        graph.SmoothingMode = SmoothingMode.AntiAlias
                        penBorder.DashStyle = _borderLineStyle
                        penBorder.DashCap = _borderCapStyle
                        pathRegion.AddEllipse(rectContourSmooth)
                        Me.Region = New Region(pathRegion)
                        graph.DrawEllipse(penSmooth, rectContourSmooth)
                        If _borderSize > 0 Then graph.DrawEllipse(penBorder, rectBorder)
                    End Using
                End Using
            End Using
        End Using
    End Sub
End Class

