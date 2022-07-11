Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
<DefaultEvent("CheckedChanged")> _
Public Class Switch_Genesis
    Inherits ThemeControl151

    Sub New()
        BackColor = Color.FromArgb(37, 37, 37)
        LockWidth = 50
        LockHeight = 20
        Checked = False

        SetColor("OnGradientA", Color.FromArgb(63, 83, 100))
        SetColor("OnGradientB", Color.FromArgb(87, 127, 151))
        SetColor("OffGradientA", Color.FromArgb(23, 23, 23))
        SetColor("OffGradientB", Color.FromArgb(33, 33, 33))
        SetColor("SwitchColor", Color.FromArgb(25, 25, 25))
        SetColor("TopGloss", Color.FromArgb(62, Color.White))
        SetColor("BottomGloss", Color.FromArgb(30, Color.White))
        SetColor("SwitchBorder", Color.Black)
        SetColor("SwitchInsetBorder", Color.FromArgb(47, 47, 47))
        SetColor("BorderColor", Color.Black)
        SetColor("TopInset", Color.FromArgb(45, 45, 45))
        SetColor("BottomInset", Color.FromArgb(70, 70, 70))
    End Sub

    Dim C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12 As Color
    Protected Overrides Sub ColorHook()
        C1 = GetColor("OnGradientA")
        C2 = GetColor("OnGradientB")
        C3 = GetColor("OffGradientA")
        C4 = GetColor("OffGradientB")
        C5 = GetColor("SwitchColor")
        C6 = GetColor("TopGloss")
        C7 = GetColor("BottomGloss")
        C8 = GetColor("SwitchBorder")
        C9 = GetColor("SwitchInsetBorder")
        C10 = GetColor("BorderColor")
        C11 = GetColor("TopInset")
        C12 = GetColor("BottomInset")
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)
        Select Case Checked
            Case True
                DrawGradient(C1, C2, ClientRectangle, 90S)
                G.FillRectangle(New SolidBrush(C5), New Rectangle(Width - 19, 1, 17, Height - 4))
                G.DrawRectangle(New Pen(New SolidBrush(C8)), New Rectangle(Width - 20, 0, 20, Height - 1))
                G.DrawRectangle(New Pen(New SolidBrush(C9)), New Rectangle(Width - 19, 1, 16, Height - 4))
                DrawGradient(C6, C7, New Rectangle(Width - 19, 2, 17, Height / 2 - 2), 90S)
                G.DrawString("ON", Font, Brushes.White, 6, 3)
            Case False
                DrawGradient(C3, C4, ClientRectangle, 90S)
                G.FillRectangle(New SolidBrush(C5), New Rectangle(1, 1, 20, Height - 1))
                G.DrawRectangle(New Pen(New SolidBrush(C8)), New Rectangle(0, 0, 20, Height - 1))
                G.DrawRectangle(New Pen(New SolidBrush(C9)), New Rectangle(2, 2, 17, Height - 5))
                DrawGradient(C6, C7, New Rectangle(1, 1, 19, Height / 2 - 2), 90S)
                G.DrawString("OFF", Font, Brushes.White, 22, 3)
        End Select
        DrawCorners(BackColor, New Rectangle(1, 2, Width - 1, Height - 4))
        G.DrawLine(New Pen(New SolidBrush(C11)), 0, 0, Width, 0)
        G.DrawLine(New Pen(New SolidBrush(C11)), 0, 0, 0, Height)
        G.DrawLine(New Pen(New SolidBrush(C11)), Width - 1, 0, Width - 1, Height)
        G.DrawLine(New Pen(New SolidBrush(C12)), 0, Height - 1, Width, Height - 1)
        DrawBorders(New Pen(New SolidBrush(C10)), 1)
        DrawCorners(BackColor)
    End Sub

    Private _Checked As Boolean
    <Category("ToolsBox Herramienta")> _
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
        MyBase.OnClick(e)
    End Sub

    Event CheckedChanged(ByVal sender As Object)
End Class
