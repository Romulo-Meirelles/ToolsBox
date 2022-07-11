Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller


<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
<DefaultEvent("CheckedChanged")> _
Public Class Switch_Dynam
    Inherits ThemeControl154

    Event CheckedChanged(ByVal sender As Object)
    Private _Switched As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Switched() As Boolean
        Get
            Return _Switched
        End Get
        Set(ByVal value As Boolean)
            _Switched = value
            Invalidate()
        End Set
    End Property

    Private _OnColor As Gradient, _OffColor As Gradient
    <Category("ToolsBox Herramienta")> _
    Private Property OnColor() As Gradient
        Get
            Return _OnColor
        End Get
        Set(ByVal value As Gradient)
            _OnColor = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Private Property OffColor() As Gradient
        Get
            Return _OffColor
        End Get
        Set(ByVal value As Gradient)
            _OffColor = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OnColor1() As Color
        Get
            Return OnColor.Color1
        End Get
        Set(ByVal value As Color)
            OnColor.Color1 = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OnColor2() As Color
        Get
            Return OnColor.Color2
        End Get
        Set(ByVal value As Color)
            OnColor.Color2 = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OffColor1() As Color
        Get
            Return OffColor.Color1
        End Get
        Set(ByVal value As Color)
            OffColor.Color1 = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OffColor2() As Color
        Get
            Return OffColor.Color2
        End Get
        Set(ByVal value As Color)
            OffColor.Color2 = value
            Invalidate()
        End Set
    End Property

    Private _Radius As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property Radius() As Integer
        Get
            Return _Radius
        End Get
        Set(ByVal value As Integer)
            _Radius = value
            Invalidate()
        End Set
    End Property

    Public Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Radius = 6
        Cursor = Cursors.Hand
        OnColor = New Gradient(Color.FromArgb(129, 200, 56), Color.FromArgb(167, 219, 96))
        OffColor = New Gradient(Color.FromArgb(221, 35, 35), Color.FromArgb(234, 70, 70))
        Size = New Size(60, 18)
        LockHeight = 18
        Font = New Font("Verdana", 8)
    End Sub

    Protected Overrides Sub ColorHook()
    End Sub

    Private GB1, GB2 As LinearGradientBrush
    Private R1, R2 As Rectangle

    Protected Overrides Sub PaintHook()
        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        R1 = New Rectangle(1, 1, Width - 6, Height - 1) ' Main Rectangle

        If _Switched Then
            R2 = New Rectangle(39, 1, 16, 16)
            DrawRoundedRectangle(G, R1, Radius, New Pen(Color.Gray, 1), New LinearGradientBrush(New Point((Width / 2), 0), New Point((Width / 2), Height), OnColor1, OnColor2))
            G.FillEllipse(New SolidBrush(Color.LightGray), R2)
            G.DrawEllipse(Pens.Gray, R2)
            G.DrawString("On", New Font("Verdana", 7, FontStyle.Bold), New SolidBrush(Color.White), New PointF(8, 3.55))
        Else
            R2 = New Rectangle(0, 1, 16, 16)
            DrawRoundedRectangle(G, R1, Radius, New Pen(Color.LightGray, 1), New LinearGradientBrush(New Point((Width / 2), 0), New Point((Width / 2), Height), OffColor1, OffColor2))
            G.FillEllipse(New SolidBrush(Color.LightGray), R2)
            G.DrawEllipse(Pens.Gray, R2)
            G.DrawString("Off", New Font("Verdana", 7, FontStyle.Bold), New SolidBrush(Color.White), New PointF(22, 3.55))
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)

        If _Switched Then
            _Switched = False
        Else
            _Switched = True
        End If

        RaiseEvent CheckedChanged(Me)
    End Sub
End Class

Module ThemeModule
    Public Sub DrawRoundedRectangle(ByVal Graphics As Graphics, ByVal Bounds As Rectangle, ByVal Radius As Integer, ByVal Outline As Pen, ByVal Fill As LinearGradientBrush)
        Dim Stroke As Integer = Convert.ToInt32(Math.Ceiling(Outline.Width))
        Bounds = Rectangle.Inflate(Bounds, -Stroke, -Stroke)
        Outline.EndCap = Outline.StartCap = LineCap.Round
        Using Path As New GraphicsPath()
            Path.AddLine(Bounds.X + Radius, Bounds.Y, Bounds.X + Bounds.Width - (Radius * 2), Bounds.Y)
            Path.AddArc(Bounds.X + Bounds.Width - (Radius * 2), Bounds.Y, Radius * 2, Radius * 2, 270, 90)
            Path.AddLine(Bounds.X + Bounds.Width, Bounds.Y + Radius, Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height - (Radius * 2))
            Path.AddArc(Bounds.X + Bounds.Width - (Radius * 2), Bounds.Y + Bounds.Height - (Radius * 2), Radius * 2, Radius * 2, 0, 90)
            Path.AddLine(Bounds.X + Bounds.Width - (Radius * 2), Bounds.Y + Bounds.Height, Bounds.X + Radius, Bounds.Y + Bounds.Height)
            Path.AddArc(Bounds.X, Bounds.Y + Bounds.Height - (Radius * 2), Radius * 2, Radius * 2, 90, 90)
            Path.AddLine(Bounds.X, Bounds.Y + Bounds.Height - (Radius * 2), Bounds.X, Bounds.Y + Radius)
            Path.AddArc(Bounds.X, Bounds.Y, Radius * 2, Radius * 2, 180, 90)
            Path.CloseFigure()
            Graphics.FillPath(Fill, Path)
            Graphics.DrawPath(Outline, Path)
        End Using
    End Sub
End Module

Public Class Gradient
    <Category("ToolsBox Herramienta")> _
    Public Property Color1() As Color
        Get
            Return m_Color1
        End Get
        Set(ByVal value As Color)
            m_Color1 = value
        End Set
    End Property

    Private m_Color1 As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Color2() As Color
        Get
            Return m_Color2
        End Get
        Set(ByVal value As Color)
            m_Color2 = value
        End Set
    End Property
    Private m_Color2 As Color

    Public Sub New(ByVal Color1 As Color, ByVal Color2 As Color)
        Me.Color1 = Color1
        Me.Color2 = Color2
    End Sub
End Class



