Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.Button))> _
Public Class Button_MultForms
    Inherits Button

    Private _borderSize As Integer = 0
    Private _borderRadius As Integer = 0
    Private _borderColor As Color = Color.PaleVioletRed

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
    Public Property BorderRadius As Integer
        Get
            Return _borderRadius
        End Get
        Set(ByVal value As Integer)
            _borderRadius = value
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
    Public Property TextColor As Color
        Get
            Return Me.ForeColor
        End Get
        Set(ByVal value As Color)
            Me.ForeColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.Size = New Size(150, 40)
        Me.BackColor = Color.MediumSlateBlue
        Me.ForeColor = Color.White
        AddHandler Me.Resize, New EventHandler(AddressOf Button_Resize)
    End Sub

    Private Function GetFigurePath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim path As GraphicsPath = New GraphicsPath()
        Dim curveSize As Single = radius * 2.0F
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90)
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90)
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90)
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)
        Dim rectSurface As Rectangle = Me.ClientRectangle
        Dim rectBorder As Rectangle = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize)
        Dim smoothSize As Integer = 2
        If _borderSize > 0 Then smoothSize = _borderSize

        If _borderRadius > 0 Then

            Using pathSurface As GraphicsPath = GetFigurePath(rectSurface, _borderRadius)

                Using pathBorder As GraphicsPath = GetFigurePath(rectBorder, _borderRadius - _borderSize)

                    Using penSurface As Pen = New Pen(Me.Parent.BackColor, smoothSize)

                        Using penBorder As Pen = New Pen(_borderColor, _borderSize)
                            pevent.Graphics.SmoothingMode = SmoothingMode.HighQuality
                            Me.Region = New Region(pathSurface)
                            pevent.Graphics.DrawPath(penSurface, pathSurface)
                            If _borderSize >= 1 Then pevent.Graphics.DrawPath(penBorder, pathBorder)
                        End Using
                    End Using
                End Using
            End Using
        Else
            pevent.Graphics.SmoothingMode = SmoothingMode.None
            Me.Region = New Region(rectSurface)

            If _borderSize >= 1 Then

                Using penBorder As Pen = New Pen(_borderColor, _borderSize)
                    penBorder.Alignment = PenAlignment.Inset
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Me.Width - 1, Me.Height - 1)
                End Using
            End If
        End If
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        AddHandler Me.Parent.BackColorChanged, New EventHandler(AddressOf Container_BackColorChanged)
    End Sub

    Private Sub Container_BackColorChanged(ByVal sender As Object, ByVal e As EventArgs)
        Me.Invalidate()
    End Sub

    Private Sub Button_Resize(ByVal sender As Object, ByVal e As EventArgs)
        If _borderRadius > Me.Height Then _borderRadius = Me.Height
    End Sub
End Class

