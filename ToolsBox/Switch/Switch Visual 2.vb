Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(Switch_Visual_02), "Green.ico")>
<DesignTimeVisible(True)>
Public Class Switch_Visual_02
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
        Size = New Size(120, 19)
        LockHeight = 19
        Font = New Font("Verdana", 8)
        _Text_On = "ON"
        _Text_Off = "OFF"
        _Color = Color.FromArgb(250, 250, 250)
        _Color_Enabled = Color.FromArgb(170, 190, 210)
        _Color_Back = Color.FromArgb(170, 175, 180)
    End Sub

    Protected Overrides Sub PaintHook()

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        Dim switchFont As New Font("Arial", 8)
        Dim textY As Integer = (Me.Height / 2) - (G.MeasureString("O", switchFont).Height / 2)
        G.DrawString(_Text_Off, switchFont, Brushes.Black, New Point(0, textY))
        G.DrawString(_Text_On, switchFont, Brushes.Black, New Point(Width - G.MeasureString(_Text_On, switchFont).Width - 1, textY))

        Dim bgBrush As New SolidBrush(Color.Black)
        If _checked Then
            bgBrush = New SolidBrush(_Color_Enabled)
        Else
            bgBrush = New SolidBrush(_Color_Back)
        End If

        Dim leftArea As New Rectangle(G.MeasureString(_Text_Off, switchFont).Width + 3, 0, Height - 1, Height - 1)
        Dim rightArea As New Rectangle(Me.Width - (G.MeasureString(_Text_On, switchFont).Width) - Height - 3, 0, Height - 1, Height - 1)
        Dim connector As New Rectangle(leftArea.X + (Height - 1) - 1, ((Height - 1) / 2) - 2, rightArea.X - (leftArea.X + (Height - 1)) + 2, 4)
        Dim drawThrough As New Rectangle(connector.X - 1, connector.Y + 1, connector.Width + 2, connector.Height - 2)

        Dim borderBrush As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), Color.FromArgb(125, 132, 140), Color.FromArgb(150, 155, 160), 90.0F)

        G.FillEllipse(bgBrush, leftArea)
        G.DrawEllipse(New Pen(borderBrush), leftArea)
        G.FillEllipse(bgBrush, rightArea)
        G.DrawEllipse(New Pen(borderBrush), rightArea)
        G.FillRectangle(bgBrush, connector)
        G.DrawRectangle(New Pen(borderBrush), connector)
        G.FillRectangle(bgBrush, drawThrough)

        Dim circleBrush As New LinearGradientBrush(leftArea, _Color, Color.FromArgb(225, 230, 235), 90.0F)

        If _checked Then
            G.FillEllipse(circleBrush, New Rectangle(rightArea.X + 1, rightArea.Y + 1, rightArea.Width - 2, rightArea.Height - 2))
            Dim innerDot As New Rectangle(rightArea.X + 7, rightArea.Y + 7, rightArea.Width - 14, rightArea.Height - 14)
            G.FillEllipse(bgBrush, innerDot)
            G.DrawEllipse(New Pen(borderBrush), innerDot)
        Else
            G.FillEllipse(circleBrush, New Rectangle(leftArea.X + 1, leftArea.Y + 1, rightArea.Width - 2, rightArea.Height - 2))
            Dim innerDot As New Rectangle(leftArea.X + 7, leftArea.Y + 7, leftArea.Width - 14, leftArea.Height - 14)
            G.FillEllipse(bgBrush, innerDot)
            G.DrawEllipse(New Pen(borderBrush), innerDot)
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
