Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
<DefaultEvent("CheckedChanged")> _
Public Class Switch_Positron : Inherits ThemeControl154

    Private TB As Brush
    Private bc As Pen

    Private _Checked As Boolean = False
    Public Event CheckedChanged As CheckedChangedEventHandler
    Public Delegate Sub CheckedChangedEventHandler(ByVal sender As Object)

    <Category("ToolsBox Herramienta")> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
            RaiseEvent CheckedChanged(Me)
        End Set
    End Property
    Protected Sub DrawBorderss(ByVal p1 As Pen, ByVal G As Graphics)
        DrawBorderss(p1, 0, 0, Width, Height, G)
    End Sub
    Protected Sub DrawBorderss(ByVal p1 As Pen, ByVal offset As Integer, ByVal G As Graphics)
        DrawBorderss(p1, 0, 0, Width, Height, offset, _
            G)
    End Sub
    Protected Sub DrawBorderss(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal G As Graphics)
        G.DrawRectangle(p1, x, y, width - 1, height - 1)
    End Sub
    Protected Sub DrawBorderss(ByVal p1 As Pen, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal offset As Integer, _
        ByVal G As Graphics)
        DrawBorderss(p1, x + offset, y + offset, width - (offset * 2), height - (offset * 2), G)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If _Checked = True Then
            _Checked = False
        Else
            _Checked = True
        End If
    End Sub

    Public Sub New()
        LockHeight = 24
        LockWidth = 62
        SetColor("Texts", Color.FromArgb(100, 100, 100))
        SetColor("border", Color.FromArgb(125, 125, 125))
    End Sub

    Protected Overrides Sub ColorHook()
        TB = GetBrush("Texts")
        bc = GetPen("border")
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)
        Dim LGB1 As New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(120, 120, 120), Color.FromArgb(100, 100, 100), 90)
        Dim HB1 As New HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(10, Color.White), Color.Transparent)

        If _Checked Then
            G.FillRectangle(LGB1, New Rectangle(2, 2, (Width / 2) - 2, Height - 4))
            G.FillRectangle(HB1, New Rectangle(2, 2, (Width / 2) - 2, Height - 4))
            G.DrawString("On", Font, TB, New Point(36, 6))
        ElseIf Not _Checked Then
            G.FillRectangle(LGB1, New Rectangle((Width / 2) - 1, 2, (Width / 2) - 1, Height - 4))
            G.FillRectangle(HB1, New Rectangle((Width / 2) - 1, 2, (Width / 2) - 1, Height - 4))
            G.DrawString("Off", Font, TB, New Point(5, 6))
        End If
        DrawBorderss(New Pen(New SolidBrush(Color.FromArgb(200, 200, 200))), G)
        DrawBorderss(New Pen(New SolidBrush(Color.FromArgb(150, 150, 150))), 1, G)

    End Sub
End Class
