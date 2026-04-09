Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(Switch_Visual_White), "Green.ico")>
<DesignTimeVisible(True)>
Public Class Switch_Visual_White
    Inherits ThemeControl154
    Protected Overrides Sub ColorHook()
    End Sub

    Event CheckedChanged(ByVal sender As Object)

    Private _checked As Boolean
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Checked() As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            _checked = value
            Invalidate()
        End Set
    End Property

    Private _Color As Color
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Color() As Color
        Get
            Return _Color
        End Get
        Set(ByVal value As Color)
            _Color = value
            Invalidate()
        End Set
    End Property

    Private _Color_Enabled As Color
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Color_Enabled() As Color
        Get
            Return _Color_Enabled
        End Get
        Set(ByVal value As Color)
            _Color_Enabled = value
            Invalidate()
        End Set
    End Property

    Private _Color_Disabled As Color
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Color_Disabled() As Color
        Get
            Return _Color_Disabled
        End Get
        Set(ByVal value As Color)
            _Color_Disabled = value
            Invalidate()
        End Set
    End Property

    Sub New()
        Size = New Size(35, 19)
        LockHeight = 19
        Font = New Font("Verdana", 8)
        _Color = Color.FromArgb(250, 250, 250)
        _Color_Enabled = Color.FromArgb(165, 185, 205)
        _Color_Disabled = Color.FromArgb(150, 155, 160)
    End Sub

    Protected Overrides Sub PaintHook()

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        Dim slope As Integer = Height - 3

        Dim mainRect As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim mainPath As GraphicsPath = CreateRound(mainRect, slope)

        Dim borderPen As New Pen(New LinearGradientBrush(mainRect, Color.FromArgb(120, 130, 140), Color.FromArgb(155, 165, 175), 90.0F))
        Dim bgBrush As New LinearGradientBrush(mainRect, Color.Black, Color.Black, 90.0F)
        If _checked Then
            bgBrush = New LinearGradientBrush(mainRect, _Color_Enabled, Color.FromArgb(185, 205, 225), 90.0F)
        Else
            bgBrush = New LinearGradientBrush(mainRect, _Color_Disabled, Color.FromArgb(165, 170, 175), 90.0F)
        End If

        G.FillPath(bgBrush, mainPath)
        G.DrawPath(borderPen, mainPath)

        Dim leftMark As New Rectangle(0, 0, Height - 1, Height - 1)
        Dim rightMark As New Rectangle((Width - 1) - (Height - 1), 0, Height - 1, Height - 1)
        Dim circleBrush As New LinearGradientBrush(leftMark, _Color, Color.FromArgb(225, 230, 235), 90.0F)

        If _checked Then
            G.FillEllipse(circleBrush, rightMark)
            Dim innerRect As New Rectangle(rightMark.X + 7, rightMark.Y + 7, rightMark.Width - 14, rightMark.Height - 14)
            G.FillEllipse(bgBrush, innerRect)
            G.DrawEllipse(borderPen, rightMark)
            G.DrawEllipse(borderPen, innerRect)
        Else
            G.FillEllipse(circleBrush, leftMark)
            Dim innerRect As New Rectangle(leftMark.X + 7, leftMark.Y + 7, leftMark.Width - 14, leftMark.Height - 14)
            G.FillEllipse(bgBrush, innerRect)
            G.DrawEllipse(borderPen, leftMark)
            G.DrawEllipse(borderPen, innerRect)
        End If

    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)

        If _checked Then
            _checked = False
        Else
            _checked = True
        End If

        RaiseEvent CheckedChanged(Me)

    End Sub

End Class

