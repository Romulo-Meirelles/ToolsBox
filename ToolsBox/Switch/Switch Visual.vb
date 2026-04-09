Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(Switch_Visual), "Green.ico")>
<DesignTimeVisible(True)>
Public Class Switch_Visual
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

    Private _Text_On As String
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Text_On() As String
        Get
            Return _Text_On
        End Get
        Set(ByVal value As String)
            _Text_On = value
            Invalidate()
        End Set
    End Property

    Private _Text_Off As String
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property Text_Off() As String
        Get
            Return _Text_Off
        End Get
        Set(ByVal value As String)
            _Text_Off = value
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

    Private _Color_Back As Color
    <Category("ToolsBox Herramienta"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ColorBack() As Color
        Get
            Return _Color_Back
        End Get
        Set(ByVal value As Color)
            _Color_Back = value
            Invalidate()
        End Set
    End Property

    Sub New()
        LockWidth = 70
        LockHeight = 30
        Font = New Font("Verdana", 8)
        _Text_On = "ON"
        _Text_Off = "OFF"
        _Color = Color.FromArgb(245, 245, 245)
        _Color_Enabled = Color.FromArgb(180, 200, 215)
        _Color_Back = Color.FromArgb(150, 155, 160)
    End Sub

    Protected Overrides Sub PaintHook()

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        Dim slope As Integer = 8
        Dim switchX As Integer = 3

        Dim mainRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim outerPath As GraphicsPath = CreateRound(mainRect, slope)
        Dim bgLGB As LinearGradientBrush = New LinearGradientBrush(mainRect, Color.Black, Color.Black, 90.0F)

        If _checked Then
            switchX = 34
            bgLGB = New LinearGradientBrush(mainRect, _Color_Enabled, Color.FromArgb(160, 180, 205), 90.0F)
        Else
            switchX = 3
            bgLGB = New LinearGradientBrush(mainRect, _Color_Back, Color.FromArgb(180, 185, 190), 90.0F)
        End If
        G.FillPath(bgLGB, outerPath)

        Dim onX, onY As Integer
        onX = (Width / 4) - (G.MeasureString(_Text_On, Font).Width / 2)
        onY = (Height / 2) - (G.MeasureString(_Text_On, Font).Height / 2)
        Dim offX, offY As Integer
        offX = (((Width - 1) / 4) * 3) - (G.MeasureString(_Text_Off, Font).Width / 2)
        offY = (Height / 2) - (G.MeasureString(_Text_Off, Font).Height / 2)
        G.DrawString(_Text_On, Font, Brushes.WhiteSmoke, onX, onY)
        G.DrawString(_Text_Off, Font, Brushes.Black, offX, offY)

        Dim switchRect As New Rectangle(switchX, 3, Width - 38, Height - 7)
        Dim switchPath As GraphicsPath = CreateRound(switchRect, slope)
        G.FillPath(Brushes.Silver, switchPath)

        Dim lgb As New LinearGradientBrush(switchRect, _Color, Color.FromArgb(230, 230, 230), LinearGradientMode.Vertical)
        G.FillPath(lgb, switchPath)
        G.DrawPath(Pens.Gray, switchPath)

        Dim borderBrush As New LinearGradientBrush(mainRect, Color.FromArgb(130, 140, 150), Color.FromArgb(165, 170, 175), 90.0F)
        G.DrawPath(New Pen(borderBrush), outerPath)

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

