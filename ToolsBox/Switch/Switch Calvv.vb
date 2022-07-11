Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
<DefaultEvent("CheckedChanged")> Public Class Switch_Calvv
    Inherits ThemeControl154
    Protected Overrides Sub ColorHook()
    End Sub

    Event CheckedChanged(ByVal sender As Object)

    Private switchX As Integer = 1

    Private _checked As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Checked() As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            _checked = value
            Invalidate()
        End Set
    End Property

    Sub New()
        IsAnimated = True
        DoubleBuffered = True
        LockWidth = 80
        LockHeight = 30
        Font = New Font("Verdana", 8)
    End Sub

    Protected Overrides Sub PaintHook()

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        Dim slope As Integer = 8

        Dim mainRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim outerPath As GraphicsPath = CreateRound(mainRect, slope)
        G.FillPath(New SolidBrush(Color.FromArgb(75, 75, 85)), outerPath)
        Dim hb As New HatchBrush(HatchStyle.Trellis, Color.FromArgb(8, Color.White), Color.Transparent)
        G.FillPath(hb, outerPath)

        Dim onX, onY As Integer
        onX = (Width / 4) - (G.MeasureString("On", Font).Width / 2)
        onY = (Height / 2) - (G.MeasureString("On", Font).Height / 2)
        Dim offX, offY As Integer
        offX = ((Width / 4) * 3) - (G.MeasureString("Off", Font).Width / 2)
        offY = (Height / 2) - (G.MeasureString("Off", Font).Height / 2)
        G.DrawString("On", Font, Brushes.WhiteSmoke, onX, onY)
        G.DrawString("Off", Font, Brushes.WhiteSmoke, offX, offY)

        If DesignMode Then
            If _checked Then switchX = 40 Else switchX = 1
        End If

        Dim switchRect As New Rectangle(switchX, 1, Width - 42, Height - 3)
        Dim switchPath As GraphicsPath = CreateRound(switchRect, slope)
        G.FillPath(Brushes.Silver, switchPath)
        Dim lgb As New LinearGradientBrush(switchRect, Color.FromArgb(215, 212, 220), Color.FromArgb(148, 140, 148), LinearGradientMode.Vertical)
        G.FillPath(lgb, switchPath)
        G.DrawPath(Pens.Black, switchPath)

        Dim borderBrush As New LinearGradientBrush(mainRect, Color.Gray, Color.Black, 80.0F)
        G.DrawPath(New Pen(borderBrush), outerPath)

    End Sub

    Protected Overrides Sub OnAnimation()
        MyBase.OnAnimation()

        If _checked Then
            If switchX < 40 Then switchX += 1
        Else
            If switchX > 1 Then switchX -= 1
        End If

        Invalidate()

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

